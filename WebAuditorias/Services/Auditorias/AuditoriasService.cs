using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using WebAuditorias.Interfaces.Auditorias;

namespace WebAuditorias.Services.Auditorias
{
    public class AuditoriasService : IAuditoriasService
    {
        public string Actualizacion(Models.Auditorias auditoria)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "Auditorias/Actualizacion";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(auditoria);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }

        public IEnumerable<Models.Auditorias> Consulta(int empresa, int codigo)
        {
            HttpClient client = new HttpClient();
            List<Models.Auditorias> listaError = new List<Models.Auditorias>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "Auditorias/Consulta/{0}/{1}", empresa.ToString().Trim(), codigo.ToString().Trim()));

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", stoken);
            var response = client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                respuesta = responseContent.ReadAsStringAsync().Result;
            }
            else
            {
                errorContent = response.StatusCode.ToString() + " - " + response.ReasonPhrase;
                respuesta = "";
            }

            if (respuesta == "")
            {
                listaError.Add(new Models.Auditorias
                {
                    au_empresa = 0,
                    au_codigo = 0,
                    au_oficina_origen = 0,
                    au_oficina_destino = 0,
                    au_tipo_proceso = 0,
                    au_fecha_inicio = DateTime.Now,
                    au_fecha_cierre = DateTime.Now,
                    au_tipo = "",
                    au_observaciones = errorContent,
                    au_estado = "",
                    au_usuario_creacion = "",
                    au_fecha_creacion = DateTime.Now,
                    au_usuario_actualizacion = "",
                    au_fecha_actualizacion = DateTime.Now
                });
                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.Auditorias>>(respuesta);
            }
        }

        public string Ingreso(Models.Auditorias auditoria)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "Auditorias/Ingreso";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(auditoria);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}