using Force.DeepCloner;
using SmeltIt.API;
using SmeltIt.Extensions;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley.GameData.Machines;

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
        /// <param name="rule">The MachineOutputRule.</param>
        private void OnEditMachineOutputRule(MachineOutputRule rule)
        {
            // Dynamically add a shallow copy of the machine output rule
            var defaultRule = rule.ShallowClone();
            defaultRules.Add(defaultRule.Id, defaultRule);

            // Set MinutesUntilReady to be 0 or to default
            Bridge.SetMinutesUntilReady(this.Config, rule, defaultRule);

            // Store a reference to the rule, so that we can modify it later if the config changes
            rules.Add(rule.Id, rule);
        }
    }
}
