using Microsoft.CodeAnalysis;
using static System.Collections.Specialized.BitVector32;

namespace Sensores.Api.Models.Datos.GetData
{
    public record GetDataQuery() : IQuery<GetDataResult>;

    public record GetDataResult(IEnumerable<SensorData> data);



    internal class GetDataQueryHandler (IDocumentSession documentSession, ILogger<GetDataQueryHandler> logger) : IQueryHandler<GetDataQuery,GetDataResult>
    {
        public async Task<GetDataResult> Handle(GetDataQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetDataQueryHandler.Hanle llamado {@query}", query);
            var products = await documentSession.Query<SensorData>().ToListAsync(cancellationToken);
            return new GetDataResult(products);
        }
    }
}
