var empRecords = [];

async function seleccionTarea() {
    const idTarea = document.getElementById('Tarea').value.split('-')[0];
    const idPlantilla = document.getElementById('Plantilla').value.split('-')[0];

    switch (idPlantilla) {
        case "1":
            await LlenaGridCheques(idTarea, idPlantilla);
            break;

        case "2":
            await LlenaGridComisiones(idTarea, idPlantilla);
            break;

        case "3":
            await LlenaGridIngresos(idTarea, idPlantilla);
            break;

        case "4":
            await LlenaGridMutuos(idTarea, idPlantilla);
            break;

        case "5":
            await LlenaGridPagos(idTarea, idPlantilla);
            break;

        case "6":
            await LlenaGridPlanilla(idTarea, idPlantilla);
            break;

        case "7":
            await LlenaGridReembolso(idTarea, idPlantilla);
            break;

        case "8":
            await LlenaGridRegalia(idTarea, idPlantilla);
            break;

        case "9":
            await LlenaGridRegularizacion(idTarea, idPlantilla);
            break;

        case "10":
            await LlenaGridTransferencia(idTarea, idPlantilla);
            break;

        case "11":
            await LlenaGridTributo(idTarea, idPlantilla);
            break;
    }
}

//Plantilla de cheques

async function CargaCheques(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria[0] + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "ConsultaPlantillasService.asmx/ConsultaPlantillasCheques",
        data: "{" + args + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        statusCode: {
            500: function (response) {
                console.log(response)
            }
        },
        success: function (response) {
            if (response.d != '') {
                strData = response.d;
            }
        },
        fail: function (response) {
            debugger;
            alert(response.d);
        }
    });

    return strData;
}

async function LlenaGridCheques(idTarea, idPlantilla) {
    var dataGrid = await CargaCheques(idTarea, idPlantilla);

    var contenedorHeight = document.getElementById('Contenedor').clientHeight;
    var formHeight = document.getElementById('Formulario').clientHeight;
    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = contenedorHeight - formHeight - 10 + "px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        toolbar: ['Search'],
        allowPaging: false,
        allowScrolling: true,
        allowResizing: true,
        height: '100%',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        gridLines: 'Both',
        columns: [
            { type: 'checkbox', width: 50 },
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaDocumento', headerText: 'ReferenciaDocumento', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Item', headerText: 'Item', width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Talonario', headerText: 'Talonario', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Req', headerText: 'Req', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Beneficiario', headerText: 'Beneficiario', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Concepto', headerText: 'Concepto', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Comprobante', headerText: 'Comprobante', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Monto', headerText: 'Monto', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Pago', headerText: 'Fecha de Pago', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Comprobante_Egreso', headerText: 'Comprobante de Egreso', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Banco', headerText: 'Banco', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Numero_Cheque', headerText: 'Numero de Cheque', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Tipo_Cambio', headerText: 'Tipo de Cambio', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Observacion_Preliminar', headerText: 'Observacion Preliminar', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Observacion_Final', headerText: 'Observacion Final', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Estado', headerText: 'Estado', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Empresa', headerText: 'Empresa', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Sede', headerText: 'Sede', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Cuenta', headerText: 'Cuenta', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Sub_Cuenta', headerText: 'Sub Cuenta', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowDataBound: customiseCell
    });
    grid.appendTo('#Grid');
}

//Plantilla de comisiones

async function CargaComisiones(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria[0] + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "ConsultaPlantillasService.asmx/ConsultaPlantillasComisiones",
        data: "{" + args + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        statusCode: {
            500: function (response) {
                console.log(response)
            }
        },
        success: function (response) {
            if (response.d != '') {
                strData = response.d;
            }
        },
        fail: function (response) {
            debugger;
            alert(response.d);
        }
    });

    return strData;
}

