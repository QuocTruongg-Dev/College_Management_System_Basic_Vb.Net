Imports System.Data.SqlClient

Public Class Fees
    Private Sub Fees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStudentID()
        LoadData()

    End Sub


    Private Sub LoadData()
        Try
            Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                Con.Open()
                Dim cmd As New SqlCommand("GetPayment", Con)
                cmd.CommandType = CommandType.StoredProcedure
                Using da As New SqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    da.Fill(table)
                    GunaDataGridView1.DataSource = table
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadStudentID()
        Try
            Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                Con.Open()
                Dim cmd As New SqlCommand("Select_Student", Con)
                cmd.CommandType = CommandType.StoredProcedure
                Using dr As SqlDataReader = cmd.ExecuteReader()
                    IdStudentF.Items.Clear()
                    While dr.Read()
                        IdStudentF.Items.Add(dr("StId").ToString())
                    End While
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading departments: " & ex.Message)
        End Try
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Application.Exit()
    End Sub

    Private Sub IdStudentF_SelectedIndexChanged(sender As Object, e As EventArgs) Handles IdStudentF.SelectedIndexChanged
        ' Kiểm tra xem ComboBox đã chọn một giá trị chưa
        If IdStudentF.SelectedIndex <> -1 Then
            ' Lấy ID sinh viên được chọn
            Dim studentId As Integer = Integer.Parse(IdStudentF.SelectedItem.ToString())
            Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                Con.Open()
                Dim query As String = "SELECT StName FROM StudentTbl WHERE StId = @StudentId"
                Using cmd As New SqlCommand(query, Con)
                    cmd.Parameters.AddWithValue("@StudentId", studentId)
                    Dim studentName As String = cmd.ExecuteScalar()
                    If Not String.IsNullOrEmpty(studentName) Then
                        NameF.Text = studentName
                    Else
                        NameF.Text = ""
                    End If
                End Using
                Con.Close()
            End Using
        End If
    End Sub

    Private Sub PayBtn_Click(sender As Object, e As EventArgs) Handles PayBtn.Click
        If NameF.Text = "" Or IdStudentF.SelectedItem Is Nothing Or AmountF.Text = "" Then
            MsgBox("Please fill in all fields.")
        Else
            Try
                Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                    Con.Open()
                    Dim cmd As New SqlCommand("InsertPayment", Con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@StdId", IdStudentF.SelectedItem.ToString())
                    cmd.Parameters.AddWithValue("@StdName", NameF.Text)
                    cmd.Parameters.AddWithValue("@Period", PeriodF.Value.Date)
                    cmd.Parameters.AddWithValue("@Amount", AmountF.Text)
                    cmd.ExecuteNonQuery()
                    MsgBox("Payment Saved successfully.")
                    LoadData()
                End Using
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click
        Dim Obj = New Teachers()
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Dim Obj = New Departments()
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Dim Obj = New Student()
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Dim Obj = New Dashboard()
        Obj.Show()
        Me.Hide()
    End Sub
End Class