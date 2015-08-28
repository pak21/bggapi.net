// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using System.Collections.Generic;

// A number of the properties in this class are called "value" rather than "Value" due to a RestSharp issue
// see https://groups.google.com/forum/#!topic/restsharp/eAqzFFM13ns

namespace BGGAPI.BGGSharedObjects
{
    public class IntValue
    {
        // ReSharper disable once InconsistentNaming - RestSharp issue
        public int value { get; set; }
    }

    public class FloatValue
    {
        // ReSharper disable once InconsistentNaming - RestSharp issue
        public float value { get; set; }
    }

    public class Ratings
    {
        // This can return "N/A" so needs to be a string rather than an integer
        // ReSharper disable once InconsistentNaming - RestSharp issue
        public string value { get; set; }

        public IntValue UsersRated { get; set; }
        public FloatValue Average { get; set; }
        public FloatValue BayesAverage { get; set; }
        public List<Rank> Ranks { get; set; }
        public FloatValue StdDev { get; set; }
        public IntValue Median { get; set; }
        public IntValue Owned { get; set; }
        public IntValue Trading { get; set; }
        public IntValue Wanting { get; set; }
        public IntValue Wishing { get; set; }
        public IntValue NumComments { get; set; }
        public IntValue NumWeights { get; set; }
        public FloatValue AverageWeight { get; set; }
    }

    public class Rank
    {
        public string Type { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        // This can return "Not Ranked" so needs to be a string rather than an integer
        // ReSharper disable once InconsistentNaming - RestSharp issue
        public string value { get; set; }
        // This can return "Not Ranked" so needs to be a string rather than an integer
        public string BayesAverage { get; set; }
    }
}
