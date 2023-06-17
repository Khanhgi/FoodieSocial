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

                    // Tính toán số lượt yêu thích (like count) cho mỗi bài post
                    var likeCount = fs.Post_like.Count(l => l.Postid == post.Id);
                    postViewModel.Likecount = likeCount;
                }
                postViewModels.Reverse();
            }

            return View(postViewModels);
        }

        //public ActionResult Index()
        //{
        //    List<User_post> userPosts;
        //    using (var fs = new FoodieSocialContext())
        //    {
        //        userPosts = fs.User_post.ToList();
        //    }

        //    return View(userPosts);
        //}



        //public List<User_profile> GetUser_Profiles()
        //{
        //    List<User_profile> profiles = fs.User_profile.ToList();
        //    return profiles;
        //}

        //public List<User_post> GetUser_Posts()
        //{
        //    List<User_post> posts = fs.User_post.ToList();
        //    return posts;
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        fs.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

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