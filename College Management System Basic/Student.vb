Imports System.Data.SqlClient
Imports System.Drawing.Printing

Public Class Student
    Private printDoc As New PrintDocument()
    Private Sub Departments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
        LoadGenders()
        LoadDepartments()
    End Sub

    Private Sub LoadGenders()
        GenderST.Items.Clear()
        GenderST.Items.Add("Male")
        GenderST.Items.Add("Female")
        GenderFilter.Items.Add("Male")
        GenderFilter.Items.Add("Female")
    End Sub

    Private Sub LoadDepartments()
        Try
            Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                Con.Open()
                Dim cmd As New SqlCommand("Select_Departments", Con)
                cmd.CommandType = CommandType.StoredProcedure
                Using dr As SqlDataReader = cmd.ExecuteReader()
                    DepartST.Items.Clear()
                    While dr.Read()
                        DepartST.Items.Add(dr("DepName").ToString())
                    End While
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading departments: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadData()
        Try
            Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                Con.Open()
                Dim cmd As New SqlCommand("GetStudents", Con)
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

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Application.Exit()
    End Sub


    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If NameST.Text = "" Or GenderST.SelectedItem Is Nothing Or DepartST.SelectedItem Is Nothing Or FeesST.Text = "" Or PhoneST.Text = "" Then
            MsgBox("Please fill in all fields.")
        Else
            Try
                Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                    Con.Open()
                    Dim cmd As New SqlCommand("InsertStudent", Con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@Stname", NameST.Text)
                    cmd.Parameters.AddWithValue("@StGender", GenderST.SelectedItem.ToString())
                    cmd.Parameters.AddWithValue("@StDOB", DobST.Value.Date)
                    cmd.Parameters.AddWithValue("@StDep", DepartST.SelectedItem.ToString())
                    cmd.Parameters.AddWithValue("@Fees", Convert.ToDecimal(FeesST.Text))
                    cmd.Parameters.AddWithValue("@StPhone", PhoneST.Text)
                    cmd.ExecuteNonQuery()
                    MsgBox("Data saved successfully.")
                    LoadData()
                    Reset()
                End Using
            Catch ex As Exception
                MsgBox("Error: " & ex.Message & vbCrLf & "Stack Trace: " & ex.StackTrace)
            End Try
        End If
    End Sub

    Private Sub EditBtn_Click(sender As Object, e As EventArgs) Handles EditBtn.Click
        If GunaDataGridView1.SelectedRows.Count = 0 Then
            MsgBox("Select the Department to Edit")
        Else
            Try
                Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                    Con.Open()
                    Dim selectedRow As DataGridViewRow = GunaDataGridView1.SelectedRows(0)
                    Dim Stld As Integer = Convert.ToInt32(selectedRow.Cells(0).Value)
                    Dim cmd As New SqlCommand("UpdateStudent", Con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@StId", Stld)
                    cmd.Parameters.AddWithValue("@Stname", NameST.Text)
                    cmd.Parameters.AddWithValue("@StGender", GenderST.SelectedItem.ToString())
                    cmd.Parameters.AddWithValue("@StDOB", DobST.Value.Date)
                    cmd.Parameters.AddWithValue("@StDep", DepartST.SelectedItem.ToString())
                    cmd.Parameters.AddWithValue("@Fees", Convert.ToDecimal(FeesST.Text))
                    cmd.Parameters.AddWithValue("@StPhone", PhoneST.Text)
                    cmd.ExecuteNonQuery()
                    MsgBox("Department Updated Successfully")
                    LoadData()
                End Using
            Catch ex As Exception
                MsgBox("Error: " & ex.Message & vbCrLf & "Stack Trace: " & ex.StackTrace)
            End Try
        End If
    End Sub

    Private Sub DeleteBtn_Click(sender As Object, e As EventArgs) Handles DeleteBtn.Click
        If GunaDataGridView1.SelectedRows.Count = 0 Then
            MsgBox("Select the Student to Delete")
        Else
            Try
                Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                    Con.Open()
                    Dim selectedRow As DataGridViewRow = GunaDataGridView1.SelectedRows(0)
                    Dim Stld As Integer = Convert.ToInt32(selectedRow.Cells(0).Value)
                    Dim cmd As New SqlCommand("DeleteStudent", Con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@Stld", Stld)
                    cmd.ExecuteNonQuery()
                    MsgBox("Student Deleted Successfully")
                    LoadData()
                    Reset()
                End Using
            Catch ex As Exception
                MsgBox("Error: " & ex.Message & vbCrLf & "Stack Trace: " & ex.StackTrace)
            End Try
        End If
    End Sub

    Private Sub Reset()
        NameST.Text = ""
        GenderST.Text = ""
        DobST.Text = ""
        DepartST.Text = ""
        PhoneST.Text = ""
        FeesST.Text = ""
    End Sub

    Private Sub ResetBtn_Click(sender As Object, e As EventArgs) Handles ResetBtn.Click
        Reset()
    End Sub

    Private Sub GunaDataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GunaDataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = GunaDataGridView1.Rows(e.RowIndex)

            NameST.Text = row.Cells(1).Value.ToString()
            GenderST.Text = row.Cells(2).Value.ToString()
            DobST.Text = row.Cells(3).Value.ToString()
            DepartST.Text = row.Cells(5).Value.ToString()
            FeesST.Text = row.Cells(6).Value.ToString()
            PhoneST.Text = row.Cells(4).Value.ToString()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim adapter As New SqlDataAdapter()
        NoDueList(adapter)
    End Sub

    Private Sub NoDueList(adapter As SqlDataAdapter)
        Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
            Con.Open()
            Dim query = "Select * from StudentTbl where StFees >= 100000"
            Dim cmd As New SqlCommand(query, Con)
            adapter.SelectCommand = cmd
            Dim ds As DataSet = New DataSet
            adapter.Fill(ds)
            GunaDataGridView1.DataSource = ds.Tables(0)
            Con.Close()
        End Using
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        LoadData()
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
        Dim Obj = New Fees()
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Dim Obj = New Dashboard()
        Obj.Show()
        Me.Hide()
    End Sub

    'reload ra theo gender for student
    Private Sub Gender_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GenderFilter.SelectedIndexChanged
        Dim gender As String = GenderFilter.SelectedItem.ToString() ' Lấy giá trị được chọn từ ComboBox

        Try
            Using con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                con.Open()

                Dim cmd As New SqlCommand("SELECT * FROM StudentTbl WHERE StGender = @gender", con)
                cmd.Parameters.AddWithValue("@gender", gender)

                Using da As New SqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    da.Fill(table)
                    GunaDataGridView1.DataSource = table
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub
End Class