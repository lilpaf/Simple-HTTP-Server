namespace MyWebSurver.Responses
{
    using MyWebSurver.Http;
    public class ViewResponse : HttpResponse
    {
        private readonly string filePath;

        public ViewResponse(string filePath) 
            : base(HttpStatusCode.OK)
        {
            this.GetHtml(filePath);
        }

        private void GetHtml(string filePath)
        {
            var directory = Directory.GetCurrentDirectory();
            var viewPath = Path.Combine(directory, filePath);

            if (!File.Exists(viewPath))
            {
                this.StatusCode = HttpStatusCode.NotFound;
            }
        }
    }
}
