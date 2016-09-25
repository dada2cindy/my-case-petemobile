<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="discount.aspx.cs" Inherits="discount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="breadcrumb">
		<p>目前位置 <i class="fa fa-angle-right fa-1x comment_pagger_angle" aria-hidden="true"></i> <a href="index.aspx">首頁</a> <i class="fa fa-angle-right fa-1x comment_pagger_angle" aria-hidden="true"></i> <span class="txt_themecolor_main">門號折扣</span></p>
	</div>
	<!--Contant ##################-->

	<div class="contant_title">請選擇電信公司</div>

	<div class="contant">
			<ul class="store_list_a">
                <asp:Literal ID="ltlSuppliersTag" runat="server"></asp:Literal>
			</ul>

            <asp:Literal ID="ltlDiscounts" runat="server"></asp:Literal>		




	</div>
	<!-- ################## Contant-->
</asp:Content>

