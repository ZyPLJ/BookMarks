using BrowerBookmariks.Model;
using BrowerBookmariks.Model.Entitys;
using BrowerBookmariks.Model.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowerBookmariks.Services.Menu
{
    public class MenusService : IMenusService
    {
        private readonly MyDbContext _myDbContext;
        public MenusService(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        public async Task<ApiResponse> AddMenusAsync(Menus menus)
        {
            try
            {
                await _myDbContext.Menus.AddAsync(menus);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                return new ApiResponse() { Message = e.Message };
            }
            await _myDbContext.SaveChangesAsync();
            return new ApiResponse() { Message = "添加成功！" };
        }

        public async Task<ApiResponse> DeleteMenusAsync(int Id)
        {
            //var menu = await _myDbContext.Menus.FindAsync(Id);
            //int pathId = Convert.ToInt32(menu.Path.Split('/')[1]);
            //var newbook = await _myDbContext.Newbookmark.Where(n=>n.pathId == pathId).ToListAsync();
            //_myDbContext.Menus.Remove(menu);
            //_myDbContext.Newbookmark.RemoveRange(newbook);
            //await _myDbContext.SaveChangesAsync();
            var menu = await _myDbContext.Menus.Include(m => m.Newbookmark).FirstOrDefaultAsync(m=>m.Id == Id);
            if (menu == null)
            {
                return new ApiResponse() { Message = "菜单不存在！",StatusCode = 201 };
            }
            _myDbContext.Menus.Remove(menu);
            if(menu.Newbookmark != null)
            {
                _myDbContext.Newbookmark.RemoveRange(menu.Newbookmark);
            }
            await _myDbContext.SaveChangesAsync();
            return new ApiResponse() { Message = "删除成功！" };
        }

        public async Task<List<Menus>> GetMenusAsync()
        {
            return await _myDbContext.Menus.OrderBy(o => o.Id).ToListAsync();
        }
    }
}
