using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using WebAuditorias.Interfaces.AuditoriaTareaProcesos;

namespace WebAuditorias.Services.AuditoriaTareaProcesos
{
    public class AuditoriaTareaProcesosService : IAuditoriaTareaProcesosService
    {
        public string Actualizacion(Models.AuditoriaTareaProcesos auditoriaTareas)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "AuditoriaTareaProcesos/Actualizacion";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(auditoriaTareas);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }

        public IEnumerable<Models.AuditoriaTareaProcesos> Consulta(int empresa, int auditoria, int tarea)
        {
            HttpClient client = new HttpClient();
            List<Models.AuditoriaTareaProcesos> listaError = new List<Models.AuditoriaTareaProcesos>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "AuditoriaTareaProcesos/Consulta/{0}/{1}/{2}", empresa.ToString().Trim(), auditoria.ToString().Trim(), tarea.ToString().Trim()));

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
                listaError.Add(new Models.AuditoriaTareaProcesos
                {
                    at_empresa = 0,
                    at_auditoria = 0,
                    at_tarea = 0,
                    at_secuencia = 0,
                    at_auditor = 0,
                    at_responsable = 0,
                    at_fecha = DateTime.Now,
                    at_observaciones = errorContent,
                    at_estado = "",
                    at_usuario_creacion = "",
                    at_fecha_creacion = DateTime.Now,
                    at_usuario_actualizacion = "",
                    at_fecha_actualizacion = DateTime.Now
                });
                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.AuditoriaTareaProcesos>>(respuesta);
            }
        }

        public string Ingreso(Models.AuditoriaTareaProcesos auditoriaTareas)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "AuditoriaTareaProcesos/Ingreso";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(auditoriaTareas);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}