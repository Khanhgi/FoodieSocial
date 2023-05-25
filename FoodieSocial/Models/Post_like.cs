namespace FoodieSocial.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Post_like
    {
        public int Id { get; set; }

        public int Postid { get; set; }

        public int Profileid { get; set; }

        [StringLength(255)]
        public string Likereaction { get; set; }

        public int? Likecount { get; set; }

        public virtual User_post User_post { get; set; }

        public virtual User_profile User_profile { get; set; }
    }
}
