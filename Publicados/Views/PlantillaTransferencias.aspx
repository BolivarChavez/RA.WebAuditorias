<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlantillaTransferencias.aspx.cs" Inherits="WebAuditorias.Views.PlantillaTransferencias" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Plantilla de transferencias</title>
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
    <script src="../Scripts/PlantillaTransferencias.js" type="text/javascript"></script>
</head>
<body style="margin: 0; height: 100%; overflow: hidden; background-color: #E5E8E8;">
        <div class="container-fluid">
            <div class="row header-bar-height" style="background-color: #E5E8E8;">
                <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 my-auto">
                    <img class="image-logo-empresa" src="../Images/LogoRomeroyAsociados.png" />
                </div>    
                <div class="col-sm-12 col-md-8 col-lg-8 col-xl-8 center-block text-left my-auto">
                    <p class="barra-titulo" style="color:#2C3E50;">Plantilla de transferencias</p>
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
                                                <button class="btn btn-outline-dark navbar-btn boton-agregaplantilla boton-margen" id="BtnCargaPlantilla" runat="server" onserverclick="BtnCargaPlantilla_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Grabar plantilla desde archivo"></button>
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
                                                        <label for="Item" class="col-form-label col-form-label-sm" style="font-weight:bold;">Item</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Item" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Req" class="col-form-label col-form-label-sm" style="font-weight:bold;">Req</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Req" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Proveedor" class="col-form-label col-form-label-sm" style="font-weight:bold;">Proveedor</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Proveedor" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Concepto" class="col-form-label col-form-label-sm" style="font-weight:bold;">Concepto</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Concepto" placeholder="" runat="server" onclick="muestraContenidoTexto('Concepto', 'Concepto')"/>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-3">
                                                        <label for="Documento" class="col-form-label col-form-label-sm" style="font-weight:bold;">Documento (Factura/Recibo/Boleta)</label>
                                                        <input type="text" class="form-control form-control-sm" style="font-size: 12px" id="Documento" placeholder="" runat="server" />
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Mes" class="col-form-label col-form-label-sm" style="font-weight:bold;">Mes</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Mes" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Moneda" class="col-form-label col-form-label-sm" style="font-weight:bold;">Moneda</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Moneda" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Importe_Monto" class="col-form-label col-form-label-sm" style="font-weight:bold;">Importe Monto</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Importe_Monto" placeholder="" runat="server"/>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-3">
                                                        <label for="Monto" class="col-form-label col-form-label-sm" style="font-weight:bold;">Monto</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Monto" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Tipo_Cambio" class="col-form-label col-form-label-sm" style="font-weight:bold;">Tipo de Cambio</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Tipo_Cambio" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Comprobante_Pago" class="col-form-label col-form-label-sm" style="font-weight:bold;">Comprobante de Pago</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Comprobante_Pago" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Fecha_Pago" class="col-form-label col-form-label-sm" style="font-weight:bold;">Fecha de Pago</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Fecha_Pago" placeholder="" runat="server"/>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-3">
                                                        <label for="Observacion_Preliminar" class="col-form-label col-form-label-sm" style="font-weight:bold;">Observación preliminar</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Observacion_Preliminar" placeholder="" runat="server" onclick="muestraContenidoTexto('Observacion Preliminar', 'Observacion_Preliminar')"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Observacion_Final" class="col-form-label col-form-label-sm" style="font-weight:bold;">Observación final</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Observacion_Final" placeholder="" runat="server" onclick="muestraContenidoTexto('Observacion Final', 'Observacion_Final')"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Estado" class="col-form-label col-form-label-sm" style="font-weight:bold;">Estado</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Estado" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Banco" class="col-form-label col-form-label-sm" style="font-weight:bold;">Banco</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Banco" placeholder="" runat="server"/>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-3">
                                                        <label for="Empresa" class="col-form-label col-form-label-sm" style="font-weight:bold;">Empresa</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Empresa" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Sede" class="col-form-label col-form-label-sm" style="font-weight:bold;">Sede</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Sede" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Cuenta" class="col-form-label col-form-label-sm" style="font-weight:bold;">Cuenta Contable</label>
                                                        <input type="text" class="form-control form-control-sm border-primary" style="font-size: 12px" id="Cuenta" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Sub_Cuenta" class="col-form-label col-form-label-sm" style="font-weight:bold;">Sub Cuenta Contable</label>
                                                        <input type="text" class="form-control form-control-sm" style="font-size: 12px" id="Sub_Cuenta" placeholder="" runat="server"/>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-3">
                                                        <label for="Soporte" class="col-form-label col-form-label-sm" style="font-weight:bold;">Soporte</label>
                                                        <input type="text" class="form-control form-control-sm" style="font-size: 12px" id="Soporte" placeholder="" runat="server" onclick="muestraContenidoTexto('Soporte', 'Soporte')"/>
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
