using DataBase.Data.Entities;
using DataBase.Data.Entities.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DataBase
{
	public class DogsDBContext(DbContextOptions<DogsDBContext> options) : DbContext(options)
	{
		public DbSet<Dog> Dogs { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration<Dog>(new DogConfig());
		}

	}
}
