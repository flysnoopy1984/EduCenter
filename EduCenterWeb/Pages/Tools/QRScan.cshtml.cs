using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.Tools
{
    public class QRScanModel : PageModel
    {
        private ToolsSrv _ToolsSrv;
        public QRScanModel(ToolsSrv toolsSrv)
        {
            _ToolsSrv = toolsSrv;
        }
        public void OnGet()
        {
            var code = Convert.ToString(Request.Query["code"]);
            NLogHelper.InfoTxt($"LessonQR Scan Code:{code}");
            var qr =  _ToolsSrv.GetQR(code);
            if(qr !=null)
            {
                if(!string.IsNullOrEmpty(qr.Url))
                {
                    if (!qr.Url.StartsWith("http"))
                        qr.Url = "http://" + qr.Url;
                    HttpContext.Response.Redirect(qr.Url);
                }
            }

        }
    }
}