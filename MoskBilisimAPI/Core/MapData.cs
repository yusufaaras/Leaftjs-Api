using MoskBilisimAPI.Core;

public class MapData
{
    public int Id { get; set; }
    public List<Marker> Markers { get; set; } = new List<Marker>();
    public List<Polygon> Polygons { get; set; } = new List<Polygon>();
    public List<Circle> Circles { get; set; } = new List<Circle>();
    public List<Polyline> Polylines { get; set; } = new List<Polyline>();
    public List<Rectangle> Rectangles { get; set; } = new List<Rectangle>();
    public List<ImportantPoint> ImportantPoints { get; set; } = new List<ImportantPoint>();
}
