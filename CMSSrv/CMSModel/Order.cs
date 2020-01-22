using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class Order
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Contact { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Total { get; set; }
        public int OrderStatus { get; set; }
        public DateTime? PayTime { get; set; }
        public DateTime? CompletePayTime { get; set; }
        public string TrackingNumber { get; set; }
        public string LogisticsCompany { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentGateway { get; set; }
        public string PaymentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public string CreateBy { get; set; }
        public string CreatebyName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateByName { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string RefundId { get; set; }
        public decimal? Refund { get; set; }
        public DateTime? RefundDate { get; set; }
        public string RefundReason { get; set; }
    }
}
