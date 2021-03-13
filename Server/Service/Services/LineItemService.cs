
using Microsoft.Extensions.Caching.Memory;
using Model.Migrations;
using Model.ViewModel;
using Newtonsoft.Json;
using Repository.Repositories;
using Service.Interfaces.ILineItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class LineItemService: IRestoredLineItemService,IReportService
    {
        private readonly LineItemRepository _lineItemRepository;
        private readonly IMemoryCache _memoryCache;

        public LineItemService(LineItemRepository lineItemRepository, IMemoryCache memoryCache)
        {
            _lineItemRepository = lineItemRepository;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// 插入大量資料 (Transaction) , 其中有一筆失敗全部復原
        /// </summary>
        /// <param name="lineItems"></param>
        public bool InsertManyLineItem(List<LineItem> lineItems)
        {
            return _lineItemRepository.InsertManyTransaction(lineItems);
        }

        /// <summary>
        /// UnblendedCost報告
        /// </summary>
        /// <param name="usageAccountId"></param>
        /// <returns></returns>
        public async Task<List<UnblendedCostReportViewModel>> UnblendedCostReport(long usageAccountId)
        {
            var lineItems = new List<UnblendedCostReportViewModel>();

            var cacheKey = $"UnblendedCostReport_{JsonConvert.SerializeObject(usageAccountId)}";

            if (!_memoryCache.TryGetValue(cacheKey, out lineItems))
            {
                lineItems = (await _lineItemRepository.GetListAsync())
                                .Where(x => x.UsageAccountId == usageAccountId)
                                .GroupBy(x => x.ProductName)
                                .Select(x =>
                                    new UnblendedCostReportViewModel
                                    {
                                        ProductName = x.Key,
                                        TotalUnblendedCost = x.Where(y => y.UnblendedCost.HasValue).Sum(y => y.UnblendedCost) ?? 0
                                    }
                                )
                                .ToList();

                _memoryCache.Set(cacheKey, lineItems, TimeSpan.FromDays(1));
            }

            return lineItems;
        }

        /// <summary>
        /// UsageAmountDaily報告
        /// </summary>
        /// <param name="usageAccountId"></param>
        /// <returns></returns>
        public async Task<List<UsageAmountDailyReportViewModel>> UsageAmountDailyReport(long usageAccountId)
        {
            var lineItems = new List<UsageAmountDailyReportViewModel>();

            var cacheKey = $"UsageAmountDailyReport_{JsonConvert.SerializeObject(usageAccountId)}";

            if (!_memoryCache.TryGetValue(cacheKey, out lineItems))
            {
                lineItems = (await _lineItemRepository.GetListAsync())
                                .Where(x => x.UsageAccountId == usageAccountId)
                                .GroupBy(x => x.ProductName)
                                .Select(x =>
                                    new UsageAmountDailyReportViewModel
                                    {
                                        ProductName = x.Key,
                                        DailyDetails = x.ToList()
                                                       .GroupBy(y => new { UsageStartDate = y.UsageStartDate.Date, UsageEndDate = y.UsageEndDate.Date })
                                                       .Select(y => new DailyDetailViewModel {
                                                           UsageStartDate = y.Key.UsageStartDate,
                                                           UsageEndDate = y.Key.UsageEndDate,
                                                           TotalUsageAmount = y.Sum(z => z.UsageAmount)
                                                       }).ToList()
                                    }
                                )
                                .ToList();

                _memoryCache.Set(cacheKey, lineItems, TimeSpan.FromDays(1));
            }

            return lineItems;
        }



    }
}
