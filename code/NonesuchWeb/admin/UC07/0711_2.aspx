<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdmMasterPage.master" AutoEventWireup="true" CodeFile="0711_2.aspx.cs" Inherits="admin_UC07_0711_2" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="aspnetpager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
    <asp:Panel ID="pnlGv" runat="server" DefaultButton="btnSearch">
        <fieldset class="fieldset">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td align="left">
                    <br />   
                        日期:                     
                        <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender7" runat="server" CssClass="MyCalendar"
                            PopupPosition="BottomLeft" Format="yyyy/MM/dd" PopupButtonID="calendar10" TargetControlID="txtDate" />
                        <asp:Image ID="calendar10" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDate"
                            ErrorMessage="請輸入申辦日期" Display="Dynamic" ValidationGroup="Search"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtDate" Display="Dynamic" ValidationGroup="Search" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>           
                        <br /><br />
                        <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" ValidationGroup="Search" />                                                
                        <br />
                        <%--<br />--%>
                        <%--<div class="customNumber">未核發佣金總額：<asp:Label ID="lblNotGetCommission" runat="server" Text="0" ForeColor="Red"></asp:Label></div>--%>
                    </td>
                    <td align="right" class="labelText">  
                             <asp:HyperLink runat="server" NavigateUrl="0711.aspx">業績報表</asp:HyperLink>                 
                    </td>
                </tr>
            </table>
            <br />
            
            <br />     
        </fieldset>
    </asp:Panel>
</asp:Content>

