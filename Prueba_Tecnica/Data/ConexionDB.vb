Imports System.Data.SqlClient

Public Class ConexionDB

    Public Shared Function ObtenerConexion() As SqlConnection

        Dim configuracion =
            ConfigurationManager.ConnectionStrings("ConexionDB")

        If configuracion Is Nothing Then
            Throw New ConfigurationErrorsException(
                "No se encontró o hubo un problema con la cadena de conexión 'ConexionDB'"
            )
        End If

        Return New SqlConnection(configuracion.ConnectionString)

    End Function

End Class
