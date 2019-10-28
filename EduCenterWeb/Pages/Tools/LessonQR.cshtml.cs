using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterCore.EduFramework;
using EduCenterModel.Common;
using EduCenterModel.Tools;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.Tools
{
    public class LessonQRModel : PageModel
    {
        private ToolsSrv _ToolsSrv;
        public LessonQRModel(ToolsSrv toolsSrv)
        {
            _ToolsSrv = toolsSrv;
        }
        public void OnGet()
        {
          
        }

        public IActionResult OnPostCreateLessonQR(ELessonQR qr)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                var code = EduCodeGenerator.Tool_LessonQRCode(); 
                var url = $"http://edu.iqianba.cn/Tools/QRScan?code={code}";
                qr.Code = code;
                qr.CreateDateTime = DateTime.Now;
                string filePath =EduEnviroment.DicPath_Tools_LessonQR+$"{code}.png";
                qr.QRFilePath = EduEnviroment.VirPath_Tools_LessonQR + $"{code}_logo.png";

                string filePathWithLogo = EduEnviroment.DicPath_Tools_LessonQR + $"{code}_logo.png";

                var desc = new List<string>();
                if(!string.IsNullOrEmpty(qr.Name))
                    desc.Add(qr.Name);

                QRHelper.GenQR(url, filePath, desc);
                var logoUrl = "http://edu.iqianba.cn/images/logo_120.png";

                QRHelper.AddLogoForQR(logoUrl, new Bitmap(filePath), filePathWithLogo);

                _ToolsSrv.AddQR(qr);
                _ToolsSrv.SaveChanges();

                result.SuccessMsg = qr.QRFilePath;

            }
            catch(Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }
            return new JsonResult(result);
        }
    }
}