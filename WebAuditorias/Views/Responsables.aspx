﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Responsables.aspx.cs" Inherits="WebAuditorias.Views.Responsables" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Responsables de auditorías</title>
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:ital,wght@0,300..800;1,300..800&display=swap" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Styles/Custom-Opciones.css" />
    <link rel="stylesheet" href="../Styles/Custom-Toolbar.css" />
    <link rel="stylesheet" href="../Styles/material.css" />
    <link rel="stylesheet" href="../Styles/Custom-Grid.css" />
    <script src="../Scripts/ej2.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.2/js/bootstrap.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body style="margin: 0; height: 100%; overflow: hidden; background-color: #E5E8E8;">
        <div class="container-fluid">
            <div class="row header-bar-height" style="background-color: #E5E8E8;">
                <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 my-auto">
                    <img class="image-logo-empresa" src="../Images/LogoRomeroyAsociados.png" />
                </div>    
                <div class="col-sm-12 col-md-8 col-lg-8 col-xl-8 center-block text-left my-auto">
                    <p class="barra-titulo" style="color:#2C3E50;">Responsables de auditorías</p>
                </div>  
                <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 center-block text-left my-auto">
                    <div class="container">
                        <div class="row">
                            <div class="col">
                                <asp:Label ID="labelUser" runat="server" ForeColor="#2C3E50" Font-Size="11px">USUARIO :</asp:Label>
                            </div>
                            <div class="col">
                                <asp:Label ID="lblNombre" runat="server" ForeColor="#2C3E50" Font-Size="11px" Font-Bold="True">Nombre del colaborador</asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:Label ID="lblFecha" runat="server" ForeColor="#2C3E50" Font-Size="11px">FECHA DE INGRESO :</asp:Label>
                            </div>
                            <div class="col">
                                <asp:Label ID="lblFechaConexion" runat="server" ForeColor="#2C3E50" Font-Size="11px" Font-Bold="True">Fecha y hora del dia</asp:Label>
                            </div>
                        </div>
                    </div>
                </div>  
            </div>
            <div class="row console-menu-height" style="background-color: #B2BABB;">
                <div id="Contenedor" class="col-sm-12 col-md-12 col-lg-12 col-xl-12 px-4 py-0 bg-white h-100 opcion-backcolor-2">
                    <div id="Formulario">
                        <form id="form1" runat="server">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <nav class="navbar navbar-expand-lg">
                                    <button class="btn btn-outline-dark navbar-btn boton-nuevo boton-margen" id="BtnNuevo" runat="server" onserverclick="BtnNuevo_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Limpia campos de ingreso"></button>
                                    <button class="btn btn-outline-dark navbar-btn boton-buscar boton-margen" id="BtnBuscar" runat="server" onserverclick="BtnBuscar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Busca registros de responsables de auditorias"></button>
                                    <button class="btn btn-outline-dark navbar-btn boton-grabar boton-margen" id="BtnGrabar" runat="server" onserverclick="BtnGrabar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Graba registro de responsables de auditorias"></button>
                                </nav>
                                <div id="DivOpciones" class="mx-2" runat="server">
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label for="Codigo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Código</label>
                                            <input type="text" class="form-control form-control-sm" style="font-size: 12px" id="Codigo" placeholder="0" readonly="true" runat="server"/>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="Nombre" class="col-form-label col-form-label-sm" style="font-weight:bold;">Nombre del responsable</label>
                                            <input type="text" class="form-control form-control-sm" style="font-size: 12px" id="Nombre" placeholder="" runat="server" maxlength="150"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label for="Cargo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Cargo</label>
                                            <input type="text" class="form-control form-control-sm" style="font-size: 12px" id="Cargo" placeholder="" runat="server" maxlength="80"/>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="Oficina" class="col-form-label col-form-label-sm" style="font-weight:bold;">Oficina de origen</label>
                                            <asp:DropDownList ID="Oficina" CssClass="form-select form-select-sm" style="width: 300px; font-size: 12px" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label for="Tipo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Tipo</label>
                                            <asp:DropDownList ID="Tipo" CssClass="form-select form-select-sm" style="width: 300px; font-size: 12px" runat="server">
                                                <asp:ListItem Value="A">AUDITOR</asp:ListItem>
                                                <asp:ListItem Value="J">JEFE DE AUDITORIA</asp:ListItem>
                                                <asp:ListItem Value="R">RESPONSABLE DE INFORMACION</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="Correo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Correo electrónico</label>
                                            <input type="email" class="form-control form-control-sm" style="font-size: 12px" id="Correo" placeholder="" runat="server" maxlength="100"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label for="Usuario" class="col-form-label col-form-label-sm" style="font-weight:bold;">Usuario</label>
                                            <input type="text" class="form-control form-control-sm" style="font-size: 12px" id="Usuario" placeholder="" runat="server"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <input class="form-check-input" type="checkbox" value="" id="chkEstado" runat="server"/>
                                            <label class="form-check-label form-control-sm" style="font-size: 12px" for="chkEstado">El registro seleccionado se encuentra activo</label>
                                        </div>
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
        <div class="modal fade" id="popupMessage" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Responsables de auditorías</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="cierraMessagePopUp()">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <p id="messageContent"></p>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="cierraMessagePopUp()">Cerrar</button>
              </div>
            </div>
          </div>
        </div>
        <script src="../Scripts/Responsables.js" type="text/javascript"></script>
        <script>
            $(document).ready(function () {
                $('[data-toggle="tooltip"]').tooltip()
            });

            $(function () {
                $('[data-toggle="tooltip"]').tooltip({
                    trigger: 'hover'
                });

                $('[data-toggle="tooltip"]').on('click', function () {
                    $(this).tooltip('hide')
                });
            });
        </script>
</body>
</html>
