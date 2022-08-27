namespace MyWebSurver.Controllers
{
    using MyWebSurver.Http;
    using MyWebSurver.Routing;
    using System;

    public static class RoutingTableExtensions
    {
        public static IRoutingTable MapGet<TController>(
            this IRoutingTable routingTable,
            string path,
            Func<TController, HttpResponse> contollerFunction)
            where TController : Controller
            => routingTable.MapGet(path,
                request => contollerFunction(CreateController<TController>(request)));
          

        public static IRoutingTable MapPost<TController>(
            this IRoutingTable routingTable,
            string path,
            Func<TController, HttpResponse> contollerFunction)
            where TController : Controller
         => routingTable.MapPost(path,
                request => contollerFunction(CreateController<TController>(request)));

        private static TController CreateController<TController>(HttpRequest request)
            => (TController)Activator.CreateInstance(typeof(TController), new[] { request });
    }
}
