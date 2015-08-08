<%@ Page Language="C#" MasterPageFile="~/admin/AdmMasterPage.master" AutoEventWireup="true"
    CodeFile="UserAdd.aspx.cs" Inherits="adm_UC14_UC14_1_UserAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnAdd">
                <fieldset class="fieldset">
                    <h1>
                        系統帳號新增</h1>
                    <table style="width: 100%" class="FormTable">
                        <tr>
                            <td>
                                登入帳號
                            </td>
                            <td>
                                <asp:TextBox ID="txtId" runat="server" Width="160px"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtId"> 請輸入登入帳號
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtId"
                                    ErrorMessage="帳號長度至少三碼" ValidationExpression=".{3,}"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                帳號啟用狀態
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdbIsValidAccount" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="立即啟用" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="停用" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                中文姓名
                            </td>
                            <td>
                                <asp:TextBox ID="txtFullNameInChinese" runat="server" Width="160px"></asp:TextBox>
                            </td>
                            <td>
                                英文姓名
                            </td>
                            <td>
                                <asp:TextBox ID="txtFullNameInEnglish" runat="server" Width="160px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                手機號碼
                            </td>
                            <td>
                                <asp:TextBox ID="txtMobile" runat="server" Width="160px" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtMobile"
                                    Display="Dynamic">
                                請輸入手機號碼
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                身分證字號
                            </td>
                            <td>
                                <asp:TextBox ID="txtSSID" runat="server" Width="160px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                電子郵件
                            </td>
                            <td >
                                <asp:TextBox ID="txtEmail" runat="server" Width="450px"></asp:TextBox>
                            </td>
                            <td>
                                於業績報表顯示
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdbShowInSalesStatistics" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="是" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                聯絡地址
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtAddress" runat="server" Width="450px"></asp:TextBox>
                        </tr>
                        <tr>
                            <td colspan="4" height="30px" align="center">
                                <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/admin/images/inner/button_insert.gif"
                                    OnClick="btnAdd_Click" Height="20px" />
                                <asp:ImageButton ID="btnUpdate" runat="server" Height="20px" ImageUrl="~/admin/images/inner/button_save.gif"
                                    OnClick="btnUpdate_Click" />
                                &nbsp;
                                <asp:ImageButton ID="btnReset" runat="server" ImageUrl="~/admin/images/inner/button_reset.gif"
                                    OnClick="btnReset_Click" Height="20px" CausesValidation="false" />
                                <asp:HiddenField ID="hdnVersion" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" height="30px" align="center">
                                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" EnableViewState="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                    AllowPaging="True" DataKeyNames="UserId" OnRowDeleting="GridView1_RowDeleting"
                    OnRowCommand="GridView1_RowCommand" AllowSorting="True" CssClass="datagrid">
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <Columns>
                        <asp:BoundField DataField="UserId" HeaderText="帳號" ReadOnly="True" ItemStyle-HorizontalAlign="left">
                            <HeaderStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FullNameInChinese" HeaderText="中文姓名" ItemStyle-HorizontalAlign="left">
                        </asp:BoundField>
                        <asp:BoundField DataField="FullNameInEnglish" HeaderText="英文姓名" ItemStyle-HorizontalAlign="left">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="編輯">
                            <ItemTemplate>
                                <asp:LinkButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="MyEdit"
                                    Text="編輯" CommandArgument='<%# Eval("UserId") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="MyDelete"
                                    OnClientClick="return confirm('確定刪除?')" Text="刪除" CommandArgument='<%# Eval("UserId") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="設定權限" ShowHeader="False" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="ImageButton2" runat="server" Text="設定權限" CausesValidation="false"
                                    CommandName="MySelect" CommandArgument='<%# Eval("UserId") %>' />
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
