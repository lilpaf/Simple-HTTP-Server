namespace MyWebSurver.Responses
{
    using MyWebSurver.Http;

    public class NotFoundResponse : HttpResponse
    {
        public NotFoundResponse() : base(HttpStatusCode.NotFound)
        {
        }
    }
}
