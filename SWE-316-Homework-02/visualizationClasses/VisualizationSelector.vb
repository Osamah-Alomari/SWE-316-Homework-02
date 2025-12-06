Public Class VisualizationSelector
    Private visualizer As Visualizer

    Public Sub New(visualizationType As Visualizer)
        Me.visualizer = visualizationType
    End Sub

    Public Function GetVisualization(folder As FolderContainer) As Panel
        Return visualizer.DrawVisualization(folder)
    End Function

End Class
