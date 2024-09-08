<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuditoriaConsultaOld.aspx.cs" Inherits="WebAuditorias.Views.AuditoriaConsulta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Consulta General de Procesos de Auditoría</title>
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
    <script src="../Scripts/AuditoriaConsulta.js" type="text/javascript"></script>
</head>
<body style="margin: 0; height: 100%; overflow: hidden; background-color: #E5E8E8;">
    <div class="container-fluid">
    <form id="form1" runat="server">
            <div class="row header-bar-height" style="background-color: #E5E8E8;">
                <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 my-auto">
                    <img class="image-logo-empresa" src="../Images/LogoRomeroyAsociados.png" />
                </div>    
                <div class="col-sm-12 col-md-8 col-lg-8 col-xl-8 center-block text-left my-auto">
                    <p class="barra-titulo" style="color:#2C3E50;">Consulta General de Procesos de Auditoría</p>
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
                <div class="col-sm-12 col-md-12 col-lg-12 col-xl-12 px-4 py-0 bg-white h-100 opcion-backcolor-2" style="overflow-y: scroll;">
                    <nav class="navbar navbar-expand-lg">
                        <button class="btn btn-outline-dark navbar-btn boton-buscar boton-margen" id="BtnBuscar" runat="server" onserverclick="BtnBuscar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Consultar información"></button>
                    </nav>
                    <div class="row py-2">
                        <div class="form-group col-md-2">
                            <label for="Anio" class="col-form-label col-form-label-sm" style="font-weight:bold;">Año</label>
                            <input type="number" class="form-control form-control-sm" style="width: 50%; font-size: 12px" id="Anio" placeholder="0" runat="server"/>
                        </div>
                        <div class="form-group col-md-2">
                            <label for="Proceso" class="col-form-label col-form-label-sm" style="font-weight:bold;">Proceso de auditoría</label>
                            <asp:DropDownList ID="Proceso" CssClass="form-select form-select-sm" style="width: 100%; font-size: 12px" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="container-fluid" id="DetalleInfo" runat="server">
                        <h4>Procesos de Auditoría</h4>
                        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-1 border-bottom"></div>
                        <div class="row py-2">
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card border-primary mb-3" style="width: 100%;">
                                  <div class="card-header">Procesos Abiertos</div>
                                  <div class="card-body text-primary">
                                    <h1 id="numPA" class="card-title" style="text-align: center;" runat="server">0</h1>
                                    <p class="card-text">Procesos de auditoría que se han registrado hasta el momento pero que aún no están en proceso.</p>
                                    <a href="RevisaProcesos.aspx?estado=A" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card border-success mb-3" style="width: 100%;">
                                  <div class="card-header">Procesos Cerrados</div>
                                  <div class="card-body text-success">
                                    <h1 id="numPC" class="card-title" style="text-align: center;" runat="server">0</h1>
                                    <p class="card-text">Procesos de auditoría que se han registrado y que se han cerrado hasta el momento.</p>
                                    <a href="RevisaProcesos.aspx?estado=C" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card border-warning mb-3" style="width: 100%;">
                                  <div class="card-header">Proceso No Cerrados</div>
                                  <div class="card-body text-warning">
                                    <h1 id="numPP" class="card-title" style="text-align: center;" runat="server">0</h1>
                                    <p class="card-text">Procesos de auditoría que aun se mantienen vigentes y que no se han cerrado.</p>
                                    <a href="RevisaProcesos.aspx?estado=P" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                        </div>
                        <h4>Plantillas Registradas</h4>
                        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-1 border-bottom"></div>
                        <div id="GridConsulta" class="content-wrapper py-2" style="width:100%">
                            <script id="template1" type="text/x-template">
                                <div>
                                    <a href="RevisaPlantillas.aspx?estado=P&plantilla=${IdPlantilla}" style="color:#0D6EFD;" target="_blank"  >
                                    ${Registradas}
                                    </a>
                                </div>
                            </script>
                            <script id="template2" type="text/x-template">
                                <div>
                                    <a href="RevisaPlantillas.aspx?estado=P&plantilla=${IdPlantilla}" style="color:#FFC107;" target="_blank"  >
                                    ${Pendientes}
                                    </a>
                                </div>
                            </script>
                            <div id="Grid">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </form>
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
