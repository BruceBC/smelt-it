using SmeltIt.Extensions;
using StardewValley.GameData.Machines;

namespace SmeltIt.API
{
    /// <summary>
    /// Bridge between Configurator and MachineAssets.
    /// </summary>
    internal static class Bridge
    {
        /// <summary>
        /// Sets machine asset rule to instaneous or default depending on configuration setting.
        /// </summary>
        /// <param name="config">The Stardew Valley ModConfig.</param>
        /// <param name="rule">The MachineOutputRule to be changed.</param>
        /// <param name="defaultRules">The list of MachineOutputRules with their original settings.</param>
        internal static void SetMinutesUntilReady(
            ModConfig? config,
            MachineOutputRule rule,
            MachineOutputRules defaultRules
        )
        {
            SetMinutesUntilReady(config, rule, MachineAssets.GetRule(defaultRules, rule.Id));
        }

        /// <summary>
        /// Sets machine asset rule to instaneous or default depending on configuration setting.
        /// </summary>
        /// <param name="config">The Stardew Valley ModConfig.</param>
        /// <param name="rule">The MachineOutputRule to be changed.</param>
        /// <param name="defaultRule">The MachineOutputRule with its original settings.</param>
        internal static void SetMinutesUntilReady(
            ModConfig? config,
            MachineOutputRule rule,
            MachineOutputRule defaultRule
        )
        {
            var isInstant = Configurator.GetConfigValue(config, rule.Id);

            if (isInstant)
                MachineAssets.SetRule(rule, 0);
            else
                MachineAssets.SetRule(rule, defaultRule.MinutesUntilReady);
        }
    }
}
