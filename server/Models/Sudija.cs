using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{

    [Table("Sudija")]
    public class Sudija
    {
        [Key]
        public int SudijaID { get; set; }

        [MaxLength(20)]
        [Required]
        public string Ime { get; set; }

        [MaxLength(20)]
        [Required]
        public string Prezime { get; set; }

        [JsonIgnore]
        public List<Utakmica> Sudjene_utakmice { get; set; }

    }
}