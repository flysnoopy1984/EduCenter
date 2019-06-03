using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.Order
{
    [Table("Order")]
    public class EOrder
    {
        [Key]
        [MaxLength(50)]
        public string  OrderId { get; set; }

        /// <summary>
        /// 关联订单，购买课时，课时套餐ID被记录
        /// </summary>
        [MaxLength(50)]
        public string RefId { get; set; }

        public double PayAmount { get; set; }

        [MaxLength(32)]
        public string CustOpenId { get; set; }

        public OrderType OrderType { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime CreateDateTime { get; set; }


    }
}
