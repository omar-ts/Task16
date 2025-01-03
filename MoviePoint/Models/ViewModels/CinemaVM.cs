namespace MoviePoint.Models.ViewModels
{
    public class CinemaVM
    {
        public List<Cinema> Cinemas { get; set; } = new List<Cinema>();
        public int Pagination { get; set; }
        public double TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}
