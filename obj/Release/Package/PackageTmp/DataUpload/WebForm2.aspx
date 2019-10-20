<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm2.aspx.vb" Inherits="Project_VB.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>檔案上傳</h1>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="Button1" runat="server" Text="上傳" /><br />
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="uploadstatuslabel" runat="server" Text="Label"></asp:Label>
        </div>
    </form>
</body>
</html>
