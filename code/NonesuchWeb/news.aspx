<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="news.aspx.cs" Inherits="news" %>

<%@ Register Src="UserControl/LeftNews.ascx" TagName="LeftMenu" TagPrefix="Newss" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="LeftBtnDown">      
    <newss:LeftMenu ID="leftMenu" runat="server" />
      <div id="AllInOB">
        <div id="AllTitle">
          臻美快訊
          <p>最新消息>臻美快訊</p>
        </div>
          <asp:Repeater ID="RepeaterNews" runat="server">
                    <ItemTemplate>
                        <div class="HotNews_PP"><a href="news_in.aspx?id=<%# Eval("PostId") %>"><img src="upload/<%# Eval("PicFileName")%>" width="730" height="355" alt="<%# Eval("Title")%>"></a></div>                        
                    </ItemTemplate>
                </asp:Repeater>
      </div>
    </div>
</asp:Content>

