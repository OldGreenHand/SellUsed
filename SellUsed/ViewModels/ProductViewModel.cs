using SellUsed.Models;

namespace SellUsed.ViewModels
{
    public class ProductViewModel
    {
        public Product Product = new Product();
        public int VisitTime = 0;
        public bool ifFavorite = false;
    }
}