<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="news_in.aspx.cs" Inherits="news_in" %>

<%@ Register Src="UserControl/LeftNews.ascx" TagName="LeftMenu" TagPrefix="Newss" %>
<%@ Register Assembly="aspnetpager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="LeftBtnDown">      
    <newss:LeftMenu ID="leftMenu" runat="server" />
      <div id="AllInOB">
        <div id="AllTitle">
          <asp:Literal ID="ltlNodeName" runat="server"></asp:Literal>
            <p>最新消息><asp:Literal ID="ltlNodeName2" runat="server"></asp:Literal></p>
        </div>
        <div id="Stitle"><asp:Literal ID="ltlTitle" runat="server"></asp:Literal></div>
        <div class="AllHTML">
          <asp:Literal ID="ltlContent" runat="server"></asp:Literal>
        </div>
      </div>
    </div>
</asp:Content>

