using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Teacher;
using EduCenterModel.Teacher.Result;
using EduCenterModel.User;
using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EduCenterCore.EduFramework;
using EduCenterModel.BaseEnum;

namespace EduCenterSrv
{
    public class TecSrv : BaseSrvMasterData<ETecInfo>
    {
      

        public TecSrv(EduDbContext dbContext):base(dbContext)
        {
           
        }

        #region SQL
        public static string sql_DeleteALLTecSkill(string tecCode)
        {
            string sql = $"delete from TecSkill where TecCode='{tecCode}'";
            return sql;
        }
        #endregion


       
        public List<STec> GetSimpleList()
        {

            return _dbContext.DBTecInfo.Select(a => new STec
            {
                Code = a.Code,
                Name = a.Name,
              

            }).ToList();
        }

        public RTecAllInfo GetAllInfo(string code)
        {
            RTecAllInfo result = new RTecAllInfo();
            result.TecInfo = Get(code);
            result.TecSkillList = GetTecSkillList(code);
       
            return result;
        }
        public ETecInfo Get(string code)
        {
            return _dbContext.DBTecInfo.Where<ETecInfo>(a => a.Code == code).FirstOrDefault();
        }

        /// <summary>
        /// 更新基本信息
        /// </summary>
        public void UpdatePartTecInfo(ETecInfo updatedTec,bool needSave =true)
        {
            _dbContext.DBTecInfo.Attach(updatedTec);
            _dbContext.Entry(updatedTec).Property(p => p.Name).IsModified = true;
            _dbContext.Entry(updatedTec).Property(p => p.Phone).IsModified = true;
            if (needSave)
                _dbContext.SaveChanges();
        }

       


        public void NewTecFromUser(EUserInfo user)
        {
            int count =  _dbContext.DBTecInfo.Count(t => t.UserOpenId == user.OpenId);
            if(count ==0)
            {
                int No = _dbContext.DBTecInfo.Count();
                No++;
                //教师信息
                ETecInfo tec = new ETecInfo
                {
                    Sex = user.Sex,
                    Code = EduCodeGenerator.GetTecCode(No),
                    Name = user.Name,
                    UserOpenId = user.OpenId,
                    WxName = user.wx_Name,
                    Phone = user.Phone,
                    CreatedDateTime = DateTime.Now,
                    UpdatedDateTime = DateTime.Now,
                };

                //教师技能
                _dbContext.Database.ExecuteSqlCommand(TecSrv.sql_DeleteALLTecSkill(tec.Code));

                CourseSrv courseSrv = new CourseSrv(this._dbContext);
                var courseList =  courseSrv.GetCourseType();
                foreach(var course in courseList)
                {
                    ETecSkill ts = new ETecSkill
                    {
                        CourseType = (CourseType)course.Key,
                        SkillLevel = SkillLevel.None,
                        TecCode = tec.Code,
                    };
                    
                   _dbContext.DBTecSkill.Add(ts);
                }

                _dbContext.DBTecInfo.Add(tec);
                _dbContext.Database.ExecuteSqlCommand(UserSrv.sql_UpdateUserRole(UserRole.Teacher, user.OpenId));
                _dbContext.SaveChanges();
               
            }
           
        }


        #region 技能
        public List<SiKsV> GetSkillLevelList()
        {
          
            return BaseEnumSrv.SkillLevelList;
        }

        public List<ETecSkill> GetTecSkillList(string tecCode = null)
        {
           
            if(tecCode!=null)
                return _dbContext.DBTecSkill.Where(s => s.TecCode == tecCode).ToList();
            else
                return _dbContext.DBTecSkill.ToList();
        }

        public List<ETecSkill> GetTecAvaliableSkill()
        {
            return _dbContext.DBTecSkill.Where(s => s.SkillLevel != SkillLevel.None).ToList();
        }

        public void UpdateTecSkillLevel(ETecSkill tecSkill,bool needSave = true)
        {
            _dbContext.DBTecSkill.Attach(tecSkill);
            _dbContext.Entry(tecSkill).Property(p => p.SkillLevel).IsModified = true;
            if (needSave)
                _dbContext.SaveChanges();
        }

        #endregion


    }
}
