// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using BGGAPI;

namespace TestApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var client = new BGGClient();

            var request = new BGGCollectionRequest {Username = "pak21"};
            var response = client.GetCollection(request);
        }
    }
}
