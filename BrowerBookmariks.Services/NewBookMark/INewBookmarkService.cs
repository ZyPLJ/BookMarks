using BrowerBookmariks.Model.Entitys;
using BrowerBookmariks.Model.Services;
using BrowerBookmariks.Services.NewBookMark.req;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowerBookmariks.Services.NewBookMark
{
    public interface INewBookmarkService
    {
        //添加
        Task<ApiResponse> addNewBookAsync(Newuidbook newbookmark);
        //查询
        Task<List<Newbookmark>> getAllNewBookmarksAsync(string PathId);
        //删除
        Task<ApiResponse> delNewBookAsync(int Id);
    }
}
