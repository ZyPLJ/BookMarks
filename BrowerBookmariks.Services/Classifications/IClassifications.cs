using BrowerBookmariks.Model.Entitys;
using BrowerBookmariks.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowerBookmariks.Services.Classifications
{
    public interface IClassifications
    {
        /// <summary>
        /// 返回所有分级
        /// </summary>
        /// <returns></returns>
        List<Classification> GetAll();
    }
}
