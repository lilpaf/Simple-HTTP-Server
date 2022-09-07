namespace MyWebSurver.App.Data
{
    using MyWebSurver.App.Data.Models;

    public class MyDbContext : IData
    {
        public MyDbContext()
            => this.Cats = new List<Cat>
            {
                new Cat {Id = 1, Name = "Ivan", Age = 5},
                new Cat {Id = 2, Name = "Gosho", Age = 6}
            };

        public IEnumerable<Cat> Cats { get; }
    }
}
