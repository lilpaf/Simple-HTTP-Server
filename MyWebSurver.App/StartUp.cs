using MyWebServer;
using MyWebSurver.Controllers;

await new HttpServer(routes => routes
    .MapStaticFiles()
    .MapControllers()
    .MapGet<HomeController>("/Softuni", c => c.ToSoftUni()))
    .Start();