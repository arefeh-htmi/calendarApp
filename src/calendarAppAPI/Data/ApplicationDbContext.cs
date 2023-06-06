using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CalendarApp.Models.DbModels;

namespace CalendarApp.Data;
public class ApplicationDbContext : DbContext {

	public ApplicationDbContext (DbContextOptions options)
	 : base (options)
	{
	}

	protected override void OnModelCreating (ModelBuilder builder)
	{
		base.OnModelCreating (builder);
	}

	protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured) {
			IConfigurationRoot configuration = new ConfigurationBuilder ()
			   .SetBasePath (Directory.GetCurrentDirectory ())
			   .AddJsonFile ("appsettings.json")
			   .Build ();
		}
	}
	public DbSet<ApplicationUser> Users { get; set; }
	public DbSet<Calendar> Calendars { get; set; }
	public DbSet<UserCalendar> UserCalendar { get; set; }
	public DbSet<Event> Events { get; set; }
	public DbSet<Location> Locations { get; set; }
}

