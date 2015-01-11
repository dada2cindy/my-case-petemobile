<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="team.aspx.cs" Inherits="team" %>

<%@ Register Src="UserControl/LeftTeam.ascx" TagName="LeftMenu" TagPrefix="team" %>
<%--<%@ Register Assembly="aspnetpager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="LeftBtnDown">      
    <team:LeftMenu ID="leftMenu" runat="server" />
      <div id="AllInOB">
        <div id="AllTitle">
          醫師陣容
            <p>醫療團隊>醫師陣容</p>
        </div>
          <asp:Repeater ID="RepeaterService" runat="server" onitemdatabound="RepeaterService_ItemDataBound">
                    <ItemTemplate>
                        <div class="Team01_Table"> 
          <h1><asp:Literal ID="ltlTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Literal></h1>
          <a href="team_in.aspx?id=<%# Eval("PostId") %>"><img src="upload/<%# Eval("PicFileName")%>" width="175" height="175"></a>
          <h2><span>學歷：</span><asp:Literal ID="ltlCustomField1" runat="server" Text='<%# Bind("CustomField1") %>'></asp:Literal></h2>
<h3>主治：<br>
              <asp:Literal ID="ltlCustomField2" runat="server" Text='<%# Bind("CustomField2") %>'></asp:Literal></h3>
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

