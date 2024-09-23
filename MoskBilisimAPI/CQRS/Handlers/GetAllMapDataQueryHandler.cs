using MediatR;
using Microsoft.EntityFrameworkCore;
using MoskBilisimAPI.Context;
using MoskBilisimAPI.Core;

namespace MoskBilisimAPI.CQRS.Handlers
{
    public class GetAllMapDataQueryHandler : IRequestHandler<GetAllMapDataQuery, List<MapData>>
    {
        private readonly MoskBilisimDbContext _context;

        public GetAllMapDataQueryHandler(MoskBilisimDbContext context)
        {
            _context = context;
        }

        public async Task<List<MapData>> Handle(GetAllMapDataQuery request, CancellationToken cancellationToken)
        {
            return await _context.MapData.Include(m => m.Markers)
                                          .Include(m => m.Polygons)
                                          .Include(m => m.Circles)
                                          .Include(m => m.Polylines)
                                          .Include(m => m.Rectangles)
                                          .Include(m => m.ImportantPoints)
                                          .ToListAsync(cancellationToken);
        }
    }
}