async function LlenaGridComisiones(idTarea, idPlantilla) {
    var dataGrid = await CargaComisiones(idTarea, idPlantilla);

    var contenedorHeight = document.getElementById('Contenedor').clientHeight;
    var formHeight = document.getElementById('Formulario').clientHeight;
    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = contenedorHeight - formHeight - 10 + "px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        toolbar: ['Search'],
        allowPaging: false,
        allowScrolling: true,
        allowResizing: true,
        height: '100%',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        gridLines: 'Both',
        columns: [
            { type: 'checkbox', width: 50 },
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaDocumento', headerText: 'ReferenciaDocumento', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Mes', headerText: 'Mes', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Monto_Recuperado', headerText: 'Monto Recuperado', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Monto_Planilla', headerText: 'Monto Planilla', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Monto_Honorarios', headerText: 'Monto Honorarios', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Total_Incentivos', headerText: 'Total Incentivos', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Cheque_Girado', headerText: 'Cheque Girado', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Pagado', headerText: 'Pagago', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Entregado_Caja_Interna_1', headerText: 'Entregado Caja Interna 1', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'No_Girado', headerText: 'No Girado', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Informe', headerText: 'Fecha de Informe', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Contabilidad', headerText: 'Fecha de Contabilidad', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Informe_Comisiones', headerText: 'Informe Comisiones', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Entregado_Caja_Interna_2', headerText: 'Entregado Caja Interna 2', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Observaciones', headerText: 'Observaciones', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowDataBound: customiseCell
    });
    grid.appendTo('#Grid');
}

//Plantilla de Ingresos

async function CargaIngresos(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria[0] + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "ConsultaPlantillasService.asmx/ConsultaPlantillasIngresos",
        data: "{" + args + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        statusCode: {
            500: function (response) {
                console.log(response)
            }
        },
        success: function (response) {
            if (response.d != '') {
                strData = response.d;
            }
        },
        fail: function (response) {
            debugger;
            alert(response.d);
        }
    });

    return strData;
}

async function LlenaGridIngresos(idTarea, idPlantilla) {
    var dataGrid = await CargaIngresos(idTarea, idPlantilla);

    var contenedorHeight = document.getElementById('Contenedor').clientHeight;
    var formHeight = document.getElementById('Formulario').clientHeight;
    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = contenedorHeight - formHeight - 10 + "px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        toolbar: ['Search'],
        allowPaging: false,
        allowScrolling: true,
        allowResizing: true,
        height: '100%',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        gridLines: 'Both',
        columns: [
            { type: 'checkbox', width: 50 },
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaDocumento', headerText: 'ReferenciaDocumento', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Mes', headerText: 'Mes', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Factura', headerText: 'Factura', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Cuenta', headerText: 'Cuenta', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Detalle', headerText: 'Detalle', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Concepto', headerText: 'Concepto', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Subtotal', headerText: 'Sub Total', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Porcentaje', headerText: 'Porcentaje', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Total', headerText: 'Total', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Detraccion', headerText: 'Fecha de Detraccion', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Detraccion_Moneda_Destino', headerText: 'Detraccion Moneda Destino', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Detraccion_Moneda_Base', headerText: 'Detraccion Moneda Base', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Comprobante_Ingreso', headerText: 'Comprobante de Ingreso', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Neto_Ingreso', headerText: 'Neto Ingreso', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Flujo', headerText: 'Flujo', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Estado_Cuenta_1', headerText: 'Estado Cuenta 1', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Estado_Cuenta_2', headerText: 'Estado Cuenta 2', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Soporte', headerText: 'Soporte', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowDataBound: customiseCell
    });
    grid.appendTo('#Grid');
}

//Plantilla de Mutuos

async function CargaMutuos(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria[0] + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "ConsultaPlantillasService.asmx/ConsultaPlantillasMutuos",
        data: "{" + args + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        statusCode: {
            500: function (response) {
                console.log(response)
            }
        },
        success: function (response) {
            if (response.d != '') {
                strData = response.d;
            }
        },
        fail: function (response) {
            debugger;
            alert(response.d);
        }
    });

    return strData;
}

async function LlenaGridMutuos(idTarea, idPlantilla) {
    var dataGrid = await CargaMutuos(idTarea, idPlantilla);

    var contenedorHeight = document.getElementById('Contenedor').clientHeight;
    var formHeight = document.getElementById('Formulario').clientHeight;
    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = contenedorHeight - formHeight - 10 + "px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        toolbar: ['Search'],
        allowPaging: false,
        allowScrolling: true,
        allowResizing: true,
        height: '100%',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        gridLines: 'Both',
        columns: [
            { type: 'checkbox', width: 50 },
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaDocumento', headerText: 'ReferenciaDocumento', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Codigo', headerText: 'Codigo', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Documento', headerText: 'Fecha de Documento', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Inicio_Pago', headerText: 'Fecha de Inicio de Pago', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Monto_Prestamo', headerText: 'Monto de Préstamo', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Valor_Cuota', headerText: 'Valor Cuota', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Total_Cancelado', headerText: 'Total Cancelado', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Saldo_Pendiente', headerText: 'Saldo Pendiente', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Cuotas_Pendientes', headerText: 'Cuotas Pendientes', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Contrato_Adjunto', headerText: 'Contrato Adjunto', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Comprobante_Pago', headerText: 'Comprobante Pago', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Cuenta', headerText: 'Cuenta', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowDataBound: customiseCell
    });
    grid.appendTo('#Grid');
}

