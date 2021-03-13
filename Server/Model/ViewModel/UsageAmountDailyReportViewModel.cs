using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModel
{

    public class UsageAmountDailyReportViewModel
    {
        public string ProductName { get; set; }
        public List<DailyDetailViewModel> DailyDetails { get; set; }
    }

    public class DailyDetailViewModel
    {
        public DateTime UsageStartDate { get; set; }
        public DateTime UsageEndDate { get; set; }

        public float TotalUsageAmount { get; set; }
    }
}
