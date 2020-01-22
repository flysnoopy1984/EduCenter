using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Data.Common;
using System.Reflection;

namespace EduCenterSrv
{
    public class BaseSrv
    {
        protected EduDbContext _dbContext;
        public BaseSrv(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();

        }

        public void BeginTrans()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTrans()
        {
            _dbContext.Database.CommitTransaction();

        }

        public void RollBackTrans()
        {
            _dbContext.Database.RollbackTransaction();

        }

        private List<Dictionary<string, object>> DataReaderSql(DbDataReader dr)
        {
            var result = new List<Dictionary<string, object>>();
            var columnSchema = dr.GetColumnSchema();
            while (dr.Read())
            {
                var item = new Dictionary<string, object>();
                foreach (var kv in columnSchema)
                {
                    if (kv.ColumnOrdinal.HasValue)
                    {
                        var itemVal = dr.GetValue(kv.ColumnOrdinal.Value);
                        item.Add(kv.ColumnName, itemVal.GetType() != typeof(DBNull) ? itemVal : "");
                    }
                }
                result.Add(item);
            }
            return result;
        }
        public List<Dictionary<string, object>> SpExecForPage<T>(string sql, Microsoft.Data.SqlClient.SqlParameter[] sqlParams) where T:class,new()
        {
            var connection = _dbContext.Database.GetDbConnection();
            var result = new List<Dictionary<string, object>>();
            using (var cmd = connection.CreateCommand())
            {
                _dbContext.Database.OpenConnection();
                cmd.CommandText = sql;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddRange(sqlParams);
                //foreach(Microsoft.Data.SqlClient.SqlParameter sp in sqlParams)
                //{

                //    cmd.Parameters.Add(sp);
                //}


                var dr = cmd.ExecuteReader();
                result.AddRange(DataReaderSql(dr));
                if(dr.NextResult())
                    result.AddRange(DataReaderSql(dr));
                dr.Dispose();
                return result;
            }
        }
    }
}
