using EduCenterModel.QR;
using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EduCenterModel.BaseEnum;
using EduCenterModel.WX;
using EduCenterCore.WX;
using EduCenterModel.Common;
using EduCenterCore.EduFramework;
using EduCenterCore.Common.Helper;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using EduCenterModel.Sales.Result;
using EduCenterSrv.Common;
using EduCenterModel.Sales;
using EduCenterModel.User;
using EduCenterModel.Order.Result;

namespace EduCenterSrv
{
    public class SalesSrv: BaseSrv
    {
        public static string sql_DeleteQRInfo(string openId)
        {
            string sql = $"delete from QRInvite where openId='{openId}'";
            return sql;
        }

      

        public SalesSrv(EduDbContext dbContext) : base(dbContext)
        {

        }

        #region UserInvite
        public EQRInvite GetQRInvite(string openId)
        {
            return _dbContext.DBQRInvite.Where(a => a.UserOpenId == openId &&
                                               a.InviteQRType == InviteQRType.UserInvite &&
                                               a.RecordStatus == RecordStatus.Normal).FirstOrDefault();
        }

        public bool GenQRInvite(string openId, string phone,string headerUrl)
        {
            var qr = _dbContext.DBQRInvite.Where(a => a.UserOpenId == openId &&
                                               a.InviteQRType == InviteQRType.UserInvite).FirstOrDefault();
            if (qr != null)
            {
                throw new EduException("不能重复创建邀请码");
            }
            string qrDownFilePath = EduEnviroment.GetQRInviteUserFilePath($"OrigUserInvite_{phone}.png");
            string qrWithLogoFilePath = EduEnviroment.GetQRInviteUserFilePath($"UserInviteWithLogo_{phone}.png");
            string bkFilePath = EduEnviroment.GetQRFilePath("InviteBK.png");
            string finalFilePath = EduEnviroment.GetQRInviteUserFilePath($"FinalUserInvite_{phone}.png");

            AccessToken accessToken = WXApi.getAccessToken();
            WXQRResult result = WXApi.getQR(WxConfig.QR_Invite_User+"_"+openId, accessToken.access_token);
            WXApi.DownLoadWXQR(result.ticket, qrDownFilePath);

            //添加Logo ,且添加文字
            QRHelper.AddLogoForQR(headerUrl, new Bitmap(qrDownFilePath), qrWithLogoFilePath);

     
            //添加背景
            QRHelper.AddBKForQR(bkFilePath, qrWithLogoFilePath, finalFilePath);

            qr = new EQRInvite()
            {
                OrigFilePath = EduEnviroment.VirPath_QRInviteUser + $"OrigUserInvite_{phone}.png",
                FileWithLogoPath = EduEnviroment.VirPath_QRInviteUser + $"UserInviteWithLogo_{phone}.png",
                FinalFilePath = EduEnviroment.VirPath_QRInviteUser + $"FinalUserInvite_{phone}.png",
                InviteQRType = InviteQRType.UserInvite,
                RecordStatus = RecordStatus.Normal,
                
                TargetUrl = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + result.ticket + "",
                CreateDateTime = DateTime.Now,
                UserOpenId = openId
            };
            _dbContext.DBQRInvite.Add(qr);
            _dbContext.SaveChanges();


            return true;
        }


        public RInviteLog GetInviteLogById(long Id)
        {
            var sql = from log in _dbContext.DBInviteLog
                      join ui in _dbContext.DBUserInfo on log.InvitedOpenId equals ui.OpenId
                      where log.Id == Id
                      select new RInviteLog
                      {
                          InvitedDateTime = log.InvitedDateTime,
                          InvitedOpenId = log.InvitedOpenId,
                          InvitedWxName = ui.Name,
                          InviteStatus = log.InviteStatus,
                          InviteStatusName = BaseEnumSrv.GetInviteStatusName(log.InviteStatus),
                          OwnOpenId = log.OwnOpenId,
                          Id = log.Id
                      };
            return sql.FirstOrDefault();
        }

