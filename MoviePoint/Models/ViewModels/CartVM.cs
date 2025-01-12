namespace MoviePoint.Models.ViewModels
{
    public class CartVM
    {
        public List<Cart> Carts { get; set; }=new List<Cart>();
        public double TotalSum { get; set; }
    }
}
