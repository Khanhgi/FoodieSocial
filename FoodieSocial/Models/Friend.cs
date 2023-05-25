namespace FoodieSocial.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Friend
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Profileid { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Profileaccept { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string Profilerequest { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string Profileblock { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Status { get; set; }

        public virtual User_profile User_profile { get; set; }
    }
}
