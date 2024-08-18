using SmeltIt.Extensions;
using StardewModdingAPI;
using StardewValley.GameData.Machines;

namespace SmeltIt.API
{
    internal static class MachineAssets
    {
        /// <summary>
        /// Finds a MachineOutputRule for the given id.
        /// </summary>
        /// <param name="rules">The lookup table of MachineOutputRules.</param>
        /// <param name="ruleId">The MachineOutputRule id.</param>
        /// <returns>Returns a MachineOutputRule</returns>
        internal static MachineOutputRule GetRule(MachineOutputRules rules, string ruleId)
        {
            return rules[ruleId];
        }

        /// <summary>
        /// Sets the MinutesUntilReady property on a given rule.
        /// </summary>
        /// <param name="rules">The lookup table of MachineOutputRules.</param>
        /// <param name="ruleId">The MachineOutputRule id.</param>
        /// <param name="minutesUntilReady">The MinutesUntilReady property that will be updated.</param>
        internal static void SetRule(MachineOutputRules rules, string ruleId, int minutesUntilReady)
        {
            SetRule(rules[ruleId], minutesUntilReady);
        }

        // Updates minutes until ready

        /// <summary>
        /// Set the MinutesUntilReady propert on a given rule.
        /// </summary>
        /// <param name="rule">The MachineOutputRule.</param>
        /// <param name="minutesUntilReady">The MinutesUntilReady property that will be updated.</param>
        internal static void SetRule(MachineOutputRule rule, int minutesUntilReady)
        {
            rule.MinutesUntilReady = minutesUntilReady;
        }

        /// <summary>
        /// The list of machine asset ids that will be modded.
        /// </summary>
        /// <remarks>
        /// Add any new machine asset ids here.
        ///     <list type="bullet">
        ///         <item>
        ///             <term>(BC)13</term>
        ///             <description>Furnace</description>
        ///         </item>
        ///         <item>
        ///             <term>(BC)15</term>
        ///             <description>Preserves Jar</description>
        ///         </item>
        ///         <item>
        ///             <term>(BC)16</term>
        ///             <description>Cheese Press</description>
        ///         </item>
        ///         <item>
        ///             <term>(BC)19</term>
        ///             <description>Oil Maker</description>
        ///         </item>
        ///         <item>
        ///             <term>(BC)20</term>
        ///             <description>Recyling Machine</description>
        ///         </item>
        ///         <item>
        ///             <term>(BC)24</term>
        ///             <description>Mayonnaise Machine</description>
        ///         </item>
        ///         <item>
        ///             <term>(BC)25</term>
        ///             <description>Seed Maker</description>
        ///         </item>
        ///         <item>
        ///             <term>(BC)114</term>
        ///             <description>Charcoal Kiln</description>
        ///         </item>
        ///         <item>
        ///             <term>(BC)FishSmoker</term>
        ///             <description>Fisher Smoker</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        private static List<string> machineIds = new List<string>()
        {
            "(BC)13",
            "(BC)15",
            "(BC)16",
            "(BC)19",
            "(BC)20",
            "(BC)24",
            "(BC)25",
            "(BC)114",
            "(BC)FishSmoker"
        };

        /// <summary>
        /// Edits a machine asset.
        /// </summary>
        /// <param name="asset">The machine asset.</param>
        /// <param name="onEditMachineOutputRule">The callback to edit the MachineOutputRule.</param>
        internal static void EditAssets(
            IAssetData asset,
            Action<string, MachineOutputRule> onEditMachineOutputRule
        )
        {
            var data = asset.AsDictionary<string, MachineData>().Data;

            foreach ((string itemID, MachineData itemData) in data)
            {
                if (machineIds.Contains(itemID))
                    itemData.OutputRules.ForEach(rule => onEditMachineOutputRule(itemID, rule));
            }
        }
    }
}
