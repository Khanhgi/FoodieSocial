using FoodieSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodieSocial.Controllers
{
    public class ChatController : Controller
    {
        FoodieSocialContext fs = new FoodieSocialContext();


        // GET: Chat
        [HttpGet]
        public ActionResult Chat()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Chat(FormCollection collection)
        {
            return View();
        }
    }
}