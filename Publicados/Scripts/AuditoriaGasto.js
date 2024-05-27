﻿async function CallServerMethod() {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('Auditoria').value.split('-');

    strParametro = "1|";
    strParametro += auditoria[0];

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "AuditoriaGasto.aspx/ConsultaGastosAuditoria",
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
        height: '100%',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        columns: [
            { field: 'ag_secuencia', headerText: 'Codigo', visible: false, width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ag_tipo', headerText: 'Tipo de Gasto', visible: false, width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'cg_descripion', headerText: 'Tipo de Gasto', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ag_fecha_inicio', headerText: 'Fecha de inicio de gasto', type: 'date', width: 150, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ag_fecha_fin', headerText: 'Fecha de fin de gasto', type: 'date', width: 150, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ag_valor', headerText: 'Total de Gasto', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ag_estado', headerText: 'Estado de tarea', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

function rowSelected(args) {
    var date1 = new Date(args.data.ag_fecha_inicio);
    var day1 = ('0' + date1.getUTCDate()).slice(-2);
    var month1 = ('0' + (date1.getMonth() + 1)).slice(-2);
    var year1 = date1.getUTCFullYear();
    var fecha1 = `${year1}-${month1}-${day1}`;

    var date2 = new Date(args.data.ag_fecha_fin);
    var day2 = ('0' + date2.getUTCDate()).slice(-2);
    var month2 = ('0' + (date2.getMonth() + 1)).slice(-2);
    var year2 = date2.getUTCFullYear();
    var fecha2 = `${year2}-${month2}-${day2}`;

    document.getElementById('Codigo').value = args.data.ag_secuencia;
    document.getElementById('FechaInicio').value = fecha1;
    document.getElementById('FechaFin').value = fecha2;
    document.getElementById('Valor').value = args.data.ag_valor;

    var dropdownlistbox1 = document.getElementById("TipoGasto")

    for (var i = 0; i <= dropdownlistbox1.length - 1; i++) {
        if (args.data.ag_tipo == dropdownlistbox1.options[i].value)
            dropdownlistbox1.selectedIndex = i;
    }

    document.getElementById("chkEstado").checked = (args.data.ag_estado === 'A') ? true : false;
}

function ValidaDatos() {
    var fechaInicio = new Date(document.getElementById('FechaInicio').value + 'T00:00:00.000Z');
    var fechaFin = new Date(document.getElementById('FechaFin').value + 'T00:00:00.000Z');
    var valor = document.getElementById('Valor').value
    var fecha1 = document.getElementById('FechaInicio').value;
    var fecha2 = document.getElementById('FechaFin').value;

    if (!valor || valor == "") {
        document.getElementById('messageContent').innerHTML = "ERROR : Valor no permitido";
        $('#popupMessage').modal('show');
        return false;
    }

    if (valor <= 0) {
        document.getElementById('messageContent').innerHTML = "ERROR : No se permiten valores en blanco o menores o iguales a cero";
        $('#popupMessage').modal('show');
        return false;
    }

    if (!fecha1) {
        document.getElementById('messageContent').innerHTML = "ERROR : Fecha de inicio de gasto no valida";
        $('#popupMessage').modal('show');
        return false;
    }

    if (!fecha2) {
        document.getElementById('messageContent').innerHTML = "ERROR : Fecha de fin de gasto no valida";
        $('#popupMessage').modal('show');
        return false;
    }

    if (fechaFin < fechaInicio) {
        document.getElementById('messageContent').innerHTML = "ERROR : La fecha de fin de gasto no debe ser anterior o igual a la de inicio";
        $('#popupMessage').modal('show');
        return false;
    }

    return true;
}

function GrabarProceso() {
    var strData;
    var strParametro;

    if (!ValidaDatos()) {
        return strData;
    }

    if (confirm("Confirma la grabacion del registro de auditoria?")) {
        var date1 = new Date(document.getElementById('FechaInicio').value + 'T00:00:00.000Z');
        var day1 = date1.getUTCDate();
        var month1 = date1.getUTCMonth() + 1;
        var year1 = date1.getUTCFullYear();
        var fecha1 = year1 + "-" + month1 + "-" + day1;

        var date2 = new Date(document.getElementById('FechaFin').value + 'T00:00:00.000Z');
        var day2 = date2.getUTCDate();
        var month2 = date2.getUTCMonth() + 1;
        var year2 = date2.getUTCFullYear();
        var fecha2 = year2 + "-" + month2 + "-" + day2;

        strParametro = "1|";
        strParametro += document.getElementById('Auditoria').value.split('-')[0] + "|";
        strParametro += document.getElementById('Codigo').value + "|";
        strParametro += document.getElementById('TipoGasto').value + "|";
        strParametro += fecha1 + "|";
        strParametro += fecha2 + "|";
        strParametro += document.getElementById('Valor').value + "|";

        if (document.getElementById("chkEstado").checked) {
            strParametro += "A";
        }
        else {
            strParametro += "I";
        }

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "AuditoriaGasto.aspx/GrabarAuditoriaGasto",
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
    }
    else {
        strData = "";
    }

    var retornoProceso = JSON.parse(strData)

    if (retornoProceso[0]['retorno'] === 0) {
        InicializaVista();
        document.getElementById('messageContent').innerHTML = "La grabación del registro ha finalizado";
        $('#popupMessage').modal('show');
    }
    else {
        document.getElementById('messageContent').innerHTML = "ERROR : " + retornoProceso[0]['mensaje'];
        $('#popupMessage').modal('show');
    }

    LlenaGrid();
    return strData;
}

function EliminarProceso() {
    var strData;
    var strParametro;

    if (confirm("Confirma la eliminación del registro de gasto auditoría?")) {
        var date1 = new Date(document.getElementById('FechaInicio').value + 'T00:00:00.000Z');
        var day1 = date1.getUTCDate();
        var month1 = date1.getUTCMonth() + 1;
        var year1 = date1.getUTCFullYear();
        var fecha1 = year1 + "-" + month1 + "-" + day1;

        var date2 = new Date(document.getElementById('FechaFin').value + 'T00:00:00.000Z');
        var day2 = date2.getUTCDate();
        var month2 = date2.getUTCMonth() + 1;
        var year2 = date2.getUTCFullYear();
        var fecha2 = year2 + "-" + month2 + "-" + day2;

        strParametro = "1|";
        strParametro += document.getElementById('Auditoria').value.split('-')[0] + "|";
        strParametro += document.getElementById('Codigo').value + "|";
        strParametro += document.getElementById('TipoGasto').value + "|";
        strParametro += fecha1 + "|";
        strParametro += fecha2 + "|";
        strParametro += document.getElementById('Valor').value + "|";
        strParametro += "X";

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "AuditoriaGasto.aspx/GrabarAuditoriaGasto",
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
    }
    else {
        strData = "";
    }

    var retornoProceso = JSON.parse(strData)

    if (retornoProceso[0]['retorno'] === 0) {
        InicializaVista();
        document.getElementById('messageContent').innerHTML = "La eliminación del registro ha finalizado";
        $('#popupMessage').modal('show');
    }
    else {
        document.getElementById('messageContent').innerHTML = "ERROR : " + retornoProceso[0]['mensaje'];
        $('#popupMessage').modal('show');
    }

    LlenaGrid();
    return strData;
}

function cierraMessagePopUp() {
    $('#popupMessage').modal('hide');
}

function InicializaVista() {
    document.getElementById('Codigo').value = "0";
    document.getElementById('Valor').value = "0";
}