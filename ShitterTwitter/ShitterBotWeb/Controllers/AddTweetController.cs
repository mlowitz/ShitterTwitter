﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShitterBotWeb;
using ShitterTwitter.Common.DAL;
using ShitterTwitter.Common.Objects;
using ShitterTwitter.DAL;
using System.Threading.Tasks;

namespace ShitterBotWeb.Controllers
{
    public class AddTweetController : Controller
    {
        // GET: AddTweet
        //public string Index()
        //{
        //     return "This is my <b>default</b> action...";
        //}

        public async Task<string> Add(string tweet, int key)
        {

            if (key == 1234)
            {


                DatabaseManeger db = new DatabaseManeger();
                await db.AddMessage(new ShitterTwitterMessage() {Message = tweet});
                //TODO need to add code to confirm uplaod 
            }
            return tweet;
        }

   
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> AddFromUI(string tweet)
        {
            DatabaseManeger db = new DatabaseManeger();
            await db.AddMessage(new ShitterTwitterMessage() {Message = tweet});
            //TODO need to add code to confirm uplaod 

            return tweet;
        }


    }
}