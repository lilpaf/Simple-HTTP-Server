namespace MyWebSurver.App.Controllers
{
    using MyWebSurver.Controllers;
    using MyWebSurver.Http;

    public class CatsController : Controller
    {
        public CatsController(HttpRequest request) 
            : base(request)
        {
        }

        [HttpGet]
        public HttpResponse Create() => View();

        [HttpPost]
        public HttpResponse Save(string name, int age) 
            => Text($"{name} - {age}");
        
    }
}
