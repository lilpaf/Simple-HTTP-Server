using MyWebServer;
using MyWebSurver.Controllers;
using MyWebSurver.Responses;
using MyWebSurver.Results;

await new HttpServer(routes => routes
    .MapGet<HomeController>("/", c => c.Index())
    .MapGet<HomeController>("/Softuni", c => c.ToSoftUni())
    .MapGet<HomeController>("/ToCats", c => c.LocalRedirect())
    .MapGet<AnumalsController>("/Cats", c => c.Cats())
    .MapGet<AnumalsController>("/Dogs", c => c.Dogs()))
    .Start();
