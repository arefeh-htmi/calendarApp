// UserCalendar.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CalendarApp.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp.Models.DbModels {
	[PrimaryKey (nameof (UserId), nameof (CalendarId))]
	public class UserCalendar {
		[Key]
		[Column (Order = 1)]
		public string UserId { get; set; }

		[ForeignKey ("UserId")]
		public ApplicationUser User { get; set; }

		[Key]
		[Column (Order = 2)]
		public string CalendarId { get; set; }

		[ForeignKey ("CalendarId")]
		public Calendar Calendar { get; set; }
	}
}

