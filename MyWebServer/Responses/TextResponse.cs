namespace MyWebSurver.Results
{
    using MyWebSurver.Http;
    using MyWebSurver.Responses;

    public class TextResponse : ContentResponse
    {
        public TextResponse(string text) 
            : base(text, HttpContentType.PlainText)
        {
        }
    }
}
