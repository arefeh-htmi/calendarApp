using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using CalendarApp.Data;
using AutoMapper;
using CalendarApp.Models.DbModels;
using CalendarApp.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using calendarApp.Filters;
using Microsoft.OpenApi.Models;
using CalendarApp.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder (args);

// Add services to the container.

builder.Services.AddControllers (options =>
		    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
		.AddNewtonsoftJson (options =>
		    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
		);

// Setting up Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen (option => {
	option.SwaggerDoc ("v1", new OpenApiInfo { Title = "HR Application", Version = "v1" });
	option.AddSecurityDefinition ("Bearer", new OpenApiSecurityScheme {
		In = ParameterLocation.Header,
		Description = "Please enter a valid token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "bearer"
	});

	option.OperationFilter<AuthResponsesOperationFilter> ();
});




//@TODO: save the DefaultConnection in a safer place like key vault in azure....


var connectionString = builder.Configuration.GetConnectionString ("DefaultConnection") ?? throw new InvalidOperationException ("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext> (options =>
    options.UseSqlite (connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter ();

// @TODO: add the SMTP gateway, extend the send email async functionality to get emails on dev
builder.Services.AddDefaultIdentity<ApplicationUser> (options => options.SignIn.RequireConfirmedAccount = true)
      .AddRoles<IdentityRole> ()
    .AddEntityFrameworkStores<ApplicationDbContext> ();

//Adds logger
var serviceProvider = builder.Services.BuildServiceProvider ();
var logger = serviceProvider.GetRequiredService<ILogger<ControllerBase>> ();
builder.Services.AddSingleton (typeof (ILogger), logger);


// Adds mapper
var config = new MapperConfiguration (cfg => {
	cfg.AddProfile (new MappingProfiles ());
});
var mapper = config.CreateMapper ();
builder.Services.AddSingleton (mapper);


// configuring the JWT
builder.Services.AddAuthentication (options => {
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer (o => {
	o.TokenValidationParameters = new TokenValidationParameters {
		ValidIssuer = builder.Configuration ["Jwt:Issuer"],
		ValidAudience = builder.Configuration ["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey
	    (Encoding.UTF8.GetBytes (builder.Configuration ["Jwt:Key"])),
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = false,
		ValidateIssuerSigningKey = true
	};
});
builder.Services.AddAuthorization ();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

//Add CORS
builder.Services.AddCors (options => {
	options.AddDefaultPolicy (
	    builder => {
		    //you can configure your custom policy
		    builder.AllowAnyOrigin ()
				    .AllowAnyHeader ()
				    .AllowAnyMethod ();
	    });
});


// Declared services
builder.Services.AddScoped<DBSeeder> ();
builder.Services.AddTransient<AuthService> ();
builder.Services.AddTransient<EventService> ();


var app = builder.Build ();

app.UseHttpsRedirection ();

app.UseAuthentication ();
app.UseAuthorization ();

//use cors
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment ()) {
	app.UseSwagger ();
	app.UseSwaggerUI ();
	app.UseSeedDB ();
}

app.MapControllers();

app.Run ();

