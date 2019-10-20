<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TemplateExam.aspx.vb" Inherits="Project_VB.TemplateExam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="https://code.jquery.com/jquery-3.1.0.min.js"></script>
    <script src="../js/print.js" type="text/javascript"></script>
    <link href="../Styles/MyStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="jquery-tabs" style="width: auto">

            <div id="tabs-nav">
                <a href="#tab0" class="tabs-menu tabs-menu-active">維護</a>
                <a href="#tab1" class="tabs-menu">新增</a>
                <a href="#tab2" class="tabs-menu">列印</a>
            </div>

            <div class="tabs-container">

                <div id="tab0" class="tabs-panel" style="display: block">
                    總共:<asp:Label ID="Count_LB" runat="server" Text=""></asp:Label>筆資料
                     
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="UserId" AllowPaging="True" AllowSorting="True">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                            <asp:TemplateField HeaderText="姓名">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UserName") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="性別">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("UserSex") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("UserSex") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="生日">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("UserBirthDay") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("UserBirthDay") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="電話">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("UserMobilePhone") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("UserMobilePhone") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>
                </div>

                <div id="tab1" class="tabs-panel">
                    姓名：<asp:TextBox ID="TB_Name" runat="server" ControlToValidate="TB_Name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" SetFocusOnError="True" ForeColor="#CC0000" ControlToValidate="TB_Name"></asp:RequiredFieldValidator>
                    <br />
                    性別：<asp:TextBox ID="TB_Sex" runat="server"></asp:TextBox>
                    <br />
                    生日：<asp:TextBox ID="TB_DB" runat="server"></asp:TextBox>
                    <br />
                    電話：<asp:TextBox ID="TB_Phone" runat="server"></asp:TextBox>
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="新增" />
                </div>

                <div id="tab2" class="tabs-panel">
                    <asp:Button ID="Print_Btn" runat="server" Text="列印" OnClientClick="printScreen(print_parts)" />
                    <div id="print_parts">
                        <asp:GridView ID="GridView2" runat="server"></asp:GridView>
                    </div>
                </div>

            </div>
        </div>

    </form>
</body>
</html>
