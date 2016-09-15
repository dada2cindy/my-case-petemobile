<%@ Page Language="C#" MasterPageFile="~/admin/AdmMasterPage.master" AutoEventWireup="true"
    CodeFile="QueryLog.aspx.cs" Inherits="adm_UC14_UC14_5_QueryLog" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="aspnetpager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnQuery">
                <table style="width: 100%" class="FormTable" align="center">
                    <tr>
                        <td height="30px" colspan="2" class="labelHeader">
                            <div align="center" id="title">
                                使用記錄查詢</div>
                            <hr border="1" />
                        </td>
                    </tr>
                    <tr>
                        <td height="30px" widht="120px">
                            項目：
                        </td>
                        <td height="30px">
                            <asp:DropDownList ID="ddlClassify" runat="server" EnableViewState="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            時間：
                        </td>
                        <td height="30px" style="text-align: left">
                            <asp:UpdatePanel ID="UpdatePanelData" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDateFrom" runat="server" Width="65px" MaxLength="10" ValidationGroup="EditDateGroup" />
                                                <asp:Image ID="butDay1" runat="server" ImageUrl="~/admin/images/calendar.gif" AlternateText="開啟日曆，點選日期"
                                                    CssClass="CalnedarImg" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom"
                                                    PopupButtonID="butDay1" CssClass="MyCalendar" Format="yyyy/MM/dd" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDateFrom"
                                                    Display="Dynamic" ErrorMessage="日期格式輸入不正確" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                                                <asp:Label ID="Label5" runat="server">~</asp:Label>
                                                <asp:TextBox ID="txtDateTo" runat="server" Width="65px" MaxLength="10" ValidationGroup="EditDateGroup" />
                                                <asp:Image ID="butDay2" runat="server" ImageUrl="~/admin/images/calendar.gif" AlternateText="開啟日曆，點選日期"
                                                    CssClass="CalnedarImg" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo"
                                                    PopupButtonID="butDay2" CssClass="MyCalendar" Format="yyyy/MM/dd" />
                                                <asp:RegularExpressionValidator ID="rev" runat="server" ControlToValidate="txtDateTo"
                                                    Display="Dynamic" ErrorMessage="日期格式輸入不正確" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                                                <asp:ValidationSummary runat="Server" ValidationGroup="EditDateGroup" ID="validationSummary"
                                                    ShowSummary="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" height="30px" align="center">
                            <asp:ImageButton ID="btnQuery" runat="server" ImageUrl="~/admin/images/inner/button_search.gif"
                                OnClick="btnQuery_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <div align="center">
                <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" Width="100%"
                    CssClass="datagrid" OnRowDataBound="gvDetail_RowDataBound">
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <Columns>
                        <asp:BoundField DataField="UpdateDate" HeaderText="日期" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemStyle Width="30%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="項目">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Fucntion") %>'></asp:Label>
                                <asp:Label ID="Label3" runat="server" Text="→"></asp:Label>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("SubFucntion") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemStyle Width="20%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Action" HeaderText="動作" ItemStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UpdateId" HeaderText="人員" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle Width="50px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IpAddress" HeaderText="登入IP" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle Width="50px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Note" HeaderText="備註" ItemStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div align="center" class="labelMsg">
                            找不到符合資料!</div>
                    </EmptyDataTemplate>
                </asp:GridView>
                <Webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="PagerStyle" HorizontalAlign="center"
                    OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxType="TextBox" PageSize="20"
                    ShowPageIndexBox="Never" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
