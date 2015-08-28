// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using System;
using System.Collections.Generic;
using System.Linq;
using BGGAPI.BGGCollectionObjects;
using RestSharp;
using BGGAPI.BGGThingsObjects;

namespace BGGAPI
{
    /// <summary>
    /// An object which can be used to query the BoardGameGeek API
    /// </summary>
    public class BGGClient
    {
        private T CallBGG<T>(string resource, object request) where T : new()
        {
            var client = new RestClient {BaseUrl = Constants.DefaultApiAddress};

            var restRequest = new RestRequest { Resource = resource };
            foreach (var parameter in SerializeRequest(request))
                restRequest.AddParameter(parameter.Key, parameter.Value);

            var response = client.Execute<T>(restRequest);
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            return response.Data;
        }

        /// <summary>
        /// Requests information about a user's collection
        /// </summary>
        /// <param name="collectionRequest">Details of the request</param>
        /// <returns>Details of the user's collection</returns>
        public BGGCollection GetCollection(BGGCollectionRequest collectionRequest)
        {
            if (string.IsNullOrEmpty(collectionRequest.Username))
                throw new ArgumentException("Null or empty username in collectionRequest");

            var rawCollection = CallBGG<Collection>("collection", collectionRequest);
            return BGGCollection.Mapper.Map<BGGCollection>(rawCollection);
        }

        /// <summary>
        /// Requests information about specific BGG objects
        /// </summary>
        /// <param name="thingsRequest">Details of the request</param>
        /// <returns>Details on the requested objects</returns>
        public BGGThings GetThings(BGGThingsRequest thingsRequest)
        {
            if (thingsRequest.Id == null || !thingsRequest.Id.Any())
                throw new ArgumentException("Null or empty list of IDs in thingsRequest");

            var rawThings = CallBGG<Things>("thing", thingsRequest);
            return BGGThings.Mapper.Map<BGGThings>(rawThings);
        }

        private const string ZeroString = "0";
        private const string OneString = "1";

        private static readonly IDictionary<Type, Func<object, string>> TypeSerializers =
            new Dictionary<Type, Func<object, string>>
                {
                    { typeof(bool), o => (bool)o ? OneString : null },
                    // We list bool? explicitly as we want different behaviour in the false case from the
                    // simple bool serialization
                    { typeof(bool?), o => ((bool?)o).Value ? OneString : ZeroString },
                    { typeof(DateTime), o => ((DateTime)o).ToString("yy-MM-dd HH:mm:ss") },
                    { typeof(List<int>), o => string.Join(",", (List<int>)o) },
                };

        // ReSharper disable once ReturnTypeCanBeEnumerable.Local - clearer as a Dictionary
        private static IDictionary<string, string> SerializeRequest(object request)
        {
            var parameters = new Dictionary<string, string>();

            foreach (var propertyInfo in request.GetType().GetProperties())
            {
                var value = propertyInfo.GetValue(request);
                if (value != null)
                {
                    Func<object, string> serializer;

                    var propertyType = propertyInfo.PropertyType;
                    if (!TypeSerializers.TryGetValue(propertyType, out serializer))
                    {
                        // If this was a nullable type, look for a serializer for the underlying type
                        var underlyingType = Nullable.GetUnderlyingType(propertyType);
                        if (underlyingType != null)
                        {
                            TypeSerializers.TryGetValue(underlyingType, out serializer);
                        }
                    }

                    var serializedValue = serializer == null ? value.ToString() : serializer(value);
                    if (serializedValue != null)
                        parameters.Add(propertyInfo.Name.ToLower(), serializedValue);
                }
            }

            return parameters;
        }
    }
}
