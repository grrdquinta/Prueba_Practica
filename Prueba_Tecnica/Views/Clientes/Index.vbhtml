
@ModelType IEnumerable(Of ClienteModel)

@Code
    ViewData("Title") = "Clientes"
    Layout = Nothing
End Code

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <title>Gestión de clientes</title>

    <style>
                * {
                    box-sizing: border-box;
                }

                body {
                    margin: 0;
                    font-family: Arial, sans-serif;
                    background: #1B1B1B;
                    color: white;
                    padding: 40px;
                }

                .container {
                    max-width: 1200px;
                    margin: auto;
                    background: #262626;
                    border-radius: 12px;
                    padding: 30px;
                }

                .header {
                    display: flex;
                    justify-content: space-between;
                    align-items: center;
                    gap: 20px;
                    margin-bottom: 25px;
                }

                h2 {
                    margin: 0;
                }

                .header-actions {
                    display: flex;
                    align-items: center;
                    gap: 12px;
                }

                .btn-primary {
                    display: inline-flex;
                    align-items: center;
                    justify-content: center;
                    gap: 10px;
                    background: #2c325c;
                    color: white;
                    padding: 12px 18px;
                    border-radius: 8px;
                    text-decoration: none;
                    font-weight: bold;
                    white-space: nowrap;
                    transition: background-color 0.2s ease;
                }

                .btn-primary:hover {
                    background: #3b447b;
                }

                .btn-icon {
                    width: 20px;
                    height: 20px;
                    object-fit: contain;
                }

                .logout-form {
                    margin: 0;
                }

                .btn-logout {
                    display: inline-flex;
                    align-items: center;
                    justify-content: center;
                    gap: 10px;
                    background: #383838;
                    color: white;
                    padding: 12px 18px;
                    border: 1px solid #505050;
                    border-radius: 8px;
                    font-family: Arial, sans-serif;
                    font-size: 14px;
                    font-weight: bold;
                    cursor: pointer;
                    white-space: nowrap;
                    transition: background-color 0.2s ease;
                }

                .btn-logout:hover {
                    background: #484848;
                }

                .btn-logout svg {
                    width: 18px;
                    height: 18px;
                    flex-shrink: 0;
                }

                .action-container {
                    display: flex;
                    align-items: center;
                    justify-content: flex-start;
                    gap: 10px;
                }

                .action-buttons {
                    display: inline-flex;
                    align-items: center;
                    justify-content: center;
                    width: 36px;
                    height: 36px;
                    border-radius: 8px;
                    background-color: #303030;
                    border: 1px solid #404040;
                    text-decoration: none;
                    cursor: pointer;
                    transition: all 0.2s ease;
                }

                .action-buttons img {
                    width: 18px;
                    height: 18px;
                    object-fit: contain;
                }

                .btn-edit {
                    background-color: #252B40;
                    border-color: #39456D;
                }

                .btn-edit:hover {
                    background-color: #354674;
                    border-color: #6478B5;
                    transform: translateY(-2px);
                }

                .btn-delete {
                    background-color: #3B2529;
                    border-color: #63363D;
                }

                .btn-delete:hover {
                    background-color: #633039;
                    border-color: #C65B65;
                    transform: translateY(-2px);
                }

                .action-buttons:focus-visible,
                .btn-logout:focus-visible {
                    outline: 2px solid #A5B4FC;
                    outline-offset: 3px;
                }

                .table-container {
                    overflow-x: auto;
                }

                table {
                    width: 100%;
                    border-collapse: collapse;
                }

                th, td {
                    padding: 14px;
                    text-align: left;
                    border-bottom: 1px solid #404040;
                }

                th {
                    background: #252B36;
                }

                tr:hover {
                    background: #303030;
                }

                .empty {
                    text-align: center;
                    color: #AAAAAA;
                    padding: 25px;
                }

                .form-eliminar {
                    display: inline-flex;
                    margin: 0;
                }

                @@media (max-width: 900px) {
            .header {
                flex-direction: column;
                align-items: flex-start;
            }

            .header-actions {
                flex-wrap: wrap;
            }
        }

        @@media (max-width: 600px) {
            body {
                padding: 15px;
            }

            .container {
                padding: 20px;
            }

            .header-actions {
                width: 100%;
                flex-direction: column;
                align-items: stretch;
            }

            .btn-primary,
            .btn-logout {
                width: 100%;
            }
        }
    </style>
</head>

