namespace MoviePoint.Models.ViewModels
{
    public class CinemaReqVM
    {
        public List<CinemaReq> CinemaReqs=new List<CinemaReq>();
        public int Pagination { get; set; }
        public double TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}
