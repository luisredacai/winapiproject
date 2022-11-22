using ApiProject2.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiProject2.Context
{
    public class CrmContext:DbContext
    {
        public CrmContext(DbContextOptions<CrmContext> options) : base(options)
        {
        }
        //public DbSet<blacklist_on> blacklist_on { get; set; }
    }
}
