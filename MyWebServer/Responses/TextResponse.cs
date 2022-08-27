namespace MyWebSurver.Results
{
    using MyWebSurver.Common;
    using MyWebSurver.Http;
    using MyWebSurver.Responses;
    using System.Text;

    public class TextResponse : ContentResponse
    {
        public TextResponse(string text) 
            : base(text, "text/plain; charset=UTF-8")
        {
        }
    }
}
