<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdmMasterPage.master" AutoEventWireup="true" CodeFile="0602.aspx.cs" Inherits="admin_UC06_0602" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlContent" runat="server">
        <fieldset class="fieldset">
            <h1>
                聯絡我們信箱編輯</h1>
            <table style="width: 100%" class="FormTable">
                
                <tr>
                    <td>
                        姓名
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Width="280"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="請輸入姓名"
                            ControlToValidate="txtName"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Email
                    </td>
                    <td>
                        <asp:TextBox ID="txtValue" runat="server" CssClass="order-01" Width="280"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="請輸入電子信箱"
                            ControlToValidate="txtValue" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="電子信箱格式不正確"
                            Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ControlToValidate="txtValue"></asp:RegularExpressionValidator>
                    </td>
                </tr>                
                <tr>
                    <td colspan="2" height="30px" align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="確定新增" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnSave" runat="server" Text="儲存修改" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" CausesValidation="false" />
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" EnableViewState="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlGv" runat="server">
        <fieldset class="fieldset">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td align="left">
                        <asp:Button ID="btnShowAdd" runat="server" Text="新增Email" OnClick="btnShowAdd_Click" />
                    </td>
                    <td align="right" class="labelText">
                      <%--  分類搜尋:<asp:DropDownList ID="ddlSelect" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelect_SelectedIndexChanged">
                        </asp:DropDownList>--%>
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="gvList" runat="server" BackColor="White" BorderColor="#DEDFDE"
                AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                ForeColor="Black" GridLines="Vertical" Width="100%" CssClass="datagrid" OnRowCommand="gvList_RowCommand1">
                <RowStyle BackColor="#F7F7DE" />
                <FooterStyle BackColor="#CCCC99" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %></ItemTemplate>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="姓名">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>    
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="lblValue" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="40%" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="修改">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnModify" runat="server" CommandArgument='<%# Eval("ItemParamId") %>'
                                Text="修改" CommandName="myModify" CausesValidation="False" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="刪除">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("ItemParamId") %>'
                                CommandName="myDel" CausesValidation="False" Text="刪除" OnClientClick="return confirm('確認刪除此Email?')" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div align="left">
                        無資料。</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </fieldset>
    </asp:Panel>
</asp:Content>

