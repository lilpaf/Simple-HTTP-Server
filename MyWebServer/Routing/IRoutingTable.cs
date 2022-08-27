﻿using MyWebSurver.Http;
using HttpMethod = MyWebSurver.Http.HttpMethod;

namespace MyWebSurver.Routing
{
    public interface IRoutingTable
    {
        //void Map(string url, HttpResponse response);

        IRoutingTable Map(HttpMethod method, string path, HttpResponse response);
        
        IRoutingTable Map(HttpMethod method, string path, Func<HttpRequest, HttpResponse> responseFunction);

        IRoutingTable MapGet(string path, HttpResponse response);
        
        IRoutingTable MapGet(string path, Func<HttpRequest, HttpResponse> responseFunction);
        
        IRoutingTable MapPost(string path, Func<HttpRequest, HttpResponse> responseFunction);

        IRoutingTable MapPost(string path, HttpResponse response);

    }
}
