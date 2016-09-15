<%@ Page Language="C#" MasterPageFile="~/admin/AdmMasterPage.master" AutoEventWireup="true" CodeFile="0431.aspx.cs" Inherits="admin_UC04_0431" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset class="fieldset">
                <asp:Panel ID="pnlContent" runat="server">
                    <h1>
                        品牌編輯</h1>
                    <table style="width: 100%" class="FormTable">
                        <tr>
                            <td>
                                品牌名稱
                            </td>
                            <td>
                                <asp:TextBox ID="txtNodeName" runat="server" Width="300px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="請輸入品牌名稱"
                                    ControlToValidate="txtNodeName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">圖片
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="FileUpload1" runat="server" accept="png|jpg" class="multi" maxlength="1" CssClass="order02" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnUpliad" runat="server" Text="確定上傳"
                                                OnClick="btnUpliad_Click" CssClass="order02" CausesValidation="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Literal ID="ltlImg" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="30px" align="center">
                                <asp:Button ID="btnAdd" runat="server" Text="新增品牌" onclick="btnAdd_Click"  />
                                <asp:Button ID="btnSave" runat="server" Text="儲存修改" onclick="btnSave_Click"  />
                                <asp:Button ID="btnCancel" runat="server" Text="取消" onclick="btnCancel_Click" CausesValidation="false" />
                                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" EnableViewState="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </fieldset>
            <br />
            <asp:Panel ID="pnlGv" runat="server">
                <fieldset class="fieldset">
                   
                    <asp:GridView ID="gvList" runat="server" BackColor="White" BorderColor="#DEDFDE"
                        AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                        ForeColor="Black" GridLines="Vertical" Width="100%" CssClass="datagrid" 
                        onrowcommand="gvList_RowCommand1">
                        <RowStyle BackColor="#F7F7DE" />
                        <FooterStyle BackColor="#CCCC99" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="品牌名稱">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="65%" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="修改">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnModify" runat="server" CommandArgument='<%# Eval("NodeId") %>'
                                        Text="修改" CommandName="myModify" CausesValidation="False" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="刪除">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("NodeId") %>'
                                        CommandName="myDel" CausesValidation="False" Text="刪除" OnClientClick="return confirm('確認刪除此品牌?')" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div align="left">
                                請新增品牌資料。</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </fieldset></asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>