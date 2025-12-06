Imports System.Drawing
Imports System.Windows.Forms

Public Class TreeVisualization
    Inherits Visualizer

    Public Overrides Function DrawVisualization(parentFolder As FolderContainer) As Panel
        Dim panel As New Panel() With {
            .Size = New Size(615, 390),
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.None,
            .AutoScroll = True
        }

        ' Layout constants
        Dim marginTop = 10
        Dim marginLeft = 10
        Dim nodeHeightBase = 30
        Dim verticalSpacing = 10
        Dim connectorDrop = 12
        Dim indentFromParent = 44       ' horizontal offset from parent center to child's left edge
        Dim padX = 10                    ' horizontal padding inside box
        Dim padY = 4                     ' vertical padding inside box
        Dim minNodeWidth = 60            ' minimum width for boxes

        AddHandler panel.Paint, Sub(sender, e)
                                    If parentFolder Is Nothing Then
                                        Return
                                    End If

                                    Dim g = e.Graphics
                                    g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                                    g.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit

                                    ' apply scroll offset so drawing moves with scrollbars
                                    Dim scrollOffset = panel.AutoScrollPosition
                                    g.TranslateTransform(scrollOffset.X, scrollOffset.Y)

                                    Dim y As Integer = marginTop
                                    Dim maxRight As Integer = 0

                                    ' Simple size formatter
                                    Dim formatSize As Func(Of Long, String) = Function(size)
                                                                                  If size < 1024L Then
                                                                                      Return size.ToString() & " B"
                                                                                  End If
                                                                                  Dim kb = size / 1024.0R
                                                                                  If kb < 1024.0R Then
                                                                                      Return kb.ToString("0.##") & " KB"
                                                                                  End If
                                                                                  Dim mb = kb / 1024.0R
                                                                                  If mb < 1024.0R Then
                                                                                      Return mb.ToString("0.##") & " MB"
                                                                                  End If
                                                                                  Dim gb = mb / 1024.0R
                                                                                  Return gb.ToString("0.##") & " GB"
                                                                              End Function

                                    ' Recursive drawing routine (pre-order).
                                    Dim drawNode As Action(Of Folder, Integer, Nullable(Of Integer)) = Nothing
                                    drawNode = Sub(node As Folder, level As Integer, parentCenterX As Nullable(Of Integer))
                                                   ' Determine X: if we have a parent center X, indent the child relative to it.
                                                   Dim x As Integer = If(parentCenterX.HasValue, parentCenterX.Value + indentFromParent, marginLeft)

                                                   ' Choose font based on node type
                                                   Dim fontToUse As Font = If(TypeOf node Is FolderContainer,
                                                                             New Font(SystemFonts.DefaultFont.FontFamily, SystemFonts.DefaultFont.Size, FontStyle.Bold),
                                                                             SystemFonts.DefaultFont)

                                                   ' Build display name (include extension for files) and size
                                                   Dim displayName As String
                                                   If TypeOf node Is FolderContainer Then
                                                       displayName = node.GetName()
                                                   Else
                                                       ' Append file extension (File.GetExtension returns extension including leading dot)
                                                       displayName = node.GetName() & node.GetExtension()
                                                   End If

                                                   Dim sizeValue As Long
                                                   If TypeOf node Is FolderContainer Then
                                                       sizeValue = DirectCast(node, FolderContainer).CalculateSize()
                                                   Else
                                                       sizeValue = node.GetSize()
                                                   End If

                                                   Dim displayText As String = $"{displayName} ({formatSize(sizeValue)})"

                                                   ' Measure text and compute dynamic width (height stays at least nodeHeightBase)
                                                   Dim measured As SizeF = g.MeasureString(displayText, fontToUse)
                                                   Dim nodeWidth As Integer = Math.Max(minNodeWidth, CInt(Math.Ceiling(measured.Width)) + padX * 2)
                                                   Dim nodeHeight As Integer = Math.Max(nodeHeightBase, CInt(Math.Ceiling(measured.Height)) + padY * 2)

                                                   Dim rect As New Rectangle(x, y, nodeWidth, nodeHeight)

                                                   ' Fill background according to node type
                                                   Using backBrush As Brush = If(TypeOf node Is FolderContainer,
                                                                                New SolidBrush(Color.LightGray),
                                                                                New SolidBrush(Color.LightGreen))
                                                       g.FillRectangle(backBrush, rect)
                                                   End Using

                                                   ' Draw border
                                                   Using pen As New Pen(Color.Black, 1)
                                                       g.DrawRectangle(pen, rect)
                                                   End Using

                                                   ' Center text inside the rectangle
                                                   Dim textX As Single = rect.X + (rect.Width - measured.Width) / 2.0F
                                                   Dim textY As Single = rect.Y + (rect.Height - measured.Height) / 2.0F
                                                   Using textBrush As New SolidBrush(Color.Black)
                                                       g.DrawString(displayText, fontToUse, textBrush, textX, textY)
                                                   End Using

                                                   If fontToUse IsNot SystemFonts.DefaultFont Then
                                                       fontToUse.Dispose()
                                                   End If

                                                   ' Track maximum right extent for scrolling
                                                   maxRight = Math.Max(maxRight, rect.X + rect.Width + marginLeft)

                                                   ' Save this node's bottom-center point to use as trunk X and start Y
                                                   Dim myBottomCenter As Point = New Point(rect.X + rect.Width \ 2, rect.Y + rect.Height)

                                                   ' Advance vertical position for children
                                                   y += nodeHeight + verticalSpacing

                                                   ' If folder, prepare connectors and draw children
                                                   If TypeOf node Is FolderContainer Then
                                                       Dim childLeftCenters As New List(Of Point)

                                                       For Each child In DirectCast(node, FolderContainer).GetSubFolders()
                                                           ' child's left-center computed from current y (child top) and computed child X
                                                           Dim childX As Integer = myBottomCenter.X + indentFromParent
                                                           Dim childLeftCenter As Point = New Point(childX, y + nodeHeight \ 2)
                                                           childLeftCenters.Add(childLeftCenter)

                                                           ' Draw the child (recursively). Pass parent's bottom-center X so child computes the same X.
                                                           drawNode(child, level + 1, New Nullable(Of Integer)(myBottomCenter.X))
                                                       Next

                                                       ' Draw the vertical trunk from parent's bottom-center to last child's center Y (straight down)
                                                       If childLeftCenters.Count > 0 Then
                                                           Using connectorPen As New Pen(Color.Gray, 1)
                                                               Dim trunkX As Integer = myBottomCenter.X
                                                               Dim trunkTopY As Integer = myBottomCenter.Y
                                                               Dim trunkBottomY As Integer = childLeftCenters(childLeftCenters.Count - 1).Y

                                                               If trunkBottomY < trunkTopY Then
                                                                   trunkBottomY = trunkTopY
                                                               End If

                                                               ' Vertical trunk
                                                               g.DrawLine(connectorPen, New Point(trunkX, trunkTopY), New Point(trunkX, trunkBottomY))

                                                               ' Horizontal branches from trunk to each child's left edge
                                                               For Each childPt In childLeftCenters
                                                                   g.DrawLine(connectorPen, New Point(trunkX, childPt.Y), childPt)
                                                               Next
                                                           End Using
                                                       End If
                                                   End If
                                               End Sub

                                    ' Start drawing at root level 0 (no parent center X)
                                    drawNode(parentFolder, 0, New Nullable(Of Integer)())

                                    ' Extend the virtual canvas if needed so scrollbars appear correctly
                                    Dim neededHeight = y + marginTop
                                    Dim neededWidth = Math.Max(maxRight, marginLeft + 200)
                                    If panel.AutoScrollMinSize.Height <> neededHeight OrElse panel.AutoScrollMinSize.Width <> neededWidth Then
                                        panel.AutoScrollMinSize = New Size(neededWidth, neededHeight)
                                    End If
                                End Sub

        Return panel
    End Function

    ' Helper to compute maximum depth of tree (used to compute required width estimate)
    Private Function GetTreeDepth(folder As Folder) As Integer
        If TypeOf folder IsNot FolderContainer Then
            Return 0
        End If

        Dim maxDepth = 0
        For Each child In DirectCast(folder, FolderContainer).GetSubFolders()
            Dim childDepth = GetTreeDepth(child)
            If childDepth > maxDepth Then
                maxDepth = childDepth
            End If
        Next

        Return maxDepth + 1
    End Function
End Class
