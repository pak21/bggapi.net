// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using System.Collections.Generic;

namespace BGGAPI
{
    public class BGGThingsRequest
    {
        /// <summary>
        /// The ids of the things to retrieve
        /// </summary>
        public List<int> Id { get; set; }

        // TODO: work out serialization etc
        // public List<SomeType> Type { get; set; }

        // TODO: not implemented yet - need to deal with multiple different <item> tags
        //public bool Versions { get; set; }

        /// <summary>
        /// If set to true, returns the associated videos for the item
        /// </summary>
        public bool Videos { get; set; }

        /// <summary>
        /// If set to true, requests that expanded stats, rating and ranking information be returned
        /// </summary>
        public bool Stats { get; set; }

        // TODO: not implemented yet
        //public bool Historical { get; set; }

        // TODO: not implemented yet
        //public bool Marketplace { get; set; }

        // TODO: not implemented yet
        //public bool Comments { get; set; }

        // TODO: not implemented yet
        //public bool RatingComments { get; set; }

        // TODO: no point having this until we implement something that needs it (Historical, Comments, RatingComments)
        //public int Page { get; set; }

        // TODO: no point having this until we implement something that needs it (Historical, Comments, RatingComments)
        //public int PageSize { get; set; }
    }
}
