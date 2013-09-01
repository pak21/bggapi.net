// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using System;
using System.Collections.Generic;

namespace BGGAPI
{
    /// <summary>
    /// The information needed to query a user's collection
    /// </summary>
    public class BGGCollectionRequest
    {
        /// <summary>
        /// The user whose collection information is being requested
        /// </summary>
        public string Username { get; set; }

        // TODO: not implemented yet - need to deal with multiple different <item> tags
        //public bool Version { get; set; }

        // TODO: work out serialization etc
        // public List<SomeType> Subtype { get; set; }
        // public List<SomeType> ExcludeSubtype { get; set; }

        /// <summary>
        /// If non-null, filter the collection to the specifically listed items
        /// </summary>
        public List<int> Id { get; set; }

        /// <summary>
        /// If set to true, return abbreviated results
        /// </summary>
        public bool Brief { get; set; }

        /// <summary>
        /// If set to true, requests that expanded stats, rating and ranking information be returned
        /// </summary>
        public bool Stats { get; set; }

        /// <summary>
        /// If set to true (false), return only items which are owned by this user. If null, return all items.
        /// </summary>
        public bool? Own { get; set; }

        /// <summary>
        /// If set to true (false), return only items which have (not) been rated. If null, return all items.
        /// </summary>
        public bool? Rated { get; set; }

        /// <summary>
        /// If set to true (false), return only items which have (not) been played. If null, return all items.
        /// </summary>
        public bool? Played { get; set; }

        /// <summary>
        /// If set to true (false), return only items which have (not) been commented on. If null, return all items.
        /// </summary>
        public bool? Comment { get; set; }

        /// <summary>
        /// If set to true (false), return only items which are (not) marked for trade. If null, return all items.
        /// </summary>
        public bool? Trade { get; set; }

        /// <summary>
        /// If set to true (false), return only items which are (not) wanted in trade. If null, return all items.
        /// </summary>
        public bool? Want { get; set; }

        /// <summary>
        /// If set to true (false), return only items which are (not) on the user's wishlist. If null, return all items.
        /// </summary>
        public bool? Wishlist { get; set; }

        /// <summary>
        /// If set, return only items with the corresponding wishlist priority (1-5). If null, return all items.
        /// </summary>
        public int? WishlistPriority { get; set; }

        /// <summary>
        /// If set to true (false), return only items which are (not) wanted to play. If null, return all items.
        /// </summary>
        public bool? WantToPlay { get; set; }

        /// <summary>
        /// If set to true (false), return only items which are (not) wanted to buy. If null, return all items.
        /// </summary>
        public bool? WantToBuy { get; set; }

        /// <summary>
        /// If set to true (false), return only items which were previously (not) owned by this user. If null, return all items.
        /// </summary>
        public bool? PrevOwned { get; set; }

        /// <summary>
        /// If set to true (false), return only items which have (do not have) a "Has parts" comment. If null, return all items.
        /// </summary>
        public bool? HasParts { get; set; }

        /// <summary>
        /// If set to true (false), return only items which have (do nothave ) a "Wants parts" comment. If null, return all items.
        /// </summary>
        public bool? WantsParts { get; set; }

        /// <summary>
        /// If set, return only items with at least this personal rating. If null, return all items.
        /// </summary>
        public int? MinRating { get; set; }

        /// <summary>
        /// If set, return only items with a personal rating up to this value (inclusive). If null, return all items.
        /// </summary>
        public int? MaxRating { get; set; }

        /// <summary>
        /// If set, return only items with at least this BoardGameGeek rating. If null, return all items.
        /// </summary>
        public float? MinBGGRating { get; set; }

        /// <summary>
        /// If set, return only items with a BoardGameGeek rating up to this value (inclusive). If null, return all items.
        /// </summary>
        public float? MaxBGGRating { get; set; }

        // TODO: not point having this until we deal with cookies etc for login
        //public bool ShowPrivate { get; set; }

        // TODO: what does this actually do?
        //public int? CollId { get; set; }

        // If set, return only items with a status (own, want, fortrade etc) changed or added since this date. If null, return all items.   
        public DateTime? ModifiedSince { get; set; }
    }
}
