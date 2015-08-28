// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using System;
using System.Collections.Generic;
using BGGAPI.SharedObjects;

namespace BGGAPI
{
    public class BGGThings
    {
        // ReSharper disable UnusedAutoPropertyAccessor.Local - called via reflection
        public string TermsOfUse { get; private set; }
        public IList<Item> Items { get; private set; }

        public class Item
        {
            public string Type { get; private set; }
            public int Id { get; private set; }

            public Uri Image { get; private set; }
            public Uri Thumbnail { get; private set; }
            public IList<Name> Names { get; private set; }
            public string Description { get; private set; }
            public int YearPublished { get; private set; }
            public int MinimumPlayers { get; private set; }
            public int MaximumPlayers { get; private set; }
            public IList<Poll> Polls { get; private set; }
            public TimeSpan PlayingTime { get; private set; }
            public int MinimumAge { get; private set; }
            public IList<Link> Categories { get; private set; }
            
            public IList<Link> Links { get; private set; }
            public IList<Video> Videos { get; private set; }

            // We pull the statistics members up to the top-level
            public float? AverageRating { get; private set; }
            public float? AverageWeight { get; private set; }
            public float? BayesAverageRating { get; private set; }
            public int? NumberOfComments { get; private set; }
            public int? NumberOfWeights { get; private set; }
            public float? RatingStandardDeviation { get; private set; }
            public int? UserWhoAreOfferingThisForTrade { get; private set; }
            public int? UsersWhoHaveRatedThis { get; private set; }
            public int? UsersWhoHaveThisOnTheirWishlist { get; private set; }
            public int? UsersWhoOwnThis { get; private set; }
            public int? UserWhoWantThisInTrade { get; private set; }
            public List<Ranking> Rankings { get; private set; }
            public int? Median { get; private set; }

            public IList<MarketplaceListing> MarketplaceListings { get; private set; }
        }

        public class Name
        {
            public string Type { get; private set; }
            public int SortIndex { get; private set; }
            public string Value { get; private set; }
        }

        public class Poll
        {
            // ReSharper disable once MemberHidesStaticFromOuterClass - not going to be referencing the class "Name" from within this class
            public string Name { get; private set; }
            public string Title { get; private set; }
            public int Votes { get; private set; }
        }

        public class Link
        {
            public string Type { get; private set; }
            public int Id { get; private set; }
            // ReSharper disable once MemberHidesStaticFromOuterClass - not going to be referencing the class "Name" from within this class
            public string Name { get; private set; }
        }

        public class Video
        {
            public int Id { get; private set; }
            public string Title { get; private set; }
            public string Category { get; private set; }
            public string Language { get; private set; }
            // ReSharper disable once MemberHidesStaticFromOuterClass - not going to be referencing the class "Link" from within this class
            public Uri Link { get; private set; }
            public string Username { get; private set; }
            public int UserId { get; private set; }
            public DateTime PostDate { get; private set; }
        }

        public class MarketplaceListing
        {
            public DateTime ListingDate { get; private set; }
            public string Currency { get; private set; }
            public float CurrencyValue { get; private set; }
            public string Condition { get; private set; }
            public string Notes { get; private set; }
            // ReSharper disable once MemberHidesStaticFromOuterClass - not going to be referencing the class "Link" from within this class
            public Uri Link { get; private set; }
            public string LinkTitle { get; private set; }
        }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}
