Imports System.Data.Odbc
Public Class login
    Dim CONN As OdbcConnection
    Dim CMD As OdbcCommand
    Dim DS As New DataSet
    Dim DA As OdbcDataAdapter
    Dim RD As OdbcDataReader
    Dim LokasiData As String
    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Visible = False
        Label3.Visible = False
    End Sub
    'placeholder
    Private Sub txtuser_MouseEnter(sender As Object, e As EventArgs) Handles txtuser.MouseEnter
        If txtuser.Text = "Username" Then
            txtuser.Text = ""
            txtuser.MaxLength = 20
        End If
    End Sub
    Private Sub txtuser_MouseLeave(sender As Object, e As EventArgs) Handles txtuser.MouseLeave
        If txtuser.Text = "" Then
            txtuser.Text = "Username"
        End If
    End Sub
    Private Sub txtpw_MouseLeave(sender As Object, e As EventArgs) Handles txtpw.MouseLeave
        If txtpw.Text = "" Then
            txtpw.Text = "Password"
            txtpw.PasswordChar = ""
        End If
    End Sub
    Private Sub txtpw_MouseEnter(sender As Object, e As EventArgs) Handles txtpw.MouseEnter
        If txtpw.Text = "Password" Then
            txtpw.Text = ""
            txtpw.PasswordChar = "*"
            txtpw.MaxLength = 30
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        txtuser.Text = "Username"
        txtpw.Text = "Password"
    End Sub
    Private Sub txtpw_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtpw.KeyPress
        txtpw.PasswordChar = "*"
    End Sub
    'biar bisa pake enter
    Private Sub txtpw_KeyDown(sender As Object, e As KeyEventArgs) Handles txtpw.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub
    'script koneksi dbpendataan
    Sub koneksi()
        LokasiData = "Driver={MySQL ODBC 3.51 Driver}; Database=dbpendataan;server=localhost; uid=root"
        CONN = New OdbcConnection(LokasiData)
        If CONN.State = ConnectionState.Closed Then
            CONN.Open()
        End If
    End Sub
    'script login dan Hak Akses
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call koneksi()
        Dim myAdapter As New OdbcDataAdapter
        Dim sqlQuery = "SELECT * FROM tlogin WHERE username='" + txtuser.Text + "' AND password='" + txtpw.Text + "'"
        Dim CMD As New OdbcCommand
        CMD.Connection = CONN
        CMD.CommandText = sqlQuery
        myAdapter.SelectCommand = CMD
        Dim myData As OdbcDataReader
        myData = CMD.ExecuteReader()
        If myData.HasRows = 0 Then
            MsgBox("username dan password salah!! ",
                   MsgBoxStyle.Exclamation, "Error Login")
        Else
            CONN.Close()
            Call koneksi()
            CMD = New OdbcCommand("SELECT * FROM tlogin WHERE username='" + txtuser.Text + "' AND password='" + txtpw.Text + "'", CONN)
            MsgBox("Login berhasil, Selamat datang " & txtuser.Text & "!", MsgBoxStyle.Information, "Successfull Login")
            RD = CMD.ExecuteReader()
            RD.Read()
            Label2.Text = RD.Item("gambar")
            Label3.Text = RD.Item("level")
            Form1.txtadmin.ImageLocation = RD.Item("gambar") 'untuk mencari lokasi gambar pada direktori
            inputdata.txtadmin.ImageLocation = RD.Item("gambar")
            data_konsumen.txtadmin.ImageLocation = RD.Item("gambar")
            Form1.Show()
            Me.Hide()
            If Label3.Text = 1 Then
            Else
                Form1.PictureBox1.Visible = False
                data_konsumen.PictureBox1.Visible = False
                inputdata.PictureBox1.Visible = False
                Form1.btnuser.Visible = False
                data_konsumen.btnuser.Visible = False
                inputdata.btnuser.Visible = False
            End If
        End If
    End Sub
End Class