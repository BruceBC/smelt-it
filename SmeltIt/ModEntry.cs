using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.GameData.Machines;

namespace SmeltIt
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        // Furnace
        const string Default_Bouquet = "Default_Bouquet";
        const string Default_CopperOre = "Default_CopperOre";
        const string Default_Quartz = "Default_Quartz";
        const string Default_FireQuartz = "Default_FireQuartz";
        const string Default_IronOre = "Default_IronOre";
        const string Default_GoldOre = "Default_GoldOre";
        const string Default_IridiumOre = "Default_IridiumOre";
        const string Default_RadioactiveOre = "Default_RadioactiveOre";

        // Recyling Machine
        const string Default_Trash = "Default_Trash";
        const string Default_Driftwood = "Default_Driftwood";
        const string Default_BrokenGlasses = "Default_BrokenGlasses";
        const string Default_BrokenCd = "Default_BrokenCd";
        const string Default_SoggyNewspaper = "Default_SoggyNewspaper";

        /*********
        ** Properties
        *********/
        /// <summary>The default furnace output rules.</summary>
        private Dictionary<string, int> defaultMachineOutputRules = new Dictionary<string, int>()
        {
            // Furnace
            [Default_Bouquet] = 10,
            [Default_CopperOre] = 30,
            [Default_Quartz] = 90,
            [Default_FireQuartz] = 90,
            [Default_IronOre] = 120,
            [Default_GoldOre] = 300,
            [Default_IridiumOre] = 480,
            [Default_RadioactiveOre] = 560,
            // Recyling Machine
            [Default_Trash] = 60,
            [Default_Driftwood] = 60,
            [Default_BrokenGlasses] = 60,
            [Default_BrokenCd] = 60,
            [Default_SoggyNewspaper] = 60
        };

        /// <summary>The mod configuration from the player.</summary>
        private ModConfig Config;

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            this.Config = this.Helper.ReadConfig<ModConfig>();
            helper.Events.Content.AssetRequested += this.OnAssetRequested;
        }

        /*********
        ** Private methods
        *********/
        /// <inheritdoc cref="IContentEvents.AssetRequested"/>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnAssetRequested(object? sender, AssetRequestedEventArgs e)
        {
            if (e.NameWithoutLocale.IsEquivalentTo("Data/Machines"))
                e.Edit(EditMachineAssets);
        }

        private void EditMachineAssets(IAssetData asset)
        {
            var data = asset.AsDictionary<string, MachineData>().Data;

            foreach ((string itemID, MachineData itemData) in data)
            {
                // Furnace
                if (itemID == "(BC)13")
                {
                    foreach (MachineOutputRule action in itemData.OutputRules)
                    {
                        ToggleAction(action);
                    }
                }
                // Recyling Machine
                if (itemID == "(BC)20")
                {
                    foreach (MachineOutputRule action in itemData.OutputRules)
                    {
                        ToggleAction(action);
                    }
                }
            }
        }

        private void ToggleAction(MachineOutputRule action)
        {
            action.MinutesUntilReady = GetConfigValue(action.Id)
                ? 0
                : defaultMachineOutputRules[action.Id];
        }

        private bool GetConfigValue(string actionId)
        {
            switch (actionId)
            {
                case Default_Bouquet:
                    return this.Config.InstantBouquet;
                case Default_CopperOre:
                    return this.Config.InstantCopperOre;
                case Default_Quartz:
                    return this.Config.InstantQuartz;
                case Default_FireQuartz:
                    return this.Config.InstantFireQuartz;
                case Default_IronOre:
                    return this.Config.InstantIronOre;
                case Default_GoldOre:
                    return this.Config.InstantGoldOre;
                case Default_IridiumOre:
                    return this.Config.InstantIridiumOre;
                case Default_RadioactiveOre:
                    return this.Config.InstantRadioactiveOre;
                case Default_Trash:
                    return this.Config.InstantTrash;
                case Default_Driftwood:
                    return this.Config.InstantDriftwood;
                case Default_BrokenGlasses:
                    return this.Config.InstantBrokenGlasses;
                case Default_BrokenCd:
                    return this.Config.InstantBrokenCd;
                case Default_SoggyNewspaper:
                    return this.Config.InstantSoggyNewspaper;
                default:
                    return false;
            }
        }
    }
}
