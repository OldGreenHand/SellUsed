using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SellUsed.Models
{
    public class ProductImages
    {
        [HiddenInput(DisplayValue = false)]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ImageId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid ProductId { get; set; }
        
        public byte[] File { get; set; }
    }
}
