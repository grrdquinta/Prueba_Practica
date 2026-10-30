Imports System.Data.SqlClient

Public Class LoginController

    Inherits Controller

    <HttpGet>
    Public Function Login() As ActionResult
        Return View()
    End Function

    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Function Login(model As LoginModel) As ActionResult

        If Not ModelState.IsValid Then
            Return View(model)
        End If

        Dim idUsuario As Guid = Guid.Empty
        Dim nombreUsuario As String = ""
        Dim hashGuardado As String = ""

        Using conexion As SqlConnection = ConexionDB.ObtenerConexion()

            Dim consulta As String = "Select id_usuario, usuario, [contrasena] " &
                "From Usuarios " &
                "Where (usuario = @usuarioCorreo OR correo_electronico = @usuarioCorreo)"

            Using comando As New SqlCommand(consulta, conexion)

                comando.Parameters.Add(
                    "@usuarioCorreo",
                    SqlDbType.VarChar,
                    100
                ).Value = model.Usuario_Correo.Trim()

                conexion.Open()

                Using reader As SqlDataReader = comando.ExecuteReader()

                    If reader.Read() Then
                        idUsuario = reader.GetGuid(0)
                        nombreUsuario = reader.GetString(1)
                        hashGuardado = reader.GetString(2)
                    End If

                End Using

            End Using

        End Using

        Dim credencialesValidas As Boolean = False

        If Not String.IsNullOrEmpty(hashGuardado) Then

            credencialesValidas = PasswordHelper.VerificarContrasena(
                model.Contrasena,
                hashGuardado
            )

        End If

        If Not credencialesValidas Then

            TempData("ErrorLogin") =
            "El usuario, correo electrónico o contraseña son incorrectos."

            Return View(model)

        End If

        Session("IdUsuario") = idUsuario.ToString()
        Session("NombreUsuario") = nombreUsuario

        TempData("LoginExitoso") =
            "¡Bienvenido, " & nombreUsuario & "! Has iniciado sesión correctamente."

        Return RedirectToAction("Index", "Clientes")

    End Function

    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Function Logout() As ActionResult
        Session.Clear()
        Session.Abandon()

        Return RedirectToAction("Login", "Login")
    End Function
End Class
