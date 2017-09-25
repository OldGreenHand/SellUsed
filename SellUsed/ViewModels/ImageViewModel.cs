using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SellUsed.ViewModels
{
    public class ImageViewModel
    {
        [Required]
        [DisplayName("Select Files to Upload (10 Pics Max)")]
        public IEnumerable<HttpPostedFileBase> File { get; set; }
        public int ProductId { get; set; }
    }
}