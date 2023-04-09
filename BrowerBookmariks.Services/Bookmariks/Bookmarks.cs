using BrowerBookmariks.Model.Entitys;
using BrowerBookmariks.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using X.PagedList;

namespace BrowerBookmariks.Model.Services
{
    public class Bookmarks : IBookmarks
    {
        private readonly MyDbContext _context;
        public Bookmarks(MyDbContext context)
        {
            _context = context;
        }
        public ApiResponse bookmarks(IFormFile file)
        {
            _context.bookmarks.RemoveRange(_context.bookmarks.ToList());
                                       _context.SaveChanges();
            List<Bookmark> list = new List<Bookmark>();
            try
            {
                using (StreamReader sr = new StreamReader(file.OpenReadStream()))
                {
                    using (JsonTextReader render = new JsonTextReader(sr))
                    {
                        JObject o = (JObject)JToken.ReadFrom(render);
                        //这是书签栏
                        var bookmark_bar = o["roots"]["bookmark_bar"];
                        //这是其他书签
                        var other = o["roots"]["other"];
                        var bookmarks_01 = ParseJson(bookmark_bar).ToList();
                        var bookmarks_02 = ParseJson(other).ToList();
                        var batchSize = 10;
                        for (int i = 0; i < bookmarks_01.Count; i += batchSize)
                        {
                            var batch = bookmarks_01.Skip(i).Take(batchSize);
                            _context.bookmarks.AddRange(batch);
                            _context.SaveChanges();
                        }
                        for (int i = 0; i < bookmarks_02.Count; i += batchSize)
                        {
                            var batch = bookmarks_02.Skip(i).Take(batchSize);
                            _context.bookmarks.AddRange(batch);
                            _context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ApiResponse { StatusCode = 500, Message = ex.Message, Successful = false };
            }
            return new ApiResponse { Message = "初始化书签成功！" };
        }
        public IEnumerable<Bookmark> ParseJson(JToken jToken)
        {
            var classifications = _context.classifications.ToDictionary(c => c.Name, c => c.Id);
            if (jToken["type"].ToString() == "folder")
            {
                string name = jToken["name"].ToString();
                int cid;
                if (!classifications.TryGetValue(name, out cid))
                {
                    var c = new Classification { Name = name };
                    _context.classifications.Add(c);
                    _context.SaveChanges();
                    cid = c.Id;
                    classifications.Add(name, cid);
                }

                foreach (var item2 in jToken["children"])
                {
                    if (item2["children"] != null)
                    {
                        foreach (var bookmark in ParseJson(item2))
                        {
                            bookmark.Children = 2;
                            yield return bookmark;
                        }
                    }
                    else
                    {
                        var bookmark = new Bookmark
                        {
                            Id = Convert.ToInt32(item2["id"]),
                            Guid = item2["guid"].ToString(),
                            Name = item2["name"].ToString(),
                            Url = item2["url"].ToString(),
                            Children = 1,
                            classificationid = cid,
                        };
                        yield return bookmark;
                    }
                }
            }
        }
        public List<NewBookmark> GetAll()
        {
            var b = _context.bookmarks.Join(_context.classifications, a => a.classificationid, g => g.Id, (a, g) => new NewBookmark(a.Name, a.Url, a.Children, a.Guid, a.Id, g.Name, false)).ToList();
            return b;
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.bookmarks.CountAsync();
        }

        public IPagedList<NewBookmark> GetPageList(QueryParameters query)
        {
            IQueryable<NewBookmark> data;
            if (!string.IsNullOrEmpty(query.Search))
            {
                // data = _context.bookmarks
                //.Where(a => a.Name.Contains(query.Search))
                //.Join(_context.classifications, a => a.classificationid, g => g.Id, (a, g) => new NewBookmark(a.Name, a.Url, a.Children, a.Guid, a.Id, g.Name,false));
                data = _context.bookmarks
                .Where(a => a.Name.Contains(query.Search))
                .OrderByDescending(o => o.Id)
                .Select(a => new NewBookmark(a.Name, a.Url, a.Children, a.Guid, a.Id, a.classification.Name, false));
            }
            else if (query.Classid != 0 && query.Classid !=null)
            {
                //  data = _context.bookmarks
                //.Where(a => a.classificationid == query.Classid)
                //.Join(_context.classifications, a => a.classificationid, g => g.Id, (a, g) => new NewBookmark(a.Name, a.Url, a.Children, a.Guid, a.Id, g.Name, false));
                data = _context.bookmarks
                .Where(a => a.classificationid == query.Classid)
                .OrderByDescending(o => o.Id)
                .Select(a => new NewBookmark(a.Name, a.Url, a.Children, a.Guid, a.Id, a.classification.Name, false));
            }
            else
            {
                data = _context.bookmarks
                .OrderByDescending(o => o.Id)
                .Select(a => new NewBookmark(a.Name, a.Url, a.Children, a.Guid, a.Id, a.classification.Name, false));
                //  data = _context.bookmarks
                //.Join(_context.classifications, a => a.classificationid, g => g.Id, (a, g) => new NewBookmark(a.Name, a.Url, a.Children, a.Guid, a.Id, g.Name,false));
            }
            return data.ToPagedList(query.Page, query.PageSize);

        }
    }
}
