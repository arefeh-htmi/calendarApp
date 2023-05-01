namespace CalendarApp.Services {
	using CalendarApp.Data;
	using Microsoft.IdentityModel.Tokens;
	using System.IdentityModel.Tokens.Jwt;
	using System.Security.Claims;
	using System.Text;
	using CalendarApp.Helpers;
	// We need this to use BCrypt
	using BC = BCrypt.Net.BCrypt;
	using CalendarApp.Models.DbModels;

	public class AuthService {
		private readonly ApplicationDbContext dataContext;
		private readonly IConfiguration configuration;

		public AuthService (ApplicationDbContext dataContext, IConfiguration configuration)
		{
			this.dataContext = dataContext;
			this.configuration = configuration;
		}

		// To authenticate the user
		public bool IsAuthenticated (string email, string password)
		{
			var ApplicationUser = this.GetByEmail (email);
			return this.DoesApplicationUserExists (email) && BC.Verify (password, ApplicationUser.Password);
		}

		// will verify if a user with a given email exists
		public bool DoesApplicationUserExists (string email)
		{
			var ApplicationUser = this.dataContext.Users.FirstOrDefault (x => x.Email == email);
			return ApplicationUser != null;
		}

		public ApplicationUser GetById (string id)
		{
			return this.dataContext.Users.FirstOrDefault (c => c.UserId == id);
		}

		public ApplicationUser [] GetAll ()
		{
			return this.dataContext.Users.ToArray ();
		}

		public ApplicationUser GetByEmail (string email)
		{
			return this.dataContext.Users.FirstOrDefault (c => c.Email == email);
		}

		public ApplicationUser RegisterApplicationUser (ApplicationUser model)
		{
			var id = IdGenerator.CreateLetterId (10);
			var existWithId = this.GetById (id);
			while (existWithId != null) {
				id = IdGenerator.CreateLetterId (10);
				existWithId = this.GetById (id);
			}
			model.UserId = id;
			model.Password = BC.HashPassword (model.Password);

			var ApplicationUserEntity = this.dataContext.Users.Add (model);
			this.dataContext.SaveChanges ();

			return ApplicationUserEntity.Entity;
		}


		// Takes user email and role and generates token
		public string GenerateJwtToken (string email, string role)
		{
			var issuer = this.configuration ["Jwt:Issuer"];
			var audience = this.configuration ["Jwt:Audience"];
			var key = Encoding.ASCII.GetBytes (this.configuration ["Jwt:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor {
				Subject = new ClaimsIdentity (new []
			    {
			    new Claim("Id", Guid.NewGuid().ToString()),
			    new Claim(JwtRegisteredClaimNames.Sub, email),
			    new Claim(JwtRegisteredClaimNames.Email, email),
			    new Claim(ClaimTypes.Role, role),
			    new Claim(JwtRegisteredClaimNames.Jti,
			    Guid.NewGuid().ToString())
			}),
				Expires = DateTime.UtcNow.AddMinutes (5),
				Issuer = issuer,
				Audience = audience,
				SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (key), SecurityAlgorithms.HmacSha512Signature)
			};
			var tokenHandler = new JwtSecurityTokenHandler ();
			var token = tokenHandler.CreateToken (tokenDescriptor);
			return tokenHandler.WriteToken (token);
		}

		public string? DecodeEmailFromToken (string token)
		{
			var decodedToken = new JwtSecurityTokenHandler ();
			var indexOfTokenValue = 7;

			var t = decodedToken.ReadJwtToken (token.Substring (indexOfTokenValue));

			return t.Payload.FirstOrDefault (x => x.Key == "email").Value.ToString ();
		}

		public ApplicationUser ChangeRole (string email, string role)
		{
			var ApplicationUser = this.GetByEmail (email);
			ApplicationUser.Role = role;
			this.dataContext.SaveChanges ();


			return ApplicationUser;
		}
	}
}