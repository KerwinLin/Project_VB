Imports System
Imports System.Web.Configuration
Imports System.Data
Imports System.Data.SqlClient

Public Class StroedProcedure
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ConnStr As New SqlConnection(WebConfigurationManager.ConnectionStrings("MVC_UserDBConnectionString").ConnectionString)
        Dim SqlComm As New SqlCommand("Get_Name", ConnStr)
        SqlComm.CommandType = CommandType.StoredProcedure
        SqlComm.Parameters.Add("@inputUserName", SqlDbType.NVarChar).Value = "林琨峰"

        Dim rc As New SqlParameter("UserName", SqlDbType.NVarChar)
        rc.Direction = ParameterDirection.Output

        Try
            ConnStr.Open()
            SqlComm.Parameters.Add(rc)
            Label1.Text = "取得單筆輸出資料: " & rc.Value
            SqlComm.ExecuteReader()

        Catch
            Label1.Text = "資料庫連線失敗"
        End Try
    End Sub

End Class