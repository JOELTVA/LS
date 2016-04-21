namespace LS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MeetUp")]
    public partial class MeetUp
    {
        public MeetUp()
        {

        }
        public MeetUp(string information)
        {
            this.Information = information;
        }
        public int MeetUpId { get; set; }

        [StringLength(255)]
        public string Information { get; set; }
    }
}
