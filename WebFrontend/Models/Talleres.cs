using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebFrontend.Models
{
    [Table("talleres_tall")]
    public class Talleres
    {
        [Key]
        public int id_tall { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string razonsocial_tall { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string direccion_tall { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string poblacion_tall { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string provincia_tall { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string cp_tall { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string telefono_tall { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string email_tall { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime? fechaDesde_tall { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime? fechaHasta_tall { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string REGION_tall { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string LC_tall { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string HPDV_tall { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string ENSENA_tall { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string alias_tall { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string pais_tall { get; set; }

        public int? usuM_tall { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime? fechaM_tall { get; set; }

        public int? usuC_tall { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime? fechaC_tall { get; set; }

        public Talleres()
        {
            fechaM_tall = DateTime.UtcNow;
            fechaC_tall = DateTime.UtcNow;
        }
    }
}