        public EInviteLog GetInviteLogByInvitedOpenId(string invitedOpenId)
        {
            return _dbContext.DBInviteLog.Where(a => a.InvitedOpenId == invitedOpenId).FirstOrDefault();
        }
        public List<RInviteLog> QueryInviteLog(string openId, out int totalPage, int pageIndex = 1,int pageSize =20)
        {
            var sql = from log in _dbContext.DBInviteLog
                      join ui in _dbContext.DBUserInfo on log.InvitedOpenId equals ui.OpenId
                      where log.OwnOpenId == openId
                      select new RInviteLog
                      {
                          InvitedDateTime = log.InvitedDateTime,
                          InvitedOpenId = log.InvitedOpenId,
                          InvitedWxName = ui.Name,
                          InviteStatus = log.InviteStatus,
                          InviteStatusName = BaseEnumSrv.GetInviteStatusName(log.InviteStatus),
                          OwnOpenId = log.OwnOpenId,
                          Id = log.Id
                      };

            int totalCount = sql.Count();
            totalPage = Convert.ToInt32(totalCount / pageSize) + 1;

            var result = sql.OrderByDescending(a => a.InvitedDateTime)
                   .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return result;

        }

        //先会判断是否被邀请过
        public EUserInfo BindUser(string ownOpenId,string invitedOpenId)
        {
            var count = _dbContext.DBInviteLog.Where(a => a.InvitedOpenId == invitedOpenId).Count();
            EUserInfo owner = null;
            if (count == 0)
            {
                owner = _dbContext.DBUserInfo.Where(a => a.OpenId == ownOpenId).FirstOrDefault();
                EInviteLog log = new EInviteLog
                {
                    InvitedDateTime = DateTime.Now,
                    InvitedOpenId = invitedOpenId,
                    InviteStatus = InviteStatus.Invited,
                    OwnOpenId = ownOpenId,
                    OwnName = owner.Name,
                };
                _dbContext.DBInviteLog.Add(log);
            }
            return owner;
          
        }
        #endregion

        #region 邀请奖励

        public List<EInviteRewardTrans> GetRewardTransByInvitedId(long inviteLogId)
        {
            return _dbContext.DBInviteRewardTrans.Where(a => a.InviteLogId == inviteLogId).ToList();
        }

        public List<RAmountTrans> QueryUserAmountTrans(string openId)
        {
           var sql = _dbContext.DBInviteRewardTrans.Where(a => a.UserOpenId == openId)
                .OrderByDescending(a=>a.TransDateTime)
                .Select(a => new RAmountTrans
            {
                Amount = a.Amount.ToString("0.00"),
                TransDate = a.TransDateTime.ToString("yyyy-MM-dd hh:mm"),
                UserOpenId = openId,
                TransTypeName = BaseEnumSrv.GetAmountTransTypeName(a.TransType),

                transDirection = a.Direction,
                
            });
            return sql.ToList();

        }

        public void CreateTransfer(double amount,string userOpenId,string transferId,bool needSave=true)
        {
            var transfer = new EInviteRewardTrans()
            {
                Amount = amount,
                Direction = AmountTransDirection.Out,
                InviteLogId = -1,
                TransDateTime = DateTime.Now,
                TransType = AmountTransType.TransferToUser,
                TransStatus = AmountTransStatus.Settled,
                UserOpenId = userOpenId,
                TransferId = transferId
            };
            _dbContext.DBInviteRewardTrans.Add(transfer);
            if(needSave)
            _dbContext.SaveChanges();
        }
      
        public bool CreateRewardTrans(long inviteLogId,string ownOpenId, AmountTransType TransType,out EUserAccount UserAccount)
        {
            int c = _dbContext.DBInviteRewardTrans.Where(a => a.InviteLogId == inviteLogId && a.TransType == TransType).Count();
            UserAccount = null;
            if (c==0)
            {
                var reward = new EInviteRewardTrans()
                {
                    Amount = GlobalSrv.GetRewardAmount(TransType),
                    Direction = AmountTransDirection.In,
                    InviteLogId = inviteLogId,
                    TransDateTime = DateTime.Now,
                    TransType = TransType,
                    UserOpenId = ownOpenId,
                    TransStatus = AmountTransStatus.Created
                };
                _dbContext.DBInviteRewardTrans.Add(reward);
                UserAccount = _dbContext.DBUserAccount.Where(a => a.UserOpenId == ownOpenId).FirstOrDefault();
                if (UserAccount != null)
                {
                    UserAccount.InviteRewards += reward.Amount;
                    UserAccount.RemainRewards += reward.Amount;
                }

                var inviteLog = _dbContext.DBInviteLog.Where(a => a.Id == inviteLogId).FirstOrDefault();
                if(inviteLog!=null)
                {
                    if (TransType == AmountTransType.Invited_TrialReward)
                        inviteLog.InviteStatus = InviteStatus.ApplyTrial;
                    else if (TransType == AmountTransType.Invited_Paied)
                        inviteLog.InviteStatus = InviteStatus.Paied;
                }
              

                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion
    }
}
