using SmeltIt.Extensions;
using StardewValley;
using StardewValley.GameData.Machines;

namespace SmeltIt.API
{
    /// <summary>
    /// Bridge between Configurator and MachineAssets.
    /// </summary>
    internal static class Bridge
    {
        /// <summary>
        /// Removes the item from the machine without destroying it if the item's cloneable config option is disabled.
        /// </summary>
        /// <param name="config">The Stardew Valley ModConfig.</param>
        /// <param name="machine">The machine with the cloneable item.</param>
        /// <param name="activeItem">The cloneable item.</param>
        /// <param name="matchingQID">The qualified item id of the cloneable item.</param>
        /// <param name="pickaxe">The tool used to remove the item from the machine without destroying it.</param>
        internal static void DropItemFromMachineIfNotCloneable(
            ModConfig? config,
            StardewValley.Object machine,
            Item activeItem,
            string matchingQID,
            Tool pickaxe
        )
        {
            var isNotCloneable = !Configurator.GetConfigValue(config, activeItem.QualifiedItemId);

            if (isNotCloneable && activeItem.QualifiedItemId == matchingQID)
                machine.performToolAction(pickaxe);
        }

        /// <summary>
        /// Adds or removes the banned context tag from the item to affect cloneability.
        /// </summary>
        /// <param name="config">The Stardew Valley ModConfig.</param>
        /// <param name="item">The Item to be made cloneable</param>
        /// <param name="bannedContextTag">The context tag that bans cloneability.</param>
        internal static void MakeCloneable(ModConfig? config, Item item, string bannedContextTag)
        {
            var isCloneable = Configurator.GetConfigValue(config, item.QualifiedItemId);
            var contextTags = item.GetContextTags();

            if (isCloneable)
                contextTags.Remove(bannedContextTag);
            else
                contextTags.Add(bannedContextTag);
        }

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

        /// <summary>
        /// Sets machine to complete instantly or defaults to remaining time depending on configuration setting.
        /// </summary>
        /// <param name="config">The Stardew Valley ModConfig.</param>
        /// <param name="machine">The Machine to be changed.</param>
        internal static void SetMinutesUntilReady(ModConfig? config, StardewValley.Object machine)
        {
            var isInstant = Configurator.GetConfigValue(config, machine.QualifiedItemId);

            if (isInstant)
                MachineAssets.SetMachine(machine, 0);
        }
    }
}
