using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using X.PagedList;

namespace BrowerBookmariks.Model.ViewModel
{
    public static class PagedListExt
    {
        public static PaginationMetadata ToPaginationMetadata(this IPagedList page)
        {
            return new PaginationMetadata
            {
                PageCount = page.PageCount,
                TotalItemCount = page.TotalItemCount,
                PageNumber = page.PageNumber,
                PageSize = page.PageSize,
                HasNextPage = page.HasNextPage,
                HasPreviousPage = page.HasPreviousPage,
                IsFirstPage = page.IsFirstPage,
                IsLastPage = page.IsLastPage,
                FirstItemOnPage = page.FirstItemOnPage,
                LastItemOnPage = page.LastItemOnPage
            };
        }
        public static string ToPaginationMetadataJson(this IPagedList page)
        {
            return JsonSerializer.Serialize(ToPaginationMetadata(page));
        }
    }
}
