using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public class DataContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        
        private readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseNpgsql(GetConnectionString("DATABASE_URL"));
        }

        /// <summary>
        /// Formats postgres URI to ADO.NET connection string
        /// </summary>
        private string GetConnectionString(string name)
        {
            var environmentVariable = Environment.GetEnvironmentVariable(name);

            if (!Uri.TryCreate(environmentVariable, UriKind.Absolute, out Uri databaseUri))
            {
                throw new ArgumentException(name);
            }

            return $"User ID={databaseUri.UserInfo.Split(':')[0]};Password={databaseUri.UserInfo.Split(':')[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={databaseUri.LocalPath.Substring(1)};SSL Mode=Require;Trust Server Certificate=true";
        }

    }
}