using CMSSrv.BookModel.Book;
using CMSSrv.CMSModel;
using CMSSrv.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMSSrv.Srv
{
    public class cmsSrv:baseCMSSrv
    {
        public cmsSrv(CmsDbContext dbContext) : base(dbContext)
        {

        }

        public Product GetProduct(int Id)
        {
          
               return _dbContext.Product.Where(a=>a.Id == Id).FirstOrDefault();
        }

        public List<RBookInfo> GetProductList(int productCategoryId = -1, int pageIndex=1,int pageSize=20)
        {
            var sql = from p in _dbContext.Product
                      join u in _dbContext.Users on p.CreateBy equals u.UserId 
                      orderby p.Id descending
                      select new RBookInfo
                      {
                          Id = p.Id,
                          Title = p.Title,
                          Description = p.Description,
                          ImageThumbUrl = p.ImageThumbUrl,
                          Author = p.Author,
                          Translator = p.Translator,
                          PublishUserId = p.CreateBy,
                          PublishUserName = p.CreatebyName,
                          ProductCategoryId = p.ProductCategoryId,
                          PublishUserHeaderUrl = u.PhotoUrl,
                          DetailUrl = p.Url,
                      };
            if (productCategoryId > 0)
                sql = sql.Where(a => a.ProductCategoryId == productCategoryId);
           
            sql = sql.Skip((pageIndex - 1) * pageSize).Take(pageSize);



            return sql.ToList();   

           // _dbContext.Product.Where(a=>a.ProductCategoryId == )
        }


    }
}
