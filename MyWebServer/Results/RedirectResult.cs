namespace MyWebSurver.Results
{
    using MyWebSurver.Http;

    public class RedirectResult : ActionResult
    {
        public RedirectResult(HttpResponse response, string location) 
            : base(response)
        {
            this.StatusCode = HttpStatusCode.Found;
            this.Headers.Add(HttpHeader.Location, location);
        }
        
    }
}
