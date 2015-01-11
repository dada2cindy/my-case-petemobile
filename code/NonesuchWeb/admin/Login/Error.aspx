<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="admin_Login_Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>請重新登入</title>
    <link href="../css/admin.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
              font-family: "微軟正黑體";
       
            margin: 0 auto;
            background-attachment: fixed;
            background-position: top;
            background-color: #FFF;
                 
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;font-family:微軟正黑體">
    
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:ImageButton ID="ImageButton1" runat="server" 
            ImageUrl="~/admin/images/inner/exclamtion_mark.png" />
&nbsp;<br />
        有可能是您的閒置時間過長導致您已被登出。<br />
        請重新<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/admin/Login/Login.aspx" Target="_top">登入</asp:HyperLink>。</div>
    </form>
</body>
</html>
