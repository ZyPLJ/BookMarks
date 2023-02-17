using BrowerBookmariks.Model.Services;
using BrowerBookmariks.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BrowerBookmariks.Services.Response
{
    public class ApiResponsePaged<T> : ApiResponse<List<T>> where T : class
    {
        public ApiResponsePaged()
        {
        }

        public ApiResponsePaged(IPagedList<T> pagedList)
        {
            Data = pagedList.ToList();
            Pagination=pagedList.ToPaginationMetadata();
        }
        public PaginationMetadata? Pagination { get; set; }
    }
}
