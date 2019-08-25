Imports System.Data.Odbc
'Pendeklarasikan Variable
Public Class inputdata
    Dim CONN As OdbcConnection
    Dim CMD As OdbcCommand
    Dim DS As New DataSet
    Dim DA As OdbcDataAdapter
    Dim RD As OdbcDataReader
    Dim LokasiData As String
    'koneksi ke database "dbpembukaan"
    Sub koneksi()
        LokasiData = "Driver={MySQL ODBC 3.51 Driver}; Database=dbpendataan; server=localhost; uid=root"
        CONN = New OdbcConnection(LokasiData)
        If CONN.State = ConnectionState.Closed Then
            CONN.Open()
        End If
    End Sub
    'proses Mengsongkan Textbox
    Sub kosongkandata()
        txtid.Text = ""
        txtnama.Text = ""
        txtalamat.Text = ""
        txtmarketing.Text = ""
        cbteknisi.Text = ""
        txttanggal.Text = ""
        txtantena.Text = "0"
        txtparabola.Text = "0"
        txtcctv.Text = "0"
        txtpetir.Text = "0"
        txttotal.Text = "0"
        jmlantena.Text = "0"
        jmlcctv.Text = "0"
        jmlparabola.Text = "0"
        jmlpetir.Text = "0"
        cbteknisi.Text = ""
        cb1.Text = ""
        cb2.Text = ""
        cb1.Visible = False
        cb2.Visible = False
        btnedit.Enabled = False
        btninput.Enabled = True
    End Sub
    'Proses Keluar
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If MessageBox.Show("Apakah Anda Ingin Keluar?", "Konfirmasi Keluar Aplikasi", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
            End
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.Show()
        Me.Hide()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        data_konsumen.Show()
        Call kosongkandata()
        Me.Hide()
    End Sub
    'Menampilkan Waktu pada Label 3
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = Format(Now, “dddd, dd MMMMM yyyy”)
    End Sub
    'Proses Simpan Pada tabel konsumen
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btninput.Click
        If txtnama.Text = "" Or txtalamat.Text = "" Or txtmarketing.Text = "" Or cbteknisi.Text = "" Or txttanggal.Text = "0" Or txttanggal.Text = "" Or txtparabola.Text = "" Then
            MessageBox.Show("Silahkan Isi Semua From", "Pendataan Konsumen", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Call koneksi()
            If cb2.Checked = True Then
                cb2.Text = cb2.Text
            ElseIf cb2.Checked = False
                cb2.Text = ""
            ElseIf cb1.Checked = True
                cb1.Text = cb1.Text
            Else cb1.Checked = False
                cb1.Text = ""
            End If
            If cb1.Checked = True And cb2.Checked Then
                cb1.Text = cb1.Text + ",  "
                cb2.Text = cb2.Text
            End If
            Dim Simpan As String = "insert into tkonsumen values (NULL, '" & txttanggal.Value & "', '" & txtnama.Text & "','" & txtalamat.Text & "', '" & txtmarketing.Text & "','" & cb1.Text & cb2.Text & "','" & txtantena.Text & "', '" & txtparabola.Text & "' ,'" & txtcctv.Text & "' ,'" & txtpetir.Text & "' ,'" & txttotal.Text & "')"
            CMD = New OdbcCommand(Simpan, CONN)
            CMD.ExecuteNonQuery()
            MsgBox("Input Data BERHASIL")
            Call kosongkandata()
            Call data_konsumen.refreshDatagrid()
            data_konsumen.Show()
            Me.Hide()
        End If
    End Sub
    'panggil script kosongkandata
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Call kosongkandata()
    End Sub
    'proses edit bersarkan id pada txtid yang diambil dari Form data_Konsumen
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If txtnama.Text = "" Or txtalamat.Text = "" Or txtmarketing.Text = "" Or cbteknisi.Text = "" Or txttanggal.Text = "0" Or txttanggal.Text = "" Or txtparabola.Text = "" Then
            MessageBox.Show("Silahkan Isi Semua From", "Pendataan Konsumen", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Call koneksi()
            If cb2.Checked = True Then
                cb2.Text = cb2.Text
            ElseIf cb2.Checked = False
                cb2.Text = ""
            ElseIf cb1.Checked = True
                cb1.Text = cb1.Text
            Else cb1.Checked = False
                cb1.Text = ""
            End If
            If cb1.Checked = True And cb2.Checked Then
                cb1.Text = cb1.Text + ",  "
                cb2.Text = cb2.Text
            End If
            Dim edit As String = "update tkonsumen set tanggal='" & txttanggal.Text & "',nama_konsumen='" & txtnama.Text & "',Alamat_konsumen ='" & txtalamat.Text & "',Nama_Marketing='" & txtmarketing.Text & "',Nama_Teknisi='" & cb1.Text & cb2.Text & "',Laba_Antena='" & txtantena.Text & "',Laba_Parabola='" & txtparabola.Text & "',Laba_CCTV='" & txtcctv.Text & "',Laba_Petir='" & txtpetir.Text & "',Total_Laba ='" & txttotal.Text & "'where id='" & txtid.Text & "'"
            CMD = New OdbcCommand(edit, CONN)
                CMD.ExecuteNonQuery()
                MsgBox("Data Berhasil Di UPDATE")
                Call kosongkandata()
                Call data_konsumen.refreshDatagrid()
                data_konsumen.Show()
                Me.Hide()
            End If
    End Sub
    'itung itungan
    Private Sub jmlantena_TextChanged(sender As Object, e As EventArgs) Handles jmlantena.TextChanged
        txtantena.Text = Val(jmlantena.Text) * 50000
        txttotal.Text = Val(txtparabola.Text) + Val(txtantena.Text) + Val(txtcctv.Text) + Val(txtpetir.Text)
    End Sub
    Private Sub jmlparabola_TextChanged(sender As Object, e As EventArgs) Handles jmlparabola.TextChanged
        txtparabola.Text = Val(jmlparabola.Text) * 100000
        txttotal.Text = Val(txtparabola.Text) + Val(txtantena.Text) + Val(txtcctv.Text) + Val(txtpetir.Text)
    End Sub
    Private Sub jmlcctv_TextChanged(sender As Object, e As EventArgs) Handles jmlcctv.TextChanged
        txtcctv.Text = Val(jmlcctv.Text) * 200000
        txttotal.Text = Val(txtparabola.Text) + Val(txtantena.Text) + Val(txtcctv.Text) + Val(txtpetir.Text)
    End Sub
    Private Sub jmlpetir_TextChanged(sender As Object, e As EventArgs) Handles jmlpetir.TextChanged
        txtpetir.Text = Val(jmlpetir.Text) * 400000
        txttotal.Text = Val(txtparabola.Text) + Val(txtantena.Text) + Val(txtcctv.Text) + Val(txtpetir.Text)
    End Sub
    'biar angka doang ama .
    Sub jmlantena_KeyPress(sender As Object, e As KeyPressEventArgs) Handles jmlantena.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") _
            AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "." Then
            e.Handled = True
        End If
    End Sub
    Private Sub jmlcctv_KeyPress(sender As Object, e As KeyPressEventArgs) Handles jmlcctv.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") _
            AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "." Then
            e.Handled = True
        End If
    End Sub
    Private Sub jmlparabola_KeyPress(sender As Object, e As KeyPressEventArgs) Handles jmlparabola.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") _
            AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "." Then
            e.Handled = True
        End If
    End Sub
    Private Sub jmlpetir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles jmlpetir.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") _
            AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "." Then
            e.Handled = True
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbteknisi.SelectedIndexChanged
        cb1.Visible = True
        cb2.Visible = True
        If cbteknisi.Text = "Cabang A" Then
            cb1.Text = "Sutarjo"
            cb2.Text = "Parmaji"
        Else
            cb1.Text = "Parjo"
            cb2.Text = "Sutrisno"
        End If
    End Sub
    Private Sub inputdata_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kosongkandata()
    End Sub

    Private Sub btnuser_Click(sender As Object, e As EventArgs) Handles btnuser.Click
        register.Show()
        Me.Hide()
    End Sub
End Class