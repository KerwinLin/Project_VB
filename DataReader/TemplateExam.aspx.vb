Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Public Class TemplateExam
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("MVC_UserDBConnectionString").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DBInit()
            Display_A()
        End If
    End Sub

    Private Sub DBInit()
        '取得資料表結果
        Dim Sqlstring As String = "select UserId, UserName, UserSex, UserBirthDay, UserMobilePhone FROM UserTable WHERE UserStatus = 'Y'"
        Dim myda As New SqlDataAdapter(Sqlstring, conn)
        Dim myds As New DataSet
        Dim mydt As New DataTable

        Try
            myda.Fill(myds, "table")
            mydt = myds.Tables("table")
            Count_LB.Text = mydt.Rows.Count()
            GridView1.DataSource = mydt.DefaultView
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write("Error")
        End Try

    End Sub

    Private Sub Display_A()
        '取得資料表結果
        Dim Sqlstring As String = "select UserName, UserSex, UserMobilePhone FROM UserTable WHERE UserStatus = 'Y'"
        Dim myda As New SqlDataAdapter(Sqlstring, conn)
        Dim myds As New DataSet
        Dim mydt As New DataTable

        Try
            myda.Fill(myds, "Table_A")
            mydt = myds.Tables("Table_A")
            Count_LB.Text = mydt.Rows.Count()
            GridView2.DataSource = mydt.DefaultView
            GridView2.DataBind()
        Catch ex As Exception
            Response.Write("Error")
        End Try

    End Sub

    Private Sub Print_Btn_Click(sender As Object, e As EventArgs) Handles Print_Btn.Click
        '列印事件
        Page.ClientScript.RegisterClientScriptInclude("myPrint", "print.js")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using conn
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If
            Dim SqlStr As String = "INSERT INTO UserTable (UserName, UserSex, UserBirthDay, UserMobilePhone)
                                                        VALUES  (@name,@sex,@bthday,@phone)"
            Dim MyComm As New SqlCommand(SqlStr, conn)
            MyComm.Parameters.Add("@name", SqlDbType.Char).Value = TB_Name.Text.Trim()
            MyComm.Parameters.Add("@sex", SqlDbType.Char).Value = TB_Sex.Text.Trim()
            MyComm.Parameters.Add("@bthday", SqlDbType.Char).Value = TB_DB.Text.Trim()
            MyComm.Parameters.Add("@phone", SqlDbType.Char).Value = TB_Phone.Text.Trim()
            MyComm.ExecuteNonQuery()
            MyComm.Cancel()
            DBInit()
        End Using
        Response.Redirect("TemplateExam.aspx")
    End Sub

    Protected Sub GridView1_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles GridView1.RowCancelingEdit
        '取消事件
        GridView1.EditIndex = -1
        DBInit()
    End Sub

    Protected Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView1.RowEditing
        '編輯事件
        GridView1.EditIndex = e.NewEditIndex
        DBInit()
    End Sub

    Protected Sub GridView1_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        '刪除事件
        Using conn
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If
            Dim SqlStr As String = "UPDATE UserTable SET UserStatus = @Status WHERE UserId = @Id"
            Dim MyComm As New SqlCommand(SqlStr, conn)
            MyComm.Parameters.Add("@Status", SqlDbType.Char).Value = "N"
            MyComm.Parameters.Add("@Id", SqlDbType.Int).Value = CInt(GridView1.DataKeys(e.RowIndex).Value.ToString)
            MyComm.ExecuteNonQuery()
            MyComm.Cancel()
            DBInit()
        End Using
        Response.Redirect("TemplateExam.aspx")
        DBInit()
    End Sub

    Protected Sub GridView1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridView1.RowUpdating
        '非樣板寫法
        'Dim Name, Sex, Bd, Phone As TextBox
        'Name = GridView1.Rows(e.RowIndex).Cells(1).Controls(0)
        'Sex = GridView1.Rows(e.RowIndex).Cells(2).Controls(0)
        'Bd = GridView1.Rows(e.RowIndex).Cells(3).Controls(0)
        'Phone = GridView1.Rows(e.RowIndex).Cells(4).Controls(0)

        '可用另一種物件轉字串寫法
        Dim Name, Sex, Bd, Phone As TextBox
        Name = GridView1.Rows(e.RowIndex).Cells(1).FindControl("TBName")
        Sex = GridView1.Rows(e.RowIndex).Cells(2).FindControl("TBSex")
        Bd = GridView1.Rows(e.RowIndex).Cells(3).FindControl("TBBD")
        Phone = GridView1.Rows(e.RowIndex).Cells(4).FindControl("TBPhone")

        Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("MVC_UserDBConnectionString").ConnectionString)
        Dim Sqlstring As String = "UPDATE  UserTable
                                                            SET           UserName = @Name, UserSex = @Sex, UserBirthDay = @BD, UserMobilePhone = @Phone
                                                            WHERE   (UserId = @Id)"

        Dim myda As SqlDataAdapter = New SqlDataAdapter
        myda.UpdateCommand = New SqlCommand(Sqlstring, conn)
        myda.UpdateCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name.Text.Trim()
        myda.UpdateCommand.Parameters.Add("@Sex", SqlDbType.NChar).Value = Sex.Text.Trim()
        myda.UpdateCommand.Parameters.Add("@BD", SqlDbType.DateTime).Value = Bd.Text.Trim()
        myda.UpdateCommand.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = Phone.Text.Trim()
        myda.UpdateCommand.Parameters.Add("@Id", SqlDbType.Int).Value = CInt(GridView1.DataKeys(e.RowIndex).Value.ToString)

        '方法A
        conn.Open()
        myda.UpdateCommand.ExecuteNonQuery()
        myda.Dispose()

        '方法B 建議寫法
        'Dim myds As New DataSet
        'myda.SelectCommand = New SqlCommand("select * from UserTable", conn)
        'myda.Fill(myds, "ViewTable")
        'myds.Tables("ViewTable").Rows(e.RowIndex).Item("UserName") = Name.Text.Trim()
        'myds.Tables("ViewTable").Rows(e.RowIndex).Item("UserSex") = Sex.Text.Trim()
        'myds.Tables("ViewTable").Rows(e.RowIndex).Item("UserBirthDay") = Bd.Text.Trim()
        'myds.Tables("ViewTable").Rows(e.RowIndex).Item("UserMobilePhone") = Phone.Text.Trim()
        'myda.Update(myds, "ViewTable")

        GridView1.EditIndex = -1
        DBInit()
    End Sub

End Class