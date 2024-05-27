<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Consola.aspx.cs" Inherits="WebAuditorias.Consola" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Consola de ejecucion de opciones</title>
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;500&display=swap" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Styles/Custom-Opciones.css" />
    <link rel="stylesheet" href="../Styles/Custom-Menu.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>

    <script type="text/javascript">
        function noBack() { window.history.forward() }
        noBack();
        window.onload = noBack;
        window.onpageshow = function (evt) { if (evt.persisted) noBack() }
        window.onunload = function () { void (0) }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row header-bar-height" style="background-color: #E5E8E8;">
                <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 my-auto">
                    <img class="image-logo-empresa" src="../Images/LogoRomeroyAsociados.png" />
                </div>    
                <div class="col-sm-12 col-md-6 col-lg-6 col-xl-6 center-block text-left my-auto">
                    <p class="barra-titulo" style="color:#2C3E50;">Sistema de Control de Auditorías - Consola Principal</p>
                </div>  
                <div id="DivBoton" class="col-sm-12 col-md-4 col-lg-4 col-xl-4 my-auto">
                    <div class="barra-boton">
                        <div class="boton-superior">
                            <button id="Salir" name="Salir" runat="server" class="botones" style="background-color: #B7BABA;" onserverclick="Salir_ServerClick" onclick="CloseTabWindow();"><img class="imagen-boton" src="../Images/PowerButton.png" /></button>
                        </div>    
                    </div>
                </div>
            </div>
            <div class="row console-menu-height" style="background-color: #E5E8E8;">
                <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 py-4 h-100 opcion-backcolor-1" style="background-color: #B7BABA;">
                    <header class="avatar" style="background-color: #B7BABA;">
                        <asp:Label ID="labelUser" runat="server" ForeColor="#2C3E50">USUARIO</asp:Label>
                        <br />
                        <asp:Label ID="lblNombre" runat="server" ForeColor="#2C3E50">Nombre del colaborador</asp:Label>
                    </header>
                    <br />
                    <div id="DivMenu" runat="server" class="overflow-auto">
                    </div>
                </div>
                <div class="col-sm-12 col-md-10 col-lg-10 col-xl-10 px-0 py-0 bg-white h-100 opcion-backcolor-2">
                    <div id="DivOpciones" class="opciones-menu" runat="server">
                    </div>    
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="../Scripts/Consola.js"></script>
</body>
</html>

