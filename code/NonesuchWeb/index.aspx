<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<%@ Register Src="UserControl/MainFooter.ascx" TagName="Footer" TagPrefix="Main" %>
<%@ Register Src="UserControl/MainTopMenu.ascx" TagName="TopMenu" TagPrefix="Main" %>
<!DOCTYPE HTML>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>臻美美學診所</title>
    <link href="css/Webcss.css" rel="stylesheet" type="text/css" />
    <link href="css/flexslider.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.flexslider.js" type="text/javascript"></script>
    <script type="text/javascript" charset="utf-8">
        $(window).load(function () {
            $('.flexslider').flexslider({
                animation: "slide",
                //controlNav: false,
                //directionNav: false,
                slideshowSpeed: 5000
            });
            $('.flexslider2').flexslider({
                animation: "slide",
                controlNav: false,
                //directionNav: false,
                slideshowSpeed: 3000
            });
            $('.flexslider3').flexslider({
                animation: "slide",
                controlNav: false,
                //directionNav: false,
                slideshowSpeed: 2000
            });
            $('.flexslider4').flexslider({
                animation: "slide",
                controlNav: false,
                //directionNav: false,
                slideshowSpeed: 4000
            });
        });
    </script>
</head>

<body>
<div id="IndexDown">
  <div class="AllTable">
    <main:TopMenu ID="topMenu" runat="server" />
    
    <div id="Slogn"></div>
    <div id="IndexAdv" class="flexslider">
        <ul class="slides">
            <asp:Repeater ID="RepeaterAdv" runat="server">
                <ItemTemplate>
                    <li>
                        <a href="<%# Eval("LinkUrl") %>" target="_blank"><img src="upload/<%# Eval("PicFileName")%>" width="430" height="210" alt="<%# Eval("Title")%>" /></a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div class="Adv_Number">&nbsp;</div>
    
    <div class="News_Table">
      <div class="News_Table01">
        <div class="News_Title">
          <img src="image/Adv_t1.png" width="85" height="25">
          <%--<ul>
            <li>1</li>
            <li>2</li>
            <li>3</li>
            <li>4</li>
            <li>5</li>
          </ul>--%>
        
        </div>
        <div class="flexslider2">
            <ul class="slides">
                <asp:Repeater ID="RepeaterNews" runat="server" onitemdatabound="RepeaterNews_ItemDataBound">
                    <ItemTemplate>
                        <li>
                    <img src="upload/<%# Eval("PicFileName")%>" width="93" height="93" class="News_PP">
        <h1><a href="news_in.aspx?id=<%# Eval("PostId") %>"><asp:Literal ID="ltlTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Literal></a></h1>
        <h2><asp:Literal ID="ltlContent" runat="server"></asp:Literal></h2>
        <p><a href="news_in.aspx?id=<%# Eval("PostId") %>"><img src="image/more.png" width="32" height="16"></a></p>
                </li>
                    </ItemTemplate>
                </asp:Repeater>                
            </ul>        
        </div>
      </div>
      <div class="News_Table02">
        <div class="News_Title2"> <img src="image/Adv_t2.png" width="84" height="25"></div>
        <div class="News_Back"></div>
          <div class="flexslider3">
              <ul class="slides">                
                    <asp:Repeater ID="RepeaterService" runat="server"  OnItemDataBound="RepeaterService_ItemDataBound">
                        <ItemTemplate>
                        <li>
                            <table border="0" cellpadding="0" cellspacing="0" class="News_Service">
          <tr>
            <td width="185" valign="top">
              <h1><a href="service.aspx?node=<%# Eval("Node1.NodeId") %>"><asp:Literal ID="ltlName" runat="server"></asp:Literal></a></h1>
              <h2><asp:Literal ID="ltlContent" runat="server"></asp:Literal></h2>
              <p><a href="service.aspx?node=<%# Eval("Node1.NodeId") %>"><img src="image/more.png" width="32" height="16"></a></p>
            </td>
            <td width="185" valign="top"><h1><a href="service.aspx?node=<%# Eval("Node2.NodeId") %>"><asp:Literal ID="ltlName2" runat="server"></asp:Literal></a></h1>
              <h2><asp:Literal ID="ltlContent2" runat="server"></asp:Literal></h2>
              <p><a href="service.aspx?node=<%# Eval("Node2.NodeId") %>">
                  <asp:Image ID="imgMore2" runat="server" Width="32" Height="16" ImageUrl="image/more.png" /></a></p></td>
          </tr>
        </table>
        </li>
                        </ItemTemplate>
                    </asp:Repeater>                                        
              </ul>
          </div>
<div class="News_Next"></div>
      </div>
      <div class="News_Table01">
        <div class="News_Title">
          <img src="image/Adv_t3.png" width="85" height="25">
        </div>
        <div class="News_Next"></div>
<div class="News_Back"></div>
<div class="flexslider4">
    <ul class="slides">
        <asp:Repeater ID="RepeaterPrety" runat="server">
            <ItemTemplate>
                <li>
                    <a href="prety_in.aspx?id=<%# Eval("PostId") %>"><img src="upload/<%# Eval("PicFileName")%>" width="195" height="85" alt="<%# Eval("Title") %>"></a>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
      </div>
    </div>
  </div>
</div>

<main:Footer ID="footer" runat="server" />
</body>
</html>
