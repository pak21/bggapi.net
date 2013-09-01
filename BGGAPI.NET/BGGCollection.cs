// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using System.Collections.Generic;
using BGGAPI.BGGCollectionObjects;

namespace BGGAPI
{
    public class BGGCollection
    {
        public int TotalItems { get; set; }
        public string TermsOfUse { get; set; }
        public List<Item> Items { get; set; }
    }
}
