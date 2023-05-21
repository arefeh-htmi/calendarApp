using AutoMapper;
using CalendarApp.Models.InputModels;
using CalendarApp.Services;
using CalendarApp.Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalendarApp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private readonly AuthService authService;

        public AuthController(ILogger logger, AuthService authService, IMapper mapper)
        {
            this.authService = authService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult<string> Login(LoginInputModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (this.authService.IsAuthenticated(userModel.Email, userModel.Password))
                    {
                        var user = this.authService.GetByEmail(userModel.Email);
                        var token = this.authService.GenerateJwtToken(userModel.Email, user.Role);

                        return Ok(Json(token));
                    }
                    return BadRequest("Email or password are not correct!");
                }

                return BadRequest(ModelState);
            }
            catch (Exception error)
            {
                logger.LogError(error.Message);
                return StatusCode(500);
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult<string> Register(RegisterInputModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (userModel.Password != userModel.ConfirmedPassword)
                    {
                        return BadRequest("Passwords does not match!");
                    }

                    if (this.authService.DoesApplicationUserExists(userModel.Email))
                    {
                        return BadRequest("User already exists!");
                    }

                    var mappedModel = this.mapper.Map<RegisterInputModel, ApplicationUser>(userModel);
                    mappedModel.Role = "User";
                    var user = this.authService.RegisterApplicationUser(mappedModel);

                    if (user != null)
                    {
                        var token = this.authService.GenerateJwtToken(user.Email, mappedModel.Role);
                        return Ok(Json(token));

                    }

                    return BadRequest("Email or password are not correct!");
                }

                return BadRequest(ModelState);
            }
            catch (Exception error)
            {
                logger.LogError(error.Message);
                return StatusCode(500);
            }
        }
    }
}

