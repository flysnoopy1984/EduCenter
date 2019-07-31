using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EduCenterModel.Common;
using EduCenterModel.BaseEnum;
using EduCenterModel.AliPay;

namespace EduCenterSrv
{
    public class GlobalSrv:BaseSrv
    {
        public GlobalSrv(EduDbContext dbContext) : base(dbContext)
        {

        }
        public EAliPayApplication GetAliPayApplication()
        {
            return _dbContext.DBAliPayApplication.FirstOrDefault();
        }


        public List<ECourseDateRange> GetCourseDateRangeList()
        {
            return _dbContext.DBCourseDateRange.ToList();
        }

        public List<EHoliday> GetHolidayJson()
        {
            return _dbContext.DBHoliday.ToList();
        }

        public const double TrialRewardAmt = 10;
        public const double PaiedReardAmt = 300;

        public static double GetRewardAmount(AmountTransType TransType)
        {
            switch (TransType)
            {
                case AmountTransType.Invited_Paied:
                    return PaiedReardAmt;
                case AmountTransType.Invited_TrialReward:
                    return TrialRewardAmt;
                default:
                    return 0;
            }
        }
        private static List<string> _NewUserReceiverList;
        public static List<string> GetNewUserReceiverList()
        {
            if(_NewUserReceiverList == null)
            {
                _NewUserReceiverList = new List<string>();
                _NewUserReceiverList.Add("oh6cV1QhPLj6XPesheYUQ4XtuGTs");  //Jacky

                _NewUserReceiverList.Add("oh6cV1dh0hjoGEizCoKH1KU70UwQ"); //童
                _NewUserReceiverList.Add("oh6cV1YaZFskTyZ3PXZ1g0VfSQjE"); //占
                _NewUserReceiverList.Add("oh6cV1XoVcMUmYztiXfOTbSnVpj8"); //marcus
                _NewUserReceiverList.Add("oh6cV1a4FW_x5u6yM86dafOY2Vgc"); //李老师
            }
            return _NewUserReceiverList;
        }
    }
}
