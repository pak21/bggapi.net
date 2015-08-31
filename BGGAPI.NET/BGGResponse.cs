// Copyright (c) Philip Kendall. See LICENSE.txt for more details.

using System.Net;

namespace BGGAPI
{
    public class BGGResponse<T>
    {
        public BGGResponse(HttpStatusCode statusCode, T data)
        {
            StatusCode = statusCode;
            Data = data;
        }

        public HttpStatusCode StatusCode { get; private set; }
        public T Data { get; private set; }
    }
}
