using System.Collections.Generic;

namespace ListMajik
{
    public static class CopyItems
    {
        public static void CopyItemsFrom<T>(this ICollection<T> collection, ICollection<T> inputList)
        {
            foreach (var item in inputList)
                if (! collection.Contains(item))
                    collection.Add(item);

            var toRemove = new List<T>();

            foreach (var item in collection)
                if(! inputList.Contains(item))
                    toRemove.Add(item);

            foreach (var item in toRemove)
                collection.Remove(item);
        }
    }
}