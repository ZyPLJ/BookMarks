using BrowerBookmariks.Model;
using BrowerBookmariks.Model.Entitys;
using BrowerBookmariks.Model.ViewModel;
using BrowerBookmariks.Services.BookTop.Res;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowerBookmariks.Services.BookTop
{
    public class BookTopService : IBookTopService
    {
        private readonly MyDbContext _myDbContext;
        public BookTopService(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<TopResponse> setTopAsync(int id)
        {
            //查询置顶是否到达12个
            if(await _myDbContext.bookTops.CountAsync() >= 12)
            {
                return new TopResponse{ Message="置顶数已满！！！",result=false};
            }
            //先查询是否已经置顶
            if (await _myDbContext.bookTops.FirstOrDefaultAsync(b => b.BookmarikId == id) == null)
            {
                await _myDbContext.bookTops.AddAsync(new Model.Entitys.BookTop()
                {
                    BookmarikId = id,
                });
                try
                {
                    await _myDbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return new TopResponse { Message=ex.Message,result=false};
                    throw;
                }
                return new TopResponse { Message="置顶成功！",result=true};
            }
            else
            {
                return new TopResponse { Message="该书签已经置顶了！",result=false};
            }

        }

        public async Task<TopResponse> cancelTopAsync(int id)
        {
            //先查询是否已经置顶
            BrowerBookmariks.Model.Entitys.BookTop b = await _myDbContext.bookTops.FirstOrDefaultAsync(b => b.BookmarikId == id);
            if (b != null)
            {
                _myDbContext.bookTops.Remove(b);
                try
                {
                    await _myDbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return new TopResponse { Message = ex.Message, result = false };
                    throw;
                }
                return new TopResponse { Message = "取消置顶成功！", result = true };
            }
            else
            {
                return new TopResponse { Message = "该书签不存在！", result = false };
            }
        }

        public List<NewBookmark> GetAllTop()
        {
            return _myDbContext.bookmarks
                .Join(_myDbContext.classifications, a => a.classificationid, 
                g => g.Id, (a, g) => new {a,g})
                .Join(_myDbContext.bookTops,a=>a.a.Id,c=>c.BookmarikId,
                (a,c)=> 
                new NewBookmark(a.a.Name,a.a.Url,a.a.Children,a.a.Guid,a.a.Id,a.g.Name,true)).ToList();
        }
    }
}
