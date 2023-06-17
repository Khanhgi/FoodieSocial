using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodieSocial.Models
{
    public class PostViewModel
    {
        public User_profile UserProfile { get; set; }
        public User_post UserPost { get; set; }
    }
}