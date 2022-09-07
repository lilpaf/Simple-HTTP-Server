using MyWebServer;
using MyWebSurver.App.Data;
using MyWebSurver.Controllers;

    await HttpServer
    .WithRoutes(routes => routes
    .MapStaticFiles()
    .MapControllers())
    //.MapGet<HomeController>("/Softuni", c => c.ToSoftUni()))
    .WithServices(services => services
    .Add<IData, MyDbContext>())
    .Start();