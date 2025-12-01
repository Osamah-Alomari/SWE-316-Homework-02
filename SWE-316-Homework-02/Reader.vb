Imports System.DirectoryServices
Imports System.IO
Imports System.Windows.Forms.Design

Public Class Reader


    Public Function ReadDirectory(folderPath As String) As FolderContainer
        ' Get the folder name
        Dim folder = New FolderContainer(Path.GetFileName(folderPath))
        ' Get the subfolders containted in the main folder
        Dim scanedFolder = My.Computer.FileSystem.GetDirectories(folderPath)

        ' loop over each folder 
        For Each directory In scanedFolder
            Dim result = ReadDirectory(directory)
            folder.Add(result)
        Next

        ' Get the files in the directory 
        Dim scanedFiles = My.Computer.FileSystem.GetFiles(folderPath)

        ' loop over each file and add it to the current directory 
        For Each file In scanedFiles
            Dim fileInfo = My.Computer.FileSystem.GetFileInfo(file)

            Dim fileExtension = fileInfo.Extension
            Dim fileName = fileInfo.Name.Replace(fileExtension, "")
            Dim fileSize = fileInfo.Length

            Dim newFile = New File(fileName, fileSize, fileExtension)

            folder.Add(newFile)
        Next

        Return folder
    End Function



End Class
