using BrowerBookmariks.Model.Entitys;
using BrowerBookmariks.Model.Services;
using BrowerBookmariks.Model.ViewModel;
using BrowerBookmariks.Services.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace BrowserBookmarks.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GetBookmarksController : ControllerBase
    {
        private readonly IBookmarks _bookmarks;
        public GetBookmarksController(IBookmarks bookmarks)
        {
            _bookmarks = bookmarks;
        }
        [HttpPost]
        public ApiResponse BookMarks(IFormFile file)
        {
            return _bookmarks.bookmarks(file);
        }
        [ResponseCache(Duration = 30)]
        [HttpGet]
        public ApiResponsePaged<NewBookmark> GetPageList([FromQuery]QueryParameters param)
        {
            var pagedList = _bookmarks.GetPageList(param);
            return new ApiResponsePaged<NewBookmark>(pagedList)
            {
                Data = pagedList.ToList(),
                Pagination = pagedList.ToPaginationMetadata(),
            };
        }
        [HttpGet("Count")]
        public async Task<ApiResponse> GetCountAsync()
        {
            return new ApiResponse { Data = await _bookmarks.GetCountAsync() };
        }
    }
}
