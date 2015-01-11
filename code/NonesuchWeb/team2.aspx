<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="team2.aspx.cs" Inherits="team2" %>

<%@ Register Src="UserControl/LeftTeam.ascx" TagName="LeftMenu" TagPrefix="team" %>
<%--<%@ Register Assembly="aspnetpager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="LeftBtnDown">      
    <team:LeftMenu ID="leftMenu" runat="server" />
      <div id="AllInOB">
        <div id="AllTitle">
          儀器設備
            <p>醫療團隊>儀器設備</p>
        </div>
          <asp:Repeater ID="RepeaterService" runat="server" onitemdatabound="RepeaterService_ItemDataBound">
                    <ItemTemplate>
                        <div class="Team02_Table"> <a href="team2_in.aspx?id=<%# Eval("PostId") %>"><img src="upload/<%# Eval("PicFileName")%>" width="340" height="213"></a>
          <h1><asp:Literal ID="ltlTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Literal></h1>
          <div class="Team02_Stxt">主要功能</div>
          <h2><asp:Literal ID="ltlCustomField2" runat="server" Text='<%# Bind("CustomField2") %>'></asp:Literal></h2>
          <div class="Team02_Stxt02">相關療程：<asp:Literal ID="ltlCustomField1" runat="server" Text='<%# Bind("CustomField1") %>'></asp:Literal></div>
        </div>
                    </ItemTemplate>
                </asp:Repeater>        
                
        <%--<div id="NumberPage">
            <Webdiyer:AspNetPager ID="AspNetPager1" runat="server"
                    OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxType="TextBox" PageSize="6"
                    ShowPageIndexBox="Never" ShowFirstLast="false" PrevPageText="上一頁" NextPageText="下一頁" /></div>
      </div>--%>
    </div>
</asp:Content>

