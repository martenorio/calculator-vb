Public Class Form1
    Private firstValue As String = ""
    Private secondValue As String = ""
    Private OperatorValue As String = ""
    Private resultValue As String = ""

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub handleButtonCalculator(sender As Object, e As EventArgs) Handles Button16.Click
        If (firstValue.Length > 0 And secondValue.Length > 0 And OperatorValue.Length > 0) Then
            Dim MyOperations As New ControlOperation()
            resultValue = MyOperations.Calculate(OperatorValue, firstValue, secondValue).ToString()
            TextBox1.Text = resultValue
        End If
    End Sub
    Private Sub handleNumberValue(sender As Object, e As EventArgs) Handles Button1.Click, Button2.Click, Button3.Click, Button4.Click, Button5.Click, Button6.Click, Button7.Click, Button8.Click, Button9.Click, Button0.Click, Button10.Click
        Dim numberControlator As New InputControlator()
        numberControlator.addValue(sender.Text, Me.firstValue, Me.secondValue, Me.OperatorValue)
        TextBox2.Text = Me.firstValue
        TextBox4.Text = Me.secondValue
    End Sub
    Private Sub hanldleOperator(sender As Object, e As EventArgs) Handles Button12.Click, Button13.MouseCaptureChanged, Button14.Click, Button15.MouseCaptureChanged
        If Me.firstValue.Length > 0 Then
            Me.OperatorValue = sender.Text
            TextBox3.Text = sender.Text
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Me.firstValue = ""
        Me.secondValue = ""
        Me.OperatorValue = ""
        Me.resultValue = ""
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
    End Sub

    Private Async Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        'API
        Debug.WriteLine("Hola")
        Dim URL As String = "https://jsonplaceholder.typicode.com/posts"
        Dim http As New APIRequest()
        Dim task = Await http.requestgAsync(URL)
        'Debug.WriteLine(task)
        RichTextBox1.Text = task
    End Sub
    Private Sub setBarPercentage(ByVal porcentage As Integer)
        ProgressBar1.Value = porcentage
    End Sub

End Class
