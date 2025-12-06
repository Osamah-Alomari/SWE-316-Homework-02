<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        TreeButton = New Button()
        BarButton = New Button()
        BrowseButton = New Button()
        VisualizationPanel = New Panel()
        FolderBrowserDialog1 = New FolderBrowserDialog()
        FolderTreeView = New TreeView()
        SuspendLayout()
        ' 
        ' TreeButton
        ' 
        TreeButton.Location = New Point(173, 12)
        TreeButton.Name = "TreeButton"
        TreeButton.Size = New Size(94, 29)
        TreeButton.TabIndex = 0
        TreeButton.Text = "Tree"
        TreeButton.UseVisualStyleBackColor = True
        ' 
        ' BarButton
        ' 
        BarButton.Location = New Point(273, 12)
        BarButton.Name = "BarButton"
        BarButton.Size = New Size(94, 29)
        BarButton.TabIndex = 1
        BarButton.Text = "Bar Chart"
        BarButton.UseVisualStyleBackColor = True
        ' 
        ' BrowseButton
        ' 
        BrowseButton.Location = New Point(73, 409)
        BrowseButton.Name = "BrowseButton"
        BrowseButton.Size = New Size(94, 29)
        BrowseButton.TabIndex = 2
        BrowseButton.Text = "Browse"
        BrowseButton.UseVisualStyleBackColor = True
        ' 
        ' VisualizationPanel
        ' 
        VisualizationPanel.Location = New Point(173, 48)
        VisualizationPanel.Name = "VisualizationPanel"
        VisualizationPanel.Size = New Size(615, 390)
        VisualizationPanel.TabIndex = 3
        ' 
        ' FolderTreeView
        ' 
        FolderTreeView.Location = New Point(12, 12)
        FolderTreeView.Name = "FolderTreeView"
        FolderTreeView.Size = New Size(155, 391)
        FolderTreeView.TabIndex = 0
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(FolderTreeView)
        Controls.Add(VisualizationPanel)
        Controls.Add(BrowseButton)
        Controls.Add(BarButton)
        Controls.Add(TreeButton)
        Name = "Form1"
        Text = "Folder Size Visualizer"
        ResumeLayout(False)
    End Sub

    Friend WithEvents TreeButton As Button
    Friend WithEvents BarButton As Button
    Friend WithEvents BrowseButton As Button
    Friend WithEvents VisualizationPanel As Panel
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents FolderTreeView As TreeView

End Class
