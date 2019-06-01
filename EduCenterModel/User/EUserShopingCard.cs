using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel.User
{
    //[Table("UserShopingCard")]
    public class EUserShopingCard: ECBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public EUserShopingCard()
        {
            CreatedDate = DateTime.Now;
        }
       
        [MaxLength(32)]
        public string OpenId { get; set; }

        [MaxLength(50)]
        public string KeyNo { get; set; }

        public DateTime CreatedDate { get; set; }

        public ShopCartType ShopCartType { get; set; }




    }
}
