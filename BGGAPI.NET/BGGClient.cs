﻿// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BGGAPI.Raw.Collection;
using BGGAPI.Raw.Things;
using RestSharp;

namespace BGGAPI
{
    /// <summary>
    /// An object which can be used to query the BoardGameGeek API
    /// </summary>
    public class BGGClient
    {
        #region Public methods

        /// <summary>
        /// Requests information about a user's collection in an asynchronous manner
        /// </summary>
        /// <param name="collectionRequest">Details of the request</param>
        /// <returns>A task which returns details of the user's collection</returns>
        public async Task<BGGResponse<BGGCollection>> GetCollectionAsync(BGGCollectionRequest collectionRequest)
        {
            if (string.IsNullOrEmpty(collectionRequest.Username))
                throw new ArgumentException("Null or empty username in collectionRequest");

            return await CallBGGAsync<Collection, BGGCollection>("collection", collectionRequest, BGGFactory.CreateCollection);
        }

        /// <summary>
        /// Requests information about specific BGG objects in an asychronous manner
        /// </summary>
        /// <param name="thingsRequest">Details of the request</param>
        /// <returns>A task which returns details on the requested objects</returns>
        public async Task<BGGResponse<BGGThings>> GetThingsAsync(BGGThingsRequest thingsRequest)
        {
            if (thingsRequest.Id == null || !thingsRequest.Id.Any())
                throw new ArgumentException("Null or empty list of IDs in thingsRequest");

            return await CallBGGAsync<Things, BGGThings>("thing", thingsRequest, BGGFactory.CreateThings);
        }

        #endregion


        #region Private methods

        private static async Task<BGGResponse<TProcessedType>> CallBGGAsync<TRawType, TProcessedType>(string resource,
            object request,
            Func<TRawType, TProcessedType> createProcessedObject) where TRawType : new()
        {
            var client = new RestClient {BaseUrl = Constants.DefaultApiAddress};

            var restRequest = new RestRequest {Resource = resource};
            foreach (var parameter in SerializeRequest(request))
                restRequest.AddParameter(parameter.Key, parameter.Value);

            var response = await client.ExecuteTaskAsync<TRawType>(restRequest);
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            var processedObject = createProcessedObject(response.Data);
            return new BGGResponse<TProcessedType>(response.StatusCode, processedObject);
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
                    { typeof(BGGSubtype), o => o.ToString().ToLower() }
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

        #endregion
    }
}
