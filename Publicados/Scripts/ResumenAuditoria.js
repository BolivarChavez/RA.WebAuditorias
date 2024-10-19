async function CallServerMethod() {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const anio = document.getElementById('Anio').value;

    strParametro = "1|" + anio;

    var args = '';
    args += "'parametros':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "ResumenAuditoria.aspx/ConsultaResumen",
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

    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = "600 px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        showColumnChooser: true,
        allowExcelExport: true,
        toolbar: ['ColumnChooser', 'ExcelExport'],
        allowPaging: false,
        allowScrolling: true,
        allowResizing: true,
        height: '100%',
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Content' },
        gridLines: 'Both',
        columns: [
            { field: 'Id', headerText: 'Id', visible: false, width: 20, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Proceso', headerText: 'Departamento', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Auditoria', headerText: 'Auditoria', width: 300, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'EstadoAuditoria', headerText: 'Estado Auditoria', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'OficinaOrigen', headerText: 'Oficina Origen', width: 200, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'OficinaDestino', headerText: 'Oficina Destino', width: 200, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'FechaInicio', headerText: 'Fecha de Inicio', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'FechaCierre', headerText: 'Fecha de Cierre', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Gastos', headerText: 'Total Gastos', width: 150, format: 'N2', textAlign: 'Right', customAttributes: { class: 'boldheadergrid' }, type: 'number' },
            { field: 'Tarea', headerText: 'Tarea', width: 300, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Asignacion', headerText: 'Asignacion', width: 300, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Responsables', headerText: 'Responsables', width: 300, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ProcesosActivos', headerText: 'Actividades Creadas', width: 150, textAlign: 'Right', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ProcesosCerrados', headerText: 'Actividades Procesadas', width: 150, textAlign: 'Right', customAttributes: { class: 'boldheadergrid' } },
            { field: 'PlantillasActivas', headerText: 'Plantillas Registradas', width: 150, textAlign: 'Right', customAttributes: { class: 'boldheadergrid' } },
            { field: 'PlantillasProcesadas', headerText: 'Plantillas Procesadas', width: 150, textAlign: 'Right', customAttributes: { class: 'boldheadergrid' } },
            { field: 'EstadoTarea', headerText: 'Estado Tarea', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 }
    });

    grid.toolbarClick = function (args) {
        if (args['item'].id === 'Grid_excelexport') {
            grid.excelExport();
        }
    }

    grid.appendTo('#Grid');
}