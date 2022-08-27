namespace MyWebSurver.Controllers
{
    using MyWebSurver.Http;
    using MyWebSurver.Results;
    using MyWebSurver.Controllers;

    public class HomeController : Controller
    {
        public HomeController(HttpRequest request) : base(request)
        {
        }

        public HttpResponse Index() => Text("Hello from Pavel!");

        public HttpResponse LocalRedirect() => Redirect("/Cats");

        public HttpResponse ToSoftUni() => Redirect("https://softuni.bg"); 
    }
}
