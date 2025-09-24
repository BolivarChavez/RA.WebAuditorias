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
            { field: 'Monto_Recuperado', headerText: 'Monto Recuperado', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Monto_Planilla', headerText: 'Monto Planilla', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Monto_Honorarios', headerText: 'Monto Honorarios', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Total_Incentivos', headerText: 'Total Incentivos', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Cheque_Girado', headerText: 'Cheque Girado', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Pagado', headerText: 'Pagado', width: 175, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
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
        document.getElementById('Monto_Recuperado').value = args.data.Monto_Recuperado;
        document.getElementById('Monto_Planilla').value = args.data.Monto_Planilla;
        document.getElementById('Monto_Honorarios').value = args.data.Monto_Honorarios;
        document.getElementById('Total_Incentivos').value = args.data.Total_Incentivos;
        document.getElementById('Cheque_Girado').value = args.data.Cheque_Girado;
        document.getElementById('Pagado').value = args.data.Pagado;
        document.getElementById('Entregado_Caja_Interna_1').value = args.data.Entregado_Caja_Interna_1;
        document.getElementById('No_Girado').value = args.data.No_Girado;

        var date1 = new Date(args.data.Fecha_Informe);
        var day1 = ('0' + date1.getUTCDate()).slice(-2);
        var month1 = ('0' + (date1.getMonth() + 1)).slice(-2);
        var year1 = date1.getUTCFullYear();
        var fecha1 = `${year1}-${month1}-${day1}`;

        var date2 = new Date(args.data.Fecha_Contabilidad);
        var day2 = ('0' + date2.getUTCDate()).slice(-2);
        var month2 = ('0' + (date2.getMonth() + 1)).slice(-2);
        var year2 = date2.getUTCFullYear();
        var fecha2 = `${year2}-${month2}-${day2}`;

        document.getElementById('Fecha_Informe').value = fecha1;
        document.getElementById('Fecha_Contabilidad').value = fecha2;
        document.getElementById('Informe_Comisiones').value = args.data.Informe_Comisiones;
        document.getElementById('Entregado_Caja_Interna_2').value = args.data.Entregado_Caja_Interna_2;
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
            title: "Plantilla de Comisiones",
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
            title: "Plantilla de Comisiones",
            text: mensaje,
            icon: "error",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        }).then(function () {
            window.open("ErroresDatosPlantilla.aspx?plantilla=Plantilla de Comisiones", "_blank");
        });
    }
}