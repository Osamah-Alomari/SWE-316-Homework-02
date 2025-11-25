Imports System.Runtime.Intrinsics.X86

Public Class File : Inherits Folder
    Private name As String
    Private size As Integer
    Private extension As String

    Public Sub New(name As String, size As Integer, extenstion As String)
        MyBase.New(name)
        Me.size = size
        Me.extension = extension

    End Sub

    Public Overrides Function GetName() As String
        Return Me.name
    End Function

    Public Function GetSize() As Integer
        Return Me.size
    End Function

    Public Function GetExtension() As String
        Return Me.extension
    End Function

    Public Overrides Sub Add(folder As Folder)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub Remove(folder As Folder)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub Display(folder As Folder)
        Throw New NotImplementedException()
    End Sub
End Class
