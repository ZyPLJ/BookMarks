using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowerBookmariks.Model.Entitys
{
    public class Classification
    {
        [Key] //主键
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//自增
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Bookmark> Bookmarks { get; set; }
    }
}
