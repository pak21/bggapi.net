// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using System;
using System.Linq;
using AutoMapper;
using AutoMapper.Mappers;
using BGGAPI.Raw;
using BGGAPI.Raw.Collection;
using BGGAPI.Raw.Things;
using BGGAPI.SharedObjects;

namespace BGGAPI
{
    public static class BGGAutomapper
    {
        static BGGAutomapper()
        {
            var configuration = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);

            CreateSharedMappings(configuration);
            CreateCollectionMappings(configuration);

            Mapper = new MappingEngine(configuration);
        }

        private static void CreateSharedMappings(ConfigurationStore configuration)
        {
            configuration.CreateMap<DateTimeValue, DateTime>().NullSafeConvertUsing(src => src.value);
            configuration.CreateMap<IntValue, int>().NullSafeConvertUsing(src => src.value);
            configuration.CreateMap<FloatValue, float>().NullSafeConvertUsing(src => src.value);
            configuration.CreateMap<StringValue, string>().NullSafeConvertUsing(src => src.value);

            configuration.CreateMap<Rank, Ranking>()
                .ForMember(dest => dest.IdWithinType, opt => opt.MapFrom(src => src.Id))
                // The explicit casts are necessary here to convince the compiler that the lambda is
                // returning a nullable type
                .ForMember(dest => dest.Position,
                    opt => opt.ResolveUsing(src => FormatExceptionSafeMapping(src, s => (int?)int.Parse(s.value))))
                .ForMember(dest => dest.BayesianAverageRating,
                    opt =>
                        opt.ResolveUsing(
                            src => FormatExceptionSafeMapping(src, s => (float?)float.Parse(s.BayesAverage))));
        }

        private static void CreateCollectionMappings(ConfigurationStore configuration)
        {
            configuration.CreateMap<Raw.Collection.Item, BGGCollection.Item>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ObjectType))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ObjectId))
                .ForMember(dest => dest.CollectionId, opt => opt.MapFrom(src => src.CollId))
                .ForMember(dest => dest.NumberOfPlays, opt => opt.MapFrom(src => src.NumPlays))
                .ForMember(dest => dest.Owned, opt => opt.MapFrom(src => src.Status.Own != 0))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => new Uri("http:" + src.Image)))
                .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => new Uri("http:" + src.Thumbnail)))
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
                                FormatExceptionSafeMapping(src,
                                    s =>
                                        s.Stats != null && s.Stats.Rating != null
                                            ? (float?) float.Parse(s.Stats.Rating.value)
                                            : null)))
                .ForMember(dest => dest.UsersRatingThisItem,
                    opt => opt.MapFrom(src => src.Stats.Rating.UsersRated.value))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Stats.Rating.Average.value))
                .ForMember(dest => dest.BayesianAverageRating,
                    opt => opt.MapFrom(src => src.Stats.Rating.BayesAverage.value))
                .ForMember(dest => dest.RatingStandardDeviation,
                    opt => opt.MapFrom(src => src.Stats.Rating.StdDev.value))
                .ForMember(dest => dest.Median, opt => opt.MapFrom(src => src.Stats.Rating.Median.value))
                .ForMember(dest => dest.Rankings, opt => opt.MapFrom(src => src.Stats.Rating.Ranks));

            configuration.CreateMap<Collection, BGGCollection>();
        }

        private static readonly MappingEngine Mapper;

        public static TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        private static TMember FormatExceptionSafeMapping<TSource, TMember>(TSource src,
            Func<TSource, TMember> sourceMapping)
        {
            try
            {
                return sourceMapping(src);
            }
            catch (FormatException)
            {
                return default(TMember);
            }
        }
    }
}
