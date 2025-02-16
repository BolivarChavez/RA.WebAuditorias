<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RevisaPlantillas.aspx.cs" Inherits="WebAuditorias.Views.RevisaPlantillas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Revisión de plantillas de información de procesos de auditoría</title>
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:ital,wght@0,300..800;1,300..800&display=swap" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Styles/Custom-Opciones.css" />
    <link rel="stylesheet" href="../Styles/Custom-Toolbar.css" />
    <link rel="stylesheet" href="../Styles/material.css" />
    <link rel="stylesheet" href="../Styles/Custom-Grid.css" />
    <link rel="stylesheet" href="../Styles/Custom-Grid-Font.css" />
    <script src="../Scripts/ej2.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.2/js/bootstrap.min.js"></script>
</head>
<body style="background-color: #E5E8E8;">
        <div class="container-fluid">
            <div class="row header-bar-height" style="background-color: #E5E8E8;">
                <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 my-auto">
                    <img class="image-logo-empresa" src="../Images/LogoRomeroyAsociados.png" />
                </div>    
                <div class="col-sm-12 col-md-8 col-lg-8 col-xl-8 center-block text-left my-auto">
                    <p id="TituloOpcion" class="barra-titulo" style="color:#2C3E50;" runat="server">Revisión de plantillas de información de procesos de auditoría</p>
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
                                    <button class="btn btn-outline-dark navbar-btn boton-buscar boton-margen" id="BtnBuscar" runat="server" onserverclick="BtnBuscar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Busca registros de actividades relacionadas a registro de plantilla"></button>
                                    <button class="btn btn-outline-dark navbar-btn boton-cerrar boton-margen" id="BtnCerrar" runat="server" data-toggle="tooltip" data-placement="bottom" title="Crear actividad de cierre de registro de plantilla"></button>
                                </nav>
                                <div id="DivOpciones" class="px-2" runat="server">
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label for="ProcesoAuditoria" class="col-form-label col-form-label-sm" style="font-weight:bold;">Proceso de Auditoría</label>
                                            <asp:DropDownList ID="ProcesoAuditoria" CssClass="form-select form-select-sm" style="width: 100%; font-size: 12px" runat="server" OnSelectedIndexChanged="ProcesoAuditoria_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                <asp:HiddenField ID="HiddenField3" runat="server" />
                                <asp:HiddenField ID="HiddenField4" runat="server" />
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </form> 
                    </div>
                    <br />
                    <div class="mx-2">
                        <div class="card text-left border-primary rounded-2 shadow" style="width: 100%; height:450px">
                            <div id="TituloSeccion3" class="card-header text-primary">
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
                        <div class="card text-left border-primary rounded-2 shadow" style="width: 100%; height:600px">
                            <div id="TituloSeccion4" class="card-header text-primary">
                                <b>Plantillas asociadas a tarea</b>
                            </div>
                            <div id="GridConsulta" class="content-wrapper py-2" style="width:100%;">
                                <div class="card-body">
                                    <div id="Grid">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="mx-2">
                        <div class="card text-left border-primary rounded-2 shadow" style="width: 100%; height:450px">
                            <div id="TituloSeccion5" class="card-header text-primary">
                                <b>Actividades asociadas a plantilla</b>
                            </div>
                            <div id="GridConsultaProcesos" class="content-wrapper py-2" style="width:100%">
                                <div class="card-body">
                                    <div id="GridProcesos">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row py-2">
                        <div id="GridFinConsultaProcesos" class="content-wrapper py-2" style="width:100%">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="popupMessage" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Revisión de plantillas de información de procesos de auditoría</h5>
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
        <script src="../Scripts/RevisaPlantillas.js" type="text/javascript"></script>
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
