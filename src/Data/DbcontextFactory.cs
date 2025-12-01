using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DbcontextFactory : IDesignTimeDbContextFactory<IDbContext>
    {
        public IDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json")
                  .Build();
            var builder = new DbContextOptionsBuilder<IDbContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            return new IDbContext(builder.Options);
        }
    }
}
