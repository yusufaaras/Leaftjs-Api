using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MoskBilisimAPI.Core
{
    public class Coordinate
    {
        [Key] 
        public int Id { get; set; }  
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

}
