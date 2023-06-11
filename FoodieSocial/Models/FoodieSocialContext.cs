using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FoodieSocial.Models
{
    public partial class FoodieSocialContext : DbContext
    {
        public FoodieSocialContext()
            : base("name=FoodieSocialContext")
        {
        }

        public virtual DbSet<Post_comment> Post_comment { get; set; }
        public virtual DbSet<Post_like> Post_like { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User_post> User_post { get; set; }
        public virtual DbSet<User_profile> User_profile { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post_comment>()
                .Property(e => e.Commentvideo)
                .IsUnicode(false);

            modelBuilder.Entity<Post_comment>()
                .Property(e => e.Commentimage)
                .IsUnicode(false);

            modelBuilder.Entity<Post_like>()
                .Property(e => e.Likereaction)
                .IsUnicode(false);

            modelBuilder.Entity<User_post>()
                .Property(e => e.Mediaimage)
                .IsUnicode(false);

            modelBuilder.Entity<User_post>()
                .HasMany(e => e.Post_comment)
                .WithRequired(e => e.User_post)
                .HasForeignKey(e => e.Postid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User_post>()
                .HasMany(e => e.Post_like)
                .WithRequired(e => e.User_post)
                .HasForeignKey(e => e.Postid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User_profile>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User_profile>()
                .Property(e => e.PassWord)
                .IsUnicode(false);

            modelBuilder.Entity<User_profile>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<User_profile>()
                .HasMany(e => e.Post_comment)
                .WithRequired(e => e.User_profile)
                .HasForeignKey(e => e.Profileid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User_profile>()
                .HasMany(e => e.Post_like)
                .WithRequired(e => e.User_profile)
                .HasForeignKey(e => e.Profileid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User_profile>()
                .HasMany(e => e.User_post)
                .WithRequired(e => e.User_profile)
                .HasForeignKey(e => e.Profileid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User_profile>()
                .HasMany(e => e.Friends)
                .WithRequired(e => e.User_profile)
                .HasForeignKey(e => e.Profileid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Friend>()
                .Property(e => e.Profileblock)
                .IsFixedLength();
        }
    }
}
