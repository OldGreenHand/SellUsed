using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SellUsed.Models
{
    public class ConversationDetail
    {
        // Primary key 
        [HiddenInput(DisplayValue = false)]
        [Key]
        public Guid ConversationId { get; set; }

        //FK
        [MaxLength(128)]
        public string FromUserId { get; set; }

        [MaxLength(128)]
        public string ToUserId { get; set; }

        [MaxLength(128)]
        public string ToUserName { get; set; }

        public int UnreadMessageNo { get; set; }

        [Required]
        public DateTime Updatetime { get; set; }

        // Navigation property 
        public virtual ICollection<MessageDetail> Messages { get; set; }

    }
}