//Plantilla de Pagos

async function CargaPagos(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria[0] + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "ConsultaPlantillasService.asmx/ConsultaPlantillasPagos",
        data: "{" + args + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        statusCode: {
            500: function (response) {
                console.log(response)
            }
        },
        success: function (response) {
            if (response.d != '') {
                strData = response.d;
            }
        },
        fail: function (response) {
            debugger;
            alert(response.d);
        }
    });

    return strData;
}

async function LlenaGridPagos(idTarea, idPlantilla) {
    var dataGrid = await CargaPagos(idTarea, idPlantilla);

    var contenedorHeight = document.getElementById('Contenedor').clientHeight;
    var formHeight = document.getElementById('Formulario').clientHeight;
    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = contenedorHeight - formHeight - 10 + "px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        toolbar: ['Search'],
        allowPaging: false,
        allowScrolling: true,
        allowResizing: true,
        height: '100%',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        gridLines: 'Both',
        columns: [
            { type: 'checkbox', width: 50 },
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaDocumento', headerText: 'ReferenciaDocumento', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Periodo', headerText: 'Periodo', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Detalle', headerText: 'Detalle', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Pago', headerText: 'Fecha de Pago', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Importe_Bruto', headerText: 'Importe Bruto', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Descuentos', headerText: 'Descuentos', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Neto_Pagar', headerText: 'Neto a Pagar', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Transferencia', headerText: 'Transferencia', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Cheque', headerText: 'Cheque', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Diferencia', headerText: 'Diferencia', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Numero_Cheque', headerText: 'Numero de Cheque', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Numero_Informe', headerText: 'Numero de Informe', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Observaciones', headerText: 'Observaciones', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowDataBound: customiseCell
    });
    grid.appendTo('#Grid');
}

//Plantilla de Planillas

async function CargaPlanillas(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria[0] + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "ConsultaPlantillasService.asmx/ConsultaPlantillasPlanillas",
        data: "{" + args + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        statusCode: {
            500: function (response) {
                console.log(response)
            }
        },
        success: function (response) {
            if (response.d != '') {
                strData = response.d;
            }
        },
        fail: function (response) {
            debugger;
            alert(response.d);
        }
    });

    return strData;
}

async function LlenaGridPlanilla(idTarea, idPlantilla) {
    var dataGrid = await CargaPlanillas(idTarea, idPlantilla);

    var contenedorHeight = document.getElementById('Contenedor').clientHeight;
    var formHeight = document.getElementById('Formulario').clientHeight;
    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = contenedorHeight - formHeight - 10 + "px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        toolbar: ['Search'],
        allowPaging: false,
        allowScrolling: true,
        allowResizing: true,
        height: '100%',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        gridLines: 'Both',
        columns: [
            { type: 'checkbox', width: 50 },
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaDocumento', headerText: 'ReferenciaDocumento', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Mes', headerText: 'Mes', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Pago_Cash', headerText: 'Fecha de Pago Cash', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Lote', headerText: 'Lote', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Remuneracion_Cash', headerText: 'Remuneracion Cash', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Remuneracion_Cheque', headerText: 'Remuneracion Cheque', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Remuneracion_Total', headerText: 'Remuneracion Total', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Pago', headerText: 'Fecha de Pago', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Honorarios_Planilla', headerText: 'Honorarios Planilla', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Honorarios_Incentivos', headerText: 'Honorarios Incentivos', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Honorarios_Total', headerText: 'Honorarios Total', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Pagado', headerText: 'Pagado', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Honorarios_Cesantes', headerText: 'Honorarios Cesantes', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Diferencia', headerText: 'Diferencia', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Pago_Gratificacion', headerText: 'Fecha de Pago Gratificacion', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Gratificaciones', headerText: 'Gratificaciones', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Numero_Informe', headerText: 'Numero Informe', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Observaciones', headerText: 'Observaciones', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowDataBound: customiseCell
    });
    grid.appendTo('#Grid');
}

//Plantilla de Reembolsos

