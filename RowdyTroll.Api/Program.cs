using System.Text.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RowdyTroll.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Register EF Core StoreContext using SQLite. Migrations assembly set to the API project.
builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlite("Data Source=../Registrar.sqlite",
        b => b.MigrationsAssembly("RowdyTroll.Api"))
);

var app = builder.Build();

var openApiJson = """
{
    "openapi": "3.0.1",
    "info": { "title": "RowdyTroll API", "version": "v1" },
    "paths": {
        "/catalog": {
            "get": { "summary": "Get all items", "responses": { "200": { "description": "OK" } } },
            "post": { "summary": "Create item", "responses": { "201": { "description": "Created" } } }
        },
        "/catalog/{id}": {
            "get": { "parameters": [ { "name": "id", "in": "path", "required": true, "schema": { "type": "integer" } } ], "responses": { "200": { "description": "OK" } } },
            "put": { "parameters": [ { "name": "id", "in": "path", "required": true, "schema": { "type": "integer" } } ], "responses": { "204": { "description": "No Content" } } },
            "delete": { "parameters": [ { "name": "id", "in": "path", "required": true, "schema": { "type": "integer" } } ], "responses": { "204": { "description": "No Content" } } }
        },
        "/catalog/{id}/ratings": {
            "post": { "parameters": [ { "name": "id", "in": "path", "required": true, "schema": { "type": "integer" } } ], "responses": { "200": { "description": "OK" } } }
        }
    },
    "components": {
        "schemas": {
            "Item": {
                "type": "object",
                "properties": {
                    "id": { "type": "integer" },
                    "name": { "type": "string" },
                    "description": { "type": "string" },
                    "brand": { "type": "string" },
                    "price": { "type": "number", "format": "decimal" },
                    "ratings": { "type": "array", "items": { "$ref": "#/components/schemas/Rating" } }
                }
            },
            "Rating": {
                "type": "object",
                "properties": {
                    "id": { "type": "integer" },
                    "stars": { "type": "integer" },
                    "userName": { "type": "string" },
                    "review": { "type": "string" }
                }
            }
        }
    }
}
""";

var swaggerHtml = """
<!doctype html>
<html>
    <head>
        <meta charset='utf-8'/>
        <meta name='viewport' content='width=device-width, initial-scale=1'>
        <title>RowdyTroll API - Swagger UI</title>
        <link rel='stylesheet' href='https://unpkg.com/swagger-ui-dist/swagger-ui.css'>
    </head>
    <body>
        <div id='swagger-ui'></div>
        <script src='https://unpkg.com/swagger-ui-dist/swagger-ui-bundle.js'></script>
        <script>
            window.onload = function() {
                SwaggerUIBundle({
                    url: '/openapi',
                    dom_id: '#swagger-ui'
                });
            };
        </script>
    </body>
</html>
""";

// Only expose the UI and OpenAPI JSON in Development
if (app.Environment.IsDevelopment())
{
    app.MapGet("/openapi", () => Results.Content(openApiJson, "application/json"));
    app.MapGet("/swagger", () => Results.Content(swaggerHtml, "text/html"));
}

app.UseHttpsRedirection();

app.MapControllers();

// Keep the sample weather endpoint (unchanged)
var summaries = new[]
{
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
