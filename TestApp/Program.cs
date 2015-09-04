// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using System.Linq;
using System.Threading.Tasks;
using BGGAPI;

namespace TestApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var client = new BGGClient();

            var usernames = new[] {"dghughes", "pak21"};
            var tasks = usernames.Select(async username => await client.GetCollectionAsync(new BGGCollectionRequest {Username = username})).ToArray();

            Task.WaitAll(tasks);
        }
    }
}
