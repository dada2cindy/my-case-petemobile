<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="admin_index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>臻美美學診所 整合平台</title>
    <frameset rows="60,*" frameborder="no" border="0">
      <frame name="top" src="top.aspx"> 
<frameset id="mainFrameset" frameborder="no" cols="17%,12px,*" border="0">
   <frame name="treefrm" target="mainfrm" src="leftmenu.aspx" scrolling="auto" marginwidth="0" marginheight="0">
  <frame name="togglefrm"  src="toggle.aspx" scrolling="auto" marginwidth="0" marginheight="0">
  <frame name="mainfrm" src="welcome.aspx" scrolling="auto" marginwidth="0" marginheight="0">
</frameset>
</frameset>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    </form>
</body>
</html>
