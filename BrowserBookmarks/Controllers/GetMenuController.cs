using BrowerBookmariks.Model.Entitys;
using BrowerBookmariks.Model.Services;
using BrowerBookmariks.Services.Menu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrowserBookmarks.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GetMenuController : ControllerBase
    {
        private readonly IMenusService _menusService;
        public GetMenuController(IMenusService menusService) {
            _menusService = menusService;
        }
        [HttpGet]
        public Task<List<Menus>> GetMenus()
        {
            return _menusService.GetMenusAsync();
        }
        [HttpPost]
        public Task<ApiResponse> addMenu(Menus menus)
        {
            return _menusService.AddMenusAsync(menus);
        }
        [HttpDelete("{id}")]
        public Task<ApiResponse> DelMenuAsync(int id)
        {
            return _menusService.DeleteMenusAsync(id);
        }
    }
}
