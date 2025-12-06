Public Class FolderContainer
    Inherits Folder

    Private folderCollection As New List(Of Folder)
    Private name As String
    Private size As Long
    Private visualizerSelector As VisualizationSelector

    Public Sub New(name As String)
        MyBase.New(name)
        Me.name = name
    End Sub

    Public Overrides Sub Add(folder As Folder)
        folderCollection.Add(folder)
    End Sub
    Public Overrides Sub Remove(folder As Folder)
        folderCollection.Remove(folder)
    End Sub
    Public Overrides Function Visualize(visualizationType As Visualizer) As Panel
        visualizerSelector = New VisualizationSelector(visualizationType)
        Return visualizerSelector.GetVisualization(Me)
    End Function

    ' loops over every folder to calculate the file sizes then the set the size for the current folder
    Public Overrides Function CalculateSize() As Long
        Dim totalSize As Long = 0
        For Each folder In folderCollection
            totalSize += folder.CalculateSize()
        Next

        Return totalSize
    End Function

    Public Function GetSubFolders() As List(Of Folder)
        Return Me.folderCollection
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
    Public Overrides Function GetExtension() As String
        Return ""
    End Function

End Class
