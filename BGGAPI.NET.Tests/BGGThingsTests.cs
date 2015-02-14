using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BGGAPI.NET.Tests
{
    [TestFixture]
    public class BGGThingsTests
    {
        [Test]
        public void TestNameConversion()
        {
            var rawName = new BGGThingsObjects.Name
            {
                Type = "NameType",
                SortIndex = 2,
                value = "Name value"
            };

            var name = new BGGThings.Name(rawName);

            Assert.AreEqual("NameType", name.Type);
            Assert.AreEqual(2, name.SortIndex);
            Assert.AreEqual("Name value", name.Value);
        }

        [Test]
        public void TestPollConversion()
        {
            var rawPoll = new BGGThingsObjects.Poll
            {
                Name = "Poll name",
                Title = "Poll title",
                TotalVotes = 1234
            };

            var poll = new BGGThings.Poll(rawPoll);

            Assert.AreEqual("Poll name", poll.Name);
            Assert.AreEqual("Poll title", poll.Title);
            Assert.AreEqual(1234, poll.Votes);
        }

        [Test]
        public void TestLinkConversion()
        {
            var rawLink = new BGGThingsObjects.Link
            {
                Id = 1234,
                Type = "Link type",
                value = "Link value"
            };

            var link = new BGGThings.Link(rawLink);

            Assert.AreEqual(1234, link.Id);
            Assert.AreEqual("Link value", link.Name);
            Assert.AreEqual("Link type", link.Type);
        }

        [Test]
        public void TestItemConversion()
        {
            var rawItem = new BGGThingsObjects.Item()
            {
                Description = "Item description",
                Id = 1234,
                Image = "http://www.example.com/image.jpeg",
                Links = new List<BGGThingsObjects.Link> {
                    new BGGThingsObjects.Link { Type = "boardgamecategory", value = "Category1" },
                    new BGGThingsObjects.Link { Type = "foo", value = "bar" },
                },
                MaxPlayers = new BGGSharedObjects.IntValue { value = 4 },
                MinAge = new BGGSharedObjects.IntValue { value = 18 },
                MinPlayers = new BGGSharedObjects.IntValue { value = 1 },
                Names = new List<BGGThingsObjects.Name> {
                    new BGGThingsObjects.Name { value = "A" },
                    new BGGThingsObjects.Name { value = "B" }
                },
                PlayingTime = new BGGSharedObjects.IntValue { value = 60 },
                Polls = new List<BGGThingsObjects.Poll> {
                    new BGGThingsObjects.Poll { Name = "C" },
                    new BGGThingsObjects.Poll { Name = "D" },
                    new BGGThingsObjects.Poll { Name = "E" }
                },
                Thumbnail = "http://www.example.com/thumbnail.jpeg",
                Type = "Item type",
                YearPublished = new BGGSharedObjects.IntValue { value = 2000 }
            };

            var item = new BGGThings.Item(rawItem);

            Assert.AreEqual(1, item.Categories.Count);
            Assert.AreEqual("Category1", item.Categories[0].Name);
            Assert.AreEqual("Item description", item.Description);
            Assert.AreEqual(1234, item.Id);
            Assert.AreEqual("http://www.example.com/image.jpeg", item.Image);
            Assert.AreEqual(2, item.Links.Count);
            Assert.AreEqual("bar", item.Links[1].Name);
            Assert.AreEqual(4, item.MaximumPlayers);
            Assert.AreEqual(18, item.MinimumAge);
            Assert.AreEqual(1, item.MinimumPlayers);
            Assert.AreEqual(2, item.Names.Count);
            Assert.AreEqual("A", item.Names[0].Value);
            Assert.AreEqual(TimeSpan.FromMinutes(60), item.PlayingTime);
            Assert.AreEqual(3, item.Polls.Count);
            Assert.AreEqual("C", item.Polls[0].Name);
            Assert.AreEqual("http://www.example.com/thumbnail.jpeg", item.Thumbnail);
            Assert.AreEqual("Item type", item.Type);
            Assert.AreEqual(2000, item.YearPublished);
        }
    }
}
