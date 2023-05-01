using MetasploitIntegration.Models;
using Microsoft.EntityFrameworkCore;
using Environment = MetasploitIntegration.Models.Environment;

namespace MetasploitIntegration.Util
{
	public class AppDataContext : DbContext
	{
		public DbSet<Environment> Environments { get; set; }
		public DbSet<Resource> Resources { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql("server=188.120.236.182;user=external;password=superFinashka;database=MsfIntegration;", new MySqlServerVersion(new Version(8, 0, 11)));
		}
	}
}
