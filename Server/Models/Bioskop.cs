using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Server.Models 
{

    [Table("Bioskop")]
    public class Bioskop {
        [Column("ID")]
        [Key]
        public int id { get; set; }

        [Column("Naziv")]
        [MaxLength(18)]
        public string naziv { get; set; }

        [Column("BrojSala")]
        public int brojSala { get; set; }

        [Column("BrojMestaUSalama")]
        public int brojMestaUSalama { get; set; }

        [Column("FormaBioskopa")]
        public FormaBioskopa formaBioskopa { get; set; }

        [Column("Sale")]
        public virtual List<Sala> sale { get; set; }


    }
}