using BrowerBookmariks.Model;
using BrowerBookmariks.Model.Entitys;
using BrowerBookmariks.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowerBookmariks.Services.Classifications
{
    public class Classifications : IClassifications
    {
        private readonly MyDbContext _dbContext;
        public Classifications(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Classification> GetAll()
        {
            return _dbContext.classifications.ToList();
        }
    }
}
