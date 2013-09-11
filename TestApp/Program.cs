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

            var collectionRequest = new BGGCollectionRequest { Username = "pak21", Rated = false, Stats = true };
            var collection = client.GetCollection(collectionRequest);
        }
    }
}
