using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebFrontend.Models
{
    [Table("participaciones_par")]
    public class Participation
    {
        [Key]
        public int id_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string nombre_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string apellidos_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string sexo_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string edad_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string email_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string dni_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string telefono_par { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? nacimiento_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string direccion_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string localidad_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string codigopostal_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string provincia_par { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? registrofecha_par { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan? registrohora_par { get; set; }

        public int? id_est { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string localizador_par { get; set; }

        public int? id_tall { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string codigo_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string codigo2_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string codigo3_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string codigo4_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string codigo5_par { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? entregafecha_par { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan? entregahora_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string valorpremio_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string motivo_par { get; set; }

        [Column(TypeName = "text")]
        public string comentarios_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string ticket_par { get; set; }

        public int? comercial_par { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? fechaM_par { get; set; }

        public int? usuM_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string url_par { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? fechaEnvioEmail_par { get; set; }

        public int? usuEnvioEmail_par { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? registrocaducidadfecha_par { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan? registrocaducidadhora_par { get; set; }

        [Required]
        public int id_gan { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? fechaid_gan { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string nombreapellidos_par { get; set; }

        public int? id_ganbck { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string lugar_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string nombretaller_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string direcciontaller_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string distribuidor_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string adjunto1_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string factura1_par { get; set; }

        public int? valoracion1_par { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? fechaCompra1_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string adjunto2_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string factura2_par { get; set; }

        public int? valoracion2_par { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? fechaCompra2_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string adjunto3_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string factura3_par { get; set; }

        public int? valoracion3_par { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? fechaCompra3_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string adjunto4_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string factura4_par { get; set; }

        public int? valoracion4_par { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? fechaCompra4_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string adjunto5_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string factura5_par { get; set; }

        public int? valoracion5_par { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? fechaCompra5_par { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? rechazoFecha_par { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan? rechazoHora_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string oleada_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string dondeconociste_par { get; set; }

        //
        public int? premioSelFrnt_par { get; set; }

        public int? premioSelBck_par { get; set; }

        [Column(TypeName = "varchar(2)")]
        public string pais_par { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string IP_Usuario { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string llanta { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string TamanoRueda { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Numero_ruedas { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string token { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string ExisteAdjunto { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string ExisteAdjunto2 { get; set; }

        public int? solicitar_adjunto { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? fecha_hora_solicitar_adjunto { get; set; }

        public int? adjunto_adjunto { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? fecha_hora_adjunto_adjunto { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string PremioseleccionadoBackp { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string medidallanta_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string numllantas_par { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? fechaValidacion_par { get; set; }

        public int? usuValidacion_par { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? registrofechaOK_par { get; set; }

        public int? Numero_ruedas_int { get; set; }

        public int? enviadoRegaloFisico { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? enviadoFechaRegaloFisico { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string tipovehiculo_par { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string nacionalidad_par { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string talla_par { get; set; }

        public Participation()
        {
            comercial_par = 0;
            id_gan = 0;
            id_ganbck = 0;
            valoracion1_par = 0;
            valoracion2_par = 0;
            valoracion3_par = 0;
            valoracion4_par = 0;
            valoracion5_par = 0;
            solicitar_adjunto = 0;
            adjunto_adjunto = 0;
        }

        public Regalo Regalo { get; set; }
    }
}
