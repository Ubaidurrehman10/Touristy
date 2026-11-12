using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Touristy.Core.DataServices;
using Touristy.Core.Processors;
using Touristy.Persistence;
using Touristy.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Touristy API",
        Version = "v1"
    });
});

var connString = "DataSource=:memory:";
var conn = new SqliteConnection(connString);
conn.Open();

builder.Services.AddDbContext<TouristyAppDbContext>(options =>
{
    options.UseSqlite(conn);
});

CreateDatabase.EnsureDatabaseCreated(conn);

builder.Services.AddScoped<IRoomBookingService, RoomBookingService>();
builder.Services.AddScoped<IRoomBookingRequestProcessor, RoomBookingRequestProcessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();