<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdmMasterPage.master" AutoEventWireup="true" CodeFile="0512.aspx.cs" Inherits="admin_UC05_0512" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="aspnetpager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlContent" runat="server">
        <fieldset class="fieldset">
            <h1>
                庫存編輯</h1>
            <table style="width: 100%" class="FormTable">
                <tr>
                    <td width="40px">
                        類別
                    </td>
                    <td width="900px">
                        <asp:DropDownList ID="ddlTypeList" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlTypeList_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtCustomField1" runat="server" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="請輸入類別"
                            ControlToValidate="txtCustomField1" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>                                
                <tr>
                    <td>
                        進貨日
                    </td>
                    <td>
                        <asp:TextBox ID="txtShowDate" runat="server" Width="100px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                            PopupPosition="BottomLeft" Format="yyyy/MM/dd" PopupButtonID="calendar1" TargetControlID="txtShowDate" />
                        <asp:Image ID="calendar1" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtShowDate"
                            ErrorMessage="請輸入進貨日" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtShowDate" Display="Dynamic" ValidationGroup="Save" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        銷貨日
                    </td>
                    <td>
                        <asp:TextBox ID="txtCloseDate" runat="server" Width="100px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
                            PopupPosition="BottomLeft" Format="yyyy/MM/dd" PopupButtonID="calendar2" TargetControlID="txtCloseDate" />
                        <asp:Image ID="calendar2" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />
                        <asp:RequiredFieldValidator ID="rfCloseDate" runat="server" ControlToValidate="txtCloseDate"
                            ErrorMessage="請輸入銷貨日" Display="Dynamic" Visible="false" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtCloseDate" Display="Dynamic" ValidationGroup="Save" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        品名
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProductList" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlProductList_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtTitle" runat="server" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="請輸入品名"
                            ControlToValidate="txtTitle" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        進貨價
                    </td>
                    <td>
                        <asp:TextBox ID="txtPrice" runat="server" Width="100px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="請輸入進貨價"
                                    ControlToValidate="txtPrice" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="進貨價請輸入0以上的整數"
                            ControlToValidate="txtPrice" MaximumValue="1000000" MinimumValue="0" Type="Integer"
                            Display="Dynamic" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        售價
                    </td>
                    <td>
                        <asp:TextBox ID="txtSellPrice" runat="server" Width="100px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfSellPrice" runat="server" ErrorMessage="請輸入售價"
                                    ControlToValidate="txtSellPrice" Display="Dynamic" ValidationGroup="Save" Visible="false"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="售價請輸入0以上的整數"
                            ControlToValidate="txtSellPrice" MaximumValue="1000000" MinimumValue="0" Type="Integer"
                            Display="Dynamic" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        數量
                    </td>
                    <td>
                        <asp:TextBox ID="txtQuantity" runat="server" Width="100px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="請輸入數量"
                                    ControlToValidate="txtQuantity" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="數量請輸入大於0的整數"
                            ControlToValidate="txtQuantity" MaximumValue="1000000" MinimumValue="1" Type="Integer"
                            Display="Dynamic" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        銷售員
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCustomField2" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfCustomField2" runat="server" ErrorMessage="請選擇銷售員"
                            ControlToValidate="ddlCustomField2" ValidationGroup="Save" Visible="false"></asp:RequiredFieldValidator>
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
                        <asp:Button ID="btnSold" runat="server" Text="售出" ValidationGroup="Save" OnClick="btnSold_Click" />
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
                        類別:
                        <asp:DropDownList ID="ddlSearchCustomField1" runat="server" >
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;
                        狀態: 
                        <asp:DropDownList ID="ddlSearchType" runat="server">
                            <asp:ListItem Text="全部" Value=""></asp:ListItem>
                            <asp:ListItem Text="庫存" Value="0"></asp:ListItem>
                            <asp:ListItem Text="售出" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        進貨日:
                        <asp:TextBox ID="txtSearchShowDateStart" runat="server"></asp:TextBox>  
                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="MyCalendar"
                            PopupPosition="BottomLeft" Format="yyyy/MM/dd" PopupButtonID="searchCalendar1" TargetControlID="txtSearchShowDateStart" />
                        <asp:Image ID="searchCalendar1" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />                        
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtSearchShowDateStart" Display="Dynamic" ValidationGroup="Search" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                        ~                        
                        <asp:TextBox ID="txtSearchShowDateEnd" runat="server"></asp:TextBox> 
                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="MyCalendar"
                            PopupPosition="BottomLeft" Format="yyyy/MM/dd" PopupButtonID="searchCalendar2" TargetControlID="txtSearchShowDateEnd" />
                        <asp:Image ID="searchCalendar2" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />                        
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtSearchShowDateEnd" Display="Dynamic" ValidationGroup="Search" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                            <br />
                        銷貨日:
                        <asp:TextBox ID="txtSearchCloseDateStart" runat="server"></asp:TextBox>  
                        <cc1:CalendarExtender ID="CalendarExtender5" runat="server" CssClass="MyCalendar"
                            PopupPosition="BottomLeft" Format="yyyy/MM/dd" PopupButtonID="searchCalendar3" TargetControlID="txtSearchCloseDateStart" />
                        <asp:Image ID="searchCalendar3" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />                        
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtSearchCloseDateStart" Display="Dynamic" ValidationGroup="Search" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                        ~                       
                        <asp:TextBox ID="txtSearchCloseDateEnd" runat="server"></asp:TextBox> 
                        <cc1:CalendarExtender ID="CalendarExtender6" runat="server" CssClass="MyCalendar"
                            PopupPosition="BottomLeft" Format="yyyy/MM/dd" PopupButtonID="searchCalendar4" TargetControlID="txtSearchCloseDateEnd" />
                        <asp:Image ID="searchCalendar4" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />                        
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtSearchCloseDateEnd" Display="Dynamic" ValidationGroup="Search" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                        <br /><br />
                        <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" ValidationGroup="Search" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSearchExport" runat="server" Text="查詢結果匯出" OnClick="btnSearchExport_Click" ValidationGroup="Search" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnShowAdd" runat="server" Text="新增庫存" OnClick="btnShowAdd_Click" />
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
                    <asp:TemplateField HeaderText="品名">
                        <ItemTemplate>
                            <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                            <asp:HiddenField ID="hdnPostId" runat="server" Value='<%# Bind("PostId") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="300" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="類別">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomField1" runat="server" Text='<%# Bind("CustomField1") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="狀態">
                        <ItemTemplate>
                            <asp:Label ID="lblGetStr_Flag" runat="server" Text='<%# Bind("GetStr_Type") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="100" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="進貨日">
                        <ItemTemplate>
                            <asp:Label ID="lblShowDate" runat="server" Text='<%# Eval("ShowDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="銷貨日">
                        <ItemTemplate>
                            <asp:Label ID="lblCloseDate" runat="server" Text='<%# Eval("CloseDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="進貨價">
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="售價">
                        <ItemTemplate>
                            <asp:Label ID="lblSellPrice" runat="server" Text='<%# Bind("SellPrice") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="數量">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="在庫總數量">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalQuantity" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="銷售員">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomField2" runat="server" Text='<%# Bind("CustomField2") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="100" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="查看">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnModify" runat="server" CommandArgument='<%# Eval("PostId") %>'
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

