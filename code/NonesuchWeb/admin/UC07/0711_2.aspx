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
                        業績月份:
                        <asp:DropDownList ID="ddlSearchYear" runat="server" ValidationGroup="Search">
                        </asp:DropDownList>
                        年
                        &nbsp;
                        <asp:DropDownList ID="ddlSearchMonth" runat="server" ValidationGroup="Search">
                        </asp:DropDownList>
                        月                        
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

