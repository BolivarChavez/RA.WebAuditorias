async function CallServerMethod() {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const proceso = document.getElementById('HiddenField1').value;

    strParametro = "1|";
    strParametro += auditoria[0] + "|";
    strParametro += proceso;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "AuditoriaTarea.aspx/ConsultaTareasAuditoria",
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
            { field: 'at_tarea', headerText: 'Codigo', visible: false, width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ct_descripcion', headerText: 'Tarea asignada', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }},
            { field: 'at_oficina', headerText: 'Oficina de asignacion', visible: false, width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }},
            { field: 'at_asignacion', headerText: 'Descripcion', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }},
            { field: 'at_estado', headerText: 'Estado de tarea', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }}
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

function rowSelected(args) {
    document.getElementById('Codigo').value = args.data.at_tarea;
    document.getElementById('Asignacion').value = args.data.at_asignacion;

    var dropdownlistbox1 = document.getElementById("Oficina")

    for (var i = 0; i <= dropdownlistbox1.length - 1; i++) {
        if (args.data.at_oficina == dropdownlistbox1.options[i].value)
            dropdownlistbox1.selectedIndex = i;
    }

    var dropdownlistbox2 = document.getElementById("Tarea")

    for (var i = 0; i <= dropdownlistbox2.length - 1; i++) {
        if (args.data.at_tarea == dropdownlistbox2.options[i].value) {
            dropdownlistbox2.selectedIndex = i;
            dropdownlistbox2.disabled = true;
        }
    }

    var dropdownlistbox3 = document.getElementById("Estado")

    for (var i = 0; i <= dropdownlistbox3.length - 1; i++) {
        if (args.data.at_estado == dropdownlistbox3.options[i].value)
            dropdownlistbox3.selectedIndex = i;
    }
}

function ValidaDatos() {
    var asignacion = document.getElementById('Asignacion').value;

    if (asignacion.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado la asignacion relacionada a la tarea de auditoria";
        $('#popupMessage').modal('show');
        return false;
    }

    return true;
}

