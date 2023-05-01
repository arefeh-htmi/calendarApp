using System;
using System.ComponentModel.DataAnnotations;
namespace calendarApp.Models.InputModels {
	public class UserCalendarInputModel {

		[Required]
		public string UserId { get; set; }

		[Required]
		public string EventId { get; set; }

	}
}

