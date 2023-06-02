using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.PageModel;
using Model.ViewModel;
using Service.Interfaces.ILineItem;

namespace WebApi.Controllers
{
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// UnblendedCost報告
        /// </summary>
        /// <param name="usageAccountId"></param>
        /// <param name="currentPage">當前頁數</param>
        /// <param name="pageCount">每頁幾筆</param>
        /// <returns></returns>
        [HttpGet, Route("api/Report/UnblendedCostReport/{usageAccountId}")]
        public async Task<ApiResModel> UnblendedCostReport(long usageAccountId, int? currentPage, int? pageCount)
        {
            ApiResModel apiResModel = new ApiResModel();
            ///////////
            var lineItems = await _reportService.UnblendedCostReport(usageAccountId);

            var pageInfo = new PageInfoModel(currentPage, pageCount);

            lineItems = pageInfo.ComputePage(lineItems);

            apiResModel.PageInfo = pageInfo;
            apiResModel.Result = lineItems;

            return apiResModel;
        }

        /// <summary>
        /// UsageAmountDaily報告
        /// </summary>
        /// <param name="usageAccountId"></param>
        /// <param name="currentPage">當前頁數</param>
        /// <param name="pageCount">每頁幾筆</param>
        /// <returns></returns>
        [HttpGet, Route("api/Report/UsageAmountDailyReport/{usageAccountId}")]
        public async Task<ApiResModel> UsageAmountDailyReport(long usageAccountId, int? currentPage, int? pageCount)
        {
            ApiResModel apiResModel = new ApiResModel();

            var lineItems = await _reportService.UsageAmountDailyReport(usageAccountId);

            var pageInfo = new PageInfoModel(currentPage, pageCount);

            lineItems = pageInfo.ComputePage(lineItems);

            apiResModel.PageInfo = pageInfo;
            apiResModel.Result = lineItems;

            return apiResModel;
        }
    }
}