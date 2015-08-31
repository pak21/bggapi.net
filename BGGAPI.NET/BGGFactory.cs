// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using BGGAPI.Raw;
using BGGAPI.Raw.Collection;
using BGGAPI.Raw.Things;

namespace BGGAPI
{
    /// <summary>
    /// Factory class for generating the top-level objects
    /// </summary>
    internal static class BGGFactory
    {
        public static BGGCollection CreateCollection(Collection rawCollection)
        {
            return BGGAutomapper.Map<BGGCollection>(rawCollection);
        }

        public static BGGThings CreateThings(Things rawThings)
        {
            return BGGAutomapper.Map<BGGThings>(rawThings);
        }
    }
}
