<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdmMasterPage.master" AutoEventWireup="true" CodeFile="0511.aspx.cs" Inherits="admin_UC05_0511" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="aspnetpager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlContent" runat="server">
        <fieldset class="fieldset">
            <h1>
                客戶編輯</h1>
            <table style="width: 100%" class="FormTable">   
                <tr>
                    <td width="80px">
                        申辦日期
                    </td>
                    <td width="860px">
                        <asp:TextBox ID="txtApplyDate2" runat="server" Width="100px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender7" runat="server" CssClass="MyCalendar"
                            PopupPosition="BottomLeft" Format="yyyy/MM/dd" PopupButtonID="calendar10" TargetControlID="txtApplyDate2" />
                        <asp:Image ID="calendar10" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtApplyDate2"
                            ErrorMessage="請輸入申辦日期" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtApplyDate2" Display="Dynamic" ValidationGroup="Save" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                    </td>
                </tr>                            
                <tr>
                    <td width="80px">
                        開通日期
                    </td>
                    <td width="860px">
                        <asp:TextBox ID="txtApplyDate" runat="server" Width="100px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                            PopupPosition="BottomLeft" Format="yyyy/MM/dd" PopupButtonID="calendar1" TargetControlID="txtApplyDate" />
                        <asp:Image ID="calendar1" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtApplyDate"
                            ErrorMessage="請輸入開通日期" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtApplyDate" Display="Dynamic" ValidationGroup="Save" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        客戶大名
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Width="100px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="請輸入客戶大名"
                            ControlToValidate="txtName" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        身分證字號
                    </td>
                    <td>
                        <asp:TextBox ID="txtPID" runat="server" Width="100px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="請輸入身分證字號"
                            ControlToValidate="txtPID" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        聯絡電話
                    </td>
                    <td>
                        <asp:TextBox ID="txtPhone" runat="server" Width="150px"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="請輸入聯絡電話"
                            ControlToValidate="txtPhone" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>                
                <tr>
                    <td width="40px">
                        客戶生日
                    </td>
                    <td width="900px">
                        <asp:DropDownList ID="ddlBirthDayYear" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlBirthDayMonth" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlBirthDayDay" runat="server">
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="txtBirthday" runat="server" Width="100px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender7" runat="server" CssClass="MyCalendar"
                            PopupPosition="BottomLeft" Format="yyyy/MM/dd" PopupButtonID="calendar3" TargetControlID="txtBirthday" />
                        <asp:Image ID="calendar3" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtBirthday" Display="Dynamic" ValidationGroup="Save" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        上線盤商
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOnlineWholesalers" runat="server">
                            <asp:ListItem Text="請選擇" Value=""></asp:ListItem>
                            <asp:ListItem Text="特約" Value="特約"></asp:ListItem>
                            <asp:ListItem Text="皇家" Value="皇家"></asp:ListItem>
                            <asp:ListItem Text="62屋" Value="62屋"></asp:ListItem>
                            <asp:ListItem Text="立穎" Value="立穎"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Sim卡卡號
                    </td>
                    <td>
                        <asp:TextBox ID="txtSimNo" runat="server" Width="150px"></asp:TextBox>
                    </td>
                </tr> 
                <tr>
                    <td>
                        申辦專案
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProject1" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlProject1_SelectedIndexChanged">
                            <asp:ListItem Text="請選擇申辦類型" Value=""></asp:ListItem>
                            <asp:ListItem Text="攜碼" Value="攜碼"></asp:ListItem>
                            <asp:ListItem Text="新辦" Value="新辦"></asp:ListItem>
                            <asp:ListItem Text="續約" Value="續約"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlProject2" runat="server">
                            <asp:ListItem Text="請選擇原電信業者" Value=""></asp:ListItem>
                            <asp:ListItem Text="太電" Value="太電"></asp:ListItem>
                            <asp:ListItem Text="遠傳" Value="遠傳"></asp:ListItem>
                            <asp:ListItem Text="中華" Value="中華"></asp:ListItem>
                            <asp:ListItem Text="亞太" Value="亞太"></asp:ListItem>
                            <asp:ListItem Text="星星" Value="星星"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlProject3" runat="server">
                            <asp:ListItem Text="請選擇新電信業者" Value=""></asp:ListItem>
                            <asp:ListItem Text="太電" Value="太電"></asp:ListItem>
                            <asp:ListItem Text="遠傳" Value="遠傳"></asp:ListItem>
                            <asp:ListItem Text="中華" Value="中華"></asp:ListItem>
                            <asp:ListItem Text="亞太" Value="亞太"></asp:ListItem>
                            <asp:ListItem Text="星星" Value="星星"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtProject" runat="server" Width="250px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="請輸入申辦專案"
                            ControlToValidate="txtProject" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        搭配手機
                    </td>
                    <td>
                        <asp:TextBox ID="txtProduct" runat="server" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="請輸入搭配手機"
                            ControlToValidate="txtProduct" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        手機序號
                    </td>
                    <td>
                        <asp:TextBox ID="txtPhoneSer" runat="server" Width="300px"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="請輸入手機序號"
                            ControlToValidate="txtPhoneSer" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        保固商
                    </td>
                    <td>
                        <asp:TextBox ID="txtWarrantySuppliers" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        手機盤商
                    </td>
                    <td>
                        <asp:TextBox ID="txtMobileWholesalers" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        申辦號碼
                    </td>
                    <td>
                        <asp:TextBox ID="txtMobile" runat="server" Width="150px"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="請輸入申辦號碼"
                            ControlToValidate="txtMobile" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        手機進價
                    </td>
                    <td>
                        <asp:TextBox ID="txtPhonePrice" runat="server" Width="100px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="請輸入手機進價"
                                    ControlToValidate="txtPhonePrice" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="手機進價請輸入0以上的整數"
                            ControlToValidate="txtPhonePrice" MaximumValue="1000000" MinimumValue="0" Type="Integer"
                            Display="Dynamic" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        銷售金額
                    </td>
                    <td>
                        <asp:TextBox ID="txtPhoneSellPrice" runat="server" Width="100px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfSellPrice" runat="server" ErrorMessage="請輸入銷售金額"
                                    ControlToValidate="txtPhoneSellPrice" Display="Dynamic" ValidationGroup="Save" Visible="false"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="銷售金額請輸入0以上的整數"
                            ControlToValidate="txtPhoneSellPrice" MaximumValue="1000000" MinimumValue="0" Type="Integer"
                            Display="Dynamic" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        門號佣金
                    </td>
                    <td>
                        <asp:TextBox ID="txtCommission" runat="server" Width="100px"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="請輸入門號佣金"
                                    ControlToValidate="txtCommission" Display="Dynamic" ValidationGroup="Save" Visible="false"></asp:RequiredFieldValidator>--%>
                        <asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="門號佣金請輸入0以上的整數"
                            ControlToValidate="txtCommission" MaximumValue="1000000" MinimumValue="0" Type="Integer"
                            Display="Dynamic" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        佣金是否核發
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGetCommission" runat="server">
                            <asp:ListItem Text="是" Value="是"></asp:ListItem>
                            <asp:ListItem Text="否" Value="否"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        違約金
                    </td>
                    <td>
                        <asp:TextBox ID="txtBreakMoney" runat="server" Width="100px"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="請輸入違約金"
                                    ControlToValidate="txtBreakMoney" Display="Dynamic" ValidationGroup="Save" Visible="false"></asp:RequiredFieldValidator>--%>
                        <asp:RangeValidator ID="RangeValidator4" runat="server" ErrorMessage="違約金請輸入0以上的整數"
                            ControlToValidate="txtBreakMoney" MaximumValue="1000000" MinimumValue="0" Type="Integer"
                            Display="Dynamic" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        綁約月數
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlContractMonths" runat="server" AutoPostBack="true" 
                            onselectedindexchanged="ddlContractMonths_SelectedIndexChanged">                            
                            <asp:ListItem Text="請選擇" Value=""></asp:ListItem>
                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                            <asp:ListItem Text="24" Value="24"></asp:ListItem>
                            <asp:ListItem Text="30" Value="30"></asp:ListItem>
                            <asp:ListItem Text="36" Value="36"></asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="txtContractMonths" runat="server" Width="100px"></asp:TextBox>--%>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="請輸入補償金"
                                    ControlToValidate="txtContractMonths" Display="Dynamic" ValidationGroup="Save" Visible="false"></asp:RequiredFieldValidator>--%>
                        <%--<asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="綁約月數請輸入0以上的整數"
                            ControlToValidate="txtContractMonths" MaximumValue="1000000" MinimumValue="0" Type="Integer"
                            Display="Dynamic" ValidationGroup="Save"></asp:RangeValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        門號到期日
                    </td>
                    <td>
                        <asp:TextBox ID="txtDueDate" runat="server" Width="100px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
                            PopupPosition="BottomLeft" Format="yyyy/MM/dd" PopupButtonID="calendar2" TargetControlID="txtDueDate" />
                        <asp:Image ID="calendar2" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />
                        <asp:RequiredFieldValidator ID="rfClodeDate" runat="server" ControlToValidate="txtDueDate"
                            ErrorMessage="請輸入門號到期日" Display="Dynamic" Visible="false" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtDueDate" Display="Dynamic" ValidationGroup="Save" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        備註
                    </td>
                    <td>
                        <asp:TextBox ID="txtNote" runat="server" Width="400px" Height="200px" TextMode="MultiLine"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="請輸入備註"
                            ControlToValidate="txtNote" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>                                
                <tr>
                    <td>
                        銷售員
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSales" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfCustomField2" runat="server" ErrorMessage="請選擇銷售員"
                            ControlToValidate="ddlSales" ValidationGroup="Save" Visible="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        店家
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStore" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="請選擇店家"
                            ControlToValidate="ddlStore" ValidationGroup="Save" Visible="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>                 
                <%--<tr>
                    <td valign="top">
                        圖片
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td  >
                                    <asp:FileUpload ID="FileUpload1" runat="server" accept="png|jpg" class="multi" maxlength="1" CssClass="order02" />
                                </td>
                                <td>
                                    <asp:Button ID="btnUpliad" runat="server" Text="確定上傳" 
                                        onclick="btnUpliad_Click" CssClass="order02" CausesValidation="false" />
                                </td>
                            </tr>
                            <tr>
                                
                                <td>
                                    <asp:Literal ID="ltlImg" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>--%>
                <%--<tr>
                    <td valign="top">
                        內容
                    </td>
                    <td>
                        <uc1:CKEditor ID="ckeContent" runat="server" />
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="2" height="30px" align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="確定新增" ValidationGroup="Save" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="修改" ValidationGroup="Save" OnClick="btnEdit_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="刪除" OnClick="btnDelete_Click" OnClientClick="return confirm('確認刪除?')" />
                        <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" CausesValidation="false" />
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" EnableViewState="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlGv" runat="server" DefaultButton="btnSearch">
        <fieldset class="fieldset">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td align="left">
                    <br />
                        關鍵字:
                        <asp:TextBox ID="txtSearchKeyword" runat="server"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                        <br />
                        生日:
                        <asp:DropDownList ID="ddlSearchBirthDay" runat="server">
                        </asp:DropDownList>
                        <br />
                        店家:
                        <asp:DropDownList ID="ddlSearchStore" runat="server">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;
                        佣金是否核發:
                        <asp:DropDownList ID="ddlSearchGetCommission" runat="server">
                            <asp:ListItem Text="全部" Value=""></asp:ListItem>
                            <asp:ListItem Text="是" Value="是"></asp:ListItem>
                            <asp:ListItem Text="否" Value="否"></asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        申辦日期:
                        <asp:TextBox ID="txtSearchApplyDate2Start" runat="server"></asp:TextBox>  
                        <cc1:CalendarExtender ID="CalendarExtender8" runat="server" CssClass="MyCalendar"
                            PopupPosition="BottomLeft" Format="yyyy/MM/dd" PopupButtonID="searchCalendar10" TargetControlID="txtSearchApplyDate2Start" />
                        <asp:Image ID="searchCalendar10" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />                        
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtSearchApplyDate2Start" Display="Dynamic" ValidationGroup="Search" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                        ~                        
                        <asp:TextBox ID="txtSearchApplyDate2End" runat="server"></asp:TextBox> 
                        <cc1:CalendarExtender ID="CalendarExtender9" runat="server" CssClass="MyCalendar"
                            PopupPosition="BottomLeft" Format="yyyy/MM/dd" PopupButtonID="searchCalendar11" TargetControlID="txtSearchApplyDate2End" />
                        <asp:Image ID="searchCalendar11" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />                        
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtSearchApplyDate2End" Display="Dynamic" ValidationGroup="Search" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                            <br />
                        開通日期:
                        <asp:TextBox ID="txtSearchApplyDateStart" runat="server"></asp:TextBox>  
                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="MyCalendar"
                            PopupPosition="BottomLeft" Format="yyyy/MM/dd" PopupButtonID="searchCalendar1" TargetControlID="txtSearchApplyDateStart" />
                        <asp:Image ID="searchCalendar1" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />                        
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtSearchApplyDateStart" Display="Dynamic" ValidationGroup="Search" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                        ~                        
                        <asp:TextBox ID="txtSearchApplyDateEnd" runat="server"></asp:TextBox> 
                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="MyCalendar"
                            PopupPosition="BottomLeft" Format="yyyy/MM/dd" PopupButtonID="searchCalendar2" TargetControlID="txtSearchApplyDateEnd" />
                        <asp:Image ID="searchCalendar2" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />                        
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtSearchApplyDateEnd" Display="Dynamic" ValidationGroup="Search" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                            <br />
                        門號到期日:
                        <asp:TextBox ID="txtSearchDueDateStart" runat="server"></asp:TextBox>  
                        <cc1:CalendarExtender ID="CalendarExtender5" runat="server" CssClass="MyCalendar"
                            PopupPosition="BottomLeft" Format="yyyy/MM/dd" PopupButtonID="searchCalendar3" TargetControlID="txtSearchDueDateStart" />
                        <asp:Image ID="searchCalendar3" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />                        
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtSearchDueDateStart" Display="Dynamic" ValidationGroup="Search" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                        ~                       
                        <asp:TextBox ID="txtSearchDueDateEnd" runat="server"></asp:TextBox> 
                        <cc1:CalendarExtender ID="CalendarExtender6" runat="server" CssClass="MyCalendar"
                            PopupPosition="BottomLeft" Format="yyyy/MM/dd" PopupButtonID="searchCalendar4" TargetControlID="txtSearchDueDateEnd" />
                        <asp:Image ID="searchCalendar4" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />                        
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtSearchDueDateEnd" Display="Dynamic" ValidationGroup="Search" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                        <br /><br />
                        <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" ValidationGroup="Search" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSearchExport" runat="server" Text="查詢結果匯出" OnClick="btnSearchExport_Click" ValidationGroup="Search" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnShowAdd" runat="server" Text="新增客戶" OnClick="btnShowAdd_Click" />
                        <br />
                        <br />
                        <asp:Label ID="lblTotalCount" runat="server" Text="" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="right" class="labelText">                        
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="gvList" runat="server" BackColor="White" BorderColor="#DEDFDE"
                AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                ForeColor="Black" GridLines="Vertical" Width="100%" CssClass="datagrid" OnRowCommand="gvList_RowCommand1"
                OnRowDataBound="gvList_RowDataBound">
                <RowStyle BackColor="#F7F7DE" />
                <FooterStyle BackColor="#CCCC99" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
                <Columns> 
                    <asp:TemplateField HeaderText="申辦日">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyDate2" runat="server" Text='<%# Eval("ApplyDate2","{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="開通日">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyDate" runat="server" Text='<%# Eval("ApplyDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="客戶大名">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="身分證">
                        <ItemTemplate>
                            <asp:Label ID="lblPID" runat="server" Text='<%# Bind("PID") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="100" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="聯絡電話">
                        <ItemTemplate>
                            <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("Phone") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="申辦專案">
                        <ItemTemplate>
                            <asp:Label ID="lblProject" runat="server" Text='<%# Bind("Project") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="300" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="搭配手機">
                        <ItemTemplate>
                            <asp:Label ID="lblProduct" runat="server" Text='<%# Bind("Product") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="300" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="保固商">
                        <ItemTemplate>
                            <asp:Label ID="lblWarrantySuppliers" runat="server" Text='<%# Bind("WarrantySuppliers") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="手機盤商">
                        <ItemTemplate>
                            <asp:Label ID="lblMobileWholesalers" runat="server" Text='<%# Bind("MobileWholesalers") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                                                              
                    <asp:TemplateField HeaderText="手機進價">
                        <ItemTemplate>
                            <asp:Label ID="lblPhonePrice" runat="server" Text='<%# Bind("PhonePrice") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="銷售金額">
                        <ItemTemplate>
                            <asp:Label ID="lblPhoneSellPrice" runat="server" Text='<%# Bind("PhoneSellPrice") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="門號到期日">
                        <ItemTemplate>
                            <asp:Label ID="lblDueDate" runat="server" Text='<%# Eval("DueDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="銷售員">
                        <ItemTemplate>
                            <asp:Label ID="lblSales" runat="server" Text='<%# Bind("Sales") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="100" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="店家">
                        <ItemTemplate>
                            <asp:Label ID="lblStore" runat="server" Text='<%# Bind("Store") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="50" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="查看">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnModify" runat="server" CommandArgument='<%# Eval("MemberId") %>'
                                Text="查看" CommandName="myModify" CausesValidation="False" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="100" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="刪除">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("PostId") %>'
                                CommandName="myDel" CausesValidation="False" Text="刪除" OnClientClick="return confirm('確認刪除此庫存?')" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>--%>
                </Columns>
                <EmptyDataTemplate>
                    <div align="left">
                        無資料。</div>
                </EmptyDataTemplate>
            </asp:GridView>
            <Webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="PagerStyle" HorizontalAlign="center"
                    OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxType="TextBox" PageSize="20"
                    ShowPageIndexBox="Never" />
        </fieldset>
    </asp:Panel>
</asp:Content>

