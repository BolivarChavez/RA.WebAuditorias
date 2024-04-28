<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Auditorias.aspx.cs" Inherits="WebAuditorias.Views.Auditorias" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Registro de auditorías</title>
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;500&display=swap" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Styles/Custom-Opciones.css" />
    <link rel="stylesheet" href="../Styles/Custom-Toolbar.css" />
    <link rel="stylesheet" href="../Styles/material.css" />
    <link rel="stylesheet" href="../Styles/Custom-Grid.css" />
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
                    <p class="barra-titulo" style="color:#2C3E50;">Registro de auditorías</p>
                </div>  
            </div>
            <div class="row console-menu-height" style="background-color: #B2BABB;">
                <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 py-4 h-100 opcion-backcolor-1" style="background-color: #B2BABB;">
                    <header class="avatar" style="background-color: #B2BABB;">
                        <asp:Label ID="label1" runat="server" style="color:#2C3E50;" >Estudio Jurídico Romero y Asociados</asp:Label>
                        <asp:Label ID="label2" runat="server" style="color:#2C3E50;">Sistema de Control de Auditorías</asp:Label>
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="labelUser" runat="server" style="color:#2C3E50;">USUARIO</asp:Label>
                        <br />
                        <asp:Label ID="lblNombre" runat="server" style="color:#2C3E50;">Nombre del colaborador</asp:Label>
                        <br />
                        <asp:Label ID="labelEmpresa" runat="server" style="color:#2C3E50;">EMPRESA</asp:Label>
                        <br />
                        <asp:Label ID="LabeNomEmpresa" runat="server" style="color:#2C3E50;">Nombre de la empresa</asp:Label>
                    </header>
                    <br />
                    <div id="DivMenu" runat="server" class="overflow-auto">
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
                                    <button class="btn btn-outline-dark navbar-btn boton-nuevo boton-margen" id="BtnNuevo" runat="server" onserverclick="BtnNuevo_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Limpia campos de ingreso"></button>
                                    <button class="btn btn-outline-dark navbar-btn boton-buscar boton-margen" id="BtnBuscar" runat="server" onserverclick="BtnBuscar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Busca registros de auditorias"></button>
                                    <button class="btn btn-outline-dark navbar-btn boton-grabar boton-margen" id="BtnGrabar" runat="server" onserverclick="BtnGrabar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Graba registro de auditoria"></button>
                                    <button class="btn btn-outline-dark navbar-btn boton-addtarea boton-margen" id="BtnAddTarea" runat="server" onserverclick="BtnAddTarea_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Asociar tareas a auditoria seleccionada"></button>
                                    <button class="btn btn-outline-dark navbar-btn boton-addgasto boton-margen" id="BtnAddGasto" runat="server" onserverclick="BtnAddGasto_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Asociar gastos a auditoria seleccionada"></button>
                                </nav>
                                <div id="DivOpciones" class="mx-2" runat="server">
                                    <div class="form-group">
                                            <label for="Codigo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Código</label>
                                            <input type="text" class="form-control form-control-sm" style="width: 300px" id="Codigo" placeholder="0" readonly="true" runat="server"/>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label for="OficinaOrigen" class="col-form-label col-form-label-sm" style="font-weight:bold;">Oficina de origen</label>
                                            <asp:DropDownList ID="OficinaOrigen" CssClass="form-select form-select-sm" style="width: 300px" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="OficinaDestino" class="col-form-label col-form-label-sm" style="font-weight:bold;">Oficina de destino</label>
                                            <asp:DropDownList ID="OficinaDestino" CssClass="form-select form-select-sm" style="width: 300px" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-6">
                                            <label for="TipoProceso" class="col-form-label col-form-label-sm" style="font-weight:bold;">Tipo de proceso</label>
                                            <asp:DropDownList ID="TipoProceso" CssClass="form-select form-select-sm" style="width: 300px" runat="server" OnSelectedIndexChanged="TipoProceso_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label for="FechaInicio" class="col-form-label col-form-label-sm" style="font-weight:bold;">Fecha de Inicio</label>
                                            <input type="date" class="form-control form-control-sm" id="FechaInicio" placeholder="" runat="server"/>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="FechaCierre" class="col-form-label col-form-label-sm" style="font-weight:bold;">Fecha de Cierre</label>
                                            <input type="date" class="form-control form-control-sm" id="FechaCierre" placeholder="" runat="server"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label for="Tipo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Tipo de auditoría</label>
                                            <asp:DropDownList ID="Tipo" CssClass="form-select form-select-sm" style="width: 300px" runat="server">
                                                <asp:ListItem Value="L">LOCAL</asp:ListItem>
                                                <asp:ListItem Value="R">REMOTA</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="Observaciones" class="col-form-label col-form-label-sm" style="font-weight:bold;">Observaciones</label>
                                            <input type="text" class="form-control form-control-sm" id="Observaciones" placeholder="" runat="server"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label for="Tipo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Tarea de auditoría</label>
                                            <asp:DropDownList ID="cboTareas" CssClass="form-select form-select-sm"  runat="server">
                                            </asp:DropDownList>
                                        </div> 
                                        <div class="form-group col-md-6">
                                            <label for="Tipo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Plantilla de registro de información</label>
                                            <asp:DropDownList ID="cboPlantillas" CssClass="form-select form-select-sm" style="width: 300px" runat="server">
                                            </asp:DropDownList>
                                        </div> 
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label class="form-check-label form-control-sm" for="Estado" style="font-weight:bold";>Estado</label>
                                        <asp:DropDownList ID="Estado" CssClass="form-select form-select-sm" style="width: 300px" runat="server">
                                            <asp:ListItem Value="A">ABIERTA</asp:ListItem>
                                            <asp:ListItem Value="P">EN PROCESO</asp:ListItem>
                                            <asp:ListItem Value="C">CERRADA</asp:ListItem>
                                            <asp:ListItem Value="X">ANULADA</asp:ListItem>
                                        </asp:DropDownList>
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
        <script src="../Scripts/Auditorias.js" type="text/javascript"></script>
        <script>
            window.onload = function () {
                var botonNuevo = document.getElementById("BtnNuevo");
                botonNuevo.click();
            };

            $(function () {
                $('[data-toggle="tooltip"]').tooltip()
            })
        </script>
</body>
</html>
