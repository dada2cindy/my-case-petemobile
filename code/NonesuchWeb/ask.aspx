<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ask.aspx.cs" Inherits="ask" %>

<%@ Register Assembly="aspnetpager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="LeftBtnDown">
    <div id="LeftBtn_Table">
        <h1>線上諮詢</h1>
</div>
      <div id="AllInOB">
        <div id="AllTitle">
          線上諮詢
            <p>線上諮詢</p>
        </div>
        <div class="Contact_HTML">
          感謝您的來信，我們將於最短時間內回覆，以下的欄位請完整填寫<br>
        讓我們能更了解您的問題</div>
        <div class="Contact_Order">
        <asp:Panel ID="PanelUI" runat="server"> 
          <table width="100%" border="0" cellpadding="0" cellspacing="16">
            <tr>
              <td width="18%">姓名</td>
              <td colspan="2">
                <asp:TextBox ID="txtCreateName" runat="server" CssClass="Order01" size="40"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="請輸入姓名"
                  ControlToValidate="txtCreateName" Display="None" ValidationGroup="Save"></asp:RequiredFieldValidator>
              </td>
            </tr>
            <tr>
              <td>聯絡電話</td>
              <td colspan="2">
                <asp:TextBox ID="txtPhone" runat="server" CssClass="Order01" size="40"></asp:TextBox></td>
            </tr>
            <tr>
              <td>Email</td>
              <td colspan="2"><asp:TextBox ID="txtEMail" runat="server" CssClass="Order01" size="70"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="請輸入E-mail"
                  ControlToValidate="txtEmail" Display="None" ValidationGroup="Save"></asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="Save" ErrorMessage="電子信箱格式不正確"
                  Display="None" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
              </td>
            </tr>
            <tr>
              <td>預約日期</td>
              <td colspan="2"><asp:DropDownList ID="ddlMonth" runat="server" CssClass="Order01">
                      </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="請選擇月"
                  ControlToValidate="ddlMonth" Display="None" ValidationGroup="Save"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlDay" runat="server" CssClass="Order01">
                      </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="請選擇日"
                  ControlToValidate="ddlDay" Display="None" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
              <td>預約時段</td>
              <td colspan="2"><asp:RadioButtonList ID="rdoReservationPeriod" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="上午" Value="上午"></asp:ListItem>
                        <asp:ListItem Text="下午" Value="下午"></asp:ListItem>
                      </asp:RadioButtonList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="請選擇預約時段"
                  ControlToValidate="rdoReservationPeriod" Display="None" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
              <td valign="top">預約內容</td>
              <td colspan="2">
              <asp:TextBox ID="txtContent" runat="server" width="300px" rows="5" CssClass="Order02" TextMode="MultiLine"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="請輸入預約內容"
                  ControlToValidate="txtContent" Display="None" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
              <td>驗證碼</td>
              <td width="14%">
                <asp:TextBox ID="txtConfirmationCode" runat="server" CssClass="Order01" size="10"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="請輸入驗證碼"
                  ControlToValidate="txtConfirmationCode" Display="None" ValidationGroup="Save"></asp:RequiredFieldValidator>
              </td>
              <td width="68%"><img id="safecode" src="admin/captcha/Handler.ashx" width="50" height="25" /></td>
            </tr>
            <tr>
              <td colspan="3" align="right"><a href="ask.aspx"><img src="image/btn01.png" width="105" height="28"></a>　<asp:ImageButton ID="btnSend" runat="server" onclick="btnSend_Click" ImageUrl="image/btn02.png" Width="105" Height="28" ValidationGroup="Save"/></td>
            </tr>
          </table>
          </asp:Panel>
        </div>
      </div>
    </div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Save" 
        ShowMessageBox="True" ShowSummary="False"/>        
</asp:Content>

