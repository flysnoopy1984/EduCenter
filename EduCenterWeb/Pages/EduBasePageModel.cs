
using EduCenterSrv.DataBase;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduCenterWeb.Pages
{
    public class EduBasePageModel: PageModel
    {
        protected EduDbContext _context;
       
    }
}
