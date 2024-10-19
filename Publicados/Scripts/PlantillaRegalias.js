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
            { field: 'Codigo', headerText: 'Código Relagía', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha', headerText: 'Fecha', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Descripcion', headerText: 'Descripción', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Moneda', headerText: 'Moneda', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Valor_Fijo', headerText: 'Valor Fijo', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Ingresos_Facturados', headerText: 'Ingresos Facturados', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Ingresos_Cartera', headerText: 'Ingresos Cartera Comprada', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Retencion', headerText: 'Retencion Renta 2da Categoría', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Total_Soles', headerText: 'Total Soles', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Tasa_Cambio', headerText: 'Tasa de Cambio', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Total_Dolares', headerText: 'Total Dólares', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Adjuntos', headerText: 'Archivos', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Cuenta', headerText: 'Cuenta Contable', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Soporte', headerText: 'Soporte', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Observaciones', headerText: 'Observaciones', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
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

    document.getElementById('CodigoRegalia').value = args.data.Codigo;

    var date = new Date(args.data.Fecha);
    var day = ('0' + date.getUTCDate()).slice(-2);
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var year = date.getUTCFullYear();
    var fecha = `${year}-${month}-${day}`;

    document.getElementById('Fecha').value = fecha;
    document.getElementById('Descripcion').value = args.data.Descripcion;
    document.getElementById('Moneda').value = args.data.Moneda;
    document.getElementById('Valor_Fijo').value = args.data.Valor_Fijo;
    document.getElementById('Ingresos_Facturados').value = args.data.Ingresos_Facturados;
    document.getElementById('Ingresos_Cartera').value = args.data.Ingresos_Cartera;
    document.getElementById('Retencion').value = args.data.Retencion;
    document.getElementById('Total_Soles').value = args.data.Total_Soles;
    document.getElementById('Tasa_Cambio').value = args.data.Tasa_Cambio;
    document.getElementById('Total_Dolares').value = args.data.Total_Dolares;
    document.getElementById('Adjuntos').value = args.data.Adjuntos;
    document.getElementById('Cuenta').value = args.data.Cuenta;
    document.getElementById('Soporte').value = args.data.Soporte;
    document.getElementById('Observaciones').value = args.data.Observaciones;
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
            title: "Plantilla de Regalías",
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
            title: "Plantilla de Regalías",
            text: mensaje,
            icon: "error",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        }).then(function () {
            window.open("ErroresDatosPlantilla.aspx?plantilla=Plantilla de Regalías", "_blank");
        });
    }
}
