Imports System.Runtime.CompilerServices

Public MustInherit Class Folder

    Private name As String
    Private size As Long
    Private visualizerSelector As VisualizationSelector

    Public Sub New(name As String)
        Me.name = name
    End Sub

    Public MustOverride Sub Add(folder As Folder)
    Public MustOverride Sub Remove(folder As Folder)
    Public MustOverride Function GetSize() As Long
    Public MustOverride Sub SetSize(size As Long)
    Public MustOverride Function GetName() As String
    Public MustOverride Function Visualize(visualizationType As Visualizer) As Panel
    Public MustOverride Function CalculateSize() As Long
    Public MustOverride Function GetExtension() As String

End Class
