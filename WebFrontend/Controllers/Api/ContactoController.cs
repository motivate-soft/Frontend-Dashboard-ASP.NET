using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFrontend.Data;
using WebFrontend.Models;

namespace WebFrontend.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Contacto")]
    public class ContactoController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        public IHttpContextAccessor _accessor;
        private string[] lines;
        private DateTime datetime;
        private string ip;

        public ContactoController(IHostingEnvironment hostingEnvironment,
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

        // POST: api/Contacto
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] ContactoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var nombre = model.nombre;
                var apellidos = model.apellidos;
                var movil = model.movil;
                var cemail = "rgarcia@sitcom.es"; //rgarcia@sitcom.es
                var email = model.email;
                var comentarios = model.escribe;
                var asunto_teenvio = "Contacto Michelin";
                var pieza_teenvio = "83140";
                var dato_20 = "";
                var result = await teenvio_actualizar_contacto(cemail, pieza_teenvio, asunto_teenvio, nombre, dato_20, email, comentarios, movil, apellidos);
                if (result)
                {
                    // log for contact
                    using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                    {
                        sw.WriteLine(
                            "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                            "nombre: " + model.nombre + ", " +
                            "apellidos: " + model.apellidos + ", " +
                            "movil: " + model.movil + ", " +
                            "email: " + model.email + ", " +
                            "comentarios: " + model.escribe + ", " +
                            "action: " + "sent contact form" + ", " +
                            "result: " + "success !"
                            );
                    }
                    return Json(new { success = true, message = "success" });
                }
                else
                {
                    // log for contact
                    using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                    {
                        sw.WriteLine(
                            "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                            "nombre: " + model.nombre + ", " +
                            "apellidos: " + model.apellidos + ", " +
                            "movil: " + model.movil + ", " +
                            "email: " + model.email + ", " +
                            "comentarios: " + model.escribe + ", " +
                            "action: " + "sent contact form" + ", " +
                            "result: " + "fail !"
                            );
                    }
                    return Json(new { success = false, message = "fail." });
                }
            }
            catch (Exception ex)
            {
                // log for contact
                using (StreamWriter sw = System.IO.File.AppendText(Constants.Log.log_path))
                {
                    sw.WriteLine(
                        "Datetime: " + datetime.ToString("dd/MM/yyyy HH:mm:ss") + " IP: " + ip + ", " +
                        "nombre: " + model.nombre + ", " +
                        "apellidos: " + model.apellidos + ", " +
                        "movil: " + model.movil + ", " +
                        "email: " + model.email + ", " +
                        "comentarios: " + model.escribe + ", " +
                        "action: " + "sent contact form" + ", " +
                        "result: " + "fail !" + ex.Message
                        );
                }
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<bool> teenvio_actualizar_contacto(String cemail, String pieza_teenvio, String asunto_teenvio, String nombre_teenvio, String dato_20, String email, String comentarios, String movil, String apellidos)
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
                if (await teenvio_enviar_pieza(contact_id, pieza_teenvio, asunto_teenvio, nombre_teenvio, email, comentarios, movil, apellidos))
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

        public async Task<bool> teenvio_enviar_pieza(String contact_id, String pieza_teenvio, String asunto_teenvio, String nombre_teenvio, String email, String comentarios, String movil, String apellidos)
        {
            var action = "send_campaign";

            string vars = "{\"contact_name\":\"" + nombre_teenvio + "\",\"contact_email\":\"" + email + "\",\"contact_message\":\"" + comentarios + "\",\"contact_apellidos\":\""+apellidos+ "\",\"contact_movil\":\"" + movil + "\"}";

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
}