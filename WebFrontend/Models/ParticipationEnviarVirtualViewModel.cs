using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFrontend.Models
{
    public class ParticipationEnviarVirtualViewModel
    {
        public string nombre_par { get; set; }

        public string apellidos_par { get; set; }

        public string email_par { get; set; }

        public string dni_par { get; set; }

        public string telefono_par { get; set; }

        public string nacionalidad_par { get; set; }

        public DateTime? registrofecha_par { get; set; }

        public TimeSpan? registrohora_par { get; set; }

        public int? id_est { get; set; }

        public DateTime? fechaM_par { get; set; }

        public int? usuM_par { get; set; }

        public string url_par { get; set; }

        public int? id_tall { get; set; } 

        public string adjunto_par { get; set; }

        public string pais_par { get; set; }

        public string ip_usuario { get; set; }

        public string dondeconociste_par { get; set; }

        public string llanta { get; set; }

        public string TamanoRueda { get; set; }

        public int? Numero_ruedas_int { get; set; }

        public string Numero_ruedas { get; set; }

        public string comercial_par { get; set; }

        //public List<IFormFile> ImageFiles { get; set; }       
    }
}
