async function CallServerMethod() {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "Responsables.aspx/ConsultaResponsables",
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
            { field: 're_codigo', headerText: 'Codigo', visible: false, width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 're_nombre', headerText: 'Descripcion', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 're_cargo', headerText: 'Cargo', width: 50, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 're_oficina', headerText: 'Oficina', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 're_tipo', headerText: 'Tipo', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 're_correo', headerText: 'Correo', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 're_usuario', headerText: 'Usuario', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false },
            { field: 're_estado', headerText: 'Estado', width: 100, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' }, visible: false }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

function rowSelected(args) {
    document.getElementById('Codigo').value = args.data.re_codigo;
    document.getElementById('Nombre').value = args.data.re_nombre;
    document.getElementById('Cargo').value = args.data.re_cargo;
    document.getElementById('Correo').value = args.data.re_correo;
    document.getElementById('Usuario').value = args.data.re_usuario;
    document.getElementById("chkEstado").checked = (args.data.re_estado === 'A') ? true : false;

    var dropdownlistbox = document.getElementById("Oficina")

    for (var i = 0; i < dropdownlistbox.length - 1; i++) {
        if (args.data.re_oficina == dropdownlistbox.options[i].value)
            dropdownlistbox.selectedIndex = i;
    }

    var dropdownlistbox1 = document.getElementById("Tipo")

    for (var i = 0; i < dropdownlistbox1.length - 1; i++) {
        if (args.data.re_tipo == dropdownlistbox1.options[i].value)
            dropdownlistbox1.selectedIndex = i;
    }

    document.getElementById('HiddenField1').value = "U";
}

function ValidaDatos() {
    var nombre;
    var cargo;
    var correo;

    nombre = document.getElementById('Nombre').value;
    cargo = document.getElementById('Cargo').value;
    correo = document.getElementById('Correo').value;

    if (nombre.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado nombre para el responsable";
        $('#popupMessage').modal('show');
        return false;
    }

    if (cargo.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado el cargo del responsable";
        $('#popupMessage').modal('show');
        return false;
    }

    if (correo.trim() !== "") {
        if (!validaEmail(correo)) {
            document.getElementById('messageContent').innerHTML = "ERROR : El correo electronico no es válido";
            $('#popupMessage').modal('show');
            return false;
        }
    }

    return true;
}

function validaEmail(email) {
    return email.match(
        /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    );
}

function GrabarResponsables() {
    var strData;
    var strParametro;

    if (!ValidaDatos()) {
        return strData;
    }

    if (confirm("Confirma la grabación del registro del responsable?")) {
        strParametro = "1|";
        strParametro += document.getElementById('Codigo').value + "|";
        strParametro += document.getElementById('Nombre').value + "|";
        strParametro += document.getElementById('Cargo').value + "|";
        strParametro += document.getElementById('Oficina').value + "|";
        strParametro += document.getElementById('Tipo').value + "|";
        strParametro += document.getElementById('Correo').value + "|";
        strParametro += document.getElementById('Usuario').value + "|";

        if (document.getElementById("chkEstado").checked) {
            strParametro += "A|";
        }
        else {
            strParametro += "I|";
        }

        if (document.getElementById('Codigo').value === "0") {
            strParametro += "I";
        }
        else {
            strParametro += "U";
        }


        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "Responsables.aspx/GrabarResponsables",
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

function cierraMessagePopUp() {
    $('#popupMessage').modal('hide');
}

function InicializaVista() {
    document.getElementById('Codigo').value = "0";
    document.getElementById('Nombre').value = "";
    document.getElementById('Cargo').value = "";
    document.getElementById('Correo').value = "";
    document.getElementById('Usuario').value = "";
    document.getElementById('HiddenField1').value = "I";
    document.getElementById("chkEstado").checked = false;
}