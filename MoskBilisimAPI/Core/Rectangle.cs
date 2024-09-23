using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MoskBilisimAPI.Core
{
    public class Rectangle
    {
        [Key] 
        public int Id { get; set; }
        public Coordinate Northeast { get; set; }
        public Coordinate Southwest { get; set; }
        public string Note { get; set; }
    }
}
