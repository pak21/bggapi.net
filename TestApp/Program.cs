using System;
using System.Linq;
using BGGAPI;

namespace TestApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var client = new BGGClient();

            var collectionRequest = new BGGCollectionRequest { Username = "pak21", Rated = true, Stats = true };
            var collection = client.GetCollection(collectionRequest);

            var gamesToRequest = collection.Items.Select(item => item.ObjectId).Take(1).ToList();
            var gamesRequest = new BGGThingsRequest { Id = gamesToRequest, Stats = true };
            var games = client.GetThings(gamesRequest);
        }
    }
}
