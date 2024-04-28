<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlantillaCheques.aspx.cs" Inherits="WebAuditorias.Views.PlantillaCheques" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Plantilla de cheques</title>
<%--    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;500&display=swap" rel="stylesheet" />--%>
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@400;500&family=Poppins:wght@400;500&display=swap" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Styles/Custom-Opciones.css" />
    <link rel="stylesheet" href="../Styles/Custom-Toolbar.css" />
    <link rel="stylesheet" href="../Styles/material.css" />
    <link rel="stylesheet" href="../Styles/Custom-Grid.css" />
    <link rel="stylesheet" href="../Styles/Custom-Grid-Font.css" />
    <script src="../Scripts/ej2.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.2/js/bootstrap.min.js"></script>
</head>
<body style="font-family: 'Poppins', sans-serif; margin: 0; height: 100%; overflow: hidden; background-color: #E5E8E8;">
        <div class="container-fluid">
            <div class="row header-bar-height" style="background-color: #E5E8E8;">
                <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 my-auto" style="border: 3px solid #DAA520;">
                    <img class="image-logo-empresa" src="../Images/LogoRomeroyAsociados.png" />
                </div>    
                <div class="col-sm-12 col-md-10 col-lg-10 col-xl-10 center-block text-left my-auto">
                    <p class="barra-titulo" style="color:#2C3E50;">Plantilla de cheques</p>
                </div>  
            </div>
            <div class="row console-menu-height" style="background-color: #B2BABB;">
                <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 py-4 h-100 opcion-backcolor-1" style="background-color: #E5E8E8; border: 3px solid #DAA520;">
                    <header class="avatar" style="background-color: #E5E8E8;">
                        <asp:Label ID="label1" runat="server" style="color:#2C3E50;" >Estudio Jurídico Romero y Asociados</asp:Label>
                        <asp:Label ID="label2" runat="server" style="color:#2C3E50;">Sistema de Control de Auditorías</asp:Label>
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="labelUser" runat="server" style="color:#2C3E50;">USUARIO</asp:Label>
                        <br />
                        <asp:Label ID="lblNombre" runat="server" style="color:#A1998E;">Nombre del colaborador</asp:Label>
                        <br />
                        <asp:Label ID="labelEmpresa" runat="server" style="color:#2C3E50;">EMPRESA</asp:Label>
                        <br />
                        <asp:Label ID="LabeNomEmpresa" runat="server" style="color:#A1998E;">Nombre de la empresa</asp:Label>
                    </header>
                    <br />
                    <div id="DivMenu" runat="server" class="overflow-auto">
                    </div>
                </div>
                <div id="Contenedor" class="col-sm-12 col-md-10 col-lg-10 col-xl-10 px-0 py-0 bg-white h-100 opcion-backcolor-2">
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
                                            </nav>
                                            <div id="DivOpciones" class="px-2" style="overflow-x: hidden; overflow-y: auto" runat="server">
                                                <div class="row">
                                                    <div class="form-group col-md-3">
                                                        <label for="Auditoria" class="col-form-label col-form-label-sm" style="font-weight:bold;">Auditoria</label>
                                                        <input type="text" class="form-control form-control-sm" id="Auditoria" placeholder="0" readonly="true" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Codigo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Codigo</label>
                                                        <input type="text" class="form-control form-control-sm" id="Codigo" placeholder="0" readonly="true" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Tarea" class="col-form-label col-form-label-sm" style="font-weight:bold;">Tarea</label>
                                                        <input type="text" class="form-control form-control-sm" id="Tarea" placeholder="0" readonly="true" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Plantilla" class="col-form-label col-form-label-sm" style="font-weight:bold;">Plantilla</label>
                                                        <input type="text" class="form-control form-control-sm" id="Plantilla" placeholder="0" readonly="true" runat="server"/>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="Referencia" class="col-form-label col-form-label-sm" style="font-weight:bold;">Referencia</label>
                                                    <input type="text" class="form-control form-control-sm" id="Referencia" placeholder="" runat="server"/>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-3">
                                                        <label for="Item" class="col-form-label col-form-label-sm" style="font-weight:bold;">Item</label>
                                                        <input type="text" class="form-control form-control-sm" id="Item" placeholder="" runat="server" onclick="muestraContenidoTexto('Item', 'Item')"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Talonario" class="col-form-label col-form-label-sm" style="font-weight:bold;">Talonario</label>
                                                        <input type="text" class="form-control form-control-sm" id="Talonario" placeholder="" runat="server" onclick="muestraContenidoTexto('Talonario', 'Talonario')"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Req" class="col-form-label col-form-label-sm" style="font-weight:bold;">Req</label>
                                                        <input type="text" class="form-control form-control-sm" id="Req" placeholder="" runat="server" onclick="muestraContenidoTexto('Req', 'Req')"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Beneficiario" class="col-form-label col-form-label-sm" style="font-weight:bold;">Beneficiario</label>
                                                        <input type="text" class="form-control form-control-sm" id="Beneficiario" placeholder="" runat="server" onclick="muestraContenidoTexto('Beneficiario', 'Beneficiario')"/>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-3">
                                                        <label for="Comprobante" class="col-form-label col-form-label-sm" style="font-weight:bold;">Comprobante</label>
                                                        <input type="text" class="form-control form-control-sm" id="Comprobante" placeholder="" runat="server" onclick="muestraContenidoTexto('Comprobante', 'Comprobante')"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Monto" class="col-form-label col-form-label-sm" style="font-weight:bold;">Monto</label>
                                                        <input type="text" class="form-control form-control-sm" id="Monto" placeholder="" runat="server" onclick="muestraContenidoTexto('Monto', 'Monto')"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="FechaPago" class="col-form-label col-form-label-sm" style="font-weight:bold;">Fecha de pago</label>
                                                        <input type="date" class="form-control form-control-sm" id="FechaPago" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="ComprobanteEgreso" class="col-form-label col-form-label-sm" style="font-weight:bold;">Comprobante de egreso</label>
                                                        <input type="text" class="form-control form-control-sm" id="ComprobanteEgreso" placeholder="" runat="server" onclick="muestraContenidoTexto('ComprobanteEgreso', 'ComprobanteEgreso')"/>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-3">
                                                        <label for="Banco" class="col-form-label col-form-label-sm" style="font-weight:bold;">Banco</label>
                                                        <input type="text" class="form-control form-control-sm" id="Banco" placeholder="" runat="server" onclick="muestraContenidoTexto('Banco', 'Banco')"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="NumeroCheque" class="col-form-label col-form-label-sm" style="font-weight:bold;">Numero de cheque</label>
                                                        <input type="text" class="form-control form-control-sm" id="NumeroCheque" placeholder="" runat="server" />
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="TipoCambio" class="col-form-label col-form-label-sm" style="font-weight:bold;">Tipo de cambio</label>
                                                        <input type="text" class="form-control form-control-sm" id="TipoCambio" placeholder="" runat="server" />
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="ObservacionPreliminar" class="col-form-label col-form-label-sm" style="font-weight:bold;">Observacion preliminar</label>
                                                        <input type="text" class="form-control form-control-sm" id="ObservacionPreliminar" placeholder="" runat="server" onclick="muestraContenidoTexto('Observacion Preliminar', 'ObservacionPreliminar')"/>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-3">
                                                        <label for="Observacion_Final" class="col-form-label col-form-label-sm" style="font-weight:bold;">Observacion final</label>
                                                        <input type="text" class="form-control form-control-sm" id="Observacion_Final" placeholder="" runat="server" onclick="muestraContenidoTexto('Observacion Final', 'Observacion_Final')"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Estado" class="col-form-label col-form-label-sm" style="font-weight:bold;">Estado</label>
                                                        <input type="text" class="form-control form-control-sm" id="Estado" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Tipo_Plantilla" class="col-form-label col-form-label-sm" style="font-weight:bold;">Tipo de plantilla</label>
                                                        <input type="text" class="form-control form-control-sm" id="Tipo_Plantilla" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Cuentas" class="col-form-label col-form-label-sm" style="font-weight:bold;">Cuentas</label>
                                                        <input type="text" class="form-control form-control-sm" id="Cuentas" placeholder="" runat="server" onclick="muestraContenidoTexto('Cuentas', 'Cuentas')"/>
                                                    </div>
                                                </div>
                                                <div class="form-check">
                                                    <input class="form-check-input" type="checkbox" value="" id="chkEstado" runat="server"/>
                                                    <label class="form-check-label form-control-sm" for="chkEstado">El registro de la plantilla se encuentra activo</label>
                                                </div>
                                               <div class="form-group">
                                                    <label for="CargaArchivo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Carga plantilla desde archivo</label>
                                                    <br />
                                                    <asp:FileUpload ID="CargaArchivo" runat="server" Width="600px"/>
                                                    <br />
                                                    <label class="form-check-label form-control-sm" for="Hoja" style="font-weight:bold";>Hoja</label>
                                                    <asp:DropDownList ID="Hoja" CssClass="form-select form-select-sm" style="width: 300px" runat="server">
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
                                    <div id="GridConsulta" class="content-wrapper py-2" style="width:100%;">
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
                        <textarea rows="5" class="form-control" style="overflow-y:scroll;" id="message-text"></textarea>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="cierraContenidoTexto()">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
        <script src="../Scripts/PlantillaCheques.js" type="text/javascript"></script>
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
