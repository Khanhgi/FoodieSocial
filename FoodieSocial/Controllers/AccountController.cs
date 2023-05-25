using FoodieSocial.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace FoodieSocial.Controllers
{
    public class AccountController : Controller
    {
        FoodieSocialContext fs = new FoodieSocialContext();

        // GET: Account
        //=========================================================== Register ==========================================================//
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection collection, User_profile us)
        {
            var email = collection["Email"];
            var Pass = collection["PassWord"];
            var name = collection["Name"];
            var phone = collection["PhoneNumber"];
            var country = collection["Country"];
            var ns = collection["Date_of_birth"];
            var avatar = collection["Avatar"];
            var MatKhauXacNhan = collection["MatKhauXacNhan"];
            string temp = phone;
            Regex regexMail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match matchMail = regexMail.Match(email);
            Regex regexPhone = new Regex(@"^(84|0[3|5|7|8|9])+([0-9]{8})\b");
            Match matchPhone = regexPhone.Match(phone);
            //Regex regexPass = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*?[0-9])(?=.*?[@!#%])[A-Za-z0-9!#%]{8,32}$");
            //Match matchPassword = regexPass.Match(Pass);
            var checkEmaill = fs.User_profile.FirstOrDefault(x => x.Email == email);
            var checkPassword = fs.User_profile.FirstOrDefault(x => x.PassWord == Pass);
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(Pass) && string.IsNullOrEmpty(name)
                    && string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(country) && string.IsNullOrEmpty(ns))
            {
                ViewData["!empty"] = "Please enter information";
                return this.Register();
            }
            if (checkEmaill != null)
            {
                ViewData["EmailExist"] = "Email already exists !!!";
            }
            if (checkPassword != null)
            {
                ViewData["PassWordExist"] = "Password already exists !!!";
            }
            if (!matchPhone.Success)
            {
                ViewData["NumWrong"] = "Phone number must be in correct format";
                return this.Register();
            }
            if (!matchMail.Success)
            {
                ViewData["EmailWrong"] = "Email must be in correct format";
                return this.Register();
            }
            //if (!matchPassword.Success)
            //{
            //    ViewData["PasswordWrong"] = "Password must be in correct format";
            //    return this.Register();
            //}
            else
            {
                us.Email = email;
                us.Gender = collection["inlineRadioOptions"];
                us.PassWord = Pass;
                us.Name = name;
                us.PhoneNumber = phone;
                us.Country = country;
                us.Date_of_birth = DateTime.Parse(ns);
                us.Avatar = avatar;

                fs.User_profile.Add(us);
                fs.SaveChanges();
                return RedirectToAction("Login", "Account");
            }


        }
        //=========================================================== Register ==========================================================//

        //================================================================== Login ==================================================================//
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection, User_profile user)
        {
            var email = collection["Email"];
            var Pass = collection["PassWord"];

            User_profile us = fs.User_profile.FirstOrDefault(u => u.Email == email && u.PassWord == Pass);
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(Pass))
            {
                ViewData["!empty"] = "Please enter information";
            }
            else if (us == null || us.Email != email || us.PassWord != Pass)
            {
                ViewData["ErrorAccount"] = "Wrong email or password !";
                return this.Login();
            }
            else
            {
                Session["UserId"] = us.Id;
                Session["UserName"] = us.Name;
                return RedirectToAction("Index", "Home");
            }
            return this.Login();
        }

        //===================================================================== Login =====================================================================//


        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Account");
        }

        //============================================================== Detail ==============================================================//
        public ActionResult Detail(int id)
        {
            var details = fs.User_profile.FirstOrDefault(x => x.Id == id);
            if (details != null && string.IsNullOrWhiteSpace(details.Avatar))
            {
                details.Avatar = "~/Content/Images/avadefault.jpg";
            }

            bool canEditProfile = false; // Đặt giá trị mặc định là không thể chỉnh sửa hồ sơ
            if (Session["UserId"] != null && (int)Session["UserId"] == id)
            {
                canEditProfile = true; // Nếu người dùng hiện tại trùng với người dùng trong trang chi tiết, cho phép chỉnh sửa hồ sơ
            }

            ViewBag.CanEditProfile = canEditProfile;
            ViewBag.IsOwner = canEditProfile;
            return View(details);
        }
        //============================================================== Detail ==============================================================//


        //====================================================== Edit ===================================================================//
        public ActionResult EditProfile(int id)
        {
            var edits = fs.User_profile.FirstOrDefault(m => m.Id == id);
            return View(edits);
        }

        [HttpPost]
        public ActionResult EditProfile(int id, FormCollection collection, User_profile us)
        {
            var edits = fs.User_profile.FirstOrDefault(m => m.Id == id);
            var E_email = collection["Email"];
            var E_pass = collection["PassWord"];
            var E_name = collection["Name"];
            var E_phone = collection["PhoneNumber"];
            var E_country = collection["Country"];
            var E_dob = collection["Date_of_birth"];
            var E_ava = collection["Avatar"];
            edits.Id = id;
            if (string.IsNullOrEmpty(E_email) && string.IsNullOrEmpty(E_pass) && string.IsNullOrEmpty(E_name)
                  && string.IsNullOrEmpty(E_phone) && string.IsNullOrEmpty(E_country)
                  && string.IsNullOrEmpty(E_dob))
            {
                ViewData["Error"] = "Please input you information";
            }
            else
            {
                edits.Email = E_email;
                edits.PassWord = E_pass;
                edits.Name = E_name;
                edits.Gender = collection["inlineRadioOptions"];
                edits.PhoneNumber = E_phone;
                edits.Country = E_country;
                edits.Date_of_birth = DateTime.Parse(E_dob);
                edits.Avatar = E_ava;
                UpdateModel(edits);
                // Lưu thông tin người dùng đã cập nhật vào nguồn dữ liệu (ví dụ: cơ sở dữ liệu)
                fs.SaveChanges();
                // Chuyển hướng người dùng đến trang thông tin người dùng sau khi đã cập nhật thành công
                Session["UserId"] = us.Id;
                return RedirectToAction("Detail", "Account", new { id = us.Id });
            }

            // Nếu dữ liệu không hợp lệ, trả về lại view với đối tượng người dùng đã chỉnh sửa để hiển thị thông báo lỗi
            return this.EditProfile(id);
        }
        //====================================================== Edit ===================================================================//

        public ActionResult Search(string keyword)
        {
            // Thực hiện tìm kiếm trong cơ sở dữ liệu dựa trên keyword (tên người dùng)
            List<User_profile> searchResults = fs.User_profile.Where(u => u.Name.Contains(keyword)).ToList();
            ViewBag.HasSearch = !string.IsNullOrEmpty(keyword) && searchResults.Count == 0;

            // Trả về kết quả tìm kiếm đến View để hiển thị
            return View(searchResults);
        }


    }
}