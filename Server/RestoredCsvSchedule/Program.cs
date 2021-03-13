using RestoredCsvSchedule.Configuration;
using Service.Interfaces.IFile;
using Service.Interfaces.ILineItem;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace RestoredCsvSchedule
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Service DI
                ProgramLoad.CreateHostBuilder(args);
                // Load AppSettings
                ProgramLoad.LoadConfiguration();

                var _lineItemFileService = ProgramLoad.GetService<ILineItemFileService>();
                var _restoredLineItemService = ProgramLoad.GetService<IRestoredLineItemService>();

                Console.WriteLine("Read file and convert migrations start...");
                var lineItems = await _lineItemFileService.ReadLineItem(RestoredCsvScheduleSettings.FilePath);
                Console.WriteLine("Read file and convert migrations sucess");

                Console.WriteLine("Data insert to database start...");
                //我看RowData是帳單類型資料 , 因此新增資料時使用 Transaction , 避免異常情況無法追蹤檔案狀況
                _restoredLineItemService.InsertManyLineItem(lineItems);
                Console.WriteLine("Data insert  to database sucess");

                Console.WriteLine("Press any key than exit process");

            }
            catch(Exception e)
            {
                Console.WriteLine($"Exception error : {e.Message}");
                Console.WriteLine("Press any key than exit process");
            }
            Console.ReadLine();
        }
    }
}
