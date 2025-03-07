Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class Login

    Dim Con As New SqlConnection("Data Source=LAPTOP-H9PTCLCF\SQLEXPRESS; Initial Catalog=CollegeVbOb; Integrated Security=True; Encrypt=True; TrustServerCertificate=True")

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Application.Exit()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        UsernameTb.Text = ""
        PasswordTb.Text = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim username As String = UsernameTb.Text
        Dim password As String = PasswordTb.Text

        If String.IsNullOrEmpty(username) Or String.IsNullOrEmpty(password) Then
            MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu.")
            Return
        End If

        'If Not IsPasswordValid(password) Then
        '    MessageBox.Show("Mật khẩu không hợp lệ. Vui lòng nhập mật khẩu có ít nhất 8 ký tự, bao gồm chữ viết hoa, ký tự đặc biệt và chữ số.")
        '    Return
        'End If

        Try
            Con.Open()
            Dim query As String = "SELECT COUNT(*) FROM UsersAdmin WHERE UserName = @UserName AND Password = @Password"
            Using cmd As New SqlCommand(query, Con)
                cmd.Parameters.AddWithValue("@UserName", username)
                cmd.Parameters.AddWithValue("@Password", password)

                Dim count As Integer = CInt(cmd.ExecuteScalar())

                If count > 0 Then
                    MessageBox.Show("Đăng nhập thành công!")
                    Me.Hide()
                    Dim mainForm As New Dashboard()
                    mainForm.Show()
                Else
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.")
                End If
            End Using
            Con.Close()
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub

    'Private Function IsPasswordValid(password As String) As Boolean
    '    If password.Length < 8 Then
    '        Return False
    '    End If
    '    If Not Regex.IsMatch(password, "[A-Z]") Then
    '        Return False
    '    End If
    '    If Not Regex.IsMatch(password, "[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]") Then
    '        Return False
    '    End If
    '    If Not Regex.IsMatch(password, "") Then
    '        Return False
    '    End If
    '    Return True
    'End Function
End Class