using EduCenterCore.Common.Helper;
using EduCenterCore.EduFramework;
using EduCenterModel.BaseEnum;
using EduCenterModel.Course;
using EduCenterModel.Order;
using EduCenterSrv.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EduCenterModel.User.Result;
using EduCenterSrv.Common;

namespace EduCenterSrv
{
    public class OrderSrv: BaseSrvMasterData<EOrder>
    {
        public OrderSrv(EduDbContext dbContext) : base(dbContext)
        {
            
        }

        #region SQL
        public static string sql_UpdateOrderStatus(string orderId,OrderStatus orderStatus)
        {
            string sql = $"update Order set OrderStatus = {(int)orderStatus} where OrderId='{orderId}'";
            return sql;
        }
        #endregion

        public List<RUserCharge> QueryChargeOrderList(string openId,int pageIndex,int pageSize)
        {
            var sql = from l in _dbContext.DBOrderLine
                      join o in _dbContext.DBOrder on l.OrderId equals o.OrderId
                      where o.CustOpenId == openId && o.OrderStatus == OrderStatus.PaySuccess
                      orderby o.CreateDateTime descending
                      select new RUserCharge
                      {
                          Amount = l.Price.ToString(),
                          CreateDateTime = o.CreateDateTime.ToString("yyyy-MM-dd"),
                          ItemName = $"{BaseEnumSrv.GetOrderTypeName(o.OrderType)}[{l.Qty}]课时",
                          Qty = l.Qty.ToString(),
                          UserOpenId = openId,
                      };
            return sql.Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();

        }
    

      
    }
}
