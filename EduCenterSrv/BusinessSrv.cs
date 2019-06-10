﻿using EduCenterCore.Common.Helper;
using EduCenterCore.EduFramework;
using EduCenterModel.BaseEnum;
using EduCenterModel.Course;
using EduCenterModel.Order;
using EduCenterModel.Teacher;
using EduCenterModel.User;
using EduCenterSrv.Common;
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

        #region SQL
        ///// <summary>
        ///// 删除某一类型的用户课程（标准，夏季，冬季）
        ///// </summary>
        //public static string sql_DeleteAllUserCourseByType(string userOpenId,CoursePriceType coursePriceType)
        //{
        //    string sql = $"delete from UserCourse where UserOpenId='{userOpenId}' and CoursePriceType={(int)coursePriceType}";
        //    return sql;
        //}

        /// <summary>
        /// 删除等待支付的用户课程
        /// </summary>
        public static string sql_DeleteWaitingPayUserCourse(string userOpenId)
        {
            string sql = $"delete from UserCourse where UserOpenId='{userOpenId}' and UserCourseStatus={(int)UserCourseStatus.WaitingPay}";
            return sql;
        }


        /// <summary>
        /// 更新这门课的总申请数
        /// </summary>
        public static string sql_UpdateCourseScheduleApplyNum(List<string> lessonCodeList)
        {
            string sqlCodes = "";
            for(int i=0;i<lessonCodeList.Count;i++)
            {
                sqlCodes += $"'{ lessonCodeList[i]}'";
                if((i+1)< lessonCodeList.Count)
                    sqlCodes += ",";
            }
            string sql = $"update CourseSchedule set ApplyNum+= 1 where LessonCode in ({sqlCodes})";
            return sql;
        }

        #endregion


        #region 提交购买课时订单

        public EOrder PayCourseOrder(string userOpenId, ECoursePrice coursePrice)
        {
            try
            {
               //  BeginTrans();

                ////删除等待支付的用户课程
                //_dbContext.Database.ExecuteSqlCommand(sql_DeleteWaitingPayUserCourse(userOpenId));

                //////新建等待支付的用户课程
                //foreach (var uc in userCourses)
                //{
                //    uc.UserCourseStatus = UserCourseStatus.WaitingPay;
                //    uc.CreateDateTime = DateTime.Now;
                //}
                //_dbContext.DBUserCoures.AddRange(userCourses);

                //创建购买课时的订单
                var Order =  CreateBuyCourseOrder(userOpenId, coursePrice);

                _dbContext.SaveChanges();

              //  CommitTrans();

                return Order;

            }
            catch (Exception ex)
            {
             //  RollBackTrans();
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
                    ItemName = $"课时购买{coursePrice.PriceCode}_{coursePrice.CourseScheduleType}",
                    Price = coursePrice.Price,
                    Qty = coursePrice.Qty,
                    Ext1 = (int)coursePrice.CourseScheduleType,
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
        public bool PayCourseSuccess(string orderId)
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

                //更新用户课程表
                //    var newLessonCode =UpdateUserCourseToAvaliable(order.CustOpenId);
                UpdateUserCourseToAvaliable(order.CustOpenId);

                /*
                if (newLessonCode.Count>0)
                {
                    foreach(var lc in newLessonCode)
                    {
                        var cs = _dbContext.DbCourseSchedule.Where(a => a.LessonCode == lc).FirstOrDefault();
                        cs.ApplyNum++;

                        //更新老师课程表
                        var cls = _dbContext.DBCourseInfoClass.Where(s => s.CourseCode == cs.CourseCode).FirstOrDefault();
                        var tecCode = cls.TecCode;
                        UpdateTecCourse(tecCode, cs);
                    }
                }
                */
          
                _dbContext.SaveChanges();
                CommitTrans();
            }
            catch(Exception ex)
            {
                RollBackTrans();
                NLogHelper.ErrorTxt($"[PayCourseSuccess] {ex.Message}");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 将等待支付的课程更新到用户课程中
        /// </summary>
        /// <returns>返回新增的课程</returns>
        private List<string> UpdateUserCourse_Old(string userOpenId)
        {
            List<string> result = new List<string>();

            List<EUserCourse> allUserCourse = _dbContext.DBUserCoures.Where(a=>a.UserOpenId == userOpenId).ToList();
            List<EUserCourse> curList = allUserCourse.Where(a=>a.UserCourseStatus != UserCourseStatus.WaitingPay).ToList();
            List<EUserCourse> waitList = allUserCourse.Where(a => a.UserCourseStatus == UserCourseStatus.WaitingPay).ToList();

            foreach(var waitCourse in waitList)
            {
                var curCourse = curList.Where(a => a.LessonCode == waitCourse.LessonCode).FirstOrDefault();
                if (curCourse == null)
                {
                    waitCourse.UserCourseStatus = UserCourseStatus.Avaliable;
                    result.Add(waitCourse.LessonCode);
                }  
                else
                {
                    if(curCourse.UserCourseStatus == UserCourseStatus.OutofData)
                        curCourse.UserCourseStatus = UserCourseStatus.Avaliable;

                    _dbContext.DBUserCoures.Remove(waitCourse);
                }                   
            }
            return result;

        }

       
        private void UpdateUserCourseToAvaliable(string userOpenId)
        {
           
            List<EUserCourse> allUserCourse = _dbContext.DBUserCoures.Where(a => a.UserOpenId == userOpenId && 
                                                                            a.UserCourseStatus != UserCourseStatus.WaitingPay
                                                                ).ToList();

            if(allUserCourse.Count>0)
            {
                foreach (var course in allUserCourse)
                {
                    course.UserCourseStatus = UserCourseStatus.Avaliable;
                }
                _dbContext.SaveChanges();
            }
        }

      
        /// <summary>
        /// /获取订单行,更新课时
        /// </summary>
        /// <param name="userOpenId"></param>
        /// <param name="line"></param>
        private void UpdateCourseTimeByLine(string userOpenId,EOrderLine line)
        {
            var CourseTime = _dbContext.DBUserCourseTime.Where(a => a.UserOpenId == userOpenId && (int)a.CourseScheduleType == line.Ext1).FirstOrDefault();
            if (CourseTime == null)
            {
                CourseTime = new EUserCourseTime
                {
                    UserOpenId = userOpenId,
                    CourseScheduleType = (CourseScheduleType)line.Ext1,
                    CreateDateTime = DateTime.Now,
                    RemainQty = 0,
                    InValidDateTime = DateTime.Now.AddYears(1),
                    ReNewDateTime = DateTime.MinValue,
                };
                _dbContext.DBUserCourseTime.Add(CourseTime);
            }
            //续费
            else
            {
                CourseTime.ReNewDateTime = DateTime.Now;
                CourseTime.InValidDateTime = DateTime.Now.AddYears(1);
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
                CourseScheduleType = (CourseScheduleType)line.Ext1,
                TransQty = line.Qty,
                UserOpenId = line.OrderId,
                CoursePriceCode = line.ItemCode,
                TransDateTime = DateTime.Now,
                
            };
            _dbContext.DBUserCourseTimeTrans.Add(trans);
        }

        #endregion

        #region 选择课程

        /// <summary>
        /// 用户选择课程
        /// </summary>
        /// <param name="courseList"></param>
        public void UserSelectNewCourses(string openId,List<EUserCourse> courseList)
        {
            try
            {
                BeginTrans();
                if (courseList.Count > 0)
                {
                    UserSrv userSrv = new UserSrv(_dbContext);
                    TecSrv tecSrv = new TecSrv(_dbContext);
                    if (userSrv.CheckHasUserCourse(openId, courseList[0].CourseScheduleType))
                        throw new Exception("已经选择过课程!");
                    else
                    {
                        foreach (var c in courseList)
                        {
                            c.UserOpenId = openId;

                            //更新课程总人数
                            var cs = _dbContext.DbCourseSchedule.Where(a => a.LessonCode == c.LessonCode).FirstOrDefault();
                            cs.ApplyNum++;

                            //获取课程对应的老师
                            var cls = _dbContext.DBCourseInfoClass.Where(s => s.CourseCode == cs.CourseCode).FirstOrDefault();
                            var tecCode = cls.TecCode;

                            //更新老师课程
                            tecSrv.UpdateTecCourse(tecCode, cs);

                        }
                        userSrv.AddUserCourse(courseList);
                    }
                    _dbContext.SaveChanges();

                    userSrv.AddNextCourseLog(openId, null, false);

                    CommitTrans();

                  

                }
            }
            catch(Exception ex)
            {
                RollBackTrans();
                NLogHelper.ErrorTxt($"[UserSelectNewCourses]{ex.Message}");
                throw ex;
            }
           
           
           
            
        }
       
      
        #endregion




    }
}
