Imports System.Runtime.Intrinsics.X86

Public Class File : Inherits Folder
    Private name As String
    Private size As Long
    Private extension As String
    Private visualizerSelector As VisualizationSelector

    Public Sub New(name As String, size As Long, extension As String)
        MyBase.New(name)
        Me.name = name
        Me.size = size
        Me.extension = extension
    End Sub

    Public Overrides Function GetName() As String
        Return Me.name
    End Function
    Public Overrides Function GetSize() As Long
        Return Me.size
    End Function
    Public Overrides Sub SetSize(size As Long)
        'DO Nothing'
    End Sub
    Public Overrides Function GetExtension() As String
        Return Me.extension
    End Function
    Public Overrides Sub Add(folder As Folder)
        Throw New NotImplementedException()
    End Sub
    Public Overrides Sub Remove(folder As Folder)
        Throw New NotImplementedException()
    End Sub
    Public Overrides Function Visualize(visualizationType As Visualizer) As Panel
        Throw New NotImplementedException()
    End Function
    Public Overrides Function CalculateSize() As Long
        Return Me.size
    End Function
End Class
