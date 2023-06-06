// EventService.cs
using System;
using CalendarApp.Data;
using CalendarApp.Helpers;
using CalendarApp.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp.Services {
	public class EventService {
		private readonly ApplicationDbContext dataContext;

		public EventService (ApplicationDbContext dataContext)
		{
			this.dataContext = dataContext;
		}

		public Event [] GetAll ()
		{
			return this.dataContext.Events
					       .Include (cal => cal.Creator)
					       .ToArray ();
		}

		public Event [] GetAllForCalendarUser (string email)
		{
			return this.dataContext.Events
					       .Include (cal => cal.Creator)
					       .Where (e => e.Creator.Email == email)
					       .ToArray ();
		}

		public Event? GetById (string id)
		{
			return this.dataContext.Events
					       .Include (cal => cal.Creator)
					       .FirstOrDefault (c => c.Id == id);
		}

		public Event Create (Event model)
		{
			var id = IdGenerator.CreateLetterId (6);
			var existWithId = this.GetById (id);
			while (existWithId != null) {
				id = IdGenerator.CreateLetterId (6);
				existWithId = this.GetById (id);
			}
			model.Id = id;

			var eventEntity = this.dataContext.Events.Add (model);
			this.dataContext.SaveChanges ();

			return eventEntity.Entity;
		}

		public Event? Update (Event model)
		{
			var EventEntity = this.dataContext.Events
							  .Include (cal => cal.Users)
							  .FirstOrDefault (c => c.Id == model.Id);
			if (EventEntity != null) {
				EventEntity.Name = model.Name != null ? model.Name : EventEntity.Name;
				EventEntity.Description = model.Description != null ? model.Description : EventEntity.Description;
				EventEntity.StartTime = model.StartTime != null ? model.StartTime : EventEntity.StartTime;
				EventEntity.EndTime = model.EndTime != null ? model.EndTime : EventEntity.EndTime;
				EventEntity.Users = model.Users?.Count! > 0 ? model.Users : EventEntity.Users;

				this.dataContext.SaveChanges ();
			}

			return EventEntity;
		}

		public Event? Delete (string id)
		{
			var EventEntity = this.GetById (id);
			if (EventEntity != null) {
				this.dataContext.Events.Remove (EventEntity);
				this.dataContext.SaveChanges ();
			}

			return EventEntity;
		}
	}
}


