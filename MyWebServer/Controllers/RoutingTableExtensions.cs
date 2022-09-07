namespace MyWebSurver.Controllers
{
    using MyWebSurver.Http;
    using MyWebSurver.Routing;
    using System;
    using System.Reflection;

    public static class RoutingTableExtensions
    {
        private static Type stringType = typeof(string);
        private static Type httpResponseType = typeof(HttpResponse);

        public static IRoutingTable MapGet<TController>(
            this IRoutingTable routingTable,
            string path,
            Func<TController, HttpResponse> contollerFunction)
            where TController : Controller
            => routingTable.MapGet(path,
                request => contollerFunction(CreateController<TController>(request)));


        public static IRoutingTable MapPost<TController>(
            this IRoutingTable routingTable,
            string path,
            Func<TController, HttpResponse> contollerFunction)
            where TController : Controller
         => routingTable.MapPost(path,
                request => contollerFunction(CreateController<TController>(request)));

        public static IRoutingTable MapControllers(this IRoutingTable routingTable)
        {
            var controllersActions = GetControllerActions();

            foreach (var controllerAction in controllersActions)
            {
                var controllerName = controllerAction.DeclaringType.GetControllerName();
                var actionName = controllerAction.Name;

                var path = $"/{controllerName}/{actionName}";

                var responseFunction = GetResponseFunction(controllerAction);

                var httpMethod = HttpMethod.Get;

                var httpMethodAttribute = controllerAction
                    .GetCustomAttribute<HttpMethodAtribute>();

                if (httpMethodAttribute != null)
                {
                    httpMethod = httpMethodAttribute.HttpMethod;
                }

                routingTable.Map(httpMethod, path, responseFunction);

                MapDefaultRoutes(routingTable, httpMethod, controllerName, actionName, responseFunction);
            }

            return routingTable;
        }

        private static IEnumerable<MethodInfo> GetControllerActions()
            => Assembly
                .GetEntryAssembly()
                .GetExportedTypes()
                .Where(t => !t.IsAbstract &&
                t.IsAssignableTo(typeof(Controller))
                && t.Name.EndsWith(nameof(Controller)))
                .SelectMany(t => t
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(m => m.ReturnType.IsAssignableTo(httpResponseType)))
                .ToList();

        private static Func<HttpRequest, HttpResponse> GetResponseFunction(MethodInfo controllerAction)
            => request =>
            {
                if (!UserIsAuthorise(controllerAction, request.Session))
                {
                    return new HttpResponse(HttpStatusCode.Unauthorized);
                }

                var controllerIstance = CreateController(controllerAction.DeclaringType, request);

                var parameterValues = GetParameterValues(controllerAction, request);

                return (HttpResponse)controllerAction.Invoke(controllerIstance, parameterValues);
            };

        private static TController CreateController<TController>(HttpRequest request)
            where TController : Controller
            => (TController)CreateController(typeof(TController), request);

        private static Controller CreateController(Type controllerType, HttpRequest request)
        {
            var controller = (Controller)request.Services.CreateInstance(controllerType);

            controllerType.GetProperty("Request", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(controller, request);

            return controller;
        }


        private static void MapDefaultRoutes(
            IRoutingTable routingTable,
            HttpMethod httpMethod,
            string controllerName,
            string actionName,
            Func<HttpRequest, HttpResponse> responseFunction)
        {
            const string defaultActionName = "Index";
            const string defaultControllerName = "Home";

            if (actionName == defaultActionName)
            {
                routingTable.Map(httpMethod, $"/{controllerName}", responseFunction);

                if (controllerName == defaultControllerName)
                {
                    routingTable.Map(httpMethod, "/", responseFunction);
                }
            }
        }

        private static bool UserIsAuthorise(
            MethodInfo controllerAction,
            HttpSession session)
        {
            var authorisationRequired = controllerAction
                .DeclaringType
                .GetCustomAttribute<AuthorizeAttribute>()
                ?? controllerAction
                .GetCustomAttribute<AuthorizeAttribute>();

            if (authorisationRequired != null)
            {
                var userIsAuthorised = session.Contains(Controller.UserSessionKey)
                && session[Controller.UserSessionKey] != null;

                if (!userIsAuthorised)
                {
                    return false;
                }
            }

            return true;
        }

        private static object[] GetParameterValues(MethodInfo controllerAction, HttpRequest request)
        {
            var actionParamets = controllerAction
                .GetParameters()
                .Select(p => new
                {
                    p.Name,
                    Type = p.ParameterType,
                })
                .ToArray();

            var parameterValues = new object[actionParamets.Length];

            for (int i = 0; i < actionParamets.Length; i++)
            {
                var parameter = actionParamets[i];
                var parameterName = parameter.Name;
                var parameterType = parameter.Type;

                if (parameterType.IsPrimitive || parameterType == stringType)
                {
                    var parameterValue = request.GetValue(parameterName);

                    parameterValues[i] = Convert.ChangeType(parameterValue, parameterType);
                }
                else
                {
                    var parameterValue = Activator.CreateInstance(parameterType);
                    
                    var parameterProperties = parameterType.GetProperties();

                    foreach (var property in parameterProperties)
                    {
                        var propertyValue = request.GetValue(property.Name);

                        property.SetValue(parameterValue, Convert.ChangeType(propertyValue , property.PropertyType));
                    }

                    parameterValues[i] = parameterValue;
                }
            }

            return parameterValues;
        }

        private static string GetValue(this HttpRequest request, string name)
            => request.Query.GetValueOrDefault(name) ??
                 request.Form.GetValueOrDefault(name);
    }
}
