namespace MyWebSurver.Results
{
    using MyWebSurver.Http;

    public class BadRequestResult : HttpResponse
    {
        public BadRequestResult() : base(HttpStatusCode.BadRequest)
        {
        }
    }
}
