// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using System;
using System.Collections.Generic;
using System.Linq;
using BGGAPI.SharedObjects;

namespace BGGAPI
{
    public class BGGThings
    {
        public BGGThings(Raw.Things.Things rawThings)
        {
            TermsOfUse = rawThings.TermsOfUse;
            Items = rawThings.Items.Select(i => new Item(i)).ToList();
        }

        // ReSharper disable UnusedAutoPropertyAccessor.Local - called via reflection
        public string TermsOfUse { get; private set; }
        public IList<Item> Items { get; private set; }

        public class Item
        {
            public Item(Raw.Things.Item rawItem)
            {
                Categories = rawItem.Links.Where(l => l.Type == "boardgamecategory").Select(l => new Link(l)).ToList();
                Description = rawItem.Description;
                Id = rawItem.Id;
                Image = new Uri("http:" + rawItem.Image);
                Links = rawItem.Links.Select(l => new Link(l)).ToList();
                MaximumPlayers = rawItem.MaxPlayers.value;
                MinimumAge = rawItem.MinAge.value;
                MinimumPlayers = rawItem.MinPlayers.value;
                Thumbnail = new Uri("http:" + rawItem.Thumbnail);
                Type = rawItem.Type;
                YearPublished = rawItem.YearPublished.value;

                if (rawItem.Names != null)
                {
                    Names = rawItem.Names.Select(n => new Name(n)).ToList();
                }

                if (rawItem.Polls != null)
                {
                    Polls = rawItem.Polls.Select(p => new Poll(p)).ToList();
                }

                if (rawItem.PlayingTime != null)
                {
                    PlayingTime = TimeSpan.FromMinutes(rawItem.PlayingTime.value);
                }

                if (rawItem.Statistics != null)
                {
                    AverageRating = rawItem.Statistics.Ratings.Average.value;
                    AverageWeight = rawItem.Statistics.Ratings.AverageWeight.value;
                    BayesAverageRating = rawItem.Statistics.Ratings.BayesAverage.value;
                    Median = rawItem.Statistics.Ratings.Median.value;
                    NumberOfComments = rawItem.Statistics.Ratings.NumComments.value;
                    NumberOfWeights = rawItem.Statistics.Ratings.NumWeights.value;
                    Rankings = rawItem.Statistics.Ratings.Ranks.Select(r => new Ranking(r)).ToList();
                    RatingStandardDeviation = rawItem.Statistics.Ratings.StdDev.value;
                    UserWhoAreOfferingThisForTrade = rawItem.Statistics.Ratings.Trading.value;
                    UsersWhoHaveRatedThis = rawItem.Statistics.Ratings.UsersRated.value;
                    UsersWhoHaveThisOnTheirWishlist = rawItem.Statistics.Ratings.Wishing.value;
                    UsersWhoOwnThis = rawItem.Statistics.Ratings.Owned.value;
                    UserWhoWantThisInTrade = rawItem.Statistics.Ratings.Wanting.value;
                }

                if (rawItem.MarketplaceListings != null)
                {
                    MarketplaceListings = rawItem.MarketplaceListings.Listings.Select(l => new MarketplaceListing(l)).ToList();
                }

                if (rawItem.Videos != null)
                {
                    Videos = rawItem.Videos.Videos.Select(v => new Video(v)).ToList();
                }
            }

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
            public Name(Raw.Things.Name rawName)
            {
                Type = rawName.Type;
                SortIndex = rawName.SortIndex;
                Value = rawName.value;
            }

            public string Type { get; private set; }
            public int SortIndex { get; private set; }
            public string Value { get; private set; }
        }

        public class Poll
        {
            public Poll(Raw.Things.Poll rawPoll)
            {
                Name = rawPoll.Name;
                Title = rawPoll.Title;
                Votes = rawPoll.TotalVotes;
            }

            // ReSharper disable once MemberHidesStaticFromOuterClass - not going to be referencing the class "Name" from within this class
            public string Name { get; private set; }
            public string Title { get; private set; }
            public int Votes { get; private set; }
        }

        public class Link
        {
            public Link(Raw.Things.Link rawLink)
            {
                Type = rawLink.Type;
                Id = rawLink.Id;
                Name = rawLink.value;
            }

            public string Type { get; private set; }
            public int Id { get; private set; }
            // ReSharper disable once MemberHidesStaticFromOuterClass - not going to be referencing the class "Name" from within this class
            public string Name { get; private set; }
        }

        public class Video
        {
            public Video(Raw.Things.Video rawVideo)
            {
                Id = rawVideo.Id;
                Title = rawVideo.Title;
                Category = rawVideo.Category;
                Language = rawVideo.Language;
                Link = new Uri(rawVideo.Link);
                Username = rawVideo.Username;
                UserId = rawVideo.UserId;
                PostDate = rawVideo.PostDate;
            }

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
            public MarketplaceListing(Raw.Things.Listing rawListing)
            {
                if (rawListing.ListDate != null)
                {
                    ListingDate = rawListing.ListDate.value;
                }

                if (rawListing.Price != null)
                {
                    Currency = rawListing.Price.Currency;
                    CurrencyValue = rawListing.Price.value;
                }

                if (rawListing.Condition != null)
                {
                    Condition = rawListing.Condition.value;
                }

                if (rawListing.Notes != null)
                {
                    Notes = rawListing.Notes.value;
                }

                if (rawListing.Link != null)
                {
                    Link = new Uri(rawListing.Link.HRef);
                    LinkTitle = rawListing.Link.Title;
                }
            }

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
