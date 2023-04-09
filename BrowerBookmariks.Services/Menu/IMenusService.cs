using BrowerBookmariks.Model.Entitys;
using BrowerBookmariks.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowerBookmariks.Services.Menu
{
    public interface IMenusService
    {
        //添加菜单
        Task<ApiResponse> AddMenusAsync(Menus menus);
        //查询菜单
        Task<List<Menus>> GetMenusAsync();
        //删除菜单
        Task<ApiResponse> DeleteMenusAsync(int Id);
    }
}
