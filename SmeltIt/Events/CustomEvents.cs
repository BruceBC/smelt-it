namespace SmeltIt.Events
{
    internal class CustomEvents
    {
        internal event EventHandler<ItemPlacedInMachineEventArgs> ItemPlacedInMachine = delegate
        { };

        internal void OnItemPlacedInMachine(object? sender, ItemPlacedInMachineEventArgs e)
        {
            ItemPlacedInMachine.Invoke(sender, e);
        }
    }
}
