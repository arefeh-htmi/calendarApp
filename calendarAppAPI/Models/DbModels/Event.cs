using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarApp.Models.DbModels {
	public class Event {
		[Key]
		public string Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }

		//Relational data
		[Required]
		[ForeignKey ("LocationId")]
		public virtual Location Location { get; set; }

		[Required]
		[ForeignKey ("UserId")]
		public virtual ApplicationUser Creator { get; }
		public virtual Calendar Calendar { get; }

		public virtual IList<ApplicationUser> Users { get; set; } = new List<ApplicationUser> ();
	}
}