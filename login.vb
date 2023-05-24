Imports MySql.Data.MySqlClient

Public Class login
    Dim test As String = "server=localhost;port=4306;user=root;password=;database=test1"
    Dim conn As New MySqlConnection(test)
    Dim gagi As Integer
    Dim rid As MySqlDataReader
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM user", conn)
            rid = cmd.ExecuteReader

            If (txtUser.Text = rid.Item("FIRSTNAME") And txtPass.Text = rid.Item("LASTNAME")) Then
                MsgBox("Login successfull")
                jollibola.Show()
                Me.Hide()
            Else
                MsgBox("Invalid username or password")
            End If


        Catch ex As Exception

        End Try
        jollibola.Show()
        Me.Hide()
    End Sub
End Class