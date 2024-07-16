<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuditoriaDocumentoProceso.aspx.cs" Inherits="WebAuditorias.Views.AuditoriaDocumentoProceso" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Actividades relacionadas a documentos de soporte de auditoría</title>
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
</head>
<body style="margin: 0; height: 100%; overflow: hidden; background-color: #E5E8E8;">
    <div class="container-fluid">
        <div class="row header-bar-height" style="background-color: #E5E8E8;">
            <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 my-auto">
                <img class="image-logo-empresa" src="../Images/LogoRomeroyAsociados.png" />
            </div>    
            <div class="col-sm-12 col-md-8 col-lg-8 col-xl-8 center-block text-left my-auto">
                <p class="barra-titulo" style="color:#2C3E50;">Actividades relacionadas a documentos de soporte de auditoría</p>
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
                                <button class="btn btn-outline-dark navbar-btn boton-buscar boton-margen" id="BtnBuscar" runat="server" onserverclick="BtnBuscar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Busca registros de actividades relacionadas al documento"></button>
                                <button class="btn btn-outline-dark navbar-btn boton-grabar boton-margen" id="BtnGrabar" runat="server" onserverclick="BtnGrabar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Graba registro de actividad relacionada al documento"></button>
                                <button class="btn btn-outline-dark navbar-btn boton-eliminar boton-margen" id="BtnEliminar" runat="server" onserverclick="BtnEliminar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Elimina registro de actividad relacionada al documento"></button>
                                <button class="btn btn-outline-dark navbar-btn boton-activar boton-margen" id="BtnActivar" runat="server" onserverclick="BtnActivar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Activa registro de actividad relacionada al documento"></button>
                                <button class="btn btn-outline-dark navbar-btn boton-inactivar boton-margen" id="BtnInactivar" runat="server" onserverclick="BtnInactivar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Inactiva registro de actividad relacionada al documento"></button>
                                <button class="btn btn-outline-dark navbar-btn boton-cerrar boton-margen" id="BtnCerrar" runat="server" onserverclick="BtnCerrar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Cierra registro de actividad relacionada al documento"></button>
                                <button class="btn btn-outline-dark navbar-btn boton-cargaplantilla boton-margen" id="BtnCargar" runat="server" onserverclick="BtnCargar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Subir documento asociado"></button>
                                <button class="btn btn-outline-dark navbar-btn boton-verlinea boton-margen" id="BtnVerLinea" runat="server" onserverclick="BtnVerLinea_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Ver detalle de línea de documento seleccionada"></button>
                                <button class="btn btn-outline-dark navbar-btn boton-verarchivo boton-margen" id="BtnVerArchivo" runat="server" onserverclick="BtnVerArchivo_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Ver documento asociado a actividad"></button>
                            </nav>
                            <div id="DivOpciones" class="px-2" runat="server">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label for="Auditoria" class="col-form-label col-form-label-sm" style="font-weight:bold;">Auditoría</label>
                                        <input type="text" class="form-control form-control-sm" id="Auditoria" placeholder="0" readonly="true" runat="server"/>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="Tarea" class="col-form-label col-form-label-sm" style="font-weight:bold;">Tarea asignada al proceso de auditoría</label>
                                        <input type="text" class="form-control form-control-sm" id="Tarea" placeholder="0" readonly="true" runat="server"/>
                                    </div>
                                    <div class="form-group col-md-4"">
                                        <label for="Plantilla" class="col-form-label col-form-label-sm" style="font-weight:bold;">Plantilla y línea de documento</label>
                                        <input type="text" class="form-control form-control-sm" id="Plantilla" placeholder="0" readonly="true" runat="server"/>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-md-4"">
                                        <label for="Codigo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Secuencia de actividad de línea de documento</label>
                                        <input type="text" class="form-control form-control-sm" id="Codigo" placeholder="0" readonly="true" runat="server"/>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="Fecha" class="col-form-label col-form-label-sm" style="font-weight:bold;">Fecha de registro del proceso</label>
                                        <input type="date" class="form-control form-control-sm" id="Fecha" placeholder="" style="width: 300px" runat="server"/>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="Auditor" class="col-form-label col-form-label-sm" style="font-weight:bold;">Auditor asignado</label>
                                        <asp:DropDownList ID="Auditor" CssClass="form-select form-select-sm" style="width: 50%" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label for="Responsable" class="col-form-label col-form-label-sm" style="font-weight:bold;">Responsable asignado</label>
                                        <asp:DropDownList ID="Responsable" CssClass="form-select form-select-sm" style="width: 50%" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="Observaciones" class="col-form-label col-form-label-sm" style="font-weight:bold;">Detalle de la actividad</label>
                                        <textarea class="form-control" id="Observaciones" rows="5" runat="server"></textarea>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="Archivo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Seleccionar documento asociado</label>
                                        <br />
                                        <asp:FileUpload ID="Archivo" runat="server" Width="600px" onchange="NombreArchivo();"/>
                                        <br />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label for="Documento" class="col-form-label col-form-label-sm" style="font-weight:bold;">Documento asociado a actividad</label>
                                        <input type="text" class="form-control form-control-sm" id="Documento" placeholder="" readonly="true" runat="server"/>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label class="form-check-label form-control-sm" for="Estado" style="font-weight:bold";>Estado</label>
                                        <asp:DropDownList ID="Estado" CssClass="form-select form-select-sm" style="width: 300px" runat="server" Enabled="false">
                                            <asp:ListItem Value="A">ACTIVO</asp:ListItem>
                                            <asp:ListItem Value="I">INACTIVO</asp:ListItem>
                                            <asp:ListItem Value="C">CERRADA</asp:ListItem>
                                            <asp:ListItem Value="X">ELIMINADA</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID = "BtnCargar" />
                        </Triggers>
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
            <h5 class="modal-title" id="exampleModalLabel">Tareas relacionadas a auditoria</h5>
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
    <script src="../Scripts/AuditoriaDocumentoProceso.js" type="text/javascript"></script>
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
