<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuditoriaConsulta.aspx.cs" Inherits="WebAuditorias.Views.AuditoriaConsultaNew" %>

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
                <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 py-0 h-100 opcion-backcolor-1" style="background-color: #B7BABA;">
                    <nav class="navbar navbar-expand-lg">
                        <button class="btn btn-outline-dark navbar-btn boton-buscar boton-margen" id="BtnBuscar" runat="server" onserverclick="BtnBuscar_ServerClick" data-toggle="tooltip" data-placement="bottom" title="Consultar información"></button>
                    </nav>
                    <div class="row py-1">
                        <div class="form-group">
                            <label for="Anio" class="col-form-label col-form-label-sm" style="font-weight:bold;">Año</label>
                            <input type="number" class="form-control form-control-sm" style="width: 50%; font-size: 12px" id="Anio" placeholder="0" runat="server"/>
                        </div>
                    </div>
                    <div class="row py-1">
                        <div class="form-group">
                            <label for="Proceso" class="col-form-label col-form-label-sm" style="font-weight:bold;">Proceso de auditoría</label>
                            <asp:DropDownList ID="Proceso" CssClass="form-select form-select-sm" style="width: 100%; font-size: 12px" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12 col-md-10 col-lg-10 col-xl-10 px-0 py-0 bg-white h-100 opcion-backcolor-2" style="overflow-y: scroll;">
                    <div class="container-fluid" id="DetalleInfo" runat="server">
                        <div class="row py-2">
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card text-left border-primary rounded-2 shadow mt-5" style="width: 100%;">
                                    <div class="card-icon position-absolute start-50 translate-middle text-light rounded-circle p-3 bg-primary">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="48px" height="48px" fill="white" class="bi bi-code-slash"
                                            viewBox="0 -960 960 960">
                                            <path
                                                d="M270.74-426.67q22.26 0 37.76-15.58 15.5-15.57 15.5-37.83 0-22.25-15.58-37.75t-37.83-15.5q-22.26 0-37.76 15.58-15.5 15.57-15.5 37.83 0 22.25 15.58 37.75t37.83 15.5Zm209.34 0q22.25 0 37.75-15.58 15.5-15.57 15.5-37.83 0-22.25-15.58-37.75-15.57-15.5-37.83-15.5-22.25 0-37.75 15.58-15.5 15.57-15.5 37.83 0 22.25 15.58 37.75 15.57 15.5 37.83 15.5Zm208.67 0q22.25 0 37.75-15.58 15.5-15.57 15.5-37.83 0-22.25-15.58-37.75t-37.83-15.5q-22.26 0-37.76 15.58-15.5 15.57-15.5 37.83 0 22.25 15.58 37.75t37.84 15.5ZM480.18-80q-82.83 0-155.67-31.5-72.84-31.5-127.18-85.83Q143-251.67 111.5-324.56T80-480.33q0-82.88 31.5-155.78Q143-709 197.33-763q54.34-54 127.23-85.5T480.33-880q82.88 0 155.78 31.5Q709-817 763-763t85.5 127Q880-563 880-480.18q0 82.83-31.5 155.67Q817-251.67 763-197.46q-54 54.21-127 85.84Q563-80 480.18-80Zm.15-66.67q139 0 236-97.33t97-236.33q0-139-96.87-236-96.88-97-236.46-97-138.67 0-236 96.87-97.33 96.88-97.33 236.46 0 138.67 97.33 236 97.33 97.33 236.33 97.33ZM480-480Z" />
                                        </svg>
                                    </div>
                                    <div class="card-body text-primary mt-4">
                                        <h5 class="card-title pt-2 fw-bold" style="text-align:center">Auditorías Abiertas</h5>
                                        <h2 id="numPA" class="card-text  py-0" style="text-align:center" runat="server">20</h2>
                                        <p class="card-text  py-0">Procesos de auditoría que se han registrado hasta el momento pero que aún no están en proceso.</p>
                                    </div>
                                    <div class="card-footer">
                                        <a href="RevisaProcesos.aspx?estado=A" target="_blank" class="text-primary fw-bold d-block py-0 text-center">Ver detalles</a>
                                    </div>
                                </div> 
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card text-left border-success rounded-2 shadow mt-5" style="width: 100%;">
                                    <div class="card-icon position-absolute start-50 translate-middle text-light rounded-circle p-3 bg-success">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="48px" height="48px" fill="white" class="bi bi-code-slash"
                                            viewBox="0 -960 960 960">
                                            <path
                                                d="M422-297.33 704.67-580l-49.34-48.67L422-395.33l-118-118-48.67 48.66L422-297.33ZM480-80q-82.33 0-155.33-31.5-73-31.5-127.34-85.83Q143-251.67 111.5-324.67T80-480q0-83 31.5-156t85.83-127q54.34-54 127.34-85.5T480-880q83 0 156 31.5T763-763q54 54 85.5 127T880-480q0 82.33-31.5 155.33-31.5 73-85.5 127.34Q709-143 636-111.5T480-80Zm0-66.67q139.33 0 236.33-97.33t97-236q0-139.33-97-236.33t-236.33-97q-138.67 0-236 97-97.33 97-97.33 236.33 0 138.67 97.33 236 97.33 97.33 236 97.33ZM480-480Z" />
                                        </svg>
                                    </div>
                                    <div class="card-body text-success mt-4">
                                        <h5 class="card-title pt-2 fw-bold" style="text-align:center">Auditorías Cerradas</h5>
                                        <h2 id="numPC" class="card-text  py-0" style="text-align:center" runat="server">20</h2>
                                        <p class="card-text  py-0">Procesos de auditoría que se han registrado y que se han cerrado hasta el momento.</p>
                                    </div>
                                    <div class="card-footer">
                                        <a href="RevisaProcesos.aspx?estado=C" target="_blank" class="text-success fw-bold d-block py-0 text-center">Ver detalles</a>
                                    </div>
                                </div>  
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card text-left border-warning rounded-2 shadow mt-5" style="width: 100%;">
                                    <div class="card-icon position-absolute start-50 translate-middle text-light rounded-circle p-3 bg-warning">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="48px" height="48px" fill="white" class="bi bi-code-slash"
                                            viewBox="0 -960 960 960">
                                            <path
                                                d="M481.33-158.67q-132 0-226.33-94-94.33-94-94.33-226V-511L86-436.33 41.33-481 194-633.67 346.67-481 302-436.33 227.33-511v32.33q0 104.67 74.5 179 74.5 74.34 179.5 74.34 28 0 53.67-5.34 25.67-5.33 49-16L632.67-198q-36.67 20.67-74.34 30-37.66 9.33-77 9.33ZM766.67-325 614-477.67 659.33-523l74 74v-29.67q0-104.66-74.5-179Q584.33-732 479.33-732q-28 0-53.66 5.67-25.67 5.66-49 15L328-760q36.67-20.67 74.33-29.67 37.67-9 77-9 132 0 226.34 94 94.33 94 94.33 226v31l74.67-74.66 44.66 44.66L766.67-325Z" />
                                        </svg>
                                    </div>
                                    <div class="card-body text-warning mt-4">
                                        <h5 class="card-title pt-2 fw-bold" style="text-align:center">Auditorías En Proceso</h5>
                                        <h2 id="numPP" class="card-text  py-0" style="text-align:center" runat="server">20</h2>
                                        <p class="card-text  py-0">Procesos de auditoría que aun se mantienen vigentes y que no se han cerrado.</p>
                                    </div>
                                    <div class="card-footer">
                                        <a href="RevisaProcesos.aspx?estado=P" target="_blank" class="text-warning fw-bold d-block py-0 text-center">Ver detalles</a>
                                    </div>
                                </div> 
                            </div>
                        </div>
                        <div class="row py-2">
                            <div class="col-sm-12 col-md-9 col-lg-9 col-xl-9 d-flex align-items-stretch">
                                <div class="card text-left border-primary rounded-2 shadow" style="width: 100%;">
                                    <div class="card-header text-primary">
                                        <b>Plantillas Registradas</b>
                                    </div>
                                    <div id="TablaPlantilla" class="card-body" runat="server">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