async function CargaReembolsos(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria[0] + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "ConsultaPlantillasService.asmx/ConsultaPlantillasReembolsos",
        data: "{" + args + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        statusCode: {
            500: function (response) {
                console.log(response)
            }
        },
        success: function (response) {
            if (response.d != '') {
                strData = response.d;
            }
        },
        fail: function (response) {
            debugger;
            alert(response.d);
        }
    });

    return strData;
}

async function LlenaGridReembolso(idTarea, idPlantilla) {
    var dataGrid = await CargaReembolsos(idTarea, idPlantilla);

    var contenedorHeight = document.getElementById('Contenedor').clientHeight;
    var formHeight = document.getElementById('Formulario').clientHeight;
    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = contenedorHeight - formHeight - 10 + "px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        toolbar: ['Search'],
        allowPaging: false,
        allowScrolling: true,
        allowResizing: true,
        height: '100%',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        gridLines: 'Both',
        columns: [
            { type: 'checkbox', width: 50 },
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaDocumento', headerText: 'ReferenciaDocumento', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Codigo', headerText: 'Codigo', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Documento', headerText: 'Fecha de Documento', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Referencia', headerText: 'No. Liquidación / Factura / Boleta', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Valor_Moneda_Destino', headerText: 'Valor Moneda Destino', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Valor_Tasa_Cambio', headerText: 'Valor Tasa Cambio', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Valor_Moneda_Base', headerText: 'Valor Moneda Base', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Estado', headerText: 'Estado', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Numero_Cheque', headerText: 'Numero Cheque', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Adjuntos', headerText: 'Adjuntos', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowDataBound: customiseCell
    });
    grid.appendTo('#Grid');
}

//Plantilla de Regalias

async function CargaRegalias(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria[0] + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "ConsultaPlantillasService.asmx/ConsultaPlantillasRegalias",
        data: "{" + args + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        statusCode: {
            500: function (response) {
                console.log(response)
            }
        },
        success: function (response) {
            if (response.d != '') {
                strData = response.d;
            }
        },
        fail: function (response) {
            debugger;
            alert(response.d);
        }
    });

    return strData;
}

async function LlenaGridRegalia(idTarea, idPlantilla) {
    var dataGrid = await CargaRegalias(idTarea, idPlantilla);

    var contenedorHeight = document.getElementById('Contenedor').clientHeight;
    var formHeight = document.getElementById('Formulario').clientHeight;
    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = contenedorHeight - formHeight - 10 + "px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        toolbar: ['Search'],
        allowPaging: false,
        allowScrolling: true,
        allowResizing: true,
        height: '100%',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        gridLines: 'Both',
        columns: [
            { type: 'checkbox', width: 50 },
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaDocumento', headerText: 'ReferenciaDocumento', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Codigo', headerText: 'Codigo', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha', headerText: 'Fecha', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Descripcion', headerText: 'Descripcion', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Valor_Fijo', headerText: 'Valor Fijo', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Valor_Proporcional', headerText: 'Valor Proporcional', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Porcentaje', headerText: 'Porcentaje', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Subtotal', headerText: 'Sub Total', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Tasa_Cambio', headerText: 'Tasa de Cambio', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Total', headerText: 'Total', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Adjuntos', headerText: 'Adjuntos', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Cuenta', headerText: 'Cuenta', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowDataBound: customiseCell
    });
    grid.appendTo('#Grid');
}

//Plantilla de Regularizaciones

async function CargaRegularizaciones(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria[0] + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "ConsultaPlantillasService.asmx/ConsultaPlantillasRegularizaciones",
        data: "{" + args + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        statusCode: {
            500: function (response) {
                console.log(response)
            }
        },
        success: function (response) {
            if (response.d != '') {
                strData = response.d;
            }
        },
        fail: function (response) {
            debugger;
            alert(response.d);
        }
    });

    return strData;
}

