<%@ Page Language="C#" AutoEventWireup="true" CodeFile="leftmenu.aspx.cs" Inherits="admin_leftmenu" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Menu List</title>
    <link href="css/ajaxcss.css" rel="stylesheet" type="text/css" />
    <link href="css/admin.css" rel="stylesheet" type="text/css" />
    <link href="css/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        html, body
        {
            width: 100%;
            height: 100%;
            
        }
        .style1
        {
            height: 12px;
        }
    </style>
</head>
<body onkeydown="return (event.keyCode!=8)">
    <form id="form1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <table border="0" cellpadding="0" cellspacing="0" class="tablenoleftmargin">
                <tr>
                    <td>
                        <img src="images/left_1.gif" width="228" border="0" height="53">
                    </td>
                </tr>
                <tr>
                    <td style="font-family: 微軟正黑體" valign="top" class="menuTd">
                        <asp:TreeView ID="tvMenu" runat="server" ExpandDepth="0" CollapseImageUrl="~/admin/images/icon.gif"
                            ExpandImageUrl="~/admin/images/icon.gif" OnSelectedNodeChanged="tvMenu_SelectedNodeChanged"
                            OnUnload="tvMenu_Unload" Style="width: 50%; text-align: left;" PopulateNodesFromClient="False"
                            NodeIndent="0" Font-Names="Verdana,微軟正黑體">
                            <ParentNodeStyle CssClass="menuParent" ForeColor="#000000" />
                            <SelectedNodeStyle ForeColor="#000000" />
                            <NodeStyle VerticalPadding="5" HorizontalPadding="3" />
                            <LeafNodeStyle CssClass="menuNode" />
                            <RootNodeStyle CssClass="menuParent" />
                        </asp:TreeView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="images/left_3.gif" width="228" border="0" height="15" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
