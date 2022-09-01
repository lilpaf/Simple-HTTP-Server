using MyWebServer;
using MyWebSurver.App.Controllers;
using MyWebSurver.Controllers;

// Browsers like safari and chrome don't display favicon if it is local like ours!

await new HttpServer(routes => routes
    .MapStaticFiles()
    .MapControllers()
    .MapGet<HomeController>("/Softuni", c => c.ToSoftUni())
    .MapPost<CatsController>("/Cats/Save", c => c.Save()))
    .Start();