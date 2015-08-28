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

            var thingsRequest = new BGGThingsRequest { Id = new List<int> {68448}, Stats = true };
            var things = client.GetThings(thingsRequest);
        }
    }
}
