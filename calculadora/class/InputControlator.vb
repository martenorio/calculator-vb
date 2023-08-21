Public Class InputControlator
    Public Sub addValue(ByVal key As String, ByRef firstValue As String, ByRef secondValue As String, myOperator As String)
        If (existValue(myOperator)) Then
            intertValue(secondValue, key)
        Else
            intertValue(firstValue, key)
        End If
    End Sub

    Private Sub intertValue(ByRef value As String, ByVal key As String)
        If Not containDot(value) Then
            value += key
        ElseIf Not key = "." Then
            value += key
        End If
    End Sub

    Private Function existValue(ByVal value As String) As Boolean
        Return IIf(value IsNot Nothing And value.Length > 0, True, False)
    End Function
    Private Function containDot(ByVal value As String) As Boolean
        Return IIf(value.Contains("."), True, False)
    End Function



End Class
