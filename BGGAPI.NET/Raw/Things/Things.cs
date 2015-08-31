// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using System;
using System.Collections.Generic;

// A number of the properties in this class are called "value" rather than "Value" due to a RestSharp issue
// see https://groups.google.com/forum/#!topic/restsharp/eAqzFFM13ns

namespace BGGAPI.Raw.Things
{
    public class Things
    {
        public string TermsOfUse { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public string Type { get; set; }
        public int Id { get; set; }

        public string Image { get; set; }
        public string Thumbnail { get; set; }
        public List<Name> Names { get; set; }
        public string Description { get; set; }
        public IntValue YearPublished { get; set; }
        public IntValue MinPlayers { get; set; }
        public IntValue MaxPlayers { get; set; }
        public List<Poll> Polls { get; set; }
        public IntValue PlayingTime { get; set; }
        public IntValue MinAge { get; set; }
        public List<Link> Links { get; set; }
        public VideosList Videos { get; set; }
        public Statistics Statistics { get; set; }
        public MarketplaceListings MarketplaceListings { get; set; }
    }

    public class Name
    {
        public string Type { get; set; }
        public int SortIndex { get; set; }
        public string value { get; set; }
    }

    public class Poll
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public int TotalVotes { get; set; }

        // TODO: how to handle poll results?
    }

    public class Link
    {
        public string Type { get; set; }
        public int Id { get; set; }
        // ReSharper disable once InconsistentNaming - RestSharp issue
        public string value { get; set; }
    }

    public class VideosList
    {
        public int Total { get; set; }
        public List<Video> Videos { get; set; }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public string Link { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }
        public DateTime PostDate { get; set; }
    }

    public class Statistics
    {
        public int Page { get; set; }

        public Ratings Ratings { get; set; }
    }

    public class MarketplaceListings
    {
        public List<Listing> Listings { get; set; }
    }

    public class Listing
    {
        public DateTimeValue ListDate { get; set; }
        public MoneyValue Price { get; set; }
        public StringValue Condition { get; set; }
        public StringValue Notes { get; set; }
        public LinkValue Link { get; set; }
    }

    public class DateTimeValue
    {
        // ReSharper disable once InconsistentNaming - RestSharp issue
        public DateTime value { get; set; }
    }

    public class MoneyValue
    {
        public string Currency { get; set; }
        // ReSharper disable once InconsistentNaming - RestSharp issue
        public float value { get; set; }
    }

    public class StringValue
    {
        // ReSharper disable once InconsistentNaming - RestSharp issue
        public string value { get; set; }
    }

    public class LinkValue
    {
        public string HRef { get; set; }
        public string Title { get; set; }
    }
}
