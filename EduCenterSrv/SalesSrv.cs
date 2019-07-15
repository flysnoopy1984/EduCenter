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
                      };

            int totalCount = sql.Count();
            totalPage = Convert.ToInt32(totalCount / pageSize) + 1;

            var result = sql.OrderByDescending(a => a.InvitedDateTime)
                   .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return result;

        }

        //先会判断是否被邀请过
        public void BindUser(string ownOpenId,string invitedOpenId)
        {
            var count = _dbContext.DBInviteLog.Where(a => a.InvitedOpenId == invitedOpenId).Count();
            if(count == 0)
            {
                EInviteLog log = new EInviteLog
                {
                    InvitedDateTime = DateTime.Now,
                    InvitedOpenId = invitedOpenId,
                    InviteStatus = InviteStatus.Invited,
                    OwnOpenId = ownOpenId
                };
                _dbContext.DBInviteLog.Add(log);
            }
          
        }
        #endregion
    }
}
