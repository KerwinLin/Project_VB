Imports System.IO
Public Class WebForm2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function AllowableFileSize()
#Region "檔案限制"
        '從 web.config 讀取判斷檔案大小的限制
        'Dim FileSizeLimit As Double = Convert.ToInt32(ConfigurationManager.AppSettings("FileSizeLimit"))
        Dim FileSizeLimit As Integer = 1000000
        '判斷檔案是否超出了限制
        If (FileSizeLimit > FileUpload1.PostedFile.ContentLength) Then
            Response.Write(“檔案剛好”)
            Return True
        Else
            Response.Write(“檔案太大”)
            Return False
        End If
    End Function
#End Region

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '方法一:Server硬碟的實體目錄
        Dim savepath As String = "C:\temp\uploads\"
        '方法二:網站上URL路徑，一樣轉換為硬碟的實體路徑
        'Dim savepath2 As String = Server.MapPath("~/")
        '方法三:
        'Dim saveDir As String = "\網路上的目錄路徑\"
        'Dim appPath As String = Request.PhysicalApplicationPath '網站跟目錄轉實體目錄
        'Dim savePath3 As String = appPath & saveDir

        If (FileUpload1.HasFile) Then
            Dim filename As String = FileUpload1.FileName
            Dim pathToCheck As String = savepath & filename
            Dim tempfileName As String = Nothing

            If AllowableFileSize() Then
                '副檔名限制
                Dim extention As String = Path.GetExtension(filename)
                If (extention = ".jpg") Then
                    '避免重複檔名
                    If (File.Exists(pathToCheck)) Then
                        Dim My_counter As Integer = 2
                        While (File.Exists(pathToCheck))
                            tempfileName = My_counter.ToString() & "_" & filename
                            pathToCheck = savepath & tempfileName
                            My_counter += 1
                        End While
                        filename = tempfileName
                        uploadstatuslabel.Text = "檔名重複修改如下" & filename
                    Else
                        uploadstatuslabel.Text = "上傳成功"
                    End If
                    savepath = savepath & filename
                    FileUpload1.SaveAs(savepath)
                    Label1.Text = "上傳成功，檔案名稱---" & filename
                Else
                    Label1.Text = "上傳未成功"
                End If

            End If
            Response.Redirect("WebForm2.aspx")
        End If

    End Sub
End Class