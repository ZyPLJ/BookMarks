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
        public async Task<ApiResponse> BookMarks(IFormFile file)
        {
            return await _bookmarks.bookmarks(file);
        }

        [HttpGet("{type}")]
        public async Task<ApiResponse> AnalysisBookmark(string type)
        {
            try
            {
                var result = await _bookmarks.AnalysisBookmark(type);
                if (result)
                {
                    return new ApiResponse { Message = "解析成功！" };
                }

                return new ApiResponse { Message = "解析失败！" };
            }
            catch (Exception e)
            {
                return new ApiResponse { Message = e.Message };
            }
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
