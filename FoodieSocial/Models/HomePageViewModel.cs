using FoodieSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodieSocial.Models
{
    public class HomePageViewModel
    {
        public User_post UserPost { get; set; }
        public User_profile UserProfile { get; set; }
    }   
}