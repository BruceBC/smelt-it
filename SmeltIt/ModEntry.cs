using Force.DeepCloner;
using Microsoft.Xna.Framework;
using SmeltIt.API;
using SmeltIt.Extensions;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Extensions;
using StardewValley.GameData.FruitTrees;
using StardewValley.GameData.Machines;
using StardewValley.GameData.WildTrees;
using StardewValley.TerrainFeatures;

namespace SmeltIt
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        /*********
        ** Properties
        *********/
        /// <summary>The mod configuration from the player.</summary>
        private ModConfig? Config;

        /// <summary>The machine output rule action references.</summary>
        private MachineOutputRules rules = new MachineOutputRules();

        /// <summary> The default machine output rules.</summary>
        private MachineOutputRules defaultRules = new MachineOutputRules();

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            this.Config = this.Helper.ReadConfig<ModConfig>();
            helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
            helper.Events.GameLoop.DayStarted += this.OnDayStarted;
            helper.Events.Content.AssetRequested += this.OnAssetRequested;
        }

        /*********
        ** Private methods
        *********/
        /// <inheritdoc cref="IGameLoopEvents.GameLaunched"/>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnGameLaunched(object? sender, GameLaunchedEventArgs e)
        {
            // Register with Generic Mod Config Menu's API
            Configurator.Register(this.Config, this.Helper, this.ModManifest, this.OnConfigSave);
        }

        private void OnDayStarted(object? sender, DayStartedEventArgs e)
        {
            if (Context.IsMainPlayer)
            {
                Utility.ForEachLocation(location =>
                {
                    foreach (
                        (Vector2 tile, TerrainFeature feature) in location.terrainFeatures.Pairs
                    )
                    {
                        // TODO: Add config for fruit trees to instantly grow max number of fruit
                        if (feature is FruitTree fruitTree)
                        {
                            // var treeId = fruitTree.treeId;
                            bool canAddMoreFruit =
                                fruitTree.fruit.Count < FruitTree.maxFruitsOnTrees;
                            while (canAddMoreFruit)
                            {
                                canAddMoreFruit = fruitTree.TryAddFruit();
                            }
                        }

                        if (feature is Tree tree && tree.tapped.Value)
                        {
                            StardewValley.Object objectAtTile = location.getObjectAtTile(
                                (int)tile.X,
                                (int)tile.Y
                            );
                            var treeType = tree.treeType.Value;

                            if (objectAtTile.IsTapper())
                            {
                                // Update to instantly complete
                                StardewValley.Object tapper = objectAtTile;

                                // TODO: Extend tree to have a GlobalID
                                var globalId = $"{tapper.QualifiedItemId}:{tree.treeType.Value}";
                                var configValue = Configurator.GetConfigValue(
                                    this.Config,
                                    globalId
                                );
                                if (configValue)
                                {
                                    tapper.MinutesUntilReady = 0;
                                    tapper.minutesElapsed(0);
                                }
                            }
                        }
                    }

                    return true;
                });
            }
            else
            {
                this.Monitor.Log(
                    "Disabled instant fruit mod; only works for the main player in multiplayer.",
                    LogLevel.Warn
                );
                this.Monitor.Log(
                    "Disabled instant tapper mod; only works for the main player in multiplayer.",
                    LogLevel.Warn
                );
            }
        }

        /// <summary>
        /// Modify MachineOutputRules when the config changes.
        /// </summary>
        /// <param name="config">The ModConfig class.</param>
        private void OnConfigSave(ModConfig config)
        {
            this.Config = config;

            foreach ((string ruleId, MachineOutputRule rule) in rules)
            {
                Bridge.SetMinutesUntilReady(this.Config, rule, defaultRules);
            }
        }

        /// <inheritdoc cref="IContentEvents.AssetRequested"/>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnAssetRequested(object? sender, AssetRequestedEventArgs e)
        {
            if (e.NameWithoutLocale.IsEquivalentTo("Data/Machines"))
                e.Edit(EditMachineAssets);
        }

        /// <summary>
        /// Edit loaded machine assets.
        /// </summary>
        /// <param name="asset">The machine asset data.</param>
        private void EditMachineAssets(IAssetData asset)
        {
            MachineAssets.EditAssets(asset, OnEditMachineOutputRule);
        }

        /// <summary>
        /// Change the MachineOutputRule to be instantaneous when the config value is set or
        /// defaulted to its original value when not set.
        /// </summary>
        /// <param name="itemId">The id of the item.</param>
        /// <param name="rule">The MachineOutputRule.</param>
        private void OnEditMachineOutputRule(string itemId, MachineOutputRule rule)
        {
            // Set globally unique id
            rule.SetGlobalID(itemId);

            // Dynamically adds a shallow copy of the machine output rule
            var defaultRule = rule.ShallowClone();
            defaultRules.Add(defaultRule.Id, defaultRule);

            // Set MinutesUntilReady to be 0 or to default
            Bridge.SetMinutesUntilReady(this.Config, rule, defaultRule);

            // Add machine output rule
            rules.Add(rule.Id, rule);
        }
    }
}
