using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BrowerBookmariks.Model
{
    public class MyDbContextDesignFac: IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MyDbContext> builder = new DbContextOptionsBuilder<MyDbContext>();
            string connStr = "Data Source=app.db;Foreign Keys=False";
            builder.UseSqlite(connStr);
            MyDbContext ctx = new MyDbContext(builder.Options);
            return ctx;
        }
    }
}
