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

      
   
    }
}
