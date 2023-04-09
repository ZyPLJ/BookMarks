using BrowerBookmariks.Model.Entitys;
using BrowerBookmariks.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BrowerBookmariks.Model.Services
{
    public interface IBookmarks
    {
        /// <summary>
        /// 初始化
        /// 给一个浏览器书签的路径
        /// </summary>
        /// <param name="path">Bookmarks文件路径</param>
        /// <returns></returns>
        ApiResponse bookmarks(IFormFile file);
        List<NewBookmark> GetAll();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IPagedList<NewBookmark> GetPageList(QueryParameters query);
        /// <summary>
        /// 返回书签个数
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync();
    }
}
