using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowerBookmariks.Model
{
    public class MyDbContextDesignFac: IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MyDbContext> builder = new DbContextOptionsBuilder<MyDbContext>();
            //Personalblog
            string connStr = "";
            builder.UseMySql(connStr, new MySqlServerVersion(new Version()));
            MyDbContext ctx = new MyDbContext(builder.Options);
            return ctx;
        }
    }
}
