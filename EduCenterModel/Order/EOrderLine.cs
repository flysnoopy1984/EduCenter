using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Order
{
    [Table("OrderLine")]
    public class EOrderLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(50)]
        public string OrderId { get; set; }

        [MaxLength(50)]
        public string ItemCode { get; set; }

        [MaxLength(50)]
        public string ItemName { get; set; }

        public double Qty { get; set; }

        public double Price { get; set; }


        public int Ext1 { get; set; }


    }
}
