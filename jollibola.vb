Imports MySql.Data.MySqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports System.Diagnostics
Imports System.Drawing

Public Class jollibola
    Dim test As String = "server=localhost;port=4306;user=root;password=;database=test1"
    Dim conn As New MySqlConnection(test)
    Dim gagi As Integer
    Dim rid As MySqlDataReader

    Dim imageInput As String
    Dim imageMeal As String
    Dim imageDrinks As String
    Dim fullName As String
    Dim mealValue As Integer
    Dim totalmealValue As Integer
    Dim drinksValue As Integer
    Dim totaldrinksValue As Integer
    Dim meal As String
    Dim drinks As String
    Dim total As Integer
    Dim ordered As String
    Dim jolly As String = "C:\Users\masantelices\Downloads\jollibee.png"
    Dim totalSales As Integer
    Dim saveDate As String


    Public Sub salesTable()
        DataGridView2.Rows.Clear()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM sales", conn)
            rid = cmd.ExecuteReader
            While rid.Read
                DataGridView2.Rows.Add(rid.Item("id"), rid.Item("total_sales"), rid.Item("date"), rid.Item("note"))
            End While
        Catch ex As Exception
            MsgBox("Doesn't work lmao")
        Finally
            conn.Close()
        End Try
    End Sub
    Public Sub current()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT SUM(price) AS total FROM orders", conn)
            rid = cmd.ExecuteReader
            While rid.Read
                totalSales = rid.GetInt32("total")
            End While
        Catch ex As Exception
            totalSales = 0
        Finally
            conn.Close()
        End Try
        lblCurrentSale.Text = totalSales
    End Sub
    Public Sub product_meal()
        pickMeal.Items.Clear()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM product", conn)
            rid = cmd.ExecuteReader
            While rid.Read
                pickMeal.Items.Add(rid.Item("name"))
            End While
        Catch ex As Exception
            MsgBox("Doesn't work lmao.")
        Finally
            conn.Close()
        End Try
    End Sub

    Public Sub product_drinks()
        pickDrinks.Items.Clear()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM product_drinks", conn)
            rid = cmd.ExecuteReader
            While rid.Read
                pickDrinks.Items.Add(rid.Item("name"))
            End While
        Catch ex As Exception
            MsgBox("Doesn't work lmao.")
        Finally
            conn.Close()
        End Try
    End Sub
    Public Sub view()
        DataGridView1.Rows.Clear()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM orders", conn)
            rid = cmd.ExecuteReader
            While rid.Read
                DataGridView1.Rows.Add(rid.Item("id"), rid.Item("FULLNAME"), rid.Item("ORDERED"), rid.Item("PRICE"), rid.Item("COMMENTS"))
            End While

        Catch ex As Exception
            MsgBox("di gumana")
        Finally
            conn.Close()
        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If (txtFname.Text = "" Or txtLname.Text = "") Then
            MsgBox("Fields can't be blank.")
        ElseIf (drinksQty.Value <= 0 Or mealQty.Value <= 0 Or cash.Value <= 0) Then
            MsgBox("You cannot order without money or quantity")
        Else
            fullName = txtFname.Text & " " & txtLname.Text
            meal = pickMeal.SelectedItem
            drinks = pickDrinks.SelectedItem

            Try
                conn.Open()
                Dim cmd As New MySqlCommand("SELECT * FROM product WHERE name=@NAME", conn)
                cmd.Parameters.AddWithValue("@NAME", meal)
                rid = cmd.ExecuteReader
                While rid.Read
                    mealValue = rid.GetInt32("price")
                End While
            Catch ex As Exception
                MsgBox("Doesn't work lmao.")
            Finally
                conn.Close()
            End Try

            Try
                conn.Open()
                Dim cmd As New MySqlCommand("SELECT * FROM product_drinks WHERE name=@NAME", conn)
                cmd.Parameters.AddWithValue("@NAME", drinks)
                rid = cmd.ExecuteReader
                While rid.Read
                    drinksValue = rid.GetInt32("price")
                End While
            Catch ex As Exception
                MsgBox("Doesn't work lmao 2.")
            Finally
                conn.Close()
            End Try

            totaldrinksValue = drinksValue * drinksQty.Value
            totalmealValue = mealValue * mealQty.Value
            total = totaldrinksValue + totalmealValue
            ordered = mealQty.Value & "pc " & meal & ", " & drinksQty.Value & "pc " & drinks
            MsgBox("Your order would be " & mealQty.Value & " " & meal & ", " & drinksQty.Value &
                   " " & drinks & ", is that right?")
            lblName.Text = fullName
            lblMealName.Text = meal
            lblDrinksName.Text = drinks
            lblMealQty.Text = mealQty.Value
            lblDrinksQty.Text = drinksQty.Value
            lblMealPrice.Text = mealValue
            lblDrinksPrice.Text = drinksValue
            lblMealTotal.Text = totalmealValue
            lblDrinksTotal.Text = totaldrinksValue
            lblTotal.Text = total
            lblcash.Text = cash.Value
            lblChange.Text = cash.Value - total

            Panel2.Show()
            Panel1.Hide()
            pangel3.Hide()
            pangel4.Hide()
            pangel5.Hide()
        End If
    End Sub

    Public Sub reset()
        drinksQty.Value = 1
        mealQty.Value = 1
        cash.Value = 0
        txtFname.Text = ""
        txtLname.Text = ""
        txtCom.Text = ""

        lblName.Text = "....."
        lblMealName.Text = "....."
        lblDrinksName.Text = "....."
        lblMealQty.Text = "....."
        lblDrinksQty.Text = "....."
        lblMealPrice.Text = "....."
        lblDrinksPrice.Text = "....."
        lblMealTotal.Text = "....."
        lblDrinksTotal.Text = "....."
        lblTotal.Text = "....."
        lblcash.Text = "....."
        lblChange.Text = "....."
    End Sub
    Private Sub jollibola_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        salesTable()
        current()
        product_drinks()
        product_meal()
        view()
        reset()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        reset()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        reset()
        Panel1.Show()
        Panel2.Hide()
        pangel3.Hide()
        pangel4.Hide()
        pangel5.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Panel2.Show()
        Panel1.Hide()
        pangel3.Hide()
        pangel4.Hide()
        pangel5.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Panel1.Show()
        Panel2.Hide()
        pangel3.Hide()
        pangel4.Hide()
        pangel5.Hide()
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Panel2.Hide()
        Panel1.Hide()
        pangel3.Show()
        pangel4.Hide()
        pangel5.Hide()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("DELETE FROM orders WHERE id=@ID", conn)
            cmd.Parameters.AddWithValue("@ID", txtId.Text)
            cmd.ExecuteNonQuery()
            MsgBox("Record successfully deleted!")
        Catch ex As Exception
            MsgBox("Delete unsuccessful")
        Finally
            conn.Close()
        End Try
        view()
    End Sub

    Private Sub btnPlace_Click(sender As Object, e As EventArgs) Handles btnPlace.Click
        If (lblName.Text = ".....") Then
            MsgBox("Order incomplete, please order first thank you.")
        ElseIf (cash.Value - total < 0) Then
            MsgBox("You don't have much money.")
        Else
            Try
                conn.Open()
                Dim cmd As New MySqlCommand("INSERT INTO orders(`FULLNAME`, `ORDERED`, `PRICE`, `COMMENTS`) VALUES(@FULLNAME, @ORDERED, @PRICE, @COMMENTS)", conn)
                cmd.Parameters.AddWithValue("@FULLNAME", fullName)
                cmd.Parameters.AddWithValue("@ORDERED", ordered)
                cmd.Parameters.AddWithValue("@PRICE", total)
                cmd.Parameters.AddWithValue("@COMMENTS", txtComment.Text)
                cmd.ExecuteNonQuery()
                MsgBox("Thank you for ordering on JolliBola, enjoy your meal!")
            Catch ex As Exception
                MsgBox("gagi ayaw")
            Finally
                conn.Close()
            End Try
            view()
            reset()
            current()
            Panel2.Hide()
            Panel1.Hide()
            pangel3.Show()
            pangel4.Hide()
            pangel5.Hide()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        txtId.Text = DataGridView1.CurrentRow.Cells(0).Value
        txtFullName.Text = DataGridView1.CurrentRow.Cells(1).Value
        txtOrder.Text = DataGridView1.CurrentRow.Cells(2).Value
        txtPrice.Text = DataGridView1.CurrentRow.Cells(3).Value
        txtCom.Text = DataGridView1.CurrentRow.Cells(4).Value
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("UPDATE orders SET FULLNAME=@FNAME, ORDERED=@ORDER, PRICE=@PRICE, COMMENTS=@COMMENT WHERE id=@ID", conn)
            cmd.Parameters.AddWithValue("@FNAME", txtFullName.Text)
            cmd.Parameters.AddWithValue("@ORDER", txtOrder.Text)
            cmd.Parameters.AddWithValue("@PRICE", txtPrice.Text)
            cmd.Parameters.AddWithValue("@COMMENT", txtCom.Text)
            cmd.Parameters.AddWithValue("@ID", txtId.Text)
            cmd.ExecuteNonQuery()
            MsgBox("Record updated!")
        Catch ex As Exception
            MsgBox("Update unsuccessful!")
        Finally
            conn.Close()
        End Try
        view()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        DataGridView1.Rows.Clear()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM orders WHERE id LIKE '%" & txtSearch.Text & "%' OR FULLNAME LIKE '%" & txtSearch.Text & "%' OR ORDERED LIKE '%" & txtSearch.Text & "%' OR PRICE LIKE '%" & txtSearch.Text & "%'", conn)
            rid = cmd.ExecuteReader
            While rid.Read
                DataGridView1.Rows.Add(rid.Item("id"), rid.Item("FULLNAME"), rid.Item("ORDERED"), rid.Item("PRICE"), rid.Item("COMMENTS"))
            End While

        Catch ex As Exception
            MsgBox("Search doesn't work")
        Finally
            conn.Close()
        End Try
        If (txtSearch.Text = "") Then
            view()
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub

    Private Sub btnAddProd_Click(sender As Object, e As EventArgs) Handles btnAddProd.Click
        Panel2.Hide()
        Panel1.Hide()
        pangel3.Hide()
        pangel4.Show()
        pangel5.Hide()
    End Sub
    Private Sub btnMealReset_Click(sender As Object, e As EventArgs) Handles btnMealReset.Click
        txtAddMeal.Text = ""
        txtAddMealPrice.Value = 0
        prevMeal.BackgroundImage = System.Drawing.Image.FromFile(jolly)
    End Sub
    Private Sub btnDrinksReset_Click(sender As Object, e As EventArgs) Handles btnDrinksReset.Click
        txtAddDrinks.Text = ""
        txtAddDrinksPrice.Value = 0
        prevDrinks.BackgroundImage = System.Drawing.Image.FromFile(jolly)
    End Sub
    Private Sub btnAddMeal_Click(sender As Object, e As EventArgs) Handles btnAddMeal.Click
        If (txtAddMeal.Text = "") Then
            MsgBox("Fields can't be blank.")
        ElseIf (txtAddMealPrice.Value <= 0) Then
            MsgBox("Price can't be below 1")
        ElseIf (imageInput = "") Then
            MsgBox("Please input an image of the product.")
        Else
            Try
                conn.Open()
                Dim cmd As New MySqlCommand("INSERT INTO product(`name`, `price`, `image_path`, `created_at`, `updated_at`) VALUES(@NAME, @PRICE, @IMAGE, NOW(), NOW())", conn)
                cmd.Parameters.AddWithValue("@NAME", txtAddMeal.Text)
                cmd.Parameters.AddWithValue("@PRICE", txtAddMealPrice.Text)
                cmd.Parameters.AddWithValue("@IMAGE", imageInput)
                cmd.ExecuteNonQuery()

                MsgBox("Product has been added.")
                txtAddMealPrice.Value = 0
                txtAddMeal.Text = ""
            Catch ex As Exception
                MsgBox("Doesn't work lmao")
            Finally
                conn.Close()
            End Try
            product_meal()
        End If
    End Sub

    Private Sub btnMealImage_Click(sender As Object, e As EventArgs) Handles btnMealImage.Click
        Dim opf As New OpenFileDialog
        opf.Filter = "Choose Image(*.jpg; *.png; *.gif) | * .jpg; *.png; *.gif"

        If opf.ShowDialog = DialogResult.OK Then
            imageInput = System.IO.Path.GetFullPath(opf.FileName)
            prevMeal.BackgroundImage = System.Drawing.Image.FromFile(imageInput)
        End If
    End Sub

    Private Sub btnDrinksImage_Click(sender As Object, e As EventArgs) Handles btnDrinksImage.Click
        Dim opf As New OpenFileDialog
        opf.Filter = "Choose Image(*.jpg; *.png; *.gif) | * .jpg; *.png; *.gif"

        If opf.ShowDialog = DialogResult.OK Then
            imageInput = System.IO.Path.GetFullPath(opf.FileName)
            prevDrinks.BackgroundImage = System.Drawing.Image.FromFile(imageInput)

        End If
    End Sub

    Private Sub btnAddDrinks_Click(sender As Object, e As EventArgs) Handles btnAddDrinks.Click
        If (txtAddDrinks.Text = "") Then
            MsgBox("Fields can't be blank.")
        ElseIf (txtAddDrinksPrice.Value <= 0) Then
            MsgBox("Price can't be below 1")
        ElseIf (imageInput = "") Then
            MsgBox("Please input an image of the product.")
        Else
            Try
                conn.Open()
                Dim cmd As New MySqlCommand("INSERT INTO product_drinks(`name`, `price`, `image_path`, `created_at`, `updated_at`) VALUES(@NAME, @PRICE, @IMAGE, NOW(), NOW())", conn)
                cmd.Parameters.AddWithValue("@NAME", txtAddDrinks.Text)
                cmd.Parameters.AddWithValue("@PRICE", txtAddDrinksPrice.Text)
                cmd.Parameters.AddWithValue("@IMAGE", imageInput)
                cmd.ExecuteNonQuery()
                MsgBox("Product has been added.")
                txtAddDrinksPrice.Value = 0
                txtAddDrinks.Text = ""
            Catch ex As Exception
                MsgBox("Doesn't work lmao")
            Finally
                conn.Close()
            End Try
            product_drinks()
        End If
    End Sub

    Private Sub pickMeal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles pickMeal.SelectedIndexChanged
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM product WHERE name=@NAME", conn)
            cmd.Parameters.AddWithValue("@NAME", pickMeal.SelectedItem)
            rid = cmd.ExecuteReader
            While rid.Read
                imageMeal = rid.GetString("image_path")
                mealValue = rid.GetInt32("price")
            End While

        Catch ex As Exception
            MsgBox("Doesn't work lmao")
        Finally
            conn.Close()
        End Try
        mealTag.Text = mealValue
        PictureBox3.BackgroundImage = System.Drawing.Image.FromFile(imageMeal)
        prevMeal.BackgroundImage = System.Drawing.Image.FromFile(jolly)
        imageInput = ""
    End Sub

    Private Sub pickDrinks_SelectedIndexChanged(sender As Object, e As EventArgs) Handles pickDrinks.SelectedIndexChanged
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM product_drinks WHERE name=@NAME", conn)
            cmd.Parameters.AddWithValue("@NAME", pickDrinks.SelectedItem)
            rid = cmd.ExecuteReader
            While rid.Read
                imageDrinks = rid.GetString("image_path")
                drinksValue = rid.GetInt32("price")
            End While

        Catch ex As Exception
            MsgBox("Doesn't work lmao")
        Finally
            conn.Close()
        End Try
        drinksTag.Text = drinksValue
        PictureBox2.BackgroundImage = System.Drawing.Image.FromFile(imageDrinks)
        prevDrinks.BackgroundImage = System.Drawing.Image.FromFile(jolly)
        imageInput = ""
    End Sub

    '---------------------------------------------- SALES ---------------------------------------------------'
    Private Sub btnSales_Click(sender As Object, e As EventArgs) Handles btnSales.Click
        Panel2.Hide()
        Panel1.Hide()
        pangel3.Hide()
        pangel4.Hide()
        pangel5.Show()
    End Sub

    Private Sub btnSetSale_Click(sender As Object, e As EventArgs) Handles btnSetSale.Click
        Dim thisId As Integer
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("INSERT INTO sales(`date`, `total_sales`, `note`) VALUES(NOW(), @TOTAL, @NOTE)", conn)
            cmd.Parameters.AddWithValue("@TOTAL", totalSales)
            cmd.Parameters.AddWithValue("@NOTE", txtSalesNote.Text)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Doesn't work lmao")
        Finally
            conn.Close()
        End Try

        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM sales WHERE total_sales=@TOTAL", conn)
            cmd.Parameters.AddWithValue("@TOTAL", totalSales)
            rid = cmd.ExecuteReader
            While rid.Read
                saveDate = rid.GetString("date")
                thisId = rid.GetInt32("id")
            End While
        Catch ex As Exception
            MsgBox("Doesn't work lmao")
        Finally
            conn.Close()
        End Try

        Dim outputPath As String = "C:\Users\masantelices\source\repos\sampleCrud\bin\sales\salesreport" & thisId & ".pdf"
        ' Create a new PDF document
        Dim doc As New Document()

        ' Create a PDF writer instance
        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(outputPath, FileMode.Create))

        ' Open the PDF document for writing
        doc.Open()
        Dim titleFont = FontFactory.GetFont("Arial", 32)
        Dim salesFont = FontFactory.GetFont("Arial", 25)
        ' Add content to the PDF document
        Dim title = New Paragraph("Daily Sales Report", titleFont)
        Dim dateend = New Paragraph("Date:" & saveDate)
        Dim p1 = New Paragraph("Total sales of the day:")
        Dim p2 = New Paragraph(totalSales, salesFont)
        title.Alignment = Element.ALIGN_CENTER
        dateend.Alignment = Element.ALIGN_CENTER
        p1.Alignment = Element.ALIGN_CENTER
        p2.Alignment = Element.ALIGN_CENTER
        doc.Add(title)
        doc.Add(New Paragraph(" "))
        doc.Add(dateend)
        doc.Add(p1)
        doc.Add(p2)

        ' Close the PDF document
        doc.Close()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("UPDATE sales SET path=@PATH WHERE id=@ID", conn)
            cmd.Parameters.AddWithValue("@PATH", outputPath)
            cmd.Parameters.AddWithValue("@ID", thisId)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Doesn't work lmao2")
        Finally
            conn.Close()
        End Try
        MsgBox("File saved to " & outputPath)

        Try
            conn.Open()
            Dim cmd As New MySqlCommand("DELETE FROM orders", conn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Doesn't work lmao")
        Finally
            conn.Close()
        End Try
        view()
        current()
        salesTable()
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        Dim filePath As String
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM sales WHERE id=@ID", conn)
            cmd.Parameters.AddWithValue("@ID", txtSelectedId.Text)
            rid = cmd.ExecuteReader
            While rid.Read
                filePath = rid.GetString("path")
            End While
        Catch ex As Exception
            MsgBox("Doesn't work lmao")
        Finally
            conn.Close()
        End Try


        ' Check if the file exists
        If System.IO.File.Exists(filePath) Then
            ' Open the PDF file using the default PDF viewer
            Process.Start(filePath)
        Else
            MessageBox.Show("The specified file does not exist.")
        End If
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        txtSelectedId.Text = DataGridView2.CurrentRow.Cells(0).Value
    End Sub
End Class