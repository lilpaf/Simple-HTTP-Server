namespace MyWebSurver.Results
{
    using MyWebSurver.Http;

    public class ViewResult : ActionResult
    {
        private const char PathSeparator = '/';

        public ViewResult(HttpResponse respose, string viewPath, string controllerName, object model) 
            : base(respose)
        =>this.GetHtml(viewPath, controllerName, model);
        

        private void GetHtml(string viewName, string controllerName, object model)
        {
            if (!viewName.Contains(PathSeparator))
            {
                viewName = controllerName + PathSeparator + viewName;
            }

            var viewPath = Path.GetFullPath("./Views/" + viewName.TrimStart(PathSeparator) + ".cshtml");

            if (!File.Exists(viewPath))
            {
                this.PrepareMissingViewError(viewPath);
                return;
            }

            var viewContent = File.ReadAllText(viewPath);

            if(model != null)
            {
                viewContent = this.PopulateModel(viewContent, model);
            }

            var layoutPath = Path.GetFullPath("./Views/Layout.cshtml");

            if (File.Exists(layoutPath))
            {
                var layoutContent = File.ReadAllText(layoutPath);

                viewContent = layoutContent.Replace("@Renderbody()", viewContent);
            }

            this.SetContent(viewContent, HttpContentType.Html);
        }

        private void PrepareMissingViewError(string viewPath)
        {
            this.StatusCode = HttpStatusCode.NotFound;

            var errorMesege = $"View '{viewPath}' was not found.";

            this.SetContent(errorMesege, HttpContentType.PlainText);
        }

        private string PopulateModel(string viewContent, object model)
        {
            var data = model.GetType().GetProperties()
                .Select(pr => new
                {
                    Name = pr.Name,
                    Value = pr.GetValue(model)
                });

            foreach (var entry in data)
            {
                viewContent = viewContent.Replace($"@Model.{entry.Name}", entry.Value.ToString());
            }

            return viewContent;
        }
    }
}
