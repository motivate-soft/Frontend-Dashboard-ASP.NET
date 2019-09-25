using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebFrontend.Models
{
    [Table("llantas_model")]
    public class Llantas
    {
        [Key]
        public int id_marca { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string literal_marca { get; set; }

        public int? orden_marca { get; set; }
    }
}
