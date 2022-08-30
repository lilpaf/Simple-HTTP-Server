﻿using MyWebServer;
using MyWebSurver.App.Controllers;
using MyWebSurver.Controllers;

await new HttpServer(routes => routes
    .MapGet<HomeController>("/", c => c.Index())
    .MapGet<HomeController>("/Softuni", c => c.ToSoftUni())
    .MapGet<HomeController>("/Error", c => c.Error())
    .MapGet<HomeController>("/ToCats", c => c.LocalRedirect())
    .MapGet<AnimalsController>("/Cats", c => c.Cats())
    .MapGet<AnimalsController>("/Dogs", c => c.Dogs())
    .MapGet<AnimalsController>("/Bunnies", c => c.Bunnies())
    .MapGet<AnimalsController>("/Turtles", c => c.Turtles())
    .MapGet<AccountController>("/Cookie", c => c.ActionWithCookies())
    .MapGet<AccountController>("/Session", c => c.ActionWithSession())
    .MapGet<CatsController>("/Cats/Create", c => c.Create())
    .MapPost<CatsController>("/Cats/Save", c => c.Save()))
    .Start();
