namespace MyWebSurver.Controllers
{
    using HttpMethod = MyWebSurver.Http.HttpMethod;

    [AttributeUsage(AttributeTargets.Method)]
    public abstract class HttpMethodAtribute : Attribute
    {
        protected HttpMethodAtribute(HttpMethod httpMethod) 
            => this.HttpMethod = httpMethod;

        public HttpMethod HttpMethod { get; }
    }
}
