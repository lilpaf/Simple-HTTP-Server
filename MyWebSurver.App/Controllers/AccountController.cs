﻿namespace MyWebSurver.App.Controllers
{
    using MyWebSurver.Controllers;
    using MyWebSurver.Http;

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

        public HttpResponse ActionWithSession()
        {
            const string currentDateKey = "CurrentDate";

            if (this.Request.Session.ContainsKey(currentDateKey))
            {
                var currentDate = this.Request.Session[currentDateKey];

                return Text($"Stored date: {currentDate}");
            }

            this.Request.Session[currentDateKey] = DateTime.UtcNow.ToString();

            return Text("Current date stored");
        }
    }
}
