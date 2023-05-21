using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp.Models.DbModels {
	public class Calendar {
		[Key]
		public string CalendarId { get; set; }
		public string? Title { get; set; }
		public string? SubTitle { get; set; }

		// Relational Data
		[Required]
		[ForeignKey ("UserId")]
		public virtual ApplicationUser Owner { get; }

		public virtual IList<Event> Events { get; set; } = new List<Event> ();
		public virtual IList<ApplicationUser> Users { get; set; } = new List<ApplicationUser> ();

	}
}

