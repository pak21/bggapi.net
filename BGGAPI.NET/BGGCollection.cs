// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using System;
using System.Collections.Generic;
using System.Linq;

namespace BGGAPI
{
    public class BGGCollection
    {
        public BGGCollection(BGGCollectionObjects.Collection rawCollection)
        {
            Items = rawCollection.Items.Select(i => new Item(i)).ToList();
            TermsOfUse = rawCollection.TermsOfUse;
        }

        public IList<Item> Items { get; private set; }
        public string TermsOfUse { get; private set; }

        public class Item
        {
            public Item(BGGCollectionObjects.Item rawItem)
            {
                Type = rawItem.ObjectType;
                Id = rawItem.ObjectId;
                Subtype = rawItem.Subtype;
                CollectionId = rawItem.CollId;
                Name = rawItem.Name;
                YearPublished = rawItem.YearPublished;
                Image = rawItem.Image;
                Thumbnail = rawItem.Thumbnail;
                NumberOfPlays = rawItem.NumPlays;

                Owned = rawItem.Status.Own != 0;
                PreviouslyOwned = rawItem.Status.PrevOwned != 0;
                AvailableForTrade = rawItem.Status.ForTrade != 0;
                WantInTrade = rawItem.Status.Want != 0;
                WantToPlay = rawItem.Status.WantToPlay != 0;
                WantToBuy = rawItem.Status.WantToBuy != 0;
                OnWishlist = rawItem.Status.Wishlist != 0;
                Preordered = rawItem.Status.Preordered != 0;
                StatusLastModified = rawItem.Status.LastModified;

                if (rawItem.Stats != null)
                {
                    var stats = rawItem.Stats;

                    MinimumPlayers = stats.MinPlayers;
                    MaximumPlayers = stats.MaxPlayers;
                    PlayingTime = TimeSpan.FromMinutes(stats.PlayingTime);
                    NumberOfOwners = stats.NumOwned;

                    var rating = stats.Rating;

                    RatingFromThisUser = rating.value == "N/A" ? (float?)null : float.Parse(rating.value);
                    UsersRatingThisItem = rating.UsersRated.value;
                    AverageRating = rating.Average.value;
                    BayesianAverageRating = rating.BayesAverage.value;
                    RatingStandardDeviation = rating.StdDev.value;
                    Median = rating.Median.value;
                    Rankings = rating.Ranks.Select(r => new Ranking(r)).ToList();
                }
            }

            public int Id { get; private set; }
            public string Type { get; private set; }
            public string Subtype { get; private set; }
            public int CollectionId { get; private set; }
            public string Name { get; private set; }
            public string YearPublished { get; private set; }
            public string Image { get; private set; }
            public string Thumbnail { get; private set; }
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
            public Ranking(BGGSharedObjects.Rank rawRanking)
            {
                Type = rawRanking.Type;
                IdWithinType = rawRanking.Id;
                Name = rawRanking.Name;
                FriendlyName = rawRanking.FriendlyName;
                Position = rawRanking.value == "Not Ranked" ? (int?)null : int.Parse(rawRanking.value);
                BayesianAverageRating = rawRanking.BayesAverage == "Not Ranked" ? (float?)null : float.Parse(rawRanking.BayesAverage);
            }

            public string Type { get; private set; }
            public int IdWithinType { get; private set; }
            public string Name { get; private set; }
            public string FriendlyName { get; private set; }
            public int? Position { get; private set; }
            public float? BayesianAverageRating { get; private set; }
        }
    }
}
