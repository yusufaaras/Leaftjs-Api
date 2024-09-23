using MoskBilisimAPI.Core;
using System.ComponentModel.DataAnnotations;

public class Polygon
{
    [Key]
    public int Id { get; set; }
    public List<Coordinate> Points { get; set; } = new List<Coordinate>(); 
    public string Note { get; set; }
}
