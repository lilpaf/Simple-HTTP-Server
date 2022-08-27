﻿using MyWebServer;
using MyWebSurver.Controllers;

await new HttpServer(routes => routes
    .MapGet<HomeController>("/", c => c.Index())
    .MapGet<HomeController>("/Softuni", c => c.ToSoftUni())
    .MapGet<HomeController>("/ToCats", c => c.LocalRedirect())
    .MapGet<AnimalsController>("/Cats", c => c.Cats())
    .MapGet<AnimalsController>("/Dogs", c => c.Dogs())
    .MapGet<AnimalsController>("/Bunnies", c => c.Bunnies())
    .MapGet<AnimalsController>("/Turtles", c => c.Turtles()))
    .Start();
