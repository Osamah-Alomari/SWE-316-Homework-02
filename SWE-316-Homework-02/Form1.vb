Public Class Form1
    Private reader As New Reader
    Private folders As FolderContainer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub FolderBrowserDialog1_HelpRequest(sender As Object, e As EventArgs) Handles FolderBrowserDialog1.HelpRequest

    End Sub

    Private Sub ListFiles(ByVal folderPath As String)

    End Sub

    Private Sub BrowseButton_Click(sender As Object, e As EventArgs) Handles BrowseButton.Click
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            ' List files in the folder
            folders = reader.ReadDirectory(FolderBrowserDialog1.SelectedPath)

        End If

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles filesListBox.SelectedIndexChanged

    End Sub
End Class
