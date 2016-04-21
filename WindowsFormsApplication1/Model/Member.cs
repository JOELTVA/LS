namespace LS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Member")]
    public partial class Member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member(string memberId, string city, string description, string email,
            string fullname, string mobileNr, string password)
        {
            this.MemberId = memberId;
            this.City = city;
            this.Email = email;
            this.Description = description;
            this.FullName = fullname;
            this.MobileNr = mobileNr;
            this.Password = password;
            LunchBox = new HashSet<LunchBox>();
        }
        public Member()
        {
            LunchBox = new HashSet<LunchBox>();
        }
 
        [StringLength(255)]
        public string MemberId { get; set; }

        [StringLength(255)]
        public string City { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(255)]
        public string MobileNr { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LunchBox> LunchBox { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rating> Rating { get; set; }
    }
}
