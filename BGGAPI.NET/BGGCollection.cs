// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.Mappers;

namespace BGGAPI
{
    public class BGGCollection
    {
        static BGGCollection()
        {
            var configuration = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);

            configuration.CreateMap<BGGSharedObjects.Rank, Ranking>()
                .ForMember(dest => dest.IdWithinType, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Position, opt => opt.ResolveUsing(src => AutomapperHelpers.FormatExceptionSafeMapping(src, s => (int?)int.Parse(s.value))))
                .ForMember(dest => dest.BayesianAverageRating, opt => opt.ResolveUsing(src => AutomapperHelpers.FormatExceptionSafeMapping(src, s => (float?)float.Parse(s.BayesAverage))));

            configuration.CreateMap<BGGCollectionObjects.Item, Item>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ObjectType))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ObjectId))
                .ForMember(dest => dest.CollectionId, opt => opt.MapFrom(src => src.CollId))
                .ForMember(dest => dest.NumberOfPlays, opt => opt.MapFrom(src => src.NumPlays))
                .ForMember(dest => dest.Owned, opt => opt.MapFrom(src => src.Status.Own != 0))
                .ForMember(dest => dest.PreviouslyOwned, opt => opt.MapFrom(src => src.Status.PrevOwned != 0))
                .ForMember(dest => dest.AvailableForTrade, opt => opt.MapFrom(src => src.Status.ForTrade != 0))
                .ForMember(dest => dest.WantInTrade, opt => opt.MapFrom(src => src.Status.Want != 0))
                .ForMember(dest => dest.WantToPlay, opt => opt.MapFrom(src => src.Status.WantToPlay != 0))
                .ForMember(dest => dest.WantToBuy, opt => opt.MapFrom(src => src.Status.WantToBuy != 0))
                .ForMember(dest => dest.OnWishlist, opt => opt.MapFrom(src => src.Status.Wishlist != 0))
                .ForMember(dest => dest.Preordered, opt => opt.MapFrom(src => src.Status.Preordered != 0))
                .ForMember(dest => dest.StatusLastModified, opt => opt.MapFrom(src => src.Status.LastModified))
                .ForMember(dest => dest.MinimumPlayers, opt => opt.MapFrom(src => src.Stats.MinPlayers))
                .ForMember(dest => dest.MaximumPlayers, opt => opt.MapFrom(src => src.Stats.MaxPlayers))
                .ForMember(dest => dest.NumberOfOwners, opt => opt.MapFrom(src => src.Stats.NumOwned))
                .ForMember(dest => dest.PlayingTime,
                    opt => opt.MapFrom(src => TimeSpan.FromMinutes(src.Stats.PlayingTime)))
                .ForMember(dest => dest.RatingFromThisUser,
                    opt =>
                        opt.ResolveUsing(
                            src =>
                                AutomapperHelpers.FormatExceptionSafeMapping(src,
                                    s => s.Stats != null && s.Stats.Rating != null ? (float?) float.Parse(s.Stats.Rating.value) : null)))
                .ForMember(dest => dest.UsersRatingThisItem,
                    opt => opt.MapFrom(src => src.Stats.Rating.UsersRated.value))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Stats.Rating.Average.value))
                .ForMember(dest => dest.BayesianAverageRating,
                    opt => opt.MapFrom(src => src.Stats.Rating.BayesAverage.value))
                .ForMember(dest => dest.RatingStandardDeviation,
                    opt => opt.MapFrom(src => src.Stats.Rating.StdDev.value))
                .ForMember(dest => dest.Median, opt => opt.MapFrom(src => src.Stats.Rating.Median.value))
                .ForMember(dest => dest.Rankings, opt => opt.MapFrom(src => src.Stats.Rating.Ranks));

            configuration.CreateMap<BGGCollectionObjects.Collection, BGGCollection>();

            Mapper = new MappingEngine(configuration);
        }

        public static readonly MappingEngine Mapper;

        // ReSharper disable UnusedAutoPropertyAccessor.Local - called via reflection
        public IList<Item> Items { get; private set; }
        public string TermsOfUse { get; private set; }

        public class Item
        {
            public int Id { get; private set; }
            public string Type { get; private set; }
            public string Subtype { get; private set; }
            public int CollectionId { get; private set; }
            public string Name { get; private set; }
            public string YearPublished { get; private set; }
            public string Image { get; private set; }
            public string Thumbnail { get; private set; }
            public int NumberOfPlays { get; private set; }

            public bool Owned { get; private set; }
            public bool PreviouslyOwned { get; private set; }
            public bool AvailableForTrade { get; private set; }
            public bool WantInTrade { get; private set; }
            public bool WantToPlay { get; private set; }
            public bool WantToBuy { get; private set; }
            public bool OnWishlist { get; private set; }
            public bool Preordered { get; private set; }
            public DateTime StatusLastModified { get; private set; }

            public int MinimumPlayers { get; private set; }
            public int MaximumPlayers { get; private set; }
            public TimeSpan PlayingTime { get; private set; }
            public int NumberOfOwners { get; private set; }

            public float? RatingFromThisUser { get; private set; }
            public int UsersRatingThisItem { get; private set; }
            public float AverageRating { get; private set; }
            public float BayesianAverageRating { get; private set; }
            public float RatingStandardDeviation { get; private set; }
            public int Median { get; private set; }

            public IList<Ranking> Rankings { get; set; }
        }

        public class Ranking
        {
            public string Type { get; private set; }
            public int IdWithinType { get; private set; }
            public string Name { get; private set; }
            public string FriendlyName { get; private set; }
            public int? Position { get; private set; }
            public float? BayesianAverageRating { get; private set; }
        }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}
