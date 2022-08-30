namespace MyWebSurver.Results
{
    using MyWebSurver.Http;

    public class HtmlResult : ContentResult
    {
        public HtmlResult(HttpResponse response, string html) : base(response, html, HttpContentType.Html)
        {
        }
    }
}
