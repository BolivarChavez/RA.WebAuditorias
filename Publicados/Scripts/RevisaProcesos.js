async function CallAuditoriaGastos() {
    var strData;
    document.getElementById('GridGastos').innerHTML = "";

    const auditoria = document.getElementById('ProcesoAuditoria').value;

    strParametro = "1|";
    strParametro += auditoria;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "RevisaProcesos.aspx/ConsultaGastosAuditoria",
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

async function LlenaGridGastos() {
    var dataGrid = await CallAuditoriaGastos();

    var divGrid = document.getElementById("GridConsultaGastos");
    divGrid.style.height = "375px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        toolbar: ['Search'],
        allowPaging: false,
        allowScrolling: true,
        allowTextWrap: true,
        height: '250px',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        columns: [
            { field: 'ag_secuencia', headerText: 'Codigo', visible: false, width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ag_tipo', headerText: 'Tipo de Gasto', visible: false, width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'cg_descripion', headerText: 'Tipo de Gasto', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ag_fecha_inicio', headerText: 'Fecha de inicio de gasto', type: 'date', width: 150, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ag_fecha_fin', headerText: 'Fecha de fin de gasto', type: 'date', width: 150, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ag_valor', headerText: 'Total de Gasto', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        aggregates: [
            {
                columns: [
                    {
                        type: 'Sum',
                        field: 'ag_valor',
                        footerTemplate: 'Total de Gastos: ${Sum}',
                        format: 'C2',
                    }
                ]
            }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 }
    });
    grid.appendTo('#GridGastos');
}

async function CallAuditoriaTareas() {
    var strData;
    document.getElementById('GridTareas').innerHTML = "";

    const auditoria = document.getElementById('ProcesoAuditoria').value;
    const proceso = document.getElementById('HiddenField1').value;

    strParametro = "1|";
    strParametro += auditoria + "|";
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

async function LlenaGridTareas() {
    var dataGrid = await CallAuditoriaTareas();

    var divGrid = document.getElementById("GridConsultaTareas");
    divGrid.style.height = "375px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        toolbar: ['Search'],
        allowPaging: false,
        allowScrolling: true,
        allowTextWrap: true,
        height: '250px',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        columns: [
            { field: 'at_tarea', headerText: 'Codigo', visible: false, width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ct_descripcion', headerText: 'Tarea asignada', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'at_oficina', headerText: 'Oficina de asignacion', visible: false, width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'at_asignacion', headerText: 'Descripcion', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'at_estado', headerText: 'Estado de tarea', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: seleccionTarea
    });
    grid.appendTo('#GridTareas');
}

async function seleccionTarea(args) {
    const idTarea = args.data.at_tarea;

    document.getElementById("TituloSeccion4").innerHTML = "<b>Responsables asignados a tarea : " + args.data.ct_descripcion + "</b>";
    document.getElementById("TituloSeccion5").innerHTML = "<b>Procesos asociados a tarea : " + args.data.ct_descripcion + "</b>";

    await LlenaGridAsignacion(idTarea);
    await LlenaGridProceso(idTarea);
    document.getElementById("GridConsultaAsignacion").scrollIntoView();
}

async function ConsultaInicial() {
    document.getElementById("TituloSeccion4").innerHTML = "<b>Responsables asignados a tarea</b>";
    document.getElementById("TituloSeccion5").innerHTML = "<b>Procesos asociados a tarea</b>"; 

    document.getElementById('GridAsignacion').innerHTML = "";
    document.getElementById('GridProcesos').innerHTML = "";

    await LlenaGridGastos();
    await LlenaGridTareas();
}

async function CallAuditoriaAsignacion(idTarea) {
    var strData;
    document.getElementById('GridAsignacion').innerHTML = "";
``
    const auditoria = document.getElementById('ProcesoAuditoria').value;
    const tarea = idTarea;

    strParametro = "1|";
    strParametro += auditoria + "|";
    strParametro += tarea;

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

async function LlenaGridAsignacion(idTarea) {
    var dataGrid = await CallAuditoriaAsignacion(idTarea);

    var divGrid = document.getElementById("GridConsultaAsignacion");
    divGrid.style.height = "375px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        toolbar: ['Search'],
        allowPaging: false,
        allowScrolling: true,
        allowTextWrap: true,
        height: '250px',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        columns: [
            { field: 'aa_secuencia', headerText: 'Secuencia de asignacion', width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'aa_responsable', headerText: 'Codigo auditor', width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 're_nombre', headerText: 'Responsable', width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'aa_tipo', headerText: 'Tipo de asignacion', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 'aa_rol', headerText: 'Rol de asignacion', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 'aa_estado', headerText: 'Estado', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 }
    });
    grid.appendTo('#GridAsignacion');
}

async function CallAuditoriaProceso(idTarea) {
    var strData;
    document.getElementById('GridProcesos').innerHTML = "";

    const auditoria = document.getElementById('ProcesoAuditoria').value;
    const tarea = idTarea;

    strParametro = "1|";
    strParametro += auditoria + "|";
    strParametro += tarea;

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

async function LlenaGridProceso(idTarea) {
    var dataGrid = await CallAuditoriaProceso(idTarea);

    var divGrid = document.getElementById("GridConsultaProcesos");
    divGrid.style.height = "375px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        toolbar: ['Search'],
        allowPaging: false,
        allowScrolling: true,
        allowTextWrap: true,
        height: '250px',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        columns: [
            { field: 'at_secuencia', headerText: 'Secuencia de actividad', visible: false, width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'at_fecha', headerText: 'Fecha de actividad', type: 'date', width: 150, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'at_auditor', headerText: 'Codigo auditor', width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 'at_responsable', headerText: 'Codigo responsable', width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 're_nombre', headerText: 'Auditor', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'responsableNombre', headerText: 'Responsable', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'at_observaciones', headerText: 'Observaciones', width: 300, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'at_documento', headerText: 'Documento Relacionado', width: 150, textAlign: 'Left', template: '#template', customAttributes: { class: 'boldheadergrid' } },
            { field: 'at_estado', headerText: 'Estado', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: seleccionProceso
    });
    grid.appendTo('#GridProcesos');
}

function seleccionProceso(args) {
    document.getElementById("Documento").value = args.data.at_documento;
}

function CerrarAuditoria() {
    var strData;
    var strParametro;

    if (confirm("Confirma el cierre del proceso de auditoria?")) {
        strParametro = "1|";
        strParametro += document.getElementById('ProcesoAuditoria').value;

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "RevisaProcesos.aspx/CerrarAuditoria",
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
        document.getElementById('messageContent').innerHTML = "El cierre del proceso de auditoría ha finalizado";
        $('#popupMessage').modal('show');
    }
    else {
        document.getElementById('messageContent').innerHTML = "ERROR : " + retornoProceso[0]['mensaje'];
        $('#popupMessage').modal('show');
    }

    return strData;
}

function cierraMessagePopUp() {
    $('#popupMessage').modal('hide');
}