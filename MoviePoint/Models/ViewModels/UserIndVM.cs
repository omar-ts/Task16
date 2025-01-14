namespace MoviePoint.Models.ViewModels
{
    public class UserIndVM
    {
        public List<ApplicationUser> users { get; set; } = new List<ApplicationUser>();
        public int Pagination { get; set; }
        public double TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}
