
@ModelType LoginModel

@Code
    ViewData("Title") = "Iniciar sesión"
    Layout = Nothing
End Code

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="utf-8" />
    <meta name="viewport"
          content="width=device-width, initial-scale=1" />

    <title>Iniciar sesión</title>

    <style>
        * {
            box-sizing: border-box;
        }

        body {
            margin: 0;
            font-family: Arial, sans-serif;
            background-color: #1B1B1B;
            min-height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .login-card {
            background: #262626;
            width: 380px;
            max-width: 92%;
            padding: 35px;
            border-radius: 12px;
            box-shadow: 0 8px 30px rgba(0,0,0,0.25);
        }

        .login-icon {
            display: flex;
            justify-content: center;
            align-items: center;
            margin-bottom: 18px;
        }

            .login-icon img {
                width: 75px;
                height: 75px;
                object-fit: contain;
            }

        h2 {
            text-align: center;
            margin-bottom: 8px;
            color: white;
        }

        .subtitle {
            color: #AAAAAA;
            text-align: center;
            margin-bottom: 28px;
        }

        label {
            display: block;
            margin-bottom: 7px;
            font-weight: bold;
            color: white;
        }

        .form-group {
            margin-bottom: 18px;
        }

        .form-control {
            width: 100%;
            padding: 12px;
            background-color: #252B36;
            border: 1px solid #252B36;
            border-radius: 6px;
            margin-bottom: 4px;
            color: white;
            outline: none;
            transition: border-color 0.2s ease;
        }

            .form-control:focus {
                background-color: #252B36;
                color: white;
                border-color: #747CA8;
                outline: none;
            }

            .form-control::placeholder {
                color: #AAAAAA;
            }

            .form-control:-webkit-autofill,
            .form-control:-webkit-autofill:focus {
                -webkit-text-fill-color: white;
                -webkit-box-shadow: 0 0 0 1000px #252B36 inset;
                caret-color: white;
            }

        /* Mostrar y ocultar contraseña */

        .password-container {
            position: relative;
            width: 100%;
        }

            .password-container .form-control {
                padding-right: 48px;
            }

        .toggle-password {
            position: absolute;
            right: 12px;
            top: 50%;
            transform: translateY(-50%);
            background: transparent;
            border: none;
            color: #AAAAAA;
            cursor: pointer;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 5px;
        }

            .toggle-password:hover {
                color: #A5B4FC;
            }

            .toggle-password svg {
                width: 20px;
                height: 20px;
            }

            .toggle-password:focus-visible {
                outline: 2px solid #A5B4FC;
                border-radius: 4px;
            }

        /* Botón de inicio de sesión */

        .btn-login {
            width: 100%;
            padding: 12px;
            background: #1a1e3b;
            color: white;
            border: none;
            border-radius: 16px;
            cursor: pointer;
            font-weight: bold;
            transition: background-color 0.2s ease;
        }

            .btn-login:hover {
                background: #2c325c;
            }

        /* Enlace de registro */

        .register-link {
            text-align: center;
            margin-top: 22px;
            color: #AAAAAA;
            font-size: 13px;
        }

            .register-link a {
                color: #A5B4FC;
                text-decoration: none;
                font-weight: bold;
            }

                .register-link a:hover {
                    text-decoration: underline;
                }

        /* Validaciones */

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
            margin-bottom: 15px;
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

            .validation-summary-errors li {
                margin: 0;
            }
    </style>
</head>

<body>

    <div class="login-card">

        <div class="login-icon">
            <img src="@Url.Content("~/Content/Images/person-crop-circle-svgrepo-com.svg")"
                 alt="Ícono de usuario" />
        </div>

        <h2>Bienvenido</h2>

        <p class="subtitle">
            Ingresa tus credenciales para continuar
        </p>

        @Html.ValidationSummary(
            True,
            "",
            New With {.class = "validation-summary-errors"}
        )

        @Using Html.BeginForm("Login", "Login", FormMethod.Post)

            @Html.AntiForgeryToken()

            @<div class="form-group">

                @Html.LabelFor(Function(m) m.Usuario_Correo)

                @Html.TextBoxFor(
                    Function(m) m.Usuario_Correo,
                    New With {
                        .class = "form-control",
                        .placeholder = "usuario o correo@ejemplo.com",
                        .autocomplete = "username"
                    }
                )

                @Html.ValidationMessageFor(
                    Function(m) m.Usuario_Correo,
                    "",
                    New With {.class = "text-danger"}
                )

            </div>

            @<div class="form-group">

                @Html.LabelFor(Function(m) m.Contrasena)

                <div class="password-container">

                    @Html.PasswordFor(
                        Function(m) m.Contrasena,
                        New With {
                            .class = "form-control",
                            .placeholder = "Ingresa tu contraseña",
                            .autocomplete = "current-password",
                            .id = "Contrasena"
                        }
                    )

                    <button type="button"
                            class="toggle-password"
                            onclick="togglePasswordLogin(this)"
                            aria-label="Mostrar contraseña"
                            aria-pressed="false">

                        <svg xmlns="http://www.w3.org/2000/svg"
                             width="20"
                             height="20"
                             viewBox="0 0 24 24"
                             fill="none"
                             stroke="currentColor"
                             stroke-width="2"
                             stroke-linecap="round"
                             stroke-linejoin="round">

                            <path d="M2 12s3.5-7 10-7 10 7 10 7-3.5 7-10 7S2 12 2 12z" />
                            <circle cx="12" cy="12" r="3" />

                        </svg>

                    </button>

                </div>

                @Html.ValidationMessageFor(
                    Function(m) m.Contrasena,
                    "",
                    New With {.class = "text-danger"}
                )

            </div>

            @<button type="submit" class="btn-login">
                Iniciar sesión
            </button>

        End Using

        <div class="register-link">
            ¿No tienes una cuenta?
            <a href="@Url.Action("Registrar", "Usuarios")">
                Crear una cuenta
            </a>
        </div>

    </div>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    @If TempData("MensajeRegistro") IsNot Nothing Then
        @<div id="alertaExito"
              data-mensaje="@TempData("MensajeRegistro")"
              style="display:none;"></div>
    End If

    @If TempData("ErrorLogin") IsNot Nothing Then
        @<div id="alertaErrorLogin"
              data-mensaje="@TempData("ErrorLogin")"
              style="display:none;"></div>
    End If

    <script>

                document.addEventListener("DOMContentLoaded", function () {

                    const alerta = document.getElementById("alertaExito");
                    const alertaError = document.getElementById("alertaErrorLogin");

                    if (alerta !== null) {

                        Swal.fire({
                            icon: "success",
                            title: "¡Registro exitoso!",
                            text: alerta.getAttribute("data-mensaje"),
                            confirmButtonText: "Iniciar sesión",
                            confirmButtonColor: "#2c325c",
                            background: "#262626",
                            color: "#FFFFFF",
                            allowOutsideClick: false,
                            allowEscapeKey: false
                        });

                    }

                    if (alertaError !== null) {

                        Swal.fire({
                            icon: "error",
                            title: "Error al iniciar sesión",
                            text: alertaError.getAttribute("data-mensaje"),
                            confirmButtonText: "Entendido",
                            confirmButtonColor: "#2c325c",
                            background: "#262626",
                            color: "#FFFFFF"
                        });

                    }

                });

                function togglePasswordLogin(boton) {

                    const input = document.getElementById("Contrasena");

                    if (!input) return;

                    const mostrar = input.type === "password";

                    input.type = mostrar ? "text" : "password";

                    boton.setAttribute(
                        "aria-label",
                        mostrar ? "Ocultar contraseña" : "Mostrar contraseña"
                    );

                    boton.setAttribute(
                        "aria-pressed",
                        mostrar.toString()
                    );

                    boton.style.color = mostrar ? "#A5B4FC" : "#AAAAAA";

                    const svg = boton.querySelector("svg");

                    if (!svg) return;

                    if (mostrar) {

                        svg.innerHTML = `
                            <path d="M3 3l18 18"/>
                            <path d="M10.6 5.1A11 11 0 0 1 12 5c6.5 0 10 7 10 7a15 15 0 0 1-4 4.6"/>
                            <path d="M6.5 6.5C3.5 8.5 2 12 2 12s3.5 7 10 7a10 10 0 0 0 4.2-.9"/>
                            <path d="M10 10a3 3 0 0 0 4 4"/>
                        `;

                    } else {

                        svg.innerHTML = `
                            <path d="M2 12s3.5-7 10-7 10 7 10 7-3.5 7-10 7S2 12 2 12z"/>
                            <circle cx="12" cy="12" r="3"/>
                        `;

                    }

                }

    </script>

</body>
</html>
