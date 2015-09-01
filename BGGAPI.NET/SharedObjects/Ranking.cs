// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

namespace BGGAPI.SharedObjects
{
    public class Ranking
    {
        public Ranking(Raw.Rank rawRanking)
        {
            Type = rawRanking.Type;
            IdWithinType = rawRanking.Id;
            Name = rawRanking.Name;
            FriendlyName = rawRanking.FriendlyName;

            int position;
            if (int.TryParse(rawRanking.value, out position))
            {
                Position = position;
            }

            float bayesianAverageRating;
            if (float.TryParse(rawRanking.BayesAverage, out bayesianAverageRating))
            {
                BayesianAverageRating = bayesianAverageRating;
            }
        }

        public string Type { get; private set; }
        public int IdWithinType { get; private set; }
        public string Name { get; private set; }
        public string FriendlyName { get; private set; }
        public int? Position { get; private set; }
        public float? BayesianAverageRating { get; private set; }
    }
}
