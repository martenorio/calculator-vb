Imports System.IO
Imports System.Net
Imports System.Text

Class HTTPWebRequest_GetResponse

    Private BUFFER_SIZE As Integer = 1024
    Public Shared response As String
    Public Shared done As Boolean = False
    Public Shared length As Long = 1
    Public Shared progress As Integer
    Public Shared myHttpWebRequest As HttpWebRequest
    Public Shared myRequestState As New RequestState()

    Shared Sub Main(url As String)

        Try
            Dim headRequest As HttpWebRequest = WebRequest.Create(url)
            headRequest.Method = "HEAD"
            Dim headResponse As HttpWebResponse = headRequest.GetResponse
            length = headResponse.ContentLength
            Debug.WriteLine(length)
            headResponse.Close()
            ' Create a HttpWebrequest object to the desired URL.  
            myHttpWebRequest = WebRequest.Create(url)

            ' Create an instance of the RequestState and assign the previous myHttpWebRequest 
            ' object to its request field.   

            myRequestState.request = myHttpWebRequest
            'Dim myResponse As New HTTPWebRequest_GetResponse()

            ' Start the asynchronous request. 
            Dim result As IAsyncResult = CType(myHttpWebRequest.BeginGetResponse(New AsyncCallback(AddressOf RespCallback), myRequestState), IAsyncResult)

        Catch e As WebException
            Debug.WriteLine("Main Exception raised!")
            Debug.WriteLine("Message: " + e.Message)
            Debug.WriteLine("Status: " + e.Status)
        Catch e As Exception
            Debug.WriteLine("Main Exception raised!")
            Debug.WriteLine("Source : " + e.Source)
            Debug.WriteLine("Message : " + e.Message)
        End Try
    End Sub 'Main

    Private Shared Sub RespCallback(asynchronousResult As IAsyncResult)
        Debug.WriteLine("RespCallBack entered")
        Try
            ' State of request is asynchronous. 
            Dim myRequestState As RequestState = CType(asynchronousResult.AsyncState, RequestState)
            Dim myHttpWebRequest As HttpWebRequest = myRequestState.request
            myRequestState.response = CType(myHttpWebRequest.EndGetResponse(asynchronousResult), HttpWebResponse)

            ' Read the response into a Stream object. 
            Dim responseStream As Stream = myRequestState.response.GetResponseStream()
            myRequestState.streamResponse = responseStream

            ' Begin the Reading of the contents of the HTML page. 
            Dim asynchronousInputRead As IAsyncResult = responseStream.BeginRead(myRequestState.BufferRead, 0, 1024, New AsyncCallback(AddressOf ReadCallBack), myRequestState)
            Return
        Catch e As WebException
            Debug.WriteLine("RespCallback Exception raised!")
            Debug.WriteLine("Message: " + e.Message)
            Debug.WriteLine("Status: " + e.Status)
        Catch e As Exception
            Debug.WriteLine("RespCallback Exception raised!")
            Debug.WriteLine("Source : " + e.Source)
            Debug.WriteLine("Message : " + e.Message)
        End Try
    End Sub 'RespCallback

    Private Shared Sub ReadCallBack(asyncResult As IAsyncResult)
        Debug.WriteLine("ReadCallBack entered")
        Try

            Dim myRequestState As RequestState = CType(asyncResult.AsyncState, RequestState)
            Dim responseStream As Stream = myRequestState.streamResponse
            Dim read As Integer = responseStream.EndRead(asyncResult)
            ' Read the HTML page. 
            If read > 0 Then
                myRequestState.requestData.Append(Encoding.ASCII.GetString(myRequestState.BufferRead, 0, read))
                If length = -1 Or length = 0 Then
                    progress = -1
                Else
                    progress = myRequestState.BufferRead.Length * 100 / length
                    Debug.WriteLine(progress)
                End If
                Dim asynchronousResult As IAsyncResult = responseStream.BeginRead(myRequestState.BufferRead, 0, 1024, New AsyncCallback(AddressOf ReadCallBack), myRequestState)

            Else
                If myRequestState.BufferRead.Length > 1 Then
                    Dim fullResponse As String = myRequestState.requestData.ToString
                    response = fullResponse.Substring(0, fullResponse.IndexOf("</body>")).Substring(fullResponse.IndexOf(">", fullResponse.IndexOf("<body")) + 2) 'Returns only body
                    ' Release the HttpWebResponse resource.
                    myRequestState.response.Close()
                    done = True
                    Debug.WriteLine(done)
                End If

                responseStream.Close()
            End If

        Catch e As WebException
            Debug.WriteLine("ReadCallBack Exception raised!")
            Debug.WriteLine("Message: " + e.Message)
            Debug.WriteLine("Status: " + e.Status)
        Catch e As Exception
            Debug.WriteLine("ReadCallBack Exception raised!")
            Debug.WriteLine("Source : " + e.Source)
            Debug.WriteLine("Message : " + e.Message)
        End Try
    End Sub 'ReadCallBack 
End Class 'HttpWebRequest_BeginGetResponse
