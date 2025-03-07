Imports System.Data.SqlClient

Public Class Dashboard
    Private Sub DisplayStudentCount()
        Try
            Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                Con.Open()
                Dim query As String = "SELECT COUNT(*) FROM StudentTbl"
                Using cmd As New SqlCommand(query, Con)
                    Dim studentCount As Integer = CInt(cmd.ExecuteScalar())
                    NumStudent.Text = studentCount.ToString()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Lỗi hiển thị số lượng sinh viên: " & ex.Message)
        End Try
    End Sub
    Private Sub DisplayTeacherCount()
        Try
            Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                Con.Open()
                Dim query As String = "SELECT COUNT(*) FROM TeacherTbl"
                Using cmd As New SqlCommand(query, Con)
                    Dim teacherCount As Integer = CInt(cmd.ExecuteScalar())
                    NumTeacher.Text = teacherCount.ToString()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Lỗi hiển thị số lượng giáo viên: " & ex.Message)
        End Try
    End Sub

    Private Sub DisplayDepartmentCount()
        Try
            Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                Con.Open()
                Dim query As String = "SELECT COUNT(DISTINCT DepId) FROM DepartmentTbl"
                Using cmd As New SqlCommand(query, Con)
                    Dim departmentCount As Integer = CInt(cmd.ExecuteScalar())
                    NumDepart.Text = departmentCount.ToString()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Lỗi hiển thị số lượng phòng ban: " & ex.Message)
        End Try
    End Sub

    Private Sub DisplayFeesCollected()
        Try
            Using Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")
                Con.Open()
                Dim query As String = "SELECT SUM(Amount) FROM PaymentTbl"
                Using cmd As New SqlCommand(query, Con)
                    Dim feesCollected As Decimal = CDec(cmd.ExecuteScalar())
                    NumFees.Text = feesCollected.ToString()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Lỗi hiển thị học phí đã thu: " & ex.Message)
        End Try
    End Sub

    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisplayStudentCount()
        DisplayTeacherCount()
        DisplayDepartmentCount()
        DisplayFeesCollected()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Dim Obj = New Login()
        Obj.Show()
        Me.Hide()
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
        Dim Obj = New Fees()
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Application.Exit()
    End Sub
End Class