using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebFrontend.Models
{
    [Table("general_gen")]
    public class General
    {
        [Key]
        public int id_gen { get; set; }

        public int? activa_gen { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string nombre_gen { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? fechadesde_gen { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? fechahasta_gen { get; set; }

        [Column(TypeName = "text")]
        public string mecanica_gen { get; set; }

        public int? nummaxdni_gen { get; set; }

        public int? chckdni_gen { get; set; }

        public int? nummaxemail_gen { get; set; }

        public int? chckemail_gen { get; set; }

        public int? nummaxcookie_gen { get; set; }

        public int? chckcookie_gen { get; set; }

        public int? nummaxpremios_gen { get; set; }

        public int? chckpremios_gen { get; set; }

        public int? nummaxnumfac_gen { get; set; }

        public int? chcknumfac_gen { get; set; }

        public int? numtotalpremios_gen { get; set; }

        public int? chcktotalpremios_gen { get; set; }

        [Column(TypeName = "text")]
        public string alertacookies_gen { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string filecookie_gen { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string fileprivacidad_gen { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string filelegal_gen { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string email_gen { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string telefono_gen { get; set; }

        [Column(TypeName = "text")]
        public string contacto_gen { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string token_gen { get; set; }

        public int? chckcine_gen { get; set; }

        [Column(TypeName = "text")]
        public string analytics_gen { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? fechaM_gen { get; set; }

        public int? usuM_gen { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string filePDF01_gen { get; set; }

        public int? emailavisochk_gen { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string emailaviso_gen { get; set; }

        public int? numemailaviso_gen { get; set; }

        public int? enviadoemailaviso_gen { get; set; }

        public int? nummaxtelefono_gen { get; set; }

        public int? chcktelefono_gen { get; set; }

        public int? nummaxip_gen { get; set; }

        public int? chckip_gen { get; set; }
    }
}
