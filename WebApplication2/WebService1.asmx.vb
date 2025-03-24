Imports System.ComponentModel
Imports System.Web.Script.Serialization
Imports System.Web.Script.Services
Imports System.Web.Services
Imports System.Web.Services.Protocols

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class WebService1
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function HelloWorld() As String
        Return "Hola a todos"
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Sub ObtenerDatos()
        ' Crear un objeto de ejemplo
        Dim datos As New With {
            .Nombre = "Juan",
            .Edad = 30,
            .Ciudad = "Madrid"
        }

        ' Serializar el objeto a JSON
        Dim jsonSerializer As New JavaScriptSerializer()
        Dim json As String = jsonSerializer.Serialize(datos)

        ' Establecer el tipo de contenido a JSON
        HttpContext.Current.Response.ContentType = "application/json"
        HttpContext.Current.Response.Write(json)
        HttpContext.Current.ApplicationInstance.CompleteRequest()

    End Sub

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
    Public Sub IngresarDatos(ByVal persona As Person)
        Try

            ' Crear un objeto de ejemplo
            Dim datos As New With {
                .Nombre = persona.Nombre,
                .Edad = persona.Edad,
                .Ciudad = persona.Ciudad
            }

            ' Serializar el objeto a JSON
            Dim jsonSerializer As New JavaScriptSerializer()
            Dim json As String = jsonSerializer.Serialize(datos)

            ' Establecer el tipo de contenido a JSON
            HttpContext.Current.Response.ContentType = "application/json"
            HttpContext.Current.Response.Write(json)
            'HttpContext.Current.Response.End()
            ' Finalizar correctamente sin abortar el hilo
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.ReadLine()
        End Try

    End Sub

End Class

''' <summary>
''' clase prueba
''' </summary>
Public Class Person
    Public Property Nombre As String
    Public Property Edad As Integer
    Public Property Ciudad As String
End Class

''' <summary>
''' clase errores
''' </summary>
Public Class Errores
    Public Property Mensaje As String
End Class