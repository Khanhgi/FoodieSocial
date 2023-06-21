using FoodieSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Dynamic;
using Microsoft.AspNet.Identity;

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


        //// Action để xử lý yêu cầu yêu thích một bài viết
        //[HttpPost]
        //public ActionResult Like(int postId, int profileId)
        //{
        //    int profileid = (int)Session["UserId"];

        //    // Kiểm tra xem bài viết có tồn tại không
        //    var post = fs.User_post.FirstOrDefault(p => p.Id == postId);
        //    if (post == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    // Kiểm tra xem người dùng đã yêu thích bài viết này chưa
        //    var existingLike = fs.Post_like.FirstOrDefault(pl => pl.Postid == postId && pl.Profileid == profileId);
        //    if (existingLike != null)
        //    {
        //        // Người dùng đã yêu thích, không thực hiện gì cả (có thể xóa yêu thích nếu muốn)
        //        return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chủ hoặc nơi khác
        //    }

        //    // Tạo một yêu thích mới
        //    var newLike = new Post_like
        //    {
        //        Postid = postId,
        //        Profileid = profileId,
        //        Likereaction = "like", 
        //        Likecount = 0 // Số lượt thích ban đầu 
        //    };

        //    fs.Post_like.Add(newLike);
        //    fs.SaveChanges();

        //    return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chủ hoặc nơi khác
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