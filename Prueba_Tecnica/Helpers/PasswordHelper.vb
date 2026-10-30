Imports System.Security.Cryptography

Public Class PasswordHelper

    Private Const Iteraciones As Integer = 100000
    Private Const LongitudSalt As Integer = 16
    Private Const LongitudHash As Integer = 32

    Public Shared Function GenerarHash(
        contrasena As String
    ) As String

        Dim salt(LongitudSalt - 1) As Byte

        Using rng = RandomNumberGenerator.Create()
            rng.GetBytes(salt)
        End Using

        Using pbkdf2 As New Rfc2898DeriveBytes(
            contrasena,
            salt,
            Iteraciones,
            HashAlgorithmName.SHA256
        )
            Dim hash = pbkdf2.GetBytes(LongitudHash)

            Return "PBKDF2-SHA256:" &
                   Iteraciones.ToString() & ":" &
                   Convert.ToBase64String(salt) & ":" &
                   Convert.ToBase64String(hash)
        End Using


    End Function

    Private Shared Function CompararHashes(
        hash1 As Byte(),
        hash2 As Byte()
    ) As Boolean

        If hash1 Is Nothing OrElse hash2 Is Nothing Then
            Return False
        End If

        If hash1.Length <> hash2.Length Then
            Return False
        End If

        Dim diferencia As Integer = 0

        For i As Integer = 0 To hash1.Length - 1
            diferencia = diferencia Or (CInt(hash1(i)) Xor CInt(hash2(i)))
        Next

        Return diferencia = 0

    End Function

    Public Shared Function VerificarContrasena(
        contrasena As String,
        hashGuardado As String
    ) As Boolean
        Try
            Dim partes = hashGuardado.Split(":"c)

            If partes.Length <> 4 OrElse
                partes(0) <> "PBKDF2-SHA256" Then
                Return False

            End If

            Dim iteracionesGuardadas As Integer =
               Integer.Parse(partes(1))

            If iteracionesGuardadas < 100000 OrElse
               iteracionesGuardadas > 1000000 Then
                Return False
            End If

            Dim sal = Convert.FromBase64String(partes(2))
            Dim hashOriginal = Convert.FromBase64String(partes(3))

            If sal.Length <> LongitudSalt OrElse
               hashOriginal.Length <> LongitudHash Then
                Return False
            End If

            Using pbkdf2 As New Rfc2898DeriveBytes(
               contrasena,
               sal,
               iteracionesGuardadas,
               HashAlgorithmName.SHA256
           )

                Dim hashNuevo =
                    pbkdf2.GetBytes(hashOriginal.Length)

                Return CompararHashes(hashNuevo, hashOriginal)

            End Using

            If String.IsNullOrEmpty(contrasena) OrElse
               String.IsNullOrEmpty(hashGuardado) Then
                Return False
            End If

        Catch ex As FormatException
            Return False

        Catch ex As OverflowException
            Return False

        Catch ex As ArgumentException
            Return False

        End Try
    End Function


End Class
