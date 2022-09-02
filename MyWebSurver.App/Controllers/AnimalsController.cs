namespace MyWebSurver.Controllers
{
    using MyWebSurver.App.Models.Animals;
    using MyWebSurver.Http;

    public class AnimalsController : Controller
    {
        public HttpResponse Cats()
        {
            const string nameKey = "Name";
            const string ageKey = "Age";

            var query = this.Request.Query;

            var catName = query.Contains(nameKey)
            ? query[nameKey]
            : "the cats";

            var catAge = query.Contains(ageKey)
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
