var windows = [];

function CallServerMethod(clicked_id, clicked_desc) {
    var strData;
    var args = '';
    args += "'name':'" + clicked_id + "', 'modulo':'" + clicked_desc + "'";
    var x = document.getElementById(clicked_id).innerText;
    document.cookie = 'modulosiep=' + clicked_desc + ';path =/';

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "Consola.aspx/GetValue",
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

    document.getElementById("DivOpciones").innerHTML = strData;
    return strData;
}

function EjecutaOpcion(codigo) {
    var descripcion = "";
    var pagina = "";

    if (codigo.includes("-")) {
        descripcion = document.getElementById('opt-' + codigo.split("-")[1]).innerText;
        pagina = document.getElementById('pag-' + codigo.split("-")[1]).innerText;
    }
    else {
        descripcion = document.getElementById('opt-' + codigo).innerText;
        pagina = document.getElementById('pag-' + codigo).innerText;
    }

    document.cookie = 'opcionsiep=' + descripcion + ';path =/';
    windows.push(window.open('../Views/' + pagina, pagina));
}

function setBackground(me, color) {
    me.setAttribute("data-oldback", me.style.backgroundColor);
    me.style.backgroundColor = color;
    me.style.fontWeight = "bold";
}

function restoreBackground(me) {
    me.style.backgroundColor = me.getAttribute("data-oldback");
    me.style.fontWeight = "normal";
}

function CloseTabWindow() {
    for (var i = 0; i < windows.length; i++) {
        windows[i].close()
    }
}