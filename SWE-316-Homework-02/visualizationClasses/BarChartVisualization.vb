Imports System.Drawing
Imports System.Windows.Forms

Public Class BarChartVisualization
    Inherits Visualizer

    Public Overrides Function DrawVisualization(folder As FolderContainer) As Panel
        Dim panel As New Panel() With {
            .Size = New Size(615, 390),
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.None,
            .AutoScroll = True
        }

        ' Layout constants
        Dim marginTop = 12
        Dim marginLeft = 12
        Dim marginRight = 12
        Dim rowHeight = 26
        Dim rowSpacing = 10
        Dim minBarWidth = 40

        ' Visualization tuning
        Dim useLogScale As Boolean = True   ' set False for linear scaling

        ' Size formatter
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

        AddHandler panel.Paint, Sub(sender, e)
                                    If folder Is Nothing Then Return

                                    Dim g = e.Graphics
                                    g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                                    g.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit

                                    ' Top-level children only
                                    Dim children = folder.GetSubFolders()
                                    If children Is Nothing OrElse children.Count = 0 Then Return

                                    ' Apply scroll transform so drawing follows scrollbars
                                    Dim scrollOffset = panel.AutoScrollPosition
                                    g.TranslateTransform(scrollOffset.X, scrollOffset.Y)

                                    Dim labelFont As Font = SystemFonts.DefaultFont

                                    ' Prepare display list (no grouping) and include extension for files (explicitly)
                                    Dim displayItems As New List(Of (LabelName As String, Size As Long, IsFolder As Boolean, SourceObj As Folder))
                                    For Each child In children
                                        Dim s As Long = If(TypeOf child Is FolderContainer,
                                                           DirectCast(child, FolderContainer).CalculateSize(),
                                                           child.GetSize())

                                        Dim labelName As String
                                        If TypeOf child Is FolderContainer Then
                                            labelName = child.GetName()
                                        Else
                                            labelName = child.GetName() & child.GetExtension
                                        End If

                                        displayItems.Add((labelName, s, TypeOf child Is FolderContainer, child))
                                    Next

                                    ' Avoid zero total
                                    Dim totalSize As Long = displayItems.Sum(Function(it) it.Size)
                                    If totalSize = 0 Then totalSize = 1L

                                    ' Prepare transformed sizes for scaling (log or linear)
                                    Dim transformed As New List(Of Double)
                                    For Each it In displayItems
                                        Dim val As Double = CDbl(it.Size)
                                        If useLogScale Then
                                            val = Math.Log10(val + 1.0)
                                        End If
                                        transformed.Add(val)
                                    Next

                                    Dim maxTransformed As Double = If(transformed.Count > 0, transformed.Max(), 1.0)
                                    If maxTransformed <= 0 Then maxTransformed = 1.0

                                    ' Measure label widths (label only — extension included in labelName)
                                    Dim measuredLabelWidths As New List(Of Single)
                                    Dim measuredLabelHeights As New List(Of Single)
                                    For i = 0 To displayItems.Count - 1
                                        Dim measured = g.MeasureString(displayItems(i).LabelName, labelFont)
                                        measuredLabelWidths.Add(measured.Width)
                                        measuredLabelHeights.Add(measured.Height)
                                    Next

                                    ' Bars start at marginLeft so labels overlap them.
                                    Dim clientWidth As Integer = Math.Max(400, panel.ClientSize.Width)
                                    Dim availableBarWidth As Integer = clientWidth - marginLeft - marginRight
                                    If availableBarWidth < minBarWidth Then availableBarWidth = minBarWidth

                                    Dim y As Integer = marginTop
                                    Dim maxRight As Integer = 0

                                    For i = 0 To displayItems.Count - 1
                                        Dim it = displayItems(i)
                                        Dim tval = transformed(i)

                                        ' Bar rectangle (starts at marginLeft so label will overlap)
                                        Dim barX As Integer = marginLeft
                                        Dim barWidth As Integer = CInt(Math.Round(tval / maxTransformed * availableBarWidth))
                                        If barWidth < 2 Then barWidth = 2
                                        Dim barRect As New Rectangle(barX, y, barWidth, rowHeight)

                                        ' Bar color: folder = blue, file = orange
                                        Dim barColor As Color = If(it.IsFolder, Color.SteelBlue, Color.Orange)
                                        Using barBrush As New SolidBrush(barColor)
                                            g.FillRectangle(barBrush, barRect)
                                        End Using

                                        ' Border for bar
                                        Using pen As New Pen(Color.Black, 1)
                                            g.DrawRectangle(pen, barRect)
                                        End Using

                                        ' Draw label (name + extension) overlapping the bar; draw size text next to the name (once)
                                        Dim labelTextOnly As String = it.LabelName
                                        Dim sizeText As String = formatSize(it.Size)
                                        Dim measuredLabelWidth = CInt(Math.Ceiling(measuredLabelWidths(i)))
                                        Dim measuredLabelHeight = CInt(Math.Ceiling(measuredLabelHeights(i)))

                                        Dim labelX As Integer = marginLeft + 6
                                        Dim labelY As Integer = y + (rowHeight - measuredLabelHeight) \ 2

                                        ' Use black font for all text (per request)
                                        Using textBrush As New SolidBrush(Color.Black)
                                            g.DrawString(labelTextOnly, labelFont, textBrush, labelX, labelY)
                                        End Using

                                        ' Draw the size to the right of the label (not repeated inside bar)
                                        Dim sizeX As Integer = labelX + measuredLabelWidth + 4
                                        Dim sizeY As Integer = labelY
                                        Using sizeBrush As New SolidBrush(Color.Black)
                                            g.DrawString(sizeText, labelFont, sizeBrush, sizeX, sizeY)
                                        End Using

                                        ' Track rightmost extent for scrolling
                                        maxRight = Math.Max(maxRight, barX + barRect.Width + marginRight)
                                        maxRight = Math.Max(maxRight, sizeX + 80)

                                        y += rowHeight + rowSpacing
                                    Next

                                    ' Set AutoScrollMinSize so scrollbars appear if needed
                                    Dim neededHeight As Integer = y + marginTop
                                    Dim neededWidth As Integer = Math.Max(maxRight, clientWidth)
                                    If panel.AutoScrollMinSize.Height <> neededHeight OrElse panel.AutoScrollMinSize.Width <> neededWidth Then
                                        panel.AutoScrollMinSize = New Size(neededWidth, neededHeight)
                                    End If
                                End Sub

        Return panel
    End Function
End Class
