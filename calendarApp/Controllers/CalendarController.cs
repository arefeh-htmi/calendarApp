namespace CalendarApp.Controllers {
	using AutoMapper;
	using CalendarApp.Models.InputModels;
	using CalendarApp.Models.DbModels;
	using CalendarApp.Services;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[ApiController]
	[Route ("[controller]")]
	public class CalendarController : Controller {

		private readonly CalendarService calendarService;
		private readonly AuthService authService;
		private readonly IMapper mapper;
		private readonly ILogger logger;

		public CalendarController (CalendarService calendarService, AuthService authService, IMapper mapper, ILogger logger)
		{
			this.calendarService = calendarService;
			this.authService = authService;
			this.mapper = mapper;
			this.logger = logger;
		}

		[HttpGet ("All")]
		[Authorize]
		public ActionResult<Calendar []> GetAll ()
		{
			try {
				var calendars = this.calendarService.GetAll ();
				return Ok (calendars);
			} catch (Exception error) {
				logger.LogError (error.Message);
				return StatusCode (500);
			}
		}

		[HttpGet]
		[Authorize]
		public ActionResult<Calendar []> GetAllForUser ()
		{
			try {
				var userEmail = this.authService.DecodeEmailFromToken (this.Request.Headers ["Authorization"]);
				var calendars = this.calendarService.GetAllForUser (userEmail);
				return Ok (calendars);
			} catch (Exception error) {
				logger.LogError (error.Message);
				return StatusCode (500);
			}
		}

		[HttpGet ("{id}")]
		[Authorize]
		public ActionResult<Calendar> GetById (string id)
		{
			try {
				var calendarEntity = this.calendarService.GetById (id);

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
		public ActionResult<Calendar> Create (CalendarInputModel model)
		{
			try {
				if (ModelState.IsValid) {
					var mappedModel = this.mapper.Map<CalendarInputModel, Calendar> (model);

					var users = model.Users.Select (x => this.authService.GetByEmail (x));
					mappedModel.Users = (IList<ApplicationUser>)users.Select (x => new UserCalendar () { CalendarId = model.Id, UserId = x.UserId, User = x }).ToList ();

					var owner = this.authService.GetByEmail (model.Owner);

					var calendarEntity = this.calendarService.Create (mappedModel);
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
		public ActionResult<Calendar> Update (CalendarInputModel model)
		{
			try {
				if (ModelState.IsValid) {
					var mappedModel = this.mapper.Map<CalendarInputModel, Calendar> (model);
					var calendarEntity = this.calendarService.Update (mappedModel);
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
		public ActionResult<Calendar> Delete (string id)
		{
			try {
				var calendarEntity = this.calendarService.Delete (id);
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