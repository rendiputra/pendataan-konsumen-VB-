Imports System.Data.Odbc
'pendeklarasikan variable
Public Class Form1
    Dim CONN As OdbcConnection
    Dim CMD As OdbcCommand
    Dim DS As New DataSet
    Dim DA As OdbcDataAdapter
    Dim RD As OdbcDataReader
    Dim LokasiData As String
    'membuat koneksi ke database "dbpembukuan"
    Sub koneksi()
        LokasiData = "Driver={MySQL ODBC 3.51 Driver}; Database=dbpendataan;server=localhost; uid=root"
        CONN = New OdbcConnection(LokasiData)
        If CONN.State = ConnectionState.Closed Then
            CONN.Open()
        End If
    End Sub
    'Proses Menampilkan Data Tabel tkonsumen Ke datagrid
    Sub TampilGrid1()
        Call koneksi()
        DA = New OdbcDataAdapter("select * from tkonsumen", CONN)
        DS = New DataSet
        DA.Fill(DS, "tkonsumen")
        DataGridView1.DataSource = DS.Tables("tkonsumen")
        DataGridView1.ReadOnly = True
    End Sub
    'proses refresh data grid
    Private Sub refreshDatagrid1()
        Try
            Call koneksi()
            DS = New DataSet
            DA = New OdbcDataAdapter("SELECT * FROM tkonsumen", CONN)
            DA.Fill(DS, "tkonsumen")
            DataGridView1.Columns(1).HeaderText = "Tanggal"
            DataGridView1.Columns(2).HeaderText = "Nama Konsumen"
            DataGridView1.Columns(3).HeaderText = "Alamat Konsumen"
            DataGridView1.Columns(4).HeaderText = "Nama Marketing"
            DataGridView1.Columns(5).HeaderText = "Nama Teknisi"
            Dim GridView As New DataView(DS.Tables("tkonsumen"))
            DataGridView1.DataSource = GridView
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    'proses load form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.WindowState = FormWindowState.Maximized
        Call TampilGrid1()
        Call aturDGV()
        inputdata.txttanggal.Text = ""
        Timer1.Enabled = True
        Label1.Text = "Selamat Datang " & login.txtuser.Text & " Di Aplikasi Pendataan Konsumen."
    End Sub
    'menampilkan waktu pada label 3, Dan 4
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = Format(Now, “dddd, dd/MM/yyyy”)
        Label4.Text = Format(Now, “HH:mm:ss”)
    End Sub
    'Proses Mengatur Desain Data Grid
    Sub aturDGV()
        DataGridView1.RowsDefaultCellStyle.BackColor = Color.DarkCyan
        DataGridView1.RowsDefaultCellStyle.ForeColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.DarkCyan
        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(1).Width = 95
        DataGridView1.Columns(2).Width = 165
        DataGridView1.Columns(3).Width = 240
        DataGridView1.Columns(4).Width = 140
        DataGridView1.Columns(5).Width = 141
        DataGridView1.Columns(6).Visible = False
        DataGridView1.Columns(7).Visible = False
        DataGridView1.Columns(8).Visible = False
        DataGridView1.Columns(9).Visible = False
        DataGridView1.Columns(10).Visible = False
    End Sub
    'Proses Refres data grid
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnuser.Click
        register.Show()
        Me.Hide()
    End Sub
    'proses cari Berdasarkan Nama, Alamat, Nama_Marketing, Nama Teknisi, Tanggal
    Sub cari()
        Call koneksi()
        DA = New OdbcDataAdapter("Select * from tkonsumen Where Nama_Konsumen like '%" & txtcari.Text & "%' OR Alamat_Konsumen like '%" & txtcari.Text & "%' OR Nama_Marketing like '%" & txtcari.Text & "%' OR Nama_Teknisi like '%" & txtcari.Text & "%' OR Tanggal like '%" & txtcari.Text & "%'", CONN)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tkonsumen")
        DataGridView1.DataSource = (DS.Tables("tkonsumen"))
        Dim tampil1 As String
        tampil1 = "select * from tkonsumen where Nama_Konsumen ='" & txtcari.Text & "'"
        CMD = New OdbcCommand(tampil1, CONN)
        RD = CMD.ExecuteReader
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        data_konsumen.refreshDatagrid()
        data_konsumen.Show()
        Me.Hide()
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        inputdata.Show()
        Me.Hide()
        Call inputdata.kosongkandata()
    End Sub
    'Proses Keluar
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If MessageBox.Show("Apakah Anda Ingin Keluar?", "Konfirmasi Keluar Aplikasi", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
            End
        End If
    End Sub
    Private Sub txtcari_TextChanged(sender As Object, e As EventArgs) Handles txtcari.TextChanged
        Call cari()
    End Sub
End Class