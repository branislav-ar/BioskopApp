using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Server.Models 
{
    [Table("Sala")]
    public class Sala {
        
        //osnovne info
        [Column("ID")]
        [Key]
        public int id { get; set; }

        [Column("Broj")]
        public int broj { get; set; }

        [Column("Naziv")]
        public string naziv { get; set; }

        //liste
        [Column("VEZA_Sala_Film")]
        public List<SalaFilm> VEZA_Sala_Film { get; set; }

        [Column("VEZA_Sala_Sediste")]
        public List<SalaSediste> VEZA_Sala_Sediste { get; set; }

        //json ignore info
        [Column("FormaBioskopa")]
        [JsonIgnore]
        public FormaBioskopa formaBioskopa { get; set; }

        [Column("Bioskop")]
        [JsonIgnore]
        public Bioskop bioskop { get; set; }
    }
}