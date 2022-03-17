using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Klub")]
    public class Klub
    {
        [Key]
        public int KlubID { get; set; }

        [MaxLength(50)]
        [Required]
        public string Naziv { get; set; }

        [MaxLength(50)]
        [Required]
        public string Grad { get; set; }
        
        [Required]
        public virtual Sezona sezona {get;set;}

        [JsonIgnore]
        public virtual List<Igrac> Igraci { get; set;}

    }
}