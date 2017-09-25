using System.Collections.Generic;
using SellUsed.Models;

namespace SellUsed.ViewModels
{
    public class AdsListViewModel
    {
        public string ViewName { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public Pager Pager { get; set; }
    }
    
}