using FoodieSocial.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace FoodieSocial.Controllers
{
    public class PostController : Controller
    {
        // GET: CreatePost
        public ActionResult CreatePost()
        {
            return View();
        }

    //    [HttpPost]
    //    public ActionResult CreatePost(User_post userPost, HttpPostedFileBase mediaImageFile)
    //    {
    //            // Xử lý tải lên file hình ảnh và lưu vào server
    //            if (mediaImageFile != null || mediaImageFile.ContentLength > 0)
    //            {
    //                string mediaImageFileName = Path.GetFileName(mediaImageFile.FileName);
    //                var imageFolderPath = Server.MapPath("~/MediaFiles/Images/");
    //                var mediaImagePath = Path.Combine(imageFolderPath, mediaImageFileName);
    //                mediaImageFile.SaveAs(mediaImagePath);
    //                userPost.Mediaimage = Path.Combine("Images/", mediaImageFileName);
    //            }
    //        return RedirectToAction("Index", "Home");
    //        // Lưu bài đăng vào cơ sở dữ liệu
    //        userPost.Createdate = DateTime.UtcNow; // Sử dụng DateTime.UtcNow để lưu thời gian tại múi giờ UTC

    //        int profileId;
    //        if (int.TryParse(GetUserId(), out profileId))
    //        {
    //            userPost.Profileid = profileId;

    //            using (var context = new FoodieSocialContext())
    //            {
    //                context.User_post.Add(userPost);
    //                context.SaveChanges();
    //            }

    //            // Điều hướng người dùng đến trang hiển thị bài đăng 
    //            return RedirectToAction("Index", "Home");
    //        }
    //        else
    //        {
    //            // Xử lý trường hợp chuyển đổi không thành công
    //            // ...

    //            // Nếu dữ liệu không hợp lệ, trả về lại View với model và thông báo lỗi
    //            return View(userPost);
    //        }
    //    }

    //        // Nếu dữ liệu không hợp lệ, trả về lại View với model và thông báo lỗi
    //        return View(userPost);
    //}
        [HttpPost]
        public ActionResult CreatePost(User_post userPost, HttpPostedFileBase mediaImageFile)
        {
            // Xử lý tải lên file hình ảnh và lưu vào server
            if (mediaImageFile != null && mediaImageFile.ContentLength > 0)
            {
                string mediaImageFileName = Path.GetFileName(mediaImageFile.FileName);
                string path = Path.Combine(Server.MapPath("~/Images"), mediaImageFileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    mediaImageFile.SaveAs(path);
                }
                else
                {
                    mediaImageFile.SaveAs(path);
                }
            }
            return RedirectToAction("Index", "Home");
        }

            private string GetUserId()
        {
            return User.Identity.GetUserId();
        }
    }
}
