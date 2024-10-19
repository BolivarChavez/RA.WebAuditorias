async function CallServerMethod() {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const auditoria = document.getElementById('Auditoria').value.split('-');
    const tarea = document.getElementById('Tarea').value.split('-');

    strParametro = "1|";
    strParametro += auditoria[0] + "|";
    strParametro += tarea[0];

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "AuditoriaTareaProceso.aspx/ConsultaTareasProcesos",
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
            { field: 'at_secuencia', headerText: 'Secuencia de actividad', visible: false, width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'at_fecha', headerText: 'Fecha de actividad', type: 'date', width: 150, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }},
            { field: 'at_auditor', headerText: 'Codigo auditor', width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 'at_responsable', headerText: 'Codigo responsable', width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 're_nombre', headerText: 'Auditor', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }},
            { field: 'responsableNombre', headerText: 'Responsable', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }},
            { field: 'at_observaciones', headerText: 'Observaciones', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'at_documento', headerText: 'Documento Relacionado', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'at_estado', headerText: 'Estado', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }}
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

function rowSelected(args) {
    var date1 = new Date(args.data.at_fecha);
    var day1 = ('0' + date1.getUTCDate()).slice(-2);
    var month1 = ('0' + (date1.getMonth() + 1)).slice(-2);
    var year1 = date1.getUTCFullYear();
    var fecha1 = `${year1}-${month1}-${day1}`;

    document.getElementById('Codigo').value = args.data.at_secuencia;
    document.getElementById('Fecha').value = fecha1;
    document.getElementById('Observaciones').value = args.data.at_observaciones;
    document.getElementById('Documento').value = args.data.at_documento;

    var dropdownlistbox1 = document.getElementById("Auditor")

    for (var i = 0; i <= dropdownlistbox1.length - 1; i++) {
        if (args.data.at_auditor == dropdownlistbox1.options[i].value)
            dropdownlistbox1.selectedIndex = i;
    }

    var dropdownlistbox2 = document.getElementById("Responsable")

    for (var i = 0; i <= dropdownlistbox2.length - 1; i++) {
        if (args.data.at_responsable == dropdownlistbox2.options[i].value)
            dropdownlistbox2.selectedIndex = i;
    }

    document.getElementById("chkEstado").checked = (args.data.at_estado === 'A') ? true : false;
}

function ValidaDatos() {
    var observaciones = document.getElementById('Observaciones').value;

    if (observaciones.trim() === "") {
        Swal.fire({
            title: "Actvidades relacionadas a tareas de auditoría",
            text: "No se ha ingresado la observacion relacionada a la actividad",
            icon: "error",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        });

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

    Swal.fire({
        title: "Actvidades relacionadas a tareas de auditoría",
        text: "Confirma la grabación del registro de actividad de tarea de auditoría?",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
    }).then((result) => {
        if (result.isConfirmed) {
            const auditoria = document.getElementById('Auditoria').value.split('-');
            const tarea = document.getElementById('Tarea').value.split('-');

            var date1 = new Date(document.getElementById('Fecha').value + 'T00:00:00.000Z');
            var day1 = date1.getUTCDate();
            var month1 = date1.getUTCMonth() + 1;
            var year1 = date1.getUTCFullYear();
            var fecha1 = year1 + "-" + month1 + "-" + day1;

            strParametro = "1|";
            strParametro += auditoria[0] + "|";
            strParametro += tarea[0] + "|";
            strParametro += document.getElementById('Codigo').value + "|";
            strParametro += document.getElementById('Auditor').value + "|";
            strParametro += document.getElementById('Responsable').value + "|";
            strParametro += fecha1 + "|";
            strParametro += document.getElementById('Observaciones').value + "|";
            strParametro += document.getElementById('Documento').value + "|";

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
                url: "AuditoriaTareaProceso.aspx/GrabarAuditoriaTareasProcesos",
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

            retornoProceso(strData, 'La grabación del registro ha finalizado');
        }
        else {
            strData = "";
        }
    });

    return strData;
}

function EliminarProceso() {
    var strData;
    var strParametro;

    Swal.fire({
        title: "Actvidades relacionadas a tareas de auditoría",
        text: "Confirma la eliminación del registro de actividad de tarea de auditoría?",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
    }).then((result) => {
        if (result.isConfirmed) {
            const auditoria = document.getElementById('Auditoria').value.split('-');
            const tarea = document.getElementById('Tarea').value.split('-');

            var date1 = new Date(document.getElementById('Fecha').value + 'T00:00:00.000Z');
            var day1 = date1.getUTCDate();
            var month1 = date1.getUTCMonth() + 1;
            var year1 = date1.getUTCFullYear();
            var fecha1 = year1 + "-" + month1 + "-" + day1;

            strParametro = "1|";
            strParametro += auditoria[0] + "|";
            strParametro += tarea[0] + "|";
            strParametro += document.getElementById('Codigo').value + "|";
            strParametro += document.getElementById('Auditor').value + "|";
            strParametro += document.getElementById('Responsable').value + "|";
            strParametro += fecha1 + "|";
            strParametro += document.getElementById('Observaciones').value + "|";
            strParametro += document.getElementById('Documento').value + "|";
            strParametro += "X";

            var args = '';
            args += "'parametros':'" + strParametro + "'";
            $.ajax({
                async: false,
                cache: false,
                type: "POST",
                url: "AuditoriaTareaProceso.aspx/GrabarAuditoriaTareasProcesos",
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

            retornoProceso(strData, 'La eliminación del registro ha finalizado');
        }
        else {
            strData = "";
        }
    });

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
    document.getElementById('Observaciones').value = "";
    document.getElementById('Documento').value = "";
    document.getElementById("chkEstado").checked = false;
}

function retornoProceso(dataProceso, mensaje) {
    var retornoProceso = JSON.parse(dataProceso)

    if (retornoProceso[0]['retorno'] === 0) {
        InicializaVista();

        Swal.fire({
            title: "Actvidades relacionadas a tareas de auditoría",
            text: mensaje,
            icon: "success",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        });
    }
    else {
        Swal.fire({
            title: "Actvidades relacionadas a tareas de auditoría",
            text: retornoProceso[0]['mensaje'],
            icon: "error",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        });
    }

    LlenaGrid();
}
