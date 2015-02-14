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
            configuration.CreateMap<BGGThingsObjects.Link, Link>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.value));
            configuration.CreateMap<BGGThingsObjects.Name, Name>();
            configuration.CreateMap<BGGThingsObjects.Poll, Poll>()
                .ForMember(dest => dest.Votes, opt => opt.MapFrom(src => src.TotalVotes));

            Mapper = new MappingEngine(configuration);
        }

        public static readonly MappingEngine Mapper;

        public BGGThings(BGGThingsObjects.Things rawThings)
        {
            TermsOfUse = rawThings.TermsOfUse;
            Items = rawThings.Items.Select(i => new Item(i)).ToList();
        }

        public string TermsOfUse { get; private set; }
        public IList<Item> Items { get; private set; }

        public class Item
        {
            public Item(BGGThingsObjects.Item rawItem)
            {
                Type = rawItem.Type;
                Id = rawItem.Id;
                Image = rawItem.Image;
                Thumbnail = rawItem.Thumbnail;
                Names = rawItem.Names.Select(Mapper.Map<Name>).ToList();
                Description = rawItem.Description;
                YearPublished = rawItem.YearPublished.value;
                MinimumPlayers = rawItem.MinPlayers.value;
                MaximumPlayers = rawItem.MaxPlayers.value;
                Polls = rawItem.Polls.Select(Mapper.Map<Poll>).ToList();
                PlayingTime = TimeSpan.FromMinutes(rawItem.PlayingTime.value);
                MinimumAge = rawItem.MinAge.value;
                Categories = rawItem.Links.Where(l => l.Type == "boardgamecategory").Select(Mapper.Map<Link>).ToList();
                Links = rawItem.Links.Select(Mapper.Map<Link>).ToList();
                // Videos
                // Statistics
                // MarketplaceListings
            }

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
            //public Statistics Statistics { get; private set; }
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
