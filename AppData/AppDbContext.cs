using Microsoft.EntityFrameworkCore;
using Wörter.AppData.Entities;

namespace Wörter.AppData;

public partial class AppDbContext : DbContext
{
	public AppDbContext()
	{
	}

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		// if (System.Diagnostics.Debugger.IsAttached == false) System.Diagnostics.Debugger.Launch();

		base.OnConfiguring(optionsBuilder);

		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlite(connectionString: App.Instance._DataConfiguration.GetSection("Database:ConnectionString").Value);
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// if (System.Diagnostics.Debugger.IsAttached == false) System.Diagnostics.Debugger.Launch();

		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
	}

	public virtual DbSet<Wort> Wörter => Set<Wort>();
	public virtual DbSet<Notiz> Notizen => Set<Notiz>();
}

