using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BGGAPI;

namespace WantToPlay
{
    class Program
    {
        private static readonly IList<string> Usernames = new List<string>
        {
            "pak21",
            "Puddle_101",
            "dogbomb",
            "dghughes",
            "tarnop",
            "mischip",
            "bigflyer",
            "mish666uk",
            "mike3838",
            "count_zero99uk",
            "ezekremiah",
            "Quartez"
        };

        private static readonly object LockObject = new object();

        private static readonly IDictionary<int, List<string>> WantToPlay = new Dictionary<int, List<string>>();

        private static readonly IDictionary<int, List<string>> Owners = new Dictionary<int, List<string>>();

        private static readonly IDictionary<int, string> GameNames = new Dictionary<int, string>();

        private static void Main(string[] args)
        {
            var client = new BGGClient();

            var tasks = Usernames.Select(username => FetchUser(client, username, true)).ToArray();

            Task.WaitAll(tasks);

            foreach (var wantToPlay in WantToPlay)
            {
                var id = wantToPlay.Key;

                List<string> owners;
                if (Owners.TryGetValue(id, out owners))
                {
                    Console.WriteLine("{0}\t{1}\t{2}", GameNames[id], string.Join(",", wantToPlay.Value), string.Join(",", owners));
                }
            }
        }

        private async static Task FetchUser(BGGClient client, string username, bool retry)
        {
            var request = new BGGCollectionRequest { Username = username };

            var task = client.GetCollectionAsync(request);
            var response = await task;

            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                if (retry)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(30));
                    await FetchUser(client, username, false);
                    return;
                }
                else
                {
                    Console.WriteLine("Failed to fetch collection at second attempt for {0}", username);
                }
            }

            var collection = response.Data;

            lock (LockObject)
            {
                foreach (var game in collection.Items)
                {
                    var id = game.Id;

                    GameNames[id] = game.Name;

                    if (game.Owned)
                    {
                        var owners = Owners.GetOrCreate(id, () => new List<string>());
                        owners.Add(username);
                    }

                    if (game.WantToPlay)
                    {
                        var players = WantToPlay.GetOrCreate(id, () => new List<string>());
                        players.Add(username);
                    }
                }
            }
        }
    }
}
