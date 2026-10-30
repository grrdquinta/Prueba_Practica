Imports System.ComponentModel.DataAnnotations

Public Class RegistroModel

    <Required(ErrorMessage:="El usuario es obligatorio")>
    <StringLength(50)>
    <Display(Name:="Usuario")>
    Public Property Usuario As String

    <Required(ErrorMessage:="El correo es obligatorio")>
    <EmailAddress(ErrorMessage:="Ingrese un correo válido")>
    <StringLength(100)>
    <Display(Name:="Correo electrónico")>
    Public Property CorreoElectronico As String

    <Required(ErrorMessage:="La contraseña es obligatoria")>
    <RegularExpression(
        "^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9\s])\S{8,}$",
        ErrorMessage:="La contraseña debe tener mínimo 8 caracteres, una mayúscula, una minúscula, un número y un carácter especial."
    )>
    <Display(Name:="Contraseña")>
    Public Property Contrasena As String

    <Required(ErrorMessage:="Confirme su contraseña")>
    <DataType(DataType.Password)>
    <Compare("Contrasena", ErrorMessage:="Las contraseñas no coinciden")>
    <Display(Name:="Confirmar contraseña")>
    Public Property ConfirmarContrasena As String

End Class
