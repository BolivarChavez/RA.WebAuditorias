<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InformeTareas.aspx.cs" Inherits="WebAuditorias.Views.InformeTareas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Informe de actividades de seguimiento de tareas de auditoría</title>
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:ital,wght@0,300..800;1,300..800&display=swap" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Styles/Custom-Opciones.css" />
    <link rel="stylesheet" href="../Styles/Custom-Toolbar.css" />
    <link rel="stylesheet" href="../Styles/material.css" />
    <link rel="stylesheet" href="../Styles/Custom-Grid.css" />
    <link rel="stylesheet" href="../Styles/Custom-Grid-Font.css" />
    <link rel="stylesheet" href="../Styles/Custom-Collapse.css" />
    <script src="../Scripts/ej2.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.1.3/js/bootstrap.min.js"></script>
</head>
<body style="margin: 0; height: 100%; overflow: hidden; background-color: #E5E8E8;">
    <div class="container-fluid">
    <form id="form1" runat="server">
            <div class="row header-bar-height" style="background-color: #E5E8E8;">
                <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 my-auto">
                    <img class="image-logo-empresa" src="../Images/LogoRomeroyAsociados.png" />
                </div>    
                <div class="col-sm-12 col-md-8 col-lg-8 col-xl-8 center-block text-left my-auto">
                    <p class="barra-titulo" style="color:#2C3E50;">Informe de actividades de seguimiento de tareas de auditoría</p>
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
                    <div class="container-fluid" id="DetalleInfo" runat="server">
                        <br />
                        <h4 id="Auditoria" runat="server">Descripcion de auditoria</h4>
                        <h4 id="Proceso" runat="server">Proceso de auditoria</h4>
                        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-1 border-bottom"></div>
                        <div id="DetalleConsulta" class="content-wrapper py-2" style="width:100%" runat="server">
                        </div>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
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
