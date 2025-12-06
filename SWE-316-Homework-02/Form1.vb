Imports System.ComponentModel
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form1
    Private reader As New Reader
    Private folders As FolderContainer
    Private visualizationFolder As FolderContainer

    Private Sub BrowseButton_Click(sender As Object, e As EventArgs) Handles BrowseButton.Click

        ' clearing the treeview
        FolderTreeView.Nodes.Clear()

        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            ' List files in the folder
            folders = reader.ReadDirectory(FolderBrowserDialog1.SelectedPath)
        End If

        folders.CalculateSize()
        ' Add the files to the tree view 
        FolderTreeView.Nodes.Add(PopulateTreeView(folders))

        visualizationFolder = folders
    End Sub

    ' Fills the tree view and store an instance of each file object in the tree view node
    Private Function PopulateTreeView(parentFolder As FolderContainer) As TreeNode

        Dim parentNode As TreeNode = New TreeNode(parentFolder.GetName) With {.Tag = parentFolder}


        For Each subfolder In parentFolder.GetSubFolders
            If TypeOf subfolder Is FolderContainer Then
                parentNode.Nodes.Add(PopulateTreeView(subfolder))
            Else
                Dim fileNode = New TreeNode(subfolder.GetName) With {.Tag = subfolder}
                parentNode.Nodes.Add(fileNode)
            End If
        Next

        Return parentNode
    End Function

    Private Sub FolderTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles FolderTreeView.AfterSelect
        Dim selectedNode As TreeNode = e.Node
        Dim selectedItem = selectedNode.Tag
        If TypeOf selectedItem Is FolderContainer Then
            visualizationFolder = selectedItem
        End If
    End Sub

    Private Sub TreeButton_Click(sender As Object, e As EventArgs) Handles TreeButton.Click
        If visualizationFolder Is Nothing Then
            MessageBox.Show("No folder selected. Please browse or select a folder.", "No folder", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Keep the designer panel as the container; clear it and add the visualization panel returned by Visualize().
        VisualizationPanel.Controls.Clear()
        Dim viz As Panel = visualizationFolder.Visualize(New TreeVisualization)
        viz.Dock = DockStyle.Fill
        VisualizationPanel.Controls.Add(viz)
        VisualizationPanel.Invalidate()
    End Sub

    Private Sub BarButton_Click(sender As Object, e As EventArgs) Handles BarButton.Click
        If visualizationFolder Is Nothing Then
            MessageBox.Show("No folder selected. Please browse or select a folder.", "No folder", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Keep the designer panel as the container; clear it and add the visualization panel returned by Visualize().
        VisualizationPanel.Controls.Clear()
        Dim viz As Panel = visualizationFolder.Visualize(New BarChartVisualization)
        viz.Dock = DockStyle.Fill
        VisualizationPanel.Controls.Add(viz)
        VisualizationPanel.Invalidate()
    End Sub
End Class
