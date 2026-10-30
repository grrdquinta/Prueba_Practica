
@ModelType RegistroModel

@Code
    ViewData("Title") = "Crear cuenta"
    Layout = Nothing
End Code

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <title>Crear cuenta</title>

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
            padding: 24px 0;
        }

        .register-card {
            background: #262626;
            width: 380px;
            max-width: 92%;
            padding: 35px;
            border-radius: 12px;
            box-shadow: 0 8px 30px rgba(0,0,0,0.25);
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

        h2 {
            text-align: center;
            color: white;
            margin-bottom: 8px;
        }

        .subtitle {
            color: #AAAAAA;
            text-align: center;
            margin-bottom: 28px;
            font-size: 14px;
        }

        label {
            display: block;
            color: white;
            font-weight: bold;
            margin-bottom: 7px;
            font-size: 14px;
        }

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

        .password-requirements {
            display: none;
            margin-top: 12px;
            padding: 12px;
            background: #252B36;
            border-radius: 8px;
            font-size: 12px;
        }

        .password-requirements p {
            margin: 0 0 10px;
            color: #AAAAAA;
            font-weight: bold;
        }

        .requirement {
            display: flex;
            align-items: center;
            gap: 8px;
            margin-top: 7px;
            color: #FCA5A5;
            transition: color 0.2s ease;
        }

        .requirement::before {
            content: "✕";
            font-weight: bold;
        }

        .requirement.valid {
            color: #86EFAC;
        }

        .requirement.valid::before {
            content: "✓";
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
            color: white;
            outline: none;
            transition: border-color 0.2s ease;
        }

        .form-control:focus {
            background-color: #252B36;
            border-color: #747CA8;
            color: white;
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

        .btn-register {
            width: 100%;
            padding: 12px;
            background-color: #1a1e3b;
            color: white;
            border: none;
            border-radius: 16px;
            cursor: pointer;
            font-weight: bold;
        }

        .btn-register:hover {
            background-color: #2c325c;
        }

        .login-link {
            text-align: center;
            margin-top: 22px;
            color: #AAAAAA;
            font-size: 13px;
        }

        .login-link a {
            color: #A5B4FC;
            text-decoration: none;
            font-weight: bold;
        }

        .login-link a:hover {
            text-decoration: underline;
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
            font-weight: bold;
        }

        .field-validation-valid,
        .validation-summary-valid {
            display: none;
        }

        .form-control.input-validation-error {
            border-color: #C65B65;
        }

        .validation-summary-errors {
            background-color: #3B2529;
            color: #FCA5A5;
            padding: 12px;
            border-radius: 6px;
            font-size: 12px;
            margin-bottom: 18px;
        }

        .validation-summary-errors ul {
            margin: 0;
            padding-left: 18px;
        }
    </style>
</head>

<body>

    <div class="register-card">

        <div class="register-icon">
            <img src="@Url.Content("~/Content/Images/person-crop-circle-svgrepo-com.svg")"
                 alt="Ícono de usuario" />
        </div>

        <h2>Crear cuenta</h2>

        <p class="subtitle">
            Completa los datos para registrarte
        </p>

        @Using Html.BeginForm("Registrar", "Usuarios", FormMethod.Post)

            @Html.AntiForgeryToken()

            @<div class="form-group">
                @Html.LabelFor(Function(m) m.Usuario)
                @Html.TextBoxFor(
                    Function(m) m.Usuario,
                    New With {
                        .class = "form-control",
                        .placeholder = "Ingresa tu usuario",
                        .autocomplete = "username"
                    }
                )
                @Html.ValidationMessageFor(
                    Function(m) m.Usuario,
                    "",
                    New With {.class = "text-danger"}
                )
            </div>

            @<div class="form-group">
                @Html.LabelFor(Function(m) m.CorreoElectronico)
                @Html.TextBoxFor(
                    Function(m) m.CorreoElectronico,
                    New With {
                        .class = "form-control",
                        .placeholder = "correo@ejemplo.com",
                        .type = "email"
                    }
                )
                @Html.ValidationMessageFor(
                    Function(m) m.CorreoElectronico,
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
                             .autocomplete = "new-password",
                             .id = "Contrasena"
                         }
                     )

                     <button type="button"
                             class="toggle-password"
                             onclick="togglePassword('Contrasena', this)"
                             aria-label="Mostrar contraseña"
                             aria-pressed="false">

                         <svg xmlns="http://www.w3.org/2000/svg"
                              width="20" height="20"
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

                <div class="password-requirements">
                    <p>La contraseña debe contener:</p>

                    <div id="req-length" class="requirement">
                        Mínimo 8 caracteres
                    </div>

                    <div id="req-upper" class="requirement">
                        Una letra mayúscula
                    </div>

                    <div id="req-lower" class="requirement">
                        Una letra minúscula
                    </div>

                    <div id="req-number" class="requirement">
                        Un número
                    </div>

                    <div id="req-special" class="requirement">
                        Un carácter especial
                    </div>
                </div>
            </div>


            @<div class="form-group">
                @Html.LabelFor(Function(m) m.ConfirmarContrasena)
                 <div class="password-container">

                     @Html.PasswordFor(
                        Function(m) m.ConfirmarContrasena,
                        New With {
                            .class = "form-control",
                            .placeholder = "Confirma tu contraseña",
                            .autocomplete = "new-password",
                            .id = "ConfirmarContrasena"
                        }
                    )

                    <button type="button"
                            class="toggle-password"
                            onclick="togglePassword('ConfirmarContrasena', this)"
                            aria-label="Mostrar contraseña"
                            aria-pressed="false">

                        <svg xmlns="http://www.w3.org/2000/svg"
                             width="20" height="20"
                             viewBox="0 0 24 24"
                             fill="none"
                             stroke="currentColor"
                             stroke-width="2"
                             stroke-linecap="round"
                             stroke-linejoin="round">

                            <path d="M2 12s3.5-7 10-7 10 7 10 7-3.5 7-10 7S2 12 2 12z"/>
                            <circle cx="12" cy="12" r="3"/>

                        </svg>
                    </button>

                 </div>
                @Html.ValidationMessageFor(
                    Function(m) m.ConfirmarContrasena,
                    "",
                    New With {.class = "text-danger"}
                )
            </div>

            @<button type="submit" class="btn-register">
                Registrarse
            </button>

        End Using

        <div class="login-link">
            ¿Ya tienes una cuenta?
            <a href="@Url.Action("Login", "Login")">
                Iniciar sesión
            </a>
        </div>

    </div>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    @If TempData("ErrorRegistro") IsNot Nothing Then
        @<div id="alertaRegistro"
              data-mensaje="@TempData("ErrorRegistro")"
              style="display:none;"></div>
    End If

    @If TempData("MensajeRegistro") IsNot Nothing Then
        @<div id="alertaExito"
              data-mensaje="@TempData("MensajeRegistro")"
              style="display:none;"></div>
    End If

    <script>
        document.addEventListener("DOMContentLoaded", function () {

            var alerta = document.getElementById("alertaRegistro");

            if (alerta !== null) {

                Swal.fire({
                    icon: "error",
                    title: "Error al registrarse",
                    text: alerta.getAttribute("data-mensaje"),
                    confirmButtonText: "Entendido",
                    confirmButtonColor: "#2c325c",
                    background: "#262626",
                    color: "#FFFFFF"
                });

            }

        });
  
        document.addEventListener("DOMContentLoaded", function () {

            var alerta = document.getElementById("alertaExito");

            if (alerta !== null) {
                Swal.fire({
                    icon: "success",
                    title: "¡Registro exitoso!",
                    text: alerta.getAttribute("data-mensaje"),
                    confirmButtonText: "Iniciar sesión",
                    confirmButtonColor: "#2c325c",
                    background: "#262626",
                    color: "#FFFFFF",
                    allowOutsideClick: false
                });
            }

        });

        document.addEventListener("DOMContentLoaded", function () {

            const passwordInput = document.getElementById("Contrasena");
            const requirementsBox = document.querySelector(".password-requirements");

            if (!passwordInput || !requirementsBox) return;

            function validarContrasena() {

                const password = passwordInput.value;

                requirementsBox.style.display =
                    password.length > 0 ? "block" : "none";

                const requisitos = {
                    "req-length": password.length >= 8 && !/\s/.test(password),
                    "req-upper": /[A-Z]/.test(password),
                    "req-lower": /[a-z]/.test(password),
                    "req-number": /\d/.test(password),
                    "req-special": /[^a-zA-Z0-9\s]/.test(password)
                };

                Object.keys(requisitos).forEach(function (id) {

                    const elemento = document.getElementById(id);

                    if (elemento) {
                        elemento.classList.toggle("valid", requisitos[id]);
                    }

                });
            }

            passwordInput.addEventListener("input", validarContrasena);

            validarContrasena();

        });

        function togglePassword(inputId, boton) {

            const input = document.getElementById(inputId);

            if (!input) return;

            const mostrar = input.type === "password";

            input.type = mostrar ? "text" : "password";

            boton.setAttribute(
                "aria-label",
                mostrar ? "Ocultar contraseña" : "Mostrar contraseña"
            );

            boton.setAttribute("aria-pressed", mostrar.toString());

            boton.style.color = mostrar ? "#A5B4FC" : "#AAAAAA";

            const svg = boton.querySelector("svg");

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
