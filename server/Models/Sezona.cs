using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

namespace Models
{
    [Table("Sezona")]
    public class Sezona
    {
        [Key]
        public int SezonaID { get; set; }

        [MaxLength(10)]
        [Required]
        public string Godina { get; set; }

        [JsonIgnore]
        public List<Klub> Klubovi { get; set; }

        [JsonIgnore]
        public List<Utakmica> Utakmice { get; set; }
    }
}