using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MoskBilisimAPI.Core
{
    public class Polyline
    {
        [Key] 
        public int Id { get; set; }
        public List<Coordinate> Points { get; set; }
        public string Note { get; set; }
    }
}
