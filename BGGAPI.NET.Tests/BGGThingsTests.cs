using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BGGAPI.NET.Tests
{
    [TestFixture]
    public class BGGThingsTests
    {
        [Test]
        public void TestNameConversion()
        {
            var rawName = new BGGThingsObjects.Name
            {
                Type = "NameType",
                SortIndex = 2,
                value = "Name value"
            };

            var name = BGGThings.Mapper.Map<BGGThings.Name>(rawName);

            Assert.AreEqual("NameType", name.Type);
            Assert.AreEqual(2, name.SortIndex);
            Assert.AreEqual("Name value", name.Value);
        }

        [Test]
        public void TestPollConversion()
        {
            var rawPoll = new BGGThingsObjects.Poll
            {
                Name = "Poll name",
                Title = "Poll title",
                TotalVotes = 1234
            };

            var poll = BGGThings.Mapper.Map<BGGThings.Poll>(rawPoll);

            Assert.AreEqual("Poll name", poll.Name);
            Assert.AreEqual("Poll title", poll.Title);
            Assert.AreEqual(1234, poll.Votes);
        }

        [Test]
        public void TestLinkConversion()
        {
            var rawLink = new BGGThingsObjects.Link
            {
                Id = 1234,
                Type = "Link type",
                value = "Link value"
            };

            var link = BGGThings.Mapper.Map<BGGThings.Link>(rawLink);

            Assert.AreEqual(1234, link.Id);
            Assert.AreEqual("Link value", link.Name);
            Assert.AreEqual("Link type", link.Type);
        }

        [Test]
        public void TestRankingConversion()
        {
            var rawRanking = new BGGSharedObjects.Rank
            {
                Type = "family",
                Id = 5497,
                Name = "strategygames",
                FriendlyName = "Strategy Game Rank",
                value = "15",
                BayesAverage = "7.77473"
            };

            var ranking = BGGThings.Mapper.Map<BGGThings.Ranking>(rawRanking);

            Assert.AreEqual("family", ranking.Type);
            Assert.AreEqual(5497, ranking.Id);
            Assert.AreEqual("strategygames", ranking.Name);
            Assert.AreEqual("Strategy Game Rank", ranking.FriendlyName);
            Assert.AreEqual(15, ranking.Value);
            Assert.AreEqual(7.77473f, ranking.BayesAverage);
        }

        [Test]
        public void TestVideoConversion()
        {
            var rawVideo = new BGGThingsObjects.Video
            {
                Id = 1,
                Title = "Video title",
                Category = "Video category",
                Language = "English",
                Link = "http://www.example.com/",
                Username = "User123",
                UserId = 2,
                PostDate = new DateTime(2015, 1, 2, 3, 4, 5)
            };

            var video = BGGThings.Mapper.Map<BGGThings.Video>(rawVideo);

            Assert.AreEqual(1, video.Id);
            Assert.AreEqual("Video title", video.Title);
            Assert.AreEqual("Video category", video.Category);
            Assert.AreEqual("English", video.Language);
            Assert.AreEqual("http://www.example.com/", video.Link);
            Assert.AreEqual("User123", video.Username);
            Assert.AreEqual(2, video.UserId);
            Assert.AreEqual(new DateTime(2015, 1, 2, 3, 4, 5), video.PostDate);
        }

        [Test]
        public void TestListingConversion()
        {
            var rawListing = new BGGThingsObjects.Listing
            {
                ListDate = new BGGThingsObjects.DateTimeValue { value = new DateTime(2014, 6, 7, 8, 9, 10) },
                Price = new BGGThingsObjects.MoneyValue
                {
                    Currency = "GBP",
                    value = 12.34f
                },
                Condition = new BGGThingsObjects.StringValue { value = "Mint" },
                Notes = new BGGThingsObjects.StringValue { value = "Really mint" },
                Link = new BGGThingsObjects.LinkValue
                {
                    HRef = "http://www.example.com/",
                    Title = "Example link"
                }
            };

            var listing = BGGThings.Mapper.Map<BGGThings.MarketplaceListing>(rawListing);

            Assert.AreEqual(new DateTime(2014, 6, 7, 8, 9, 10), listing.ListingDate);
            Assert.AreEqual("GBP", listing.Currency);
            Assert.AreEqual(12.34f, listing.CurrencyValue);
            Assert.AreEqual("Mint", listing.Condition);
            Assert.AreEqual("Really mint", listing.Notes);
            Assert.AreEqual("http://www.example.com/", listing.Link);
            Assert.AreEqual("Example link", listing.LinkTitle);
        }

        [Test]
        public void TestRankingConversion_NotRanked()
        {
            var rawRanking = new BGGSharedObjects.Rank
            {
                Type = "family",
                Id = 5497,
                Name = "strategygames",
                FriendlyName = "Strategy Game Rank",
                value = "Not Ranked",
                BayesAverage = "Not Ranked"
            };

            var ranking = BGGThings.Mapper.Map<BGGThings.Ranking>(rawRanking);

            Assert.IsNull(ranking.Value);
            Assert.IsNull(ranking.BayesAverage);
        }

        [Test]
        public void TestItemConversion()
        {
            var rawItem = new BGGThingsObjects.Item()
            {
                Description = "Item description",
                Id = 1234,
                Image = "http://www.example.com/image.jpeg",
                Links = new List<BGGThingsObjects.Link>
                {
                    new BGGThingsObjects.Link { Type = "boardgamecategory", value = "Category1" },
                    new BGGThingsObjects.Link { Type = "foo", value = "bar" },
                },
                MarketplaceListings = new BGGThingsObjects.MarketplaceListings
                {
                    Listings = new List<BGGThingsObjects.Listing>
                    {
                        new BGGThingsObjects.Listing
                        {
                            Condition = new BGGThingsObjects.StringValue { value = "Bashed" }
                        }
                    }
                },
                MaxPlayers = new BGGSharedObjects.IntValue { value = 4 },
                MinAge = new BGGSharedObjects.IntValue { value = 18 },
                MinPlayers = new BGGSharedObjects.IntValue { value = 1 },
                Names = new List<BGGThingsObjects.Name>
                {
                    new BGGThingsObjects.Name { value = "A" },
                    new BGGThingsObjects.Name { value = "B" }
                },
                PlayingTime = new BGGSharedObjects.IntValue { value = 60 },
                Polls = new List<BGGThingsObjects.Poll> {
                    new BGGThingsObjects.Poll { Name = "C" },
                    new BGGThingsObjects.Poll { Name = "D" },
                    new BGGThingsObjects.Poll { Name = "E" }
                },
                Thumbnail = "http://www.example.com/thumbnail.jpeg",
                Type = "Item type",
                Videos = new BGGThingsObjects.VideosList
                {
                    Total = 3,
                    Videos = new List<BGGThingsObjects.Video>
                    {
                        new BGGThingsObjects.Video { Id = 7 }
                    }
                },
                YearPublished = new BGGSharedObjects.IntValue { value = 2000 }
            };

            var item = BGGThings.Mapper.Map<BGGThings.Item>(rawItem);

            Assert.AreEqual(1, item.Categories.Count);
            Assert.AreEqual("Category1", item.Categories[0].Name);
            Assert.AreEqual("Item description", item.Description);
            Assert.AreEqual(1234, item.Id);
            Assert.AreEqual("http://www.example.com/image.jpeg", item.Image);
            Assert.AreEqual(2, item.Links.Count);
            Assert.AreEqual("bar", item.Links[1].Name);
            Assert.AreEqual(1, item.MarketplaceListings.Count);
            Assert.AreEqual("Bashed", item.MarketplaceListings[0].Condition);
            Assert.AreEqual(4, item.MaximumPlayers);
            Assert.AreEqual(18, item.MinimumAge);
            Assert.AreEqual(1, item.MinimumPlayers);
            Assert.AreEqual(2, item.Names.Count);
            Assert.AreEqual("A", item.Names[0].Value);
            Assert.AreEqual(TimeSpan.FromMinutes(60), item.PlayingTime);
            Assert.AreEqual(3, item.Polls.Count);
            Assert.AreEqual("C", item.Polls[0].Name);
            Assert.AreEqual("http://www.example.com/thumbnail.jpeg", item.Thumbnail);
            Assert.AreEqual("Item type", item.Type);
            Assert.AreEqual(1, item.Videos.Count);
            Assert.AreEqual(7, item.Videos[0].Id);
            Assert.AreEqual(2000, item.YearPublished);
        }

        [Test]
        public void TestStatisticsConversion()
        {
            var rawItem = NullItemFactory(123);
            rawItem.Statistics = new BGGThingsObjects.Statistics
            {
                Ratings = new BGGSharedObjects.Ratings
                {
                    Average = new BGGSharedObjects.FloatValue { value = 9.87f },
                    AverageWeight = new BGGSharedObjects.FloatValue { value = 1.23f },
                    BayesAverage = new BGGSharedObjects.FloatValue { value = 6.54f },
                    Median = new BGGSharedObjects.IntValue { value = 8 },
                    NumComments = new BGGSharedObjects.IntValue { value = 6 },
                    NumWeights = new BGGSharedObjects.IntValue { value = 7 },
                    Owned = new BGGSharedObjects.IntValue { value = 2 },
                    Ranks = new List<BGGSharedObjects.Rank>
                    {
                        new BGGSharedObjects.Rank
                        {
                            Type = "Type1", Id = 2, Name = "Name1", FriendlyName = "FriendlyName1", value = "3", BayesAverage = "4"
                        }
                    },
                    StdDev = new BGGSharedObjects.FloatValue { value = 3.21f },
                    Trading = new BGGSharedObjects.IntValue { value = 3 },
                    UsersRated = new BGGSharedObjects.IntValue { value = 1 },
                    Wanting = new BGGSharedObjects.IntValue { value = 4 },
                    Wishing = new BGGSharedObjects.IntValue { value = 5 }
                }
            };

            var item = BGGThings.Mapper.Map<BGGThings.Item>(rawItem);

            Assert.AreEqual(9.87f, item.AverageRating);
            Assert.AreEqual(1.23f, item.AverageWeight);
            Assert.AreEqual(6.54f, item.BayesAverageRating);
            Assert.AreEqual(8, item.Median);
            Assert.AreEqual(6, item.NumberOfComments);
            Assert.AreEqual(7, item.NumberOfWeights);
            Assert.AreEqual(1, item.Rankings.Count);
            Assert.AreEqual(2, item.Rankings[0].Id);
            Assert.AreEqual(3.21f, item.RatingStandardDeviation);
            Assert.AreEqual(3, item.UserWhoAreOfferingThisForTrade);
            Assert.AreEqual(1, item.UsersWhoHaveRatedThis);
            Assert.AreEqual(5, item.UsersWhoHaveThisOnTheirWishlist);
            Assert.AreEqual(2, item.UsersWhoOwnThis);
            Assert.AreEqual(4, item.UserWhoWantThisInTrade);
        }

        [Test]
        public void TestThingsConversion()
        {
            var rawThings = new BGGThingsObjects.Things
            {
                TermsOfUse = "blah",
                Items = new List<BGGThingsObjects.Item>
                {
                    NullItemFactory(123), NullItemFactory(456)
                }
            };

            var things = BGGThings.Mapper.Map<BGGThings>(rawThings);

            Assert.AreEqual("blah", things.TermsOfUse);
            Assert.AreEqual(2, things.Items.Count);
            Assert.AreEqual(123, things.Items[0].Id);
        }

        private static BGGThingsObjects.Item NullItemFactory(int id)
        {
            return new BGGThingsObjects.Item
            {
                Id = id,
                Links = new List<BGGThingsObjects.Link>(),
                MaxPlayers = new BGGSharedObjects.IntValue(),
                MinAge = new BGGSharedObjects.IntValue(),
                MinPlayers = new BGGSharedObjects.IntValue(),
                YearPublished = new BGGSharedObjects.IntValue()
            };
        }
    }
}
