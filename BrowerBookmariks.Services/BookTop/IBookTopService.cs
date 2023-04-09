using BrowerBookmariks.Model.Entitys;
using BrowerBookmariks.Model.ViewModel;
using BrowerBookmariks.Services.BookTop.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowerBookmariks.Services.BookTop
{
    public interface IBookTopService
    {
        //设置置顶
        Task<TopResponse> setTopAsync(int id);
        //取消置顶
        Task<TopResponse> cancelTopAsync(int id);
        //查询置顶书签
        List<NewBookmark> GetAllTop();
    }
}
