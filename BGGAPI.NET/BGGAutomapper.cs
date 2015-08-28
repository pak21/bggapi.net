using System;
using System.Linq;
using AutoMapper;
using AutoMapper.Mappers;

namespace BGGAPI
{
    public static class BGGAutomapper
    {
        static BGGAutomapper()
        {
            var configuration = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);

            CreateCollectionMappings(configuration);
            CreateThingsMappings(configuration);

            Mapper = new MappingEngine(configuration);
        }

        private static void CreateCollectionMappings(ConfigurationStore configuration)
        {
            configuration.CreateMap<BGGSharedObjects.Rank, BGGCollection.Ranking>()
                .ForMember(dest => dest.IdWithinType, opt => opt.MapFrom(src => src.Id))
                // The explicit casts are necessary here to convince the compiler that the lambda is
                // returning a nullable type
                .ForMember(dest => dest.Position,
                    opt => opt.ResolveUsing(src => FormatExceptionSafeMapping(src, s => (int?) int.Parse(s.value))))
                .ForMember(dest => dest.BayesianAverageRating,
                    opt =>
                        opt.ResolveUsing(
                            src => FormatExceptionSafeMapping(src, s => (float?) float.Parse(s.BayesAverage))));

            configuration.CreateMap<BGGCollectionObjects.Item, BGGCollection.Item>()
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

            configuration.CreateMap<BGGCollectionObjects.Collection, BGGCollection>();
        }

        private static void CreateThingsMappings(ConfigurationStore configuration)
        {
            configuration.CreateMap<BGGThingsObjects.DateTimeValue, DateTime>().NullSafeConvertUsing(src => src.value);
            configuration.CreateMap<BGGSharedObjects.IntValue, int>().NullSafeConvertUsing(src => src.value);
            configuration.CreateMap<BGGSharedObjects.FloatValue, float>().NullSafeConvertUsing(src => src.value);
            configuration.CreateMap<BGGThingsObjects.StringValue, string>().NullSafeConvertUsing(src => src.value);

            configuration.CreateMap<BGGThingsObjects.Item, BGGThings.Item>()
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Statistics.Ratings.Average))
                .ForMember(dest => dest.AverageWeight, opt => opt.MapFrom(src => src.Statistics.Ratings.AverageWeight))
                .ForMember(dest => dest.BayesAverageRating,
                    opt => opt.MapFrom(src => src.Statistics.Ratings.BayesAverage))
                .ForMember(dest => dest.Categories,
                    opt => opt.MapFrom(src => src.Links.Where(l => l.Type == "boardgamecategory")))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => new Uri("http:" + src.Image)))
                .ForMember(dest => dest.MarketplaceListings, opt => opt.MapFrom(src => src.MarketplaceListings.Listings))
                .ForMember(dest => dest.MaximumPlayers, opt => opt.MapFrom(src => src.MaxPlayers))
                .ForMember(dest => dest.Median, opt => opt.MapFrom(src => src.Statistics.Ratings.Median))
                .ForMember(dest => dest.MinimumAge, opt => opt.MapFrom(src => src.MinAge))
                .ForMember(dest => dest.MinimumPlayers, opt => opt.MapFrom(src => src.MinPlayers))
                .ForMember(dest => dest.NumberOfComments, opt => opt.MapFrom(src => src.Statistics.Ratings.NumComments))
                .ForMember(dest => dest.NumberOfWeights, opt => opt.MapFrom(src => src.Statistics.Ratings.NumWeights))
                .ForMember(dest => dest.PlayingTime,
                    opt => opt.MapFrom(src => TimeSpan.FromMinutes(src.PlayingTime.value)))
                .ForMember(dest => dest.Rankings, opt => opt.MapFrom(src => src.Statistics.Ratings.Ranks))
                .ForMember(dest => dest.RatingStandardDeviation,
                    opt => opt.MapFrom(src => src.Statistics.Ratings.StdDev))
                .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => new Uri("http:" + src.Thumbnail)))
                .ForMember(dest => dest.UserWhoAreOfferingThisForTrade,
                    opt => opt.MapFrom(src => src.Statistics.Ratings.Trading))
                .ForMember(dest => dest.UsersWhoHaveRatedThis,
                    opt => opt.MapFrom(src => src.Statistics.Ratings.UsersRated))
                .ForMember(dest => dest.UsersWhoHaveThisOnTheirWishlist,
                    opt => opt.MapFrom(src => src.Statistics.Ratings.Wishing))
                .ForMember(dest => dest.UsersWhoOwnThis, opt => opt.MapFrom(src => src.Statistics.Ratings.Owned))
                .ForMember(dest => dest.UserWhoWantThisInTrade,
                    opt => opt.MapFrom(src => src.Statistics.Ratings.Wanting))
                .ForMember(dest => dest.Videos, opt => opt.MapFrom(src => src.Videos.Videos));
            configuration.CreateMap<BGGThingsObjects.Link, BGGThings.Link>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.value));
            configuration.CreateMap<BGGThingsObjects.Name, BGGThings.Name>();
            configuration.CreateMap<BGGThingsObjects.Poll, BGGThings.Poll>()
                .ForMember(dest => dest.Votes, opt => opt.MapFrom(src => src.TotalVotes));

            // The explicit casts are necessary here to convince the compiler that the lambda is
            // returning a nullable type
            configuration.CreateMap<BGGSharedObjects.Rank, BGGThings.Ranking>()
                .ForMember(dest => dest.Value,
                    opt => opt.ResolveUsing(src => FormatExceptionSafeMapping(src, s => (int?) int.Parse(s.value))))
                .ForMember(dest => dest.BayesAverage,
                    opt =>
                        opt.ResolveUsing(
                            src => FormatExceptionSafeMapping(src, s => (float?) float.Parse(s.BayesAverage))));
            configuration.CreateMap<BGGThingsObjects.Video, BGGThings.Video>()
                .ForMember(dest => dest.Link, opt => opt.MapFrom(src => new Uri(src.Link)));
            configuration.CreateMap<BGGThingsObjects.Listing, BGGThings.MarketplaceListing>()
                .ForMember(dest => dest.ListingDate, opt => opt.MapFrom(src => src.ListDate))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Price.Currency))
                .ForMember(dest => dest.CurrencyValue, opt => opt.MapFrom(src => src.Price.value))
                .ForMember(dest => dest.Link, opt => opt.MapFrom(src => new Uri(src.Link.HRef)))
                .ForMember(dest => dest.LinkTitle, opt => opt.MapFrom(src => src.Link.Title));
            configuration.CreateMap<BGGThingsObjects.Things, BGGThings>();
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
