<%@ Page Language="C#" MasterPageFile="~/admin/AdmMasterPage.master" AutoEventWireup="true"
    CodeFile="UserRoleSet.aspx.cs" Inherits="adm_UC14_UC14_3_UserRoleSet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 11%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="Button3">
                <table style=" width: 100%" class="FormTable">
                    <tr>
                        <td height="30px" colspan="3" class="labelHeader">
                            <div id="title" align="center">
                                帳號群組設定</div>
                            <hr border="1" />
                        </td>
                    </tr>
                    <tr>
                        <td height="30px" colspan="3" class="labelHeader">
                            <div align="center">
                                使用者名稱：
                                <asp:DropDownList ID="ddlUser" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllUsers"
                                    TypeName="com.pro2e.console.service.auth.AuthService"></asp:ObjectDataSource>
                            </div>
                        </td>
                    </tr>
                    <tr class="labelHeader">
                        <td width="40%" height="30px" align="center">
                            可供設定群組
                        </td>
                        <td width="20%" align="center" class="style1">
                            設定操作
                        </td>
                        <td width="40%" align="center">
                            目前群組
                        </td>
                    </tr>
                    <tr class="labelHeader">
                        <td width="40%" height="30px" align="center">
                            <asp:ListBox ID="lblxToBeRole" runat="server" Height="98px" Width="246px" OnSelectedIndexChanged="lblxToBeRole_SelectedIndexChanged"
                                SelectionMode="Multiple"></asp:ListBox>
                        </td>
                        <td align="center" class="style1">
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/admin/images/btn_rightArrow.jpg"
                                OnClick="btnAdd_Click" />
                            <br />
                            <br />
                            <asp:ImageButton ID="btnRemove" runat="server" ImageUrl="~/admin/images/btn_leftArrow.jpg"
                                OnClick="btnRemove_Click" />
                        </td>
                        <td width="40%" align="center">
                            <asp:ListBox ID="lbxHadRole" runat="server" Height="98px" Width="246px" OnSelectedIndexChanged="lbxHadRole_SelectedIndexChanged"
                                SelectionMode="Multiple"></asp:ListBox>
                        </td>
                    </tr>
                    <tr class="labelHeader">
                        <td height="30px" colspan="3" align="center">
                            <hr border="1" />
                        </td>
                    </tr>
                    <tr class="labelHeader">
                        <td width="40%" height="30px" align="center">
                            &nbsp;
                        </td>
                        <td align="center" class="style1">
                            <asp:ImageButton ID="Button3" runat="server" ImageUrl="~/admin/images/inner/button_save_1.gif"
                                OnClick="Button3_Click" />
                        </td>
                        <td width="40%" align="center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr class="labelHeader">
                        <td align="center" colspan="3">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
