using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

namespace Models
{
    [Table("Igrac")]
    public class Igrac
    {
        [Key]
        public int IgracID { get; set; }

        [MaxLength(20)]
        [Required]
        public string Ime { get; set; }

        [MaxLength(20)]
        [Required]
        public string Prezime { get; set; }

        [Required]
        public int GodinaRodjenja { get; set; }

        [Required]
        public string Nacionalnost { get; set; }

        [Required]
        public int Golovi{ get; set; }

        [Required]
        public int Asistencije{ get; set; }

        [Required]
        public virtual Klub Klub { get; set; }
    }
}