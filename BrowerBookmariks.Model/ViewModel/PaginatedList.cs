using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrowerBookmariks.Model.ViewModel
{
    public class PaginatedList<T>:List<T>
    {
        /// <summary>
        /// 页面索引 当前页码
        /// </summary>
        public int PageIndex { get; private set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; private set; }
        /// <summary>
        /// 当前页面大小
        /// </summary>
        public int pageSize { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            this.pageSize = pageSize;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
        /// <summary>
        /// 是第一页
        /// </summary>
        public bool isFirstPage
        {
            get
            {
                return PageIndex == 1;
            }
        }
        /// <summary>
        /// 是最后一页
        /// </summary>
        public bool isLastPage
        {
            get
            {
                return PageIndex == TotalPages;
            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }  
}
