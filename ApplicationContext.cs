
using Microsoft.EntityFrameworkCore;

namespace AutoMapper.EntityFramework.Example
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            :base(options)
        {
            
        }

        public DbSet<NameEntity> Names { get; set; }
    }
}