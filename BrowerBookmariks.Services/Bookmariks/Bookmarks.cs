using BrowerBookmariks.Model.Entitys;
using BrowerBookmariks.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
        public List<Bookmark> bookmarks(string path)
        {
            _context.bookmarks.RemoveRange(_context.bookmarks.ToList());
            List<Bookmark> list = new List<Bookmark>();
            if (_context.classifications.FirstOrDefault(c => c.Name == "书签栏") == null)
            {
                _context.classifications.Add(new Classification { Name = "书签栏" });
                _context.SaveChanges();
            }
			try
			{
				using (StreamReader sr = new StreamReader(path))
				{
					using (JsonTextReader render = new JsonTextReader(sr))
					{
						JObject o = (JObject)JToken.ReadFrom(render);
						//这是书签栏
						var bookmark_bar = o["roots"]["bookmark_bar"];
						//这是其他书签
						var other = o["roots"]["other"];
                        ForE(bookmark_bar,list);
                        ForE(other,list);
                        _context.bookmarks.AddRange(list);
                        _context.SaveChanges();
                    }
				}
				Console.WriteLine("");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return list;
        }
		public void ForE(JToken jToken,List<Bookmark> list)
		{
            int cid = _context.classifications.FirstOrDefault(c => c.Name == "书签栏").Id;
            foreach (var item in jToken["children"])
            {
                //说明有书签中有文件夹
                if (item["type"].ToString() == "folder")
                {
                    Classification c = new Classification()
                    {
                        Name = item["name"].ToString()
                    };
                    var cName = _context.classifications.FirstOrDefault(c => c.Name == item["name"].ToString());
                    if (cName == null) {
                        _context.classifications.Add(c);
                        _context.SaveChanges();
                    }
                    int cid2 = _context.classifications.FirstOrDefault(c => c.Name == item["name"].ToString()).Id;
                    foreach (var item2 in item["children"])
                    {
                        Bookmark b2 = new Bookmark()
                        {
                            Id = Convert.ToInt32(item2["id"]),
                            Guid = item2["guid"].ToString(),
                            Name = item2["name"].ToString(),
                            Url = item2["url"].ToString(),
                            Children = 2,
                            classificationid = cid2
                        };
                        list.Add(b2);
                    }
                }
                else
                {
                    Bookmark b = new Bookmark()
                    {
                        Id = Convert.ToInt32(item["id"]),
                        Guid = item["guid"].ToString(),
                        Name = item["name"].ToString(),
                        Url = item["url"].ToString(),
                        Children = 1,
                        classificationid = cid
                    };
                    list.Add(b);
                }
            }
        }

        public List<NewBookmark> GetAll()
        {
            var b = _context.bookmarks.Join(_context.classifications, a => a.classificationid, g => g.Id, (a, g) => new NewBookmark(a.Name, a.Url, a.Children, a.Guid, a.Id, g.Name,false)).ToList();
            return b;
        }

        public IPagedList<NewBookmark> GetPageList(QueryParameters query)
        {
            IQueryable<NewBookmark> data;
            if (!string.IsNullOrEmpty(query.Search))
            {
                data = _context.bookmarks
               .Where(a => a.Name.Contains(query.Search))
               .Join(_context.classifications, a => a.classificationid, g => g.Id, (a, g) => new NewBookmark(a.Name, a.Url, a.Children, a.Guid, a.Id, g.Name,false));
            }
            else
            {
                data = _context.bookmarks
              .Join(_context.classifications, a => a.classificationid, g => g.Id, (a, g) => new NewBookmark(a.Name, a.Url, a.Children, a.Guid, a.Id, g.Name,false));
            }   
            return data.ToPagedList(query.Page,query.PageSize);

        }
    }
}
