<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftService.ascx.cs" Inherits="UserControl_LeftService" %>
<div id="LeftBtn_Table">
        <h1>服務項目</h1>
        <ul>
            <asp:Repeater ID="RepeaterMenu" runat="server">
                    <ItemTemplate>
                        <li><a href="service.aspx?node=<%# Eval("NodeId") %>"><asp:Literal ID="ltlName" runat="server" Text='<%# Bind("Name") %>'></asp:Literal></a></li>
                    </ItemTemplate>
                </asp:Repeater>
        </ul>
      </div>