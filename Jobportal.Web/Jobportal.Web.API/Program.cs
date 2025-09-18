using Jobportal.Web.API.JobsController;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{    
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:5173", "")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials()); // Optional: if you need to send cookies/credentials
                                    
    // You can also define a default policy or allow any origin (use with caution in production)
    options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Define a policy in Program.cs
/*
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Administrator"));
});
*/

var app = builder.Build();

// Use authentication and authorization middleware
app.UseAuthentication();
/*
app.MapGet("/user-data", (ClaimsPrincipal user) => "User or Admin data accessed!")
    .RequireAuthorization(policy => policy.RequireRole("User", "Administrator"));

app.MapPost("/GenerateJwtToken", ([FromBody]string userId, string userName, string roless) => {

    List<string> roles = new List<string>();
    roles.AddRange(roless.Split(','));
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Name, userName)
    };

    foreach (var role in roles)
    {
        claims.Add(new Claim(ClaimTypes.Role, role));
    }

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: builder.Configuration["Jwt:Issuer"],
        audience: builder.Configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.Now.AddMinutes(30), // Set appropriate expiration
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
});
*/

app.UseCors("AllowSpecificOrigin");
app.MapGet("/", () => "Hello World!");
app.MapGet("/api/jobs",  () =>  new JobsController().GetJob()).RequireCors("AllowSpecificOrigin"); ;
app.Run();
