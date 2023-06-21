using FoodieSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Dynamic;

namespace FoodieSocial.Controllers
{
    public class HomeController : Controller
    {
        FoodieSocialContext fs = new FoodieSocialContext();

        public ActionResult Index()
        {
            List<PostViewModel> postViewModels = new List<PostViewModel>();

            using (var fs = new FoodieSocialContext()) 
            {
                var userPosts = fs.User_post.ToList();
                var userProfileIds = userPosts.Select(post => post.Profileid).ToList();
                var userProfiles = fs.User_profile.Where(profile => userProfileIds.Contains(profile.Id)).ToList();

                foreach (var post in userPosts)
                {
                    var userProfile = userProfiles.FirstOrDefault(profile => profile.Id == post.Profileid);

                    var postViewModel = new PostViewModel
                    {
                        UserPost = post,
                        UserProfile = userProfile
                    };
                    postViewModels.Add(postViewModel);
                }
                postViewModels.Reverse();
            }

            return View(postViewModels);
        }

        //[HttpPost]
        //public ActionResult Like(int postId)
        //{
        //    // Tìm bài đăng theo postId trong cơ sở dữ liệu và tăng số lượt like lên 1
        //    using (var fs = new FoodieSocialContext())
        //    {
        //        var post = fs.User_post.FirstOrDefault(p => p.Id == postId);
        //        if (post != null)
        //        {
        //            post.Likecount += 1;
        //            fs.SaveChanges();
        //            return Json(new { success = true });
        //        }
        //    }
        //    return Json(new { success = false });
        //}


        public JsonResult DeletePost(int postId)
        {
            var userPost = fs.User_post.Find(postId);
            if (userPost != null)
            {
                fs.User_post.Remove(userPost);
                fs.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}