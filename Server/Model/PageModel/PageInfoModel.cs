using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.PageModel
{
    public class PageInfoModel
    {
        public PageInfoModel(int? currentPage, int? pageCount)
        {
            //如果沒有分頁資訊 , 給予初始值
            CurrentPage = currentPage ?? 0;
            PageCount = pageCount ?? 5;
        }

        /// <summary>
        /// 總頁數
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// 當前頁數
        /// </summary>
        public int? CurrentPage { get; set; }

        /// <summary>
        /// 每頁幾筆
        /// </summary>
        public int? PageCount { get; set; }

        /// <summary>
        /// 計算分頁
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> ComputePage<T>(List<T> datas)
        {
            //PageCount先轉double相除在無條件進位
            TotalPage = Convert.ToInt32(Math.Ceiling(datas.Count / Convert.ToDouble(PageCount)));

            return datas.Skip(CurrentPage.Value * PageCount.Value).Take(PageCount.Value).ToList();

        }
    }
}
