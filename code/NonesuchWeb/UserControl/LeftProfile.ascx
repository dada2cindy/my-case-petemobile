<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftProfile.ascx.cs" Inherits="UserControl_LeftProfile" %>
<div id="LeftBtn_Table">
        <h1>關於臻美</h1>
        <ul>
            <asp:Repeater ID="RepeaterMenu" runat="server">
                    <ItemTemplate>
                        <li><a href="profile.aspx?id=<%# Eval("PostId") %>"><asp:Literal ID="ltlTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Literal></a></li>
                    </ItemTemplate>
                </asp:Repeater>
        </ul>
      </div>