namespace MyWebSurver.Results
{
    using MyWebSurver.Http;

    public class ContentResult : ActionResult
    {
        public ContentResult(HttpResponse response, string content, string contentType) 
            : base(response) 
            =>PrepareContent(content, contentType);
        
    }
}
