using Newtonsoft.Json;
using PrototipoData.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.AuditoriaDocumentos;
using WebAuditorias.Models.Bases;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Models;
using System.Runtime.Caching;

namespace WebAuditorias.Views
{
    public partial class PlantillaRegalias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializedView();
                CargaDatosUsuario();
            }
        }

        private void InitializedView()
        {
            string[] arrayParametros;
            arrayParametros = Request.QueryString["plantilla"].Split('|');

            Auditoria.Value = arrayParametros[0] + "-" + arrayParametros[1];
            Codigo.Value = "0";
            Tarea.Value = arrayParametros[2] + "-" + arrayParametros[3];
            Plantilla.Value = arrayParametros[4] + "-" + arrayParametros[5];
        }

        private void CargaDatosUsuario()
        {
            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            lblNombre.Text = user_cookie.Nombre;
            lblFechaConexion.Text = DateTime.Now.ToString();
        }

        protected void BtnNuevo_ServerClick(object sender, EventArgs e)
        {
            InitializedView();
        }

        protected void BtnBuscar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "document.getElementById('profile-tab').click(); LlenaGrid();", true);
        }

        protected void BtnGrabar_ServerClick(object sender, EventArgs e)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            Models.AuditoriaDocumentos parametro = new Models.AuditoriaDocumentos();
            Plantilla_Regalias regalia = new Plantilla_Regalias();
            List<ValidaPlantilla> validaPlantilla = new List<ValidaPlantilla>();
            CargaPlantillaRegaliasController plantillaContoller = new CargaPlantillaRegaliasController();
            string jsonString;
            string response;
            double valorDecimal;
            DateTime fechaTabla;

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string[] arrayParametros;
            arrayParametros = Auditoria.Value.Split('-');
            int auditoriaId = int.Parse(arrayParametros[0]);
            arrayParametros = Tarea.Value.Split('-');
            Int16 tareaId = Int16.Parse(arrayParametros[0]);
            arrayParametros = Plantilla.Value.Split('-');
            Int16 plantillaId = Int16.Parse(arrayParametros[0]);

            regalia.Codigo = CodigoRegalia.Value.ToUpper();
            regalia.Fecha = !DateTime.TryParse(Fecha.Value.Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Fecha.Value.Trim());  
            regalia.Descripcion = Descripcion.Value.ToUpper();
            regalia.Moneda = Moneda.Value.ToUpper();
            regalia.Valor_Fijo = !double.TryParse(Valor_Fijo.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Valor_Fijo.Value.Trim(), CultureInfo.InvariantCulture);  
            regalia.Ingresos_Facturados = !double.TryParse(Ingresos_Facturados.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Ingresos_Facturados.Value.Trim(), CultureInfo.InvariantCulture);  
            regalia.Ingresos_Cartera = !double.TryParse(Ingresos_Cartera.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Ingresos_Cartera.Value.Trim(), CultureInfo.InvariantCulture);  
            regalia.Retencion = !double.TryParse(Retencion.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Retencion.Value.Trim(), CultureInfo.InvariantCulture);  
            regalia.Total_Soles = !double.TryParse(Total_Soles.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Total_Soles.Value.Trim(), CultureInfo.InvariantCulture);  
            regalia.Tasa_Cambio = !double.TryParse(Tasa_Cambio.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Tasa_Cambio.Value.Trim(), CultureInfo.InvariantCulture);  
            regalia.Total_Dolares = !double.TryParse(Total_Dolares.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Total_Dolares.Value.Trim(), CultureInfo.InvariantCulture);  
            regalia.Adjuntos = Adjuntos.Value.ToUpper();
            regalia.Cuenta = Cuenta.Value.ToUpper();
            regalia.Soporte = Soporte.Value.ToUpper();
            regalia.Observaciones = Observaciones.Value.ToUpper();

            var validaResponse = plantillaContoller.ValidarRegistroRegalia(regalia);

            if (validaResponse != null && validaResponse.Count > 0)
            {
                validaPlantilla.Add(new ValidaPlantilla() { Linea = 0, Campos = validaResponse });
                ObjectCache cache = MemoryCache.Default;
                string CacheKey = user_cookie.Usuario.Trim();

                if (cache.Contains(CacheKey))
                    cache.Remove(CacheKey);

                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(1.0);
                cache.Add(CacheKey, validaPlantilla, cacheItemPolicy);

                ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "mensajeGrabacion('0', 'Existen campos con errores')", true);
                return;
            }

            jsonString = JsonConvert.SerializeObject(regalia);

            parametro.ad_empresa = 1;
            parametro.ad_auditoria = auditoriaId;
            parametro.ad_tarea = tareaId;
            parametro.ad_codigo = Int16.Parse(Codigo.Value);
            parametro.ad_plantilla = plantillaId;
            parametro.ad_referencia = "";
            parametro.ad_registro = jsonString;
            parametro.ad_auditoria_origen = 0;
            parametro.ad_responsable = 0;
            parametro.ad_estado = "A";
            parametro.ad_usuario_creacion = user_cookie.Usuario;
            parametro.ad_fecha_creacion = DateTime.Now;
            parametro.ad_usuario_actualizacion = user_cookie.Usuario;
            parametro.ad_fecha_actualizacion = DateTime.Now;

            if (Int16.Parse(Codigo.Value) == 0)
            {
                response = _controller.Ingreso(parametro);
            }
            else
            {
                response = _controller.Actualizacion(parametro);
            }

            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "mensajeGrabacion('1', 'El registro de plantilla se grabó exitosamente')", true);
        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            Models.AuditoriaDocumentos parametro = new Models.AuditoriaDocumentos();
            Plantilla_Regalias regalia = new Plantilla_Regalias();
            string jsonString;
            string response;
            double valorDecimal;
            DateTime fechaTabla;

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string[] arrayParametros;
            arrayParametros = Auditoria.Value.Split('-');
            int auditoriaId = int.Parse(arrayParametros[0]);
            arrayParametros = Tarea.Value.Split('-');
            Int16 tareaId = Int16.Parse(arrayParametros[0]);
            arrayParametros = Plantilla.Value.Split('-');
            Int16 plantillaId = Int16.Parse(arrayParametros[0]);

            regalia.Codigo = CodigoRegalia.Value.ToUpper();
            regalia.Fecha = !DateTime.TryParse(Fecha.Value.Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Fecha.Value.Trim());
            regalia.Descripcion = Descripcion.Value.ToUpper();
            regalia.Moneda = Moneda.Value.ToUpper();
            regalia.Valor_Fijo = !double.TryParse(Valor_Fijo.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Valor_Fijo.Value.Trim(), CultureInfo.InvariantCulture);
            regalia.Ingresos_Facturados = !double.TryParse(Ingresos_Facturados.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Ingresos_Facturados.Value.Trim(), CultureInfo.InvariantCulture);
            regalia.Ingresos_Cartera = !double.TryParse(Ingresos_Cartera.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Ingresos_Cartera.Value.Trim(), CultureInfo.InvariantCulture);
            regalia.Retencion = !double.TryParse(Retencion.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Retencion.Value.Trim(), CultureInfo.InvariantCulture);
            regalia.Total_Soles = !double.TryParse(Total_Soles.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Total_Soles.Value.Trim(), CultureInfo.InvariantCulture);
            regalia.Tasa_Cambio = !double.TryParse(Tasa_Cambio.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Tasa_Cambio.Value.Trim(), CultureInfo.InvariantCulture);
            regalia.Total_Dolares = !double.TryParse(Total_Dolares.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Total_Dolares.Value.Trim(), CultureInfo.InvariantCulture);
            regalia.Adjuntos = Adjuntos.Value.ToUpper();
            regalia.Cuenta = Cuenta.Value.ToUpper();
            regalia.Soporte = Soporte.Value.ToUpper();
            regalia.Observaciones = Observaciones.Value.ToUpper();

            jsonString = JsonConvert.SerializeObject(regalia);

            parametro.ad_empresa = 1;
            parametro.ad_auditoria = auditoriaId;
            parametro.ad_tarea = tareaId;
            parametro.ad_codigo = Int16.Parse(Codigo.Value);
            parametro.ad_plantilla = plantillaId;
            parametro.ad_referencia = "";
            parametro.ad_registro = jsonString;
            parametro.ad_auditoria_origen = 0;
            parametro.ad_responsable = 0;
            parametro.ad_estado = "X";
            parametro.ad_usuario_creacion = user_cookie.Usuario;
            parametro.ad_fecha_creacion = DateTime.Now;
            parametro.ad_usuario_actualizacion = user_cookie.Usuario;
            parametro.ad_fecha_actualizacion = DateTime.Now;

            response = _controller.Actualizacion(parametro);

            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "mensajeGrabacion('1', 'El registro de plantilla se eliminó exitosamente')", true);
        }

        protected void BtnCargar_ServerClick(object sender, EventArgs e)
        {
            string fileName;

            if (CargaArchivo.HasFile)
            {
                fileName = CargaArchivo.FileName.Trim();
                HiddenField2.Value = fileName;
                CargaArchivo.SaveAs(Server.MapPath("~") + ConfigurationManager.AppSettings["PathDocs"] + @"\" + fileName);
                CargaHojasArchivo(Server.MapPath("~") + ConfigurationManager.AppSettings["PathDocs"] + @"\" + fileName);
            }
        }

        public void CargaHojasArchivo(string fileName)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;

            String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                fileName.Trim() +
                ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

            objConn = new OleDbConnection(constr);
            objConn.Open();
            dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            objConn.Close();

            Hoja.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                if (row["TABLE_NAME"].ToString().Contains("$"))
                {
                    string s = row["TABLE_NAME"].ToString();
                    Hoja.Items.Add(s.StartsWith("'") ? s.Substring(1, s.Length - 3) : s.Substring(0, s.Length - 1));
                }
            }
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaPlantillas(string parametros)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();
            Plantilla_Regalias regalia = new Plantilla_Regalias();
            List<Plantilla_Regalias_Base> listaRegalias = new List<Plantilla_Regalias_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3]), 0).Where(x => x.ad_estado == "A").ToList();

            foreach (var lineaDoc in _documentos)
            {
                regalia = JsonConvert.DeserializeObject<Plantilla_Regalias>(lineaDoc.ad_registro);
                listaRegalias.Add(
                    new Plantilla_Regalias_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        ReferenciaDocumento = "",
                        Codigo = regalia.Codigo,
                        Fecha = regalia.Fecha,
                        Descripcion = regalia.Descripcion,
                        Moneda = regalia.Moneda,
                        Valor_Fijo = Math.Round(regalia.Valor_Fijo, 2),
                        Ingresos_Facturados = Math.Round(regalia.Ingresos_Facturados, 2),
                        Ingresos_Cartera = Math.Round(regalia.Ingresos_Cartera, 2),
                        Retencion = Math.Round(regalia.Retencion, 2),
                        Total_Soles = Math.Round(regalia.Total_Soles, 2),
                        Tasa_Cambio = Math.Round(regalia.Tasa_Cambio, 2),
                        Total_Dolares = Math.Round(regalia.Total_Dolares, 2),
                        Adjuntos = regalia.Adjuntos,
                        Cuenta = regalia.Cuenta,
                        Soporte = regalia.Soporte,
                        Observaciones = regalia.Observaciones
                    }
                    );
            }

            return JsonConvert.SerializeObject(listaRegalias);
        }

        protected void BtnCargaPlantilla_ServerClick(object sender, EventArgs e)
        {
            CargaPlantillaRegaliasController plantilla = new CargaPlantillaRegaliasController();
            string fileName = HiddenField2.Value;
            string archivoCarga = Server.MapPath("~") + ConfigurationManager.AppSettings["PathDocs"] + @"\" + fileName;
            string hojaArchivo = Hoja.Text.Trim();
            string referencia = Referencia.Value.Trim();
            string response = "";

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string[] arrayParametros;
            arrayParametros = Auditoria.Value.Split('-');
            int auditoriaId = int.Parse(arrayParametros[0]);
            arrayParametros = Tarea.Value.Split('-');
            Int16 tareaId = Int16.Parse(arrayParametros[0]);
            arrayParametros = Plantilla.Value.Split('-');
            Int16 plantillaId = Int16.Parse(arrayParametros[0]);

            response = plantilla.CargaPlantillaRegalias(archivoCarga, hojaArchivo, 1, auditoriaId, tareaId, plantillaId, referencia);

            if (response == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "mensajeGrabacion('1', 'La plantilla se ha procesado correctamente')", true);
            }
            else
            {
                var listaErrores = JsonConvert.DeserializeObject<List<ValidaPlantilla>>(response);

                ObjectCache cache = MemoryCache.Default;
                string CacheKey = user_cookie.Usuario.Trim();

                if (cache.Contains(CacheKey))
                    cache.Remove(CacheKey);

                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(1.0);
                cache.Add(CacheKey, listaErrores, cacheItemPolicy);

                ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "mensajeGrabacion('0', 'Existen campos con errores')", true);
            }
        }

        protected void BtnAddTarea_ServerClick(object sender, EventArgs e)
        {
            string parametros = "";

            parametros += "1|";
            parametros += Auditoria.Value.ToString().Split('-')[0] + "|";
            parametros += Auditoria.Value.ToString().Substring(Auditoria.Value.ToString().IndexOf('-') + 1) + "|";
            parametros += Tarea.Value.ToString().Split('-')[0] + "|";
            parametros += Tarea.Value.ToString().Substring(Tarea.Value.ToString().IndexOf('-') + 1) + "|";
            parametros += Plantilla.Value.ToString().Split('-')[0] + "|" + Plantilla.Value.ToString().Split('-')[1] + "|" + Codigo.Value.ToString();

            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "window.open('AuditoriaDocumentoProceso.aspx?plantilla=" + parametros + "', '_blank');", true);
        }

        protected void BtnAddTareaGrupo_ServerClick(object sender, EventArgs e)
        {
            string parametros = "";

            parametros += "1|";
            parametros += Auditoria.Value.ToString().Split('-')[0] + "|";
            parametros += Auditoria.Value.ToString().Substring(Auditoria.Value.ToString().IndexOf('-') + 1) + "|";
            parametros += Tarea.Value.ToString().Split('-')[0] + "|";
            parametros += Tarea.Value.ToString().Substring(Tarea.Value.ToString().IndexOf('-') + 1) + "|";
            parametros += Plantilla.Value.ToString().Split('-')[0] + "|" + Plantilla.Value.ToString().Split('-')[1] + "|" + Codigo.Value.ToString();

            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "window.open('AuditoriaDocumentoProcesoGrupo.aspx?plantilla=" + parametros + "', '_blank');", true);
        }
    }
}