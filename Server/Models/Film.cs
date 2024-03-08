using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Server.Models
{
    [Table("Film")]
    public class Film
    {
        [Key]
        [Column("ID")]
        public int id { get; set; }

        [Column("Naziv")]
        public string naziv {get; set;}

        [Column("CenaKarte")]
        public int cenaKarte {get; set;}

        [Column("FormaBioskopaID")]
        public int FormaBioskopaID { get; set; }

        [Column("uSali")]
        public virtual List<SalaFilm> Veza { get; set; }
    }
}