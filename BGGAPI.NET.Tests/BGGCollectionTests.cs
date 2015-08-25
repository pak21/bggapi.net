using System;
using BGGAPI.BGGCollectionObjects;
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

            var ranking = BGGCollection.Mapper.Map<BGGCollection.Ranking>(rawRanking);

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

            var ranking = BGGCollection.Mapper.Map<BGGCollection.Ranking>(rawRanking);

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
                Image = "//localhost",
                Thumbnail = "//localhost",
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

            var item = new BGGCollection.Item(rawItem);

            Assert.AreEqual("thing", item.Type);
            Assert.AreEqual(1, item.Id);
            Assert.AreEqual("boardgame", item.Subtype);
            Assert.AreEqual(2, item.CollectionId);
            Assert.AreEqual("My Funky Boardgame", item.Name);
            Assert.AreEqual("2015", item.YearPublished);
            Assert.AreEqual("//localhost", item.Image);
            Assert.AreEqual("//localhost", item.Thumbnail);
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
    }
}
