using StardewValley;

namespace SmeltIt.Extensions
{
    /// <summary>
    /// StardewValley.Object Extension Methods.
    /// </summary>
    internal static class SObjectExtension
    {
        /// <summary>
        /// Trys to cast a StardewValley.Object into an Item.
        /// </summary>
        /// <param name="sObject">The StardewValley.Object to be cast as an Item.</param>
        /// <returns>Returns an Item or null.</returns>
        internal static Item? AsItem(this StardewValley.Object sObject)
        {
            if (sObject is Item item)
                return item;

            return null;
        }
    }
}
