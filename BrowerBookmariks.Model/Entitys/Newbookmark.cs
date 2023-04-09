using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowerBookmariks.Model.Entitys
{
    public class Newbookmark
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int MenusId { get; set; }
        public Menus? Menus { get; set; }
    }
}
