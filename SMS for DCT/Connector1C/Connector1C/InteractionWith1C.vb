
Public Class InteractionWith1C

    Private Connection1C As Object
    Public ConString As String


    Public Function QueryTo1CServer(ByVal UserName As String, ByVal QueryName As String, ByVal Parameters As String) As String
        Dim Answer As String
        Try
            Answer = Connection1C.PerformQuery(UserName, QueryName, Parameters)
            Return Answer
        Catch exp As Exception
            If ConnectTo1CServer() = "ok" Then
                Try
                    Answer = Connection1C.PerformQuery(UserName, QueryName, Parameters)
                    Return Answer
                Catch exp2 As Exception
                    Return exp.Message + "   :   " + exp2.Message
                End Try
            Else
                Return Nothing
            End If
        End Try
    End Function

    Public Function ConnectTo1CServer(ByVal ConnectonString As String) As String
        ConString = ConnectonString
        Return ConnectTo1CServer()
    End Function

    Private Function ConnectTo1CServer() As String

        Dim Connector1C As Object

        Connector1C = CreateObject("V81.COMConnector")

        Try
            Connection1C = Connector1C.Connect(ConString)
            Return "ok"
        Catch exp As Exception
            ' Исключение возникает, например, если агент 1С сервера не запущен
            Return exp.Message
        End Try

    End Function

    Public Function Disconnect() As Boolean
        Connection1C = Nothing
        System.GC.Collect()
    End Function

    Public Function GetConnectionNumber() As Integer
        Try
            Return Connection1C.GetConnectionNumber()
        Catch
            Return 0
        End Try
    End Function

End Class
