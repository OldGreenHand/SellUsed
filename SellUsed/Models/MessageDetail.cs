using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SellUsed.Models
{
    public class MessageDetail
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public Guid MessageId { get; set; }

        [MaxLength(128)]
        public string FromUserId { get; set; }
        public string FromUserName { get; set; }
        [MaxLength(128)]
        public string ToUserId { get; set; }
        public string ToUserName { get; set; }
        public string Message { get; set; }

        [Required]
        public DateTime Createtime { get; set; }

        // Foreign key 
        public Guid ConversationId1 { get; set; }
        public Guid ConversationId2 { get; set; }
        // Navigation properties 
        public virtual ConversationDetail Conversation { get; set; }
    }
}