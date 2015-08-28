// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using System;
using System.Collections.Generic;

namespace BGGAPI.Raw
{
    public class Collection
    {
        public int TotalItems { get; set; }
        public string TermsOfUse { get; set; }
        public List<CollectionItem> Items { get; set; }
    }

    public class CollectionItem
    {
        public int ObjectId { get; set; }
        public string ObjectType { get; set; }
        public string Subtype { get; set; }
        public int CollId { get; set; }

        public string Name { get; set; }
        public string YearPublished { get; set; }
        public string Image { get; set; }
        public string Thumbnail { get; set; }
        public Stats Stats { get; set; }
        public Status Status { get; set; }
        public int NumPlays { get; set; }
    }

    public class Stats
    {
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int PlayingTime { get; set; }
        public int NumOwned { get; set; }

        public Ratings Rating { get; set; }
    }

    public class Status
    {
        public int Own { get; set; }
        public int PrevOwned { get; set; }
        public int ForTrade { get; set; }
        public int Want { get; set; }
        public int WantToPlay { get; set; }
        public int WantToBuy { get; set; }
        public int Wishlist { get; set; }
        public int Preordered { get; set; }
        public DateTime LastModified { get; set; }
    }
}
