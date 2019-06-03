using EduCenterCore.Common.Helper;
using EduCenterCore.EduFramework;
using EduCenterModel.BaseEnum;
using EduCenterModel.Course;
using EduCenterModel.Order;
using EduCenterModel.User;
using EduCenterSrv.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduCenterSrv
{
    public class BusinessSrv:BaseSrv
    {
        public BusinessSrv(EduDbContext dbContext) : base(dbContext)
        {

        }

        //删除某一类型的用户课程（标准，夏季，冬季）
        public static string sql_DeleteAllUserCourseByType(string userOpenId,CoursePriceType coursePriceType)
        {
            string sql = $"delete from UserCourse where UserOpenId='{userOpenId}' and CoursePriceType={(int)coursePriceType}";
            return sql;
        }

        //更新这门课的总申请数
        public static string sql_UpdateCourseScheduleApplyNum(string lessonCode)
        {
            string sql = $"update CourseSchedule set ApplyNum+= 1 where LessonCode = '{lessonCode}'";
            return sql;
        }

        #region 创建首次购买课时订单

        public EOrder PayCourseOrder(string userOpenId, ECoursePrice coursePrice,List<EUserCourse> userCourses)
        {
            try
            {
                BeginTrans();

                //删除某一类型的用户课程（标准，夏季，冬季）
                _dbContext.Database.ExecuteSqlCommand(sql_DeleteAllUserCourseByType(userOpenId, coursePrice.CoursePriceType));
                //新建用户所有课程
                
                _dbContext.DBUserCoures.AddRange(userCourses);

                var Order =  CreateBuyCourseOrder(userOpenId, coursePrice);

                _dbContext.SaveChanges();

                CommitTrans();

                return Order;

            }
            catch (Exception ex)
            {
                RollBackTrans();
                NLogHelper.ErrorTxt($"[PayCourseFirst]{ex.Message}");
                throw ex;
            }
        }


        private EOrder CreateBuyCourseOrder(string userOpenId, ECoursePrice coursePrice)
        {
            EOrder order = null;
            try
            {
                order = new EOrder
                {
                    CreateDateTime = DateTime.Now,
                    CustOpenId = userOpenId,
                    OrderStatus = OrderStatus.Created,
                    OrderType = OrderType.UserCourse,
                    OrderId = EduCodeGenerator.GetOrderOrder(),
                   
                };
                _dbContext.Add(order);

                EOrderLine line = new EOrderLine
                {
                    OrderId = order.OrderId,
                    ItemCode = coursePrice.PriceCode,
                    ItemName = $"课时购买{coursePrice.PriceCode}_{coursePrice.CoursePriceType}",
                    Price = coursePrice.Price,
                    Qty = coursePrice.Qty,
                    Ext1 = (int)coursePrice.CoursePriceType,
                };
                _dbContext.Add(line);

            }
            catch (Exception ex)
            {
              
                throw ex;
            }
            return order;

        }
        #endregion

        #region 购买课时成功
        public void PayCourseSuccess(string orderId)
        {
            try
            {
                BeginTrans();
                //跟新订单状态
                var order = _dbContext.DBOrder.Where(a => a.OrderId == orderId && a.OrderStatus == OrderStatus.Created).FirstOrDefault();
                if (order == null)
                    throw new Exception($"没有找到状态为[{ OrderStatus.Created}]的[{orderId}]订单,");
                order.OrderStatus = OrderStatus.PaySuccess;

                //获取订单行,更新课时
                var line = _dbContext.DBOrderLine.Where(a => a.OrderId == orderId).FirstOrDefault();
                UpdateCourseTimeByLine(order.CustOpenId, line);

                //新建课时交易
                AddCourseTimeTransByLine(line);

                //更新这门课的总申请数

                _dbContext.SaveChanges();
                CommitTrans();
            }
            catch(Exception ex)
            {
                RollBackTrans();
                NLogHelper.ErrorTxt($"[PayCourseSuccess] {ex.Message}");
            }
        }

        /// <summary>
        /// /获取订单行,更新课时
        /// </summary>
        /// <param name="userOpenId"></param>
        /// <param name="line"></param>
        private void UpdateCourseTimeByLine(string userOpenId,EOrderLine line)
        {
            var CourseTime = _dbContext.DBUserCourseTime.Where(a => a.UserOpenId == userOpenId && (int)a.CoursePriceType == line.Ext1).FirstOrDefault();
            if (CourseTime == null)
            {
                CourseTime = new EUserCourseTime
                {
                    UserOpenId = userOpenId,
                    CoursePriceType = (CoursePriceType)line.Ext1,
                };
                _dbContext.DBUserCourseTime.Add(CourseTime);
            }
            CourseTime.RemainQty += line.Qty;
        }

        /// <summary>
        /// 新建课时交易
        /// </summary>
        /// <param name="line"></param>
        private void AddCourseTimeTransByLine(EOrderLine line)
        {
            EUserCourseTimeTrans trans = new EUserCourseTimeTrans
            {
                CoursePriceType = (CoursePriceType)line.Ext1,
                TransQty = line.Qty,
                UserOpenId = line.OrderId,
                CoursePriceCode = line.ItemCode,
            };
            _dbContext.DBUserCourseTimeTrans.Add(trans);
        }




        #endregion




    }
}
