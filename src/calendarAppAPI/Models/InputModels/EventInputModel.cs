
using CalendarApp.Models.DbModels;

namespace CalendarApp.Models.InputModels {
	public class EventInputModel {
		public string Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }

		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }

		public Location Location { get; set; }
		public ApplicationUser Creator { get; set; }
		public IList<string> Users { get; set; }
		public Calendar Calendar { get; set; }
	}
}