<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="prety.aspx.cs" Inherits="prety" %>

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
            <p>美麗見證><asp:Literal ID="ltlNodeName2" runat="server"></asp:Literal></p>
        </div>
          <asp:Repeater ID="RepeaterPrety" runat="server" onitemdatabound="RepeaterPrety_ItemDataBound">
                    <ItemTemplate>
                        <div class="Perty_Down">
          <h1><asp:Literal ID="ltlTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Literal></h1>
          <a href="prety_in.aspx?id=<%# Eval("PostId") %>"><img src="upload/<%# Eval("PicFileName")%>" width="270" height="177"></a>
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

