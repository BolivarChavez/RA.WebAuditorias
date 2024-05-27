async function CallServerMethod() {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const tarea = document.getElementById('Tarea').value.split('-');

    strParametro = "1|";
    strParametro += auditoria[0] + "|";
    strParametro += tarea[0] + "|";
    strParametro += document.getElementById('HiddenField1').value;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "AuditoriaDocumentoProceso.aspx/ConsultaDocumentosProcesos",
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
            { field: 'ad_secuencia', headerText: 'Secuencia de actividad', visible: false, width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ad_fecha', headerText: 'Fecha de actividad', type: 'date', width: 150, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ad_auditor', headerText: 'Codigo auditor', width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 'ad_responsable', headerText: 'Codigo responsable', width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 're_nombre', headerText: 'Auditor', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'responsableNombre', headerText: 'Responsable', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ad_observaciones', headerText: 'Observaciones', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ad_documento', headerText: 'Documento Relacionado', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ad_estado', headerText: 'Estado', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

function rowSelected(args) {
    var date1 = new Date(args.data.ad_fecha);
    var day1 = ('0' + date1.getUTCDate()).slice(-2);
    var month1 = ('0' + (date1.getMonth() + 1)).slice(-2);
    var year1 = date1.getUTCFullYear();
    var fecha1 = `${year1}-${month1}-${day1}`;

    document.getElementById('Codigo').value = args.data.ad_secuencia;
    document.getElementById('Fecha').value = fecha1;
    document.getElementById('Observaciones').value = args.data.ad_observaciones;
    document.getElementById('Documento').value = args.data.ad_documento;

    var dropdownlistbox1 = document.getElementById("Auditor")

    for (var i = 0; i <= dropdownlistbox1.length - 1; i++) {
        if (args.data.ad_auditor == dropdownlistbox1.options[i].value)
            dropdownlistbox1.selectedIndex = i;
    }

    var dropdownlistbox2 = document.getElementById("Responsable")

    for (var i = 0; i <= dropdownlistbox2.length - 1; i++) {
        if (args.data.ad_responsable == dropdownlistbox2.options[i].value)
            dropdownlistbox2.selectedIndex = i;
    }

    var dropdownlistbox3 = document.getElementById("Estado")
    for (var i = 0; i <= dropdownlistbox3.length - 1; i++) {
        if (args.data.ad_estado == dropdownlistbox3.options[i].value)
            dropdownlistbox3.selectedIndex = i;
    }
}

function ValidaDatos() {
    var observacion = document.getElementById('Observaciones').value;

    if (observacion.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado el detalle de la actividad";
        $('#popupMessage').modal('show');
        return false;
    }

    return true;
}

function GrabarProceso() {
    var strData;
    var strParametro;

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const tarea = document.getElementById('Tarea').value.split('-');

    if (!ValidaDatos()) {
        return strData;
    }

    if (confirm("Confirma la grabación del registro de actividad del documento?")) {
        var date1 = new Date(document.getElementById('Fecha').value + 'T00:00:00.000Z');
        var day1 = date1.getUTCDate();
        var month1 = date1.getUTCMonth() + 1;
        var year1 = date1.getUTCFullYear();
        var fecha1 = year1 + "-" + month1 + "-" + day1;

        strParametro = "1|";
        strParametro += auditoria[0] + "|";
        strParametro += tarea[0] + "|";
        strParametro += document.getElementById('HiddenField1').value + "|";
        strParametro += document.getElementById('Codigo').value + "|";
        strParametro += fecha1 + "|";
        strParametro += document.getElementById('Auditor').value + "|";
        strParametro += document.getElementById('Responsable').value + "|";
        strParametro += document.getElementById('Observaciones').value + "|";
        strParametro += document.getElementById('Documento').value + "|";

        if (document.getElementById('Codigo').value === "0") {
            strParametro += "A";
        }
        else {
            strParametro += document.getElementById('Estado').value;
        }

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "AuditoriaDocumentoProceso.aspx/GrabarDocumentosProcesos",
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

function ActivarProceso() {
    var strData;
    var strParametro;

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const tarea = document.getElementById('Tarea').value.split('-');

    if (confirm("Confirma la activación del registro de actividad del documento?")) {
        var date1 = new Date(document.getElementById('Fecha').value + 'T00:00:00.000Z');
        var day1 = date1.getUTCDate();
        var month1 = date1.getUTCMonth() + 1;
        var year1 = date1.getUTCFullYear();
        var fecha1 = year1 + "-" + month1 + "-" + day1;

        strParametro = "1|";
        strParametro += auditoria[0] + "|";
        strParametro += tarea[0] + "|";
        strParametro += document.getElementById('HiddenField1').value + "|";
        strParametro += document.getElementById('Codigo').value + "|";
        strParametro += fecha1 + "|";
        strParametro += document.getElementById('Auditor').value + "|";
        strParametro += document.getElementById('Responsable').value + "|";
        strParametro += document.getElementById('Observaciones').value + "|";
        strParametro += document.getElementById('Documento').value + "|";
        strParametro += "A";

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "AuditoriaDocumentoProceso.aspx/GrabarDocumentosProcesos",
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
        document.getElementById('messageContent').innerHTML = "La activación del registro ha finalizado";
        $('#popupMessage').modal('show');
    }
    else {
        document.getElementById('messageContent').innerHTML = "ERROR : " + retornoProceso[0]['mensaje'];
        $('#popupMessage').modal('show');
    }

    LlenaGrid();
    return strData;
}

function InactivarProceso() {
    var strData;
    var strParametro;

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const tarea = document.getElementById('Tarea').value.split('-');

    if (confirm("Confirma la inactivación del registro de actividad del documento?")) {
        var date1 = new Date(document.getElementById('Fecha').value + 'T00:00:00.000Z');
        var day1 = date1.getUTCDate();
        var month1 = date1.getUTCMonth() + 1;
        var year1 = date1.getUTCFullYear();
        var fecha1 = year1 + "-" + month1 + "-" + day1;

        strParametro = "1|";
        strParametro += auditoria[0] + "|";
        strParametro += tarea[0] + "|";
        strParametro += document.getElementById('HiddenField1').value + "|";
        strParametro += document.getElementById('Codigo').value + "|";
        strParametro += fecha1 + "|";
        strParametro += document.getElementById('Auditor').value + "|";
        strParametro += document.getElementById('Responsable').value + "|";
        strParametro += document.getElementById('Observaciones').value + "|";
        strParametro += document.getElementById('Documento').value + "|";
        strParametro += "I";

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "AuditoriaDocumentoProceso.aspx/GrabarDocumentosProcesos",
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
        document.getElementById('messageContent').innerHTML = "La inactivación del registro ha finalizado";
        $('#popupMessage').modal('show');
    }
    else {
        document.getElementById('messageContent').innerHTML = "ERROR : " + retornoProceso[0]['mensaje'];
        $('#popupMessage').modal('show');
    }

    LlenaGrid();
    return strData;
}

function CerrarProceso() {
    var strData;
    var strParametro;

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const tarea = document.getElementById('Tarea').value.split('-');

    if (confirm("Confirma el cierre del registro de actividad del documento?")) {
        var date1 = new Date(document.getElementById('Fecha').value + 'T00:00:00.000Z');
        var day1 = date1.getUTCDate();
        var month1 = date1.getUTCMonth() + 1;
        var year1 = date1.getUTCFullYear();
        var fecha1 = year1 + "-" + month1 + "-" + day1;

        strParametro = "1|";
        strParametro += auditoria[0] + "|";
        strParametro += tarea[0] + "|";
        strParametro += document.getElementById('HiddenField1').value + "|";
        strParametro += document.getElementById('Codigo').value + "|";
        strParametro += fecha1 + "|";
        strParametro += document.getElementById('Auditor').value + "|";
        strParametro += document.getElementById('Responsable').value + "|";
        strParametro += document.getElementById('Observaciones').value + "|";
        strParametro += document.getElementById('Documento').value + "|";
        strParametro += "C";

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "AuditoriaDocumentoProceso.aspx/GrabarDocumentosProcesos",
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
        document.getElementById('messageContent').innerHTML = "El cierre del registro ha finalizado";
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

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const tarea = document.getElementById('Tarea').value.split('-');

    if (confirm("Confirma la eliminación del registro de actividad del documento?")) {
        var date1 = new Date(document.getElementById('Fecha').value + 'T00:00:00.000Z');
        var day1 = date1.getUTCDate();
        var month1 = date1.getUTCMonth() + 1;
        var year1 = date1.getUTCFullYear();
        var fecha1 = year1 + "-" + month1 + "-" + day1;

        strParametro = "1|";
        strParametro += auditoria[0] + "|";
        strParametro += tarea[0] + "|";
        strParametro += document.getElementById('HiddenField1').value + "|";
        strParametro += document.getElementById('Codigo').value + "|";
        strParametro += fecha1 + "|";
        strParametro += document.getElementById('Auditor').value + "|";
        strParametro += document.getElementById('Responsable').value + "|";
        strParametro += document.getElementById('Observaciones').value + "|";
        strParametro += document.getElementById('Documento').value + "|";
        strParametro += "X";

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "AuditoriaDocumentoProceso.aspx/GrabarDocumentosProcesos",
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

function NombreArchivo() {
    var fileUpload = document.getElementById('Archivo');
    document.getElementById('Documento').value = fileUpload.files.item(0).name;
}

function cierraMessagePopUp() {
    $('#popupMessage').modal('hide');
}

function InicializaVista() {
    document.getElementById('Codigo').value = "0";
    document.getElementById('Documento').value = "";
    document.getElementById('Observaciones').value = "";
}