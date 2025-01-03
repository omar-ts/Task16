namespace MoviePoint.Models.ViewModels
{
    public class CategoryVM
    {
        public List<Category> Categories { get; set; }
        public double TotalPages {  get; set; }
        public int Pagination { get; set; }
        public int StartPage {  get; set; }
        public int EndPage { get; set; }  
    }
}
