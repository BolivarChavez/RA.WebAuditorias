<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlantillaRegalias.aspx.cs" Inherits="WebAuditorias.Views.PlantillaRegalias" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Plantilla de regalías</title>
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;500&display=swap" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Styles/Custom-Opciones.css" />
    <link rel="stylesheet" href="../Styles/Custom-Toolbar.css" />
    <link rel="stylesheet" href="../Styles/material.css" />
    <link rel="stylesheet" href="../Styles/Custom-Grid.css" />
    <script src="../Scripts/ej2.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.2/js/bootstrap.min.js"></script>
</head>
<body style="margin: 0; height: 100%; overflow: hidden; background-color: #E5E8E8;">
        <div class="container-fluid">
            <div class="row header-bar-height" style="background-color: #E5E8E8;">
                <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 my-auto">
                    <img class="image-logo-empresa" src="../Images/LogoRomeroyAsociados.png" />
                </div>    
                <div class="col-sm-12 col-md-10 col-lg-10 col-xl-10 center-block text-left my-auto">
                    <p class="barra-titulo" style="color:#2C3E50;">Plantilla de regalías</p>
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
                                                <button class="btn btn-outline-dark navbar-btn boton-nuevo boton-margen" id="BtnNuevo" runat="server" data-toggle="tooltip" data-placement="bottom" title="Limpia campos de ingreso"></button>
                                                <button class="btn btn-outline-dark navbar-btn boton-buscar boton-margen" id="BtnBuscar" runat="server" data-toggle="tooltip" data-placement="bottom" title="Busca registros de plantilla"></button>
                                                <button class="btn btn-outline-dark navbar-btn boton-grabar boton-margen" id="BtnGrabar" runat="server" data-toggle="tooltip" data-placement="bottom" title="Graba registro de plantilla"></button>
                                                <button class="btn btn-outline-dark navbar-btn boton-eliminar boton-margen" id="BtnEliminar" runat="server" data-toggle="tooltip" data-placement="bottom" title="Elimina registro de plantilla"></button>
                                                <button class="btn btn-outline-dark navbar-btn boton-cargaplantilla boton-margen" id="BtnCargar" runat="server" data-toggle="tooltip" data-placement="bottom" title="Subir archivo de plantilla"></button>
                                                <button class="btn btn-outline-dark navbar-btn boton-agregaplantilla boton-margen" id="BtnCargaPlantilla" runat="server" data-toggle="tooltip" data-placement="bottom" title="Grablar plantilla desde archivo"></button>
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
                                                        <label for="CodigoRegalia" class="col-form-label col-form-label-sm" style="font-weight:bold;">Codigo relagia</label>
                                                        <input type="text" class="form-control form-control-sm" id="CodigoRegalia" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Fecha" class="col-form-label col-form-label-sm" style="font-weight:bold;">Fecha</label>
                                                        <input type="text" class="form-control form-control-sm" id="Fecha" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Descripcion" class="col-form-label col-form-label-sm" style="font-weight:bold;">Descripcion</label>
                                                        <input type="text" class="form-control form-control-sm" id="Descripcion" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Valor_Fijo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Valor fijo</label>
                                                        <input type="text" class="form-control form-control-sm" id="Valor_Fijo" placeholder="" runat="server"/>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-3">
                                                        <label for="Valor_Proporcional" class="col-form-label col-form-label-sm" style="font-weight:bold;">Valor proporcional</label>
                                                        <input type="text" class="form-control form-control-sm" id="Valor_Proporcional" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Porcentaje" class="col-form-label col-form-label-sm" style="font-weight:bold;">Porcentaje</label>
                                                        <input type="text" class="form-control form-control-sm" id="Porcentaje" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Subtotal" class="col-form-label col-form-label-sm" style="font-weight:bold;">Subtotal</label>
                                                        <input type="text" class="form-control form-control-sm" id="Subtotal" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Tasa_Cambio" class="col-form-label col-form-label-sm" style="font-weight:bold;">Tasa cambio</label>
                                                        <input type="text" class="form-control form-control-sm" id="Tasa_Cambio" placeholder="" runat="server"/>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-3">
                                                        <label for="Total" class="col-form-label col-form-label-sm" style="font-weight:bold;">Fecha Total</label>
                                                        <input type="text" class="form-control form-control-sm" id="Total" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Adjuntos" class="col-form-label col-form-label-sm" style="font-weight:bold;">Adjuntos</label>
                                                        <input type="text" class="form-control form-control-sm" id="Adjuntos" placeholder="" runat="server"/>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="Cuenta" class="col-form-label col-form-label-sm" style="font-weight:bold;">Cuenta</label>
                                                        <input type="text" class="form-control form-control-sm" id="Cuenta" placeholder="" runat="server"/>
                                                    </div>
                                                </div>
                                                <div class="form-check">
                                                    <input class="form-check-input" type="checkbox" value="" id="chkEstado" runat="server"/>
                                                    <label class="form-check-label form-control-sm" for="chkEstado">El registro de la plantilla se encuentra activo</label>
                                                </div>
                                               <div class="form-group">
                                                   <label for="CargaArchivo" class="col-form-label col-form-label-sm" style="font-weight:bold;">Carga plantilla desde archivo</label>
                                                   <input type="file" class="form-control form-control-sm" id="CargaArchivo" placeholder="" runat="server"/>
                                               </div>
                                            </div>
                                        </ContentTemplate>
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
        <script src="../Scripts/PlantillaRegalias.js" type="text/javascript"></script>
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
