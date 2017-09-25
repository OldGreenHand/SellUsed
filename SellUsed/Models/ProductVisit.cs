using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SellUsed.Models
{
    public class ProductVisit
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public Guid ProductId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int VisitTimes { get; set; }
    }
}
