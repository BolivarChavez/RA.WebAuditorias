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
            { field: 'IdRegistro', headerText: 'Id', width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Item', headerText: 'Item', width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Talonario', headerText: 'Talonario', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Req', headerText: 'Req', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Beneficiario', headerText: 'Beneficiario', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Comprobante', headerText: 'Comprobante', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Monto', headerText: 'Monto', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Pago', headerText: 'Fecha de Pago', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Comprobante_Egreso', headerText: 'Comprobante de Egreso', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Banco', headerText: 'Banco', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Numero_Cheque', headerText: 'Numero de Cheque', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Tipo_Cambio', headerText: 'Tipo de Cambio', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Observacion_Preliminar', headerText: 'Observacion Preliminar', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Observacion_Final', headerText: 'Observacion Final', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Estado', headerText: 'Estado', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Tipo_Plantilla', headerText: 'Tipo de Plantilla', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Cuentas', headerText: 'Cuentas', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
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
    document.getElementById('Talonario').value = args.data.Talonario;
    document.getElementById('Req').value = args.data.Req;
    document.getElementById('Beneficiario').value = args.data.Beneficiario;

    document.getElementById('Comprobante').value = args.data.Comprobante;
    document.getElementById('Monto').value = args.data.Monto;

    var date = new Date(args.data.Fecha_Pago);
    var day = ('0' + date.getUTCDate()).slice(-2);
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var year = date.getUTCFullYear();
    var fecha = `${year}-${month}-${day}`;

    document.getElementById('FechaPago').value = fecha;
    document.getElementById('ComprobanteEgreso').value = args.data.Comprobante_Egreso;

    document.getElementById('Banco').value = args.data.Banco;
    document.getElementById('NumeroCheque').value = args.data.Numero_Cheque;
    document.getElementById('TipoCambio').value = args.data.Tipo_Cambio;
    document.getElementById('ObservacionPreliminar').value = args.data.Observacion_Preliminar;

    document.getElementById('Observacion_Final').value = args.data.Observacion_Final;
    document.getElementById('Estado').value = args.data.Estado;
    document.getElementById('Tipo_Plantilla').value = args.data.Tipo_Plantilla;
    document.getElementById('Cuentas').value = args.data.Cuentas;
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
