namespace MyWebSurver.Results
{
    using MyWebSurver.Http;
    using System.Reflection.PortableExecutable;

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

        private void PrepareHeaders(IDictionary<string, HttpHeader> headers)
        {
            foreach (var header in headers.Values)
            {
                this.AddHeader(header.Name, header.Value);
            }
        }

        private void PrapareCookies(IDictionary<string, HttpCookie> cookies)
        {
            foreach (var cookie in cookies.Values)
            {
                this.AddCookie(cookie.Name, cookie.Value);
            }
        }
    }
}
