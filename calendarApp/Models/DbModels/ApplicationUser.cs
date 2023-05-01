using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Microsoft.AspNetCore.Identity;

namespace CalendarApp.Models.DbModels;

[Table ("Users")]
public class ApplicationUser : IdentityUser {
	[Key]
	public string UserId { get; set; }
	public string? Email { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string? Password { get; set; }
	public string? Role { get; set; }

	// Relational Data
	public virtual IList<Calendar> Calendars { get; set; } = new List<Calendar> ();
}