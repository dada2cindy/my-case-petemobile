<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdmMasterPage.master" AutoEventWireup="true" CodeFile="0611.aspx.cs" Inherits="admin_UC06_0611" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="aspnetpager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlContent" runat="server">
        <fieldset class="fieldset">
            <h1>
                檔案編輯</h1>
            <table style="width: 100%" class="FormTable">
                <tr>
                    <td width="80px">
                        類型
                    </td>
                    <td width="900px">
                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlType_SelectedIndexChanged">
                            <asp:ListItem Text="請選擇類型" Value=""></asp:ListItem>
                            <asp:ListItem Text="門號" Value="1"></asp:ListItem>
                            <asp:ListItem Text="進貨" Value="2"></asp:ListItem>
                            <asp:ListItem Text="切結書" Value="3"></asp:ListItem>
                        </asp:DropDownList>                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="請選擇類型"
                            ControlToValidate="ddlType" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>                                
                <tr>
                    <td>
                        日期
                    </td>
                    <td>
                        <asp:TextBox ID="txtShowDate" runat="server" Width="100px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                            PopupPosition="BottomLeft" Format="yyyy/MM/dd" PopupButtonID="calendar1" TargetControlID="txtShowDate" />
                        <asp:Image ID="calendar1" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtShowDate"
                            ErrorMessage="請輸入日期" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtShowDate" Display="Dynamic" ValidationGroup="Save" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr runat="server" id="trContent2" visible="false">
                    <td>
                        門號
                    </td>
                    <td>
                        <asp:TextBox ID="txtContent2" runat="server" Width="150px" autocomplete="on"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="請輸入門號"
                            ControlToValidate="txtContent2" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>  
                <tr runat="server" id="trContent1" visible="false">
                    <td>
                        身分證字號
                    </td>
                    <td>
                        <asp:TextBox ID="txtContent1" runat="server" Width="150px" autocomplete="on"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="請輸入身分證字號"
                            ControlToValidate="txtContent1" ValidationGroup="Save"></asp:RequiredFieldValidator>
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
                        類型: 
                        <asp:DropDownList ID="ddlSearchType" runat="server">
                            <asp:ListItem Text="全部" Value=""></asp:ListItem>
                            <asp:ListItem Text="門號" Value="1"></asp:ListItem>
                            <asp:ListItem Text="進貨" Value="2"></asp:ListItem>
                            <asp:ListItem Text="切結書" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        日期:
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
                        <br /><br />
                        <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" ValidationGroup="Search" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSearchExport" runat="server" Text="查詢結果匯出" OnClick="btnSearchExport_Click" ValidationGroup="Search" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnShowAdd" runat="server" Text="新增檔案" OnClick="btnShowAdd_Click" />
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
                    <asp:TemplateField HeaderText="類型">
                        <ItemTemplate>
                            <asp:Label ID="lblGetStr_Type" runat="server" Text='<%# Bind("GetStr_Type") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="100" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="日期">
                        <ItemTemplate>
                            <asp:Label ID="lblShowDate" runat="server" Text='<%# Eval("ShowDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="100" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="編號">
                        <ItemTemplate>
                            <asp:Label ID="lblFileNo" runat="server" Text='<%# Bind("FileNo") %>'></asp:Label>                            
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                  
                    <asp:TemplateField HeaderText="門號">
                        <ItemTemplate>
                            <asp:Label ID="lblContent2" runat="server" Text='<%# Bind("Content2") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="100" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="身分證字號">
                        <ItemTemplate>
                            <asp:Label ID="lblContent1" runat="server" Text='<%# Bind("Content1") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="100" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="上傳者">
                        <ItemTemplate>
                            <asp:Label ID="lblUpdatedBy" runat="server" Text='<%# Bind("UpdatedBy") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="100" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="查看">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnModify" runat="server" CommandArgument='<%# Eval("FileId") %>'
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

