using System;
using System.Data.Entity;
using System.Linq;
using SellUsed.Hubs;
using SellUsed.Models;

namespace SellUsed.DataContexts
{
    public class ConversationDb : DbContext
    {
        public ConversationDb()
            : base("DefaultConnection")
        {
        }
        
        public virtual DbSet<ConversationDetail> Conversations { get; set; }
        public virtual DbSet<MessageDetail> Messages { get; set; }
    }
    
}