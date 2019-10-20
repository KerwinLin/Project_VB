<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm1.aspx.vb" Inherits="Project_VB.WebForm1" EnableEventValidation = "false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        姓名：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        電話：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        日期：<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:Button ID="Button3" runat="server" Text="選擇日期" />
        <asp:Button ID="Button2" runat="server" Text="送出" />
        <asp:Button ID="Button4" runat="server" Text="匯出" />
        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" NextPrevFormat="ShortMonth" Visible="False" Width="330px">
            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
            <DayStyle BackColor="#CCCCCC" />
            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
            <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
            <TodayDayStyle BackColor="#999999" ForeColor="White" />
        </asp:Calendar>
        <div>
            <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" AutoGenerateColumns="False">
                <Columns>
                    <asp:CommandField ShowEditButton="True" ButtonType="Button" />
                    <asp:BoundField DataField="UserName" HeaderText="姓名" />
                    <asp:BoundField DataField="UserMobilePhone" HeaderText="電話" />
                </Columns>
                <EmptyDataTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("UserName") %>'></asp:TextBox>
                </EmptyDataTemplate>
                <FooterStyle BackColor="White" ForeColor="#333333" />
                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                <PagerTemplate>
                    <asp:Button ID="Button1" runat="server" CommandArgument="Prev" CommandName="page" Text="Prev" />
                    <asp:Button ID="Next" runat="server" CommandArgument="Next" CommandName="page" Text="Next" />
                </PagerTemplate>
                <RowStyle BackColor="White" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#487575" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#275353" />
            </asp:GridView>
            <asp:GridView ID="GridView2" runat="server"></asp:GridView>
            <br />
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </div>
    </form>
</body>
</html>
