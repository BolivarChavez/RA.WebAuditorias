﻿async function CallServerMethod() {
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
            { field: 'au_codigo', headerText: 'Codigo', visible: false, width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
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

function ValidaDatos() {
    var observacion;
    var fechaInicio = new Date(document.getElementById('FechaInicio').value + 'T00:00:00.000Z');
    var fechaCierre = new Date(document.getElementById('FechaCierre').value + 'T00:00:00.000Z');
    var fecha1 = document.getElementById('FechaInicio').value;
    var fecha2 = document.getElementById('FechaCierre').value;
    var estado = document.getElementById('Estado').value;

    observacion = document.getElementById('Observaciones').value;

    if (estado != "A") {
        Swal.fire({
            title: "Registro de auditorías",
            text: "Solo pueden actualizarce auditorias que esten en estado ACTIVO",
            icon: "error",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        });

        return false;
    }

    if (observacion.trim() === "") {
        Swal.fire({
            title: "Registro de auditorías",
            text: "No se ha ingresado la observacion o referencia para la auditoria",
            icon: "error",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        });

        return false;
    }

    if (!fecha1) {
        Swal.fire({
            title: "Registro de auditorías",
            text: "Fecha de inicio de auditoria no valida",
            icon: "error",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        });

        return false;
    }

    if (!fecha2) {
        Swal.fire({
            title: "Registro de auditorías",
            text: "Fecha de cierre de auditoria no valida",
            icon: "error",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        });

        return false;
    }

    if (fechaCierre <= fechaInicio) {
        Swal.fire({
            title: "Registro de auditorías",
            text: "La fecha de cierre de la auditoria no debe ser anterior o igual a la de inicio",
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
        title: "Registro de auditorías",
        text: "Confirma la grabación del registro de auditoria?",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
    }).then((result) => {
        if (result.isConfirmed) {
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
        title: "Registro de auditorías",
        text: "Confirma la eliminación del registro de auditoría?",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
    }).then((result) => {
        if (result.isConfirmed) {
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
            strParametro += "X";

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

            retornoProceso(strData, 'La eliminación del registro ha finalizado');
        }
        else {
            strData = "";
        }
    });

    return strData;
}

function IniciarProceso() {
    var strData;
    var strParametro;

    Swal.fire({
        title: "Registro de auditorías",
        text: "Confirma colocar EN PROCESO el registro de auditoria?",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
    }).then((result) => {
        if (result.isConfirmed) {
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
            strParametro += "P";

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

            retornoProceso(strData, 'El proceso de inicio (colocar estado EN PROCESO) ha finalizado');
        }
        else {
            strData = "";
        }
    });

    return strData;
}

function CerrarProceso() {
    var strData;
    var strParametro;

    Swal.fire({
        title: "Registro de auditorías",
        text: "Confirma el cierre del registro de auditoria?",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
    }).then((result) => {
        if (result.isConfirmed) {
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
            strParametro += "C";

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

            retornoProceso(strData, 'El cierre del registro ha finalizado');
        }
        else {
            strData = "";
        }
    });

    return strData;
}

function ValidaDatosCopia() {
    var codigo = document.getElementById('Codigo').value;

    if (codigo.trim() === "0") {
        Swal.fire({
            title: "Registro de auditorías",
            text: "Debe seleccionar un registro de auditoría para poder realizar la copia",
            icon: "error",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        });

        return false;
    }

    return true;
}

function CopiarProceso() {
    var strData;
    var strParametro;

    if (!ValidaDatosCopia()) {
        return strData;
    }

    Swal.fire({
        title: "Registro de auditorías",
        text: "Confirma la creación de la copia del registro de auditoria?",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
    }).then((result) => {
        if (result.isConfirmed) {
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
            strParametro += "A";

            var args = '';
            args += "'parametros':'" + strParametro + "'";
            $.ajax({
                async: false,
                cache: false,
                type: "POST",
                url: "Auditorias.aspx/CopiarAuditoria",
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

function CargaTareas() {
    var estado = document.getElementById('Estado').value;

    if (estado === "P" || estado === "A") {
        var auditoria = document.getElementById('Codigo').value + '|';
        auditoria += document.getElementById('Observaciones').value + '|';
        auditoria += document.getElementById('TipoProceso').value + '|' + document.getElementById("TipoProceso").options[document.getElementById("TipoProceso").selectedIndex].text;

        window.open('AuditoriaTarea.aspx?auditoria=' + auditoria, '_blank');
    }
    else {
        document.getElementById('messageContent').innerHTML = "ERROR : Solo se permite agregar tareas a auditorias que esten EN PROCESO";
        $('#popupMessage').modal('show');
        return;
    }
}

function CargaGastos() {
    var estado = document.getElementById('Estado').value;

    if (estado === "P" || estado === "A") {
        var auditoria = document.getElementById('Codigo').value + '-' + document.getElementById("Observaciones").value;

        window.open('AuditoriaGasto.aspx?auditoria=' + auditoria, '_blank');
    }
    else {
        document.getElementById('messageContent').innerHTML = "ERROR : Solo se permite agregar gastos a auditorias que esten EN PROCESO";
        $('#popupMessage').modal('show');
        return;
    }
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

function cierraMessagePopUp() {
    $('#popupMessage').modal('hide');
}

function InicializaVista() {
    document.getElementById('Codigo').value = "0";
    document.getElementById('Observaciones').value = "";

    var dropdownlistbox = document.getElementById("Estado")
    dropdownlistbox.selectedIndex = 0;
}

function retornoProceso(dataProceso, mensaje) {
    var retornoProceso = JSON.parse(dataProceso)

    if (retornoProceso[0]['retorno'] === 0) {
        InicializaVista();

        Swal.fire({
            title: "Registro de auditorías",
            text: mensaje,
            icon: "success",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        });
    }
    else {
        Swal.fire({
            title: "Registro de auditorías",
            text: retornoProceso[0]['mensaje'],
            icon: "error",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        });
    }

    LlenaGrid();
}