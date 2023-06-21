using FoodieSocial.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Data.Entity.Validation;

namespace FoodieSocial.Controllers
{
    public class PostController : Controller
    {
        FoodieSocialContext fs = new FoodieSocialContext();

        public ActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase fileImage, FormCollection formCollection)
        {
            if (fileImage != null && fileImage.ContentLength > 0)
            {
                string fileName = Path.GetFileName(fileImage.FileName);
                string path = Path.Combine(Server.MapPath("/Images/"), fileName);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    fileImage.SaveAs(path);
                }
                else
                {
                    fileImage.SaveAs(path);
                }

                // Lấy giá trị từ form
                string writtenText = formCollection["Writtentext"];
                DateTime createDate = DateTime.Now;

                // Lấy Profileid từ Session
                int profileId = (int)Session["UserId"];

                // Tạo đối tượng User_post
                User_post post = new User_post()
                {
                    Createdate = createDate,
                    Writtentext = writtenText,
                    Mediaimage = fileName,
                    Profileid = profileId,
                };

                // Thêm đối tượng User_post vào cơ sở dữ liệu
                fs.User_post.Add(post);
                fs.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
