using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Server.Models 
{

    [Table("SalaFilm")]
    public class SalaFilm {

        [Column("FilmID")]
        public int filmID { get; set; }

        [Column("Film")]
        [JsonIgnore]
        public Film film { get; set; }

        [Column("SalaID")]
        public int salaID { get; set; }

        [Column("Sala")]
        [JsonIgnore]
        public Sala sala { get; set; }
        
        /* [Column("BrojFilmova")]
        [JsonIgnore]
        public int BrojFilmova { get; set; } */
        
    }
}