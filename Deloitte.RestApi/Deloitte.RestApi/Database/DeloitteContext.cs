using Deloitte.RestApi.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Deloitte.RestApi.Database
{
    public class DeloitteContext : DbContext
    {
        public DeloitteContext(DbContextOptions<DeloitteContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
    }
}