<body>

    <div class="container">

        <div class="header">

            <h2>Gestión de clientes</h2>

            <div class="header-actions">

                <a href="@Url.Action("Crear", "Clientes")"
                   class="btn-primary">

                    <img src="@Url.Content("~/Content/Images/person-crop-circle-badge-plus-svgrepo-com.svg")"
                         alt="Nuevo cliente"
                         class="btn-icon" />

                    Nuevo cliente
                </a>

                <a href="@Url.Action("Bitacora", "Bitacora")"
                   class="btn-primary">

                    <img src="@Url.Content("~/Content/Images/book-open-svgrepo-com.svg")"
                         alt="Bitácora"
                         class="btn-icon" />

                    Ver bitácora
                </a>

                @Using Html.BeginForm(
                    "Logout",
                    "Login",
                    FormMethod.Post,
                    New With {
                        .id = "formLogout",
                        .class = "logout-form"
                    }
                )

                    @Html.AntiForgeryToken()

                    @<button type="submit"
                             class="btn-logout"
                             title="Cerrar sesión">

                        <svg xmlns="http://www.w3.org/2000/svg"
                             viewBox="0 0 24 24"
                             fill="none"
                             stroke="currentColor"
                             stroke-width="2"
                             stroke-linecap="round"
                             stroke-linejoin="round">

                            <path d="M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4" />
                            <polyline points="16 17 21 12 16 7" />
                            <line x1="21" y1="12" x2="9" y2="12" />
                        </svg>

                        Cerrar sesión
                    </button>

                End Using

            </div>

        </div>

        <div class="table-container">

            <table>
                <thead>
                    <tr>
                        <th>Nombre</th>
                        <th>Apellido</th>
                        <th>Correo electrónico</th>
                        <th>Dirección</th>
                        <th>Teléfono</th>
                        <th>Acciones</th>
                    </tr>
                </thead>

                <tbody>

                    @For Each cliente In Model
                        @<tr>

                            <td>@cliente.Nombre</td>
                            <td>@cliente.Apellido</td>
                            <td>@cliente.Correo</td>
                            <td>@cliente.Direccion</td>
                            <td>@cliente.Telefono</td>

                            <td>
                                <div class="action-container">

                                    <a href="@Url.Action("Editar", "Clientes", New With {.id = cliente.IdCliente})"
                                       class="action-buttons btn-edit"
                                       title="Editar cliente"
                                       aria-label="Editar cliente">

                                        <img src="@Url.Content("~/Content/Images/edit-svgrepo-com.svg")"
                                             alt="Editar" />
                                    </a>

                                    @Using Html.BeginForm(
                                        "Eliminar",
                                        "Clientes",
                                        FormMethod.Post,
                                        New With {.class = "form-eliminar"}
                                    )

                                        @Html.AntiForgeryToken()

                                        @<input type="hidden"
                                                name="id"
                                                value="@cliente.IdCliente" />

                                        @<button type="button"
                                                 class="action-buttons btn-delete"
                                                 onclick="confirmarEliminar(this)"
                                                 title="Eliminar cliente">

                                            <img src="@Url.Content("~/Content/Images/trash-svgrepo-com.svg")"
                                                 alt="Eliminar" />

                                        </button>

                                    End Using

                                </div>
                            </td>

                        </tr>
                    Next

                </tbody>
            </table>

        </div>

        @If Not Model.Any() Then
            @<div class="empty">
                No hay clientes registrados.
            </div>
        End If

        @If TempData("LoginExitoso") IsNot Nothing Then
            @<div id="alertaLoginExitoso"
                  data-mensaje="@TempData("LoginExitoso")"
                  style="display:none;"></div>
        End If

        @If TempData("MensajeClientes") IsNot Nothing Then
            @<div id="mensajeExito"
                  data-mensaje="@TempData("MensajeClientes")"
                  style="display:none;"></div>
        End If

        @If TempData("ErrorClientes") IsNot Nothing Then
            @<div id="mensajeError"
                  data-mensaje="@TempData("ErrorClientes")"
                  style="display:none;"></div>
        End If

    </div>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script>

                document.addEventListener("DOMContentLoaded", function () {

                    const alerta = document.getElementById("alertaLoginExitoso");
                    const exito = document.getElementById("mensajeExito");
                    const error = document.getElementById("mensajeError");

                    if (alerta !== null) {

                        Swal.fire({
                            icon: "success",
                            title: "¡Inicio de sesión exitoso!",
                            text: alerta.getAttribute("data-mensaje"),
                            confirmButtonText: "Continuar",
                            confirmButtonColor: "#2c325c",
                            background: "#262626",
                            color: "#FFFFFF",
                            timer: 2500,
                            timerProgressBar: true
                        });

                    } else if (exito !== null) {

                        Swal.fire({
                            icon: "success",
                            title: "Operación exitosa",
                            text: exito.getAttribute("data-mensaje"),
                            confirmButtonText: "Entendido",
                            confirmButtonColor: "#2c325c",
                            background: "#262626",
                            color: "#FFFFFF"
                        });

                    } else if (error !== null) {

                        Swal.fire({
                            icon: "error",
                            title: "Ocurrió un error",
                            text: error.getAttribute("data-mensaje"),
                            confirmButtonText: "Entendido",
                            confirmButtonColor: "#2c325c",
                            background: "#262626",
                            color: "#FFFFFF"
                        });

                    }

                    const formLogout = document.getElementById("formLogout");

                    if (formLogout !== null) {

                        formLogout.addEventListener("submit", function (event) {

                            event.preventDefault();

                            const formulario = this;

                            Swal.fire({
                                icon: "question",
                                title: "¿Cerrar sesión?",
                                text: "¿Estás seguro de que deseas salir del sistema?",
                                showCancelButton: true,
                                confirmButtonText: "Sí, cerrar sesión",
                                cancelButtonText: "Cancelar",
                                confirmButtonColor: "#2c325c",
                                cancelButtonColor: "#383838",
                                background: "#262626",
                                color: "#FFFFFF",
                                reverseButtons: true,
                                allowOutsideClick: false
                            }).then(function (resultado) {

                                if (resultado.isConfirmed) {
                                    formulario.submit();
                                }

                            });

                        });

                    }

                });

                function confirmarEliminar(boton) {

                    Swal.fire({
                        title: "¿Eliminar cliente?",
                        text: "Esta acción no se puede deshacer.",
                        icon: "warning",
                        showCancelButton: true,
                        confirmButtonText: "Sí, eliminar",
                        cancelButtonText: "Cancelar",
                        confirmButtonColor: "#C65B65",
                        cancelButtonColor: "#404040",
                        background: "#262626",
                        color: "#FFFFFF",
                        reverseButtons: true
                    }).then(function (resultado) {

                        if (resultado.isConfirmed) {
                            boton.closest("form").submit();
                        }

                    });

                }

    </script>

</body>
</html>
