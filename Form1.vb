Imports System.Text

Public Class Form1
    Dim sFile As New s_File

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Call qrEncodeGen()
            Clipboard.SetImage(PictureBox1.Image)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub qrEncodeGen()
        Try
            Dim qrCode As New QRCodeEncoderLibrary.QRCodeEncoder
            qrCode.Encode(TextBox1.Text)
            PictureBox1.Image = qrCode.CreateQRCodeBitmap()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub qrDecodeGen()
        Try
            Dim qrCode As New QRCodeDecoderLibrary.QRDecoder
            TextBox1.Text = ""

            Dim Image As String = sFile.OpenFileDialog(My.Computer.FileSystem.SpecialDirectories.MyDocuments, "*.png;*.jpg;*.gif;*.tif;*.bmp)|*.png;*.jpg;*.gif;*.tif;*.bmp", "png")
            Dim QRCodeInputImage = New Bitmap(Image)
            PictureBox1.Load(Image)
            Dim DataByteArray()() As Byte = qrCode.ImageDecoder(QRCodeInputImage)
            TextBox1.Text = QRCodeResult(DataByteArray)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If Me.PictureBox1.Image IsNot Nothing Then
                Dim Image As String = sFile.SaveFileDialog(My.Computer.FileSystem.SpecialDirectories.MyDocuments, "*.png;*.jpg;*.gif;*.tif;*.bmp)|*.png;*.jpg;*.gif;*.tif;*.bmp", "png")
                Me.PictureBox1.Image.Save(IO.Path.Combine(Image))
                MessageBox.Show("QR wurde gespeichert.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        qrDecodeGen()
    End Sub

    Private Shared Function QRCodeResult(ByVal DataByteArray As Byte()()) As String
        If DataByteArray Is Nothing Then Return String.Empty
        If DataByteArray.Length = 1 Then Return (QRCodeDecoderLibrary.QRDecoder.ByteArrayToStr(DataByteArray(0)))
        Dim Str As StringBuilder = New StringBuilder()

        Return Str.ToString()
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
