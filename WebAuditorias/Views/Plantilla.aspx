<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Plantilla.aspx.cs" Inherits="WebAuditorias.Views.Plantilla" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Plantilla de formularios web</title>
    <link href="https://fonts.googleapis.com/css2?family=IBM+Plex+Sans:wght@200;400&display=swap" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Styles/Custom-Opciones.css" />
    <link rel="stylesheet" href="../Styles/Custom-Toolbar.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row header-bar-height">
                <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 my-auto">
                    <img class="image-logo-empresa" src="../Images/LogoRomeroyAsociados.png" />
                </div>    
                <div class="col-sm-12 col-md-10 col-lg-10 col-xl-10 center-block text-left my-auto">
                    <p class="barra-titulo">Plantilla de ejecucion de opciones</p>
                </div>  
            </div>
            <div class="row console-menu-height">
                <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 py-4 h-100 opcion-backcolor-1">
                    <header class="avatar">
                        <asp:Label ID="label1" runat="server" >Estudio Juridico Romero y Asociados</asp:Label>
                        <asp:Label ID="label2" runat="server" >Sistema de Control de Auditorias</asp:Label>
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="labelUser" runat="server" >USUARIO</asp:Label>
                        <br />
                        <asp:Label ID="lblNombre" runat="server" >Nombre del colaborador</asp:Label>
                    </header>
                    <br />
                    <div id="DivMenu" runat="server" class="overflow-auto">
                    </div>
                </div>
                <div class="col-sm-12 col-md-10 col-lg-10 col-xl-10 px-0 py-0 bg-white h-100 opcion-backcolor-2">
                    <nav class="navbar navbar-expand-lg">
                        <button class="btn btn-outline-dark navbar-btn boton-nuevo boton-margen" id="BtnNuevo"></button>
                        <button class="btn btn-outline-dark navbar-btn boton-buscar boton-margen" id="BtnBuscar"></button>
                        <button class="btn btn-outline-dark navbar-btn boton-grabar boton-margen" id="BtnGrabar"></button>
                        <button class="btn btn-outline-dark navbar-btn boton-eliminar boton-margen" id="BtnEliminar"></button>
                    </nav>
                    <div id="DivOpciones" runat="server">
                    </div>    
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        function noBack() { window.history.forward() }
        noBack();
        window.onload = noBack;
        window.onpageshow = function (evt) { if (evt.persisted) noBack() }
        window.onunload = function () { void (0) }
    </script>
</body>
</html>
