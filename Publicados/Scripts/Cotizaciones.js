async function CallServerMethod() {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    var date = new Date(document.getElementById('Fecha').value + 'T00:00:00.000Z');
    var year = date.getUTCFullYear();

    strParametro = "1|";
    strParametro += document.getElementById('MonedaBase').value + "|";
    strParametro += document.getElementById('MonedaDestino').value + "|";
    strParametro += year;

    var args = '';
    args += "'parametros':'" + strParametro + "'";
    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "Cotizaciones.aspx/ConsultaCotizaciones",
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
            { field: 'co_fecha_vigencia', headerText: 'Fecha de vigencia', type: 'date', format: { type: 'date', format: 'dd/MM/yyyy-hh:mm:ss' }, width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'co_cotizacion', headerText: 'Cotizacion', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'co_estado', headerText: 'Estado', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

function rowSelected(args) {
    var date = new Date(args.data.co_fecha_vigencia);
    var day = ('0' + date.getUTCDate()).slice(-2);
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var year = date.getUTCFullYear();
    var fecha = `${year}-${month}-${day}`;

    document.getElementById('Cotizacion').value = args.data.co_cotizacion;
    document.getElementById('Fecha').value = fecha;
    document.getElementById("chkEstado").checked = (args.data.co_estado === 'A') ? true : false;
}

function ValidaDatos() {
    var cotizacion;

    cotizacion = document.getElementById('Cotizacion').value;

    if (cotizacion.trim() === "" || cotizacion.trim() === "0") {
        document.getElementById('messageContent').innerHTML = "La cotizacion no puede estar sin valor o con valor cero";
        $('#popupMessage').modal('show');
        return false;
    }

    return true;
}

function GrabarCotizacion() {
    var strData;
    var strParametro;

    if (!ValidaDatos()) {
        return strData;
    }

    if (confirm("Confirma la grabacion del registro de cotizacion?")) {
        var date = new Date(document.getElementById('Fecha').value + 'T00:00:00.000Z');
        var day = date.getUTCDate();
        var month = date.getUTCMonth() + 1;
        var year = date.getUTCFullYear();
        var fechaCotizacion = year + "-" + month + "-" + day;

        strParametro = "1|";
        strParametro += document.getElementById('MonedaBase').value + "|";
        strParametro += document.getElementById('MonedaDestino').value + "|";
        strParametro += document.getElementById('Cotizacion').value + "|";
        strParametro += fechaCotizacion + "|";

        if (document.getElementById("chkEstado").checked) {
            strParametro += "A|";
        }
        else {
            strParametro += "I|";
        }

        strParametro += document.getElementById('HiddenField1').value;

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "Cotizaciones.aspx/GrabarCotizacion",
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

    document.getElementById('messageContent').innerHTML = "La grabación del registro ha finalizado";
    $('#popupMessage').modal('show');

    LlenaGrid();
    return strData;
}

function cierraMessagePopUp() {
    $('#popupMessage').modal('hide');
}