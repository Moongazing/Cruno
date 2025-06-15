using MediatR;
using Moongazing.Cruno.Modules.Jobs.Application.Features.Jobs.Commands.Create;

namespace Moongazing.Cruno.Api.Modules.Job;

public static class CreateJobEndpoint
{
    public static IEndpointRouteBuilder MapCreateJobEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/jobs", async (CreateJobCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Created($"/jobs/{result}", new { Id = result });
        })
        .WithName("CreateJob")
        .Produces(201)
        .Produces(400);

        return app;
    }
}
