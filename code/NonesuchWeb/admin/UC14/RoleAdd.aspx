<%@ Page Language="C#" MasterPageFile="~/admin/AdmMasterPage.master" AutoEventWireup="true"
    CodeFile="RoleAdd.aspx.cs" Inherits="adm_UC14_UC14_1_RoleAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="ImageButton1">
                <table style="width: 100%" class="FormTable">
                    <tr>
                        <td height="30px" colspan="2" class="labelHeader">
                            <div id="title" align="center">
                                �s�շs�W</div>
                            <hr border="1" />
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="labelHeader" height="30px" align="right">
                            �s�զW�١G
                        </td>
                        <td height="30px">
                            <asp:TextBox ID="txtRoleName" runat="server" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRoleName"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" height="30px" align="center">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/admin/images/inner/button_insert.gif"
                                OnClick="ImageButton1_Click" Style="width: 45px; height: 20px;" />
                            &nbsp;&nbsp;
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/admin/images/inner/button_reset.gif"
                                OnClick="ImageButton2_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" height="30px" align="center">
                            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                AllowPaging="False" OnRowCommand="GridView1_RowCommand" CssClass="datagrid">
                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="�s��">
                        <ItemTemplate>
                            <asp:Label ID="lblRoleName" runat="server" Text='<%# Bind("RoleName") %>'></asp:Label>
                            <asp:TextBox ID="txtRoleName" runat="server" Text='<%# Bind("RoleName") %>' Visible="false"></asp:TextBox>
                            <asp:HiddenField ID="hdnRoleId" runat="server" Value='<%# Bind("RoleId") %>' />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="���i����"
                                ControlToValidate="txtRoleName"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�ק�">
                        <ItemTemplate>
                            <asp:LinkButton ID="imgUpdate" runat="server" CausesValidation="False" CommandName="MyUpdate"
                            Text="�ק�" Visible="true" CommandArgument='<%# Bind("RoleId") %>' />
                            <asp:LinkButton ID="imgCancel" runat="server" CausesValidation="False" CommandName="MyCancel"
                             Text="����" Visible="false" CommandArgument='<%# Bind("RoleId") %>' />
                            <asp:LinkButton ID="imgUpdateSure" runat="server" CausesValidation="False" CommandName="MyUpdateSure"
                                Visible="false"  Text="�T�w" CommandArgument='<%# Bind("RoleId") %>' /></ItemTemplate>
                        <HeaderStyle Width="150px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�R��">
                        <ItemTemplate>
                            <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="MyDelete"
                                OnClientClick="return confirm('�T�w�R��?')"
                                CommandArgument='<%# Bind("RoleId") %>' Text="�R��" /></ItemTemplate>
                        <HeaderStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>          
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>