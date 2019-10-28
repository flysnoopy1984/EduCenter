using System;

using EduCenterCore.Common.Helper;
using EduCenterCore.EduFramework;
using EduCenterCore.WX;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Order;
using EduCenterModel.Teacher;
using EduCenterModel.User;
using EduCenterModel.WX;
using EduCenterSrv.Common;
using EduCenterSrv.DataBase;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using EduCenterModel.Sales;

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

             
                //检查用户是否已经购买暑假寒假
                if(coursePrice.CourseScheduleType == CourseScheduleType.Summer ||
                    coursePrice.CourseScheduleType == CourseScheduleType.Winter)
                {
                    var sql = from o in _dbContext.DBOrder
                              join l in _dbContext.DBOrderLine on o.OrderId equals l.OrderId
                              where o.OrderStatus == OrderStatus.PaySuccess && o.CustOpenId == userOpenId
                              && l.ItemCode == coursePrice.PriceCode
                              select o.OrderId;

                    var item = sql.FirstOrDefault();
                    if(item != null)
                    {
                        var cstName = BaseEnumSrv.GetCourseScheduleTypeName(coursePrice.CourseScheduleType);
                        throw new EduException($"[{cstName}]只能购买一次，您已购买过");
                    }
                }

                //创建购买课时的订单
                var Order =  CreateBuyCourseOrder(userOpenId, coursePrice);

                _dbContext.SaveChanges();

              //  CommitTrans();

                return Order;

            }
            catch (Exception ex)
            {
             //  RollBackTrans();
              
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
        public EUserAccount PayCourseSuccess(WXPaySuccess wXPaySuccess)
        {
            EUserAccount eUserAccount = null;
            try
            {
                string orderId = wXPaySuccess.OrderId;
            //    NLogHelper.InfoTxt($"PayCourseSuccess OrderId:{orderId}");
                BeginTrans();
                //跟新订单状态
                var order = _dbContext.DBOrder.Where(a => a.OrderId == orderId && a.OrderStatus == OrderStatus.Created).FirstOrDefault();
                if (order == null)
                    throw new Exception($"没有找到状态为[{ OrderStatus.Created}]的[{orderId}]订单,");
                order.OrderStatus = OrderStatus.PaySuccess;

                //获取订单行,更新课时
                var line = _dbContext.DBOrderLine.Where(a => a.OrderId == orderId).FirstOrDefault();
                eUserAccount = UpdateUserAccountByOrderLine(order.CustOpenId, line);

                var ui = _dbContext.DBUserInfo.Where(a => a.OpenId == order.CustOpenId).FirstOrDefault();
                if(ui.UserRole == UserRole.Visitor)
                {
                    ui.UserRole = UserRole.Member;
                }
                //如果是JSPay
                if(wXPaySuccess.IsJSPay)
                {
                    ui.MemberType = MemberType.VIP;
                    eUserAccount.VIPPrice1 = Math.Round(line.Price/line.Qty, 2);
                }
                //查看用户是否有邀请人

                SalesSrv salesSrv = new SalesSrv(_dbContext);
                var inviteLog = salesSrv.GetInviteLogByInvitedOpenId(ui.OpenId);
                if(inviteLog!=null)
                {
                    salesSrv.CreateRewardTrans(inviteLog.Id, inviteLog.OwnOpenId, AmountTransType.Invited_Paied, out eUserAccount,false);
                }               

                _dbContext.SaveChanges();
                CommitTrans();
            }
            catch(Exception ex)
            {
                RollBackTrans();
                NLogHelper.ErrorTxt($"[PayCourseSuccess] {ex.Message}");
                throw ex;
            }
            return eUserAccount;
        }


       
      

      
        /// <summary>
        /// /获取订单行,更新课时
        /// </summary>
        /// <param name="userOpenId"></param>
        /// <param name="line"></param>
        private EUserAccount UpdateUserAccountByOrderLine(string userOpenId,EOrderLine line)
        {
       
            EUserAccount userAccount =  _dbContext.DBUserAccount.Where(a => a.UserOpenId == userOpenId).FirstOrDefault();
            if(userAccount == null)
            {
                UserSrv userSrv = new UserSrv(_dbContext);
                userAccount = userSrv.CreateNewUserAccount(userOpenId);
            }
            
            CourseScheduleType courseScheduleType = (CourseScheduleType)line.Ext1;
            ECourseDateRange dr = null;
            switch(courseScheduleType)
            {
                case CourseScheduleType.Standard:
                case CourseScheduleType.VIP:
                    userAccount.RemainCourseTime += line.Qty;
                    if (userAccount.BuyDate == DateTime.MinValue)
                        userAccount.BuyDate = DateTime.Now;
                    if (userAccount.DeadLine == DateTime.MinValue)
                        userAccount.DeadLine = DateTime.Now;
                    userAccount.DeadLine = userAccount.DeadLine.AddYears(1);
                
                    break;
                case CourseScheduleType.Summer:
                    userAccount.RemainSummerTime += line.Qty;
                    dr = StaticDataSrv.CourseDateRange.Where(a => a.CourseScheduleType == CourseScheduleType.Summer && a.Year == DateTime.Now.Year).FirstOrDefault();

                    userAccount.SummerDeadLine = dr.EndDate;

                    if (userAccount.SummerBuyDate == DateTime.MinValue)
                        userAccount.SummerBuyDate = DateTime.Now;

                    break;
                case CourseScheduleType.Winter:
                    userAccount.RemainWinterTime += line.Qty;
                    dr = StaticDataSrv.CourseDateRange.Where(a => a.CourseScheduleType == CourseScheduleType.Winter && a.Year == DateTime.Now.Year).FirstOrDefault();
                    userAccount.WinterDeadLine = dr.EndDate;
                    if (userAccount.WinterBuyDate == DateTime.MinValue)
                        userAccount.WinterBuyDate = DateTime.Now;
                    break;
                
            }
            return userAccount;

        }

        /// <summary>
        /// 新建课时交易
        /// </summary>
        /// <param name="line"></param>
        //private void AddCourseTimeTransByLine(EOrderLine line)
        //{
        //    EUserCourseTimeTrans trans = new EUserCourseTimeTrans
        //    {
        //        CourseScheduleType = (CourseScheduleType)line.Ext1,
        //        TransQty = line.Qty,
        //        UserOpenId = line.OrderId,
        //        CoursePriceCode = line.ItemCode,
        //        TransDateTime = DateTime.Now,
                
        //    };
        //    _dbContext.DBUserCourseTimeTrans.Add(trans);
        //}

        #endregion

        #region 用户课程

        /// <summary>
        /// 用户选择课程
        /// </summary>
        /// <param name="courseList"></param>
        public void UserSelectNewCourses(string openId,List<EUserCourse> courseList, CourseScheduleType courseScheduleType,bool useRightNow = false,bool isAdmin =false)
        {
            try
            {
                BeginTrans();
                if (courseList.Count > 0)
                {
                    UserSrv userSrv = new UserSrv(_dbContext);
                    TecSrv tecSrv = new TecSrv(_dbContext);
                    //不是后台选择，且选择已满
                    if (!isAdmin && !userSrv.CheckUserCanSelectCourse(openId, courseScheduleType))
                        throw new EduException("无法选择，您已经选择过此类课程!，如果疑问，请联系客服");
                    else
                    {

                        foreach (var c in courseList)
                        {
                            if (userSrv.CheckUserHasThisCourse(openId, c.LessonCode))
                            {
                                c.CourseScheduleType = courseScheduleType;
                                continue;
                            }
                               

                            c.UserOpenId = openId;
                            c.UseRightNow = useRightNow;

                            //更新课程总人数
                            var cs = _dbContext.DbCourseSchedule.Where(a => a.LessonCode == c.LessonCode).FirstOrDefault();
                            cs.ApplyNum++;

                            //获取课程对应的老师
                            var cls = _dbContext.DBCourseInfoClass.Where(s => s.CourseCode == cs.CourseCode).FirstOrDefault();
                            var tecCode = cls.TecCode;

                            //更新老师课程
                            tecSrv.UpdateTecCourse(tecCode, cs,DateTime.Now, useRightNow);

                            //添加用户课程
                            userSrv.AddUserCourse(c);

                        }
                        
                        userSrv.UpdateCanSelectCourse(openId, courseScheduleType, false);
                    }
                    _dbContext.SaveChanges();

                    userSrv.AddNextCourseLog(openId, false);

                    CommitTrans();

                  

                }
            }
            catch(Exception ex)
            {
                RollBackTrans();
              
                throw ex;
            }
           
            
        }

        public void UserSelectNewCourses(string openId, string lessonCode, CourseScheduleType courseScheduleType, bool useRightNow = false,bool isAdmin=false)
        {
            var list = CreateNewCourse(openId, lessonCode, courseScheduleType);
            UserSelectNewCourses(openId, list, courseScheduleType, useRightNow, isAdmin);
        }

        //删除用户一门课程
        public bool DeleteUserCourse(string openId, string LessonCode)
        {
            try
            {
                _dbContext.Database.BeginTransaction();
                //删除用户课程表
                UserSrv userSrv = new UserSrv(_dbContext);
                userSrv.DeleteUserCourse(openId, LessonCode);

                //更新课程报名数
                var cs = _dbContext.DbCourseSchedule.Where(a => a.LessonCode == LessonCode).FirstOrDefault();
                cs.ApplyNum--;
                if(cs.ApplyNum == 0)
                {
                    TecSrv tecSrv = new TecSrv(_dbContext);
                    //删除老师课程
                    tecSrv.DeleteTecCourse(LessonCode);

                }
                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();
            }
            catch(Exception ex)
            {
                _dbContext.Database.RollbackTransaction();
                throw ex;
            }
            return true;

        }

        public List<EUserCourse> CreateNewCourse(string openId, string lessonCode, CourseScheduleType courseScheduleType)
        {
            EUserCourse eUserCourse = new EUserCourse
            {
                CourseScheduleType = courseScheduleType,
                CreateDateTime = DateTime.Now,
                LessonCode = lessonCode,
                UserOpenId = openId,
                UseRightNow = false,
            };
            List<EUserCourse> list = new List<EUserCourse>();
            list.Add(eUserCourse);

            return list;
        }

        //删除用户课程，并创建新课程
        public void AdjustUserCourse(string openId,string fromLessonCode,string toLessonCode, CourseScheduleType courseScheduleType,bool isAdmin= false)
        {
            try
            {
                bool isDelete = false; 
                try
                {
                    isDelete = DeleteUserCourse(openId, fromLessonCode);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                if(isDelete)
                    UserSelectNewCourses(openId, toLessonCode, courseScheduleType,true,true);
              
            }
            catch(Exception ex)
            {
                throw ex;
            }
           


        }
        #endregion

        #region 用户课时消耗
        /// <summary>
        /// VIP 扣减课时，根据传入的班类，如果假期班，有假期班课时，则扣，不然扣标准版
        /// 
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="memberType"></param>
        /// <param name="courseScheduleType"></param>
        /// <param name="lessonCode"></param>
        /// <param name="signDate"></param>
        /// <param name="needSave"></param>
        /// <returns></returns>
        public EUserCourseLog UpdateCourseLogToSigned(string openId,
            MemberType memberType, 
            CourseScheduleType courseScheduleType, 
            string lessonCode,
            DateTime signDate,
            string signOpenId = "",
            bool skipLeave = true, //不管是否请假都签到
            bool needSave = true)
        {
            var date = signDate.ToString("yyyy-MM-dd");

           

            var log = _dbContext.DBUserCourseLog.Where(a => a.UserOpenId == openId &&
                                              a.LessonCode == lessonCode &&
                                              a.CourseDateTime == date).FirstOrDefault();
            if (log == null)
            {
                log = new EUserCourseLog
                {
                    CourseDateTime = date,
                    CourseScheduleType = courseScheduleType,
                    CreatedDateTime = DateTime.Now,
                    LessonCode = lessonCode,
                    UserSignDateTime = DateTime.Now,
                    UserOpenId = openId
                };
                
                _dbContext.DBUserCourseLog.Add(log);
            }
            else
            {
                if(log.UserCourseLogStatus != UserCourseLogStatus.PreNext)
                {
                    if (log.UserCourseLogStatus == UserCourseLogStatus.Leave && !skipLeave)
                        return log;
                   
                }
            }
            //更新用户课时
            if((int)log.UserCourseLogStatus <10)
            {
                int result = UpdateUserCourseTimeOnce(openId, memberType, courseScheduleType);
                if (result == -1)
                {
                    string courseScheduleTypeName = BaseEnumSrv.GetCourseScheduleTypeName(courseScheduleType);
                    throw new EduException($"您的[{courseScheduleTypeName}]课时已用完，请先充值！", EduErrorMessage.NoCourseTime);
                }
            }
          


            EUserInfo signUser = null;
            if (string.IsNullOrEmpty(signOpenId))
                signUser = _dbContext.DBUserInfo.Where(a => a.OpenId == signOpenId).FirstOrDefault();

            if (signUser != null)
            {
                log.SignOpenId = signUser.OpenId;

                if (signUser.UserRole == UserRole.Member)
                    log.SignName = signUser.ChildName;
                else if (signUser.UserRole == UserRole.Teacher)
                    log.SignName = signUser.RealName;
            }
            else
                log.SignName = "系统签到";

            log.UserCourseLogStatus = UserCourseLogStatus.SignIn;
            log.UserSignDateTime = DateTime.Now;

            if (needSave)
             _dbContext.SaveChanges();
            return log;
        }

        /// <summary>
        ///根据传入的班类，如果是假期班，有假期班课时则扣减假期班，没则扣标准班
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="memberType"></param>
        /// <param name="courseScheduleType"></param>
        /// <returns>返回-1 用户余额不足</returns>
        public int UpdateUserCourseTimeOnce(string openId,MemberType memberType, CourseScheduleType courseScheduleType)
        {
            EUserAccount result = _dbContext.DBUserAccount.Where(a => a.UserOpenId == openId).FirstOrDefault();
            double reduceTime = 2;
            if (result.ReduceTime > 0)
                reduceTime = result.ReduceTime;

            switch (courseScheduleType)
            {
                case CourseScheduleType.Summer:
                    if (result.RemainSummerTime == 0)
                    {
                        if (result.RemainCourseTime == 0) return -1;
                        else result.RemainCourseTime -= reduceTime;
                    }
                    else
                        result.RemainSummerTime -= reduceTime;
                    break;
                case CourseScheduleType.Winter:
                    if (result.RemainWinterTime == 0)
                    {
                        if (result.RemainCourseTime == 0) return -1;
                        else result.RemainCourseTime -= reduceTime;
                    }
                    else
                        result.RemainWinterTime -= reduceTime;
                    break;
                default:
                    if (result.RemainCourseTime == 0) return -1;
                    else result.RemainCourseTime -= reduceTime;
                    break;

            }

            //if(memberType == MemberType.Normal)
            //{
            //    switch (courseScheduleType)
            //    {
            //        case CourseScheduleType.Summer:
            //            if (result.RemainSummerTime == 0) return -1;
            //            else result.RemainSummerTime -= 2;
            //            break;
            //        case CourseScheduleType.Winter:
            //            if (result.RemainWinterTime == 0) return -1;
            //            else result.RemainWinterTime -= 2;
            //            break;
            //        default:
            //            if (result.RemainCourseTime == 0) return -1;
            //            else result.RemainCourseTime -= 2;
            //            break;

            //    }
            //}
            //else
            //{

            //}

            return 0;

        }
        #endregion

        #region 用户
        /// <summary>
        /// 被邀请用户首次进入公众号，绑定关系
        /// wxUser 因为获取方式不同
        /// </summary>
        /// <param name="wxMessage"></param>
        /// <param name="ownOpenId"></param>
        /// <returns></returns>
        public EUserInfo InvitedUserComing(string InvitedOpenId,string ownOpenId, WXUserInfo wxUser = null)
        {
            UserSrv userSrv = new UserSrv(_dbContext);
            SalesSrv salesSrv = new SalesSrv(_dbContext);
            EUserInfo user = null;
            try
            {
                if (wxUser == null)
                    wxUser = WXApi.GetWXUserInfo(InvitedOpenId);

            }
            catch(Exception ex)
            {
                NLogHelper.ErrorTxt($"businessSrv-[InvitedUserComing] -GetWXUserInfo Error:{ex.Message}");
                throw ex;
            }


            EUserInfo owner = null;
            try
            {
                //如果是老用户，不能绑定邀请
                if (!userSrv.IsExistUser(InvitedOpenId))
                    owner = salesSrv.BindUser(ownOpenId, InvitedOpenId);
            }
            catch (Exception ex)
            {
                NLogHelper.ErrorTxt($"businessSrv-[InvitedUserComing] -BindUser Error:{ex.Message}");
                throw ex;
            }

            try
            {
               user = userSrv.AddOrUpdateFromWXUser(wxUser, owner, false);
            }
            catch (Exception ex)
            {
                NLogHelper.ErrorTxt($"businessSrv-[InvitedUserComing] -AddOrUpdateFromWXUser Error:{ex.Message}");
                throw ex;
            }
           

            _dbContext.SaveChanges();
            return user;

        }
        public bool UserRegisterByPhone(string openId, string phone, string babyName)
        {
            try
            {
                var updatePhoneSql = UserSrv.sql_UpdateUserPhone(openId, phone);
                _dbContext.Database.BeginTransaction();
                //更新用户手机号
                _dbContext.Database.ExecuteSqlCommand(updatePhoneSql);

                UserSrv userSrv = new UserSrv(_dbContext);
                //添加宝贝，同时更新用户表中宝贝名显示字段
                userSrv.AddNewSimpleChild(openId, babyName);
                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();
            }
            catch(Exception ex)
            {
                _dbContext.Database.RollbackTransaction();
                throw ex;
            }
            return true;
          

        }
        #endregion

      




    }
}
