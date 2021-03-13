
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.ILineItem
{
    public interface IReportService
    {
        /// <summary>
        /// UnblendedCost報告
        /// </summary>
        /// <param name="usageAccountId"></param>
        /// <returns></returns>
        Task<List<UnblendedCostReportViewModel>> UnblendedCostReport(long usageAccountId);


        /// <summary>
        /// UsageAmountDaily報告
        /// </summary>
        /// <param name="usageAccountId"></param>
        /// <returns></returns>
        Task<List<UsageAmountDailyReportViewModel>> UsageAmountDailyReport(long usageAccountId);
    }
}
