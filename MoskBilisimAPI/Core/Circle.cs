using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MoskBilisimAPI.Core
{
    public class Circle
    {
        [Key]
        public int Id { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public double Radius { get; set; }
        public string Note { get; set; }
    }
}
