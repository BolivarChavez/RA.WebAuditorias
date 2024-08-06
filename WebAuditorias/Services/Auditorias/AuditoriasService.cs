using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using WebAuditorias.Interfaces.Auditorias;
using WebAuditorias.Models;
using static WebAuditorias.Models.TipoPlantilla;

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

        public IEnumerable<Models.Auditorias> Consulta(int empresa, int codigo, int anio)
        {
            HttpClient client = new HttpClient();
            List<Models.Auditorias> listaError = new List<Models.Auditorias>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "Auditorias/Consulta/{0}/{1}/{2}", empresa.ToString().Trim(), codigo.ToString().Trim(), anio.ToString().Trim()));

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

        public IEnumerable<Models.Auditorias> ConsultaPlantilla(int empresa, int codigo, int anio, int plantilla)
        {
            HttpClient client = new HttpClient();
            List<Models.Auditorias> listaError = new List<Models.Auditorias>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "Auditorias/ConsultaPlantilla/{0}/{1}/{2}/{3}", empresa.ToString().Trim(), codigo.ToString().Trim(), anio.ToString().Trim(), plantilla.ToString().Trim()));

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

        public IEnumerable<AuditoriaResumen> ConsultaResumen(int empresa, int anio)
        {
            HttpClient client = new HttpClient();
            List<Models.AuditoriaResumen> listaError = new List<Models.AuditoriaResumen>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "Auditorias/ConsultaResumen/{0}/{1}", empresa.ToString().Trim(), anio.ToString().Trim()));

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
                listaError.Add(new Models.AuditoriaResumen
                {
                    Id = 0,
                    Proceso = errorContent,
                    Auditoria = "",
                    EstadoAuditoria = "",
                    OficinaOrigen = "",
                    OficinaDestino = "",
                    FechaInicio = "",
                    FechaCierre = "",
                    Gastos = 0,
                    Tarea = "",
                    Asignacion = "",
                    Responsables = "",
                    ProcesosActivos = 0,
                    ProcesosCerrados = 0,
                    PlantillasActivas = 0,
                    PlantillasProcesadas = 0,
                    EstadoTarea = ""
                });

                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.AuditoriaResumen>>(respuesta);
            }
        }

        public string CopiaAuditoria(Models.Auditorias auditoria)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "Auditorias/CopiaAuditoria";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(auditoria);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
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