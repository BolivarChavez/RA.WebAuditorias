﻿using Newtonsoft.Json;
using PrototipoData.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Caching;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.AuditoriaDocumentos;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Models;
using WebAuditorias.Models.Bases;

namespace WebAuditorias.Views
{
    public partial class PlantillaCheques : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializedView();
                CargaDatosUsuario();
            }
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

        private void InitializedView()
        {
            string[] arrayParametros;
            arrayParametros = Request.QueryString["plantilla"].Split('|');

            Auditoria.Value = arrayParametros[0] + "-" + arrayParametros[1];
            Codigo.Value = "0";
            Tarea.Value = arrayParametros[2] + "-" + arrayParametros[3];
            Plantilla.Value = arrayParametros[4] + "-" + arrayParametros[5];
        }

        protected void BtnNuevo_ServerClick(object sender, EventArgs e)
        {
            InitializedView();
        }

        protected void BtnBuscar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "document.getElementById('profile-tab').click(); LlenaGrid();", true);
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaPlantillas(string parametros)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();
            Plantilla_Cheques cheque = new Plantilla_Cheques();
            List<Plantilla_Cheques_Base> listaCheques = new List<Plantilla_Cheques_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3]), 0).Where(x => x.ad_estado == "A").ToList();

            foreach (var lineaDoc in _documentos)
            {
                cheque = JsonConvert.DeserializeObject<Plantilla_Cheques>(lineaDoc.ad_registro);
                listaCheques.Add(
                    new Plantilla_Cheques_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        ReferenciaDocumento = "",
                        Item = cheque.Item,
                        Talonario = cheque.Talonario,
                        Req = cheque.Req,
                        Beneficiario = cheque.Beneficiario,
                        Concepto = cheque.Concepto,
                        Comprobante = cheque.Comprobante,
                        Moneda = cheque.Moneda,
                        Monto = Math.Round(cheque.Monto, 2),
                        Fecha_Pago = cheque.Fecha_Pago,
                        Comprobante_Egreso = cheque.Comprobante_Egreso,
                        Banco = cheque.Banco,
                        Numero_Cheque = cheque.Numero_Cheque,
                        Tipo_Cambio = cheque.Tipo_Cambio,
                        Observacion_Preliminar = cheque.Observacion_Preliminar,
                        Observacion_Final = cheque.Observacion_Final,
                        Estado = cheque.Estado,
                        Empresa = cheque.Empresa,
                        Sede = cheque.Sede,
                        Cuenta = cheque.Cuenta,
                        Sub_Cuenta = cheque.Sub_Cuenta
                    }
                    );
            }

            return JsonConvert.SerializeObject(listaCheques);
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

        protected void BtnGrabar_ServerClick(object sender, EventArgs e)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            Models.AuditoriaDocumentos parametro = new Models.AuditoriaDocumentos();
            Plantilla_Cheques cheque = new Plantilla_Cheques();
            List<ValidaPlantilla> validaPlantilla = new List<ValidaPlantilla>();
            CargaPlantillaChequesController plantillaContoller = new CargaPlantillaChequesController();
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

            cheque.Item = Item.Value.ToUpper();
            cheque.Talonario = Talonario.Value.ToUpper();
            cheque.Req = Req.Value.ToUpper();
            cheque.Beneficiario = Beneficiario.Value.ToUpper();
            cheque.Concepto = Concepto.Value.ToUpper();
            cheque.Comprobante = Comprobante.Value.ToUpper();
            cheque.Moneda = Moneda.Value.ToUpper();
            cheque.Monto = !double.TryParse(Monto.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Monto.Value.Trim(), CultureInfo.InvariantCulture);
            cheque.Fecha_Pago = !DateTime.TryParse(FechaPago.Value.Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(FechaPago.Value.Trim()); 
            cheque.Comprobante_Egreso = ComprobanteEgreso.Value.ToUpper();
            cheque.Banco = Banco.Value.ToUpper();
            cheque.Numero_Cheque = NumeroCheque.Value.ToUpper();
            cheque.Tipo_Cambio = !double.TryParse(TipoCambio.Value.Trim(), out valorDecimal) ? 0 : double.Parse(TipoCambio.Value.Trim(), CultureInfo.InvariantCulture); 
            cheque.Observacion_Preliminar = ObservacionPreliminar.Value.ToUpper();
            cheque.Observacion_Final = Observacion_Final.Value.ToUpper();
            cheque.Estado = Estado.Value.ToUpper();
            cheque.Empresa = Empresa.Value.ToUpper();
            cheque.Sede = Sede.Value.ToUpper();
            cheque.Cuenta = Cuenta.Value.ToUpper();
            cheque.Sub_Cuenta = SubCuenta.Value.ToUpper();

            var validaResponse = plantillaContoller.ValidarRegistroCheque(cheque);

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

            jsonString = JsonConvert.SerializeObject(cheque);

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
            Plantilla_Cheques cheque = new Plantilla_Cheques();
            string jsonString;
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

            cheque.Item = Item.Value.ToUpper();
            cheque.Talonario = Talonario.Value.ToUpper();
            cheque.Req = Req.Value.ToUpper();
            cheque.Beneficiario = Beneficiario.Value.ToUpper();
            cheque.Concepto = Concepto.Value.ToUpper();
            cheque.Comprobante = Comprobante.Value.ToUpper();
            cheque.Moneda = Moneda.Value.ToUpper();
            cheque.Monto = !double.TryParse(Monto.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Monto.Value.Trim(), CultureInfo.InvariantCulture);//double.Parse(Monto.Value, CultureInfo.InvariantCulture);
            cheque.Fecha_Pago = !DateTime.TryParse(FechaPago.Value.Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(FechaPago.Value.Trim()); //DateTime.Parse(FechaPago.Value);
            cheque.Comprobante_Egreso = ComprobanteEgreso.Value.ToUpper();
            cheque.Banco = Banco.Value.ToUpper();
            cheque.Numero_Cheque = NumeroCheque.Value.ToUpper();
            cheque.Tipo_Cambio = !double.TryParse(TipoCambio.Value.Trim(), out valorDecimal) ? 0 : double.Parse(TipoCambio.Value.Trim(), CultureInfo.InvariantCulture); //double.Parse(TipoCambio.Value, CultureInfo.InvariantCulture);
            cheque.Observacion_Preliminar = ObservacionPreliminar.Value.ToUpper();
            cheque.Observacion_Final = Observacion_Final.Value.ToUpper();
            cheque.Estado = Estado.Value.ToUpper();
            cheque.Empresa = Empresa.Value.ToUpper();
            cheque.Sede = Sede.Value.ToUpper();
            cheque.Cuenta = Cuenta.Value.ToUpper();
            cheque.Sub_Cuenta = SubCuenta.Value.ToUpper();

            jsonString = JsonConvert.SerializeObject(cheque);

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

            _controller.Actualizacion(parametro);

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

        protected void BtnCargaPlantilla_ServerClick(object sender, EventArgs e)
        {
            CargaPlantillaChequesController plantilla = new CargaPlantillaChequesController();
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

            response = plantilla.CargaPlantillaCheques(archivoCarga, hojaArchivo, 1, auditoriaId, tareaId, plantillaId, referencia);

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