namespace MyWebSurver.Controllers
{
    using MyWebSurver.Http;
    public class HttpPostAttribute : HttpMethodAtribute
    {
        public HttpPostAttribute() : base(HttpMethod.Post)
        {
        }
    }
}
