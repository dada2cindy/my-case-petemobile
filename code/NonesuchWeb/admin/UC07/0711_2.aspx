<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdmMasterPage.master" AutoEventWireup="true" CodeFile="0711_2.aspx.cs" Inherits="admin_UC07_0711_2" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="aspnetpager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
    <asp:Panel ID="pnlGv" runat="server" DefaultButton="btnSearch">
        <fieldset class="fieldset">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td align="left">
                    <br />   
                        日期：                     
                        <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender7" runat="server" CssClass="MyCalendar"
                            PopupPosition="BottomLeft" Format="yyyy/MM/dd" PopupButtonID="calendar10" TargetControlID="txtDate" />
                        <asp:Image ID="calendar10" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDate"
                            ErrorMessage="請輸入申辦日期" Display="Dynamic" ValidationGroup="Search"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="日期格式錯誤"
                            ControlToValidate="txtDate" Display="Dynamic" ValidationGroup="Search" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>           
                        <br /><br />
                        <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" ValidationGroup="Search" />  
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnRefresh" runat="server" Text="重整一個月結帳資料" OnClick="btnRefresh_Click" />                                         
                        <br />
                        <%--<br />--%>
                        <%--<div class="customNumber">未核發佣金總額：<asp:Label ID="lblNotGetCommission" runat="server" Text="0" ForeColor="Red"></asp:Label></div>--%>
                    </td>
                    <td align="right" class="labelText">  
                             <asp:HyperLink runat="server" NavigateUrl="0711.aspx">業績報表</asp:HyperLink>                 
                    </td>
                </tr>
            </table>
            <br />
            <table cellpadding="5" cellspacing="1" border="0" width="400px">
                <tr>
                    <td align="left" width="60px">
                        前日餘額：
                    </td>
                    <td align="right" width="70px">
                        <asp:Label ID="lblCashYesterday" runat="server" Text="" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" width="80px">
                        今日收支：
                    </td>
                    <td width="70px" align="right">
                        <asp:Label ID="lblTotalToday" runat="server" Text="" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">                        
                    </td>
                    <td>                        
                    </td>
                    <td align="right">
                        今日結餘：
                    </td>
                    <td align="right">
                        <asp:Label ID="lblCashToday" runat="server" Text="" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            今日進貨：&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblBuyToday" runat="server" Text="" Font-Bold="true"></asp:Label>
            <asp:GridView ID="gvBuyToday" runat="server" BackColor="White" BorderColor="#DEDFDE"
                AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                ForeColor="Black" GridLines="Vertical" Width="100%" CssClass="datagrid2">
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
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="140" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="進貨盤商">
                        <ItemTemplate>
                            <asp:Label ID="lblWholesalers" runat="server" Text='<%# Bind("Wholesalers") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="進貨價">
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="數量">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                    
                    <%--<asp:TemplateField HeaderText="操作員">
                        <ItemTemplate>
                            <asp:Label ID="lblUpdateBy" runat="server" Text='<%# Bind("UpdateBy") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>--%>                                                                                                     
                </Columns>
                <EmptyDataTemplate>
                    <div align="left">
                        無資料。</div>
                </EmptyDataTemplate>
            </asp:GridView>
            <br />
            今日銷貨：&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblSellToday" runat="server" Text="" Font-Bold="true"></asp:Label>
            <asp:GridView ID="gvSellToday" runat="server" BackColor="White" BorderColor="#DEDFDE"
                AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                ForeColor="Black" GridLines="Vertical" Width="100%" CssClass="datagrid2">
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
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="140" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="進貨盤商">
                        <ItemTemplate>
                            <asp:Label ID="lblWholesalers" runat="server" Text='<%# Bind("Wholesalers") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="進貨價">
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="售價">
                        <ItemTemplate>
                            <asp:Label ID="lblSellPrice" runat="server" Text='<%# Bind("SellPrice") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="數量">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="銷售員">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomField2" runat="server" Text='<%# Bind("CustomField2") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                                                                                                     
                </Columns>
                <EmptyDataTemplate>
                    <div align="left">
                        無資料。</div>
                </EmptyDataTemplate>
            </asp:GridView>
            <br />
            今日門號：&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMobileToday" runat="server" Text="" Font-Bold="true"></asp:Label>
            <asp:GridView ID="gvMobileToday" runat="server" BackColor="White" BorderColor="#DEDFDE"
                AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                ForeColor="Black" GridLines="Vertical" Width="100%" CssClass="datagrid2">
                <RowStyle BackColor="#F7F7DE" />
                <FooterStyle BackColor="#CCCC99" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
                <Columns> 
                    <asp:TemplateField HeaderText="門號">
                        <ItemTemplate>
                            <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("Mobile") %>'></asp:Label>                            
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="140" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="手機進價">
                        <ItemTemplate>
                            <asp:Label ID="lblPhonePrice" runat="server" Text='<%# Bind("PhonePrice") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="銷售金額">
                        <ItemTemplate>
                            <asp:Label ID="lblPhoneSellPrice" runat="server" Text='<%# Bind("PhoneSellPrice") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="是否預繳">
                        <ItemTemplate>
                            <asp:Label ID="lblSelfPrepayment" runat="server" Text='<%# Bind("SelfPrepayment") %>'></asp:Label>                            
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="預繳金額">
                        <ItemTemplate>
                            <asp:Label ID="lblPrepayment" runat="server" Text='<%# Bind("Prepayment") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="門號佣金">
                        <ItemTemplate>
                            <asp:Label ID="lblCommission" runat="server" Text='<%# Bind("Commission") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>     
                    <asp:TemplateField HeaderText="後退佣金">
                        <ItemTemplate>
                            <asp:Label ID="lblReturnCommission" runat="server" Text='<%# Bind("ReturnCommission") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="吸收違約金">
                        <ItemTemplate>
                            <asp:Label ID="lblBreakMoney" runat="server" Text='<%# Bind("BreakMoney") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                
                    <asp:TemplateField HeaderText="銷售員">
                        <ItemTemplate>
                            <asp:Label ID="lblSales" runat="server" Text='<%# Bind("Sales") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>   
                    <asp:TemplateField HeaderText="手機來自庫存">
                        <ItemTemplate>
                            <asp:Label ID="lblHasPost" runat="server" Text='<%# Bind("GetStr_HasPost") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                                                                                                   
                </Columns>
                <EmptyDataTemplate>
                    <div align="left">
                        無資料。</div>
                </EmptyDataTemplate>
            </asp:GridView>
            <br />  
            特別收支：&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblSpecialToday" runat="server" Text="" Font-Bold="true"></asp:Label>
            <br />
            <asp:Panel ID="pnlSpecialToday" runat="server">
            品名：<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="請輸入品名"
                            ControlToValidate="txtTitle" ValidationGroup="Save" Display="None" ></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlType" runat="server">
                    <asp:ListItem Text="請選擇收支" Value=""></asp:ListItem>
                    <asp:ListItem Text="收入" Value="0"></asp:ListItem>
                    <asp:ListItem Text="支出" Value="1"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="請選擇收支"
                            ControlToValidate="ddlType" Display="None" ValidationGroup="Save"></asp:RequiredFieldValidator>
                金額：
                <asp:TextBox ID="txtPrice" runat="server" Width="100px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="請輸入金額"
                                    ControlToValidate="txtPrice" Display="None" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="金額輸入0以上的整數"
                            ControlToValidate="txtPrice" MaximumValue="1000000" MinimumValue="0" Type="Integer"
                            Display="None" ValidationGroup="Save"></asp:RangeValidator>
                <asp:Button ID="btnSave" runat="server" Text="新增" onclick="btnSave_Click" ValidationGroup="Save"  />
                <asp:ValidationSummary
                    ID="ValidationSummary1" runat="server" ValidationGroup="Save" DisplayMode="List" />
            </asp:Panel>            
            <asp:GridView ID="gvSpecialToday" runat="server" BackColor="White" BorderColor="#DEDFDE"
                AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                ForeColor="Black" GridLines="Vertical" Width="100%" CssClass="datagrid2" OnRowCommand="gvSpecialToday_RowCommand1"
                OnRowDataBound="gvSpecialToday_RowDataBound">
                <RowStyle BackColor="#F7F7DE" />
                <FooterStyle BackColor="#CCCC99" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
                <Columns> 
                    <asp:TemplateField HeaderText="日期">
                        <ItemTemplate>
                            <asp:Label ID="lblCloseDate" runat="server" Text='<%# Eval("CloseDate","{0:yyyy-MM-dd}") %>'></asp:Label>                            
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="140" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="品名">
                        <ItemTemplate>
                            <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="140" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="收支">
                        <ItemTemplate>
                            <asp:Label ID="lblType_Cash" runat="server" Text='<%# Bind("GetStr_Type_Cash") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="金額">
                        <ItemTemplate>
                            <asp:Label ID="lblPrice_Cash" runat="server" Text='<%# Bind("GetStr_Price_Cash") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="刪除">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("PostId") %>'
                                CommandName="myDel" CausesValidation="False" Text="刪除" OnClientClick="return confirm('確認刪除?')" />
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
            <br />     
        </fieldset>
    </asp:Panel>
</asp:Content>

