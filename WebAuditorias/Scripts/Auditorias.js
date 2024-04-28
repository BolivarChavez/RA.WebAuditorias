async function CallServerMethod() {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "Auditorias.aspx/ConsultaAuditorias",
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
            { field: 'au_codigo', headerText: 'Codigo', width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'au_oficina_origen', headerText: 'Oficina origen', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 'au_oficina_destino', headerText: 'Oficina destino', width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 'au_tipo_proceso', headerText: 'Tipo de proceso', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 'au_fecha_inicio', headerText: 'Fecha de inicio', type: 'date', width: 150, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'au_fecha_cierre', headerText: 'Fecha de cierre', type: 'date', width: 100, format: { type: 'date', format: 'dd/MM/yyyy' }, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'au_tipo', headerText: 'Tipo', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 'au_observaciones', headerText: 'Observaciones', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'au_estado', headerText: 'Estado', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

function rowSelected(args) {
    var date1 = new Date(args.data.au_fecha_inicio);
    var day1 = ('0' + date1.getUTCDate()).slice(-2);
    var month1 = ('0' + (date1.getMonth() + 1)).slice(-2);
    var year1 = date1.getUTCFullYear();
    var fecha1 = `${year1}-${month1}-${day1}`;

    var date2 = new Date(args.data.au_fecha_cierre);
    var day2 = ('0' + date2.getUTCDate()).slice(-2);
    var month2 = ('0' + (date2.getMonth() + 1)).slice(-2);
    var year2 = date2.getUTCFullYear();
    var fecha2 = `${year2}-${month2}-${day2}`;

    document.getElementById('Codigo').value = args.data.au_codigo;
    document.getElementById('FechaInicio').value = fecha1;
    document.getElementById('FechaCierre').value = fecha2;
    document.getElementById('Observaciones').value = args.data.au_observaciones;

    var dropdownlistbox1 = document.getElementById("OficinaOrigen")

    for (var i = 0; i <= dropdownlistbox1.length - 1; i++) {
        if (args.data.au_oficina_origen == dropdownlistbox1.options[i].value)
            dropdownlistbox1.selectedIndex = i;
    }

    var dropdownlistbox2 = document.getElementById("OficinaDestino")

    for (var i = 0; i <= dropdownlistbox2.length - 1; i++) {
        if (args.data.au_oficina_destino == dropdownlistbox2.options[i].value)
            dropdownlistbox2.selectedIndex = i;
    }

    var dropdownlistbox3 = document.getElementById("TipoProceso")

    for (var i = 0; i <= dropdownlistbox3.length - 1; i++) {
        if (args.data.au_tipo_proceso == dropdownlistbox3.options[i].value)
            dropdownlistbox3.selectedIndex = i;
    }

    var dropdownlistbox4 = document.getElementById("Tipo")

    for (var i = 0; i <= dropdownlistbox4.length - 1; i++) {
        if (args.data.au_tipo == dropdownlistbox4.options[i].value)
            dropdownlistbox4.selectedIndex = i;
    }

    var dropdownlistbox5 = document.getElementById("Estado")

    for (var i = 0; i <= dropdownlistbox5.length - 1; i++) {
        if (args.data.au_estado == dropdownlistbox5.options[i].value)
            dropdownlistbox5.selectedIndex = i;
    }
}

function GrabarProceso() {
    var strData;
    var strParametro;

    if (confirm("Confirma la grabacion del registro de auditoria?")) {
        var date1 = new Date(document.getElementById('FechaInicio').value + 'T00:00:00.000Z');
        var day1 = date1.getUTCDate();
        var month1 = date1.getUTCMonth() + 1;
        var year1 = date1.getUTCFullYear();
        var fecha1 = year1 + "-" + month1 + "-" + day1;

        var date2 = new Date(document.getElementById('FechaCierre').value + 'T00:00:00.000Z');
        var day2 = date2.getUTCDate();
        var month2 = date2.getUTCMonth() + 1;
        var year2 = date2.getUTCFullYear();
        var fecha2 = year2 + "-" + month2 + "-" + day2;

        strParametro = "1|";
        strParametro += document.getElementById('Codigo').value + "|";
        strParametro += document.getElementById('OficinaOrigen').value + "|";
        strParametro += document.getElementById('OficinaDestino').value + "|";
        strParametro += document.getElementById('TipoProceso').value + "|";
        strParametro += fecha1 + "|";
        strParametro += fecha2 + "|";
        strParametro += document.getElementById('Tipo').value + "|";
        strParametro += document.getElementById('Observaciones').value + "|";
        strParametro += document.getElementById('Estado').value;

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "Auditorias.aspx/GrabarAuditoria",
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

    LlenaGrid();
    return strData;
}

function CargaPlantilla() {
    var auditoria = document.getElementById('Codigo').value + '|';
    auditoria += document.getElementById('Observaciones').value + '|';
    auditoria += document.getElementById("cboTareas").value + '|' + document.getElementById("cboTareas").options[document.getElementById("cboTareas").selectedIndex].text + '|';
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
