using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Server.Models
{
    [Table("Sediste")]
    public class Sediste
    {
        [Column("Broj")]
        [Key]
        public int broj {get; set;}

        [Column("Zauzetost")]
        public bool zauzetost {get; set;}

        [Column("uSali")]
        public virtual List<SalaSediste> Veza { get; set; }

        [Column("FormaBioskopaID")]
        public int FormaBioskopaID { get; set; }
    }
}