using Microsoft.AspNet.Identity;
using SellUsed.DataContexts;
using SellUsed.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using SellUsed.Hubs;
using SellUsed.Utility;
using SellUsed.ViewModels;

namespace SellUsed.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private ConversationDb _conDb = new ConversationDb();

        //UserManager
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Chat
        
        public ActionResult Index()
        {
            string myUserId = User.Identity.GetUserId();
            //read all conversation ordered by descending updatetime
            //read history of first conversation
            IEnumerable<ConversationDetail> conversations = _conDb.Conversations.Where(x => x.FromUserId == myUserId).OrderByDescending(x => x.Updatetime);

            return View(conversations);
        }
        
        public async Task<ActionResult> Create(string id)
        {
            if (id == User.Identity.GetUserId())
            {
                return Content("You can't chat with yourself!");
            }
            string myUserId = User.Identity.GetUserId();
            string myUserName = User.Identity.Name;
            
            if (!_conDb.Conversations.Where(x => x.FromUserId == id).Any(x => x.ToUserId == myUserId))
            {
                // create new conversation
                var newConversationDetail = new ConversationDetail()
                {
                    ConversationId = Guid.NewGuid(),
                    FromUserId = id,
                    ToUserId = myUserId,
                    ToUserName = myUserName,
                    UnreadMessageNo = 0,
                    Updatetime = DateTime.Now
                };
                _conDb.Conversations.Add(newConversationDetail);
                await _conDb.SaveChangesAsync();
            }
            if (!_conDb.Conversations.Where(x => x.FromUserId == myUserId).Any(x => x.ToUserId == id))
            {
                // create new conversation
                var newConversationDetail = new ConversationDetail()
                {
                    ConversationId = Guid.NewGuid(),
                    FromUserId = myUserId,
                    ToUserId = id,
                    ToUserName = UserManager.FindById(id).UserName,
                    UnreadMessageNo = 0,
                    Updatetime = DateTime.Now
                };
                _conDb.Conversations.Add(newConversationDetail);
                await _conDb.SaveChangesAsync();
            }

            //if target user is online, creating real time connection
            if (OnlineUsersHelper.CheckIfUserOnline(UserManager.FindById(id).UserName) && 
                OnlineUsersHelper.CheckIfUserOnline(UserManager.FindById(myUserId).UserName))
            {
                
            }
            
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(string conversationId)
        {
            string myUserId = User.Identity.GetUserId();
            string myUserName = User.Identity.Name;

            ConversationDetail conversation = await _conDb.Conversations.FindAsync(new Guid(conversationId));
            if (conversation != null) _conDb.Conversations.Remove(conversation);
            await _conDb.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public ActionResult LoadChatArea(Guid conversationId)
        {
            int unreadMessageNo = _conDb.Conversations.Find(conversationId).UnreadMessageNo;
            IEnumerable<MessageDetail> messages =  _conDb.Messages.Where(x => x.ConversationId1 == conversationId || x.ConversationId2 == conversationId).OrderBy(x => x.Createtime);
            ChatViewModel model = new ChatViewModel
            {
                Messages = messages,
                UnreadMessagesNo = unreadMessageNo
            };
            _conDb.Conversations.Find(conversationId).UnreadMessageNo = 0;
            _conDb.SaveChanges();
            return PartialView(model);
        }

        public void SaveMessage(MessageDetail messageDetail)
        {
            string toName = UserManager.FindById(messageDetail.ToUserId).UserName;
            messageDetail.Createtime = DateTime.Now;
            messageDetail.FromUserId = User.Identity.GetUserId();
            messageDetail.FromUserName = User.Identity.Name;
            messageDetail.ToUserName = toName;
            messageDetail.MessageId = Guid.NewGuid();
            messageDetail.ConversationId2 = Guid.Empty;
            if (_conDb.Conversations.Any(x => x.ToUserId == messageDetail.FromUserId &&
                                              x.FromUserId == messageDetail.ToUserId))
            {
                messageDetail.ConversationId2 = _conDb.Conversations.Where(x => x.ToUserId == messageDetail.FromUserId &&
                                                                             x.FromUserId == messageDetail.ToUserId)
                                                                    .FirstOrDefault()
                                                                    .ConversationId;
                _conDb.Conversations.Find(messageDetail.ConversationId2).UnreadMessageNo++;
            }
            _conDb.Messages.Add(messageDetail);
            _conDb.Conversations.Find(messageDetail.ConversationId1).Updatetime = messageDetail.Createtime;
            _conDb.Conversations.Find(messageDetail.ConversationId2).Updatetime = messageDetail.Createtime;
            _conDb.SaveChanges();
        }
    }
}