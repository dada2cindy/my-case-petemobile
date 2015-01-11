<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="news3.aspx.cs" Inherits="news3" %>

<%@ Register Src="UserControl/LeftNews.ascx" TagName="LeftMenu" TagPrefix="Newss" %>
<%@ Register Assembly="aspnetpager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="LeftBtnDown">      
    <newss:LeftMenu ID="leftMenu" runat="server" />
      <div id="AllInOB">
        <div id="AllTitle">
          療程分享
            <p>最新消息>療程分享</p>
        </div>
          <asp:Repeater ID="RepeaterNews" runat="server" onitemdatabound="RepeaterNews_ItemDataBound">
                    <ItemTemplate>
                        <div class="Shaer_Table"> <a href="news_in.aspx?id=<%# Eval("PostId") %>"><img src="upload/<%# Eval("PicFileName")%>" width="210" height="170"></a>
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

