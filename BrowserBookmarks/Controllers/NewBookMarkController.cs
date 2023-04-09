using BrowerBookmariks.Model.Entitys;
using BrowerBookmariks.Model.Services;
using BrowerBookmariks.Services.NewBookMark;
using BrowerBookmariks.Services.NewBookMark.req;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrowserBookmarks.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewBookMarkController : ControllerBase
    {
        private readonly INewBookmarkService _newBookmarkService;
        public NewBookMarkController(INewBookmarkService newBookmarkService)
        {
            _newBookmarkService = newBookmarkService;
        }
        [HttpPost]
        public Task<ApiResponse> AddNewBook(Newuidbook newbookmark)
        {
            return _newBookmarkService.addNewBookAsync(newbookmark);
        }
        [HttpGet("{pathId}")]
        public Task<List<Newbookmark>> GetNewBook(string pathId)
        {
            return _newBookmarkService.getAllNewBookmarksAsync(pathId);
        }
        [HttpDelete("{Id}")]
        public Task<ApiResponse> DelNewBook(int Id)
        {
            return _newBookmarkService.delNewBookAsync(Id);
        }
    }
}
