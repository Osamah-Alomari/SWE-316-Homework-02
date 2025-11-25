Public Class FolderContainer : Inherits Folder
    Private folderCollection As List(Of Folder)
    Private name As String

    Public Sub New(name As String)
        MyBase.New(name)
    End Sub
    Public Overrides Sub Add(folder As Folder)
        folderCollection.Add(folder)
    End Sub

    Public Overrides Sub Remove(folder As Folder)
        folderCollection.Remove(folder)
    End Sub

    Public Overrides Sub Display(folder As Folder)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Function GetName() As String
        Return Me.name
    End Function
End Class
