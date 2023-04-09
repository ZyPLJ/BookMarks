using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowerBookmariks.Model.ViewModel
{
    public class QueryParameters
    {
        /// <summary>
        /// 最大页面条目
        /// </summary>
        public const int MaxPageSize = 50;
        private int _pageSize = 10;
        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; } = 1;
        /// <summary>
        /// 搜索关键词
        /// </summary>
        public string? Search { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string? SortBy { get; set; }
        /// <summary>
        /// 分级筛选id
        /// </summary>
        public int? Classid { get; set; }
    }
}
