//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using FoodieSocial.Models;

//namespace FoodieSocial.Controllers
//{
//    public class FriendController : Controller
//    {
//        private FoodieSocialContext fs = new FoodieSocialContext();

//        // Action để gửi lời mời kết bạn
//        public ActionResult SendFriendRequest(int senderId, int receiverId)
//        {
//            // Kiểm tra xem đã tồn tại mối quan hệ bạn bè giữa hai người chưa
//            var existingFriendship = fs.Friends.FirstOrDefault(f =>
//                (f.Profileid == senderId && f.Profileaccept == receiverId) ||
//                (f.Profileid == receiverId && f.Profileaccept == senderId));

//            if (existingFriendship != null)
//            {
//                // Đã tồn tại mối quan hệ bạn bè
//                // Thực hiện các xử lý hoặc trả về thông báo lỗi
//                // Ví dụ:
//                TempData["ErrorMessage"] = "Đã tồn tại mối quan hệ bạn bè giữa hai người này.";
//                return RedirectToAction("Index", "Home");
//            }

//            // Tạo một đối tượng Friend mới
//            var newFriendship = new Friend
//            {
//                Profileid = senderId,
//                Profileaccept = receiverId,
//                Status = 0 // Chưa kết bạn
//            };

//            // Lưu mối quan hệ bạn bè vào cơ sở dữ liệu
//            fs.Friends.Add(newFriendship);
//            fs.SaveChanges();

//            // Thực hiện các xử lý hoặc trả về thông báo thành công
//            // Ví dụ:
//            TempData["SuccessMessage"] = "Lời mời kết bạn đã được gửi thành công.";
//            return RedirectToAction("Index", "Home");
//        }

//        // Action để chấp nhận lời mời kết bạn
//        public ActionResult AcceptFriendRequest(int requestId)
//        {
//            // Tìm mối quan hệ bạn bè dựa trên requestId
//            var friendship = fs.Friends.Find(requestId);

//            if (friendship == null)
//            {
//                // Mối quan hệ bạn bè không tồn tại
//                // Thực hiện các xử lý hoặc trả về thông báo lỗi
//                // Ví dụ:
//                TempData["ErrorMessage"] = "Mối quan hệ bạn bè không tồn tại.";
//                return RedirectToAction("Index", "Home");
//            }

//            // Chấp nhận lời mời kết bạn bằng cách cập nhật trạng thái
//            friendship.Status = 1; // Đã kết bạn
//            fs.SaveChanges();

//            // Thực hiện các xử lý hoặc trả về thông báo thành công
//            // Ví dụ:
//            TempData["SuccessMessage"] = "Đã chấp nhận lời mời kết bạn.";
//            return RedirectToAction("Index", "Home");
//        }
//    }
//}
