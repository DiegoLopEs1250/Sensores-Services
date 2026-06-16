namespace Sensores.Api.Models.Datos.CreateData
{

    public record CreateDataRequest(double Temperatura, double Humedad, DateTime Fecha);

    public record CreateDataResponse(Guid Id);


    public class CreateDataEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/datas", async (CreateDataRequest request, ISender sender, HttpContext httpContext) =>
            {
                // IP del cliente (LO QUE QUIERES)
                var clientIp = httpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();

                // IP del contenedor
                var containerIp = httpContext.Connection.LocalIpAddress?.MapToIPv4().ToString();

                Console.WriteLine($"IP Cliente: {clientIp}");
                Console.WriteLine($"IP Contenedor: {containerIp}");

                var command = request.Adapt<CreateDataCommand>();

                command = command with
                {
                    IpServidor = clientIp,      // ahora es cliente
                    IpContenedor = containerIp
                };

                var result = await sender.Send(command);
                var response = result.Adapt<CreateDataResponse>();

                return Results.Created($"/datas/{response.Id}", response);
            })
                .WithName("InsertarData")
                .Produces<CreateDataResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Insertar un nuevo valor")
                .WithDescription("Se crea un nuevo valor y se retorna el identificador de la entidad");
        }
    }
}
