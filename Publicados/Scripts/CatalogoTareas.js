﻿async function CallServerMethod() {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "CatalogoTareas.aspx/ConsultaTareas",
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
            { field: 'ct_codigo', headerText: 'Codigo', visible: false, width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ct_proceso', headerText: 'Codigo Proceso', width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 'cp_descripcion', headerText: 'Proceso', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ct_descripcion', headerText: 'Descripcion', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'ct_estado', headerText: 'Estado', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

function rowSelected(args) {
    document.getElementById('Codigo').value = args.data.ct_codigo;
    document.getElementById('Descripcion').value = args.data.ct_descripcion;
    document.getElementById("chkEstado").checked = (args.data.ct_estado === 'A') ? true : false;

    var dropdownlistbox = document.getElementById("Proceso")

    for (var i = 0; i <= dropdownlistbox.length - 1; i++) {
        if (args.data.ct_proceso == dropdownlistbox.options[i].value)
            dropdownlistbox.selectedIndex = i;
    }
}

function ValidaDatos() {
    var descripcion;

    descripcion = document.getElementById('Descripcion').value;

    if (descripcion.trim() === "") {
        Swal.fire({
            title: "Catálogo de tareas relacionadas a auditorías",
            text: "No se ha ingresado una descripción para la tarea",
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
        title: "Catálogo de tareas relacionadas a auditorías",
        text: "Confirma la grabación del registro de proceso?",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
    }).then((result) => {
        if (result.isConfirmed) {
            strParametro = "1|";
            strParametro += document.getElementById('Proceso').value + "|";
            strParametro += document.getElementById('Codigo').value + "|";
            strParametro += document.getElementById('Descripcion').value + "|";

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
                url: "CatalogoTareas.aspx/GrabarTarea",
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
        title: "Catálogo de tareas relacionadas a auditorías",
        text: "Confirma la eliminación del registro de proceso?",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
    }).then((result) => {
        if (result.isConfirmed) {
            strParametro = "1|";
            strParametro += document.getElementById('Proceso').value + "|";
            strParametro += document.getElementById('Codigo').value + "|";
            strParametro += document.getElementById('Descripcion').value + "|";
            strParametro += "X";

            var args = '';
            args += "'parametros':'" + strParametro + "'";
            $.ajax({
                async: false,
                cache: false,
                type: "POST",
                url: "CatalogoTareas.aspx/EliminarTarea",
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
    document.getElementById('Descripcion').value = "";
    document.getElementById("chkEstado").checked = false;
}

function retornoProceso(dataProceso, mensaje) {
    var retornoProceso = JSON.parse(dataProceso)

    if (retornoProceso[0]['retorno'] === 0) {
        InicializaVista();

        Swal.fire({
            title: "Catálogo de tareas relacionadas a auditorías",
            text: mensaje,
            icon: "success",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        });
    }
    else {
        Swal.fire({
            title: "Catálogo de tareas relacionadas a auditorías",
            text: retornoProceso[0]['mensaje'],
            icon: "error",
            confirmButtonColor: "#3085d6",
            confirmButtonText: "Continuar"
        });
    }

    LlenaGrid();
}