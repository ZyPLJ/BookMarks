using BrowerBookmariks.Model.Services;
using BrowerBookmariks.Model.ViewModel;
using BrowerBookmariks.Services.BookTop;
using BrowerBookmariks.Services.BookTop.Res;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrowserBookmarks.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TopBookmarksController : ControllerBase
    {
        private readonly IBookTopService _bookTopService;
        public TopBookmarksController(IBookTopService bookTopService)
        {
            _bookTopService = bookTopService;
        }
        [HttpGet]
        public ApiResponse<List<NewBookmark>> GetAllTop()
        {
            return new ApiResponse<List<NewBookmark>>() { Data = _bookTopService.GetAllTop() };
        }
        [HttpGet("{id}")]
        public async Task<ApiResponse> SetTopBookMark(int id)
        {
            var t = await _bookTopService.setTopAsync(id);
            return new ApiResponse() { Message = t.Message, Successful = t.result };
        }
        [HttpDelete("{id}")]
        public async Task<ApiResponse> CancelTopBookMark(int id)
        {
            var t = await _bookTopService.cancelTopAsync(id);
            return new ApiResponse() { Message = t.Message };
        }
    }
}
