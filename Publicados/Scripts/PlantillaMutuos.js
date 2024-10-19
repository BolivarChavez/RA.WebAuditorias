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
        allowResizing: true,
        height: '100%',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        gridLines: 'Both',
        columns: [
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Codigo', headerText: 'Codigo Mutuo', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Banco', headerText: 'Banco', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Moneda', headerText: 'Moneda', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Detalle', headerText: 'Detalle', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Documento', headerText: 'Fecha de Documento', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Monto_Prestamo', headerText: 'Monto de Préstamo', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Pago_Cuota', headerText: 'Fecha de Pago de Cuota', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Numero_Cuota', headerText: 'Numero de Cuota', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Valor_Cuota', headerText: 'Valor Cuota', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Comprobante_Pago', headerText: 'Comprobante de Pago', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Saldo_Pendiente', headerText: 'Saldo Pendiente', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Cuotas_Pendientes', headerText: 'Cuotas Pendientes', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Documento_Legal', headerText: 'Documento Legal', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Observacion', headerText: 'Observaciones', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
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

    document.getElementById('CodigoMutuo').value = args.data.Codigo;
    document.getElementById('Banco').value = args.data.Banco;
    document.getElementById('Moneda').value = args.data.Moneda;
    document.getElementById('Detalle').value = args.data.Detalle;

    var date1 = new Date(args.data.Fecha_Documento);
    var day1 = ('0' + date1.getUTCDate()).slice(-2);
    var month1 = ('0' + (date1.getMonth() + 1)).slice(-2);
    var year1 = date1.getUTCFullYear();
    var fecha1 = `${year1}-${month1}-${day1}`;

    var date2 = new Date(args.data.Fecha_Pago_Cuota);
    var day2 = ('0' + date2.getUTCDate()).slice(-2);
    var month2 = ('0' + (date2.getMonth() + 1)).slice(-2);
    var year2 = date1.getUTCFullYear();
    var fecha2 = `${year2}-${month2}-${day2}`;

    document.getElementById('Fecha_Documento').value = fecha1;
    document.getElementById('Monto_Prestamo').value = args.data.Monto_Prestamo;
    document.getElementById('Fecha_Pago_Cuota').value = fecha2;
    document.getElementById('Numero_Cuota').value = args.data.Numero_Cuota;
    document.getElementById('Valor_Cuota').value = args.data.Valor_Cuota;
    document.getElementById('Comprobante_Pago').value = args.data.Comprobante_Pago;
    document.getElementById('Saldo_Pendiente').value = args.data.Saldo_Pendiente;
    document.getElementById('Cuotas_Pendientes').value = args.data.Cuotas_Pendientes;
    document.getElementById('Documento_Legal').value = args.data.Documento_Legal;
    document.getElementById('Observacion').value = args.data.Observacion;
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

function mensajeGrabacion(respuesta, mensaje) {
    if (respuesta === "1") {
        Swal.fire({
            title: "Plantilla de Mutuos",
            text: mensaje,
            icon: "success",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        }).then(function () {
            document.getElementById('profile-tab').click();
            LlenaGrid();
        });
    }
    else {
        Swal.fire({
            title: "Plantilla de Mutuos",
            text: mensaje,
            icon: "error",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        }).then(function () {
            window.open("ErroresDatosPlantilla.aspx?plantilla=Plantilla de Mutuos", "_blank");
        });
    }
}