using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

namespace Models
{

    [Table("Utakmica")]
    public class Utakmica
    {
        [Key]
        public int MecID { get; set; }

        [Required]
        public Sezona Sezona { get; set; }

        [Required]
        public int Kolo { get; set; }
        
        [Required]
        public Klub Domacin { get; set; }

        [Required]
        public int golovi_domacin { get; set; }
        
        [Required]
        public Klub Gost { get; set; }

        [Required]
        public int golovi_gost { get; set; }

        [Required]
        public Sudija sudija { get; set; }
    }
}