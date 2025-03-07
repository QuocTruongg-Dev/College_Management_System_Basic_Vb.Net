Imports System.Data.SqlClient

Public Class Departments

    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If DepNameTb.Text = "" Or DescTb.Text = "" Or DurationTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            Try
                Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                    Con.Open()
                    Dim cmd As New SqlCommand("InsertDepartment", Con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@DepName", DepNameTb.Text)
                    cmd.Parameters.AddWithValue("@Desc", DescTb.Text)
                    cmd.Parameters.AddWithValue("@Duration", DurationTb.Text)
                    cmd.ExecuteNonQuery()
                    MsgBox("Department Saved Successfully")
                    LoadData()
                End Using
            Catch ex As Exception
                MsgBox("Error: " & ex.Message & vbCrLf & "Stack Trace: " & ex.StackTrace)
            End Try
        End If
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Application.Exit()
    End Sub

    Private Sub LoadData()
        Try
            Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                Con.Open()
                Dim cmd As New SqlCommand("GetDepartments", Con)
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

    Private Sub Reset()
        DepNameTb.Text = ""
        DurationTb.Text = ""
        DescTb.Text = ""
    End Sub

    Private Sub ResetBtn_Click(sender As Object, e As EventArgs) Handles ResetBtn.Click
        Reset()
    End Sub

    Private Sub Departments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub EditBtn_Click(sender As Object, e As EventArgs) Handles EditBtn.Click
        If GunaDataGridView1.SelectedRows.Count = 0 Then
            MsgBox("Select the Department to Edit")
        Else
            Try
                Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                    Con.Open()
                    Dim selectedRow As DataGridViewRow = GunaDataGridView1.SelectedRows(0)
                    Dim depId As Integer = Convert.ToInt32(selectedRow.Cells(0).Value)
                    Dim cmd As New SqlCommand("UpdateDepartment", Con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@DepId", depId)
                    cmd.Parameters.AddWithValue("@DepName", DepNameTb.Text)
                    cmd.Parameters.AddWithValue("@Desc", DescTb.Text)
                    cmd.Parameters.AddWithValue("@Duration", DurationTb.Text)
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
            MsgBox("Select the Department to Delete")
        Else
            Try
                Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                    Con.Open()
                    Dim selectedRow As DataGridViewRow = GunaDataGridView1.SelectedRows(0)
                    Dim depId As Integer = Convert.ToInt32(selectedRow.Cells(0).Value)
                    Dim cmd As New SqlCommand("DeleteDepartment", Con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@DepId", depId)
                    cmd.ExecuteNonQuery()
                    MsgBox("Department Deleted Successfully")
                    LoadData()
                    Reset()
                End Using
            Catch ex As Exception
                MsgBox("Error: " & ex.Message & vbCrLf & "Stack Trace: " & ex.StackTrace)
            End Try
        End If
    End Sub

    Private Sub GunaDataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GunaDataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = GunaDataGridView1.Rows(e.RowIndex)

            DepNameTb.Text = row.Cells(1).Value.ToString()
            DescTb.Text = row.Cells(2).Value.ToString()
            DurationTb.Text = row.Cells(3).Value.ToString()
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
        Dim Obj = New Fees()
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Dim Obj = New Dashboard()
        Obj.Show()
        Me.Hide()
    End Sub
End Class