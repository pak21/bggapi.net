// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

namespace BGGAPI.SharedObjects
{
    public class Ranking
    {
        public string Type { get; private set; }
        public int IdWithinType { get; private set; }
        public string Name { get; private set; }
        public string FriendlyName { get; private set; }
        public int? Position { get; private set; }
        public float? BayesianAverageRating { get; private set; }
    }
}
