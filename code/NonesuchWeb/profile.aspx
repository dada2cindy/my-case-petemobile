<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="profile" %>

<%@ Register Src="UserControl/LeftProfile.ascx" TagName="LeftMenu" TagPrefix="profile" %>
<%@ Register Assembly="aspnetpager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="LeftBtnDown">      
    <profile:LeftMenu ID="leftMenu" runat="server" />
      <div id="AllInOB">
        <div id="AllTitle">
          <asp:Literal ID="ltlNodeName" runat="server"></asp:Literal>
            <p>關於臻美><asp:Literal ID="ltlNodeName2" runat="server"></asp:Literal></p>
        </div>
        <div id="Stitle"><asp:Literal ID="ltlTitle" runat="server"></asp:Literal></div>
        <div class="AllHTML">
          <asp:Literal ID="ltlContent" runat="server"></asp:Literal>
        </div>
      </div>
    </div>
</asp:Content>

