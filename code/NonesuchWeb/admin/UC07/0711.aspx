<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdmMasterPage.master" AutoEventWireup="true" CodeFile="0711.aspx.cs" Inherits="admin_UC07_0711" ValidateRequest="false" %>

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
                        業績月份:
                        <asp:DropDownList ID="ddlSearchYear" runat="server" ValidationGroup="Search">
                        </asp:DropDownList>
                        年
                        &nbsp;
                        <asp:DropDownList ID="ddlSearchMonth" runat="server" ValidationGroup="Search">
                        </asp:DropDownList>
                        月                        
                        <br /><br />
                        <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" ValidationGroup="Search" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSearchExport" runat="server" Text="查詢結果匯出" OnClick="btnSearchExport_Click" ValidationGroup="Search" />                                          
                        <br />
                        <%--<br />--%>
                        <%--<div class="customNumber">未核發佣金總額：<asp:Label ID="lblNotGetCommission" runat="server" Text="0" ForeColor="Red"></asp:Label></div>--%>
                    </td>
                    <td align="right" class="labelText">                        
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="gvList" runat="server" BackColor="White" BorderColor="#DEDFDE"
                AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                ForeColor="Black" GridLines="Vertical" Width="100%" CssClass="datagrid2"
                OnRowDataBound="gvList_RowDataBound">
                <RowStyle BackColor="#F7F7DE" />
                <FooterStyle BackColor="#CCCC99" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
                <Columns> 
                    <asp:TemplateField HeaderText="姓名">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>                            
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="140" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="本月目標">
                        <ItemTemplate>
                            <asp:Label ID="lblTarget" runat="server" Text='<%# Bind("Target") %>'></asp:Label>
                            <asp:TextBox ID="txtTarget" runat="server" Text='<%# Bind("Target") %>' Width="70px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="太電">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyTelCom1Count" runat="server" Text='<%# Bind("ApplyTelCom1Count") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="遠傳">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyTelCom2Count" runat="server" Text='<%# Bind("ApplyTelCom2Count") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="中華">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyTelCom3Count" runat="server" Text='<%# Bind("ApplyTelCom3Count") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="亞太">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyTelCom4Count" runat="server" Text='<%# Bind("ApplyTelCom4Count") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="星星">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyTelCom5Count" runat="server" Text='<%# Bind("ApplyTelCom5Count") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField> 
                    <asp:TemplateField >
                        <HeaderTemplate>
                            上線<br />件數
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblApplyCount" runat="server" Text='<%# Bind("ApplyCount") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="門號營收">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyRevenue" runat="server" Text='<%# Bind("ApplyRevenue") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="門號毛利">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyProfit" runat="server" Text='<%# Bind("ApplyProfit") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            配件<br />件數
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFittingCount" runat="server" Text='<%# Bind("FittingCount") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="配件營收">
                        <ItemTemplate>
                            <asp:Label ID="lblFittingRevenue" runat="server" Text='<%# Bind("FittingRevenue") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="配件毛利">
                        <ItemTemplate>
                            <asp:Label ID="lblFittingProfit" runat="server" Text='<%# Bind("FittingProfit") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="總毛利">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalProfit" runat="server" Text='<%# Bind("TotalProfit") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="達成率">
                        <ItemTemplate>
                            <asp:Label ID="lblTargetAchievementRates" runat="server" Text='<%# Bind("TargetAchievementRates") %>' ForeColor="Blue"></asp:Label> %
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                                                                                  
                </Columns>
                <EmptyDataTemplate>
                    <div align="left">
                        無資料。</div>
                </EmptyDataTemplate>
            </asp:GridView>
            <br />            
            <asp:GridView ID="gvListStore" runat="server" BackColor="White" BorderColor="#DEDFDE"
                AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                ForeColor="Black" GridLines="Vertical" Width="100%" CssClass="datagrid2"
                OnRowDataBound="gvListStore_RowDataBound">
                <RowStyle BackColor="#F7F7DE" />
                <FooterStyle BackColor="#CCCC99" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
                <Columns> 
                    <asp:TemplateField HeaderText="銷售店點">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>                            
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="140" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            未核發<br />佣金總額
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblNotGetTotalCommission" runat="server" ForeColor="Red" Text='<%# Bind("NotGetTotalCommission") %>'></asp:Label>                            
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="本月目標">
                        <ItemTemplate>
                            <asp:Label ID="lblTarget" runat="server" Text='<%# Bind("Target") %>'></asp:Label>
                            <asp:TextBox ID="txtTarget" runat="server" Text='<%# Bind("Target") %>' Width="70px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="太電">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyTelCom1Count" runat="server" Text='<%# Bind("ApplyTelCom1Count") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="遠傳">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyTelCom2Count" runat="server" Text='<%# Bind("ApplyTelCom2Count") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="中華">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyTelCom3Count" runat="server" Text='<%# Bind("ApplyTelCom3Count") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="亞太">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyTelCom4Count" runat="server" Text='<%# Bind("ApplyTelCom4Count") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="星星">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyTelCom5Count" runat="server" Text='<%# Bind("ApplyTelCom5Count") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField> 
                    <asp:TemplateField >
                        <HeaderTemplate>
                            上線<br />件數
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblApplyCount" runat="server" Text='<%# Bind("ApplyCount") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="門號營收">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyRevenue" runat="server" Text='<%# Bind("ApplyRevenue") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="門號毛利">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyProfit" runat="server" Text='<%# Bind("ApplyProfit") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            配件<br />件數
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFittingCount" runat="server" Text='<%# Bind("FittingCount") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="配件營收">
                        <ItemTemplate>
                            <asp:Label ID="lblFittingRevenue" runat="server" Text='<%# Bind("FittingRevenue") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="配件毛利">
                        <ItemTemplate>
                            <asp:Label ID="lblFittingProfit" runat="server" Text='<%# Bind("FittingProfit") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="總毛利">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalProfit" runat="server" Text='<%# Bind("TotalProfit") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="達成率">
                        <ItemTemplate>
                            <asp:Label ID="lblTargetAchievementRates" runat="server" Text='<%# Bind("TargetAchievementRates") %>' ForeColor="Blue"></asp:Label> %
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                                                                                  
                </Columns>
                <EmptyDataTemplate>
                    <div align="left">
                        無資料。</div>
                </EmptyDataTemplate>
            </asp:GridView>
            <br />
            <div style="width:100%";>
                <asp:Button ID="btnSave" runat="server" Text="修改本月目標" ValidationGroup="Save" 
                    onclick="btnSave_Click" />
            </div>  
            <br />      
        </fieldset>
    </asp:Panel>
</asp:Content>

