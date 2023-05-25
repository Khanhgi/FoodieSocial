using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodieSocial.Models;

namespace FoodieSocial.Controllers
{
    public class FriendController : Controller
    {
        private FoodieSocialContext fs = new FoodieSocialContext();

        // GET: Friend
        public ActionResult Index()
        {
            return View();
        }

        // POST: Friend/AddFriend
        [HttpPost]
        public ActionResult AddFriend(int receiverId)
        {
            int currentUserId = GetCurrentUserId(); // Get the ID of the current user

            // Check if a friend request already exists between the two users
            bool isFriendRequestExists = fs.Friends.Any(f => f.Profileid == currentUserId && f.Profileaccept == receiverId);

            if (!isFriendRequestExists)
            {
                // Create a new Friend object with a pending status (Status = 0)
                Friend friend = new Friend
                {
                    Profileid = currentUserId,
                    Profileaccept = receiverId,
                    Profilerequest = currentUserId.ToString(),
                    Profileblock = "", // Change if there is a blocking feature
                    Status = 0 // Friend request status (0 - Pending, 1 - Accepted)
                };

                fs.Friends.Add(friend);
                fs.SaveChanges();

                var result = new { success = true, message = "Friend request sent successfully" };
                return Json(result);
            }
            else
            {
                var result = new { success = false, message = "Friend request already sent" };
                return Json(result);
            }
        }

        // POST: Friend/AcceptFriendRequest
        [HttpPost]
        public ActionResult AcceptFriendRequest(int friendRequestId)
        {
            int currentUserId = GetCurrentUserId(); // Get the ID of the current user

            var friendRequest = fs.Friends.FirstOrDefault(f => f.Profilerequest == currentUserId.ToString() && f.Profileaccept == friendRequestId && f.Status == 0);

            if (friendRequest != null)
            {
                // Accept the friend request by updating the status to 1
                friendRequest.Status = 1;
                fs.SaveChanges();

                var result = new { success = true, message = "Friend request accepted" };
                return Json(result);
            }
            else
            {
                var result = new { success = false, message = "Friend request not found" };
                return Json(result);
            }
        }

        // Other methods like viewing friend requests, checking friend status, etc.

        private int GetCurrentUserId()
        {
            if (Session["UserId"] != null && int.TryParse(Session["UserId"].ToString(), out int userId))
            {
                return userId;
            }
            else
            {
                // Handle the case when no user is logged in
                throw new Exception("User not logged in");
            }
        }

        [HttpPost]
        public ActionResult CancelFriendRequest(int receiverId)
        {
            try
            {
                // Lấy người dùng hiện tại từ phiên làm việc hoặc thông qua hệ thống xác thực
                int currentUserId = GetCurrentUserId(); // Hàm này cần được cài đặt để lấy ID của người dùng hiện tại

                // Tìm yêu cầu kết bạn trong danh sách bạn bè chưa chấp nhận của người dùng hiện tại
                Friend friendRequest = fs.Friends.FirstOrDefault(f => f.Profileid == currentUserId && f.Profilerequest == receiverId.ToString() && f.Status == 0);
                if (friendRequest != null)
                {
                    // Xóa yêu cầu kết bạn
                    fs.Friends.Remove(friendRequest);
                    fs.SaveChanges();

                    // Trả về kết quả thành công và thông báo
                    return Json(new { success = true, message = "Friend request canceled successfully." });
                }

                // Trả về kết quả không thành công và thông báo
                return Json(new { success = false, message = "Friend request not found." });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }



    }
}
