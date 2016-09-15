<%@ Page Language="C#" AutoEventWireup="true" CodeFile="top.aspx.cs" Inherits="admin_top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> 品讚行動通訊聯合系統</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" align="center" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td height="34" class="text01-12-black" colspan="2">
                    <font size="3"> 品讚行動通訊聯合系統</font>
                </td>
            </tr>
            <tr>
                <td height="20" bgcolor="#ffffff" class="text01-12-black">
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </td>
                <td align="right" bgcolor="#ffffff" class="text01-12-black">
                    <asp:LinkButton ID="btnLogOut" runat="server" OnClick="btnLogOut_Click" CssClass="text01-12-black">登出</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
