<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftPrety.ascx.cs" Inherits="UserControl_LeftPrety" %>
<div id="LeftBtn_Table">
        <h1>服務項目</h1>
        <ul>
            <asp:Repeater ID="RepeaterMenu" runat="server">
                    <ItemTemplate>
                        <li><a href="prety.aspx?node=<%# Eval("NodeId") %>"><asp:Literal ID="ltlName" runat="server" Text='<%# Bind("Name") %>'></asp:Literal></a></li>
                    </ItemTemplate>
                </asp:Repeater>
        </ul>
      </div>