using MediatR;
using Microsoft.EntityFrameworkCore;
using MoskBilisimAPI.Context;
using MoskBilisimAPI.CQRS.Queries;

namespace MoskBilisimAPI.CQRS.Handlers
{
    public class GetShapeByIDQueryHandler : IRequestHandler<GetShapeByIDQuery, MapData>
    {
        private readonly MoskBilisimDbContext _context;

        public GetShapeByIDQueryHandler(MoskBilisimDbContext context)
        {
            _context = context;
        }

        public async Task<MapData> Handle(GetShapeByIDQuery request, CancellationToken cancellationToken)
        {
            var mapData = await _context.MapData.Include(m => m.Markers)
                                                .Include(m => m.Polygons)
                                                .Include(m => m.Circles)
                                                .Include(m => m.Polylines)
                                                .Include(m => m.Rectangles)
                                                .Include(m => m.ImportantPoints)
                                                .FirstOrDefaultAsync(m => m.Id == request.id, cancellationToken);

            return mapData;
        }
    }
}