using BGGAPI.BGGCollectionObjects;
using BGGAPI.BGGThingsObjects;

namespace BGGAPI
{
    /// <summary>
    /// Factory class for generating the top-level objects
    /// </summary>
    internal static class BGGFactory
    {
        public static BGGCollection CreateCollection(Collection rawCollection)
        {
            return BGGCollection.Mapper.Map<BGGCollection>(rawCollection);
        }

        public static BGGThings CreateThings(Things rawThings)
        {
            return BGGThings.Mapper.Map<BGGThings>(rawThings);
        }
    }
}
