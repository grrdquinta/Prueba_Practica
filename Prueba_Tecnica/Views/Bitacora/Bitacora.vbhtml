
@ModelType IEnumerable(Of BitacoraModel)

@Code
    ViewData("Title") = "Bitácora"
    Layout = Nothing
End Code

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="utf-8" />
    <meta name="viewport"
          content="width=device-width, initial-scale=1" />

    <title>Historial de actividades</title>

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
            max-width: 1400px;
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

        .subtitle {
            color: #AAAAAA;
            font-size: 14px;
            margin-top: 10px;
        }

        .btn-back {
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

        .btn-back:hover {
            background: #3b447b;
        }

        .btn-icon {
            width: 18px;
            height: 18px;
            object-fit: contain;
            display: block;
        }

        .table-container {
            overflow-x: auto;
        }

        table {
            width: 100%;
            border-collapse: collapse;
        }

        th, td {
            padding: 14px 18px;
            text-align: center;
            vertical-align: middle;
            border-bottom: 1px solid #404040;
            font-size: 14px;
        }

        th {
            background: #252B36;
            white-space: nowrap;
            font-weight: bold;
        }

        td {
            line-height: 1.5;
        }

        tbody tr:hover {
            background: #303030;
        }

        .user {
            color: #A5B4FC;
            font-weight: bold;
        }

        .date {
            color: #AAAAAA;
            white-space: nowrap;
        }

        .empty {
            text-align: center;
            color: #AAAAAA;
            padding: 25px;
        }
    </style>
</head>

<body>

    <div class="container">

        <div class="header">

            <div>
                <h2>Historial de actividades</h2>
                <p class="subtitle">
                    Consulta las operaciones realizadas en el sistema
                </p>
            </div>

            <a href="@Url.Action("Index", "Clientes")"
               class="btn-back">
                <img src="@Url.Content("~/Content/Images/back-svgrepo-com.svg")"
                     alt="Bitacora-btn"
                     class="btn-icon" />
                Volver atras
            </a>

        </div>



        <div class="table-container">

            <table>
                <thead>
                    <tr>
                        <th>Usuario</th>
                        <th>Acción</th>
                        <th>Cliente afectado</th>
                        <th>Descripción</th>
                        <th>Fecha y hora</th>
                    </tr>
                </thead>

                <tbody>

                    @For Each registro In Model
                        @<tr>
                            <td class="user">
                                @registro.Usuario
                            </td>

                            <td>
                                @registro.Accion
                            </td>

                            <td>
                                @registro.NombreCliente
                            </td>

                            <td>
                                @registro.Descripcion
                            </td>

                            <td class="date">
                                @registro.FechaHora.ToString("dd/MM/yyyy HH:mm:ss")
                            </td>
                        </tr>
                    Next

                </tbody>
            </table>

        </div>


        @If Not Model.Any() Then
            @<div class="empty">
                No hay actividades registradas.
            </div>
        End If

    </div>

</body>
</html>
