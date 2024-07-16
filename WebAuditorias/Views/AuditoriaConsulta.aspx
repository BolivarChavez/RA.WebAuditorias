<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuditoriaConsulta.aspx.cs" Inherits="WebAuditorias.Views.AuditoriaConsulta" %>

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
</head>
<body style="margin: 0; height: 100%; overflow: hidden; background-color: #E5E8E8;">
    <form id="form1" runat="server">
        <div class="container-fluid">
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
                <div class="col-sm-12 col-md-12 col-lg-12 col-xl-12 px-4 py-4 bg-white h-100 opcion-backcolor-2" style="overflow-y: scroll;">
                    <div class="container-fluid">
                        <h3>Procesos de Auditoría</h3>
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
                        <h3>Plantillas Registradas</h3>
                        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-1 border-bottom"></div>
                        <div class="row py-2">
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card border-primary mb-3" style="width: 100%;">
                                  <div class="card-header">Plantilla de Cheques</div>
                                  <div class="card-body text-primary">
                                    <h3 id="p1t" class="card-title" style="text-align: center;" runat="server">0</h3>
                                    <p class="card-text">Plantillas registradas en procesos de auditorías</p>
                                    <h3 id="p1r" class="card-title" style="text-align: center; color: #FFC107;" runat="server">0</h3>
                                    <p class="card-text" style="color: #FFC107;">Plantillas registradas y pedientes de revisión.</p>
                                    <a href="RevisaPlantillas.aspx?estado=P&plantilla=1" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card border-primary mb-3" style="width: 100%;">
                                  <div class="card-header">Plantilla de Comisiones</div>
                                  <div class="card-body text-primary">
                                    <h3 id="p2t" class="card-title" style="text-align: center;" runat="server">0</h3>
                                    <p class="card-text">Plantillas registradas en procesos de auditorías</p>
                                    <h3 id="p2r" class="card-title" style="text-align: center; color: #FFC107;" runat="server">0</h3>
                                    <p class="card-text" style="color: #FFC107;">Plantillas registradas y pedientes de revisión.</p>
                                    <a href="RevisaPlantillas.aspx?estado=P&plantilla=2" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card border-primary mb-3" style="width: 100%;">
                                  <div class="card-header">Plantilla de Ingresos</div>
                                  <div class="card-body text-primary">
                                    <h3 id="p3t" class="card-title" style="text-align: center;" runat="server">0</h3>
                                    <p class="card-text">Plantillas registradas en procesos de auditorías</p>
                                    <h3 id="p3r" class="card-title" style="text-align: center; color: #FFC107;" runat="server">0</h3>
                                    <p class="card-text" style="color: #FFC107;">Plantillas registradas y pedientes de revisión.</p>
                                    <a href="RevisaPlantillas.aspx?estado=P&plantilla=3" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card border-primary mb-3" style="width: 100%;">
                                  <div class="card-header">Plantilla de Mutuos</div>
                                  <div class="card-body text-primary">
                                    <h3 id="p4t" class="card-title" style="text-align: center;" runat="server">0</h3>
                                    <p class="card-text">Plantillas registradas en procesos de auditorías</p>
                                    <h3 id="p4r" class="card-title" style="text-align: center; color: #FFC107;" runat="server">0</h3>
                                    <p class="card-text" style="color: #FFC107;">Plantillas registradas y pedientes de revisión.</p>
                                    <a href="RevisaPlantillas.aspx?estado=P&plantilla=4" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                        </div>
                        <div class="row py-2">
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card border-primary mb-3" style="width: 100%;">
                                  <div class="card-header">Plantilla de Pagos</div>
                                  <div class="card-body text-primary">
                                    <h3 id="p5t" class="card-title" style="text-align: center;" runat="server">0</h3>
                                    <p class="card-text">Plantillas registradas en procesos de auditorías</p>
                                    <h3 id="p5r" class="card-title" style="text-align: center; color: #FFC107;" runat="server">0</h3>
                                    <p class="card-text" style="color: #FFC107;">Plantillas registradas y pedientes de revisión.</p>
                                    <a href="RevisaPlantillas.aspx?estado=P&plantilla=5" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card border-primary mb-3" style="width: 100%;">
                                  <div class="card-header">Plantilla de Planillas</div>
                                  <div class="card-body text-primary">
                                    <h3 id="p6t" class="card-title" style="text-align: center;" runat="server">0</h3>
                                    <p class="card-text">Plantillas registradas en procesos de auditorías</p>
                                    <h3 id="p6r" class="card-title" style="text-align: center; color: #FFC107;" runat="server">0</h3>
                                    <p class="card-text" style="color: #FFC107;">Plantillas registradas y pedientes de revisión.</p>
                                    <a href="RevisaPlantillas.aspx?estado=P&plantilla=6" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card border-primary mb-3" style="width: 100%;">
                                  <div class="card-header">Plantilla de Reembolsos</div>
                                  <div class="card-body text-primary">
                                    <h3 id="p7t" class="card-title" style="text-align: center;" runat="server">0</h3>
                                    <p class="card-text">Plantillas registradas en procesos de auditorías</p>
                                    <h3 id="p7r" class="card-title" style="text-align: center; color: #FFC107;" runat="server">0</h3>
                                    <p class="card-text" style="color: #FFC107;">Plantillas registradas y pedientes de revisión.</p>
                                    <a href="RevisaPlantillas.aspx?estado=P&plantilla=7" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card border-primary mb-3" style="width: 100%;">
                                  <div class="card-header">Plantilla de Regalias</div>
                                  <div class="card-body text-primary">
                                    <h3 id="p8t" class="card-title" style="text-align: center;" runat="server">0</h3>
                                    <p class="card-text">Plantillas registradas en procesos de auditorías</p>
                                    <h3 id="p8r" class="card-title" style="text-align: center; color: #FFC107;" runat="server">0</h3>
                                    <p class="card-text" style="color: #FFC107;">Plantillas registradas y pedientes de revisión.</p>
                                    <a href="RevisaPlantillas.aspx?estado=P&plantilla=8" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                        </div>
                        <div class="row py-2">
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card border-primary mb-3" style="width: 100%;">
                                  <div class="card-header">Plantilla de Regularizaciones</div>
                                  <div class="card-body text-primary">
                                    <h3 id="p9t" class="card-title" style="text-align: center;" runat="server">0</h3>
                                    <p class="card-text">Plantillas registradas en procesos de auditorías</p>
                                    <h3 id="p9r" class="card-title" style="text-align: center; color: #FFC107;" runat="server">0</h3>
                                    <p class="card-text" style="color: #FFC107;">Plantillas registradas y pedientes de revisión.</p>
                                    <a href="RevisaPlantillas.aspx?estado=P&plantilla=9" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card border-primary mb-3" style="width: 100%;">
                                  <div class="card-header">Plantilla de Transferencias</div>
                                  <div class="card-body text-primary">
                                    <h3 id="p10t" class="card-title" style="text-align: center;" runat="server">0</h3>
                                    <p class="card-text">Plantillas registradas en procesos de auditorías</p>
                                    <h3 id="p10r" class="card-title" style="text-align: center; color: #FFC107;" runat="server">0</h3>
                                    <p class="card-text" style="color: #FFC107;">Plantillas registradas y pedientes de revisión.</p>
                                    <a href="RevisaPlantillas.aspx?estado=P&plantilla=10" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card border-primary mb-3" style="width: 100%;">
                                  <div class="card-header">Plantilla de Tributos</div>
                                  <div class="card-body text-primary">
                                    <h3 id="p11t" class="card-title" style="text-align: center;" runat="server">0</h3>
                                    <p class="card-text">Plantillas registradas en procesos de auditorías</p>
                                    <h3 id="p11r" class="card-title" style="text-align: center; color: #FFC107;" runat="server">0</h3>
                                    <p class="card-text" style="color: #FFC107;">Plantillas registradas y pedientes de revisión.</p>
                                    <a href="RevisaPlantillas.aspx?estado=P&plantilla=11" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
