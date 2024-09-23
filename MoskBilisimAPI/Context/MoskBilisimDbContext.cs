using Microsoft.EntityFrameworkCore;
using MoskBilisimAPI.Core;

namespace MoskBilisimAPI.Context
{
    public class MoskBilisimDbContext:DbContext
    {
        public MoskBilisimDbContext(DbContextOptions<MoskBilisimDbContext> options)
            : base(options)
        {
        }

        public DbSet<MapData> MapData { get; set; }
        public DbSet<Marker> Markers { get; set; }
        public DbSet<Polygon> Polygons { get; set; }
        public DbSet<Circle> Circles { get; set; }
        public DbSet<Polyline> Polylines { get; set; }
        public DbSet<Rectangle> Rectangles { get; set; }
        public DbSet<ImportantPoint> ImportantPoints { get; set; }
        public DbSet<Coordinate> Coordinates { get; set; }
    }
}

