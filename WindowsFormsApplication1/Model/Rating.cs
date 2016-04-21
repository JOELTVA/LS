namespace LS.Model
{

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


    [Table("Rating")]
    public partial class Rating
    {

        //HEJ JOEL
        public Rating()
        {

        }

        public Rating(Decimal grade, Member member)
        {
            this.Grade = grade;
            this.Member = member;
        }

        public int RatingId { get; set; }

        public Decimal Grade { get; set; }

        public virtual string MemberId { get; set; }
        public virtual Member Member { get; set; }
        





    }
}
