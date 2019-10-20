Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.IO 'Excel
Imports System.Web.UI 'Excel


Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            DBInit()
            Label1.Text = "目前位於第" & GridView1.PageIndex & "頁，總共有" & GridView1.PageCount
        End If

        'DataReader 範例
        Dim Sqlconn As SqlConnection = New SqlConnection
        Sqlconn.ConnectionString = WebConfigurationManager.ConnectionStrings("MVC_UserDBConnectionString").ConnectionString

        Dim Sqlstring As String = "select * FROM UserTable"
        Dim MyComm As SqlCommand = New SqlCommand(Sqlstring, Sqlconn)
        Using Sqlconn
            Try
                If (Sqlconn.State = ConnectionState.Closed) Then
                    Sqlconn.Open()
                End If
            Catch

            End Try
            Dim Mydr As SqlDataReader = MyComm.ExecuteReader()
            GridView2.DataSource = Mydr
            GridView2.DataBind()

            MyComm.Cancel()
        End Using

    End Sub

    Private Sub DBInit()
        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("MVC_UserDBConnectionString").ConnectionString)
        Dim Sqlstring As String = "select UserName, UserMobilePhone FROM UserTable"
        Dim myda As SqlDataAdapter = New SqlDataAdapter(Sqlstring, conn)
        Dim myds As DataSet = New DataSet
        Try
            myda.Fill(myds, "table")
            GridView1.DataSource = myds.Tables("table").DefaultView
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write("Error")
        End Try

    End Sub

    Protected Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView1.RowEditing
        GridView1.EditIndex = e.NewEditIndex
        DBInit()
    End Sub

    Protected Sub GridView1_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles GridView1.RowCancelingEdit
        GridView1.EditIndex = -1
        DBInit()
    End Sub

    Protected Sub GridView1_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles GridView1.SelectedIndexChanging
        GridView1.SelectedIndex = e.NewSelectedIndex
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        Label1.Text = "目前位於第" & GridView1.PageIndex & "頁，總共有" & GridView1.PageCount
    End Sub

    Protected Sub GridView1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridView1.RowUpdating
        Dim myname, myphone As TextBox
        myname = GridView1.Rows(e.RowIndex).Cells(1).Controls(0)
        myphone = GridView1.Rows(e.RowIndex).Cells(2).Controls(0)

        Dim conn As SqlConnection = New SqlConnection
        conn.ConnectionString = WebConfigurationManager.ConnectionStrings("MVC_UserDBConnectionString").ConnectionString

        Dim myda As SqlDataAdapter = New SqlDataAdapter
        Dim mycom As String = "update [UserTable] set [UserName] =@user, [UserMobilePhone] =@phone where [UserId] = @id"
        myda.UpdateCommand = New SqlCommand(mycom, conn)

        myda.UpdateCommand.Parameters.Add("@user", SqlDbType.Char).Value = myname.Text.Trim()
        myda.UpdateCommand.Parameters.Add("@phone", SqlDbType.Char).Value = myphone.Text.Trim()
        myda.UpdateCommand.Parameters.Add("@id", SqlDbType.Int).Value = CInt(GridView1.DataKeys(e.RowIndex).Value.ToString.Trim())

        Try
            conn.Open()
            myda.UpdateCommand.ExecuteNonQuery()
            myda.Dispose()
        Catch ex As Exception

        End Try

        GridView1.EditIndex = -1
        DBInit()

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Dim myds As New DataSet
        Dim Sqlconn As New SqlConnection(WebConfigurationManager.ConnectionStrings("MVC_UserDBConnectionString").ConnectionString)
        'Dim Mydr As SqlDataReader = Nothing
        Using Sqlconn
            If (Sqlconn.State = ConnectionState.Closed) Then
                Sqlconn.Open()
            End If
            Dim SqlStr As String = "INSERT INTO UserTable(UserName, UserBirthDay,UserMobilePhone)" &
                                                        "VALUES  (@name,@bthday,@phone)"
            Dim MyComm As New SqlCommand(SqlStr, Sqlconn)
            MyComm.Parameters.Add("@name", SqlDbType.Char).Value = TextBox1.Text.Trim()
            MyComm.Parameters.Add("@phone", SqlDbType.Char).Value = TextBox2.Text.Trim()
            MyComm.Parameters.Add("@bthday", SqlDbType.Char).Value = TextBox3.Text.Trim()
            Try
                MyComm.ExecuteNonQuery()
            Catch ex As Exception

            End Try
            'Dim myda As New SqlDataAdapter(MyComm)
            'myda.Fill(myds, "Display_table")
            'GridView1.DataSource = myds.Tables("Display_table").DefaultView
            'GridView1.DataBind()

            MyComm.Cancel()

        End Using

    End Sub

    Protected Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar1.SelectionChanged
        TextBox3.Text = Calendar1.SelectedDate
        Calendar1.Visible = False
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Calendar1.Visible = True
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Excel匯出
        Dim FileName As String = Date.Today.ToLongDateString() & "分機表.xls"
        'Dim FileName As String = Date.Today.ToLongDateString() & "分機表.csv\"

        'First let's clean up the response.object
        Response.Clear()
        Response.Buffer = True
        'Response.Charset = "Big5"
        Response.Charset = "utf-8"

        '排除亂碼
        Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>")

        '將所有欄位格式改為"文字"
        'Response.Write("<style type=text/css>")
        'Response.Write("td{mso-number-format:\" & "\\@\" & ";}")
        'Response.Write("</style>")

        'Response.ContentEncoding = Encoding.GetEncoding("Big5")
        Response.ContentEncoding = Encoding.GetEncoding("utf-8")
        Response.ContentType = "application/vnd.ms-excel"
        'Response.ContentType = "application/text"

        'Response.AppendHeader("Content-Disposition", "attachment;filename=" & HttpUtility.UrlEncode(FileName))
        Response.AppendHeader("Content-Disposition", "attachment;filename=" & FileName)

        '關掉GridView功能
        GridView2.EnableViewState = False
        GridView2.AllowPaging = False

        'Create a string writer
        Dim sw As New StringWriter
        Dim htw As New HtmlTextWriter(sw)
        Using sw
            Using htw
                GridView2.RenderControl(htw)
                Response.Write(sw.ToString())
                Response.End()
            End Using
        End Using
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal Control As Control)
        '搭配Excel匯出
    End Sub
End Class