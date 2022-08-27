namespace MyWebSurver.Responses
{
    using MyWebSurver.Http;

    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string html) : base(html, HttpContentType.Html)
        {
        }
    }
}
