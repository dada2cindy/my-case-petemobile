<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="prety_in.aspx.cs" Inherits="prety_in" %>

<%@ Register Src="UserControl/LeftPrety.ascx" TagName="LeftMenu" TagPrefix="prety" %>
<%@ Register Assembly="aspnetpager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="LeftBtnDown">      
    <prety:LeftMenu ID="leftMenu" runat="server" />
      <div id="AllInOB">
        <div id="AllTitle">
          <asp:Literal ID="ltlNodeName" runat="server"></asp:Literal>
            <p>服務項目><asp:Literal ID="ltlNodeName2" runat="server"></asp:Literal></p>
        </div>
        <div id="Stitle"><asp:Literal ID="ltlTitle" runat="server"></asp:Literal></div>
        <div class="AllHTML">
          <asp:Literal ID="ltlContent" runat="server"></asp:Literal>
        </div>
      </div>
    </div>
</asp:Content>

