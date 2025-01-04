using BrowerBookmariks.Model.Entitys;
using BrowerBookmariks.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public async Task<ApiResponse> bookmarks(IFormFile file)
        {
            try
            {
                await using var stream = file.OpenReadStream();
                await ImportBookmarks(stream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ApiResponse { StatusCode = 500, Message = ex.Message, Successful = false };
            }
            return new ApiResponse { Message = "初始化书签成功！" };
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

        public async Task<bool> AnalysisBookmark(string type)
        {
            //获取本机浏览器书签文件
            //C:\Users\Lenovo\AppData\Local\Google\Chrome\User Data\Default\Bookmarks
            string file = "";
            if (type == "Chrome")
                file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Google", "Chrome", "User Data", "Default", "Bookmarks");
            else if (type == "Edge")
                file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Microsoft", "Edge", "User Data", "Default", "Bookmarks");
            else
                throw new ArgumentException("不支持的浏览器类型");
            if (!File.Exists(file))
            {
                throw new FileNotFoundException("未找到浏览器书签文件");
            }

            //解析书签文件
            await using Stream stream = File.OpenRead(file);
            await ImportBookmarks(stream);

            return true;
        }

        public IPagedList<NewBookmark> GetPageList(QueryParameters query)
        {
            IQueryable<NewBookmark> data;
            if (!string.IsNullOrEmpty(query.Search))
            {
                data = _context.bookmarks
                .Where(a => a.Name.Contains(query.Search))
                .OrderByDescending(o => o.Id)
                .Select(a => new NewBookmark(a.Name, a.Url, a.Children, a.Guid, a.Id, a.classification.Name, false));
            }
            else if (query.Classid != 0 && query.Classid !=null)
            {
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
            }
            return data.ToPagedList(query.Page, query.PageSize);
        }
        
        private async Task ImportBookmarks(Stream fileStream)
        {
            // 清空现有书签和分类
            _context.bookmarks.RemoveRange(_context.bookmarks);
            _context.classifications.RemoveRange(_context.classifications);
            await _context.SaveChangesAsync();

            using StreamReader sr = new StreamReader(fileStream);
            await using JsonTextReader render = new JsonTextReader(sr);
            JObject o = (JObject)await JToken.ReadFromAsync(render);
        
            // 这是书签栏和其他书签
            var bookmarkBar = o["roots"]?["bookmark_bar"];
            var other = o["roots"]?["other"];
        
            // 解析书签
            var bookmarks01 = ParseJson(bookmarkBar).ToList();
            var bookmarks02 = ParseJson(other).ToList();
        
            // 批量添加书签
            await AddBookmarksToContext(bookmarks01);
            await AddBookmarksToContext(bookmarks02);
        }

        private async Task AddBookmarksToContext(List<Bookmark> bookmarks)
        {
            const int batchSize = 10;
            var totalBookmarks = bookmarks.Count;

            for (int i = 0; i < totalBookmarks; i += batchSize)
            {
                var batch = bookmarks.Skip(i).Take(batchSize).ToList();
                _context.bookmarks.AddRange(batch);
                await _context.SaveChangesAsync();
            }
        }
        private IEnumerable<Bookmark> ParseJson(JToken jToken)
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
    }
}
