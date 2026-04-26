using System.Text.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RowdyTroll.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RowdyTroll.Api.Security;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
// Add CORS policy to allow calls from the local frontend during development
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhostFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://127.0.0.1:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Exclude the sample weather endpoint from the generated Swagger document
    c.DocInclusionPredicate((docName, apiDesc) =>
        apiDesc.RelativePath == null || apiDesc.RelativePath.IndexOf("weatherforecast", StringComparison.OrdinalIgnoreCase) < 0);
});

// Register EF Core StoreContext using SQLite. Migrations assembly set to the API project.
builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlite("Data Source=../Registrar.sqlite",
        b => b.MigrationsAssembly("RowdyTroll.Api"))
);

// Configure authentication & authorization if Auth0 settings are present
var auth0Section = builder.Configuration.GetSection("Auth0");
var auth0Domain = auth0Section.GetValue<string>("Domain");
var auth0Audience = auth0Section.GetValue<string>("Audience");

if (!string.IsNullOrEmpty(auth0Domain) && !string.IsNullOrEmpty(auth0Audience))
{
    builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.Authority = auth0Domain;
        options.Audience = auth0Audience;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true
        };
    });

    builder.Services.AddAuthorization(options =>
    {
        // Example policy that requires delete:catalog scope
        options.AddPolicy("delete:catalog", policy =>
            policy.Requirements.Add(new HasScopeRequirement("delete:catalog", auth0Domain)));
    });
}

var app = builder.Build();

// Enable Swagger only in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RowdyTroll API v1"));
}

app.UseHttpsRedirection();

// Enable CORS using the policy defined above
app.UseCors("AllowLocalhostFrontend");

// If Auth0 is configured, enable authentication & authorization in the pipeline
if (!string.IsNullOrEmpty(auth0Domain) && !string.IsNullOrEmpty(auth0Audience))
{
    app.UseAuthentication();
    app.UseAuthorization();
}

app.MapControllers();

app.Run();
