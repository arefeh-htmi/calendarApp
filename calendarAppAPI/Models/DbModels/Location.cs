using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarApp.Models.DbModels {
	public class Location {
		[Key]
		public int LocationId { get; set; }
		public string? Name { get; set; }

		//Relational data
		[Required]
		public virtual Event Event { get; set; }
	}
}