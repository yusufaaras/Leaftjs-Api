using MediatR;
using MoskBilisimAPI.Context;
using MoskBilisimAPI.CQRS.Commands;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MoskBilisimAPI.CQRS.Handlers
{
    public class RemoveShapeCommandHandler : IRequestHandler<RemoveShapeCommand, bool>
    {
        private readonly MoskBilisimDbContext _context;

        public RemoveShapeCommandHandler(MoskBilisimDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(RemoveShapeCommand request, CancellationToken cancellationToken)
        {
            var mapData = await _context.MapData
                .Include(m => m.Polygons)
                .Include(m => m.Circles)
                .Include(m => m.Markers)
                .Include(m => m.Polylines)
                .Include(m => m.ImportantPoints)
                .Include(m => m.Rectangles)
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (mapData == null)
            {
                return false; 
            }

            foreach (var polyline in mapData.Polylines)
            {
                if (polyline.Points != null && polyline.Points.Any())
                {
                    _context.Coordinates.RemoveRange(polyline.Points);
                }
            }

            foreach (var polygon in mapData.Polygons)
            {
                if (polygon.Points != null && polygon.Points.Any())
                {
                    _context.Coordinates.RemoveRange(polygon.Points);
                }
            }

            _context.Polylines.RemoveRange(mapData.Polylines);
            _context.Polygons.RemoveRange(mapData.Polygons);
            _context.Circles.RemoveRange(mapData.Circles);
            _context.Markers.RemoveRange(mapData.Markers);
            _context.ImportantPoints.RemoveRange(mapData.ImportantPoints);
            _context.Rectangles.RemoveRange(mapData.Rectangles);

            _context.MapData.Remove(mapData);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                
                return false; 
            }

            return true; 
        }
    }
}
