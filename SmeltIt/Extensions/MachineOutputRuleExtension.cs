using StardewValley.GameData.Machines;

namespace SmeltIt.Extensions
{
    /// <summary>
    /// MachineOuputRule Extension Methods.
    /// </summary>
    internal static class MachineOutputRuleExtension
    {
        /// <summary>
        /// Set a globally unique id as the MachineOutputRule's identifier, as recommended by <see cref="MachineOutputRule.Id"/>.
        /// </summary>
        /// <param name="machineOutputRule">The MachineOutputRule to be modified.</param>
        /// <param name="itemId">The item id that will be used to make the id unique.</param>
        internal static void SetGlobalID(this MachineOutputRule machineOutputRule, string itemId)
        {
            var globalId = $"{itemId}:{machineOutputRule.Id}";

            if (machineOutputRule.Id != globalId)
                machineOutputRule.Id = $"{itemId}:{machineOutputRule.Id}";
        }
    }
}
