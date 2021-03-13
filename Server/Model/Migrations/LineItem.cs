using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Migrations
{
    /// <summary>
    /// 帳單
    /// </summary>
    [Table("lineitem")]
    public class LineItem
    {
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// LineItemId , Notice 資料來源有重複的Id因此不當作主鍵
        /// </summary>
        //public string LineItemId { get; set; }

        public long PayerAccountId { get; set; }

        public float? UnblendedCost { get; set; }

        public float? UnblendedRate { get; set; }

        public long UsageAccountId { get; set; }

        public float UsageAmount { get; set; }

        public DateTime UsageStartDate { get; set; }

        public DateTime UsageEndDate { get; set; }

        public string ProductName { get; set; }

    }
}
