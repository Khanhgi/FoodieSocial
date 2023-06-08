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
            var matkhau = collection["PassWord"];
            var email = collection["Email"];

            User_profile us = fs.User_profile.FirstOrDefault(x => x.Email== email && x.PassWord == matkhau);
            if (us != null)
            {
                ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                Session["UserName"] = us.Name;
                ViewBag.UserName = us.Name;
            }
            return View();
        }
    }
}