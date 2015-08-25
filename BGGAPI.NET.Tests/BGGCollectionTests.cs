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

            var ranking = new BGGCollection.Ranking(rawRanking);

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

            var ranking = new BGGCollection.Ranking(rawRanking);

            Assert.IsNull(ranking.Position);
            Assert.IsNull(ranking.BayesianAverageRating);
        }
    }
}
