Imports System.Data.Odbc
'mendeklarasikan Variable
Public Class data_konsumen
    Dim CONN As OdbcConnection
    Dim CMD As OdbcCommand
    Dim DS As New DataSet
    Dim DA As OdbcDataAdapter
    Dim RD As OdbcDataReader
    Dim LokasiData As String
    'Membuat Koneksi Ke database "dbpembukuan"
    Sub koneksi()
        LokasiData = "Driver={MySQL ODBC 3.51 Driver}; Database=dbpendataan; server=localhost; uid=root"
        CONN = New OdbcConnection(LokasiData)
        If CONN.State = ConnectionState.Closed Then
            CONN.Open()
        End If
    End Sub
    'Proses Menampilkan tabel tkonsumen pada Data Grid
    Sub TampilGrid()
        Call koneksi()
        DA = New OdbcDataAdapter("select * from tkonsumen", CONN)
        DS = New DataSet
        DA.Fill(DS, "tkonsumen")
        DataGridView1.DataSource = DS.Tables("tkonsumen")
        DataGridView1.ReadOnly = True
    End Sub
    'proses auto complete pada Textbox apabila id di db sama dengan txtid di form"
    Sub tampiltextbox()
        CONN.Close()
        Call koneksi()
        CMD = New OdbcCommand("SELECT * FROM tkonsumen WHERE id='" & txtid.Text & "'", CONN)
        RD = CMD.ExecuteReader()
        RD.Read()
        If RD.HasRows Then
            inputdata.txttanggal.Text = RD.Item("Tanggal")
            inputdata.txtnama.Text = RD.Item("Nama_Konsumen")
            inputdata.txtalamat.Text = RD.Item("Alamat_Konsumen")
            inputdata.txtmarketing.Text = RD.Item("Nama_Marketing")
            'inputdata.txtteknisi.Text = RD.Item("Nama_Teknisi")
            inputdata.txtantena.Text = RD.Item("Laba_Antena")
            inputdata.txtparabola.Text = RD.Item("Laba_Parabola")
            inputdata.txtcctv.Text = RD.Item("Laba_CCTV")
            inputdata.txtpetir.Text = RD.Item("Laba_Petir")
            inputdata.txttotal.Text = RD.Item("Total_Laba")
            inputdata.jmlantena.Text = Val(inputdata.txtantena.Text) / 50000
            inputdata.jmlcctv.Text = Val(inputdata.jmlcctv.Text) / 200000
            inputdata.jmlparabola.Text = Val(inputdata.txtparabola.Text) / 100000
            inputdata.jmlpetir.Text = Val(inputdata.txtpetir.Text) / 400000
        End If
    End Sub
    'Desain DGV
    Sub aturDGV()
        DataGridView1.RowsDefaultCellStyle.BackColor = Color.DarkCyan
        DataGridView1.RowsDefaultCellStyle.ForeColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.DarkCyan
        DataGridView1.Columns(0).Width = 60
        DataGridView1.Columns(1).Width = 195
        DataGridView1.Columns(2).Width = 165
        DataGridView1.Columns(3).Width = 160
        DataGridView1.Columns(4).Width = 140
        DataGridView1.Columns(5).Width = 105
        DataGridView1.Columns(6).Width = 100
        DataGridView1.Columns(7).Width = 100
        DataGridView1.Columns(8).Width = 100
        DataGridView1.Columns(9).Width = 100
        DataGridView1.Columns(10).Width = 100
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.Show()
        Me.Hide()
    End Sub
    Private Sub data_konsumen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call TampilGrid()
        Call aturDGV()
    End Sub
    'Menampilkan Tanggal di label3
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = Format(Now, “dddd, dd MMMMM yyyy”)
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        inputdata.Show()
        Me.Hide()
        Call inputdata.kosongkandata()
    End Sub
    'proses refresh dg1
    Sub refreshDatagrid()
        Try
            Call koneksi()
            DS = New DataSet
            DA = New OdbcDataAdapter("SELECT * FROM tkonsumen", CONN)
            DA.Fill(DS, "tkonsumen")
            DataGridView1.Columns(0).HeaderText = "ID"
            DataGridView1.Columns(1).HeaderText = "Tanggal"
            DataGridView1.Columns(2).HeaderText = "Nama Konsumen"
            DataGridView1.Columns(3).HeaderText = "Alamat Konsumen"
            DataGridView1.Columns(4).HeaderText = "Nama Marketing"
            DataGridView1.Columns(5).HeaderText = "Nama Teknisi"
            DataGridView1.Columns(6).HeaderText = "Laba Antena"
            DataGridView1.Columns(7).HeaderText = "Laba Parabola"
            DataGridView1.Columns(8).HeaderText = "Laba CCTV"
            DataGridView1.Columns(9).HeaderText = "Laba Petir"
            DataGridView1.Columns(10).HeaderText = "Total Laba"
            txtid.Text = ""
            Dim GridView As New DataView(DS.Tables("tkonsumen"))
            DataGridView1.DataSource = GridView
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    'menghapus Data Berdasarkan id pada txtid
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If txtid.Text = "" Then
            MessageBox.Show("Silahkan Pilih Data Yang Akan DIHAPUS dengan masukkan ID", "Pendataan Konsumen", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            If MessageBox.Show("Apakah Anda Ingin Menghapus Data?", "Konfirmasi Perubahan Data", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                Call koneksi()
                Dim hapus As String = "delete from tkonsumen where id='" & txtid.Text & "'"
                CMD = New OdbcCommand(hapus, CONN)
                CMD.ExecuteNonQuery()
                Call TampilGrid()
                txtid.Text = ""
            End If
        End If
    End Sub
    'proses pengiriman edit ke "inputdata" dengan membawa nilai id di "data_konsumen"
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If inputdata.txtid.Text = txtid.Text Or txtid.Text = inputdata.txtid.Text Then
            inputdata.btninput.Enabled = True
            inputdata.btnedit.Enabled = False
        Else
            inputdata.btnedit.Enabled = True
            inputdata.btninput.Enabled = False
        End If
        If DataGridView1.Rows.Count > 1 AndAlso DataGridView1.Rows IsNot Nothing Then
            inputdata.txtid.Text = txtid.Text
            Call tampiltextbox()
            inputdata.Show()
            Me.Hide()
        Else
            MessageBox.Show("ID data konsumen tidak ditemukan", "Pendataan Konsumen", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    'proses cari data berdasarkan id, nama, alamat, nama marketing, Nama Teknisi, Tanggal
    Sub cari()
        Call koneksi()
        DA = New OdbcDataAdapter("Select * from tkonsumen Where id like '%" & txtcari.Text & "%' OR nama_konsumen like '%" & txtcari.Text & "%' OR Nama_Marketing like '%" & txtcari.Text & "%' OR Nama_Teknisi like '%" & txtcari.Text & "%' OR Tanggal like '%" & txtcari.Text & "%' OR Alamat_konsumen like '%" & txtcari.Text & "%'", CONN)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tkonsumen")
        DataGridView1.DataSource = (DS.Tables("tkonsumen"))
        Dim tampil1 As String
        tampil1 = "select * from tkonsumen where id ='" & txtcari.Text & "'"
        CMD = New OdbcCommand(tampil1, CONN)
        RD = CMD.ExecuteReader
    End Sub
    'cari id doang
    Sub cari1()
        Call koneksi()
        DA = New OdbcDataAdapter("Select * from tkonsumen Where id like '%" & txtid.Text & "%'", CONN)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tkonsumen")
        DataGridView1.DataSource = (DS.Tables("tkonsumen"))
        Dim tampil1 As String
        tampil1 = "select * from tkonsumen where id ='" & txtid.Text & "'"
        CMD = New OdbcCommand(tampil1, CONN)
        RD = CMD.ExecuteReader
    End Sub
    'proses keluar
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If MessageBox.Show("Apakah Anda Ingin Keluar?", "Konfirmasi Keluar Aplikasi", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
            End
        End If
    End Sub
    'cursor loading saat membuka cetakkonsumen
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            cetakkonsumen.Show()
            Me.Hide()
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    'panggil sub cari
    Private Sub txtcari_TextChanged(sender As Object, e As EventArgs) Handles txtcari.TextChanged
        Call cari()
    End Sub
    'angka ama . doang
    Private Sub txtid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtid.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") _
            AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtid_TextChanged(sender As Object, e As EventArgs) Handles txtid.TextChanged
        Call cari1()
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles btnuser.Click
        register.Show()
        Me.Hide()
    End Sub
    Private Sub btnuser_KeyDown(sender As Object, e As KeyEventArgs) Handles btnuser.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button4.PerformClick()
        End If
    End Sub
End Class