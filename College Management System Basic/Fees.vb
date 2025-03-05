Imports System.Data.SqlClient

Public Class Fees
    Private Sub Fees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStudentID()
        'LoadData()

    End Sub


    'Private Sub LoadData()
    '    Try
    '        Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
    '            Con.Open()
    '            Dim cmd As New SqlCommand("GetTeachers", Con)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            Using da As New SqlDataAdapter(cmd)
    '                Dim table As New DataTable()
    '                da.Fill(table)
    '                GunaDataGridView1.DataSource = table
    '            End Using
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show("Error: " & ex.Message)
    '    End Try
    'End Sub

    Private Sub LoadStudentID()
        Try
            Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                Con.Open()
                Dim cmd As New SqlCommand("Select_Student", Con)
                cmd.CommandType = CommandType.StoredProcedure
                Using dr As SqlDataReader = cmd.ExecuteReader()
                    GenderTC.Items.Clear()
                    While dr.Read()
                        GenderTC.Items.Add(dr("StId").ToString())
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
End Class