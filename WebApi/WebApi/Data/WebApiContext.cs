using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class WebApiContext : DbContext
    {
        public WebApiContext (DbContextOptions<WebApiContext> options)
            : base(options)
        {
        }

        public DbSet<WebApi.Models.Resource> Resources { get; set; }

        public DbSet<WebApi.Models.Input> Inputs { get; set; }

        public DbSet<WebApi.Models.Output> Outputs { get; set; }
    }
}
