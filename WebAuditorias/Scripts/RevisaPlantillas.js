async function CallAuditoriaTareas() {
    var strData;
    document.getElementById('GridTareas').innerHTML = "";

    const auditoria = document.getElementById('ProcesoAuditoria').value;
    const proceso = document.getElementById('HiddenField1').value;

    strParametro = "1|";
    strParametro += auditoria + "|";
    strParametro += proceso;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "AuditoriaTarea.aspx/ConsultaTareasAuditoria",
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

async function LlenaGridTareas() {
    var dataGrid = await CallAuditoriaTareas();

    var divGrid = document.getElementById("GridConsultaTareas");
    divGrid.style.height = "375px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        toolbar: ['Search'],
        allowPaging: false,
        allowScrolling: true,
        allowTextWrap: true,
        height: '100%',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        columns: [
            { field: 'at_tarea', headerText: 'Codigo', visible: false, width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ct_descripcion', headerText: 'Tarea asignada', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'at_oficina', headerText: 'Oficina de asignacion', visible: false, width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'at_asignacion', headerText: 'Descripcion', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'at_estado', headerText: 'Estado de tarea', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: seleccionTarea
    });
    grid.appendTo('#GridTareas');
}

async function seleccionTarea(args) {
    const idTarea = args.data.at_tarea;
    const idPlantilla = document.getElementById('HiddenField2').value;

    document.getElementById('HiddenField3').value = idTarea;
    document.getElementById("TituloSeccion4").innerHTML = "Plantillas asociadas a tarea : " + args.data.ct_descripcion;

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

async function ConsultaInicial() {
    document.getElementById("TituloSeccion4").innerHTML = "Plantillas asociadas a tarea";

    document.getElementById('Grid').innerHTML = "";
    document.getElementById('GridProcesos').innerHTML = "";

    await LlenaGridTareas();
}

//Plantilla de cheques

async function CargaCheques(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('ProcesoAuditoria').value;
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "PlantillaCheques.aspx/ConsultaPlantillas",
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

    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = "600px";

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
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
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
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

//Plantilla de comisiones

async function CargaComisiones(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('ProcesoAuditoria').value;
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "PlantillaComisiones.aspx/ConsultaPlantillas",
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

    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = "600px";

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
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
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
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

//Plantilla de Ingresos

async function CargaIngresos(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('ProcesoAuditoria').value;
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "PlantillaIngresos.aspx/ConsultaPlantillas",
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

    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = "600px";

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
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
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
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

//Plantilla de Mutuos

async function CargaMutuos(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('ProcesoAuditoria').value;
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "PlantillaMutuos.aspx/ConsultaPlantillas",
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

    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = "600px";

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
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
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
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

//Plantilla de Pagos

async function CargaPagos(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('ProcesoAuditoria').value;
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "PlantillaPagos.aspx/ConsultaPlantillas",
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

    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = "600px";

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
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
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
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

//Plantilla de Planillas

async function CargaPlanillas(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('ProcesoAuditoria').value;
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "PlantillaPlanillas.aspx/ConsultaPlantillas",
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

    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = "600px";

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
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
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
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

//Plantilla de Reembolsos

async function CargaReembolsos(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('ProcesoAuditoria').value;
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "PlantillaReembolsos.aspx/ConsultaPlantillas",
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

    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = "600px";

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
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
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
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

//Plantilla de Regalias

async function CargaRegalias(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('ProcesoAuditoria').value;
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "PlantillaRegalias.aspx/ConsultaPlantillas",
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

    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = "600px";

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
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
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
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

//Plantilla de Regularizaciones

async function CargaRegularizaciones(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('ProcesoAuditoria').value;
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "PlantillaRegularizaciones.aspx/ConsultaPlantillas",
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

    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = "600px";

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
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
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
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

//Plantilla de Transferencias

async function CargaTransferencias(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('ProcesoAuditoria').value;
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "PlantillaTransferencias.aspx/ConsultaPlantillas",
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

    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = "600px";

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
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
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
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

//Plantilla de Tributos

async function CargaTributos(idTarea, idPlantilla) {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('ProcesoAuditoria').value;
    const tarea = idTarea;
    const plantilla = idPlantilla;

    strParametro = "1|";
    strParametro += auditoria + "|";
    strParametro += tarea + "|";
    strParametro += plantilla;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "PlantillaTributos.aspx/ConsultaPlantillas",
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

    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = "600px";

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
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
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
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

//Seleccion de item de plantilla

async function rowSelected(args) {
    const idTarea = document.getElementById('HiddenField3').value;
    const idRegistro = args.data.IdRegistro;

    await LlenaGridProcesos(idTarea, idRegistro);
    document.getElementById("GridConsultaProcesos").scrollIntoView();
}

//Actividades asociadas a plantillas

async function CargaProcesos(idTarea, idRegistro) {
    var strData;
    document.getElementById('GridProcesos').innerHTML = "";

    const auditoria = document.getElementById('ProcesoAuditoria').value;
    const tarea = idTarea;

    strParametro = "1|";
    strParametro += auditoria + "|";
    strParametro += tarea + "|";
    strParametro += idRegistro;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "AuditoriaDocumentoProceso.aspx/ConsultaDocumentosProcesos",
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

async function LlenaGridProcesos(idTarea, idRegistro) {
    var dataGrid = await CargaProcesos(idTarea, idRegistro);

    var divGrid = document.getElementById("GridConsultaProcesos");
    divGrid.style.height = "600px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        toolbar: ['Search'],
        allowPaging: false,
        allowScrolling: true,
        height: '100%',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        columns: [
            { field: 'ad_secuencia', headerText: 'Secuencia de actividad', visible: false, width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ad_fecha', headerText: 'Fecha de actividad', type: 'date', width: 150, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ad_auditor', headerText: 'Codigo auditor', width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 'ad_responsable', headerText: 'Codigo responsable', width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 're_nombre', headerText: 'Auditor', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'responsableNombre', headerText: 'Responsable', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ad_observaciones', headerText: 'Observaciones', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ad_documento', headerText: 'Documento Relacionado', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ad_estado', headerText: 'Estado', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 }
    });
    grid.appendTo('#GridProcesos');
}

