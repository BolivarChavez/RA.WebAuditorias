async function CallServerMethod() {
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
            { field: 'Codigo', headerText: 'Codigo', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Documento', headerText: 'Fecha de Documento', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Referencia', headerText: 'Referencia', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
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

function rowSelected(args) {
    document.getElementById('home-tab').click();

    document.getElementById('Codigo').value = args.data.IdRegistro;
    document.getElementById('Referencia').value = args.data.ReferenciaLinea;

    document.getElementById('CodigoReembolso').value = args.data.Codigo;

    var date = new Date(args.data.Fecha_Documento);
    var day = ('0' + date.getUTCDate()).slice(-2);
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var year = date.getUTCFullYear();
    var fecha = `${year}-${month}-${day}`;

    document.getElementById('Fecha_Documento').value = fecha;
    document.getElementById('ReferenciaReembolso').value = args.data.Referencia;
    document.getElementById('Valor_Moneda_Destino').value = args.data.Valor_Moneda_Destino;
    document.getElementById('Valor_Tasa_Cambio').value = args.data.Valor_Tasa_Cambio;
    document.getElementById('Valor_Moneda_Base').value = args.data.Valor_Moneda_Base;
    document.getElementById('Estado').value = args.data.Estado;
    document.getElementById('Numero_Cheque').value = args.data.Numero_Cheque;
    document.getElementById('Adjuntos').value = args.data.Adjuntos;
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
