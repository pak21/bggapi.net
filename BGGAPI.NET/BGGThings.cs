// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Mappers;

namespace BGGAPI
{
    public class BGGThings
    {
        static BGGThings()
        {
            var configuration = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);

            configuration.CreateMap<BGGSharedObjects.IntValue, int>().ConvertUsing(src => src.value);

            configuration.CreateMap<BGGThingsObjects.Item, Item>()
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Statistics.Ratings.Average.value))
                .ForMember(dest => dest.AverageWeight, opt => opt.MapFrom(src => src.Statistics.Ratings.AverageWeight.value))
                .ForMember(dest => dest.BayesAverageRating, opt => opt.MapFrom(src => src.Statistics.Ratings.BayesAverage.value))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Links.Where(l => l.Type == "boardgamecategory")))
                .ForMember(dest => dest.MaximumPlayers, opt => opt.MapFrom(src => src.MaxPlayers))
                .ForMember(dest => dest.MinimumAge, opt => opt.MapFrom(src => src.MinAge))
                .ForMember(dest => dest.MinimumPlayers, opt => opt.MapFrom(src => src.MinPlayers))
                .ForMember(dest => dest.NumberOfComments, opt => opt.MapFrom(src => src.Statistics.Ratings.NumComments.value))
                .ForMember(dest => dest.NumberOfWeights, opt => opt.MapFrom(src => src.Statistics.Ratings.NumWeights.value))
                .ForMember(dest => dest.PlayingTime, opt => opt.MapFrom(src => TimeSpan.FromMinutes(src.PlayingTime.value)))
                .ForMember(dest => dest.RatingStandardDeviation, opt => opt.MapFrom(src => src.Statistics.Ratings.StdDev.value))
                .ForMember(dest => dest.UserWhoAreOfferingThisForTrade, opt => opt.MapFrom(src => src.Statistics.Ratings.Trading.value))
                .ForMember(dest => dest.UsersWhoHaveRatedThis, opt => opt.MapFrom(src => src.Statistics.Ratings.UsersRated.value))
                .ForMember(dest => dest.UsersWhoHaveThisOnTheirWishlist, opt => opt.MapFrom(src => src.Statistics.Ratings.Wishing.value))
                .ForMember(dest => dest.UsersWhoOwnThis, opt => opt.MapFrom(src => src.Statistics.Ratings.Owned.value))
                .ForMember(dest => dest.UserWhoWantThisInTrade, opt => opt.MapFrom(src => src.Statistics.Ratings.Wanting.value));
            configuration.CreateMap<BGGThingsObjects.Link, Link>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.value));
            configuration.CreateMap<BGGThingsObjects.Name, Name>();
            configuration.CreateMap<BGGThingsObjects.Poll, Poll>()
                .ForMember(dest => dest.Votes, opt => opt.MapFrom(src => src.TotalVotes));
            configuration.CreateMap<BGGThingsObjects.Things, BGGThings>();

            Mapper = new MappingEngine(configuration);
        }

        public static readonly MappingEngine Mapper;

        public string TermsOfUse { get; private set; }
        public IList<Item> Items { get; private set; }

        public class Item
        {
            public string Type { get; private set; }
            public int Id { get; private set; }

            public string Image { get; private set; }
            public string Thumbnail { get; private set; }
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
            //public VideosList Videos { get; private set; }

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
            //TODO public List<Rank> Ranks { get; set; }
            //TODO public IntValue Median { get; set; }

            //public MarketplaceListings MarketplaceListings { get; private set; }
        }

        public class Name
        {
            public string Type { get; private set; }
            public int SortIndex { get; private set; }
            public string Value { get; private set; }
        }

        public class Poll
        {
            public string Name { get; private set; }
            public string Title { get; private set; }
            public int Votes { get; private set; }
        }

        public class Link
        {
            public string Type { get; private set; }
            public int Id { get; private set; }
            public string Name { get; private set; }
        }
    }
}
