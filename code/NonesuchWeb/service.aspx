<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="service.aspx.cs" Inherits="service" %>

<%@ Register Src="UserControl/LeftService.ascx" TagName="LeftMenu" TagPrefix="service" %>
<%@ Register Assembly="aspnetpager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="LeftBtnDown">      
    <service:LeftMenu ID="leftMenu" runat="server" />
      <div id="AllInOB">
        <div id="AllTitle">
          <asp:Literal ID="ltlNodeName" runat="server"></asp:Literal>
            <p>服務項目><asp:Literal ID="ltlNodeName2" runat="server"></asp:Literal></p>
        </div>
          <asp:Repeater ID="RepeaterService" runat="server" onitemdatabound="RepeaterService_ItemDataBound">
                    <ItemTemplate>
                        <div class="Shaer_Table"> <a href="service_in.aspx?id=<%# Eval("PostId") %>"><img src="upload/<%# Eval("PicFileName")%>" width="210" height="170"></a>
          <h1><asp:Literal ID="ltlTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Literal></h1>
          <h2><asp:Literal ID="ltlContent" runat="server" Text=''></asp:Literal></h2>
        </div>
                    </ItemTemplate>
                </asp:Repeater>        
                
        <div id="NumberPage">
            <Webdiyer:AspNetPager ID="AspNetPager1" runat="server"
                    OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxType="TextBox" PageSize="10"
                    ShowPageIndexBox="Never" ShowFirstLast="false" PrevPageText="上一頁" NextPageText="下一頁" /></div>
      </div>
    </div>
</asp:Content>

