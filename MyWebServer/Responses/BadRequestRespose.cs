namespace MyWebSurver.Responses
{
    using MyWebSurver.Http;

    public class BadRequestRespose : HttpResponse
    {
        public BadRequestRespose() : base(HttpStatusCode.BadRequest)
        {
        }
    }
}
