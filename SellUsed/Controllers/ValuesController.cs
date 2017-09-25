using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SellUsed.Hubs;
using SellUsed.Models;
using SellUsed.Utility;

namespace SellUsed.Controllers
{
    public class ValuesController : ApiController
    {

        ConversationRepository objRepo = new ConversationRepository();


        // GET api/values
        public IEnumerable<ConversationDetail> Get()
        {

            return objRepo.GetData();
        }
    }
}
