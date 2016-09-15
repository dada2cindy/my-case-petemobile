<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="admin_Login_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title> 品讚行動通訊聯合系統</title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function changeImg() {
            document.getElementById("safecode").src = "../captcha/Handler.ashx?" + Math.random();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td height="34" background="../images/white.gif" class="text01-12-black">
                        <font size="3"> 品讚行動通訊聯合系統</font>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100" border="0" cellpadding="0" cellspacing="0" class="table01">
                            <tr>
                                <%--<td width="467" height="388" align="center">
                                    <img src="../../image/logo.png"/>
                                </td>--%>
                                <td width="511">
                                    <table width="393" border="0" align="center" cellpadding="9" cellspacing="0" class="logintable">
                                        <tr>
                                            <td colspan="2" valign="top">
                                                <img src="../images/login.gif" width="181" height="21" />
                                                <asp:Panel ID="pnlDebugMode" runat="server" Visible="false">
                                                    <asp:DropDownList ID="ddlTestAccount" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTestAccount_SelectedIndexChanged">
                                                        <asp:ListItem Value="-1">快速登入</asp:ListItem>
                                                        <asp:ListItem Value="admin">admin(系統管理員)</asp:ListItem>
                                                    </asp:DropDownList>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="187" colspan="2" bgcolor="#E3EAED">
                                                <table width="374" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="84" height="25" class="text02-black-b-12">
                                                            帳號：
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtId" runat="server" CssClass="order01"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="25" class="text02-black-b-12">
                                                            密碼：
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtPw" runat="server" TextMode="Password" CssClass="order01"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="25" class="text02-black-b-12">
                                                            驗證碼：
                                                        </td>
                                                        <td width="56">
                                                            <asp:TextBox ID="txtConfirmationCode" runat="server" CssClass="order01"></asp:TextBox>
                                                        </td>
                                                        <td width="67" align="center">
                                                            <img id="safecode" src="../captcha/Handler.ashx" width="50" height="25" />
                                                        </td>
                                                        <td width="167">
                                                            <a href="javascript:changeImg();" class="btn2">更換驗證碼</a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="35" colspan="4" align="center" class="text02-black-b-12">
                                                            <table width="100" border="0" align="center" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnLogin" runat="server" Text="送出" name="button" CssClass="btn" OnClick="btnLogin_Click" />
                                                                        <asp:Button ID="btnReset" runat="server" Text="重填" name="button" CssClass="btn" OnClick="btnReset_Click" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
