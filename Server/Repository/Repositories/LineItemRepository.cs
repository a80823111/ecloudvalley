using Dapper;
using Dapper.Contrib.Extensions;
using Model.Migrations;
using Repository.Configuration;
using Repository.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Repository.Repositories
{
    public class LineItemRepository : BaseRepository<LineItem>
    {
        public LineItemRepository(SqlServerConnection sqlServerConnection)
        {
            _conn = sqlServerConnection.Database(ConnectionStrings.SqlServer);
        }

        /// <summary>
        /// 插入大量資料 (Transaction) , 其中有一筆失敗全部復原
        /// </summary>
        /// <param name="lineItems"></param>
        public bool InsertManyTransaction(List<LineItem> lineItems)
        {
            try
            {
                using (var transaction = _conn.BeginTransaction())
                {    
                    foreach (var lineItem in lineItems)
                    {
                        _conn.Insert(lineItem, transaction);
                    }

                    transaction.Commit();

                    return true;              
                }
            }
            catch (Exception ex)
            {
                return false;
            }


        }
    }
}
