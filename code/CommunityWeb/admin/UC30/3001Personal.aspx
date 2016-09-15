<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdmMasterPage.master" AutoEventWireup="true"
    CodeFile="3001Personal.aspx.cs" Inherits="admin_UC30_3001Personal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            height: 22px;
        }
        .style2
        {
            background: #ffffff no-repeat left center;
            color: #808080;
            font-weight: bold;
            font-family: "Lucida Grande" ,Verdana,sans-serif;
            font-size: 12px;
            text-decoration: none;
            -moz-border-radius-bottomleft: 4px;
            -moz-border-radius-bottomright: 4px;
            -moz-border-radius-topleft: 4px;
            -moz-border-radius-topright: 4px;
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <fieldset class="fieldset">
                <h1>
                    變更密碼
                </h1>
                <table width="100%" class="FormTable">
                    <tr>
                        <td width="15%">
                            舊密碼
                        </td>
                        <td class="GrayLabel">
                            <asp:TextBox ID="txtOldPassword" CssClass="inputbox" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            新密碼
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtNewPassword" runat="server" CssClass="inputbox" TextMode="Password"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="請輸入英文或數字(至少４位)"
                                ValidationExpression="\w*.{4,}" ControlToValidate="txtNewPassword" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            確認新密碼
                        </td>
                        <td class="GrayLabel">
                            <asp:TextBox ID="txtNewPasswordConfirm" runat="server" CssClass="inputbox" TextMode="Password"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="與新密碼不相符"
                                Display="Dynamic" ControlToValidate="txtNewPasswordConfirm" Operator="Equal"
                                ControlToCompare="txtNewPassword"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnChangePassword" runat="server" Text="變更密碼" OnClick="btnChangePassword_Click" /><asp:Label
                                ID="lblMsg" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
