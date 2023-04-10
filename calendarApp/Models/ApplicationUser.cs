using System.Globalization;
using Microsoft.AspNetCore.Identity;

namespace calendarApp.Models;

public class ApplicationUser : IdentityUser
{
    public virtual ICollection<Calendar>? Calendars { get; set; }
}

