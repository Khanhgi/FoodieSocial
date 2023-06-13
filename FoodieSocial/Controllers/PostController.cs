using FoodieSocial.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Linq;
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

                // Lấy thông tin người dùng từ bảng User_profile dựa trên Profileid
                User_profile userProfile = fs.User_profile.FirstOrDefault(u => u.Id == profileId);


                // Tạo đối tượng User_post
                User_post post = new User_post()
                {
                    Createdate = createDate,
                    Writtentext = writtenText,
                    Mediaimage = fileName,
                    Profileid = profileId,
                    User_profile = userProfile
                };

                // Thêm đối tượng User_post vào cơ sở dữ liệu
                fs.User_post.Add(post);
                fs.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }


        //// GET: Post/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Post/Create
        //[HttpPost]
        //public ActionResult Create(User_post post, HttpPostedFileBase imageFile)
        //{
        //    try
        //    {
        //        // Lấy ProfileId từ session
        //        int profileId = (int)Session["UserId"];

        //        if (ModelState.IsValid)
        //        {
        //            // Set thời gian đăng
        //            post.Createdate = DateTime.Now;

        //            // Check if an image file was uploaded
        //            if (imageFile != null && imageFile.ContentLength > 0)
        //            {
        //                // Save the image file to a local directory
        //                string imagePath = SaveImage(imageFile);

        //                // Set the Mediaimage property to the local image path
        //                post.Mediaimage = imagePath;
        //            }

        //            post.User_profile = new User_profile { Id = profileId };

        //            // Add the post to the database
        //            fs.User_post.Add(post);
        //            fs.SaveChanges();

        //            return RedirectToAction("Index", "Home"); // Redirect to home page or any other desired page
        //        }
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        // Xử lý lỗi kiểm tra dữ liệu không thành công
        //        foreach (var validationErrors in ex.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
        //            }
        //        }
        //        // Trả về View với thông báo lỗi hoặc xử lý khác tùy ý
        //        return View(post);
        //    }
        //    return View(post);
        //}

        //private string SaveImage(HttpPostedFileBase imageFile)
        //{
        //    // Tạo tên tệp tin duy nhất
        //    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

        //    // Đường dẫn đến thư mục "Images" trong dự án
        //    string folderPath = Path.Combine(Server.MapPath("/Images/"));

        //    // Đường dẫn đầy đủ của tệp tin ảnh
        //    string imagePath = Path.Combine(folderPath, fileName);

        //    // Lưu trữ tệp tin ảnh vào thư mục
        //    imageFile.SaveAs(imagePath);

        //    return imagePath;
        //}


    }
}
