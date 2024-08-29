using Force.DeepCloner;
using Microsoft.Xna.Framework;
using SmeltIt.API;
using SmeltIt.Events;
using SmeltIt.Extensions;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.GameData.Machines;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;

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

        /// <summary>The default machine output rules.</summary>
        private MachineOutputRules defaultRules = new MachineOutputRules();

        /// <summary>The custom patcher events.</summary>
        private CustomEvents customEvents = new CustomEvents();

        /// <summary>The pickaxe.</summary>
        private Tool pickaxe = new Pickaxe();

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
            helper.Events.Player.InventoryChanged += this.OnInventoryChanged;
            customEvents.ItemPlacedInMachine += this.OnItemPlacedInMachine;
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

            // Apply patches
            Patcher.Initialize(this.Monitor, this.customEvents);
            Patcher.ApplyPatches(this.ModManifest);
        }

        /// <inheritdoc cref="IGameLoopEvents.DayStarted"/>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnDayStarted(object? sender, DayStartedEventArgs e)
        {
            Utility.ForEachItem(EditItem);

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
        /// Modify Machine and MachineOutputRules when the config changes.
        /// </summary>
        /// <param name="config">The ModConfig class.</param>
        private void OnConfigSave(ModConfig config)
        {
            // Update the config
            this.Config = config;

            // Allows crystalrium to be completed instantly when the config value is enabled
            // Allows prismatic shard to be made cloneable when the config value is enabled
            Utility.ForEachItem(EditItem);

            // Modifies machine rules according to config values
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

        /// <inheritdoc cref="IPlayerEvents.InventoryChanged"/>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnInventoryChanged(object? sender, InventoryChangedEventArgs e)
        {
            if (e.Added != null)
                foreach (Item item in e.Added)
                    EditItem(item);
        }

        // TODO: Add ability to clone Coal, Wood, Copper Ore, Iron Ore, Gold Ore, Iridium Ore, RadioactiveOre

        /// <summary>
        /// Event handler when an item is placed in a machine. Intended to be used with <see cref="Patcher"/>.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnItemPlacedInMachine(object? sender, ItemPlacedInMachineEventArgs e)
        {
            MachineAssets.EditMachine(e.Machine, OnEditMachine);
        }

        /// <summary>
        /// Helper to edit items when using <see cref="Utility.ForEachItem"/>.
        /// </summary>
        /// <param name="item">The StardewValley item.</param>
        /// <returns></returns>
        private bool EditItem(Item item)
        {
            if (item is StardewValley.Object machine)
                MachineAssets.EditMachine(machine, OnEditMachine, OnMachineSideEffects);

            if (item.QualifiedItemId == StardewValley.Object.prismaticShardQID)
                ObjectAssets.EditItem(item, OnEditItem);

            return true;
        }

        /// <summary>
        /// Handles callback to edit the machine object.
        /// </summary>
        /// <param name="machine">The machine to be edited.</param>
        private void OnEditMachine(StardewValley.Object machine)
        {
            Bridge.SetMinutesUntilReady(this.Config, machine);
        }

        /// <summary>
        /// Sometimes it can be useful to perform side effects after a machine has been edited.
        ///
        /// For example, after disabling the prismatic shard cloneable config option, we want to remove all prismatic
        /// shards from the machines without destroying them.
        /// </summary>
        /// <param name="machine">The machine to perform side effects on.</param>
        private void OnMachineSideEffects(StardewValley.Object machine)
        {
            if (machine.QualifiedItemId == "(BC)21")
                PerformCrystalariumSideEffects(machine);
            else
                this.Monitor.Log(
                    $"No side effects to perform for this machine {machine.QualifiedItemId}.",
                    LogLevel.Debug
                );
        }

        /// <summary>
        /// Perform side effects on the crystalarium after it has been edited.
        ///
        /// For example, this allows us to remove all cloneable items from the crystalarium without destroying them
        /// when the config option is disabled.
        /// </summary>
        /// <param name="machine">The crystalarium machine to perform side effects on.</param>
        private void PerformCrystalariumSideEffects(StardewValley.Object machine)
        {
            Item? item = machine.AsItem();

            item?.ForEachItem(
                (Item activeItem, Action remove, Action<Item> replaceWith) =>
                {
                    Bridge.DropItemFromMachineIfNotCloneable(
                        this.Config,
                        machine,
                        activeItem,
                        StardewValley.Object.prismaticShardQID,
                        this.pickaxe
                    );

                    return true;
                }
            );
        }

        /// <summary>
        /// Enables and disables the cloneability of an item based on the config option value.
        /// </summary>
        /// <param name="item">The item to be made cloneable.</param>
        private void OnEditItem(Item item)
        {
            Bridge.MakeCloneable(this.Config, item, "crystalarium_banned");
        }
    }
}
