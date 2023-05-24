Imports MySql.Data.MySqlClient
Public Class Form1
    Dim test As String = "server=172.30.15.107;port=3306;user=root;password=;database=test1"
    Dim conn As New MySqlConnection(test)
    Dim gagi As Integer
    Dim rid As MySqlDataReader
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        read()
    End Sub
    Public Sub read()
        DataGridView1.Rows.Clear()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM user", conn)
            rid = cmd.ExecuteReader
            While rid.Read
                DataGridView1.Rows.Add(rid.Item("id"), rid.Item("FIRSTNAME"), rid.Item("LASTNAME"))
            End While

        Catch ex As Exception
            MsgBox("Di nabasa")
        Finally
            conn.Close()
        End Try

    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            conn.Open()
            Dim del As New MySqlCommand("DELETE FROM user WHERE FIRSTNAME=@FIRSTNAME", conn)
            del.Parameters.AddWithValue("@FIRSTNAME", txtFname.Text)
            del.ExecuteNonQuery()
            MsgBox("Na delete na kuys")
        Catch ex As Exception
            MsgBox("gagi ayaw")
        Finally
            conn.Close()
        End Try
        read()
    End Sub
    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click

        Try
            conn.Open()
            Dim cmd As New MySqlCommand("INSERT INTO user(`FIRSTNAME`, `LASTNAME`) VALUES(@FIRSTNAME, @LASTNAME)", conn)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@FIRSTNAME", txtFname.Text)
            cmd.Parameters.AddWithValue("@LASTNAME", txtLname.Text)

            gagi = cmd.ExecuteNonQuery
            If (gagi > 0) Then
                MsgBox("nice gumana yiiee")
            Else
                MsgBox("ayaw amp hahah")
            End If
        Catch ex As Exception
            MsgBox("Sira boy")
        Finally
            conn.Close()
        End Try
        read()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        read()
        Try
            conn.Open()
            MsgBox("gumagana gagi")
        Catch ex As Exception
            MsgBox("gagi ayaw")
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("UPDATE user SET `FIRSTNAME`=@FIRSTNAME, `LASTNAME`=@LASTNAME WHERE `FIRSTNAME`=@HAHA", conn)
            cmd.Parameters.AddWithValue("@FIRSTNAME", txtFname.Text)
            cmd.Parameters.AddWithValue("@LASTNAME", txtLname.Text)
            cmd.Parameters.AddWithValue("@HAHA", txtUname.Text)
            cmd.ExecuteNonQuery()
            MsgBox("Nabago na")
        Catch ex As Exception
            MsgBox("Di nabago haha")
        Finally
            conn.Close()
        End Try
        read()
        login.Show()
        Me.Hide()

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        txtFname.Text = DataGridView1.CurrentRow.Cells(1).Value
        txtLname.Text = DataGridView1.CurrentRow.Cells(2).Value
        txtUname.Text = DataGridView1.CurrentRow.Cells(1).Value
    End Sub
End Class
