
Imports System.Data.SqlClient

Public Class UsuariosController
    Inherits Controller

    <HttpGet>
    Public Function Registrar() As ActionResult

        Return View("~/Views/Registrar/Registrar.vbhtml")

    End Function

    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Function Registrar(model As RegistroModel) As ActionResult

        If Not ModelState.IsValid Then
            Return View("~/Views/Registrar/Registrar.vbhtml", model)
        End If

        Dim usuario As String = model.Usuario.Trim()
        Dim correo As String = model.CorreoElectronico.Trim()

        Using conexion As SqlConnection = ConexionDB.ObtenerConexion()

            conexion.Open()

            Dim consultaExiste As String =
                "SELECT COUNT(*) FROM Usuarios " &
                "WHERE usuario = @usuario " &
                "OR correo_electronico = @correo " &
                "OR usuario = @correo " &
                "OR correo_electronico = @usuario"

            Using comando As New SqlCommand(consultaExiste, conexion)

                comando.Parameters.Add(
                    "@usuario", SqlDbType.VarChar, 100
                ).Value = usuario

                comando.Parameters.Add(
                    "@correo", SqlDbType.VarChar, 100
                ).Value = correo

                Dim existe As Integer =
                    Convert.ToInt32(comando.ExecuteScalar())

                If existe > 0 Then

                    TempData("ErrorRegistro") =
                    "El usuario o correo electrónico ya está registrado."

                    Return View("~/Views/Registrar/Registrar.vbhtml", model)

                End If

            End Using

            Dim hashContrasena As String =
                PasswordHelper.GenerarHash(model.Contrasena)

            Dim consultaInsertar As String =
                "INSERT INTO Usuarios " &
                "(usuario, correo_electronico, contrasena) " &
                "VALUES (@usuario, @correo, @contrasena)"

            Try

                Using comando As New SqlCommand(consultaInsertar, conexion)

                    comando.Parameters.Add(
                        "@usuario", SqlDbType.VarChar, 50
                    ).Value = usuario

                    comando.Parameters.Add(
                        "@correo", SqlDbType.VarChar, 100
                    ).Value = correo

                    comando.Parameters.Add(
                        "@contrasena", SqlDbType.VarChar, 150
                    ).Value = hashContrasena

                    comando.ExecuteNonQuery()

                End Using

            Catch ex As SqlException When ex.Number = 2601 OrElse ex.Number = 2627

                TempData("ErrorRegistro") =
                "El usuario o correo electrónico ya está registrado."

                Return View("~/Views/Registrar/Registrar.vbhtml", model)

            End Try

        End Using

        TempData("MensajeRegistro") =
            "Cuenta creada correctamente. Ya puedes iniciar sesión."

        Return RedirectToAction("Login", "Login")

    End Function

End Class
