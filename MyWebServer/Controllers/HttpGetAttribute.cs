namespace MyWebSurver.Controllers
{
    using MyWebSurver.Http;

    public class HttpGetAttribute : HttpMethodAtribute
    {
        public HttpGetAttribute() : base(HttpMethod.Get)
        {
        }
    }
}
