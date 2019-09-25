using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebFrontend.Controllers
{
    public class InfoBitSmsManager
    {
        private string sender = "MICHELIN";
	    private string user = "chequemotiva";
	    private string password = "$Cheque14";
	    private string send_sms_url_json = "http://api.infobip.com/api/v3/sendsms/json";

        public async Task<Boolean> SendSMS(String text, String phone)
        {
            var data = "{\"authentication\":{\"username\":\""+ user + "\",\"password\":\""+ password + "\"},\"messages\":[{\"sender\":\""+ sender + "\",\"text\":\""+text+"\",\"recipients\":[{\"gsm\":\"" + phone+"\"}]}]}";
            var httpClient = new HttpClient();
            
            var request = new HttpRequestMessage(new HttpMethod("POST"), send_sms_url_json);
            request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
            
            request.Content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(request);
            HttpContent responseContent = response.Content;
            var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
            var id_envio = await reader.ReadToEndAsync();

            if ((int)response.StatusCode == 200)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
