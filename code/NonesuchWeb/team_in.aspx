<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="team_in.aspx.cs" Inherits="team_in" %>

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
            <td width="51%"><div class="Team01_in_P">
                <asp:Image ID="imgPicFileName" runat="server" Width="342" Height="342" /></div></td>
            <td width="49%" valign="top">
            <div  class="Team01_in_Txt">
               學歷：<asp:Literal ID="ltlCustomField1" runat="server" Text='<%# Bind("CustomField1") %>'></asp:Literal><br />
               主治項目：<br />
               <asp:Literal ID="ltlCustomField2" runat="server" Text='<%# Bind("CustomField2") %>'></asp:Literal><br />
               醫師經歷：<br />
               <asp:Literal ID="ltlContent" runat="server"></asp:Literal>
            </div>
            </td>
          </tr>
        </table>
      </div>
    </div>
</asp:Content>

