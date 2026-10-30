
Imports System.Data.SqlClient
Imports System.Web.Mvc

Public Class BitacoraController
    Inherits Controller

    <HttpGet>
    Public Function Bitacora() As ActionResult

        If Session("IdUsuario") Is Nothing Then
            Return RedirectToAction("Login", "Login")
        End If

        Dim registros As New List(Of BitacoraModel)

        Using conexion As SqlConnection = ConexionDB.ObtenerConexion()

            Dim consulta As String =
                "SELECT B.id_bitacora, " &
                "B.id_usuario, " &
                "U.usuario AS usuario, " &
                "B.accion, " &
                "B.tabla_afectada, " &
                "B.id_registro, " &
                "B.nombre_cliente, " &
                "B.descripcion, " &
                "DATEADD(HOUR, -6, B.fecha_hora) AS fecha_hora " &
                "FROM Bitacora B " &
                "INNER JOIN Usuarios U " &
                "ON B.id_usuario = U.id_usuario " &
                "ORDER BY B.fecha_hora DESC"

            Using comando As New SqlCommand(consulta, conexion)

                conexion.Open()

                Using reader As SqlDataReader = comando.ExecuteReader()

                    While reader.Read()

                        Dim registro As New BitacoraModel With {
                            .IdBitacora = reader.GetGuid(0),
                            .IdUsuario = reader.GetGuid(1),
                            .Usuario = reader.GetString(2),
                            .Accion = reader.GetString(3),
                            .TablaAfectada = reader.GetString(4),
                            .IdRegistro = If(
                                reader.IsDBNull(5),
                                CType(Nothing, Guid?),
                                CType(reader.GetGuid(5), Guid?)
                            ),
                            .NombreCliente = If(
                                reader.IsDBNull(6),
                                "No registrado",
                                reader.GetString(6)
                            ),
                            .Descripcion = If(
                                reader.IsDBNull(7),
                                "",
                                reader.GetString(7)
                            ),
                            .FechaHora = reader.GetDateTime(8)
                        }

                        registros.Add(registro)

                    End While

                End Using

            End Using

        End Using

        Return View("~/Views/Bitacora/Bitacora.vbhtml", registros)

    End Function

End Class
