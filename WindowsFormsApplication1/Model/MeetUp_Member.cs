namespace LS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MeetUp_Member
    {
        public MeetUp_Member(Member member, MeetUp meetUp)
        {
            this.Member = member;
            this.MeetUp = meetUp;
        }
        public MeetUp_Member()
        {

        }

        public virtual string MemberId { get; set; }
        public virtual int MeetUpId { get; set; }
        public virtual Member Member { get; set; }
        public virtual MeetUp MeetUp { get; set; }
        
    }
}
