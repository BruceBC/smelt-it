using StardewValley;
using StardewValley.GameData.Machines;

namespace SmeltIt.Events
{
    internal class ItemPlacedInMachineEventArgs : EventArgs
    {
        internal StardewValley.Object Machine { get; private set; }
        internal MachineData MachineData { get; private set; }
        internal MachineOutputRule MachineOutputRule { get; private set; }
        internal Item InputItem { get; private set; }
        internal Farmer Who { get; private set; }
        internal GameLocation Location { get; private set; }
        internal bool Probe { get; private set; }

        internal ItemPlacedInMachineEventArgs(
            StardewValley.Object machine,
            MachineData machineData,
            MachineOutputRule outputRule,
            Item inputItem,
            Farmer who,
            GameLocation location,
            bool probe
        )
        {
            this.Machine = machine;
            this.MachineData = machineData;
            this.MachineOutputRule = outputRule;
            this.InputItem = inputItem;
            this.Who = who;
            this.Location = location;
            this.Probe = probe;
        }
    }
}
