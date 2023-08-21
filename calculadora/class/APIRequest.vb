Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Text

Public Class APIRequest
    Function HttpRequest(URL As String) As String
        Dim request As WebRequest = WebRequest.Create(URL)
        Dim dataStream As Stream = request.GetResponse.GetResponseStream()
        Dim sr As New StreamReader(dataStream)
        Return sr.ReadToEnd
    End Function

    ' Post Request
    Function HttpPost(URL As String, JSON As String) As String

        ' JSON Data
        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(JSON)

        ' HTTP Request
        Dim request As WebRequest = WebRequest.Create(URL)
        request.Method = "POST"

        ' Requesting
        Dim dataStream As Stream = request.GetRequestStream()
        dataStream.Write(byteArray, 0, byteArray.Length)
        dataStream.Close()

        ' Recieving a Response
        dataStream = request.GetResponse.GetResponseStream()
        Dim reader As New StreamReader(dataStream)
        Dim responseFromServer As String = reader.ReadToEnd()
        dataStream.Close()
        reader.Close()
        'Return JObject.Parse(responseFromServer)
        Return responseFromServer

    End Function

    Public Async Function requestgAsync(URL As String) As Task(Of String)
        'Dim client As New HttpClient
        'Dim data = ""
        'Dim data2 = "{""username"": ""some uname"", ""password"": ""somepass"", ""msisdn"": ""somenumber"", ""content"": ""Hello, this is a sample broadcast"", ""shortcode_mask"" :""somemask""}"
        'Dim buffer = Encoding.UTF8.GetBytes(data)
        'Dim bytes = New ByteArrayContent(buffer)
        'bytes.Headers.ContentType = New Headers.MediaTypeHeaderValue("application/json")
        ''Dim request1 = Await client.PostAsync(URL, bytes)
        ''Dim request = client.GetAsync(URL).Result
        'Dim response = client.GetAsync(URL).Result
        ''response.EnsureSuccessStatusCode()
        'Debug.WriteLine(response.EnsureSuccessStatusCode())
        'Debug.WriteLine(response.Content.Headers())
        ''Debug.WriteLine(Await response.Content.ReadAsStringAsync())
        'Return response.ToString
        Dim hc As HttpClient
        hc = New HttpClient
        Try
            Dim rm As HttpResponseMessage
            rm = Await hc.GetAsync(URL)
            If rm.IsSuccessStatusCode Then
                Dim res As String
                Debug.WriteLine(rm.Content.Headers.ContentLength)
                res = Await rm.Content.ReadAsStringAsync()
                If TypeOf res Is String Then
                    Dim result = rm.Content()
                    Debug.WriteLine(res)
                    Return res
                    '...work with a result...
                End If
            End If
        Catch ex As Exception
            '...work with exception...
        End Try
        Return ""
    End Function
End Class
