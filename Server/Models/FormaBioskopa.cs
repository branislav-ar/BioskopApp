using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Server.Models 
{

    [Table("FormaBioskopa")]
    public class FormaBioskopa {

        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("BioskopRef")]
        [JsonIgnore]
        public Bioskop bioskop { get; set; }

        [Column("Stavke")]
        public virtual List<Film> Stavke { get; set; }

    }
}