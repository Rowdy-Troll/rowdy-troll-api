using System.Text.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RowdyTroll.Data;

var builder = WebApplication.CreateBuilder(args);
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

var app = builder.Build();

// Enable Swagger only in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RowdyTroll API v1"));
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
