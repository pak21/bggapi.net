// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using System;
using System.Collections.Generic;

namespace BGGAPI
{
    public class BGGCollection
    {
        // ReSharper disable UnusedAutoPropertyAccessor.Local - called via reflection
        public IList<Item> Items { get; private set; }
        public string TermsOfUse { get; private set; }

        public class Item
        {
            public int Id { get; private set; }
            public string Type { get; private set; }
            public string Subtype { get; private set; }
            public int CollectionId { get; private set; }
            public string Name { get; private set; }
            public string YearPublished { get; private set; }
            public Uri Image { get; private set; }
            public Uri Thumbnail { get; private set; }
            public int NumberOfPlays { get; private set; }

            public bool Owned { get; private set; }
            public bool PreviouslyOwned { get; private set; }
            public bool AvailableForTrade { get; private set; }
            public bool WantInTrade { get; private set; }
            public bool WantToPlay { get; private set; }
            public bool WantToBuy { get; private set; }
            public bool OnWishlist { get; private set; }
            public bool Preordered { get; private set; }
            public DateTime StatusLastModified { get; private set; }

            public int MinimumPlayers { get; private set; }
            public int MaximumPlayers { get; private set; }
            public TimeSpan PlayingTime { get; private set; }
            public int NumberOfOwners { get; private set; }

            public float? RatingFromThisUser { get; private set; }
            public int UsersRatingThisItem { get; private set; }
            public float AverageRating { get; private set; }
            public float BayesianAverageRating { get; private set; }
            public float RatingStandardDeviation { get; private set; }
            public int Median { get; private set; }

            public IList<Ranking> Rankings { get; set; }
        }

        public class Ranking
        {
            public string Type { get; private set; }
            public int IdWithinType { get; private set; }
            public string Name { get; private set; }
            public string FriendlyName { get; private set; }
            public int? Position { get; private set; }
            public float? BayesianAverageRating { get; private set; }
        }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}
