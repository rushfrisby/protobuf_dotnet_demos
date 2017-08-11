﻿namespace ProtobufStuff.HttpListenerDemo
{
    public class RouteResponse
    {
        public RouteResponse()
        {
        }

        public RouteResponse(string contentType)
        {
            ContentType = contentType;
        }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }
    }
}
