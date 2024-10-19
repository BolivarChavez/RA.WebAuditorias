<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlantillaTributos.aspx.cs" Inherits="WebAuditorias.Views.PlantillaTributos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Plantilla de tributos</title>
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
    <script src="../Scripts/PlantillaTributos.js" type="text/javascript"></script>
</head>
<body style="margin: 0; height: 100%; overflow: hidden; background-color: #E5E8E8;">
        <div class="container-fluid">
            <div class="row header-bar-height" style="background-color: #E5E8E8;">
                <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 my-auto">
                    <img class="image-logo-empresa" src="../Images/LogoRomeroyAsociados.png" />
                </div>    
                <div class="col-sm-12 col-md-8 col-lg-8 col-xl-8 center-block text-left my-auto">
                    <p class="barra-titulo" style="color:#2C3E50;">Plantilla de tributos</p>
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
                        <div class="container-fluid">
                            <ul class="nav nav-tabs" id="myTab" role="tablist">
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link active" id="home-tab" data-bs-toggle="tab" data-bs-target="#Divdetalle" type="button" role="tab" aria-controls="Divdetalle" aria-selected="true">Detalle de registro de plantilla</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="profile-tab" data-bs-toggle="tab" data-bs-target="#Divlista" type="button" role="tab" aria-controls="Divlista" aria-selected="false">Listado de registros de plantilla</button>
                                </li>
                            </ul>
                            <div class="tab-content" id="myTabContent">
                                <div id="Divdetalle" class="tab-pane fade show active" role="tabpanel" aria-labelledby="home-tab">
                                    <form id="form1" runat="server">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                                        </asp:ScriptManager>
                                        <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <nav class="navbar navbar-expand-lg">
                                                <button class="btn btn-outline-dark navbar-btn boton-nuevo boton-margen" id="BtnNuevo" runat="server" onserverclick="BtnNuevo_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Limpia campos de ingreso"></button>
                                                <button class="btn btn-outline-dark navbar-btn boton-buscar boton-margen" id="BtnBuscar" runat="server" onserverclick="BtnBuscar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Busca registros de plantilla"></button>
                                                <button class="btn btn-outline-dark navbar-btn boton-grabar boton-margen" id="BtnGrabar" runat="server" onserverclick="BtnGrabar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Graba registro de plantilla"></button>
                                                <button class="btn btn-outline-dark navbar-btn boton-eliminar boton-margen" id="BtnEliminar" runat="server" onserverclick="BtnEliminar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Elimina registro de plantilla"></button>
                                                <button class="btn btn-outline-dark navbar-btn boton-cargaplantilla boton-margen" id="BtnCargar" runat="server" onserverclick="BtnCargar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Subir archivo de plantilla"></button>
                                                <button class="btn btn-outline-dark navbar-btn boton-agregaplantilla boton-margen" id="BtnCargaPlantilla" runat="server" onserverclick="BtnCargaPlantilla_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Grablar plantilla desde archivo"></button>
                                                <button class="btn btn-outline-dark navbar-btn boton-addproceso boton-margen" id="BtnAddTarea" runat="server" onserverclick="BtnAddTarea_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Asociar actividades al registro seleccionado"></button>
                                                <button class="btn btn-outline-dark navbar-btn boton-gruporegistro boton-margen" id="BtnAddTareaGrupo" runat="server" onserverclick="BtnAddTareaGrupo_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Asociar actividades a un grupo de registros"></button>
                                            </nav>
                                            <div id="DivOpciones" class="px-2" style="overflow-x: hidden; overflow-y: auto" runat="server">
                                                <div class="row">
                                                    <div class="form-group col-md-3">
                                                        <label for="Auditoria" class="col-form-label col-form-label-sm" style="font-weight:bold;">Auditoría</label>
                                                        <input type="text" class="form-control form-control-sm" style="font-size: 12px" id="Auditoria" placeholder="0" readonly="true" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Codigo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Código</label>
                                                        <input type="text" class="form-control form-control-sm" style="font-size: 12px" id="Codigo" placeholder="0" readonly="true" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Tarea" class="col-form-label col-form-label-sm" style="font-weight:bold;">Tarea</label>
                                                        <input type="text" class="form-control form-control-sm" style="font-size: 12px" id="Tarea" placeholder="0" readonly="true" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Plantilla" class="col-form-label col-form-label-sm" style="font-weight:bold;">Plantilla</label>
                                                        <input type="text" class="form-control form-control-sm" style="font-size: 12px" id="Plantilla" placeholder="0" readonly="true" runat="server"/>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="Referencia" class="col-form-label col-form-label-sm" style="font-weight:bold;">Referencia</label>
                                                    <input type="text" class="form-control form-control-sm" style="font-size: 12px" id="Referencia" placeholder="" runat="server"/>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-3">
                                                        <label for="Fecha" class="col-form-label col-form-label-sm" style="font-weight:bold;">Fecha</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Fecha" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Periodo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Periodo</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Periodo" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Tributo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Tributo</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Tributo" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Tributo_Resultante" class="col-form-label col-form-label-sm" style="font-weight:bold;">Tributo Resultante</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Tributo_Resultante" placeholder="" runat="server"/>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-3">
                                                        <label for="Intereses" class="col-form-label col-form-label-sm" style="font-weight:bold;">Intereses</label>
                                                        <input type="text" class="form-control form-control-sm" style="font-size: 12px" id="Intereses" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Total_Pagar" class="col-form-label col-form-label-sm" style="font-weight:bold;">Total por Pagar</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Total_Pagar" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Forma_Pago" class="col-form-label col-form-label-sm" style="font-weight:bold;">Forma de Pago</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Forma_Pago" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Egreso" class="col-form-label col-form-label-sm" style="font-weight:bold;">Egreso</label>
                                                        <input type="text" class="form-control form-control-sm" style="font-size: 12px" id="Egreso" placeholder="" runat="server"/>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-3">
                                                        <label for="Fecha_Informe" class="col-form-label col-form-label-sm" style="font-weight:bold;">Fecha de Informe</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Fecha_Informe" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Numero_Informe" class="col-form-label col-form-label-sm" style="font-weight:bold;">Número de Informe</label>
                                                        <input type="text" class="form-control form-control-sm" style="font-size: 12px" id="Numero_Informe" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Observaciones" class="col-form-label col-form-label-sm" style="font-weight:bold;">Observaciones</label>
                                                        <input type="text" class="form-control form-control-sm" style="font-size: 12px" id="Observaciones" placeholder="" runat="server" onclick="muestraContenidoTexto('Observaciones', 'Observaciones')"/>
                                                    </div>
                                                </div>
                                                <div class="form-check">
                                                    <input class="form-check-input" type="checkbox" value="" id="chkEstado" runat="server"/>
                                                    <label class="form-check-label form-control-sm" style="font-size: 12px" for="chkEstado">El registro de la plantilla se encuentra activo</label>
                                                </div>
                                               <div class="form-group">
                                                    <label for="CargaArchivo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Carga plantilla desde archivo</label>
                                                    <br />
                                                    <asp:FileUpload ID="CargaArchivo" runat="server" Width="600px" Font-Size="12 px"/>
                                                    <br />
                                                    <label class="form-check-label form-control-sm" for="Hoja" style="font-weight:bold";>Hoja</label>
                                                    <asp:DropDownList ID="Hoja" CssClass="form-select form-select-sm" style="width: 300px; font-size: 12px" runat="server">
                                                    </asp:DropDownList>                                                    
                                                </div>
                                                </div>
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID = "BtnCargar" />
                                            <asp:PostBackTrigger ControlID = "BtnCargaPlantilla" />
                                        </Triggers>
                                        </asp:UpdatePanel>
                                    </form> 
                                </div>
                                <div id="Divlista" class="tab-pane fade" role="tabpanel" aria-labelledby="profile-tab">
                                    <div id="GridConsulta" class="content-wrapper py-2" style="width:100%">
                                        <div id="Grid">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Editor de campos de texto -->
        <div class="modal fade" id="myModal">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="tituloCampo"></h4>
                        <button type="button" class="close" data-dismiss="modal" onclick="cierraContenidoTexto()">&times;</button>
                    </div>
                    <div class="modal-body">
                        <textarea rows="5" class="form-control" style="overflow-y:scroll; font-size: 12px" id="message-text"></textarea>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="cierraContenidoTexto()">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
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
