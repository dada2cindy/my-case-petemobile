<%@ Page Language="C#" MasterPageFile="~/admin/AdmMasterPage.master" AutoEventWireup="true"
    CodeFile="RoleFuncSet.aspx.cs" Inherits="adm_UC14_UC14_4_RoleFuncSet" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnUpdate">
                <table style="width: 100%" CssClass="FormTable">
                    <tr>
                        <td height="30px" class="labelHeader">
                            <div align="center" id="title">
                                群組權限設定</div>
                            <hr border="1" />
                        </td>
                    </tr>
                    <tr>
                        <td height="30px" class="labelHeader">
                            <div align="center">
                                群組名稱：
                                <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp;&nbsp; 功能群組
                                <asp:DropDownList ID="ddlMenuFunc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMenuFunc_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp;
                            </div>
                        </td>
                    </tr>
                    <tr class="labelHeader">
                        <td height="30px" align="center">
                            <br />
                            設定權限
                            <hr border="1" />
                            <br />
                            <asp:GridView ID="gvAuth" runat="server" AutoGenerateColumns="False" Width="500px" CssClass="datagrid">
                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                <Columns >
                                    <%--<asp:BoundField DataField="No" HeaderText="編號">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>--%>
                                    <asp:BoundField HeaderText="名稱" DataField="Name" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="權限">
                                        <HeaderTemplate>
                                            <div align="center">
                                                權限
                                                <br />
                                                <asp:CheckBox ID="ckAll" runat="server" AutoPostBack="true" OnCheckedChanged="ckAll_CheckedChanged" />
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckIsAuth" runat="server" Checked='<%# Bind("IsAuth") %>' />
                                            &nbsp;<asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("Id") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                </Columns>
                            
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr class="labelHeader">
                        <td width="34%" align="center">
                            <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/admin/images/inner/button_save_1.gif"
                                OnClick="btnUpdate_Click" />
                        </td>
                    </tr>
                    <tr class="labelHeader">
                        <td width="34%" align="center" class="style1">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
            </asp:Panel>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
