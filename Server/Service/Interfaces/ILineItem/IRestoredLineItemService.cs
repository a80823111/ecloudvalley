

using Model.Migrations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.ILineItem
{
    public interface IRestoredLineItemService
    {
        /// <summary>
        /// 插入大量資料 (Transaction) , 其中有一筆失敗全部復原
        /// </summary>
        /// <param name="lineItems"></param>
        bool InsertManyLineItem(List<LineItem> lineItems);
    }
}
