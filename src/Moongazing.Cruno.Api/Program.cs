using Microsoft.EntityFrameworkCore;
using Moongazing.Cruno.Api.Modules.Job;
using Moongazing.Cruno.Modules.Jobs.Infrastructure.Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// --------------------------
// Services (DI Container)
// --------------------------

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core - PostgreSQL kullanıyorsan:
builder.Services.AddDbContext<JobsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CrunoJobs")));

// MediatR
builder.Services.AddMediatR(config=>config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));



// --------------------------
// Build & Middleware
// --------------------------

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// --------------------------
// Endpoints
// --------------------------

app.MapGet("/", () => "Cruno is alive!");
app.MapCreateJobEndpoint();

app.Run();
