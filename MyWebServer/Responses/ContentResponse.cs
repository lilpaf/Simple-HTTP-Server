namespace MyWebSurver.Responses
{
    using MyWebSurver.Http;

    public class ContentResponse : HttpResponse
    {
        public ContentResponse(string content, string contentType) 
            : base(HttpStatusCode.OK) 
            =>PrepareContent(content, contentType);
        
    }
}
