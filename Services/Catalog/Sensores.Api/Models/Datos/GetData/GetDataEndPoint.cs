namespace Sensores.Api.Models.Datos.GetData
{
    public record GetDataResponse(IEnumerable<SensorData> data);


    public class GetDataEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/datos", async (ISender sender) =>
            {
                var result = await sender.Send(new GetDataQuery());
                var response = result.Adapt<GetDataResponse>();

                return Results.Ok(response);
            })
            .WithName("GetDatos")
            .Produces<GetDataResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Resumen")
            .WithDescription("Retorna esto perro");
        }

    }
}
