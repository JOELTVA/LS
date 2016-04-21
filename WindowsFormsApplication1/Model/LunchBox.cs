namespace LS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LunchBox")]
    public partial class LunchBox
    {
        public LunchBox()
        {

        }
        public LunchBox(string name, string content, string foodCategory, int quantity, Member m)
        {
            this.Name = name;
            this.Content = content;
            this.FoodCategory = foodCategory;
            this.Quantity = quantity;
            this.Member = m;
        }
        public int LunchBoxId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Content { get; set; }

        [StringLength(255)]
        public string FoodCategory { get; set; }

        public int Quantity { get; set; }

        [System.ComponentModel.Browsable(false)]
        [StringLength(255)]
        public string MemberId { get; set; }

        [System.ComponentModel.Browsable(false)]
        public virtual Member Member { get; set; }
    }
}
