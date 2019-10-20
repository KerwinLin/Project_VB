Imports System.Data.SqlClient
Imports System.Web.Configuration

Public Class Extension
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(WebConfigurationManager.ConnectionStrings("MVC_UserDBConnectionString").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DBInit()
        End If
    End Sub

    Private Sub DBInit()
        '取得資料表結果
        Dim Sqlstring As String = "SELECT  UserId, CASE WHEN UserName IS NULL THEN UserMemo ELSE UserName END AS UserName, UserSex, UserBirthDay, 
                                                        UserMobilePhone, UserStatus, UserMemo 
                                                        FROM      UserTable 
                                                        WHERE   (UserStatus = 'Y')"
        Dim myda As New SqlDataAdapter(Sqlstring, conn)
        Dim myds As New DataSet
        Dim mydt As New DataTable
        Try
            myda.Fill(myds, "ViewTable")
            mydt = myds.Tables("ViewTable")
            GridView1.DataSource = mydt.DefaultView
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write("Error")
        End Try
    End Sub

    Protected Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView1.RowEditing
        '編輯事件
        GridView1.EditIndex = e.NewEditIndex
        DBInit()
    End Sub

    Protected Sub GridView1_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles GridView1.RowCancelingEdit
        GridView1.EditIndex = -1
        DBInit()
    End Sub

    Protected Sub GridView1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridView1.RowUpdating

        Dim sex, Name, Bd, Phone, Memo As String
        Name = CType(GridView1.Rows(e.RowIndex).Cells(1).FindControl("txtName"), TextBox).Text.Trim()
        sex = CType(GridView1.Rows(e.RowIndex).Cells(2).FindControl("lblSex"), Label).Text.Trim()
        Bd = CType(GridView1.Rows(e.RowIndex).Cells(3).FindControl("txtBD"), TextBox).Text.Trim()
        Phone = CType(GridView1.Rows(e.RowIndex).Cells(4).FindControl("txtMP"), TextBox).Text.Trim()
        Memo = CType(GridView1.Rows(e.RowIndex).Cells(5).FindControl("lblMemo"), Label).Text.Trim()

        Dim Sqlstring As String = "UPDATE  UserTable
                                                            SET           UserName = @Name, UserBirthDay = @BD, UserMobilePhone = @Phone, "
        If sex = "M" Then
            Sqlstring += "UserMemo = @Memo "
        End If
        Sqlstring += "WHERE   (UserId = @Id)"

        Dim myda As SqlDataAdapter = New SqlDataAdapter
        myda.UpdateCommand = New SqlCommand(Sqlstring, conn)
        myda.UpdateCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name
        myda.UpdateCommand.Parameters.Add("@Sex", SqlDbType.NChar).Value = sex
        myda.UpdateCommand.Parameters.Add("@BD", SqlDbType.NVarChar).Value = Bd
        myda.UpdateCommand.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = Phone
        myda.UpdateCommand.Parameters.Add("@Memo", SqlDbType.NVarChar).Value = Name
        myda.UpdateCommand.Parameters.Add("@Id", SqlDbType.Int).Value = CInt(GridView1.DataKeys(e.RowIndex).Value.ToString)

        conn.Open()
        myda.UpdateCommand.ExecuteNonQuery()
        myda.Dispose()

        GridView1.EditIndex = -1
        DBInit()
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If (e.Row.RowType <> DataControlRowType.DataRow) Then
            Return
        End If
        Try
            If (e.Row.RowType = DataControlRowType.DataRow) Then
                If (e.Row.DataItem("UserName")) <> "R" Then
                    e.Row.FindControl("txtName").Visible = False
                    'e.Row.Cells(1).Controls.Clear 
                Else
                    e.Row.FindControl("lblName2").Visible = False
                End If
                'RowState 等於哪種狀態上 顯示哪種狀態的 Template
                'e.Row.RowState = DataControlRowState.Edit
                'e.Row.RowState = DataControlRowState.Normal
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class