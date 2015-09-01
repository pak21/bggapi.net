// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using System;
using System.Collections.Generic;
using System.Linq;
using BGGAPI.SharedObjects;

namespace BGGAPI
{
    public class BGGCollection
    {
        public BGGCollection(Raw.Collection.Collection rawCollection)
        {
            TermsOfUse = rawCollection.TermsOfUse;
            Items = rawCollection.Items.Select(i => new Item(i)).ToList();
        }

        // ReSharper disable UnusedAutoPropertyAccessor.Local - called via reflection
        public IList<Item> Items { get; private set; }
        public string TermsOfUse { get; private set; }

        public class Item
        {
            public Item(Raw.Collection.Item rawItem)
            {
                CollectionId = rawItem.CollId;
                Id = rawItem.ObjectId;
                Image = new Uri("http:" + rawItem.Image);
                Name = rawItem.Name;
                NumberOfPlays = rawItem.NumPlays;
                Subtype = rawItem.Subtype;
                Thumbnail = new Uri("http:" + rawItem.Thumbnail);
                Type = rawItem.ObjectType;
                YearPublished = rawItem.YearPublished;

                StatusLastModified = rawItem.Status.LastModified;
                AvailableForTrade = rawItem.Status.ForTrade != 0;
                OnWishlist = rawItem.Status.Wishlist != 0;
                Owned = rawItem.Status.Own != 0;
                Preordered = rawItem.Status.Preordered != 0;
                PreviouslyOwned = rawItem.Status.PrevOwned != 0;
                WantInTrade = rawItem.Status.Want != 0;
                WantToBuy = rawItem.Status.WantToBuy != 0;
                WantToPlay = rawItem.Status.WantToPlay != 0;

                if (rawItem.Stats != null)
                {
                    MinimumPlayers = rawItem.Stats.MinPlayers;
                    MaximumPlayers = rawItem.Stats.MaxPlayers;
                    NumberOfOwners = rawItem.Stats.NumOwned;
                    PlayingTime = TimeSpan.FromMinutes(rawItem.Stats.PlayingTime);

                    if (rawItem.Stats.Rating != null)
                    {
                        AverageRating = rawItem.Stats.Rating.Average.value;
                        BayesianAverageRating = rawItem.Stats.Rating.BayesAverage.value;
                        Median = rawItem.Stats.Rating.Median.value;
                        RatingStandardDeviation = rawItem.Stats.Rating.StdDev.value;
                        UsersRatingThisItem = rawItem.Stats.Rating.UsersRated.value;

                        Rankings = rawItem.Stats.Rating.Ranks.Select(r => new Ranking(r)).ToList();

                        float ratingFromThisUser;
                        if (float.TryParse(rawItem.Stats.Rating.value, out ratingFromThisUser))
                        {
                            RatingFromThisUser = ratingFromThisUser;
                        }
                    }
                }
            }

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
        // ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}
