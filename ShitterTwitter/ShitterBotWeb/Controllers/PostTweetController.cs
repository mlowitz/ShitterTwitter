using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ShitterTwitter.Common.DAL;
using ShitterTwitter.Common.Objects;
using ShitterTwitter.Common.MessagePublisher;
namespace ShitterBotWeb.Controllers
{
    public class PostTweetController : Controller
    {
        // GET: post
        public string Index()
        {
            return "This is my <b>default</b> action...";
        }

        public string Post()
        {
                DatabaseManeger db = new DatabaseManeger();
            IShitterTwitterMessage message = db.GetMessageToTweet();

            TwitterManeger tm = new TwitterManeger();
            tm.sendTweet(message.Message);
         

            return message.Message;
        }
    }
}