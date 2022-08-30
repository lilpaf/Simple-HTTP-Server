namespace MyWebSurver.Results
{
    using MyWebSurver.Http;

    public class TextResult : ContentResult
    {
        public TextResult(HttpResponse response, string text) 
            : base(response, text, HttpContentType.PlainText)
        {
        }
    }
}
