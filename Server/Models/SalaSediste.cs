using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Server.Models 
{

    [Table("SalaSediste")]
    public class SalaSediste {

        [Column("SedisteID")]
        public int sedisteID { get; set; }

        [Column("Sediste")]
        [JsonIgnore]
        public Sediste sediste { get; set; }

        [Column("SalaID")]
        public int salaID { get; set; }

        [Column("Sala")]
        [JsonIgnore]
        public Sala sala { get; set; }

        [Column("BrojSedista")]
        public int BrojSedista { get; set; }
    }
}