<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoAuth.aspx.cs" Inherits="adm_Login_NoAuth" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;font-family:微軟正黑體">
    
        <br />
        <br />
        <br />
        <br />
    
        沒有使用權限<br />
        請使用<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/admin/Login/Login.aspx" Target="_top">登入</asp:HyperLink>
        有權限的帳號</div>
    </form>
</body>
</html>
