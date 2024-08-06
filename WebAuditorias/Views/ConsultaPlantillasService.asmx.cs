using Newtonsoft.Json;
using PrototipoData.Models;
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
                        Monto = cheque.Monto,
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
                        Monto_Recuperado = comision.Monto_Recuperado,
                        Monto_Planilla = comision.Monto_Planilla,
                        Monto_Honorarios = comision.Monto_Honorarios,
                        Total_Incentivos = comision.Total_Incentivos,
                        Cheque_Girado = comision.Cheque_Girado,
                        Pagado = comision.Pagado,
                        Entregado_Caja_Interna_1 = comision.Entregado_Caja_Interna_1,
                        No_Girado = comision.No_Girado,
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
                        Subtotal = ingreso.Subtotal,
                        Porcentaje = ingreso.Porcentaje,
                        Total = ingreso.Total,
                        Fecha_Detraccion = ingreso.Fecha_Detraccion,
                        Detraccion_Moneda_Destino = ingreso.Detraccion_Moneda_Destino,
                        Detraccion_Moneda_Base = ingreso.Detraccion_Moneda_Base,
                        Comprobante_Ingreso = ingreso.Comprobante_Ingreso,
                        Neto_Ingreso = ingreso.Neto_Ingreso,
                        Flujo = ingreso.Flujo,
                        Estado_Cuenta_1 = ingreso.Estado_Cuenta_1,
                        Estado_Cuenta_2 = ingreso.Estado_Cuenta_2,
                        Soporte = ingreso.Soporte
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
                        Fecha_Documento = mutuo.Fecha_Documento,
                        Fecha_Inicio_Pago = mutuo.Fecha_Inicio_Pago,
                        Monto_Prestamo = mutuo.Monto_Prestamo,
                        Valor_Cuota = mutuo.Valor_Cuota,
                        Total_Cancelado = mutuo.Total_Cancelado,
                        Saldo_Pendiente = mutuo.Saldo_Pendiente,
                        Cuotas_Pendientes = mutuo.Cuotas_Pendientes,
                        Contrato_Adjunto = mutuo.Contrato_Adjunto,
                        Comprobante_Pago = mutuo.Comprobante_Pago,
                        Cuenta = mutuo.Cuenta
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
                        Descuentos = pago.Descuentos,
                        Neto_Pagar = pago.Neto_Pagar,
                        Transferencia = pago.Transferencia,
                        Cheque = pago.Cheque,
                        Diferencia = pago.Diferencia,
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
                        Remuneracion_Cash = planilla.Remuneracion_Cash,
                        Remuneracion_Cheque = planilla.Remuneracion_Cheque,
                        Remuneracion_Total = planilla.Remuneracion_Total,
                        Fecha_Pago = planilla.Fecha_Pago,
                        Honorarios_Planilla = planilla.Honorarios_Planilla,
                        Honorarios_Incentivos = planilla.Honorarios_Incentivos,
                        Honorarios_Total = planilla.Honorarios_Total,
                        Pagado = planilla.Pagado,
                        Honorarios_Cesantes = planilla.Honorarios_Cesantes,
                        Diferencia = planilla.Diferencia,
                        Fecha_Pago_Gratificacion = planilla.Fecha_Pago_Gratificacion,
                        Gratificaciones = planilla.Gratificaciones,
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
                        Codigo = reembolso.Codigo,
                        Fecha_Documento = reembolso.Fecha_Documento,
                        Referencia = reembolso.Referencia,
                        Valor_Moneda_Destino = reembolso.Valor_Moneda_Destino,
                        Valor_Tasa_Cambio = reembolso.Valor_Tasa_Cambio,
                        Valor_Moneda_Base = reembolso.Valor_Moneda_Base,
                        Estado = reembolso.Estado,
                        Numero_Cheque = reembolso.Numero_Cheque,
                        Adjuntos = reembolso.Adjuntos
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
                        Valor_Fijo = regalia.Valor_Fijo,
                        Valor_Proporcional = regalia.Valor_Proporcional,
                        Porcentaje = regalia.Porcentaje,
                        Subtotal = regalia.Subtotal,
                        Tasa_Cambio = regalia.Tasa_Cambio,
                        Total = regalia.Total,
                        Adjuntos = regalia.Adjuntos,
                        Cuenta = regalia.Cuenta
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
                        Monto = regularizacion.Monto,
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
                        Referencia = transferencia.Referencia,
                        Mes = transferencia.Mes,
                        Importe_Monto = transferencia.Importe_Monto,
                        Monto = transferencia.Monto,
                        Tipo_Cambio = transferencia.Tipo_Cambio,
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
                        Tributo_Resultante = tributo.Tributo_Resultante,
                        Intereses = tributo.Intereses,
                        Total_Pagar = tributo.Total_Pagar,
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
