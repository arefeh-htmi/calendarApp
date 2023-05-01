using AutoMapper;
using CalendarApp.Models.InputModels;
using CalendarApp.Models.DbModels;
using CalendarApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CalendarApp.Models.InputModels;

namespace CalendarApp.Controllers {
	[ApiController]
	[Route ("[controller]")]
	public class EventController : Controller {

		private readonly EventService eventService;
		private readonly AuthService authService;
		private readonly IMapper mapper;
		private readonly ILogger logger;

		public EventController (EventService eventService, AuthService authService, IMapper mapper, ILogger logger)
		{
			this.eventService = eventService;
			this.authService = authService;
			this.mapper = mapper;
			this.logger = logger;
		}

		[HttpGet ("All")]
		[Authorize]
		public ActionResult<Event []> GetAll ()
		{
			try {
				var calendars = this.eventService.GetAll ();
				return Ok (calendars);
			} catch (Exception error) {
				logger.LogError (error.Message);
				return StatusCode (500);
			}
		}

		[HttpGet]
		[Authorize]
		public ActionResult<Event []> GetAllForCalendarUser ()
		{
			try {
				var userEmail = this.authService.DecodeEmailFromToken (this.Request.Headers ["Authorization"]);
				var calendars = this.eventService.GetAllForCalendarUser (userEmail);
				return Ok (calendars);
			} catch (Exception error) {
				logger.LogError (error.Message);
				return StatusCode (500);
			}
		}

		[HttpGet ("{id}")]
		[Authorize]
		public ActionResult<Event> GetById (string id)
		{
			try {
				var calendarEntity = this.eventService.GetById (id);

				if (calendarEntity != null) {
					return Ok (calendarEntity);
				}

				return NotFound ();
			} catch (Exception error) {
				logger.LogError (error.Message);
				return StatusCode (500);
			}
		}

		[HttpPost]
		[Authorize (Roles = "Administrator")]
		public ActionResult<Event> Create (CalendarInputModel model)
		{
			try {
				if (ModelState.IsValid) {
					var mappedModel = this.mapper.Map<CalendarInputModel, Event> (model);

					var users = model.Users.Select (x => this.authService.GetByEmail (x));
					mappedModel.Users = (IList<ApplicationUser>)users;

					var owner = this.authService.GetByEmail (model.Owner);

					var calendarEntity = this.eventService.Create (mappedModel);
					if (calendarEntity != null) {
						return Ok (calendarEntity);
					}
				}

				return BadRequest (ModelState);
			} catch (Exception error) {
				logger.LogError (error.Message);
				return StatusCode (500);
			}
		}

		[HttpPut]
		[Authorize (Roles = "Administrator")]
		public ActionResult<Event> Update (CalendarInputModel model)
		{
			try {
				if (ModelState.IsValid) {
					var mappedModel = this.mapper.Map<CalendarInputModel, Event> (model);
					var calendarEntity = this.eventService.Update (mappedModel);
					if (calendarEntity != null) {
						return Ok (calendarEntity);
					}

					return NotFound ();
				}

				return BadRequest (ModelState);
			} catch (Exception error) {
				logger.LogError (error.Message);
				return StatusCode (500);
			}
		}

		[HttpDelete ("{id}")]
		[Authorize (Roles = "Administrator")]
		public ActionResult<Event> Delete (string id)
		{
			try {
				var calendarEntity = this.eventService.Delete (id);
				if (calendarEntity != null) {
					return Ok (calendarEntity);
				}

				return NotFound ();
			} catch (Exception error) {
				logger.LogError (error.Message);
				return StatusCode (500);
			}
		}
	}
}