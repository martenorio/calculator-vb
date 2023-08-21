Public Class ControlOperation

    Public Function Calculate(ByVal operatorKey As String, ByVal firstValue As String, ByVal secondValue As String) As Decimal

        Select Case operatorKey
            Case "+"
                Return Convert.ToDecimal(firstValue) + Convert.ToDecimal(secondValue)
        ' The following is the only Case clause that evaluates to True.
            Case "-"
                Return Convert.ToDecimal(firstValue) - Convert.ToDecimal(secondValue)
            Case "*"
                Return Convert.ToDecimal(firstValue) * Convert.ToDecimal(secondValue)
            Case "/"
                Return Convert.ToDecimal(firstValue) / Convert.ToDecimal(secondValue)
        End Select

    End Function

End Class
