using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Volunteering.ApplicationServices;
using Volunteering.Data.DomainServices;
using Volunteering.Data.Mapping;
using Volunteering.Data.Models;
using Volunteering.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(NewsMappingProfile).Assembly);

// Add services to the container.

builder.Services.AddCors(options => options.AddPolicy("corspolicy", policy =>
{
    policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddControllers();

builder.Services.AddTransient<NewsDomainService>();
builder.Services.AddTransient<NewsApplicationService>();
builder.Services.AddTransient<UserDomainService>();
builder.Services.AddTransient<UserApplicationService>();

builder.Services.AddTransient<CampaignStatusDomainService>();
builder.Services.AddTransient<CampaignStatusApplicationService>();
builder.Services.AddTransient<CampaignPriorityDomainService>();
builder.Services.AddTransient<CampaignPriorityApplicationService>();
builder.Services.AddTransient<CampaignDomainService>();
builder.Services.AddTransient<CampaignApplicationService>();
builder.Services.AddTransient<CategoryDomainService>();
builder.Services.AddTransient<CategoryApplicationService>();
builder.Services.AddTransient<DonationDomainService>();
builder.Services.AddTransient<DonationApplicationService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (ConnectionHealth.CheckConnectionHealth(builder.Configuration.GetConnectionString("DefaultConnectionString")))
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
    });
}
else
{
    Console.WriteLine("Primary connection failed. Switching to fallback connection...");
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("FallbackConnectionString"));
    });
}

builder.Services.AddHealthChecks()
       .AddDbContextCheck<AppDbContext>();


builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:SecretKey").Value);
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false, // for dev
        ValidateAudience = false, // for dev
        RequireExpirationTime = false, // for dev
        ValidateLifetime = true
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    builder.Configuration.AddUserSecrets<Program>();
}

app.UseHttpsRedirection();

app.UseCors("corspolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

DbInitializer.Initialize(app);

app.Run();
