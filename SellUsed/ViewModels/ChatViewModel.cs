using System.Collections.Generic;
using SellUsed.Models;

namespace SellUsed.ViewModels
{
    public class ChatViewModel
    {
        public IEnumerable<MessageDetail> Messages { get; set; }

        public int UnreadMessagesNo { get; set; }
    }
}