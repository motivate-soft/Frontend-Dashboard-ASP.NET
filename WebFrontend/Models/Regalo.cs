using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebFrontend.Models
{
    [Table("regalos_re")]
    public class Regalo
    {
        [Key]
        public int id_regalo { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string pais { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string vehiculo { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string llantas { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string neumaticos { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string tipo { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string producto { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string valor { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string descripcion { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string tipo_manhattan { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string numero_manhattan { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string plantilla_manhattan { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string codigoinforme { get; set; }

        public decimal pvpinforme { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string codigofacturainforme { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string paisinforme { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string grupoproducto { get; set; }

        public int? mostrar { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string link { get; set; }

        public Participation Participation { get; set; }
    }
}
