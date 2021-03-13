using Model.Migrations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.IFile
{
    public interface ILineItemFileService
    {
        /// <summary>
        /// 讀取LineItem檔案資料
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task<List<LineItem>> ReadLineItem(string filePath);
    }
}
