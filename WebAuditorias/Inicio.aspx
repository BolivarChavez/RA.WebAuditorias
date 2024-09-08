<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="WebAuditorias.Inicio" Debug="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Sistema de Control de Auditorias - Romero y Asociados</title>
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:ital,wght@0,300..800;1,300..800&display=swap" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Styles/font-awesome.min.css" />
    <link rel="stylesheet" href="../Styles/Custom-Inicio.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script type="text/javascript">
        function noBack() { window.history.forward() }
        noBack();
        window.onload = noBack;
        window.onpageshow = function (evt) { if (evt.persisted) noBack() }
        window.onunload = function () { void (0) }

        function validaLogin(respuesta, mensaje, pagina) {
            if (respuesta === "1") {
                Swal.fire({
                    title: "Inicio de Sesión",
                    text: mensaje,
                    icon: "success",
                    confirmButtonColor: "#3085d6",
                    confirmButtonText: "Acceder"
                }).then(function () {
                    window.location = pagina;
                });
            }
            else {
                Swal.fire({
                    title: "Inicio de Sesión",
                    text: mensaje,
                    icon: "error",
                    confirmButtonColor: "#3085d6",
                    confirmButtonText: "Ingresar credenciales nuevamente"
                }).then(function () {
                    window.location = pagina;
                });
            }
        }
    </script>
</head>
<body>
    <div class="container-fluid vh-100">
        <div class="row h-100">
            <div class="col-sm-12 col-md-9 col-lg-9 col-xl-9 py-4 bg-white h-100 company-image">
            </div>
            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 py-4 h-100 company-backcolor">
                <div class="container">
                    <h1 class="letter-format-simple" style="color:#2C3E50;">Estudio Jurídico Romero y Asociados</h1>
                    <h2 class="letter-format-simple" style="color:#2C3E50;">Sistema de Control de Auditorías</h2>
                    <br />

                    <form id="form1" runat="server">
                        <div class="mb-3">
                            <label class="form-label letter-format-bold" style="color:#2C3E50;">Usuario</label>
                            <div class="input-container">
                                <span class="fa fa-user"></span>
                                <input id="UserId" placeholder="Usuario" name="usuario" type="text" required="" runat="server"/>
                            </div>
                        </div>
                        <div class="mb-3">
                            <label class="form-label letter-format-bold" style="color:#2C3E50;">Contraseña</label>
                            <div class="input-container">
                                <span class="fa fa-key"></span>
                                <input id="UserPassword" placeholder="Contraseña" name="password" type="password" required="" runat="server"/>
                            </div>
                        </div>
                        <div class="d-grid gap-2">
                            <button class="btn btn-warning" id="BtnLogin" name="login" runat="server"  type="button" onserverclick="BtnLogin_ServerClick">Acceder</button>
                        </div>

                        <asp:HiddenField ID="HiddenField1" runat="server" />
                    </form>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var input = document.getElementById("UserPassword");
        input.addEventListener("keypress", function (event) {
            if (event.key === "Enter") {
                event.preventDefault();
                document.getElementById("BtnLogin").click();
            }
        });
    </script>    
</body>
</html>
