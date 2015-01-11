<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="team2_in.aspx.cs" Inherits="team2_in" %>

<%@ Register Src="UserControl/LeftTeam.ascx" TagName="LeftMenu" TagPrefix="team" %>
<%@ Register Assembly="aspnetpager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="LeftBtnDown">      
    <team:LeftMenu ID="leftMenu" runat="server" />
      <div id="AllInOB">
        <div id="AllTitle">
          <asp:Literal ID="ltlNodeName" runat="server"></asp:Literal>
            <p>醫療團隊><asp:Literal ID="ltlNodeName2" runat="server"></asp:Literal></p>
        </div>
        <div id="Stitle"><asp:Literal ID="ltlTitle" runat="server"></asp:Literal></div>
        <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td width="46%">
            <asp:Image ID="imgPicFileName" runat="server" Width="301" Height="301" /></td>
            <td width="54%" valign="top">
              <div class="Team02_t01">主要功能</div>
              <h1 class="Team02_H1"><asp:Literal ID="ltlCustomField2" runat="server" Text='<%# Bind("CustomField2") %>'></asp:Literal></h1>
              <div class="Team02_t01">設備簡述</div>
              <h1 class="Team02_H1"><asp:Literal ID="ltlContent" runat="server"></asp:Literal></h1>
            </td>
          </tr>
        </table>
      </div>
    </div>
</asp:Content>

