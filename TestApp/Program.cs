// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using System;
using System.Collections.Generic;
using System.Linq;
using BGGAPI;

namespace TestApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var client = new BGGClient();

            var x = new BGGCollectionRequest { Username = "pak21" };
            var y = client.GetCollection(x);
        }
    }
}
