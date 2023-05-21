// CalendarService.cs
using System;
using CalendarApp.Data;
using CalendarApp.Helpers;
using CalendarApp.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp.Services {
	public class CalendarService {
		private readonly ApplicationDbContext dataContext;

		public CalendarService (ApplicationDbContext dataContext)
		{
			this.dataContext = dataContext;
		}

		public Calendar [] GetAll ()
		{
			return this.dataContext.Calendars
					       .Include (cal => cal.Owner)
					       .ToArray ();
		}

		public Calendar [] GetAllForUser (string email)
		{
			var user = this.dataContext.Users
						   .Include (cal => cal.Calendars)
						   .FirstOrDefault (user => user.Email == email);

			return this.dataContext.Calendars
					       .Include (cal => cal.Owner)
					       .Where (e => e.Owner.UserId == user.UserId)
					       .ToArray ();
		}

		public Calendar? GetById (string id)
		{
			return this.dataContext.Calendars
					       .Include (cal => cal.Owner)
					       .FirstOrDefault (c => c.CalendarId == id);
		}

		public Calendar Create (Calendar model)
		{
			var id = IdGenerator.CreateLetterId (6);
			var existWithId = this.GetById (id);
			while (existWithId != null) {
				id = IdGenerator.CreateLetterId (6);
				existWithId = this.GetById (id);
			}
			model.CalendarId = id;

			var eventEntity = this.dataContext.Calendars.Add (model);
			this.dataContext.SaveChanges ();

			return eventEntity.Entity;
		}

		public Calendar? Update (Calendar model)
		{
			var CalendarEntity = this.dataContext.Calendars
							  .Include (cal => cal.Users)
							  .FirstOrDefault (c => c.CalendarId == model.CalendarId);
			if (CalendarEntity != null) {
				CalendarEntity.Title = model.Title != null ? model.Title : CalendarEntity.Title;
				CalendarEntity.SubTitle = model.SubTitle != null ? model.SubTitle : CalendarEntity.SubTitle;
				CalendarEntity.Events = model.Events?.Count! > 0 ? model.Events : CalendarEntity.Events;
				CalendarEntity.Users = model.Users?.Count! > 0 ? model.Users : CalendarEntity.Users;

				this.dataContext.SaveChanges ();
			}

			return CalendarEntity;
		}

		public Calendar? Delete (string id)
		{
			//TODo apply restriction for deletion and use a better error handling strategy, for example 'user can not delete a calendar if the calendar has events...
			var CalendarEntity = this.GetById (id);
			if (CalendarEntity != null) {
				this.dataContext.Calendars.Remove (CalendarEntity);
				this.dataContext.SaveChanges ();
			}

			return CalendarEntity;
		}
	}
}