
using Model.Migrations;
using Service.Interfaces.IFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class FileService: ILineItemFileService
    {
        private async Task<string> ReadCsvFile(string filePath)
        {
            using (var sr = new StreamReader(filePath))
            {
                return await sr.ReadToEndAsync();
            }
        }

        /// <summary>
        /// 讀取LineItem檔案資料
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<List<LineItem>> ReadLineItem(string filePath)
        {
            var dataStr = await ReadCsvFile(filePath);

             var dataLines = dataStr.Split("\n");

            //取得第一行colum , 資料位置用colum去算
            var colums = dataLines.FirstOrDefault().Split(',');

            List<LineItem> lineItems = new List<LineItem>();

            foreach (var dataLine in dataLines.Skip(1))
            {
                var datas = dataLine.Split(',');

                if(datas.Length <= 1)
                {
                    continue;
                }

                //var dataLineItemId = datas[Array.IndexOf(colums, "identity/LineItemId")];
                var dataPayerAccountId = datas[Array.IndexOf(colums, "bill/PayerAccountId")];
                var dataUnblendedCost = datas[Array.IndexOf(colums, "lineItem/UnblendedCost")];
                var dataUnblendedRate = datas[Array.IndexOf(colums, "lineItem/UnblendedRate")];
                var dataUsageAccountId = datas[Array.IndexOf(colums, "lineItem/UsageAccountId")];
                var dataUsageAmount = datas[Array.IndexOf(colums, "lineItem/UsageAmount")];
                var dataUsageStartDate = datas[Array.IndexOf(colums, "lineItem/UsageStartDate")];
                var dataUsageEndDate = datas[Array.IndexOf(colums, "lineItem/UsageEndDate")];
                var dataProductName = datas[Array.IndexOf(colums, "product/ProductName")];


                float? unblendedCost = null;
                //這裡不用TryPares 因為DB結構為float? 因此這樣寫比較簡單
                try { unblendedCost = float.Parse(dataUnblendedCost); } catch { };

                float? unblendedRate = null;
                try { unblendedRate = float.Parse(dataUnblendedRate); } catch { };

                lineItems.Add(
                    new LineItem {
                        //LineItemId = dataLineItemId,
                        PayerAccountId = Convert.ToInt64(dataPayerAccountId),
                        UnblendedCost = unblendedCost,
                        UnblendedRate = unblendedRate,
                        UsageAccountId = Convert.ToInt64(dataUsageAccountId),
                        UsageAmount = float.Parse(dataUsageAmount),
                        UsageStartDate = Convert.ToDateTime(dataUsageStartDate),
                        UsageEndDate = Convert.ToDateTime(dataUsageEndDate),
                        ProductName = dataProductName

                    }
                );
            }

            return lineItems;
        }

    }


}
