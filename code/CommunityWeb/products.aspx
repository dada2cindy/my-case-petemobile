<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="products.aspx.cs" Inherits="products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="breadcrumb">
		<p>目前位置 <i class="fa fa-angle-right fa-1x comment_pagger_angle" aria-hidden="true"></i> <a href="index.html">首頁</a> <i class="fa fa-angle-right fa-1x comment_pagger_angle" aria-hidden="true"></i> <span class="txt_themecolor_main">價格總覽</span></p>
	</div>
	<!--Contant ##################-->
    <asp:Literal ID="ltlKeyword" runat="server"></asp:Literal>    
	<div class="contant_title">請選擇品牌</div>
	<div class="contant">
		<div class="contant_left" style="width:744px;">
			<ul class="store_list_a">
                <asp:Literal ID="ltlSuppliersTag" runat="server"></asp:Literal>				
			</ul>

            <asp:Literal ID="ltlProducts" runat="server"></asp:Literal>		



		</div>

		<div class="contant_right">
            <asp:Literal ID="ltlRight" runat="server"></asp:Literal>
			<iframe src="https://www.facebook.com/plugins/page.php?href=https%3A%2F%2Fwww.facebook.com%2F%E5%93%81%E8%AE%9A%E8%A1%8C%E5%8B%95%E9%80%9A%E8%A8%8A-1743733255899753%2F&tabs=timeline&width=300&height=370&small_header=false&adapt_container_width=true&hide_cover=false&show_facepile=true&appId=167136590020161" width="300" height="370" style="border: none; overflow: hidden" scrolling="no" frameborder="0" allowtransparency="true"></iframe>
		</div>
	</div>
	<!-- ################## Contant-->
</asp:Content>

