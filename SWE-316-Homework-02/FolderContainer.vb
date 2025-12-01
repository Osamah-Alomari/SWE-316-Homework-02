Public Class FolderContainer : Inherits Folder
    Private folderCollection As New List(Of Folder)
    Private name As String
    Private size As long

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

    ' loops over every folder to calculate the file sizes then the set the size for the current folder
    Public Overrides Function CalculateSize() As Long
        Dim totalSize As Long = 0
        For Each folder In folderCollection
            totalSize += folder.CalculateSize()
            'folder.SetSize(totalSize)
        Next

        Return totalSize
    End Function


    Public Overrides Function GetName() As String
        Return Me.name
    End Function
    Public Overrides Function GetSize() As Long
        Return Me.size
    End Function

    Public Overrides Sub SetSize(size As Long)
        Me.size = size
    End Sub
End Class
