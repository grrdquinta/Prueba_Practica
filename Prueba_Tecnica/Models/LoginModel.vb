Imports System.ComponentModel.DataAnnotations

Public Class LoginModel

    <Required(ErrorMessage:="El usuario o correo es obligatorio")>
    <Display(Name:="Usuario o correo electrónico")>
    Public Property Usuario_Correo As String

    <Required(ErrorMessage:="La contraseña es obligatoria")>
    <Display(Name:="Contraseña")>
    Public Property Contrasena As String


End Class
