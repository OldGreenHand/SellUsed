using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SellUsed.Models
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductId { get; set; }

        [HiddenInput(DisplayValue = false)]
        [MaxLength(128)]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Please enter a title")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "Title")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please choose a category")]
        public Category Category { get; set; }

        [Required]
        public DateTime Createtime { get; set; }

        [MaxLength(5)]
        public string StreetNo { get; set; }

        public string StreetRoute { get; set; }

        [Required(ErrorMessage = "Please enter a suburb")]
        [MaxLength(26)]
        public string Suburb { get; set; }

        [Required(ErrorMessage = "Please enter state")]
        [MaxLength(3)]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter a postcode")]
        [MaxLength(4)]
        public string Postcode { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Boolean Status { get; set; }
    }
}