async function LlenaGridRegularizacion(idTarea, idPlantilla) {
    var dataGrid = await CargaRegularizaciones(idTarea, idPlantilla);

    var contenedorHeight = document.getElementById('Contenedor').clientHeight;
    var formHeight = document.getElementById('Formulario').clientHeight;
    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = contenedorHeight - formHeight - 10 + "px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        toolbar: ['Search'],
        allowPaging: false,
        allowScrolling: true,
        allowResizing: true,
        height: '100%',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        gridLines: 'Both',
        columns: [
            { type: 'checkbox', width: 50 },
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaDocumento', headerText: 'ReferenciaDocumento', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Mes', headerText: 'Mes', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha', headerText: 'Fecha', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Detalle', headerText: 'Detalle', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Monto', headerText: 'Monto', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Motivo', headerText: 'Motivo', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Banco_Ingreso', headerText: 'Banco Ingreso', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Banco_Regularizar', headerText: 'Banco Regularizar', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Cuenta', headerText: 'Cuenta', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Estado', headerText: 'Estado', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Soporte', headerText: 'Soporte', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowDataBound: customiseCell
    });
    grid.appendTo('#Grid');
}

//Plantilla de Transferencias

async function CargaTransferencias(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria[0] + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "ConsultaPlantillasService.asmx/ConsultaPlantillasTransferencias",
        data: "{" + args + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        statusCode: {
            500: function (response) {
                console.log(response)
            }
        },
        success: function (response) {
            if (response.d != '') {
                strData = response.d;
            }
        },
        fail: function (response) {
            debugger;
            alert(response.d);
        }
    });

    return strData;
}

async function LlenaGridTransferencia(idTarea, idPlantilla) {
    var dataGrid = await CargaTransferencias(idTarea, idPlantilla);

    var contenedorHeight = document.getElementById('Contenedor').clientHeight;
    var formHeight = document.getElementById('Formulario').clientHeight;
    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = contenedorHeight - formHeight - 10 + "px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        toolbar: ['Search'],
        allowPaging: false,
        allowScrolling: true,
        allowResizing: true,
        height: '100%',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        gridLines: 'Both',
        columns: [
            { type: 'checkbox', width: 50 },
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaDocumento', headerText: 'ReferenciaDocumento', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Item', headerText: 'Item', width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Req', headerText: 'Req', width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Proveedor', headerText: 'Proveedor', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Concepto', headerText: 'Concepto', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Referencia', headerText: 'Documento Factura/Recibo/Boleta', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Mes', headerText: 'Mes', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Importe_Monto', headerText: 'Importe Monto', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Monto', headerText: 'Monto', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Tipo_Cambio', headerText: 'Tipo Cambio', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Comprobante_Pago', headerText: 'Comprobante Pago', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Pago', headerText: 'Fecha de Pago', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Observacion_Preliminar', headerText: 'Observacion Preliminar', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Observacion_Final', headerText: 'Observacion Final', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Estado', headerText: 'Estado', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Banco', headerText: 'Banco', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Empresa', headerText: 'Empresa', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Sede', headerText: 'Sede', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Cuenta', headerText: 'Cuenta', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Sub_Cuenta', headerText: 'Sub Cuenta', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Soporte', headerText: 'Soporte', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowDataBound: customiseCell
    });
    grid.appendTo('#Grid');
}

//Plantilla de Tributos

async function CargaTributos(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria[0] + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "ConsultaPlantillasService.asmx/ConsultaPlantillasTributos",
        data: "{" + args + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        statusCode: {
            500: function (response) {
                console.log(response)
            }
        },
        success: function (response) {
            if (response.d != '') {
                strData = response.d;
            }
        },
        fail: function (response) {
            debugger;
            alert(response.d);
        }
    });

    return strData;
}

async function LlenaGridTributo(idTarea, idPlantilla) {
    var dataGrid = await CargaTributos(idTarea, idPlantilla);

    var contenedorHeight = document.getElementById('Contenedor').clientHeight;
    var formHeight = document.getElementById('Formulario').clientHeight;
    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = contenedorHeight - formHeight - 10 + "px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        toolbar: ['Search'],
        allowPaging: false,
        allowScrolling: true,
        allowResizing: true,
        height: '100%',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        gridLines: 'Both',
        columns: [
            { type: 'checkbox', width: 50 },
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaDocumento', headerText: 'ReferenciaDocumento', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha', headerText: 'Fecha', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Periodo', headerText: 'Periodo', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Tributo', headerText: 'Tributo', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Tributo_Resultante', headerText: 'Tributo Resultante', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Intereses', headerText: 'Intereses', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Total_Pagar', headerText: 'Total a Pagar', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Forma_Pago', headerText: 'Forma de Pago', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Egreso', headerText: 'Egreso', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Informe', headerText: 'Fecha de Informe', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Numero_Informe', headerText: 'Numero de Informe', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Observaciones', headerText: 'Observaciones', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowDataBound: customiseCell
    });
    grid.appendTo('#Grid');
}

