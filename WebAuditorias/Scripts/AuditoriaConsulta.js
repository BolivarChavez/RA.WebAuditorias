async function CallServerMethod() {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    const anio = document.getElementById('Anio').value;

    strParametro = anio;

    var args = '';
    args += "'anio':'" + strParametro + "'";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "AuditoriaConsulta.aspx/CargaPlantillasRegistradas",
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

async function LlenaGridPlantilla() {
    var dataGrid = await CallServerMethod();

    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = "300 px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        toolbar: ['Search'],
        allowPaging: false,
        allowScrolling: true,
        height: '100%',
        columns: [
            { field: 'IdPlantilla', headerText: 'Codigo', visible: false, width: 20, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'NombrePlantilla', headerText: 'Tipo de Plantilla', width: 300, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Registradas', headerText: 'Plantillas Registradas', width: 150, textAlign: 'Left', template: '#template1', customAttributes: { class: 'boldheadergrid' } },
            { field: 'Pendientes', headerText: 'Plantillas Pendientes', width: 150, textAlign: 'Left', template: '#template2', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        queryCellInfo: customiseCell
    });
    grid.appendTo('#Grid');
}

function customiseCell(args) {
    if (args.column.field === 'Registradas') {
        args.cell.style.color = "#0D6EFD";
        args.cell.style.fontWeight = "bold"; 
    }
    if (args.column.field === 'Pendientes') {
        args.cell.style.color = "#FFC107";
        args.cell.style.fontWeight = "bold"; 
    }
}