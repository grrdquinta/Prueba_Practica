Imports System.Data.SqlClient

Public Class ClientesController
    Inherits Controller

    Private Function UsuarioActual() As Guid?
        Dim id As Guid

        If Session("IdUsuario") IsNot Nothing AndAlso
            Guid.TryParse(Session("IdUsuario").ToString(), id) Then
            Return id
        End If

        Return Nothing

    End Function

    Private Sub RegistrarBitacora(
        conexion As SqlConnection,
        transaccion As SqlTransaction,
        accion As String,
        idRegistro As Guid,
        nombreCliente As String,
        descripcion As String
    )

        Dim sql As String =
            "INSERT INTO Bitacora " &
            "(id_usuario, accion, tabla_afectada, id_registro, nombre_cliente, descripcion) " &
            "VALUES (@usuario, @accion, 'Clientes', @registro, @nombreCliente, @descripcion)"

        Using comando As New SqlCommand(sql, conexion, transaccion)

            comando.Parameters.Add("@usuario", SqlDbType.UniqueIdentifier).Value =
                UsuarioActual().Value

            comando.Parameters.Add("@accion", SqlDbType.VarChar, 50).Value =
                accion

            comando.Parameters.Add("@registro", SqlDbType.UniqueIdentifier).Value =
                idRegistro

            comando.Parameters.Add("@nombreCliente", SqlDbType.VarChar, 201).Value =
                nombreCliente

            comando.Parameters.Add("@descripcion", SqlDbType.VarChar, 255).Value =
                descripcion

            comando.ExecuteNonQuery()

        End Using

    End Sub

    <HttpGet>
    Public Function Index() As ActionResult
        If Session("IdUsuario") Is Nothing Then
            Return RedirectToAction("Login", "Login")
        End If

        Dim clientes As New List(Of ClienteModel)

        Using conexion As SqlConnection = ConexionDB.ObtenerConexion()

            Dim consulta As String =
                 "SELECT id_cliente, nombre, apellido, " &
                "correo_electronico, direccion, telefono " &
                "FROM Clientes ORDER BY nombre ASC"

            Using comando As New SqlCommand(consulta, conexion)

                conexion.Open()

                Using reader As SqlDataReader = comando.ExecuteReader()

                    While reader.Read()

                        Dim cliente As New ClienteModel With {
                            .IdCliente = reader.GetGuid(0),
                            .Nombre = reader.GetString(1),
                            .Apellido = reader.GetString(2),
                            .Correo = reader.GetString(3),
                            .Direccion = reader.GetString(4),
                            .Telefono = reader.GetString(5)
                        }

                        clientes.Add(cliente)

                    End While

                End Using

            End Using

        End Using

        Return View(clientes)
    End Function

    <HttpGet>
    Public Function Crear() As ActionResult
        If Not UsuarioActual.HasValue Then
            Return RedirectToAction("Login", "Login")
        End If

        Return View(New ClienteModel())
    End Function

    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Function Crear(model As ClienteModel) As ActionResult

        If Not UsuarioActual().HasValue Then
            Return RedirectToAction("Login", "Login")
        End If

        If Not ModelState.IsValid Then
            Return View(model)
        End If

        Using conexion As SqlConnection = ConexionDB.ObtenerConexion()

            conexion.Open()

            Using transaccion As SqlTransaction = conexion.BeginTransaction()

                Try

                    Dim sql As String =
                    "INSERT INTO Clientes " &
                    "(nombre, apellido, correo_electronico, direccion, telefono) " &
                    "OUTPUT INSERTED.id_cliente " &
                    "VALUES (@nombre, @apellido, @correo, @direccion, @telefono)"

                    Dim idCliente As Guid

                    Using comando As New SqlCommand(sql, conexion, transaccion)

                        comando.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value = model.Nombre.Trim()
                        comando.Parameters.Add("@apellido", SqlDbType.VarChar, 100).Value = model.Apellido.Trim()
                        comando.Parameters.Add("@correo", SqlDbType.VarChar, 100).Value = model.Correo.Trim()
                        comando.Parameters.Add("@direccion", SqlDbType.VarChar, 250).Value = model.Direccion.Trim()
                        comando.Parameters.Add("@telefono", SqlDbType.VarChar, 15).Value = model.Telefono.Trim()

                        idCliente = CType(comando.ExecuteScalar(), Guid)

                    End Using

                    RegistrarBitacora(
                        conexion,
                        transaccion,
                        "CREAR",
                        idCliente,
                        model.Nombre.Trim() & " " & model.Apellido.Trim(),
                        "Se registró un nuevo cliente."
                    )

                    transaccion.Commit()

                Catch ex As SqlException When ex.Number = 2601 OrElse ex.Number = 2627

                    transaccion.Rollback()

                    If ex.Message.Contains("correo_electronico") Then

                        TempData("ErrorClientes") =
                        "El correo electrónico ya está registrado."

                    ElseIf ex.Message.Contains("telefono") Then

                        TempData("ErrorClientes") =
                        "El número de teléfono ya está registrado."

                    Else

                        TempData("ErrorClientes") =
                        "El correo electrónico o teléfono ya está registrado."

                    End If

                    Return View(model)

                Catch ex As Exception

                    transaccion.Rollback()

                    TempData("ErrorClientes") =
                        "Ocurrió un error al registrar el cliente."

                    Return View(model)

                End Try

            End Using

        End Using

        TempData("MensajeClientes") =
        "Cliente registrado correctamente."

        Return RedirectToAction("Index", "Clientes")

    End Function

    <HttpGet>
    Public Function Editar(id As Guid?) As ActionResult
        If Not UsuarioActual().HasValue Then
            Return RedirectToAction("Login", "Login")
        End If

        If Not id.HasValue Then
            Return HttpNotFound()
        End If

        Dim cliente As ClienteModel = Nothing

        Using conexion As SqlConnection = ConexionDB.ObtenerConexion()

            Dim sql As String =
                "SELECT id_cliente, nombre, apellido, correo_electronico, direccion, telefono " &
                "FROM Clientes WHERE id_cliente = @id"

            Using comando As New SqlCommand(sql, conexion)

                comando.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id.Value

                conexion.Open()

                Using reader = comando.ExecuteReader()

                    If reader.Read() Then
                        cliente = New ClienteModel With {
                            .IdCliente = reader.GetGuid(0),
                            .Nombre = reader.GetString(1),
                            .Apellido = reader.GetString(2),
                            .Correo = reader.GetString(3),
                            .Direccion = reader.GetString(4),
                            .Telefono = reader.GetString(5)
                        }
                    End If

                End Using

            End Using

        End Using

        If cliente Is Nothing Then
            Return HttpNotFound()
        End If

        Return View(cliente)

    End Function


    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Function Editar(model As ClienteModel) As ActionResult

        If Not UsuarioActual().HasValue Then
            Return RedirectToAction("Login", "Login")
        End If

        If model.IdCliente = Guid.Empty Then
            Return HttpNotFound()
        End If

        If Not ModelState.IsValid Then
            Return View(model)
        End If

        Using conexion As SqlConnection = ConexionDB.ObtenerConexion()

            conexion.Open()

            Using transaccion As SqlTransaction = conexion.BeginTransaction()

                Try
                    Dim sql As String =
                    "UPDATE Clientes SET " &
                    "nombre = @nombre, apellido = @apellido, " &
                    "correo_electronico = @correo, " &
                    "direccion = @direccion, telefono = @telefono " &
                    "WHERE id_cliente = @id"

                    Dim filas As Integer

                    Using comando As New SqlCommand(sql, conexion, transaccion)

                        comando.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = model.IdCliente
                        comando.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value = model.Nombre.Trim()
                        comando.Parameters.Add("@apellido", SqlDbType.VarChar, 100).Value = model.Apellido.Trim()
                        comando.Parameters.Add("@correo", SqlDbType.VarChar, 100).Value = model.Correo.Trim()
                        comando.Parameters.Add("@direccion", SqlDbType.VarChar, 250).Value = model.Direccion.Trim()
                        comando.Parameters.Add("@telefono", SqlDbType.VarChar, 15).Value = model.Telefono.Trim()

                        filas = comando.ExecuteNonQuery()

                    End Using

                    If filas = 0 Then
                        transaccion.Rollback()
                        Return HttpNotFound()
                    End If

                    RegistrarBitacora(
                        conexion,
                        transaccion,
                        "EDITAR",
                        model.IdCliente,
                        model.Nombre.Trim() & " " & model.Apellido.Trim(),
                        "Se actualizaron los datos del cliente."
                    )

                    transaccion.Commit()

                Catch ex As SqlException When ex.Number = 2601 OrElse ex.Number = 2627

                    transaccion.Rollback()

                    If ex.Message.IndexOf("correo_electronico", StringComparison.OrdinalIgnoreCase) >= 0 Then

                        TempData("ErrorClientes") =
                        "El correo electrónico ya está registrado por otro cliente."

                    ElseIf ex.Message.IndexOf("telefono", StringComparison.OrdinalIgnoreCase) >= 0 Then

                        TempData("ErrorClientes") =
                        "El número de teléfono ya está registrado por otro cliente."

                    Else

                        TempData("ErrorClientes") =
                        "El correo electrónico o teléfono ya está registrado."

                    End If

                    Return View(model)

                Catch ex As Exception

                    transaccion.Rollback()

                    TempData("ErrorClientes") =
                    "Ocurrió un error al actualizar el cliente."

                    Return View(model)

                End Try

            End Using

        End Using

        TempData("MensajeClientes") = "Cliente actualizado correctamente."

        Return RedirectToAction("Index")

    End Function


    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Function Eliminar(id As Guid) As ActionResult

        If Not UsuarioActual().HasValue Then
            Return RedirectToAction("Login", "Login")
        End If

        Using conexion As SqlConnection = ConexionDB.ObtenerConexion()

            conexion.Open()

            Using transaccion = conexion.BeginTransaction()

                Try

                    Dim nombreCliente As String = ""

                    Dim sqlNombre As String =
                        "SELECT nombre + ' ' + apellido " &
                        "FROM Clientes WHERE id_cliente = @id"

                    Using comandoNombre As New SqlCommand(sqlNombre, conexion, transaccion)

                        comandoNombre.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id

                        Dim resultado As Object = comandoNombre.ExecuteScalar()

                        If resultado IsNot Nothing AndAlso resultado IsNot DBNull.Value Then
                            nombreCliente = resultado.ToString()
                        End If

                    End Using

                    Dim sql As String =
                        "DELETE FROM Clientes WHERE id_cliente = @id"

                    Dim filas As Integer

                    Using comando As New SqlCommand(sql, conexion, transaccion)

                        comando.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id

                        filas = comando.ExecuteNonQuery()

                    End Using

                    If filas = 0 Then
                        transaccion.Rollback()
                        Return HttpNotFound()
                    End If

                    RegistrarBitacora(
                        conexion,
                        transaccion,
                        "ELIMINAR",
                        id,
                        nombreCliente,
                        "Se eliminó un cliente."
                    )

                    transaccion.Commit()

                Catch
                    transaccion.Rollback()
                    TempData("ErrorClientes") = "No se pudo eliminar el cliente."
                    Return RedirectToAction("Index")
                End Try

            End Using

        End Using

        TempData("MensajeClientes") = "Cliente eliminado correctamente."

        Return RedirectToAction("Index")

    End Function

End Class
