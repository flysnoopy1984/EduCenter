using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.Common;
using EduCenterModel.User;
using EduCenterModel.User.Result;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.User
{
    public class NoteModel : EduBaseAppPageModel
    {
        public string CurrentUserName { get; set; }
        private UserSrv _UserSrv;
        public NoteModel(UserSrv userSrv)
        {
            _UserSrv = userSrv;
        }
        public void OnGet()
        {
            var us = GetUserSession();
            if (us != null)
                CurrentUserName = us.UserName;
            else
                CurrentUserName = "";
        }

        public IActionResult OnPostSaveNote(EUserNote eUserNote)
        {
            ResultNormal result = new ResultNormal();

            try
            {
                _UserSrv.AddUserNote(eUserNote);
                _UserSrv.SaveChanges();

            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
               
            }
            return new JsonResult(result);
        }

        public IActionResult OnPostDeleteNote(long Id)
        {
            ResultNormal result = new ResultNormal();

            try
            {
                _UserSrv.DeleteUserNote(Id);
               

            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;

            }
            return new JsonResult(result);
        }

        public IActionResult OnPostQueryUserNote(string userOpenId)
        {
            ResultList<RUserNote> result = new ResultList<RUserNote>();

            try
            {
                result.List  = _UserSrv.QueryUserNote(userOpenId);
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;

            }
            return new JsonResult(result);
        }
    }
}