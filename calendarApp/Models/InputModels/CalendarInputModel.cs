
using CalendarApp.Models.DbModels;

namespace CalendarApp.Models.InputModels {
	public class CalendarInputModel {
		public string Id { get; set; }
		public string? Title { get; set; }
		public string? SubTitle { get; set; }
		public IList<string>? Events { get; set; }
		public string? Owner { get; set; }
		public IList<string>? Users { get; set; }
	}
}