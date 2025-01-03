namespace MoviePoint.Models.ViewModels
{
    public class ActorMovieVM
    {
        public List<ActorMovie> ActorMovies { get; set; } = new List<ActorMovie>();
        public int Pagination { get; set; }
        public double TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}
