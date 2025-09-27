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
            { field: 'Fecha_Pago_Cash', headerText: 'Fecha de Pago Cash', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Lote', headerText: 'Lote', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Remuneracion_Cash', headerText: 'Remuneraciones Cash', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Remuneracion_Cheque', headerText: 'Remuneraciones Cheque', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Remuneracion_Total', headerText: 'Remuneraciones Total', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Pago', headerText: 'Fecha de Pago', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Honorarios_Planilla', headerText: 'Honorarios Planilla', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Honorarios_Incentivos', headerText: 'Honorarios Incentivos', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Honorarios_Total', headerText: 'Honorarios Total', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Pagado', headerText: 'Pagado', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Honorarios_Cesantes', headerText: 'Honorarios Cesantes', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Diferencia', headerText: 'Diferencia', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Fecha_Pago_Gratificacion', headerText: 'Fecha de Pago Gratificación', type: 'date', width: 175, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Gratificaciones', headerText: 'Gratificaciones', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Numero_Informe', headerText: 'Número de Informe', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Observaciones', headerText: 'Observaciones', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
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

        var date1 = new Date(args.data.Fecha_Pago_Cash);
        var day1 = ('0' + date1.getUTCDate()).slice(-2);
        var month1 = ('0' + (date1.getMonth() + 1)).slice(-2);
        var year1 = date1.getUTCFullYear();
        var fecha1 = `${year1}-${month1}-${day1}`;

        var date2 = new Date(args.data.Fecha_Pago);
        var day2 = ('0' + date2.getUTCDate()).slice(-2);
        var month2 = ('0' + (date2.getMonth() + 1)).slice(-2);
        var year2 = date2.getUTCFullYear();
        var fecha2 = `${year2}-${month2}-${day2}`;

        var date3 = new Date(args.data.Fecha_Pago_Gratificacion);
        var day3 = ('0' + date3.getUTCDate()).slice(-2);
        var month3 = ('0' + (date3.getMonth() + 1)).slice(-2);
        var year3 = date3.getUTCFullYear();
        var fecha3 = `${year3}-${month3}-${day3}`;

        document.getElementById('Fecha_Pago_Cash').value = fecha1;
        document.getElementById('Lote').value = args.data.Lote;
        document.getElementById('Remuneracion_Cash').value = args.data.Remuneracion_Cash;
        document.getElementById('Remuneracion_Cheque').value = args.data.Remuneracion_Cheque;
        document.getElementById('Remuneracion_Total').value = args.data.Remuneracion_Total;
        document.getElementById('Fecha_Pago').value = fecha2;
        document.getElementById('Honorarios_Planilla').value = args.data.Honorarios_Planilla;
        document.getElementById('Honorarios_Incentivos').value = args.data.Honorarios_Incentivos;
        document.getElementById('Honorarios_Total').value = args.data.Honorarios_Total;
        document.getElementById('Pagado').value = args.data.Pagado;
        document.getElementById('Honorarios_Cesantes').value = args.data.Honorarios_Cesantes;
        document.getElementById('Diferencia').value = args.data.Diferencia;
        document.getElementById('Fecha_Pago_Gratificacion').value = fecha3;
        document.getElementById('Gratificaciones').value = args.data.Gratificaciones;
        document.getElementById('Numero_Informe').value = args.data.Numero_Informe;
        document.getElementById('Observaciones').value = args.data.Observaciones;
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
            title: "Plantilla de Planillas",
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
            title: "Plantilla de Planillas",
            text: mensaje,
            icon: "error",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        }).then(function () {
            window.open("ErroresDatosPlantilla.aspx?plantilla=Plantilla de Planillas", "_blank");
        });
    }
}