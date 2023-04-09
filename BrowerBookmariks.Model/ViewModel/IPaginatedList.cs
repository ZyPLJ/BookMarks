using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowerBookmariks.Model.ViewModel
{
    public interface IPaginatedList
    {
        /// <summary>
        /// 页面索引 当前页码
        /// </summary>
        public int PageIndex { get; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; }
        /// <summary>
        /// 当前页面大小
        /// </summary>
        public int pageSize { get; }
        public bool HasPreviousPage { get; }
        public bool HasNextPagePp { get; }
        /// <summary>
        /// 是第一页
        /// </summary>
        public bool isFirstPage { get; }
        /// <summary>
        /// 是最后一页
        /// </summary>
        public bool isLastPage { get; }
    }
}
