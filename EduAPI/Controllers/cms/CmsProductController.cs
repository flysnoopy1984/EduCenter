using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSSrv;
using CMSSrv.BookModel.Book;
using CMSSrv.CMSModel;
//using CMSSrv.CMSModel;
using CMSSrv.Srv;
using EduCenterModel.Common;
using EduCenterSrv.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduAPI.Controllers.cms
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CmsProductController : BaseAPI
    {
        private cmsSrv _cmsSrv;
        public CmsProductController(cmsSrv cmsSrv)
        {
            _cmsSrv = cmsSrv;
        }

        [HttpPost]
        public ResultList<Product> Test()
        {

            ResultList<Product> result = new ResultList<Product>();
           

            // _cmsSrv


            return result;
        }

        [HttpPost]
        public ResultList<RBookInfo> QueryList(QProductList q)
        {
            ResultList<RBookInfo> result = new ResultList<RBookInfo>();
            try
            {
                result.List = _cmsSrv.GetProductList(q.ProductCategoryId, q.pageIndex, q.pageSize);
            }
            catch(Exception ex)
            {
                result.ErrorMsg = "Query Error!";
            }
            return result;
        }

        public ResultObject<Product> Get(int productId)
        {
            ResultObject<Product> result = new ResultObject<Product>();
            try
            {
                result.Entity = _cmsSrv.GetProduct(productId);
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "Get Error!";
                
            }
            return result;
        }
    }

    public class QProductList
    {
        public int ProductCategoryId { get; set; }

        public int pageIndex { get; set; }

        public int pageSize { get; set; }
    }
}