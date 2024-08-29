using StardewValley;

namespace SmeltIt.API
{
    internal class ObjectAssets
    {
        /// <summary>
        /// The list of item ids that will be modded.
        /// </summary>
        /// <remarks>
        /// Add any new item ids here.
        ///     <list type="bullet">
        ///         <item>
        ///             <term>(O)74</term>
        ///             <description>Prismatic Shard</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        private static List<string> itemIds = new List<string>() { "(O)74" };

        internal static void EditItem(Item item, Action<Item> onEditItem)
        {
            if (itemIds.Contains(item.QualifiedItemId))
                onEditItem(item);
        }
    }
}
