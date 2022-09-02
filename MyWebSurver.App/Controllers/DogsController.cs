namespace MyWebSurver.App.Controllers
{
    using MyWebSurver.App.Models.Animals;
    using MyWebSurver.Controllers;
    using MyWebSurver.Http;

    public class DogsController : Controller
    { 
        [HttpGet]
        public HttpResponse Create() => View();

        [HttpPost]
        public HttpResponse Create(DogFormModel model)
            => Text($"Dog: {model.Name} - {model.Age} - {model.Breed}");
    }
}
