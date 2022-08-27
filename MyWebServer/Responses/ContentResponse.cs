namespace MyWebSurver.Responses
{
    using MyWebSurver.Common;
    using MyWebSurver.Http;
    using System.Text;

    public class ContentResponse : HttpResponse
    {
        public ContentResponse(string text, string contentType) : base(HttpStatusCode.OK)
        {
            Guard.AgainstNull(text);

            this.Headers.Add("Content-Type", contentType);
            this.Headers.Add("Content-Length", $"{Encoding.UTF8.GetByteCount(text)}");

            this.Content = text;
        }
       
    }
}
