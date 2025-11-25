Imports System.Runtime.CompilerServices

Public MustInherit Class Folder
    Private name As String

    Public Sub New(name As String)
        Me.name = name
    End Sub

    Public MustOverride Function GetName() As String

    Public MustOverride Sub Add(folder As Folder)
    Public MustOverride Sub Remove(folder As Folder)
    Public MustOverride Sub Display(folder As Folder)


End Class
