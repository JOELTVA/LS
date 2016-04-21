using LS.Model;

namespace LS.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LunchSwitchEDM : DbContext
    {

        public LunchSwitchEDM()
            : base("name=LunchSwitchEDM")

        {
        }

        public virtual DbSet<LunchBox> LunchBoxes { get; set; }
        public virtual DbSet<MeetUp> MeetUps { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MeetUp_Member> MeetUp_Members { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LunchBox>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<LunchBox>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<LunchBox>()
                .Property(e => e.FoodCategory)
                .IsUnicode(false);

            modelBuilder.Entity<LunchBox>()
                .Property(e => e.MemberId)
                .IsUnicode(false);

            modelBuilder.Entity<MeetUp>()
                .Property(e => e.Information)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.MemberId)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.MobileNr)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<MeetUp_Member>()
               .HasKey(e => new { e.MeetUpId, e.MemberId });

            modelBuilder.Entity<Rating>()
             .Property(e => e.MemberId)
             .IsUnicode(false);

        }
    }
}
