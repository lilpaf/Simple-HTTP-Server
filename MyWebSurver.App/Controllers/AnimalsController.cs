namespace MyWebSurver.Controllers
{
    using MyWebSurver.App.Models.Animals;
    using MyWebSurver.Http;
    using System.Xml.Linq;

    public class AnimalsController : Controller
    {
        public AnimalsController(HttpRequest request) : base(request)
        {
            
        }

        public HttpResponse Cats()
        {
            const string nameKey = "Name";
            const string ageKey = "Age";

            var query = this.Request.Query;

            var catName = query.ContainsKey(nameKey)
            ? query[nameKey]
            : "the cats";

            var catAge = query.ContainsKey(ageKey)
                ? int.Parse(query[ageKey])
            : 0;

            var viewModel = new CatViewModel{
                Name = catName,
                Age = catAge
            }; 

            return View(viewModel);
        }

        public HttpResponse Dogs() => View(new DogViewModel
        {
            Name = "Boris",
            Age = 5,
            Breed = "Golden" 
        });
        
        public HttpResponse Bunnies() => View("Rabbits");
        
        public HttpResponse Turtles() => View("Animals/Wild/Turtles");
        
    }
}
