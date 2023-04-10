using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CalendarApp.Models;

namespace calendarApp.Models
{
	public class Calendar
	{
        [Key]
        public int Id { get; set; }
		public string? Title { get; set; }
        public string? SubTitle { get; set; }

        // Relational Data
        public virtual ICollection<Event>? Events { get; set; }

    }
}

