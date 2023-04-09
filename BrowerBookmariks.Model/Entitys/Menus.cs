using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowerBookmariks.Model.Entitys
{
    public class Menus
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public ICollection<Newbookmark>? Newbookmark { get; set; }

    }
}
