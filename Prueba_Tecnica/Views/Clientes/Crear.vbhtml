
@ModelType ClienteModel

@Code
    ViewData("Title") = "Nuevo cliente"
    Layout = Nothing
End Code

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Nuevo cliente</title>

    <style>
        * {
            box-sizing: border-box;
        }

        body {
            margin: 0;
            min-height: 100vh;
            background: #1B1B1B;
            color: white;
            font-family: Arial, sans-serif;
            display: flex;
            justify-content: center;
            align-items: center;
            padding: 24px;
        }

        .form-card {
            background: #262626;
            width: 460px;
            max-width: 100%;
            padding: 35px;
            border-radius: 12px;
        }

        h2 {
            text-align: center;
            color: white;
            margin: 0 0 8px;
            font-size: 24px;
        }

        .subtitle {
            color: #AAAAAA;
            text-align: center;
            margin-bottom: 28px;
            font-size: 14px;
        }

        .form-group {
            margin-bottom: 18px;
        }

        label {
            display: block;
            margin-bottom: 8px;
            font-weight: bold;
            font-size: 14px;
        }

        .form-control {
            width: 100%;
            padding: 12px;
            background: #252B36;
            border: 1px solid #40475A;
            border-radius: 6px;
            color: white;
            outline: none;
        }

        .form-control:focus {
            border-color: #A5B4FC;
        }

        .field-validation-error {
            display: flex;
            align-items: center;
            gap: 8px;
            background-color: #3B2529;
            color: #FCA5A5;
            padding: 10px 12px;
            border-radius: 6px;
            font-size: 12px;
            margin-top: 7px;
            line-height: 1.4;
        }

        .field-validation-error::before {
            content: "!";
            display: flex;
            align-items: center;
            justify-content: center;
            flex-shrink: 0;
            width: 16px;
            height: 16px;
            border: 1px solid #FCA5A5;
            border-radius: 50%;
            font-size: 11px;
            font-weight: bold;
        }

        .field-validation-valid,
        .validation-summary-valid {
            display: none;
        }

        .form-control.input-validation-error {
            border: 1px solid #C65B65;
        }

        .form-control.input-validation-error:focus {
            background-color: #252B36;
            border-color: #FCA5A5;
            box-shadow: 0 0 0 2px rgba(252, 165, 165, 0.10);
        }

        .validation-summary-errors {
            background-color: #3B2529;
            color: #FCA5A5;
            border: 1px solid #8B454D;
            padding: 12px 14px;
            border-radius: 6px;
            font-size: 12px;
            margin-bottom: 18px;
            line-height: 1.5;
        }

        .validation-summary-errors ul {
            margin: 0;
            padding-left: 18px;
        }

        .buttons {
            display: flex;
            gap: 12px;
            margin-top: 25px;
        }

        .btn {
            flex: 1;
            padding: 12px;
            border-radius: 8px;
            text-align: center;
            text-decoration: none;
            font-weight: bold;
            font-size: 14px;
            cursor: pointer;
            border: none;
        }

        .btn-save {
            background: #2c325c;
            color: white;
        }

        .btn-save:hover {
            background: #3b447b;
        }

        .btn-cancel {
            background: #383838;
            color: white;
        }

        .btn-cancel:hover {
            background: #484848;
        }

        .register-icon {
            display: flex;
            justify-content: center;
            margin-bottom: 18px;
        }

        .register-icon img {
            width: 75px;
            height: 75px;
            object-fit: contain;
        }
    </style>
</head>

<body>
    <div class="form-card">

        <div class="register-icon">
            <img src="@Url.Content("~/Content/Images/person-crop-circle-svgrepo-com.svg")"
                 alt="Ícono de usuario" />
        </div>

        <h2>Nuevo cliente</h2>
        <p class="subtitle">Completa los datos del cliente</p>

        @Html.ValidationSummary(True, "", New With {.class = "validation-summary-errors"})
        @Using Html.BeginForm("Crear", "Clientes", FormMethod.Post)

            @Html.AntiForgeryToken()

            @<div class="form-group">
                @Html.LabelFor(Function(m) m.Nombre)
                @Html.TextBoxFor(Function(m) m.Nombre,
                    New With {.class = "form-control"})
                @Html.ValidationMessageFor(Function(m) m.Nombre)
            </div>

            @<div class="form-group">
                @Html.LabelFor(Function(m) m.Apellido)
                @Html.TextBoxFor(Function(m) m.Apellido,
                    New With {.class = "form-control"})
                @Html.ValidationMessageFor(Function(m) m.Apellido)
            </div>

            @<div class="form-group">
                @Html.LabelFor(Function(m) m.Correo)
                @Html.TextBoxFor(Function(m) m.Correo,
                    New With {.class = "form-control", .type = "email"})
                @Html.ValidationMessageFor(Function(m) m.Correo)
            </div>

            @<div class="form-group">
                @Html.LabelFor(Function(m) m.Direccion)
                @Html.TextBoxFor(Function(m) m.Direccion,
                    New With {.class = "form-control"})
                @Html.ValidationMessageFor(Function(m) m.Direccion)
            </div>

            @<div class="form-group">
                @Html.LabelFor(Function(m) m.Telefono)

                @Html.TextBoxFor(
                    Function(m) m.Telefono,
                    New With {
                        .class = "form-control",
                        .type = "tel",
                        .placeholder = "0000-0000",
                        .maxlength = "9",
                        .inputmode = "numeric",
                        .autocomplete = "tel",
                        .oninput = "formatearTelefonoCrear(this)"
                    }
                )

                @Html.ValidationMessageFor(Function(m) m.Telefono)
            </div>

            @<div class="buttons">
                <a href="@Url.Action("Index", "Clientes")"
                   class="btn btn-cancel">Cancelar</a>

                <button type="submit" class="btn btn-save">
                    Guardar cliente
                </button>
            </div>

        End Using

    </div>

   <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    @If TempData("ErrorClientes") IsNot Nothing Then
        @<div id="alertaErrorCliente"
              data-mensaje="@TempData("ErrorClientes")"
              style="display:none;"></div>
    End If

    <script>
        function formatearTelefonoCrear(input) {

            let numero = input.value.replace(/\D/g, "");

            numero = numero.substring(0, 8);

            if (numero.length > 4) {
                numero = numero.substring(0, 4) + "-" +
                         numero.substring(4);
            }

            input.value = numero;
        }

        document.addEventListener("DOMContentLoaded", function () {

            const alerta = document.getElementById("alertaErrorCliente");

            if (alerta !== null) {

                Swal.fire({
                    icon: "error",
                    title: "No se pudo registrar",
                    text: alerta.getAttribute("data-mensaje"),
                    confirmButtonText: "Entendido",
                    confirmButtonColor: "#2c325c",
                    background: "#262626",
                    color: "#FFFFFF",
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });

            }

        });
    </script>
</body>
</html>
