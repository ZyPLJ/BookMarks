using BrowerBookmariks.Model.Entitys;
using BrowerBookmariks.Model.Services;
using BrowerBookmariks.Model.ViewModel;
using BrowerBookmariks.Services.BookTop;
using BrowerBookmariks.Services.Classifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrowserBookmarks.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GetClassController : ControllerBase
    {
        private readonly IClassifications _classifications;
        public GetClassController(IClassifications classifications)
        {
            _classifications = classifications;
        }
        [HttpGet]
        public ApiResponse<List<Classification>> GetAllClass()
        {
            return new ApiResponse<List<Classification>>() { Data = _classifications.GetAll() };
        }
    }
}
