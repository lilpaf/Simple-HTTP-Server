namespace MyWebSurver.Controllers
{
    using MyWebSurver.Http;
    using MyWebSurver.Controllers;

    public class AnumalsController : Controller
    {
        public AnumalsController(HttpRequest request) : base(request)
        {
            
        }

        public HttpResponse Cats()
        {
            const string nameKey = "Name";

            var query = this.Request.Query;

            var catName = query.ContainsKey(nameKey)
            ? query[nameKey]
            : "the cats";

            var result = $"<h1>Hello from {catName}!</h1>";

            return Html(result);
        }

        public HttpResponse Dogs()
        => View("/Views/Animals/Dogs.cshtml");
        
    }
}
