namespace MyWebSurver.Results
{
    using MyWebSurver.Http;
    using MyWebServer.Http.Collections;
    using MyWebSurver.Http.Collections;

    public abstract class ActionResult : HttpResponse
    {
        protected ActionResult(HttpResponse response) : base(response.StatusCode)
        {
            this.Content = response.Content;
            this.StatusCode = response.StatusCode;
            this.PrepareHeaders(response.Headers);
            this.PrapareCookies(response.Cookies);
        }

        protected HttpResponse Response { get; private init; }

        private void PrepareHeaders(HeaderCollection headers)
        {
            foreach (var header in headers)
            {
                this.Headers.Add(header.Name, header.Value);
            }
        }

        private void PrapareCookies(CookieCollection cookies)
        {
            foreach (var cookie in cookies)
            {
                this.Cookies.Add(cookie.Name, cookie.Value);
            }
        }
    }
}
