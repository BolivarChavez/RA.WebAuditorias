﻿using Newtonsoft.Json;
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
    public partial class PlantillaPlanillas : System.Web.UI.Page
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

            if (user_cookie.Usuario == null || user_cookie.Usuario.Trim() == "")
            {
                Response.Redirect("ErrorAccesoOpcion.aspx", true);
            }

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
            Plantilla_Planillas planilla = new Plantilla_Planillas();
            List<ValidaPlantilla> validaPlantilla = new List<ValidaPlantilla>();
            CargaPlantillaPlanillasController plantillaContoller = new CargaPlantillaPlanillasController();
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

            planilla.Mes = Mes.Value.ToUpper();
            planilla.Fecha_Pago_Cash = !DateTime.TryParse(Fecha_Pago_Cash.Value.Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Fecha_Pago_Cash.Value.Trim());
            planilla.Lote = Lote.Value.ToUpper();
            planilla.Remuneracion_Cash = !double.TryParse(Remuneracion_Cash.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Remuneracion_Cash.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Remuneracion_Cheque = !double.TryParse(Remuneracion_Cheque.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Remuneracion_Cheque.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Remuneracion_Total = !double.TryParse(Remuneracion_Total.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Remuneracion_Total.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Fecha_Pago = !DateTime.TryParse(Fecha_Pago.Value.Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Fecha_Pago.Value.Trim());
            planilla.Honorarios_Planilla = !double.TryParse(Honorarios_Planilla.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Honorarios_Planilla.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Honorarios_Incentivos = !double.TryParse(Honorarios_Incentivos.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Honorarios_Incentivos.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Honorarios_Total = !double.TryParse(Honorarios_Total.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Honorarios_Total.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Pagado = !double.TryParse(Pagado.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Pagado.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Honorarios_Cesantes = !double.TryParse(Honorarios_Cesantes.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Honorarios_Cesantes.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Diferencia = !double.TryParse(Diferencia.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Diferencia.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Fecha_Pago_Gratificacion = !DateTime.TryParse(Fecha_Pago_Gratificacion.Value.Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Fecha_Pago_Gratificacion.Value.Trim());
            planilla.Gratificaciones = !double.TryParse(Gratificaciones.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Gratificaciones.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Numero_Informe = Numero_Informe.Value.ToUpper();
            planilla.Observaciones = Observaciones.Value.ToUpper();

            var validaResponse = plantillaContoller.ValidarRegistroPlanilla(planilla);

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

            jsonString = JsonConvert.SerializeObject(planilla);

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
            Plantilla_Planillas planilla = new Plantilla_Planillas();
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

            planilla.Mes = Mes.Value.ToUpper();
            planilla.Fecha_Pago_Cash = !DateTime.TryParse(Fecha_Pago_Cash.Value.Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Fecha_Pago_Cash.Value.Trim());
            planilla.Lote = Lote.Value.ToUpper();
            planilla.Remuneracion_Cash = !double.TryParse(Remuneracion_Cash.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Remuneracion_Cash.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Remuneracion_Cheque = !double.TryParse(Remuneracion_Cheque.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Remuneracion_Cheque.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Remuneracion_Total = !double.TryParse(Remuneracion_Total.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Remuneracion_Total.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Fecha_Pago = !DateTime.TryParse(Fecha_Pago.Value.Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Fecha_Pago.Value.Trim());
            planilla.Honorarios_Planilla = !double.TryParse(Honorarios_Planilla.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Honorarios_Planilla.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Honorarios_Incentivos = !double.TryParse(Honorarios_Incentivos.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Honorarios_Incentivos.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Honorarios_Total = !double.TryParse(Honorarios_Total.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Honorarios_Total.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Pagado = !double.TryParse(Pagado.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Pagado.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Honorarios_Cesantes = !double.TryParse(Honorarios_Cesantes.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Honorarios_Cesantes.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Diferencia = !double.TryParse(Diferencia.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Diferencia.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Fecha_Pago_Gratificacion = !DateTime.TryParse(Fecha_Pago_Gratificacion.Value.Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Fecha_Pago_Gratificacion.Value.Trim());
            planilla.Gratificaciones = !double.TryParse(Gratificaciones.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Gratificaciones.Value.Trim(), CultureInfo.InvariantCulture);
            planilla.Numero_Informe = Numero_Informe.Value.ToUpper();
            planilla.Observaciones = Observaciones.Value.ToUpper();

            jsonString = JsonConvert.SerializeObject(planilla);

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
            Plantilla_Planillas planilla = new Plantilla_Planillas();
            List<Plantilla_Planillas_Base> listaPlanillas = new List<Plantilla_Planillas_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3]), 0).Where(x => x.ad_estado == "A").ToList();

            foreach (var lineaDoc in _documentos)
            {
                planilla = JsonConvert.DeserializeObject<Plantilla_Planillas>(lineaDoc.ad_registro);
                listaPlanillas.Add(
                    new Plantilla_Planillas_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        ReferenciaDocumento = "",
                        Mes = planilla.Mes,
                        Fecha_Pago_Cash = planilla.Fecha_Pago_Cash,
                        Lote = planilla.Lote,
                        Remuneracion_Cash = Math.Round(planilla.Remuneracion_Cash, 2),
                        Remuneracion_Cheque = Math.Round(planilla.Remuneracion_Cheque, 2),
                        Remuneracion_Total = Math.Round(planilla.Remuneracion_Total, 2),
                        Fecha_Pago = planilla.Fecha_Pago,
                        Honorarios_Planilla = Math.Round(planilla.Honorarios_Planilla, 2),
                        Honorarios_Incentivos = Math.Round(planilla.Honorarios_Incentivos, 2),
                        Honorarios_Total = Math.Round(planilla.Honorarios_Total, 2),
                        Pagado = Math.Round(planilla.Pagado, 2),
                        Honorarios_Cesantes = Math.Round(planilla.Honorarios_Cesantes, 2),
                        Diferencia = Math.Round(planilla.Diferencia, 2),
                        Fecha_Pago_Gratificacion = planilla.Fecha_Pago_Gratificacion,
                        Gratificaciones = Math.Round(planilla.Gratificaciones, 2),
                        Numero_Informe = planilla.Numero_Informe,
                        Observaciones = planilla.Observaciones
                    }
                    );
            }

            return JsonConvert.SerializeObject(listaPlanillas);
        }

        protected void BtnCargaPlantilla_ServerClick(object sender, EventArgs e)
        {
            CargaPlantillaPlanillasController plantilla = new CargaPlantillaPlanillasController();
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

            response = plantilla.CargaPlantillaPlanillas(archivoCarga, hojaArchivo, 1, auditoriaId, tareaId, plantillaId, referencia);

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