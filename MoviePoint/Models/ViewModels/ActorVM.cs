namespace MoviePoint.Models.ViewModels
{
    public class ActorVM
    {
        public List<Actor> Actors { get; set; } = new List<Actor>();
        public int Pagination { get; set; }
        public double TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}
