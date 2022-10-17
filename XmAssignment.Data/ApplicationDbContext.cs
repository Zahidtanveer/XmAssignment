using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using XmAssignment.Common.Entities;
using XmAssignment.Common.Enums;

namespace XmAssignment.Data
{
    public sealed class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration Configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationDbContext(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

        }

        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            if (_httpContextAccessor.HttpContext is not null)
            {
                var queryParam = _httpContextAccessor.HttpContext.Request.Query["dataStoreType"][0];
                if (queryParam is not null)
                {
                    var dataStoreType = Enum.Parse<DataStoreType>(queryParam);

                    if (DataStoreType.SQLite == dataStoreType)
                    {
                        optionsBuilder.UseSqlite(Configuration.GetConnectionString("dbconnectionSQLite"));
                    }
                    else if (DataStoreType.InMemoryDatabase == dataStoreType)
                    {
                        optionsBuilder.UseInMemoryDatabase(databaseName: "Bitcoin_db");
                    }
                    else if (DataStoreType.DockerContainerDatabase == dataStoreType)
                    {
                        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
                        optionsBuilder.UseNpgsql(connectionString);
                    }
                }
            }
            else
            {   
                // default is sqlite
                optionsBuilder.UseSqlite(Configuration.GetConnectionString("dbconnectionSQLite"));
            }


        }
        public DbSet<BtcPrice> BtcPrices { get; set; }
    }
}
