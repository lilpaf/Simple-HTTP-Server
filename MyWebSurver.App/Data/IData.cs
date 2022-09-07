namespace MyWebSurver.App.Data
{
    using MyWebSurver.App.Data.Models;

    public interface IData
    {
        IEnumerable<Cat> Cats { get; }
    }
}
