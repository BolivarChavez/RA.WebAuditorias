﻿async function CallServerMethod() {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const tarea = document.getElementById('Tarea').value.split('-');
    const plantilla = document.getElementById('Plantilla').value.split('-');

    strParametro = "1|";
    strParametro += auditoria[0] + "|";
    strParametro += tarea[0] + "|";
    strParametro += plantilla[0];

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

async function LlenaGrid() {
    var dataGrid = await CallServerMethod();

    var contenedorHeight = document.getElementById('Contenedor').clientHeight;
    var formHeight = document.getElementById('myTab').clientHeight;
    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = contenedorHeight - formHeight - 20 + "px";

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
        gridLines: 'Both',
        columns: [
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Item', headerText: 'Item', width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Proveedor', headerText: 'Proveedor', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Concepto', headerText: 'Concepto', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Referencia', headerText: 'Referencia', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Mes', headerText: 'Mes', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Importe_Monto', headerText: 'Importe Monto', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Monto', headerText: 'Monto', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Tipo_Cambio', headerText: 'Tipo Cambio', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Comprobante_Pago', headerText: 'Comprobante Pago', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
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

function rowSelected(args) {
    document.getElementById('home-tab').click();

    document.getElementById('Codigo').value = args.data.IdRegistro;
    document.getElementById('Referencia').value = args.data.ReferenciaLinea;
    document.getElementById('Item').value = args.data.Item;
    document.getElementById('Proveedor').value = args.data.Proveedor;
    document.getElementById('Concepto').value = args.data.Concepto;
    document.getElementById('ReferenciaTransferencia').value = args.data.Referencia;
    document.getElementById('Mes').value = args.data.Mes;
    document.getElementById('Importe_Monto').value = args.data.Importe_Monto;
    document.getElementById('Monto').value = args.data.Monto;
    document.getElementById('Tipo_Cambio').value = args.data.Tipo_Cambio;
    document.getElementById('Comprobante_Pago').value = args.data.Comprobante_Pago;
    document.getElementById('Observacion_Preliminar').value = args.data.Observacion_Preliminar;
    document.getElementById('Observacion_Final').value = args.data.Observacion_Final;
    document.getElementById('Estado').value = args.data.Estado;
    document.getElementById('Banco').value = args.data.Banco;
    document.getElementById('Empresa').value = args.data.Empresa;
    document.getElementById('Sede').value = args.data.Sede;
    document.getElementById('Cuenta').value = args.data.Cuenta;
    document.getElementById('Sub_Cuenta').value = args.data.Sub_Cuenta;
    document.getElementById('Soporte').value = args.data.Soporte;
}

function muestraContenidoTexto(titulo, campo) {
    document.getElementById('HiddenField1').value = campo;
    document.getElementById('message-text').value = document.getElementById(campo).value;
    document.getElementById('tituloCampo').innerHTML = titulo;
    $('#myModal').modal('show');
}

function cierraContenidoTexto() {
    var campo = document.getElementById('HiddenField1').value;
    document.getElementById(campo).value = document.getElementById('message-text').value;
    $('#myModal').modal('hide');
}
