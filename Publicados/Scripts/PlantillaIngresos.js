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
        selectionSettings: { mode: 'Row', type: 'Multiple' },
        columns: [
            { type: 'checkbox', width: 50 },
            { field: 'IdRegistro', headerText: 'Id', visible: false, width: 75, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ReferenciaLinea', headerText: 'Referencia', visible: false, width: 125, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Mes', headerText: 'Mes', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Factura', headerText: 'Factura', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Cuenta', headerText: 'Cuenta', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Detalle', headerText: 'Detalle', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Concepto', headerText: 'Concepto', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Moneda', headerText: 'Moneda', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Subtotal', headerText: 'Sub Total', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Porcentaje', headerText: 'IGV', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Total', headerText: 'Total', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Detraccion', headerText: 'Fecha de Detracción', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Detraccion_Moneda_Destino', headerText: 'Detracción Moneda Destino', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Neto_Ingreso', headerText: 'Neto Ingreso', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Flujo', headerText: 'Flujo', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Estado_Cuenta_1', headerText: 'Estado Cuenta 1', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Estado_Cuenta_2', headerText: 'Estado Cuenta 2', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Soporte', headerText: 'Soporte', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Observacion', headerText: 'Observaciones', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Banco', headerText: 'Banco', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Empresa', headerText: 'Empresa', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Sede', headerText: 'Sede', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Cuenta_Contable', headerText: 'Cuenta Contable', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'SubCuenta', headerText: 'Sub Cuenta Contable', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

function rowSelected(args) {
    if (document.getElementById("chkEliminaTodos").checked === true) {
        var selectedRecords = this.getSelectedRecords();
        var ids = selectedRecords.map(r => r.IdRegistro).join(',');

        document.getElementById('HiddenField3').value = ids;
    }
    else {
        document.getElementById('home-tab').click();

        document.getElementById('Codigo').value = args.data.IdRegistro;
        document.getElementById('Referencia').value = args.data.ReferenciaLinea;

        document.getElementById('Mes').value = args.data.Mes;
        document.getElementById('Factura').value = args.data.Factura;
        document.getElementById('Cuenta').value = args.data.Cuenta;
        document.getElementById('Detalle').value = args.data.Detalle;
        document.getElementById('Concepto').value = args.data.Concepto;
        document.getElementById('Moneda').value = args.data.Moneda;
        document.getElementById('Subtotal').value = args.data.Subtotal;
        document.getElementById('Porcentaje').value = args.data.Porcentaje;
        document.getElementById('Total').value = args.data.Total;

        var date = new Date(args.data.Fecha_Detraccion);
        var day = ('0' + date.getUTCDate()).slice(-2);
        var month = ('0' + (date.getMonth() + 1)).slice(-2);
        var year = date.getUTCFullYear();
        var fecha = `${year}-${month}-${day}`;

        document.getElementById('Fecha_Detraccion').value = fecha;
        document.getElementById('Detraccion_Moneda_Destino').value = args.data.Detraccion_Moneda_Destino;
        document.getElementById('Neto_Ingreso').value = args.data.Neto_Ingreso;
        document.getElementById('Flujo').value = args.data.Flujo;
        document.getElementById('Estado_Cuenta_1').value = args.data.Estado_Cuenta_1;
        document.getElementById('Estado_Cuenta_2').value = args.data.Estado_Cuenta_2;
        document.getElementById('Soporte').value = args.data.Soporte;
        document.getElementById('Observacion').value = args.data.Observacion;
        document.getElementById('Banco').value = args.data.Banco;
        document.getElementById('Empresa').value = args.data.Empresa;
        document.getElementById('Sede').value = args.data.Sede;
        document.getElementById('Cuenta_Contable').value = args.data.Cuenta_Contable;
        document.getElementById('SubCuenta').value = args.data.SubCuenta;
    }
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
            title: "Plantilla de Ingresos",
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
            title: "Plantilla de Ingresos",
            text: mensaje,
            icon: "error",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        }).then(function () {
            window.open("ErroresDatosPlantilla.aspx?plantilla=Plantilla de Ingresos", "_blank");
        });
    }
}