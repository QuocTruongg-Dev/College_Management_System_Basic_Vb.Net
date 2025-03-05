Imports System.Data.SqlClient

Public Class Student
    Private Sub Departments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
        LoadGenders()
        LoadDepartments()
    End Sub

    Private Sub LoadGenders()
        GenderST.Items.Clear()
        GenderST.Items.Add("Male")
        GenderST.Items.Add("Female")
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


End Class