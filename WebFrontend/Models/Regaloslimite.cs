using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebFrontend.Models
{
    [Table("regaloslimite_lim")]
    public class Regaloslimite
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string grupoproducto_lim { get; set; }

        public int? unidades_lim { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string especial { get; set; }
    }
}
