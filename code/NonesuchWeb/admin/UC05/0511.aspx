<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdmMasterPage.master" AutoEventWireup="true" CodeFile="0511.aspx.cs" Inherits="admin_UC05_0511" ValidateRequest="false" %>

<%@ Register Src="../UserControl/CKEditor.ascx" TagName="CKEditor" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="aspnetpager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlContent" runat="server">
        <fieldset class="fieldset">
            <h1>
                臻美快訊編輯</h1>
            <table style="width: 100%" class="FormTable">                
                <tr>
                    <td>
                        文章標題
                    </td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" Width="700px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="請輸入名稱"
                            ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        上架
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFlag" runat="server">
                            <asp:ListItem Text="請選擇" Value=""></asp:ListItem>
                            <asp:ListItem Text="是" Value="1"></asp:ListItem>
                            <asp:ListItem Text="否" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="請選擇是否上架"
                            ControlToValidate="ddlFlag"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        日期
                    </td>
                    <td>
                        <asp:TextBox ID="txtShowDate" runat="server" Width="100px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                            PopupPosition="TopLeft" Format="yyyy/MM/dd" PopupButtonID="butDay1" TargetControlID="txtShowDate" />
                        <asp:Image ID="butDay1" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtShowDate"
                            ErrorMessage="請輸入日期" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtShowDate" Display="Dynamic" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        排序編號
                    </td>
                    <td>
                        <asp:TextBox ID="txtSortNo" runat="server" Width="100px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="請輸入排序編號(數字)"
                                    ControlToValidate="txtSortNo" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="請輸入排序編號(數字)"
                            ControlToValidate="txtSortNo" MaximumValue="1000000" MinimumValue="0" Type="Integer"
                            Display="Dynamic"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
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
                </tr>
                <tr>
                    <td valign="top">
                        內容
                    </td>
                    <td>
                        <uc1:CKEditor ID="ckeContent" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" height="30px" align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="確定新增" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnSave" runat="server" Text="儲存修改" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" CausesValidation="false" />
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" EnableViewState="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlGv" runat="server">
        <fieldset class="fieldset">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td align="left">
                        <asp:Button ID="btnShowAdd" runat="server" Text="新增文章" OnClick="btnShowAdd_Click" />
                    </td>
                    <td align="right" class="labelText">
                      <%--  分類搜尋:<asp:DropDownList ID="ddlSelect" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelect_SelectedIndexChanged">
                        </asp:DropDownList>--%>
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
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %></ItemTemplate>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="文章標題">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="45%" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="排序">
                        <ItemTemplate>
                            <asp:Label ID="lblSort" runat="server" Text='<%# Bind("SortNo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="上架">
                        <ItemTemplate>
                            <asp:Label ID="lblGetStr_Flag" runat="server" Text='<%# Bind("GetStr_Flag") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="日期">
                        <ItemTemplate>
                            <asp:Label ID="lblShowDate" runat="server" Text='<%# Eval("ShowDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="修改">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnModify" runat="server" CommandArgument='<%# Eval("PostId") %>'
                                Text="修改" CommandName="myModify" CausesValidation="False" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="刪除">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("PostId") %>'
                                CommandName="myDel" CausesValidation="False" Text="刪除" OnClientClick="return confirm('確認刪除此文章?')" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
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

