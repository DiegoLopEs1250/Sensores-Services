namespace Sensores.Api.Models.Datos.CreateData
{

    public record CreateDataCommand(double Temperatura, double Humedad, DateTime Fecha, string? IpServidor, string? IpContenedor) : ICommand<CreateDataResult>;

    public record CreateDataResult(Guid Id);

    internal class CreateDataCommandHandler(IDocumentSession documentSession) : ICommandHandler<CreateDataCommand, CreateDataResult>
    {
        public async Task<CreateDataResult> Handle(CreateDataCommand request, CancellationToken cancellationToken)
        {
            SensorData data = new SensorData
            {
                Temperatura = request.Temperatura,
                Humedad = request.Humedad,
                Fecha = request.Fecha,
                IpServidor = request.IpServidor,
                IpContenedor = request.IpContenedor
            };

            documentSession.Store(data);
            await documentSession.SaveChangesAsync(cancellationToken);
            return new CreateDataResult(data.Id);
        }
    }
}
