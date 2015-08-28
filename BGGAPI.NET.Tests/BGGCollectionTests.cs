using System;
using System.Collections.Generic;
using BGGAPI.BGGCollectionObjects;
using BGGAPI.BGGSharedObjects;
using NUnit.Framework;

namespace BGGAPI.NET.Tests
{
    [TestFixture]
    public class BGGCollectionTests
    {
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

            var ranking = BGGAutomapper.Map<BGGCollection.Ranking>(rawRanking);

            Assert.AreEqual("family", ranking.Type);
            Assert.AreEqual(5497, ranking.IdWithinType);
            Assert.AreEqual("strategygames", ranking.Name);
            Assert.AreEqual("Strategy Game Rank", ranking.FriendlyName);
            Assert.AreEqual(15, ranking.Position);
            Assert.AreEqual(7.77473f, ranking.BayesianAverageRating);
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

            var ranking = BGGAutomapper.Map<BGGCollection.Ranking>(rawRanking);

            Assert.IsNull(ranking.Position);
            Assert.IsNull(ranking.BayesianAverageRating);
        }

        [Test]
        public void TestItemConversion()
        {
            var rawItem = new Item
            {
                ObjectType = "thing",
                ObjectId = 1,
                Subtype = "boardgame",
                CollId = 2,
                Name = "My Funky Boardgame",
                YearPublished = "2015",
                Image = "//www.example.com/image.jpeg",
                Thumbnail = "//www.example.com/thumbnail.jpeg",
                NumPlays = 3,

                Status = new Status
                {
                    Own = 1,
                    PrevOwned = 0,
                    ForTrade = 1,
                    Want = 0,
                    WantToPlay = 1,
                    WantToBuy = 0,
                    Wishlist = 1,
                    Preordered = 0,
                    LastModified = new DateTime(2015, 8, 25, 21, 14, 32, DateTimeKind.Utc)
                }
            };

            var item = BGGAutomapper.Map<BGGCollection.Item>(rawItem);

            Assert.AreEqual("thing", item.Type);
            Assert.AreEqual(1, item.Id);
            Assert.AreEqual("boardgame", item.Subtype);
            Assert.AreEqual(2, item.CollectionId);
            Assert.AreEqual("My Funky Boardgame", item.Name);
            Assert.AreEqual("2015", item.YearPublished);
            Assert.AreEqual(new Uri("http://www.example.com/image.jpeg"), item.Image);
            Assert.AreEqual(new Uri("http://www.example.com/thumbnail.jpeg"), item.Thumbnail);
            Assert.AreEqual(3, item.NumberOfPlays);

            Assert.IsTrue(item.Owned);
            Assert.IsFalse(item.PreviouslyOwned);
            Assert.IsTrue(item.AvailableForTrade);
            Assert.IsFalse(item.WantInTrade);
            Assert.IsTrue(item.WantToPlay);
            Assert.IsFalse(item.WantToBuy);
            Assert.IsTrue(item.OnWishlist);
            Assert.IsFalse(item.Preordered);
            Assert.AreEqual(new DateTime(2015, 8, 25, 21, 14, 32, DateTimeKind.Utc), item.StatusLastModified);
        }

        [Test]
        public void TestStatisticsConversion()
        {
            var rawItem = NullItemFactory(123);
            rawItem.Stats = new Stats
            {
                MinPlayers = 1,
                MaxPlayers = 2,
                NumOwned = 3,
                PlayingTime = 4,

                Rating = new Ratings
                {
                    value = "5.6",
                    UsersRated = new IntValue { value = 7 },
                    Average = new FloatValue { value = 8.9f },
                    BayesAverage = new FloatValue { value = 1.2f },
                    StdDev = new FloatValue { value = 3.4f },
                    Median = new IntValue { value = 5 },
                    Ranks = new List<Rank> { new Rank { Id = 6, value = "0", BayesAverage = "0" } }
                }
            };

            var item = BGGAutomapper.Map<BGGCollection.Item>(rawItem);

            Assert.AreEqual(1, item.MinimumPlayers);
            Assert.AreEqual(2, item.MaximumPlayers);
            Assert.AreEqual(3, item.NumberOfOwners);
            Assert.AreEqual(TimeSpan.FromMinutes(4), item.PlayingTime);

            Assert.AreEqual(5.6f, item.RatingFromThisUser);
            Assert.AreEqual(7, item.UsersRatingThisItem);
            Assert.AreEqual(8.9f, item.AverageRating);
            Assert.AreEqual(1.2f, item.BayesianAverageRating);
            Assert.AreEqual(3.4f, item.RatingStandardDeviation);
            Assert.AreEqual(5, item.Median);

            Assert.AreEqual(1, item.Rankings.Count);
            Assert.AreEqual(6, item.Rankings[0].IdWithinType);
        }

        [Test]
        public void TestCollectionConversion()
        {
            var rawCollection = new Collection
            {
                TermsOfUse = "blah",
                Items = new List<Item>
                {
                    NullItemFactory(123), NullItemFactory(456)
                }
            };

            var collection = BGGAutomapper.Map<BGGCollection>(rawCollection);

            Assert.AreEqual("blah", collection.TermsOfUse);
            Assert.AreEqual(2, collection.Items.Count);
            Assert.AreEqual(123, collection.Items[0].Id);
        }

        private static Item NullItemFactory(int id)
        {
            return new Item
            {
                ObjectId = id,
                Status = new Status(),
                Image = "//localhost",
                Thumbnail = "//localhost"
            };
        }
    }
}
