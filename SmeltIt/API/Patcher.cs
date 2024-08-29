using HarmonyLib;
using SmeltIt.Events;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.Machines;

namespace SmeltIt.API
{
    // TODO: Add documentation
    internal static class Patcher
    {
        private static IMonitor? Monitor;
        private static CustomEvents? CustomEvents;

        internal static void Initialize(IMonitor monitor, CustomEvents customEvents)
        {
            Monitor = monitor;
            CustomEvents = customEvents;
        }

        internal static void ApplyPatches(IManifest manifest)
        {
            var harmony = new Harmony(manifest.UniqueID);

            harmony.Patch(
                original: AccessTools.Method(
                    typeof(StardewValley.Object),
                    nameof(StardewValley.Object.OutputMachine)
                ),
                postfix: new HarmonyMethod(typeof(Patcher), nameof(OutputMachine))
                {
                    priority = Priority.HigherThanNormal
                }
            );
        }

        private static bool OutputMachine(
            bool __result,
            StardewValley.Object __instance,
            MachineData machine,
            MachineOutputRule outputRule,
            Item inputItem,
            Farmer who,
            GameLocation location,
            bool probe
        )
        {
            // Check for any null objcect references to prevent unexpected game crashes
            if (
                __result
                && __instance != null
                && machine != null
                && outputRule != null
                && inputItem != null
                && who != null
                && location != null
            )
            {
                var args = new ItemPlacedInMachineEventArgs(
                    __instance,
                    machine,
                    outputRule,
                    inputItem,
                    who,
                    location,
                    probe
                );
                CustomEvents?.OnItemPlacedInMachine(typeof(Patcher), args);
                Monitor?.Log(
                    $"Player Placed an Item {inputItem.QualifiedItemId} in the Machine {__instance.QualifiedItemId}",
                    LogLevel.Debug
                );
            }

            return __result;
        }
    }
}