function GrabarProceso() {
    var strData;
    var strParametro;
    var codigoTarea;

    if (!ValidaDatos()) {
        return strData;
    }

    if (confirm("Confirma la grabacion del registro de tarea relacionada a auditoría?")) {
        const auditoria = document.getElementById('Auditoria').value.split('-');

        if (document.getElementById('Codigo').value === "0") {
            codigoTarea = document.getElementById('Tarea').value;
        }
        else {
            codigoTarea = document.getElementById('Codigo').value;
        }

        strParametro = "1|";
        strParametro += auditoria[0] + "|";
        strParametro += codigoTarea + "|";
        strParametro += document.getElementById('Oficina').value + "|";
        strParametro += document.getElementById('Asignacion').value + "|";
        strParametro += document.getElementById('Estado').value + "|";
        strParametro += document.getElementById('Codigo').value;

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "AuditoriaTarea.aspx/GrabarAuditoriaTarea",
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
    var codigoTarea;

    if (confirm("Confirma la eliminación del registro de tarea relacionada a auditoría?")) {
        const auditoria = document.getElementById('Auditoria').value.split('-');

        if (document.getElementById('Codigo').value === "0") {
            codigoTarea = document.getElementById('Tarea').value;
        }
        else {
            codigoTarea = document.getElementById('Codigo').value;
        }

        strParametro = "1|";
        strParametro += auditoria[0] + "|";
        strParametro += codigoTarea + "|";
        strParametro += document.getElementById('Oficina').value + "|";
        strParametro += document.getElementById('Asignacion').value + "|";
        strParametro += 'X' + "|";
        strParametro += codigoTarea;

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "AuditoriaTarea.aspx/GrabarAuditoriaTarea",
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

function CerrarProceso() {
    var strData;
    var strParametro;
    var codigoTarea;

    if (confirm("Confirma el cierre del registro de tarea relacionada a auditoría?")) {
        const auditoria = document.getElementById('Auditoria').value.split('-');

        if (document.getElementById('Codigo').value === "0") {
            codigoTarea = document.getElementById('Tarea').value;
        }
        else {
            codigoTarea = document.getElementById('Codigo').value;
        }

        strParametro = "1|";
        strParametro += auditoria[0] + "|";
        strParametro += codigoTarea + "|";
        strParametro += document.getElementById('Oficina').value + "|";
        strParametro += document.getElementById('Asignacion').value + "|";
        strParametro += 'C' + "|";
        strParametro += codigoTarea;

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "AuditoriaTarea.aspx/GrabarAuditoriaTarea",
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

function CargaPlantilla() {
    const auditoriaCodigo = document.getElementById('Auditoria').value.split('-');

    var auditoria = auditoriaCodigo[0] + '|';
    auditoria += document.getElementById('Auditoria').value.substring(document.getElementById('Auditoria').value.indexOf('-') + 1) + '|';
    auditoria += document.getElementById("Tarea").value + '|' + document.getElementById("Tarea").options[document.getElementById("Tarea").selectedIndex].text + '|';
    auditoria += document.getElementById("cboPlantillas").value + '|' + document.getElementById("cboPlantillas").options[document.getElementById("cboPlantillas").selectedIndex].text;

    var vistaPlantilla = '';

    switch (document.getElementById("cboPlantillas").value) {
        case '1':
            vistaPlantilla = 'PlantillaCheques.aspx';
            break;

        case '2':
            vistaPlantilla = 'PlantillaComisiones.aspx';
            break;

        case '3':
            vistaPlantilla = 'PlantillaIngresos.aspx';
            break;

        case '4':
            vistaPlantilla = 'PlantillaMutuos.aspx';
            break;

        case '5':
            vistaPlantilla = 'PlantillaPagos.aspx';
            break;

        case '6':
            vistaPlantilla = 'PlantillaPlanillas.aspx';
            break;

        case '7':
            vistaPlantilla = 'PlantillaReembolsos.aspx';
            break;

        case '8':
            vistaPlantilla = 'PlantillaRegalias.aspx';
            break;

        case '9':
            vistaPlantilla = 'PlantillaRegularizaciones.aspx';
            break;

        case '10':
            vistaPlantilla = 'PlantillaTransferencias.aspx';
            break;

        case '11':
            vistaPlantilla = 'PlantillaTributos.aspx';
            break;
    }

    window.open(vistaPlantilla + '?plantilla=' + auditoria, '_blank');
}

function CargaActividades() {
    var estado = document.getElementById('Estado').value;

    if (estado === "P") {
        var auditoria = document.getElementById('Auditoria').value + '|';
        auditoria += document.getElementById('Tarea').value + '-' + document.getElementById("Tarea").options[document.getElementById("Tarea").selectedIndex].text;

        window.open('AuditoriaTareaProceso.aspx?auditoria=' + auditoria, '_blank');
    }
    else {
        document.getElementById('messageContent').innerHTML = "ERROR : Solo se permite agregar actividades a tareas que esten EN PROCESO";
        $('#popupMessage').modal('show');
        return;
    }
}

function CargaAsignaciones() {
    var estado = document.getElementById('Estado').value;

    if (estado === "P") {
        var auditoria = document.getElementById('Auditoria').value + '|';
        auditoria += document.getElementById('Tarea').value + '-' + document.getElementById("Tarea").options[document.getElementById("Tarea").selectedIndex].text;

        window.open('AuditoriaTareaAsignacion.aspx?auditoria=' + auditoria, '_blank');
    }
    else {
        document.getElementById('messageContent').innerHTML = "ERROR : Solo se permite agregar actividades a tareas que esten EN PROCESO";
        $('#popupMessage').modal('show');
        return;
    }
}

function cierraMessagePopUp() {
    $('#popupMessage').modal('hide');
}

function InicializaVista() {
    document.getElementById('Codigo').value = "0";
    document.getElementById('Asignacion').value = "";

    var dropdownlistbox = document.getElementById("Tarea");
    dropdownlistbox.disabled = false;
}

