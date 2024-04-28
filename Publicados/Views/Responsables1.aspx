<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Responsables1.aspx.cs" Inherits="WebAuditorias.Views.Responsables1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Mantenimiento de responsables de auditorias</title>
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;500&display=swap" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Styles/Custom-Opciones.css" />
    <link rel="stylesheet" href="../Styles/Custom-Toolbar.css" />
    <link rel="stylesheet" href="../Styles/material.css" />
    <link rel="stylesheet" href="../Styles/Custom-Grid.css" />
    <link rel="stylesheet" href="../Styles/font-awesome.min.css" />
    <script src="../Scripts/ej2.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
</head>
<body style="margin: 0; height: 100%; overflow: hidden; background-color: #E5E8E8;">
        <div class="container-fluid">
            <div class="row header-bar-height" style="background-color: #E5E8E8;">
                <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 my-auto">
                    <img class="image-logo-empresa" src="../Images/LogoRomeroyAsociados.png" />
                </div>    
                <div class="col-sm-12 col-md-10 col-lg-10 col-xl-10 center-block text-left my-auto">
                    <p class="barra-titulo" style="color:#2C3E50;">Catalogo de tipos de tareas relacionadas a auditorias</p>
                </div>  
            </div>
            <div class="container">
                <div id="ContenedorForm" class="col-sm-12 col-md-2 col-lg-2 col-xl-2 py-4 h-100 opcion-backcolor-1" style="background-color: #B2BABB;">
                        <div class="l-navbar" id="nav-bar">
                            <nav class="nav">
                                <div> <a href="#" class="nav_logo"> <i class='bx bx-layer nav_logo-icon'></i> <span class="nav_logo-name">BBBootstrap</span> </a>
                                    <div class="nav_list"> <a href="#" class="nav_link active"> <i class='bx bx-grid-alt nav_icon'></i> <span class="nav_name">Dashboard</span> </a> <a href="#" class="nav_link"> <i class='bx bx-user nav_icon'></i> <span class="nav_name">Users</span> </a> <a href="#" class="nav_link"> <i class='bx bx-message-square-detail nav_icon'></i> <span class="nav_name">Messages</span> </a> <a href="#" class="nav_link"> <i class='bx bx-bookmark nav_icon'></i> <span class="nav_name">Bookmark</span> </a> <a href="#" class="nav_link"> <i class='bx bx-folder nav_icon'></i> <span class="nav_name">Files</span> </a> <a href="#" class="nav_link"> <i class='bx bx-bar-chart-alt-2 nav_icon'></i> <span class="nav_name">Stats</span> </a> </div>
                                </div> <a href="#" class="nav_link"> <i class='bx bx-log-out nav_icon'></i> <span class="nav_name">SignOut</span> </a>
                            </nav>
                        </div>
                    </div>
                <div id="Contenedor" class="col-sm-12 col-md-10 col-lg-10 col-xl-10 px-0 py-0 bg-white h-100 opcion-backcolor-2">
                    <div id="Formulario">
                        <form id="form1" runat="server">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <nav class="navbar navbar-expand-lg">
                                    <button class="btn btn-outline-dark navbar-btn boton-nuevo boton-margen" id="BtnNuevo" runat="server" onserverclick="BtnNuevo_ServerClick"></button>
                                    <button class="btn btn-outline-dark navbar-btn boton-buscar boton-margen" id="BtnBuscar" runat="server" onserverclick="BtnBuscar_ServerClick"></button>
                                    <button class="btn btn-outline-dark navbar-btn boton-grabar boton-margen" id="BtnGrabar" runat="server" onserverclick="BtnGrabar_ServerClick"></button>
                                </nav>
                                <div id="DivOpciones" class="mx-2" runat="server">
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label for="Codigo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Codigo</label>
                                            <input type="text" class="form-control form-control-sm" id="Codigo" placeholder="0" readonly="true" runat="server"/>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="Nombre" class="col-form-label col-form-label-sm" style="font-weight:bold;">Nombre del responsable</label>
                                            <input type="text" class="form-control form-control-sm" id="Nombre" placeholder="" runat="server"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label for="Cargo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Cargo</label>
                                            <input type="text" class="form-control form-control-sm" id="Cargo" placeholder="" runat="server"/>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="Oficina" class="col-form-label col-form-label-sm" style="font-weight:bold;">Oficina de origen</label>
                                            <asp:DropDownList ID="Oficina" CssClass="form-select form-select-sm" style="width: 300px" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label for="Tipo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Tipo</label>
                                            <asp:DropDownList ID="Tipo" CssClass="form-select form-select-sm" style="width: 300px" runat="server">
                                                <asp:ListItem Value="A">AUDITOR</asp:ListItem>
                                                <asp:ListItem Value="J">JEFE DE AUDITORIA</asp:ListItem>
                                                <asp:ListItem Value="R">RESPONSABLE DE INFORMACION</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="Correo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Correo electronico</label>
                                            <input type="email" class="form-control form-control-sm" id="Correo" placeholder="" runat="server"/>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="Usuario" class="col-form-label col-form-label-sm" style="font-weight:bold;">Usuario</label>
                                        <input type="text" class="form-control form-control-sm" id="Usuario" placeholder="" runat="server"/>
                                    </div>
                                    <div class="form-check col-md-6">
                                        <input class="form-check-input" type="checkbox" value="" id="chkEstado" runat="server"/>
                                        <label class="form-check-label form-control-sm" for="chkEstado">El responsable se encuentra activo</label>
                                    </div>
                                </div>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </form> 
                    </div>
                    <div id="GridConsulta" class="content-wrapper py-2" style="width:100%">
                        <div id="Grid">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="../Scripts/Responsables.js" type="text/javascript"></script>
</body>
</html>
