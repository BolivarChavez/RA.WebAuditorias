﻿using Newtonsoft.Json;
using PrototipoData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using WebAuditorias.Controllers.AuditoriaDocumentoProcesos;
using WebAuditorias.Controllers.AuditoriaDocumentos;
using WebAuditorias.Models.Bases;

namespace WebAuditorias.Views
{
    /// <summary>
    /// Descripción breve de ConsultaPlantillasService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class ConsultaPlantillasService : System.Web.Services.WebService
    {

        [WebMethod]
        public string ConsultaPlantillasCheques(string parametros)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();
            Plantilla_Cheques cheque = new Plantilla_Cheques();
            List<Plantilla_Cheques_Base> listaCheques = new List<Plantilla_Cheques_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            AuditoriaDocumentoProcesosController _controllerDocumentos = new AuditoriaDocumentoProcesosController();
            List<Models.AuditoriaDocumentoProcesos> _auditoriaDocumentosProcesos = new List<Models.AuditoriaDocumentoProcesos>();

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3]), 0).Where(x => x.ad_estado == "A").ToList();
            _auditoriaDocumentosProcesos = _controllerDocumentos.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), 0, 0).Where(x => x.ad_estado != "X").ToList();

            var listaDocumentos = from documento in _documentos
                                  join proceso in _auditoriaDocumentosProcesos on documento.ad_codigo equals proceso.ad_codigo into procesosDocumento
                                  from docProceso in procesosDocumento.DefaultIfEmpty()
                                  let referenciaDocumento = docProceso != null ? "S" : ""
                                  select new {
                                        documento.ad_empresa,
                                        documento.ad_auditoria,
                                        documento.ad_tarea,
                                        documento.ad_codigo,
                                        documento.ad_plantilla,
                                        documento.ad_referencia,
                                        documento.ad_registro,
                                        documento.ad_auditoria_origen,
                                        documento.ad_responsable,
                                        documento.ad_estado,
                                        documento.ad_usuario_creacion,
                                        documento.ad_fecha_creacion,
                                        documento.ad_usuario_actualizacion,
                                        documento.ad_fecha_actualizacion,
                                        referenciaDocumento
                                  };

            foreach (var lineaDoc in listaDocumentos.Distinct())
            {
                cheque = JsonConvert.DeserializeObject<Plantilla_Cheques>(lineaDoc.ad_registro);
                listaCheques.Add(
                    new Plantilla_Cheques_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        ReferenciaDocumento = lineaDoc.referenciaDocumento,
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

        [WebMethod]
        public string ConsultaPlantillasComisiones(string parametros)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();
            Plantilla_Comisiones comision = new Plantilla_Comisiones();
            List<Plantilla_Comisiones_Base> listaComisiones = new List<Plantilla_Comisiones_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            AuditoriaDocumentoProcesosController _controllerDocumentos = new AuditoriaDocumentoProcesosController();
            List<Models.AuditoriaDocumentoProcesos> _auditoriaDocumentosProcesos = new List<Models.AuditoriaDocumentoProcesos>();

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3]), 0).Where(x => x.ad_estado == "A").ToList();
            _auditoriaDocumentosProcesos = _controllerDocumentos.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), 0, 0).Where(x => x.ad_estado != "X").ToList();

            var listaDocumentos = from documento in _documentos
                                  join proceso in _auditoriaDocumentosProcesos on documento.ad_codigo equals proceso.ad_codigo into procesosDocumento
                                  from docProceso in procesosDocumento.DefaultIfEmpty()
                                  let referenciaDocumento = docProceso != null ? "S" : ""
                                  select new
                                  {
                                      documento.ad_empresa,
                                      documento.ad_auditoria,
                                      documento.ad_tarea,
                                      documento.ad_codigo,
                                      documento.ad_plantilla,
                                      documento.ad_referencia,
                                      documento.ad_registro,
                                      documento.ad_auditoria_origen,
                                      documento.ad_responsable,
                                      documento.ad_estado,
                                      documento.ad_usuario_creacion,
                                      documento.ad_fecha_creacion,
                                      documento.ad_usuario_actualizacion,
                                      documento.ad_fecha_actualizacion,
                                      referenciaDocumento
                                  };

            foreach (var lineaDoc in listaDocumentos.Distinct())
            {
                comision = JsonConvert.DeserializeObject<Plantilla_Comisiones>(lineaDoc.ad_registro);
                listaComisiones.Add(
                    new Plantilla_Comisiones_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        ReferenciaDocumento = lineaDoc.referenciaDocumento,
                        Mes = comision.Mes,
                        Monto_Recuperado = Math.Round(comision.Monto_Recuperado, 2),
                        Monto_Planilla = Math.Round(comision.Monto_Planilla, 2),
                        Monto_Honorarios = Math.Round(comision.Monto_Honorarios, 2),
                        Total_Incentivos = Math.Round(comision.Total_Incentivos, 2),
                        Cheque_Girado = Math.Round(comision.Cheque_Girado, 2),
                        Pagado = Math.Round(comision.Pagado, 2),
                        Entregado_Caja_Interna_1 = comision.Entregado_Caja_Interna_1,
                        No_Girado = Math.Round(comision.No_Girado, 2),
                        Fecha_Informe = comision.Fecha_Informe,
                        Fecha_Contabilidad = comision.Fecha_Contabilidad,
                        Informe_Comisiones = comision.Informe_Comisiones,
                        Entregado_Caja_Interna_2 = comision.Entregado_Caja_Interna_2,
                        Observaciones = comision.Observaciones
                    }
                    );
            }

            return JsonConvert.SerializeObject(listaComisiones);
        }

        [WebMethod]
        public string ConsultaPlantillasIngresos(string parametros)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();
            Plantilla_Ingresos ingreso = new Plantilla_Ingresos();
            List<Plantilla_Ingresos_Base> listaIngresos = new List<Plantilla_Ingresos_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            AuditoriaDocumentoProcesosController _controllerDocumentos = new AuditoriaDocumentoProcesosController();
            List<Models.AuditoriaDocumentoProcesos> _auditoriaDocumentosProcesos = new List<Models.AuditoriaDocumentoProcesos>();

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3]), 0).Where(x => x.ad_estado == "A").ToList();
            _auditoriaDocumentosProcesos = _controllerDocumentos.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), 0, 0).Where(x => x.ad_estado != "X").ToList();

            var listaDocumentos = from documento in _documentos
                                  join proceso in _auditoriaDocumentosProcesos on documento.ad_codigo equals proceso.ad_codigo into procesosDocumento
                                  from docProceso in procesosDocumento.DefaultIfEmpty()
                                  let referenciaDocumento = docProceso != null ? "S" : ""
                                  select new
                                  {
                                      documento.ad_empresa,
                                      documento.ad_auditoria,
                                      documento.ad_tarea,
                                      documento.ad_codigo,
                                      documento.ad_plantilla,
                                      documento.ad_referencia,
                                      documento.ad_registro,
                                      documento.ad_auditoria_origen,
                                      documento.ad_responsable,
                                      documento.ad_estado,
                                      documento.ad_usuario_creacion,
                                      documento.ad_fecha_creacion,
                                      documento.ad_usuario_actualizacion,
                                      documento.ad_fecha_actualizacion,
                                      referenciaDocumento
                                  };

            foreach (var lineaDoc in listaDocumentos.Distinct())
            {
                ingreso = JsonConvert.DeserializeObject<Plantilla_Ingresos>(lineaDoc.ad_registro);
                listaIngresos.Add(
                    new Plantilla_Ingresos_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        ReferenciaDocumento = lineaDoc.referenciaDocumento,
                        Mes = ingreso.Mes,
                        Factura = ingreso.Factura,
                        Cuenta = ingreso.Cuenta,
                        Detalle = ingreso.Detalle,
                        Concepto = ingreso.Concepto,
                        Moneda = ingreso.Moneda,
                        Subtotal = Math.Round(ingreso.Subtotal, 2),
                        Porcentaje = Math.Round(ingreso.Porcentaje, 2),
                        Total = Math.Round(ingreso.Total, 2),
                        Fecha_Detraccion = ingreso.Fecha_Detraccion,
                        Detraccion_Moneda_Destino = Math.Round(ingreso.Detraccion_Moneda_Destino, 2),
                        Neto_Ingreso = Math.Round(ingreso.Neto_Ingreso, 2),
                        Flujo = ingreso.Flujo,
                        Estado_Cuenta_1 = ingreso.Estado_Cuenta_1,
                        Estado_Cuenta_2 = ingreso.Estado_Cuenta_2,
                        Soporte = ingreso.Soporte,
                        Observacion = ingreso.Observacion,
                        Banco = ingreso.Banco,
                        Empresa = ingreso.Empresa,
                        Sede = ingreso.Sede,
                        Cuenta_Contable = ingreso.Cuenta_Contable,
                        SubCuenta = ingreso.SubCuenta
                    }
                    );
            }

            return JsonConvert.SerializeObject(listaIngresos);
        }

        [WebMethod]
        public string ConsultaPlantillasMutuos(string parametros)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();
            Plantilla_Mutuos mutuo = new Plantilla_Mutuos();
            List<Plantilla_Mutuos_Base> listaMutuos = new List<Plantilla_Mutuos_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            AuditoriaDocumentoProcesosController _controllerDocumentos = new AuditoriaDocumentoProcesosController();
            List<Models.AuditoriaDocumentoProcesos> _auditoriaDocumentosProcesos = new List<Models.AuditoriaDocumentoProcesos>();

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3]), 0).Where(x => x.ad_estado == "A").ToList();
            _auditoriaDocumentosProcesos = _controllerDocumentos.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), 0, 0).Where(x => x.ad_estado != "X").ToList();

            var listaDocumentos = from documento in _documentos
                                  join proceso in _auditoriaDocumentosProcesos on documento.ad_codigo equals proceso.ad_codigo into procesosDocumento
                                  from docProceso in procesosDocumento.DefaultIfEmpty()
                                  let referenciaDocumento = docProceso != null ? "S" : ""
                                  select new
                                  {
                                      documento.ad_empresa,
                                      documento.ad_auditoria,
                                      documento.ad_tarea,
                                      documento.ad_codigo,
                                      documento.ad_plantilla,
                                      documento.ad_referencia,
                                      documento.ad_registro,
                                      documento.ad_auditoria_origen,
                                      documento.ad_responsable,
                                      documento.ad_estado,
                                      documento.ad_usuario_creacion,
                                      documento.ad_fecha_creacion,
                                      documento.ad_usuario_actualizacion,
                                      documento.ad_fecha_actualizacion,
                                      referenciaDocumento
                                  };

            foreach (var lineaDoc in listaDocumentos.Distinct())
            {
                mutuo = JsonConvert.DeserializeObject<Plantilla_Mutuos>(lineaDoc.ad_registro);
                listaMutuos.Add(
                    new Plantilla_Mutuos_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        ReferenciaDocumento = lineaDoc.referenciaDocumento,
                        Codigo = mutuo.Codigo,
                        Banco = mutuo.Banco,
                        Moneda = mutuo.Moneda,
                        Detalle = mutuo.Detalle,
                        Fecha_Documento = mutuo.Fecha_Documento,
                        Monto_Prestamo = Math.Round(mutuo.Monto_Prestamo, 2),
                        Fecha_Pago_Cuota = mutuo.Fecha_Pago_Cuota,
                        Numero_Cuota = mutuo.Numero_Cuota,
                        Valor_Cuota = Math.Round(mutuo.Valor_Cuota, 2),
                        Comprobante_Pago = mutuo.Comprobante_Pago,
                        Saldo_Pendiente = mutuo.Saldo_Pendiente,
                        Cuotas_Pendientes = mutuo.Cuotas_Pendientes,
                        Documento_Legal = mutuo.Documento_Legal,
                        Observacion = mutuo.Observacion
                    }
                    );
            }

            return JsonConvert.SerializeObject(listaMutuos);
        }

        [WebMethod]
        public string ConsultaPlantillasPagos(string parametros)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();
            Plantilla_Pagos pago = new Plantilla_Pagos();
            List<Plantilla_Pagos_Base> listaPagos = new List<Plantilla_Pagos_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            AuditoriaDocumentoProcesosController _controllerDocumentos = new AuditoriaDocumentoProcesosController();
            List<Models.AuditoriaDocumentoProcesos> _auditoriaDocumentosProcesos = new List<Models.AuditoriaDocumentoProcesos>();

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3]), 0).Where(x => x.ad_estado == "A").ToList();
            _auditoriaDocumentosProcesos = _controllerDocumentos.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), 0, 0).Where(x => x.ad_estado != "X").ToList();

            var listaDocumentos = from documento in _documentos
                                  join proceso in _auditoriaDocumentosProcesos on documento.ad_codigo equals proceso.ad_codigo into procesosDocumento
                                  from docProceso in procesosDocumento.DefaultIfEmpty()
                                  let referenciaDocumento = docProceso != null ? "S" : ""
                                  select new
                                  {
                                      documento.ad_empresa,
                                      documento.ad_auditoria,
                                      documento.ad_tarea,
                                      documento.ad_codigo,
                                      documento.ad_plantilla,
                                      documento.ad_referencia,
                                      documento.ad_registro,
                                      documento.ad_auditoria_origen,
                                      documento.ad_responsable,
                                      documento.ad_estado,
                                      documento.ad_usuario_creacion,
                                      documento.ad_fecha_creacion,
                                      documento.ad_usuario_actualizacion,
                                      documento.ad_fecha_actualizacion,
                                      referenciaDocumento
                                  };

            foreach (var lineaDoc in listaDocumentos.Distinct())
            {
                pago = JsonConvert.DeserializeObject<Plantilla_Pagos>(lineaDoc.ad_registro);
                listaPagos.Add(
                    new Plantilla_Pagos_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        ReferenciaDocumento = lineaDoc.referenciaDocumento,
                        Periodo = pago.Periodo,
                        Detalle = pago.Detalle,
                        Fecha_Pago = pago.Fecha_Pago,
                        Importe_Bruto = pago.Importe_Bruto,
                        Descuentos = Math.Round(pago.Descuentos, 2),
                        Neto_Pagar = Math.Round(pago.Neto_Pagar, 2),
                        Transferencia = Math.Round(pago.Transferencia, 2),
                        Cheque = Math.Round(pago.Cheque, 2),
                        Diferencia = Math.Round(pago.Diferencia, 2),
                        Numero_Cheque = pago.Numero_Cheque,
                        Numero_Informe = pago.Numero_Informe,
                        Observaciones = pago.Observaciones
                    }
                    );
            }

            return JsonConvert.SerializeObject(listaPagos);
        }

        [WebMethod]
        public string ConsultaPlantillasPlanillas(string parametros)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();
            Plantilla_Planillas planilla = new Plantilla_Planillas();
            List<Plantilla_Planillas_Base> listaPlanillas = new List<Plantilla_Planillas_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            AuditoriaDocumentoProcesosController _controllerDocumentos = new AuditoriaDocumentoProcesosController();
            List<Models.AuditoriaDocumentoProcesos> _auditoriaDocumentosProcesos = new List<Models.AuditoriaDocumentoProcesos>();

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3]), 0).Where(x => x.ad_estado == "A").ToList();
            _auditoriaDocumentosProcesos = _controllerDocumentos.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), 0, 0).Where(x => x.ad_estado != "X").ToList();

            var listaDocumentos = from documento in _documentos
                                  join proceso in _auditoriaDocumentosProcesos on documento.ad_codigo equals proceso.ad_codigo into procesosDocumento
                                  from docProceso in procesosDocumento.DefaultIfEmpty()
                                  let referenciaDocumento = docProceso != null ? "S" : ""
                                  select new
                                  {
                                      documento.ad_empresa,
                                      documento.ad_auditoria,
                                      documento.ad_tarea,
                                      documento.ad_codigo,
                                      documento.ad_plantilla,
                                      documento.ad_referencia,
                                      documento.ad_registro,
                                      documento.ad_auditoria_origen,
                                      documento.ad_responsable,
                                      documento.ad_estado,
                                      documento.ad_usuario_creacion,
                                      documento.ad_fecha_creacion,
                                      documento.ad_usuario_actualizacion,
                                      documento.ad_fecha_actualizacion,
                                      referenciaDocumento
                                  };

            foreach (var lineaDoc in listaDocumentos.Distinct())
            {
                planilla = JsonConvert.DeserializeObject<Plantilla_Planillas>(lineaDoc.ad_registro);
                listaPlanillas.Add(
                    new Plantilla_Planillas_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        ReferenciaDocumento = lineaDoc.referenciaDocumento,
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

        [WebMethod]
        public string ConsultaPlantillasReembolsos(string parametros)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();
            Plantilla_Reembolsos reembolso = new Plantilla_Reembolsos();
            List<Plantilla_Reembolsos_Base> listaReembolsos = new List<Plantilla_Reembolsos_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            AuditoriaDocumentoProcesosController _controllerDocumentos = new AuditoriaDocumentoProcesosController();
            List<Models.AuditoriaDocumentoProcesos> _auditoriaDocumentosProcesos = new List<Models.AuditoriaDocumentoProcesos>();

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3]), 0).Where(x => x.ad_estado == "A").ToList();
            _auditoriaDocumentosProcesos = _controllerDocumentos.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), 0, 0).Where(x => x.ad_estado != "X").ToList();

            var listaDocumentos = from documento in _documentos
                                  join proceso in _auditoriaDocumentosProcesos on documento.ad_codigo equals proceso.ad_codigo into procesosDocumento
                                  from docProceso in procesosDocumento.DefaultIfEmpty()
                                  let referenciaDocumento = docProceso != null ? "S" : ""
                                  select new
                                  {
                                      documento.ad_empresa,
                                      documento.ad_auditoria,
                                      documento.ad_tarea,
                                      documento.ad_codigo,
                                      documento.ad_plantilla,
                                      documento.ad_referencia,
                                      documento.ad_registro,
                                      documento.ad_auditoria_origen,
                                      documento.ad_responsable,
                                      documento.ad_estado,
                                      documento.ad_usuario_creacion,
                                      documento.ad_fecha_creacion,
                                      documento.ad_usuario_actualizacion,
                                      documento.ad_fecha_actualizacion,
                                      referenciaDocumento
                                  };

            foreach (var lineaDoc in listaDocumentos.Distinct())
            {
                reembolso = JsonConvert.DeserializeObject<Plantilla_Reembolsos>(lineaDoc.ad_registro);
                listaReembolsos.Add(
                    new Plantilla_Reembolsos_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        ReferenciaDocumento = lineaDoc.referenciaDocumento,
                        Documento = reembolso.Documento,
                        Fecha_Documento = reembolso.Fecha_Documento,
                        Soporte = reembolso.Soporte,
                        Valor_Total = Math.Round(reembolso.Valor_Total, 2),
                        Moneda = reembolso.Moneda,
                        Estado = reembolso.Estado,
                        Numero_Cheque = reembolso.Numero_Cheque,
                        Adjuntos = reembolso.Adjuntos,
                        Observaciones = reembolso.Observaciones
                    }
                    );
            }

            return JsonConvert.SerializeObject(listaReembolsos);
        }

        [WebMethod]
        public string ConsultaPlantillasRegalias(string parametros)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();
            Plantilla_Regalias regalia = new Plantilla_Regalias();
            List<Plantilla_Regalias_Base> listaRegalias = new List<Plantilla_Regalias_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            AuditoriaDocumentoProcesosController _controllerDocumentos = new AuditoriaDocumentoProcesosController();
            List<Models.AuditoriaDocumentoProcesos> _auditoriaDocumentosProcesos = new List<Models.AuditoriaDocumentoProcesos>();

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3]), 0).Where(x => x.ad_estado == "A").ToList();
            _auditoriaDocumentosProcesos = _controllerDocumentos.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), 0, 0).Where(x => x.ad_estado != "X").ToList();

            var listaDocumentos = from documento in _documentos
                                  join proceso in _auditoriaDocumentosProcesos on documento.ad_codigo equals proceso.ad_codigo into procesosDocumento
                                  from docProceso in procesosDocumento.DefaultIfEmpty()
                                  let referenciaDocumento = docProceso != null ? "S" : ""
                                  select new
                                  {
                                      documento.ad_empresa,
                                      documento.ad_auditoria,
                                      documento.ad_tarea,
                                      documento.ad_codigo,
                                      documento.ad_plantilla,
                                      documento.ad_referencia,
                                      documento.ad_registro,
                                      documento.ad_auditoria_origen,
                                      documento.ad_responsable,
                                      documento.ad_estado,
                                      documento.ad_usuario_creacion,
                                      documento.ad_fecha_creacion,
                                      documento.ad_usuario_actualizacion,
                                      documento.ad_fecha_actualizacion,
                                      referenciaDocumento
                                  };

            foreach (var lineaDoc in listaDocumentos.Distinct())
            {
                regalia = JsonConvert.DeserializeObject<Plantilla_Regalias>(lineaDoc.ad_registro);
                listaRegalias.Add(
                    new Plantilla_Regalias_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        ReferenciaDocumento = lineaDoc.referenciaDocumento,
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

        [WebMethod]
        public string ConsultaPlantillasRegularizaciones(string parametros)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();
            Plantilla_Regularizaciones regularizacion = new Plantilla_Regularizaciones();
            List<Plantilla_Regularizaciones_Base> listaRegularizaciones = new List<Plantilla_Regularizaciones_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            AuditoriaDocumentoProcesosController _controllerDocumentos = new AuditoriaDocumentoProcesosController();
            List<Models.AuditoriaDocumentoProcesos> _auditoriaDocumentosProcesos = new List<Models.AuditoriaDocumentoProcesos>();

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3]), 0).Where(x => x.ad_estado == "A").ToList();
            _auditoriaDocumentosProcesos = _controllerDocumentos.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), 0, 0).Where(x => x.ad_estado != "X").ToList();

            var listaDocumentos = from documento in _documentos
                                  join proceso in _auditoriaDocumentosProcesos on documento.ad_codigo equals proceso.ad_codigo into procesosDocumento
                                  from docProceso in procesosDocumento.DefaultIfEmpty()
                                  let referenciaDocumento = docProceso != null ? "S" : ""
                                  select new
                                  {
                                      documento.ad_empresa,
                                      documento.ad_auditoria,
                                      documento.ad_tarea,
                                      documento.ad_codigo,
                                      documento.ad_plantilla,
                                      documento.ad_referencia,
                                      documento.ad_registro,
                                      documento.ad_auditoria_origen,
                                      documento.ad_responsable,
                                      documento.ad_estado,
                                      documento.ad_usuario_creacion,
                                      documento.ad_fecha_creacion,
                                      documento.ad_usuario_actualizacion,
                                      documento.ad_fecha_actualizacion,
                                      referenciaDocumento
                                  };

            foreach (var lineaDoc in listaDocumentos.Distinct())
            {
                regularizacion = JsonConvert.DeserializeObject<Plantilla_Regularizaciones>(lineaDoc.ad_registro);
                listaRegularizaciones.Add(
                    new Plantilla_Regularizaciones_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        ReferenciaDocumento = lineaDoc.referenciaDocumento,
                        Mes = regularizacion.Mes,
                        Fecha = regularizacion.Fecha,
                        Detalle = regularizacion.Detalle,
                        Monto = Math.Round(regularizacion.Monto, 2),
                        Motivo = regularizacion.Motivo,
                        Banco_Ingreso = regularizacion.Banco_Ingreso,
                        Banco_Regularizar = regularizacion.Banco_Regularizar,
                        Cuenta = regularizacion.Cuenta,
                        Estado = regularizacion.Estado,
                        Soporte = regularizacion.Soporte
                    }
                    );
            }

            return JsonConvert.SerializeObject(listaRegularizaciones);
        }

        [WebMethod]
        public string ConsultaPlantillasTransferencias(string parametros)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();
            Plantilla_Transferencias transferencia = new Plantilla_Transferencias();
            List<Plantilla_Transferencias_Base> listaTransferencias = new List<Plantilla_Transferencias_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            AuditoriaDocumentoProcesosController _controllerDocumentos = new AuditoriaDocumentoProcesosController();
            List<Models.AuditoriaDocumentoProcesos> _auditoriaDocumentosProcesos = new List<Models.AuditoriaDocumentoProcesos>();

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3]), 0).Where(x => x.ad_estado == "A").ToList();
            _auditoriaDocumentosProcesos = _controllerDocumentos.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), 0, 0).Where(x => x.ad_estado != "X").ToList();

            var listaDocumentos = from documento in _documentos
                                  join proceso in _auditoriaDocumentosProcesos on documento.ad_codigo equals proceso.ad_codigo into procesosDocumento
                                  from docProceso in procesosDocumento.DefaultIfEmpty()
                                  let referenciaDocumento = docProceso != null ? "S" : ""
                                  select new
                                  {
                                      documento.ad_empresa,
                                      documento.ad_auditoria,
                                      documento.ad_tarea,
                                      documento.ad_codigo,
                                      documento.ad_plantilla,
                                      documento.ad_referencia,
                                      documento.ad_registro,
                                      documento.ad_auditoria_origen,
                                      documento.ad_responsable,
                                      documento.ad_estado,
                                      documento.ad_usuario_creacion,
                                      documento.ad_fecha_creacion,
                                      documento.ad_usuario_actualizacion,
                                      documento.ad_fecha_actualizacion,
                                      referenciaDocumento
                                  };

            foreach (var lineaDoc in listaDocumentos.Distinct())
            {
                transferencia = JsonConvert.DeserializeObject<Plantilla_Transferencias>(lineaDoc.ad_registro);
                listaTransferencias.Add(
                    new Plantilla_Transferencias_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        ReferenciaDocumento = lineaDoc.referenciaDocumento,
                        Item = transferencia.Item,
                        Req = transferencia.Req,
                        Proveedor = transferencia.Proveedor,
                        Concepto = transferencia.Concepto,
                        Documento = transferencia.Documento,
                        Mes = transferencia.Mes,
                        Moneda = transferencia.Moneda,
                        Importe_Monto = Math.Round(transferencia.Importe_Monto, 2),
                        Monto = Math.Round(transferencia.Monto, 2),
                        Tipo_Cambio = Math.Round(transferencia.Tipo_Cambio, 2),
                        Comprobante_Pago = transferencia.Comprobante_Pago,
                        Fecha_Pago = transferencia.Fecha_Pago,
                        Observacion_Preliminar = transferencia.Observacion_Preliminar,
                        Observacion_Final = transferencia.Observacion_Final,
                        Estado = transferencia.Estado,
                        Banco = transferencia.Banco,
                        Empresa = transferencia.Empresa,
                        Sede = transferencia.Sede,
                        Cuenta = transferencia.Cuenta,
                        Sub_Cuenta = transferencia.Sub_Cuenta,
                        Soporte = transferencia.Soporte
                    }
                    );
            }

            return JsonConvert.SerializeObject(listaTransferencias);
        }

        [WebMethod]
        public string ConsultaPlantillasTributos(string parametros)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();
            Plantilla_Tributos tributo = new Plantilla_Tributos();
            List<Plantilla_Tributos_Base> listaTributos = new List<Plantilla_Tributos_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            AuditoriaDocumentoProcesosController _controllerDocumentos = new AuditoriaDocumentoProcesosController();
            List<Models.AuditoriaDocumentoProcesos> _auditoriaDocumentosProcesos = new List<Models.AuditoriaDocumentoProcesos>();

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3]), 0).Where(x => x.ad_estado == "A").ToList();
            _auditoriaDocumentosProcesos = _controllerDocumentos.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), 0, 0).Where(x => x.ad_estado != "X").ToList();

            var listaDocumentos = from documento in _documentos
                                  join proceso in _auditoriaDocumentosProcesos on documento.ad_codigo equals proceso.ad_codigo into procesosDocumento
                                  from docProceso in procesosDocumento.DefaultIfEmpty()
                                  let referenciaDocumento = docProceso != null ? "S" : ""
                                  select new
                                  {
                                      documento.ad_empresa,
                                      documento.ad_auditoria,
                                      documento.ad_tarea,
                                      documento.ad_codigo,
                                      documento.ad_plantilla,
                                      documento.ad_referencia,
                                      documento.ad_registro,
                                      documento.ad_auditoria_origen,
                                      documento.ad_responsable,
                                      documento.ad_estado,
                                      documento.ad_usuario_creacion,
                                      documento.ad_fecha_creacion,
                                      documento.ad_usuario_actualizacion,
                                      documento.ad_fecha_actualizacion,
                                      referenciaDocumento
                                  };

            foreach (var lineaDoc in listaDocumentos.Distinct())
            {
                tributo = JsonConvert.DeserializeObject<Plantilla_Tributos>(lineaDoc.ad_registro);
                listaTributos.Add(
                    new Plantilla_Tributos_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        ReferenciaDocumento = lineaDoc.referenciaDocumento,
                        Fecha = tributo.Fecha,
                        Periodo = tributo.Periodo,
                        Tributo = tributo.Tributo,
                        Tributo_Resultante = Math.Round(tributo.Tributo_Resultante, 2),
                        Intereses = Math.Round(tributo.Intereses, 2),
                        Total_Pagar = Math.Round(tributo.Total_Pagar, 2),
                        Forma_Pago = tributo.Forma_Pago,
                        Egreso = tributo.Egreso,
                        Fecha_Informe = tributo.Fecha_Informe,
                        Numero_Informe = tributo.Numero_Informe,
                        Observaciones = tributo.Observaciones
                    }
                    );
            }

            return JsonConvert.SerializeObject(listaTributos);
        }
    }
}
