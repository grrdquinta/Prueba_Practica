Imports System.ComponentModel.DataAnnotations

Public Class ClienteModel
    Public Property IdCliente As Guid

    <Required(ErrorMessage:="El nombre es obligatorio")>
    <StringLength(100)>
    <Display(Name:="Nombre")>
    Public Property Nombre As String

    <Required(ErrorMessage:="El apellido es obligatorio")>
    <StringLength(100)>
    <Display(Name:="Apellido")>
    Public Property Apellido As String

    <Required(ErrorMessage:="El correo electrónico es obligatorio")>
    <EmailAddress(ErrorMessage:="Ingrese un correo válido")>
    <StringLength(250)>
    <Display(Name:="Correo electrónico")>
    Public Property Correo As String

    <Required(ErrorMessage:="La dirección es obligatoria")>
    <StringLength(250)>
    <Display(Name:="Dirección")>
    Public Property Direccion As String

    <Required(ErrorMessage:="El teléfono es obligatorio")>
    <RegularExpression("^[0-9]{4}-[0-9]{4}$",
                   ErrorMessage:="El teléfono debe tener el formato 0000-0000")>
    <StringLength(9, ErrorMessage:="El teléfono debe tener 9 caracteres")>
    <Display(Name:="Teléfono")>
    Public Property Telefono As String

End Class
