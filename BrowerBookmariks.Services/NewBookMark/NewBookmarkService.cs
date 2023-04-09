using BrowerBookmariks.Model;
using BrowerBookmariks.Model.Entitys;
using BrowerBookmariks.Model.Services;
using BrowerBookmariks.Services.NewBookMark.req;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowerBookmariks.Services.NewBookMark
{
    public class NewBookmarkService : INewBookmarkService
    {
        private readonly MyDbContext _dbContext;
        public NewBookmarkService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> addNewBookAsync(Newuidbook newbookmark)
        {
            var menuid = await _dbContext.Menus.FirstOrDefaultAsync(m => m.Uid == newbookmark.Uid);
            Newbookmark nb = new Newbookmark()
            {
                MenusId = menuid.Id,
                Name = newbookmark.Name,
                Url = newbookmark.Url,
            };
            try
            {
                await _dbContext.Newbookmark.AddAsync(nb);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                return new ApiResponse() { Message = e.Message };
            }
            await _dbContext.SaveChangesAsync();
            return new ApiResponse() { Message = "添加成功！" };
        }

        public async Task<ApiResponse> delNewBookAsync(int Id)
        {
            try
            {
                var newbookmark = await _dbContext.Newbookmark.FindAsync(Id);
                if(newbookmark == null)
                {
                    return new ApiResponse() { Message = $"新书签不存在！",StatusCode=201};
                }
                _dbContext.Newbookmark.Remove(newbookmark);
                await _dbContext.SaveChangesAsync();
                return new ApiResponse() { Message = $"删除成功！"};
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return new ApiResponse() { Message = $"删除失败！{ex.Message}", StatusCode = 500 };
            }
        }

        public async Task<List<Newbookmark>> getAllNewBookmarksAsync(string PathId)
        {
            int? menuId = await _dbContext.Menus
                                          .Where(m => m.Uid == PathId)
                                          .Select(m => (int?)m.Id)
                                          .FirstOrDefaultAsync();

            if (menuId.HasValue)
            {
                return await _dbContext.Newbookmark
                                       .Where(n => n.MenusId == menuId.Value)
                                       .ToListAsync();
            }

            return new List<Newbookmark>();
        }
    }
}
