using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowerBookmariks.Model.Entitys
{
    public class BookTop
    {
        [Key] //主键
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//自增
        public int Id { get; set; }
        public int BookmarikId { get; set; }
    }
}
