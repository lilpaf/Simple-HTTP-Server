﻿namespace MyWebSurver.Controllers
{
    using MyWebSurver.Http;

    public class HomeController : Controller
    {
        public HttpResponse Index() => Text("Hello from Pavel!");

        public HttpResponse LocalRedirect() => Redirect("/Animals/Cats");

        public HttpResponse ToSoftUni() => Redirect("https://softuni.bg");
        
        public HttpResponse StaticFiles() => View();

        public HttpResponse Error() => throw new InvalidOperationException("Invalid action");
    }
}
