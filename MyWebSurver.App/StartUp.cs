using MyWebServer;
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
    .MapGet<AccountController>("/Cookie", c => c.CookiesCheck())
    .MapGet<AccountController>("/Session", c => c.SessionCheck())
    .MapGet<AccountController>("/Login", c => c.Login())
    .MapGet<AccountController>("/Logout", c => c.Logout())
    .MapGet<AccountController>("/Authentication", c => c.AuthenticationCheck())
    .MapGet<CatsController>("/Cats/Create", c => c.Create())
    .MapPost<CatsController>("/Cats/Save", c => c.Save()))
    .Start();
