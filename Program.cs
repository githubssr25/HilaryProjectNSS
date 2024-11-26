using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Models; // Namespace for your models
using Mapper; // Namespace for your MappingProfile class

var builder = WebApplication.CreateBuilder(args);

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:5173") // Replace with your frontend URL if needed
                     .AllowAnyHeader()
                     .AllowAnyMethod();
    });
});

// Configure EF Core with PostgreSQL
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // Handles legacy timestamp behavior
builder.Services.AddNpgsql<HilaryDbContext>(builder.Configuration["HilaryProjectDbConnectionString"]); // Replace with your DbContext class

// Configure AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

// Add Swagger/OpenAPI (for API documentation and testing)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply CORS Policy
app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add endpoints (replace placeholder routes as needed)
app.MapGet("/", () => "Hillary's Hair Care API is running!");

app.Run();
