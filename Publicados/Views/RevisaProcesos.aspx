<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RevisaProcesos.aspx.cs" Inherits="WebAuditorias.Views.RevisaProcesos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Revisión de procesos de auditoría</title>
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
                    <p id="TituloOpcion" class="barra-titulo" style="color:#2C3E50;" runat="server">Revisión de procesos de auditoría</p>
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
                <div id="Contenedor" class="col-sm-12 col-md-12 col-lg-12 col-xl-12 px-4 py-0 bg-white h-100 opcion-backcolor-2" style="overflow-y: scroll;">
                    <div id="Formulario">
                        <form id="form1" runat="server">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <nav class="navbar navbar-expand-lg">
                                    <button class="btn btn-outline-dark navbar-btn boton-buscar boton-margen" id="BtnBuscar" runat="server" onserverclick="BtnBuscar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Buscar detalles de registro de auditoría"></button>
                                    <button class="btn btn-outline-dark navbar-btn boton-cerrar boton-margen" id="BtnCerrar" runat="server" onserverclick="BtnCerrar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Cerrar proceso de auditoría"></button>
                                    <button class="btn btn-outline-dark navbar-btn boton-informe boton-margen" id="BtnInforme" runat="server" onserverclick="BtnInforme_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Generar informe de actividades de auditoría"></button>
                                </nav>
                                <div id="DivOpciones" class="px-2" runat="server">
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label for="ProcesoAuditoria" class="col-form-label col-form-label-sm" style="font-weight:bold;">Proceso de Auditoría</label>
                                            <asp:DropDownList ID="ProcesoAuditoria" CssClass="form-select form-select-sm" style="width: 100%" runat="server" OnSelectedIndexChanged="ProcesoAuditoria_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div id="Div1" class="mx-2" runat="server">
                                    <br />
                                    <div class="card text-left border-primary rounded-2 shadow" style="width: 100%;">
                                        <div class="card-header text-primary">
                                            <b>Información General</b>
                                        </div>
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="form-group col-md-4">
                                                        <label for="Codigo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Código</label>
                                                        <input type="text" class="form-control form-control-sm" style="width: 300px; font-size: 12px" id="Codigo" placeholder="0" readonly="true" runat="server"/>
                                                </div>
                                                <div class="form-group col-md-4">
                                                        <label for="TipoProceso" class="col-form-label col-form-label-sm" style="font-weight:bold;">Tipo de proceso</label>
                                                        <asp:DropDownList ID="TipoProceso" CssClass="form-select form-select-sm" style="width: 300px; font-size: 12px" runat="server" Enabled="false"></asp:DropDownList>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <label for="OficinaOrigen" class="col-form-label col-form-label-sm" style="font-weight:bold;">Oficina de origen</label>
                                                    <asp:DropDownList ID="OficinaOrigen" CssClass="form-select form-select-sm" style="width: 300px; font-size: 12px" runat="server" Enabled="false"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-md-4">
                                                    <label for="OficinaDestino" class="col-form-label col-form-label-sm" style="font-weight:bold;">Oficina de destino</label>
                                                    <asp:DropDownList ID="OficinaDestino" CssClass="form-select form-select-sm" style="width: 300px; font-size: 12px" runat="server" Enabled="false"></asp:DropDownList>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <label for="FechaInicio" class="col-form-label col-form-label-sm" style="font-weight:bold;">Fecha de Inicio</label>
                                                    <input type="date" class="form-control form-control-sm" id="FechaInicio" placeholder="" style="width: 300px; font-size: 12px" runat="server" readonly="true"/>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <label for="FechaCierre" class="col-form-label col-form-label-sm" style="font-weight:bold;">Fecha de Cierre</label>
                                                    <input type="date" class="form-control form-control-sm" id="FechaCierre" placeholder="" style="width: 300px; font-size: 12px" runat="server" readonly="true"/>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-md-4">
                                                    <label for="Tipo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Tipo de auditoría</label>
                                                    <asp:DropDownList ID="Tipo" CssClass="form-select form-select-sm" style="width: 300px; font-size: 12px" runat="server" Enabled="false">
                                                        <asp:ListItem Value="L">LOCAL</asp:ListItem>
                                                        <asp:ListItem Value="R">REMOTA</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <label for="Observaciones" class="col-form-label col-form-label-sm" style="font-weight:bold;">Observaciones</label>
                                                    <input type="text" class="form-control form-control-sm" style="font-size: 12px" id="Observaciones" placeholder="" runat="server" maxlength="400" readonly="true"/>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <label class="form-check-label form-control-sm" for="Estado" style="font-weight:bold";>Estado</label>
                                                    <asp:DropDownList ID="Estado" CssClass="form-select form-select-sm" style="width: 300px; font-size: 12px" runat="server" Enabled="false">
                                                        <asp:ListItem Value="A">ABIERTA</asp:ListItem>
                                                        <asp:ListItem Value="P">EN PROCESO</asp:ListItem>
                                                        <asp:ListItem Value="C">CERRADA</asp:ListItem>
                                                        <asp:ListItem Value="X">ANULADA</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                <input type="hidden" id="Documento" name="Documento" value="" runat="server" />
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </form> 
                    </div>
                    <br />
                    <div class="mx-2">
                        <div class="card text-left border-primary rounded-2 shadow" style="width: 100%; height:450px">
                            <div class="card-header text-primary">
                                <b>Gastos de proceso de auditoría</b>
                            </div>
                            <div id="GridConsultaGastos" class="content-wrapper py-2" style="width:100%;">
                                <div class="card-body">
                                    <div id="GridGastos">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="mx-2">
                        <div class="card text-left border-primary rounded-2 shadow" style="width: 100%; height:450px">
                            <div class="card-header text-primary">
                                <b>Tareas asociadas a proceso de auditoría</b>
                             </div>
                            <div id="GridConsultaTareas" class="content-wrapper py-2" style="width:100%">
                                <div class="card-body">
                                    <div id="GridTareas">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="mx-2">
                        <div class="card text-left border-primary rounded-2 shadow" style="width: 100%; height:450px">
                            <div id="TituloSeccion4" class="card-header text-primary">
                                <b>Responsables asignados a tarea</b>
                            </div>
                            <div id="GridConsultaAsignacion" class="content-wrapper py-2" style="width:100%">
                                <div class="card-body">
                                    <div id="GridAsignacion">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="mx-2">
                        <div class="card text-left border-primary rounded-2 shadow" style="width: 100%; height:450px">
                            <div id="TituloSeccion5" class="card-header text-primary">
                                <b>Procesos asociados a tarea</b>
                            </div>
                            <div id="GridConsultaProcesos" class="content-wrapper py-2" style="width:100%;">
                            <script id="template" type="text/x-template">
                                <div >
                                    <a href="VistaArchivo.aspx?archivo=${at_documento}" target="_blank"  >
                                    ${at_documento}
                                    </a>
                                </div>
                            </script>
                            <div class="card-body">
                                <div id="GridProcesos">
                                </div>
                            </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        <div class="modal fade" id="popupMessage" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Revisión de procesos de auditoría</h5>
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
        <script src="../Scripts/RevisaProcesos.js" type="text/javascript"></script>
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
