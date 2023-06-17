namespace FoodieSocial.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User_post()
        {
            Post_comment = new HashSet<Post_comment>();
            Post_like = new HashSet<Post_like>();
        }

        public int Id { get; set; }

        public int Profileid { get; set; }

        [StringLength(255)]
        public string Writtentext { get; set; }

        [StringLength(255)]
        public string Mediaimage { get; set; }

        public int? Likecount { get; set; }

        public DateTime Createdate { get; set; }

        public DateTime? Postupdate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post_comment> Post_comment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post_like> Post_like { get; set; }

        public virtual User_profile User_profile { get; set; }
    }
}
