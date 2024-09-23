using MediatR;
using MoskBilisimAPI.Core;

namespace MoskBilisimAPI.CQRS.Handlers
{
    public class GetAllMapDataQuery : IRequest<List<MapData>>
    {
    }
}
