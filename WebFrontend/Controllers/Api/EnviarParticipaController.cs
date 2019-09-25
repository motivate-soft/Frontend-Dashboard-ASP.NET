using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFrontend.Data;
using WebFrontend.Models;

namespace WebFrontend.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/EnviarVirtualParticipa")]
    public class EnviarParticipaController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        public IHttpContextAccessor _accessor;
        private string[] lines;
        private DateTime datetime;
        private string ip;

        public EnviarParticipaController(IHostingEnvironment hostingEnvironment, 
            ApplicationDbContext context, IHttpContextAccessor accessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _accessor = accessor;
            lines = System.IO.File.ReadAllLines(Constants.Config.config_path);
            var timezone = lines[3].Replace("Timezone: ", "");
            var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            datetime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);
            ip = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            lines = System.IO.File.ReadAllLines(@"config.txt");
        }
        [HttpPost]
        public async Task<IActionResult> EnviarAsync([FromBody] ParticipationEnviarVirtualViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var idsession = HttpContext.Session.GetString("idsession");
            try
            {
                string msg = "";
                if (idsession == null)
                {
                    // log for form
                    using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                    {
                        sw.WriteLine(
                            "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                            "id_tall: " + model.id_tall + ", " +
                            "nombre: " + model.nombre_par + ", " +
                            "apellidos: " + model.apellidos_par + ", " +
                            "email: " + model.email_par + ", " +
                            "telefono: " + model.telefono_par + ", " +
                            "nacionalidad: " + model.nacionalidad_par + ", " +
                            "dni: " + model.dni_par + ", " +
                            "dondeconociste: " + model.dondeconociste_par + ", " +
                            "comercial: " + model.comercial_par + ", " +
                            "idseesion: " + idsession + ", " +
                            "action: " + "register form" + ", " +
                            "result: " + "idsession destory"
                            );
                    }
                    return Json(new { success = false, sessiondestroy = true });
                }
                var timezone = lines[3].Replace("Timezone: ", "");
                var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);

                int? premioSelFrnt_par = Convert.ToInt32(Request.Cookies["id_regalo"]);
                string llanta = Request.Cookies["llanta"];
                string llanta_model = Request.Cookies["llanta_model"];
                string tipo = Request.Cookies["tipo"];
                string neumatico = Request.Cookies["neumatico"].ToUpper();
                int? Numero_ruedas_int = Convert.ToInt32(neumatico.Substring(0, 1));
                string talla = Request.Cookies["talla"];

                int? comercial_par = 0;
                if (model.comercial_par == "on")
                {
                    comercial_par = 1;
                }
                int? id_tall = model.id_tall;
                string email_par = model.email_par;
                string nombre_par = model.nombre_par;
                string apellidos_par = model.apellidos_par;
                string dni_par = model.dni_par;
                string telefono_par = model.telefono_par;
                string dondeconociste_par = model.dondeconociste_par;
                string nacionalidad_par = model.nacionalidad_par;
                
                string pais_par = "ES";
                
                int? mywheels = Numero_ruedas_int; //mywheels

                // check limit by email/dni
                var producto = _context.Regalo.Where(r => r.id_regalo.Equals(premioSelFrnt_par)).FirstOrDefault().producto;
                var grupoproducto = _context.Regalo.Where(r => r.id_regalo.Equals(premioSelFrnt_par)).FirstOrDefault().grupoproducto;

                var general = _context.General.SingleOrDefault();

                var chckdni_gen = general.chckdni_gen;
                var chckpremios_gen = general.chckpremios_gen;
                var chckemail_gen = general.chckemail_gen;
                var chckip_gen = general.chckip_gen;
                var chcktelefono_gen = general.chcktelefono_gen;

                var nummaxdni_gen = general.nummaxdni_gen;
                var nummaxemail_gen = general.nummaxemail_gen;
                var nummaxpremios_gen = general.nummaxpremios_gen;
                var nummaxip_gen = general.nummaxip_gen;
                var nummaxtelefono_gen = general.nummaxtelefono_gen;

                if (chckdni_gen == 1)
                {
                    int? totalwheels = 0;
                    var participationes = _context.Participation.AsNoTracking().Where(x => x.id_est.Equals(2) && x.dni_par.Equals(dni_par));
                    foreach (var item in participationes)
                    {
                        totalwheels = totalwheels + item.Numero_ruedas_int;
                    }

                    if (mywheels + totalwheels > nummaxdni_gen)
                    {
                        msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos. Número excedido por DNI";
                        // log for form
                        using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                        {
                            sw.WriteLine(
                                "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                "id_tall: " + id_tall + ", " +
                                "nombre: " + nombre_par + ", " +
                                "apellidos: " + apellidos_par + ", " +
                                "email: " + email_par + ", " +
                                "telefono: " + telefono_par + ", " +
                                "nacionalidad: " + nacionalidad_par + ", " +
                                "dni: " + dni_par + ", " +
                                "dondeconociste: " + dondeconociste_par + ", " +
                                "comercial: " + comercial_par + ", " +
                                "idseesion: " + idsession + ", " +
                                "action: " + "register form" + ", " +
                                "result: " + grupoproducto + " Número excedido por DNI"
                                );
                        }
                        return Json(new { success = false, message = "in case units are over", limit = true });
                    }
                }

                if (chckemail_gen == 1)
                {
                    int? totalwheels = 0;
                    var participationes = _context.Participation.AsNoTracking().Where(x => x.id_est.Equals(2) && x.email_par.Equals(email_par));
                    foreach (var item in participationes)
                    {
                        totalwheels = totalwheels + item.Numero_ruedas_int;
                    }

                    if (mywheels + totalwheels > nummaxemail_gen)
                    {
                        msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos. Número excedido por EMAIL";
                        // log for form
                        using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                        {
                            sw.WriteLine(
                                "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                "id_tall: " + id_tall + ", " +
                                "nombre: " + nombre_par + ", " +
                                "apellidos: " + apellidos_par + ", " +
                                "email: " + email_par + ", " +
                                "telefono: " + telefono_par + ", " +
                                "nacionalidad: " + nacionalidad_par + ", " +
                                "dni: " + dni_par + ", " +
                                "dondeconociste: " + dondeconociste_par + ", " +
                                "comercial: " + comercial_par + ", " +
                                "idseesion: " + idsession + ", " +
                                "action: " + "register form" + ", " +
                                "result: " + grupoproducto + " Número excedido por EMAIL"
                                );
                        }
                        return Json(new { success = false, message = "in case units are over", limit = true });
                    }
                }

                if (chckpremios_gen == 1)
                {
                    int? totalwheels = 0;
                    var participationes = _context.Participation.AsNoTracking().Where(x => x.id_est.Equals(2));
                    foreach (var item in participationes)
                    {
                        totalwheels = totalwheels + item.Numero_ruedas_int;
                    }

                    if (mywheels + totalwheels > nummaxpremios_gen)
                    {
                        msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos. Número excedido PROMO";
                        // log for form
                        using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                        {
                            sw.WriteLine(
                                "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                "id_tall: " + id_tall + ", " +
                                "nombre: " + nombre_par + ", " +
                                "apellidos: " + apellidos_par + ", " +
                                "email: " + email_par + ", " +
                                "telefono: " + telefono_par + ", " +
                                "nacionalidad: " + nacionalidad_par + ", " +
                                "dni: " + dni_par + ", " +
                                "dondeconociste: " + dondeconociste_par + ", " +
                                "comercial: " + comercial_par + ", " +
                                "idseesion: " + idsession + ", " +
                                "action: " + "register form" + ", " +
                                "result: " + grupoproducto + " Número excedido PROMO"
                                );
                        }
                        return Json(new { success = false, message = "in case units are over", limit = true });
                    }
                }

                // new limit
                if (chckip_gen == 1)
                {
                    int? totalwheels = 0;
                    var participationes = _context.Participation.AsNoTracking().Where(x => x.id_est.Equals(2) && x.IP_Usuario.Equals(ip));
                    foreach (var item in participationes)
                    {
                        totalwheels = totalwheels + item.Numero_ruedas_int;
                    }

                    if (mywheels + totalwheels > nummaxip_gen)
                    {
                        msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos. Número excedido por IP";
                        // log for form
                        using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                        {
                            sw.WriteLine(
                                "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                "id_tall: " + id_tall + ", " +
                                "nombre: " + nombre_par + ", " +
                                "apellidos: " + apellidos_par + ", " +
                                "email: " + email_par + ", " +
                                "telefono: " + telefono_par + ", " +
                                "nacionalidad: " + nacionalidad_par + ", " +
                                "dni: " + dni_par + ", " +
                                "dondeconociste: " + dondeconociste_par + ", " +
                                "comercial: " + comercial_par + ", " +
                                "idseesion: " + idsession + ", " +
                                "action: " + "register form" + ", " +
                                "result: " + grupoproducto + " Número excedido por IP"
                                );
                        }
                        return Json(new { success = false, message = "in case units are over", limit = true });
                    }
                }

                if (chcktelefono_gen == 1)
                {
                    int? totalwheels = 0;
                    var participationes = _context.Participation.AsNoTracking().Where(x => x.id_est.Equals(2) && x.telefono_par.Equals(telefono_par));
                    foreach (var item in participationes)
                    {
                        totalwheels = totalwheels + item.Numero_ruedas_int;
                    }

                    if (mywheels + totalwheels > nummaxtelefono_gen)
                    {
                        msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos. Número excedido por TELEPHONE";
                        // log for form
                        using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                        {
                            sw.WriteLine(
                                "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                "id_tall: " + id_tall + ", " +
                                "nombre: " + nombre_par + ", " +
                                "apellidos: " + apellidos_par + ", " +
                                "email: " + email_par + ", " +
                                "telefono: " + telefono_par + ", " +
                                "nacionalidad: " + nacionalidad_par + ", " +
                                "dni: " + dni_par + ", " +
                                "dondeconociste: " + dondeconociste_par + ", " +
                                "comercial: " + comercial_par + ", " +
                                "idseesion: " + idsession + ", " +
                                "action: " + "register form" + ", " +
                                "result: " + grupoproducto + " Número excedido por TELEPHONE"
                                );
                        }
                        return Json(new { success = false, message = "in case units are over", limit = true });
                    }
                }

                var participation = new Participation
                {
                    id_tall = id_tall,
                    nombre_par = nombre_par,
                    apellidos_par = apellidos_par,
                    email_par = email_par,
                    telefono_par = telefono_par,
                    nacionalidad_par = nacionalidad_par,
                    dni_par = dni_par,
                    dondeconociste_par = dondeconociste_par,
                    registrofecha_par = datetime,
                    registrohora_par = datetime.TimeOfDay,
                    id_est = 1,
                    fechaM_par = datetime,
                    tipovehiculo_par = "COCHE",
                    url_par = idsession,
                    premioSelFrnt_par = premioSelFrnt_par,
                    pais_par = pais_par,
                    IP_Usuario = ip,
                    llanta = llanta_model,
                    TamanoRueda = llanta,
                    Numero_ruedas = neumatico,
                    Numero_ruedas_int = Numero_ruedas_int,
                    comercial_par = comercial_par,
                    talla_par = talla,
                    numllantas_par = "cochex" + Numero_ruedas_int
                };

                _context.Participation.Add(participation);
                _context.SaveChanges();

                int id_par = participation.id_par;

                string folderName = "adjuntos/" + id_par;
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                
                string total_adjunto_par = model.adjunto_par;
                string[] total_adjunto_par_list = total_adjunto_par.Split(new Char[] { '|' });

                var updatePar = _context.Participation.Where(p => p.id_par.Equals(id_par)).FirstOrDefault();
                if (updatePar == null)
                {
                    return NotFound();
                }

                string logFileName = "";
                int index = 0;
                foreach (string adjunto_par in total_adjunto_par_list)
                {
                    index = index + 1;
                    
                    string[] test = adjunto_par.Split(new Char[] { ':' });
                    string tempFileName = Path.GetTempPath() + test[0];
                    string fileName = test[1];
                    
                    string fullPath = Path.Combine(newPath, fileName);
                    fileName = idsession + "_" + index + "_" + Path.GetFileNameWithoutExtension(fullPath);
                    var extension = Path.GetExtension(fullPath);
                    fileName = fileName + extension;
                    fullPath = Path.Combine(newPath, fileName);
                    System.IO.File.Move(tempFileName, fullPath);
                    logFileName = logFileName + "| " + fileName;
                    switch (index)
                    {
                        case 1:
                            updatePar.adjunto1_par = fileName;
                            break;
                        case 2:
                            updatePar.adjunto2_par = fileName;
                            break;
                        case 3:
                            updatePar.adjunto3_par = fileName;
                            break;
                        case 4:
                            updatePar.adjunto4_par = fileName;
                            break;
                        case 5:
                            updatePar.adjunto5_par = fileName;
                            break;
                        default:
                            updatePar.adjunto1_par = fileName;
                            break;
                    }
                    _context.SaveChanges();
                }

                // send email and sms
                
                var url = "";
                var DOMAIN01 = "DOMAIN01";
                var DOMAIN02 = "DOMAIN02";
                var sms = "";
                Random random = new Random();
                DOMAIN01 = lines[4].Replace("DOMAIN01: ", "");
                url = idsession + "&z=" + id_par;

                if (pais_par == "ES")
                {
                    if (tipo == "VIRTUAL")
                    {
                        //curl
                        var httpClient = new HttpClient();
                        httpClient.Timeout = TimeSpan.FromSeconds(5);
                        var request = new HttpRequestMessage(new HttpMethod("POST"), "http://tinyurl.com/api-create.php?url=" + url);
                        var response = await httpClient.SendAsync(request);
                        HttpContent responseContent = response.Content;
                        var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
                        var http = await reader.ReadToEndAsync();

                        var FechaCaducidad = "05/05/2020";
                        FechaCaducidad = lines[0].Replace("FechaCaducidad: ", "");
                        sms = "Hola "+nombre_par + " "+ apellidos_par + "Tu participacion en la promocion MICHELIN  & Tu ha sido registrada con exito.En 5 dias laborables procederemos a validar tu participacio";

                        InfoBitSmsManager _infoBitSmsManager = new InfoBitSmsManager();
                        if(await _infoBitSmsManager.SendSMS(sms, "+34" + telefono_par))
                        {
                            // log for sms
                            using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                            {
                                sw.WriteLine(
                                    "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                    "id_tall: " + id_tall + ", " +
                                    "nombre: " + nombre_par + ", " +
                                    "apellidos: " + apellidos_par + ", " +
                                    "email: " + email_par + ", " +
                                    "telefono: " + telefono_par + ", " +
                                    "nacionalidad: " + nacionalidad_par + ", " +
                                    "dni: " + dni_par + ", " +
                                    "dondeconociste: " + dondeconociste_par + ", " +
                                    "comercial: " + comercial_par + ", " +
                                    "idseesion: " + idsession + ", " +
                                    "attachment: " + logFileName + ", " +
                                    "action: " + "register form" + ", " +
                                    "result: " + "send sms - " + telefono_par
                                    );
                            }
                        }
                        else
                        {
                            // log for sms
                            using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                            {
                                sw.WriteLine(
                                    "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                    "id_tall: " + id_tall + ", " +
                                    "nombre: " + nombre_par + ", " +
                                    "apellidos: " + apellidos_par + ", " +
                                    "email: " + email_par + ", " +
                                    "telefono: " + telefono_par + ", " +
                                    "nacionalidad: " + nacionalidad_par + ", " +
                                    "dni: " + dni_par + ", " +
                                    "dondeconociste: " + dondeconociste_par + ", " +
                                    "comercial: " + comercial_par + ", " +
                                    "idseesion: " + idsession + ", " +
                                    "attachment: " + logFileName + ", " +
                                    "action: " + "register form" + ", " +
                                    "result: " + "send sms - fail"
                                    );
                            }
                        }

                    }
                    else
                    {
                        sms = "Hola " + nombre_par + " " + apellidos_par + "Tu participacion en la promocion MICHELIN  & Tu ha sido registrada con exito.En 5 dias laborables procederemos a validar tu participacio";
                        InfoBitSmsManager _infoBitSmsManager = new InfoBitSmsManager();
                        await _infoBitSmsManager.SendSMS(sms, "+34" + telefono_par);

                        
                        //await _infoBitSmsManager.SendSMS(sms, "+86" + telefono_par);
                    }
                }
                

                var pieza = "";
                var asunto = "";
                if (pais_par == "ES")
                {
                    url = idsession + "&z=" + id_par;
                    pieza = "82201";
                    if (tipo == "VIRTUAL") pieza = "82201";
                    asunto = "La compra de neumáticos MICHELIN tiene premio";
                }
                else
                {
                    url = idsession + "&z=" + id_par;
                    pieza = "82201";
                    if (tipo == "VIRTUAL") pieza = "82201";
                    asunto = "A compra de pneus MICHELIN tem um prêmio";
                }

                var cemail = email_par;
                var nombre = nombre_par + " " + apellidos_par;
                var dato_20 = "Campaña Michelin";
                byte[] nombre_bytes = Encoding.UTF8.GetBytes(nombre);
                nombre = Encoding.UTF8.GetString(nombre_bytes);

                var regalo = _context.Regalo.Where(x => x.id_regalo.Equals(premioSelFrnt_par)).FirstOrDefault();

                var nombreregalo = ""; // error
                var valorpremio_par = participation.valorpremio_par;
                if (valorpremio_par == null)
                {
                    nombreregalo = regalo.producto;
                }
                else
                {
                    nombreregalo = valorpremio_par;
                }
                byte[] nombreregalo_bytes = Encoding.UTF8.GetBytes(nombreregalo);
                nombreregalo = Encoding.UTF8.GetString(nombreregalo_bytes);

                var taller = _context.Talleres.Where(x => x.id_tall.Equals(id_tall)).SingleOrDefault().razonsocial_tall;
                byte[] taller_bytes = Encoding.UTF8.GetBytes(taller);
                taller = Encoding.UTF8.GetString(taller_bytes);
                var code = "";

                if (await teenvio_actualizar_regalovalidado(cemail, pieza, asunto, nombre, dato_20, cemail, nombreregalo, url, taller))
                {
                    code = "ok";
                    msg = "Participación validada con éxito.";
                    // log for email
                    using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                    {
                        sw.WriteLine(
                            "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                            "id_tall: " + id_tall + ", " +
                            "nombre: " + nombre_par + ", " +
                            "apellidos: " + apellidos_par + ", " +
                            "email: " + email_par + ", " +
                            "telefono: " + telefono_par + ", " +
                            "nacionalidad: " + nacionalidad_par + ", " +
                            "dni: " + dni_par + ", " +
                            "dondeconociste: " + dondeconociste_par + ", " +
                            "comercial: " + comercial_par + ", " +
                            "idseesion: " + idsession + ", " +
                            "attachment: " + logFileName + ", " +
                            "action: " + "register form" + ", " +
                            "result: " + "send email - " + email_par
                            );
                    }
                }
                else
                {
                    code = "ko";
                    msg = "Participación validada. Ha ocurrido un error en el envío del email";
                }
                CookieOptions option = new CookieOptions();
                Response.Cookies.Append("idsession", idsession, option);
                
                // log for form
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id_tall: " + id_tall + ", " +
                        "nombre: " + nombre_par + ", " +
                        "apellidos: " + apellidos_par + ", " +
                        "email: " + email_par + ", " +
                        "telefono: " + telefono_par + ", " +
                        "nacionalidad: " + nacionalidad_par + ", " +
                        "dni: " + dni_par + ", " +
                        "dondeconociste: " + dondeconociste_par + ", " +
                        "comercial: " + comercial_par + ", " +
                        "idseesion: " + idsession + ", " +
                        "attachment: " + logFileName + ", " +
                        "action: " + "register form" + ", " +
                        "result: " + "success !"
                        );
                }

                return Json(new { success = true, message = "ok" });
            }
            catch (Exception ex)
            {
                // log for form
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id_tall: " + model.id_tall + ", " +
                        "nombre: " + model.nombre_par + ", " +
                        "apellidos: " + model.apellidos_par + ", " +
                        "email: " + model.email_par + ", " +
                        "telefono: " + model.telefono_par + ", " +
                        "nacionalidad: " + model.nacionalidad_par + ", " +
                        "dni: " + model.dni_par + ", " +
                        "dondeconociste: " + model.dondeconociste_par + ", " +
                        "comercial: " + model.comercial_par + ", " +
                        "idseesion: " + idsession + ", " +
                        "action: " + "register form" + ", " +
                        "result: " + ex.Message
                        );
                }
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<bool> teenvio_actualizar_regalovalidado(String cemail, String pieza_teenvio, String asunto_teenvio, String nombre_teenvio, String dato_20, String email, String nombreregalo, String urlregalo, String nombre_taller)
        {
            var action = "contact_save";
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://master2.teenvio.com/v4/public/api/post/");
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(action), "action");
            multipartContent.Add(new StringContent("chequemotiva"), "plan");
            multipartContent.Add(new StringContent("daniel"), "user");
            multipartContent.Add(new StringContent("Xmsfj"), "pass");
            multipartContent.Add(new StringContent("455"), "rid");
            multipartContent.Add(new StringContent(cemail), "email");
            multipartContent.Add(new StringContent(dato_20), "dato_20");
            multipartContent.Add(new StringContent(pieza_teenvio), "pid");
            request.Content = multipartContent;

            var response = await httpClient.SendAsync(request);
            HttpContent responseContent = response.Content;
            var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
            var contact_id = await reader.ReadToEndAsync();
            if ((int)response.StatusCode == 200)
            {
                contact_id = contact_id.Replace("OK: ", "");
                if (await teenvio_enviar_regalovalidado(contact_id, pieza_teenvio, asunto_teenvio, nombre_teenvio, email, nombreregalo, urlregalo, nombre_taller))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<bool> teenvio_enviar_regalovalidado(String contact_id, String pieza_teenvio, String asunto_teenvio, String nombre_teenvio, String email, String nombreregalo, String urlregalo, String nombre_taller)
        {
            var action = "send_campaign";

            string vars = "{\"var_nombre\":\"" + nombre_teenvio + "\", \"var_url\":\""+ urlregalo + "\"}";

            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://master2.teenvio.com/v4/public/api/post/");
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(action), "action");
            multipartContent.Add(new StringContent("chequemotiva"), "plan");
            multipartContent.Add(new StringContent("daniel"), "user");
            multipartContent.Add(new StringContent("Xmsfj"), "pass");
            multipartContent.Add(new StringContent("455"), "rid");
            multipartContent.Add(new StringContent(contact_id), "contact_id");
            multipartContent.Add(new StringContent("Michelin"), "name");
            multipartContent.Add(new StringContent(pieza_teenvio), "pid");
            multipartContent.Add(new StringContent(asunto_teenvio), "subject");
            multipartContent.Add(new StringContent(vars, Encoding.UTF8, "application/json"), "vars");

            request.Content = multipartContent;

            var response = await httpClient.SendAsync(request);
            HttpContent responseContent = response.Content;
            var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
            var id_envio = await reader.ReadToEndAsync();
            if ((int)response.StatusCode == 200)
            {
                id_envio = contact_id.Replace("OK: ", "");
                return true;
            }
            return false;
        }
    }

    [Produces("application/json")]
    [Route("api/EnviarFisicoParticipa")]
    public class EnviarParticipaFisicoController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        public IHttpContextAccessor _accessor;
        private string[] lines;
        private DateTime datetime;
        private string ip;

        public EnviarParticipaFisicoController(IHostingEnvironment hostingEnvironment,
            ApplicationDbContext context, IHttpContextAccessor accessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _accessor = accessor;
            lines = System.IO.File.ReadAllLines(Constants.Config.config_path);
            var timezone = lines[3].Replace("Timezone: ", "");
            var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            datetime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);
            ip = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            lines = System.IO.File.ReadAllLines(@"config.txt");
        }
        [HttpPost]
        public async Task<IActionResult> EnviarAsync([FromBody] ParticipationEnviarFisicoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var idsession = HttpContext.Session.GetString("idsession");
            try
            {
                string msg = "";
                
                if (idsession == null)
                {
                    // log for form
                    using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                    {
                        sw.WriteLine(
                            "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                            "id_tall: " + model.id_tall + ", " +
                            "nombre: " + model.nombre_par + ", " +
                            "apellidos: " + model.apellidos_par + ", " +
                            "email: " + model.email_par + ", " +
                            "telefono: " + model.telefono_par + ", " +
                            "direccion: " + model.direccion_par + ", " +
                            "localidad: " + model.localidad_par + ", " +
                            "codigopostal: " + model.codigopostal_par + ", " +
                            "provincia: " + model.provincia_par + ", " +
                            "nacionalidad: " + model.nacionalidad_par + ", " +
                            "dni: " + model.dni_par + ", " +
                            "dondeconociste: " + model.dondeconociste_par + ", " +
                            "comercial: " + model.comercial_par + ", " +
                            "idseesion: " + idsession + ", " +
                            "action: " + "register form" + ", " +
                            "result: " + "idsession destory"
                            );
                    }
                    return Json(new { success = false, sessiondestroy = true });
                }
                var timezone = lines[3].Replace("Timezone: ", "");
                var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);

                int? premioSelFrnt_par = Convert.ToInt32(Request.Cookies["id_regalo"]);
                string llanta = Request.Cookies["llanta"];
                string llanta_model = Request.Cookies["llanta_model"];
                string tipo = Request.Cookies["tipo"];
                string neumatico = Request.Cookies["neumatico"].ToUpper();
                int? Numero_ruedas_int = Convert.ToInt32(neumatico.Substring(0, 1));
                string talla = Request.Cookies["talla"];

                int? comercial_par = 0;
                if (model.comercial_par == "on")
                {
                    comercial_par = 1;
                }
                int? id_tall =  model.id_tall;
                string email_par = model.email_par;
                string nombre_par = model.nombre_par;
                string apellidos_par = model.apellidos_par;
                string dni_par = model.dni_par;
                string telefono_par = model.telefono_par;
                string dondeconociste_par = model.dondeconociste_par;
                string nacionalidad_par = model.nacionalidad_par;

                string direccion_par = model.direccion_par;
                string localidad_par = model.localidad_par;
                string codigopostal_par = model.codigopostal_par;
                string provincia_par = model.provincia_par;

                string pais_par = "ES";
                
                int? mywheels = Numero_ruedas_int; //mywheels

                // check limit by email/dni
                var producto = _context.Regalo.Where(r => r.id_regalo.Equals(premioSelFrnt_par)).FirstOrDefault().producto;
                var grupoproducto = _context.Regalo.Where(r => r.id_regalo.Equals(premioSelFrnt_par)).FirstOrDefault().grupoproducto;

                var general = _context.General.SingleOrDefault();

                var chckdni_gen = general.chckdni_gen;
                var chckpremios_gen = general.chckpremios_gen;
                var chckemail_gen = general.chckemail_gen;
                var chckip_gen = general.chckip_gen;
                var chcktelefono_gen = general.chcktelefono_gen;

                var nummaxdni_gen = general.nummaxdni_gen;
                var nummaxemail_gen = general.nummaxemail_gen;
                var nummaxpremios_gen = general.nummaxpremios_gen;
                var nummaxip_gen = general.nummaxip_gen;
                var nummaxtelefono_gen = general.nummaxtelefono_gen;

                if (chckdni_gen == 1)
                {
                    int? totalwheels = 0;
                    var participationes = _context.Participation.AsNoTracking().Where(x => x.id_est.Equals(2) && x.dni_par.Equals(dni_par));
                    foreach (var item in participationes)
                    {
                        totalwheels = totalwheels + item.Numero_ruedas_int;
                    }

                    if (mywheels + totalwheels > nummaxdni_gen)
                    {
                        msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos. Número excedido por DNI";
                        // log for form
                        using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                        {
                            sw.WriteLine(
                                "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                "id_tall: " + id_tall + ", " +
                                "nombre: " + nombre_par + ", " +
                                "apellidos: " + apellidos_par + ", " +
                                "email: " + email_par + ", " +
                                "telefono: " + telefono_par + ", " +
                                "nacionalidad: " + nacionalidad_par + ", " +
                                "dni: " + dni_par + ", " +
                                "dondeconociste: " + dondeconociste_par + ", " +
                                "comercial: " + comercial_par + ", " +
                                "idseesion: " + idsession + ", " +
                                "action: " + "register form" + ", " +
                                "result: " + grupoproducto + " Número excedido por DNI"
                                );
                        }
                        return Json(new { success = false, message = "in case units are over", limit = true });
                    }
                }

                if (chckemail_gen == 1)
                {
                    int? totalwheels = 0;
                    var participationes = _context.Participation.AsNoTracking().Where(x => x.id_est.Equals(2) && x.email_par.Equals(email_par));
                    foreach (var item in participationes)
                    {
                        totalwheels = totalwheels + item.Numero_ruedas_int;
                    }

                    if (mywheels + totalwheels > nummaxemail_gen)
                    {
                        msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos. Número excedido por EMAIL";
                        // log for form
                        using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                        {
                            sw.WriteLine(
                                "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                "id_tall: " + id_tall + ", " +
                                "nombre: " + nombre_par + ", " +
                                "apellidos: " + apellidos_par + ", " +
                                "email: " + email_par + ", " +
                                "telefono: " + telefono_par + ", " +
                                "nacionalidad: " + nacionalidad_par + ", " +
                                "dni: " + dni_par + ", " +
                                "dondeconociste: " + dondeconociste_par + ", " +
                                "comercial: " + comercial_par + ", " +
                                "idseesion: " + idsession + ", " +
                                "action: " + "register form" + ", " +
                                "result: " + grupoproducto + " Número excedido por EMAIL"
                                );
                        }
                        return Json(new { success = false, message = "in case units are over", limit = true });
                    }
                }

                if (chckpremios_gen == 1)
                {
                    int? totalwheels = 0;
                    var participationes = _context.Participation.AsNoTracking().Where(x => x.id_est.Equals(2));
                    foreach (var item in participationes)
                    {
                        totalwheels = totalwheels + item.Numero_ruedas_int;
                    }

                    if (mywheels + totalwheels > nummaxpremios_gen)
                    {
                        msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos. Número excedido PROMO";
                        // log for form
                        using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                        {
                            sw.WriteLine(
                                "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                "id_tall: " + id_tall + ", " +
                                "nombre: " + nombre_par + ", " +
                                "apellidos: " + apellidos_par + ", " +
                                "email: " + email_par + ", " +
                                "telefono: " + telefono_par + ", " +
                                "nacionalidad: " + nacionalidad_par + ", " +
                                "dni: " + dni_par + ", " +
                                "dondeconociste: " + dondeconociste_par + ", " +
                                "comercial: " + comercial_par + ", " +
                                "idseesion: " + idsession + ", " +
                                "action: " + "register form" + ", " +
                                "result: " + grupoproducto + " Número excedido PROMO"
                                );
                        }
                        return Json(new { success = false, message = "in case units are over", limit = true });
                    }
                }

                // new limit
                if (chckip_gen == 1)
                {
                    int? totalwheels = 0;
                    var participationes = _context.Participation.AsNoTracking().Where(x => x.id_est.Equals(2) && x.IP_Usuario.Equals(ip));
                    foreach (var item in participationes)
                    {
                        totalwheels = totalwheels + item.Numero_ruedas_int;
                    }

                    if (mywheels + totalwheels > nummaxip_gen)
                    {
                        msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos. Número excedido por IP";
                        // log for form
                        using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                        {
                            sw.WriteLine(
                                "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                "id_tall: " + id_tall + ", " +
                                "nombre: " + nombre_par + ", " +
                                "apellidos: " + apellidos_par + ", " +
                                "email: " + email_par + ", " +
                                "telefono: " + telefono_par + ", " +
                                "nacionalidad: " + nacionalidad_par + ", " +
                                "dni: " + dni_par + ", " +
                                "dondeconociste: " + dondeconociste_par + ", " +
                                "comercial: " + comercial_par + ", " +
                                "idseesion: " + idsession + ", " +
                                "action: " + "register form" + ", " +
                                "result: " + grupoproducto + " Número excedido por IP"
                                );
                        }
                        return Json(new { success = false, message = "in case units are over", limit = true });
                    }
                }

                if (chcktelefono_gen == 1)
                {
                    int? totalwheels = 0;
                    var participationes = _context.Participation.AsNoTracking().Where(x => x.id_est.Equals(2) && x.telefono_par.Equals(telefono_par));
                    foreach (var item in participationes)
                    {
                        totalwheels = totalwheels + item.Numero_ruedas_int;
                    }

                    if (mywheels + totalwheels > nummaxtelefono_gen)
                    {
                        msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos. Número excedido por TELEPHONE";
                        // log for form
                        using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                        {
                            sw.WriteLine(
                                "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                "id_tall: " + id_tall + ", " +
                                "nombre: " + nombre_par + ", " +
                                "apellidos: " + apellidos_par + ", " +
                                "email: " + email_par + ", " +
                                "telefono: " + telefono_par + ", " +
                                "nacionalidad: " + nacionalidad_par + ", " +
                                "dni: " + dni_par + ", " +
                                "dondeconociste: " + dondeconociste_par + ", " +
                                "comercial: " + comercial_par + ", " +
                                "idseesion: " + idsession + ", " +
                                "action: " + "register form" + ", " +
                                "result: " + grupoproducto + " Número excedido por TELEPHONE"
                                );
                        }
                        return Json(new { success = false, message = "in case units are over", limit = true });
                    }
                }

                // check limit by talla (L or XL)
                if (grupoproducto != null)
                {
                    var a = _context.Participation.Where(x => x.id_est.Equals(2) && x.valorpremio_par.Equals(producto)).Count();
                    var regaloslimite = _context.Regaloslimite.Where(x => x.grupoproducto_lim.Equals(grupoproducto)).SingleOrDefault();
                    if (regaloslimite != null)
                    {
                        var b = regaloslimite.unidades_lim;
                        if (a < b) // count < limit
                        {
                            if (grupoproducto == "CHAQUETA" || grupoproducto == "SUDADERA")
                            {
                                a = _context.Participation.Where(x => x.id_est.Equals(2) && x.valorpremio_par.Equals(producto) && x.talla_par.Equals(talla)).Count();
                                b = 50;

                                if (a > b)
                                {
                                    msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos.";
                                    // log for form
                                    using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                                    {
                                        sw.WriteLine(
                                            "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                            "id_tall: " + id_tall + ", " +
                                            "nombre: " + nombre_par + ", " +
                                            "apellidos: " + apellidos_par + ", " +
                                            "email: " + email_par + ", " +
                                            "telefono: " + telefono_par + ", " +
                                            "nacionalidad: " + nacionalidad_par + ", " +
                                            "dni: " + dni_par + ", " +
                                            "dondeconociste: " + dondeconociste_par + ", " +
                                            "comercial: " + comercial_par + ", " +
                                            "idseesion: " + idsession + ", " +
                                            "action: " + "register form" + ", " +
                                            "result: " + grupoproducto + " in case units are over"
                                            );
                                    }
                                    return Json(new { success = false, message = "in case units are over", limit = true });
                                }
                            }
                        }
                        else // count > limit
                        {
                            msg = "La participación no se puede validar porque cumple con alguno de los limites/restricciones definidos.";
                            // log for form
                            using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                            {
                                sw.WriteLine(
                                    "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                    "id_tall: " + id_tall + ", " +
                                    "nombre: " + nombre_par + ", " +
                                    "apellidos: " + apellidos_par + ", " +
                                    "email: " + email_par + ", " +
                                    "telefono: " + telefono_par + ", " +
                                    "nacionalidad: " + nacionalidad_par + ", " +
                                    "dni: " + dni_par + ", " +
                                    "dondeconociste: " + dondeconociste_par + ", " +
                                    "comercial: " + comercial_par + ", " +
                                    "idseesion: " + idsession + ", " +
                                    "action: " + "register form" + ", " +
                                    "result: " + grupoproducto + " in case units are over"
                                    );
                            }
                            return Json(new { success = false, message = "in case units are over", limit = true });
                        }
                    }

                }

                var participation = new Participation ()
                {
                    id_tall = id_tall,
                    nombre_par = nombre_par,
                    apellidos_par = apellidos_par,
                    email_par = email_par,
                    telefono_par = telefono_par,
                    direccion_par = direccion_par,
                    localidad_par = localidad_par,
                    codigopostal_par = codigopostal_par,
                    provincia_par = provincia_par,
                    nacionalidad_par = nacionalidad_par,
                    dni_par = dni_par,
                    dondeconociste_par = dondeconociste_par,
                    registrofecha_par = datetime,
                    registrohora_par = datetime.TimeOfDay,
                    id_est = 1,
                    fechaM_par = datetime,
                    tipovehiculo_par = "COCHE",
                    url_par = idsession,
                    premioSelFrnt_par = premioSelFrnt_par,
                    pais_par = pais_par,
                    IP_Usuario = ip,
                    llanta = llanta_model,
                    TamanoRueda = llanta,
                    Numero_ruedas = neumatico,
                    Numero_ruedas_int = Numero_ruedas_int,
                    comercial_par = comercial_par,
                    talla_par  = talla,
                    numllantas_par = "cochex" + Numero_ruedas_int
                };

                _context.Participation.Add(participation);
                _context.SaveChanges();

                int id_par = participation.id_par;

                string folderName = "adjuntos/" + id_par;
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                
                string total_adjunto_par = model.adjunto_par;
                string[] total_adjunto_par_list = total_adjunto_par.Split(new Char[] { '|' });

                var updatePar = _context.Participation.Where(p => p.id_par.Equals(id_par)).FirstOrDefault();
                if (updatePar == null)
                {
                    return NotFound();
                }

                string logFileName = "";
                int index = 0;
                foreach (string adjunto_par in total_adjunto_par_list)
                {
                    index = index + 1;

                    string[] test = adjunto_par.Split(new Char[] { ':' });
                    string tempFileName = Path.GetTempPath() + test[0];
                    string fileName = test[1];

                    string fullPath = Path.Combine(newPath, fileName);
                    fileName = idsession + "_" + index + "_" + Path.GetFileNameWithoutExtension(fullPath);
                    var extension = Path.GetExtension(fullPath);
                    fileName = fileName + extension;
                    fullPath = Path.Combine(newPath, fileName);
                    System.IO.File.Move(tempFileName, fullPath);
                    logFileName = logFileName + "| " + fileName;
                    switch (index)
                    {
                        case 1:
                            updatePar.adjunto1_par = fileName;
                            break;
                        case 2:
                            updatePar.adjunto2_par = fileName;
                            break;
                        case 3:
                            updatePar.adjunto3_par = fileName;
                            break;
                        case 4:
                            updatePar.adjunto4_par = fileName;
                            break;
                        case 5:
                            updatePar.adjunto5_par = fileName;
                            break;
                        default:
                            updatePar.adjunto1_par = fileName;
                            break;
                    }
                    _context.SaveChanges();
                }


                // send email and sms
                

                var url = "";
                var DOMAIN01 = "DOMAIN01";
                var DOMAIN02 = "DOMAIN02";
                var sms = "";
                Random random = new Random();
                DOMAIN01 = lines[4].Replace("DOMAIN01: ", "");
                url = idsession + "&z=" + id_par;
                if (pais_par == "ES")
                {
                    if (tipo == "VIRTUAL")
                    {
                        //curl
                        var httpClient = new HttpClient();
                        httpClient.Timeout = TimeSpan.FromSeconds(5);
                        var request = new HttpRequestMessage(new HttpMethod("POST"), "http://tinyurl.com/api-create.php?url=" + url);
                        var response = await httpClient.SendAsync(request);
                        HttpContent responseContent = response.Content;
                        var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
                        var http = await reader.ReadToEndAsync();

                        var FechaCaducidad = "05/05/2020";
                        FechaCaducidad = lines[0].Replace("FechaCaducidad: ", "");
                        sms = "Hola " + nombre_par + " " + apellidos_par + "Tu participacion en la promocion MICHELIN  & Tu ha sido registrada con exito.En 5 dias laborables procederemos a validar tu participacio";

                        InfoBitSmsManager _infoBitSmsManager = new InfoBitSmsManager();
                        await _infoBitSmsManager.SendSMS(sms, "+34" + telefono_par);
                        //await _infoBitSmsManager.SendSMS(sms, "+86" + telefono_par);

                    }
                    else
                    {
                        sms = "Hola " + nombre_par + " " + apellidos_par + "Tu participacion en la promocion MICHELIN  & Tu ha sido registrada con exito.En 5 dias laborables procederemos a validar tu participacio";
                        InfoBitSmsManager _infoBitSmsManager = new InfoBitSmsManager();
                        if(await _infoBitSmsManager.SendSMS(sms, "+34" + telefono_par))
                        {
                            // log for sms
                            using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                            {
                                sw.WriteLine(
                                    "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                    "id_tall: " + id_tall + ", " +
                                    "nombre: " + nombre_par + ", " +
                                    "apellidos: " + apellidos_par + ", " +
                                    "email: " + email_par + ", " +
                                    "telefono: " + telefono_par + ", " +
                                    "nacionalidad: " + nacionalidad_par + ", " +
                                    "dni: " + dni_par + ", " +
                                    "dondeconociste: " + dondeconociste_par + ", " +
                                    "comercial: " + comercial_par + ", " +
                                    "idseesion: " + idsession + ", " +
                                    "attachment: " + logFileName + ", " +
                                    "action: " + "register form" + ", " +
                                    "result: " + "send sms - " + telefono_par
                                    );
                            }
                        }
                        else
                        {
                            // log for sms
                            using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                            {
                                sw.WriteLine(
                                    "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                                    "id_tall: " + id_tall + ", " +
                                    "nombre: " + nombre_par + ", " +
                                    "apellidos: " + apellidos_par + ", " +
                                    "email: " + email_par + ", " +
                                    "telefono: " + telefono_par + ", " +
                                    "nacionalidad: " + nacionalidad_par + ", " +
                                    "dni: " + dni_par + ", " +
                                    "dondeconociste: " + dondeconociste_par + ", " +
                                    "comercial: " + comercial_par + ", " +
                                    "idseesion: " + idsession + ", " +
                                    "attachment: " + logFileName + ", " +
                                    "action: " + "register form" + ", " +
                                    "result: " + "send sms - fail"
                                    );
                            }
                        }
                        
                        //await _infoBitSmsManager.SendSMS(sms, "+86" + telefono_par);
                    }
                }
                
                var pieza = "";
                var asunto = "";
                if (pais_par == "ES")
                {
                    url = idsession + "&z=" + id_par;
                    pieza = "82201";
                    if (tipo == "VIRTUAL") pieza = "82201";
                    asunto = "La compra de neumáticos MICHELIN tiene premio";
                }
                else
                {
                    //url = "https://" + DOMAIN02 + "/mi-participacion?u=" + url_par + "&z=" + id_par + "&xa=" + +random.Next();
                    url = idsession + "&z=" + id_par;
                    pieza = "82201";
                    if (tipo == "VIRTUAL") pieza = "82201";
                    asunto = "A compra de pneus MICHELIN tem um prêmio";
                }

                var cemail = email_par;
                var nombre = nombre_par + apellidos_par;
                var dato_20 = "Campaña Michelin";
                byte[] nombre_bytes = Encoding.UTF8.GetBytes(nombre);
                nombre = Encoding.UTF8.GetString(nombre_bytes);

                var regalo = _context.Regalo.Where(x => x.id_regalo.Equals(premioSelFrnt_par)).FirstOrDefault();

                var nombreregalo = ""; // error
                var valorpremio_par = participation.valorpremio_par;
                if (valorpremio_par == null)
                {
                    nombreregalo = regalo.producto;
                }
                else
                {
                    nombreregalo = valorpremio_par;
                }
                byte[] nombreregalo_bytes = Encoding.UTF8.GetBytes(nombreregalo);
                nombreregalo = Encoding.UTF8.GetString(nombreregalo_bytes);

                var taller = _context.Talleres.Where(x => x.id_tall.Equals(id_tall)).SingleOrDefault().razonsocial_tall;
                byte[] taller_bytes = Encoding.UTF8.GetBytes(taller);
                taller = Encoding.UTF8.GetString(taller_bytes);
                var code = "";

                if (await teenvio_actualizar_regalovalidado(cemail, pieza, asunto, nombre, dato_20, cemail, nombreregalo, url, taller))
                {
                    code = "ok";
                    msg = "Participación validada con éxito.";
                    // log for email
                    using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                    {
                        sw.WriteLine(
                            "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                            "id_tall: " + id_tall + ", " +
                            "nombre: " + nombre_par + ", " +
                            "apellidos: " + apellidos_par + ", " +
                            "email: " + email_par + ", " +
                            "telefono: " + telefono_par + ", " +
                            "nacionalidad: " + nacionalidad_par + ", " +
                            "dni: " + dni_par + ", " +
                            "dondeconociste: " + dondeconociste_par + ", " +
                            "comercial: " + comercial_par + ", " +
                            "idseesion: " + idsession + ", " +
                            "attachment: " + logFileName + ", " +
                            "action: " + "register form" + ", " +
                            "result: " + "send email - " + email_par
                            );
                    }
                }
                else
                {
                    code = "ko";
                    msg = "Participación validada. Ha ocurrido un error en el envío del email";
                }

                // log for form
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id_tall: " + id_tall + ", " +
                        "nombre: " + nombre_par + ", " +
                        "apellidos: " + apellidos_par + ", " +
                        "email: " + email_par + ", " +
                        "telefono: " + telefono_par + ", " +
                        "direccion: " + direccion_par + ", " +
                        "localidad: " + localidad_par + ", " +
                        "codigopostal: " + codigopostal_par + ", " +
                        "provincia: " + provincia_par + ", " +
                        "nacionalidad: " + nacionalidad_par + ", " +
                        "dni: " + dni_par + ", " +
                        "dondeconociste: " + dondeconociste_par + ", " +
                        "comercial: " + comercial_par + ", " +
                        "attachment: " + logFileName + ", " +
                        "idseesion: " + idsession + ", " +
                        "action: " + "register form" + ", " +
                        "result: " + "success !"
                        );
                }

                return Json(new { success = true, message = "ok" });
            }
            catch (Exception ex)
            {
                // log for form
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip +", " +
                        "id_tall: " + model.id_tall + ", " +
                        "nombre: " + model.nombre_par + ", " +
                        "apellidos: " + model.apellidos_par + ", " +
                        "email: " + model.email_par + ", " +
                        "telefono: " + model.telefono_par + ", " +
                        "direccion: " + model.direccion_par + ", " +
                        "localidad: " + model.localidad_par + ", " +
                        "codigopostal: " + model.codigopostal_par + ", " +
                        "provincia: " + model.provincia_par + ", " +
                        "nacionalidad: " + model.nacionalidad_par + ", " +
                        "dni: " + model.dni_par + ", " +
                        "dondeconociste: " + model.dondeconociste_par + ", " +
                        "comercial: " + model.comercial_par + ", " +
                        "idseesion: " + idsession + ", " +
                        "action: " + "register form" + ", " +
                        "result: " + ex.Message
                        );
                }
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<bool> teenvio_actualizar_regalovalidado(String cemail, String pieza_teenvio, String asunto_teenvio, String nombre_teenvio, String dato_20, String email, String nombreregalo, String urlregalo, String nombre_taller)
        {
            var action = "contact_save";
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://master2.teenvio.com/v4/public/api/post/");
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(action), "action");
            multipartContent.Add(new StringContent("chequemotiva"), "plan");
            multipartContent.Add(new StringContent("daniel"), "user");
            multipartContent.Add(new StringContent("Xmsfj"), "pass");
            multipartContent.Add(new StringContent("455"), "rid");
            multipartContent.Add(new StringContent(cemail), "email");
            multipartContent.Add(new StringContent(dato_20), "dato_20");
            multipartContent.Add(new StringContent(pieza_teenvio), "pid");
            request.Content = multipartContent;

            var response = await httpClient.SendAsync(request);
            HttpContent responseContent = response.Content;
            var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
            var contact_id = await reader.ReadToEndAsync();
            if ((int)response.StatusCode == 200)
            {
                contact_id = contact_id.Replace("OK: ", "");
                if (await teenvio_enviar_regalovalidado(contact_id, pieza_teenvio, asunto_teenvio, nombre_teenvio, email, nombreregalo, urlregalo, nombre_taller))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<bool> teenvio_enviar_regalovalidado(String contact_id, String pieza_teenvio, String asunto_teenvio, String nombre_teenvio, String email, String nombreregalo, String urlregalo, String nombre_taller)
        {
            var action = "send_campaign";

            string vars = "{\"var_nombre\":\"" + nombre_teenvio + "\", \"var_url\":\"" + urlregalo + "\"}";

            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://master2.teenvio.com/v4/public/api/post/");
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(action), "action");
            multipartContent.Add(new StringContent("chequemotiva"), "plan");
            multipartContent.Add(new StringContent("daniel"), "user");
            multipartContent.Add(new StringContent("Xmsfj"), "pass");
            multipartContent.Add(new StringContent("455"), "rid");
            multipartContent.Add(new StringContent(contact_id), "contact_id");
            multipartContent.Add(new StringContent("Michelin"), "name");
            multipartContent.Add(new StringContent(pieza_teenvio), "pid");
            multipartContent.Add(new StringContent(asunto_teenvio), "subject");
            multipartContent.Add(new StringContent(vars, Encoding.UTF8, "application/json"), "vars");

            request.Content = multipartContent;

            var response = await httpClient.SendAsync(request);
            HttpContent responseContent = response.Content;
            var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
            var id_envio = await reader.ReadToEndAsync();
            if ((int)response.StatusCode == 200)
            {
                id_envio = contact_id.Replace("OK: ", "");
                return true;
            }
            return false;
        }
    }

    [Produces("application/json")]
    [Route("api/EnviarResendParticipa")]
    public class EnviarResendParticipaController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        public IHttpContextAccessor _accessor;
        private string[] lines;
        private DateTime datetime;
        private string ip;

        public EnviarResendParticipaController(IHostingEnvironment hostingEnvironment,
            ApplicationDbContext context, IHttpContextAccessor accessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _accessor = accessor;
            lines = System.IO.File.ReadAllLines(Constants.Config.config_path);
            var timezone = lines[3].Replace("Timezone: ", "");
            var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            datetime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, targetTimeZone);
            ip = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            lines = System.IO.File.ReadAllLines(@"config.txt");
        }
        [HttpPost]
        public IActionResult Enviar([FromBody] ParticipationEnviarResendViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                string url_par = model.url_par;
                var participation = _context.Participation.Where(p => p.url_par.Equals(url_par)).FirstOrDefault();
                int id_par = participation.id_par;
                string folderName = "adjuntos/" + id_par;
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (Directory.Exists(newPath))
                {
                    Directory.Delete(newPath, true);
                }
                Directory.CreateDirectory(newPath);

                string total_adjunto_par = model.adjunto_par;
                string[] total_adjunto_par_list = total_adjunto_par.Split(new Char[] { '|' });

                string logFileName = "";
                int index = 0;
                foreach (string adjunto_par in total_adjunto_par_list)
                {
                    index = index + 1;

                    string[] test = adjunto_par.Split(new Char[] { ':' });
                    string tempFileName = Path.GetTempPath() + test[0];
                    string fileName = test[1];

                    string fullPath = Path.Combine(newPath, fileName);
                    fileName = url_par + "_" + index + "_" + Path.GetFileNameWithoutExtension(fullPath);
                    var extension = Path.GetExtension(fullPath);
                    fileName = fileName + extension;
                    fullPath = Path.Combine(newPath, fileName);
                    System.IO.File.Move(tempFileName, fullPath);
                    logFileName = logFileName + "| " + fileName;
                    switch (index)
                    {
                        case 1:
                            participation.adjunto1_par = fileName;
                            break;
                        case 2:
                            participation.adjunto2_par = fileName;
                            break;
                        case 3:
                            participation.adjunto3_par = fileName;
                            break;
                        case 4:
                            participation.adjunto4_par = fileName;
                            break;
                        case 5:
                            participation.adjunto5_par = fileName;
                            break;
                        default:
                            participation.adjunto1_par = fileName;
                            break;
                    }
                    
                }
                participation.id_est = 1;
                participation.solicitar_adjunto = 1;
                participation.adjunto_adjunto = 1;

                _context.SaveChanges();

                var id_tall = participation.id_tall;
                var nombre_par = participation.nombre_par;
                var apellidos_par = participation.apellidos_par;
                var email_par = participation.email_par;
                var telefono_par = participation.telefono_par;
                // log for <update new attach>
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "id_tall: " + id_tall + ", " +
                        "nombre: " + nombre_par + ", " +
                        "apellidos: " + apellidos_par + ", " +
                        "email: " + email_par + ", " +
                        "telefono: " + telefono_par + ", " +
                        "attachment: " + logFileName + ", " +
                        "action: " + "update new attach" + ", " +
                        "result: success !"
                        );
                }

                return Json(new { success = true, message = "ok" });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}