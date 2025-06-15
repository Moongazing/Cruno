using Moongazing.Cruno.Worker.Services;
using Moongazing.Cruno.Worker.Services.Handlers;
using Moongazing.Cruno.Worker.Services.Handlers.Common;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<JobExecutionConsumer>();

builder.Services.AddSingleton<IJobExecutor, JobExecutor>();

builder.Services.AddSingleton<IJobHandler, SendEmailJobHandler>();
builder.Services.AddSingleton<IJobHandler, GeneratePdfJobHandler>();
builder.Services.AddSingleton<IJobHandler, CallWebhookJobHandler>();
builder.Services.AddSingleton<IJobHandler, WriteToFileJobHandler>();
builder.Services.AddSingleton<IJobHandler, ExecuteShellCommandJobHandler>();

builder.Services.AddHttpClient();

builder.Logging.AddConsole();

var host = builder.Build();
host.Run();
