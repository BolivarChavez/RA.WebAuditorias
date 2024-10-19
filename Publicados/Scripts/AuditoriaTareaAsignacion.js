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
        url: "AuditoriaTareaAsignacion.aspx/ConsultaTareaAsignacion",
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
            { field: 'aa_secuencia', headerText: 'Secuencia de asignacion', width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'aa_responsable', headerText: 'Codigo auditor', width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 're_nombre', headerText: 'Responsable', width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }},
            { field: 'aa_tipo', headerText: 'Tipo de asignacion', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 'aa_rol', headerText: 'Rol de asignacion', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 'aa_estado', headerText: 'Estado', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

function rowSelected(args) {
    document.getElementById('Codigo').value = args.data.aa_secuencia;

    var dropdownlistbox1 = document.getElementById("Responsable")

    for (var i = 0; i <= dropdownlistbox1.length - 1; i++) {
        if (args.data.aa_responsable == dropdownlistbox1.options[i].value)
            dropdownlistbox1.selectedIndex = i;
    }

    var dropdownlistbox2 = document.getElementById("TipoAsignacion")

    for (var i = 0; i <= dropdownlistbox2.length - 1; i++) {
        if (args.data.aa_tipo == dropdownlistbox2.options[i].value)
            dropdownlistbox2.selectedIndex = i;
    }

    var dropdownlistbox3 = document.getElementById("RolAsignacion")

    for (var i = 0; i <= dropdownlistbox3.length - 1; i++) {
        if (args.data.aa_rol == dropdownlistbox3.options[i].value)
            dropdownlistbox3.selectedIndex = i;
    }

    document.getElementById("chkEstado").checked = (args.data.aa_estado === 'A') ? true : false;
}

function GrabarProceso() {
    var strData;
    var strParametro;

    Swal.fire({
        title: "Asignaciones a tareas de auditoría",
        text: "Confirma la grabación del registro de asignación de tarea de auditoría?",
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

            strParametro = "1|";
            strParametro += auditoria[0] + "|";
            strParametro += tarea[0] + "|";
            strParametro += document.getElementById('Codigo').value + "|";
            strParametro += document.getElementById('Responsable').value + "|";
            strParametro += document.getElementById('TipoAsignacion').value + "|";
            strParametro += document.getElementById('RolAsignacion').value + "|";

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
                url: "AuditoriaTareaAsignacion.aspx/GrabarAuditoriaAsignacion",
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
        title: "Asignaciones a tareas de auditoría",
        text: "Confirma la grabación del registro de asignación de tarea de auditoría?",
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

            strParametro = "1|";
            strParametro += auditoria[0] + "|";
            strParametro += tarea[0] + "|";
            strParametro += document.getElementById('Codigo').value + "|";
            strParametro += document.getElementById('Responsable').value + "|";
            strParametro += document.getElementById('TipoAsignacion').value + "|";
            strParametro += document.getElementById('RolAsignacion').value + "|";
            strParametro += "X";

            var args = '';
            args += "'parametros':'" + strParametro + "'";
            $.ajax({
                async: false,
                cache: false,
                type: "POST",
                url: "AuditoriaTareaAsignacion.aspx/GrabarAuditoriaAsignacion",
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

function cierraMessagePopUp() {
    $('#popupMessage').modal('hide');
}

function InicializaVista() {
    document.getElementById('Codigo').value = "0";
    document.getElementById("chkEstado").checked = false;
}

function retornoProceso(dataProceso, mensaje) {
    var retornoProceso = JSON.parse(dataProceso)

    if (retornoProceso[0]['retorno'] === 0) {
        InicializaVista();

        Swal.fire({
            title: "Asignaciones a tareas de auditoría",
            text: mensaje,
            icon: "success",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        });
    }
    else {
        Swal.fire({
            title: "Asignaciones a tareas de auditoría",
            text: retornoProceso[0]['mensaje'],
            icon: "error",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        });
    }

    LlenaGrid();
}