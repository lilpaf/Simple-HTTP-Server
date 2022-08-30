namespace MyWebSurver.App.Controllers
{
    using MyWebSurver.Controllers;
    using MyWebSurver.Http;
    using MyWebSurver.Results;

    public class AccountController : Controller
    {
        public AccountController(HttpRequest request) : base(request)
        {
        }

        public HttpResponse ActionWithCookies()
        {
            const string cookieName = "My-Cookie";

            if (this.Request.Cookies.ContainsKey(cookieName))
            {
                var cookie = this.Request.Cookies[cookieName];

                return Text($"Cookies already exists - {cookie}");
            }

            this.Response.AddCookie(cookieName, "My-Value");
            this.Response.AddCookie("My-Cookie2", "My-Value2");

            return Text("Cookies set!");
        }
    }
}
