Imports System.Data.SqlClient

Public Class Teachers
    Private Sub Teachers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGenders()
        LoadData()

    End Sub

    Private Sub LoadGenders()
        GenderTC.Items.Clear()
        GenderTC.Items.Add("Male")
        GenderTC.Items.Add("Female")
    End Sub

    Private Sub LoadData()
        Try
            Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                Con.Open()
                Dim cmd As New SqlCommand("GetTeachers", Con)
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

    'Private Sub LoadStudentID()
    '    Try
    '        Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
    '            Con.Open()
    '            Dim cmd As New SqlCommand("Select_Student", Con)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            Using dr As SqlDataReader = cmd.ExecuteReader()
    '                GenderTC.Items.Clear()
    '                While dr.Read()
    '                    GenderTC.Items.Add(dr("StId").ToString())
    '                End While
    '            End Using
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show("Error loading departments: " & ex.Message)
    '    End Try
    'End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Application.Exit()
    End Sub

    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If AdressTC.Text = "" Or GenderTC.SelectedItem Is Nothing Or NameTC.Text = " " Or DobTC.Text = "" Or DepartTC.Text = "" Or PhoneTC.Text = "" Then
            MsgBox("Please fill in all fields.")
        Else
            Try
                Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                    Con.Open()
                    Dim cmd As New SqlCommand("InsertTeacher", Con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@NameTC", NameTC.Text)
                    cmd.Parameters.AddWithValue("@GenderTC", GenderTC.SelectedItem.ToString())
                    cmd.Parameters.AddWithValue("@DobTC", DobTC.Value.Date)
                    cmd.Parameters.AddWithValue("@AdressTC", AdressTC.Text)
                    cmd.Parameters.AddWithValue("@PhoneTC", PhoneTC.Text)
                    cmd.Parameters.AddWithValue("@DepartTC", DepartTC.Text)
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
                    Dim Tld As Integer = Convert.ToInt32(selectedRow.Cells(0).Value)
                    Dim cmd As New SqlCommand("UpdateTeacher", Con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@Tld", Tld)
                    cmd.Parameters.AddWithValue("@NameTC", NameTC.Text)
                    cmd.Parameters.AddWithValue("@GenderTC", GenderTC.SelectedItem.ToString())
                    cmd.Parameters.AddWithValue("@DobTC", DobTC.Value.Date)
                    cmd.Parameters.AddWithValue("@AdressTC", AdressTC.Text)
                    cmd.Parameters.AddWithValue("@PhoneTC", PhoneTC.Text)
                    cmd.Parameters.AddWithValue("@DepartTC", DepartTC.Text)
                    cmd.ExecuteNonQuery()
                    cmd.ExecuteNonQuery()
                    MsgBox("Teacher Updated Successfully")
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
                    Dim Tld As Integer = Convert.ToInt32(selectedRow.Cells(0).Value)
                    Dim cmd As New SqlCommand("DeleteTeacher", Con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@Tld", Tld)
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
        GenderTC.Text = ""
        NameTC.Text = ""
        DobTC.Text = ""
        AdressTC.Text = ""
        DepartTC.Text = ""
        PhoneTC.Text = ""
    End Sub

    Private Sub ResetBtn_Click(sender As Object, e As EventArgs) Handles ResetBtn.Click
        Reset()
    End Sub

    Private Sub GunaDataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GunaDataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = GunaDataGridView1.Rows(e.RowIndex)

            NameTC.Text = row.Cells(1).Value.ToString()
            GenderTC.Text = row.Cells(2).Value.ToString()
            DobTC.Text = row.Cells(3).Value.ToString()
            DepartTC.Text = row.Cells(5).Value.ToString()
            AdressTC.Text = row.Cells(6).Value.ToString()
            PhoneTC.Text = row.Cells(4).Value.ToString()
        End If
    End Sub


End Class