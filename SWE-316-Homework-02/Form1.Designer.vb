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
        Panel1 = New Panel()
        Panel2 = New Panel()
        SuspendLayout()
        ' 
        ' TreeButton
        ' 
        TreeButton.Location = New Point(194, 13)
        TreeButton.Name = "TreeButton"
        TreeButton.Size = New Size(94, 29)
        TreeButton.TabIndex = 0
        TreeButton.Text = "Tree"
        TreeButton.UseVisualStyleBackColor = True
        ' 
        ' BarButton
        ' 
        BarButton.Location = New Point(305, 13)
        BarButton.Name = "BarButton"
        BarButton.Size = New Size(94, 29)
        BarButton.TabIndex = 1
        BarButton.Text = "Bar Chart"
        BarButton.UseVisualStyleBackColor = True
        ' 
        ' BrowseButton
        ' 
        BrowseButton.Location = New Point(68, 409)
        BrowseButton.Name = "BrowseButton"
        BrowseButton.Size = New Size(94, 29)
        BrowseButton.TabIndex = 2
        BrowseButton.Text = "Browse"
        BrowseButton.UseVisualStyleBackColor = True
        ' 
        ' Panel1
        ' 
        Panel1.Location = New Point(194, 48)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(594, 390)
        Panel1.TabIndex = 3
        ' 
        ' Panel2
        ' 
        Panel2.Location = New Point(12, 13)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(166, 382)
        Panel2.TabIndex = 4
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
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
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel

End Class
