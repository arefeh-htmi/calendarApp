using System;
using CalendarApp.Models.InputModels;
using CalendarApp.Services;
using CalendarApp.Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRApplication.Models.InputModels;

namespace calendarApp.Controllers {
	[ApiController]
	[Route ("[controller]")]
	public class UserController : Controller {
		private readonly ILogger logger;
		private readonly AuthService authService;

		public UserController (ILogger logger, AuthService authService)
		{
			this.authService = authService;
			this.logger = logger;
		}

		[Authorize]
		[HttpGet]
		public ActionResult<ApplicationUser []> GetAll ()
		{
			try {
				var users = this.authService.GetAll ();
				return Ok (users);
			} catch (Exception error) {
				logger.LogError (error.Message);
				return StatusCode (500);
			}
		}

		[Authorize]
		[HttpPost ("Role")]
		public ActionResult<ApplicationUser> ChangeRole (UserChangeRoleInputModel model)
		{
			try {
				var user = this.authService.ChangeRole (model.Email, model.Role);
				return Ok (user);
			} catch (Exception error) {
				logger.LogError (error.Message);
				return StatusCode (500);
			}
		}
	}
}