function customiseCell(args) {
    if (args.data.ReferenciaDocumento == "S") {
        args.row.style.fontWeight = "bold";
        args.row.style.backgroundColor = "#cee9fc";
    }
}

function seleccionaRegistros() {
    if (document.getElementById('Grid').innerHTML.trim() === "") {
        empRecords = [];
    }
    else {
        var gridObj = document.getElementById('Grid')["ej2_instances"][0];

        if (gridObj) {
            empRecords = gridObj.getSelectedRecords();
        }
        else {
            empRecords = [];
        }
    }
}

function NombreArchivo() {
    var fileUpload = document.getElementById('Archivo');
    document.getElementById('Documento').value = fileUpload.files.item(0).name;
}

function ValidaDatos() {
    var observacion = document.getElementById('Observaciones').value;

    if (observacion.trim() === "") {
        Swal.fire({
            title: "Actividades relacionadas a documentos de soporte de auditoría",
            text: "No se ha ingresado el detalle de la actividad",
            icon: "error",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        });

        return false;
    }

    if (empRecords.length <= 0) {
        Swal.fire({
            title: "Actividades relacionadas a documentos de soporte de auditoría",
            text: "No se ha seleccionado ningún registro para el ingreso de la actividad",
            icon: "error",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        });

        return false;
    }

    return true;
}

async function GrabarProceso() {
    var strData;
    var strParametro;
    var retornoProceso;

    if (!ValidaDatos()) {
        return strData;
    }

    var strParametro;
    var args;

    Swal.fire({
        title: "Actividades relacionadas a documentos de soporte de auditoría",
        text: "Confirma la grabación del registro de actividad de los documentos seleccionados?",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
    }).then((result) => {
        if (result.isConfirmed) {
            const auditoria = document.getElementById('Auditoria').value.split('-');
            const tarea = document.getElementById('Tarea').value.split('-');
            seleccionaRegistros();

            for (let i = 0; i < empRecords.length; i++) {
                var obj = empRecords[i];

                if (obj.ReferenciaDocumento === "S") {
                    continue;
                }

                var date1 = new Date(document.getElementById('Fecha').value + 'T00:00:00.000Z');
                var day1 = date1.getUTCDate();
                var month1 = date1.getUTCMonth() + 1;
                var year1 = date1.getUTCFullYear();
                var fecha1 = year1 + "-" + month1 + "-" + day1;

                strParametro = "1|";
                strParametro += auditoria[0] + "|";
                strParametro += tarea[0] + "|";
                strParametro += obj.IdRegistro + "|";
                strParametro += "0" + "|";
                strParametro += fecha1 + "|";
                strParametro += document.getElementById('Auditor').value + "|";
                strParametro += document.getElementById('Responsable').value + "|";
                strParametro += document.getElementById('Observaciones').value + "|";
                strParametro += document.getElementById('Documento').value + "|";
                strParametro += document.getElementById('Estado').value;

                args = '';
                args += "'parametros':'" + strParametro + "'";

                $.ajax({
                    async: false,
                    cache: false,
                    type: "POST",
                    url: "AuditoriaDocumentoProceso.aspx/GrabarDocumentosProcesos",
                    data: "{" + args + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d != '') {
                            strData = response.d;
                        }
                    },
                    fail: function (response) {
                        debugger;
                        alert(response.d);
                    }
                });

                retornoProceso = JSON.parse(strData);

                if (retornoProceso[0]['retorno'] !== 0) {
                    break;
                }
            }

            if (retornoProceso[0]['retorno'] === 0) {
                InicializaVista();

                Swal.fire({
                    title: "Actividades relacionadas a documentos de soporte de auditoría",
                    text: "La grabación de los registros ha finalizado",
                    icon: "success",
                    confirmButtonColor: "#3085d6",
                    confirmButtonText: "Continuar"
                });
            }
            else {
                Swal.fire({
                    title: "Actividades relacionadas a documentos de soporte de auditoría",
                    text: retornoProceso[0]['mensaje'],
                    icon: "error",
                    confirmButtonColor: "#3085d6",
                    confirmButtonText: "Continuar"
                });
            }

            await seleccionTarea();
        }
        else {
            strData = "";
        }
    });

    return strData;
}

function cierraMessagePopUp() {
    $('#popupMessage').modal('hide');
}

function InicializaVista() {
    document.getElementById('Documento').value = "";
    document.getElementById('Observaciones').value = "";
}
