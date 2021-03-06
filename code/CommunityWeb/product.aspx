﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="product.aspx.cs" Inherits="product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
    <script type="text/javascript" src="js/jquery.nivo.slider.pack.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#detial_tab_dis").click(function () {
                discount_show();
            });
            $("#detial_tab_pro").click(function () {
                product_show();
            });
            $("#detial_tab_com").click(function () {
                comment_show();
            });
            $("#detial_tab_fif").click(function () {
                fifty_show();
            });
            product_show();
        });

        var speed = 300;

        function discount_show() {
            $("#detial_dis").show(speed);
            $("#detial_tab_dis").attr('class', 'detial_tab_dis_active product_tab_item-active');
            $("#detial_tab_pro").attr('class', 'detial_tab_pro');
            $("#detial_tab_com").attr('class', 'detial_tab_com');
            $("#detial_tab_fif").attr('class', 'detial_tab_fif');
            $("#detial_pro").hide();
            $("#detial_com").hide();
            $("#detial_fif").hide();
        }
        function product_show() {
            $("#detial_pro").show(speed);
            $("#detial_tab_pro").attr('class', 'detial_tab_pro_active product_tab_item-active');
            $("#detial_tab_dis").attr('class', 'detial_tab_dis');
            $("#detial_tab_com").attr('class', 'detial_tab_com');
            $("#detial_tab_fif").attr('class', 'detial_tab_fif');
            $("#detial_dis").hide();
            $("#detial_com").hide();
            $("#detial_fif").hide();
        }
        function comment_show() {
            $("#detial_com").show(speed);
            $("#detial_tab_com").attr('class', 'detial_tab_com_active product_tab_item-active');
            $("#detial_tab_dis").attr('class', 'detial_tab_dis');
            $("#detial_tab_pro").attr('class', 'detial_tab_pro');
            $("#detial_tab_fif").attr('class', 'detial_tab_fif');
            $("#detial_pro").hide();
            $("#detial_dis").hide();
            $("#detial_fif").hide();
        }
        function fifty_show() {
            $("#detial_fif").show(speed);
            $("#detial_tab_fif").attr('class', 'detial_tab_fif_active product_tab_item-active');

            $("#detial_tab_dis").attr('class', 'detial_tab_dis');
            $("#detial_tab_pro").attr('class', 'detial_tab_pro');
            $("#detial_tab_com").attr('class', 'detial_tab_com');
            $("#detial_pro").hide();
            $("#detial_com").hide();
            $("#detial_dis").hide();
        }
    </script>
    <link rel="stylesheet" href="css/product.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">        
    <div class="breadcrumb">
		<p>目前位置 <i class="fa fa-angle-right fa-1x comment_pagger_angle" aria-hidden="true"></i> <a href="index.html">首頁</a> <i class="fa fa-angle-right fa-1x comment_pagger_angle" aria-hidden="true"></i> <span class="txt_themecolor_main">產品目錄</span></p>
	</div>
	<!--Contant ##################-->

	<div class="contant_title">產品目錄</div>
	<div class="contant">
		<div class="contant_left" style="width:744px;">
			<div class="product_item">
                <asp:Literal ID="ltlProductPic" runat="server"></asp:Literal>
				
				<div class="product_item_info">
					<div class="product_item_title"><asp:Literal ID="ltlTitle" runat="server"></asp:Literal></div>
					<ul>
                        <asp:Literal ID="ltlSummary" runat="server"></asp:Literal>
                        <li>
                            市價：<span class="product_item_priceDis">$<asp:Literal ID="ltlPrice" runat="server"></asp:Literal></span>
						    <br/>
                            <asp:literal runat="server" ID="ltlPriceTitle" ></asp:literal>：<span class="product_item_price">$<asp:Literal ID="ltlSellPrice" runat="server"></asp:Literal></span>
						</li>
					</ul>
					<div class="product_item_line"></div>
					<div class="product_item_others">
						如果選擇搭配門號購買，商品價格依照實際方案為準<br/>
						請參考門市折扣
					</div>
				</div>
			</div>


		</div>

		<div class="contant_right" style="min-height:500px;">
            <asp:Literal ID="ltlRight" runat="server"></asp:Literal>
			<iframe src="https://www.facebook.com/plugins/page.php?href=https%3A%2F%2Fwww.facebook.com%2F%E5%93%81%E8%AE%9A%E8%A1%8C%E5%8B%95%E9%80%9A%E8%A8%8A-1743733255899753%2F&tabs=timeline&width=300&height=370&small_header=false&adapt_container_width=true&hide_cover=false&show_facepile=true&appId=167136590020161" width="300" height="370" style="border: none; overflow: hidden" scrolling="no" frameborder="0" allowtransparency="true"></iframe>
		</div>
	</div>
	<!-- ################## Contant-->

	<!--product tab ##################-->
	<div class="product_tab">
		<ul class="product_tab_item">
            <li id="detial_tab_pro" class="">產品介紹</li>
			<li id="detial_tab_dis" class="">門號折扣</li>			
			<li id="detial_tab_com" class="">評論分享</li>
			<li id="detial_tab_fif" class="">50強</li>
		</ul>
		<div class="product_tab_line"></div>
	</div>
	<!-- ################## roduct tab-->



	<!-- detial discount ##################-->
	<div class="product_detail" id="detial_dis">
		<div class="contant_title">門號折扣</div>
    <ul class="store_list_a">
        <asp:Literal ID="ltlSuppliersTag" runat="server"></asp:Literal>      
    </ul>
        <asp:Literal ID="ltlDiscounts" runat="server"></asp:Literal>
	</div>
	<!-- ################## detial discount-->

	<!-- detial product ##################-->
	<div class="product_detail sogi" id="detial_pro">
		<div class="contant_title">產品介紹</div>
        <asp:Literal ID="ltlHtmlContent" runat="server"></asp:Literal>        
	</div>
	<!-- ################## detial product-->

	<!-- detial comment ##################-->
	<div class="product_detail" id="detial_com">
		<div class="contant_title">評論分享</div>
        <asp:Literal ID="ltlFbComments" runat="server"></asp:Literal>
        <%--<div class="fb-comments" data-href="http://p-like.com.tw/product.aspx?id=74" data-width="1080" data-include-parent="false" data-numposts="5"></div>--%>
	</div>
	<!-- ################## detial comment -->

	<!-- detial fifty ##################-->
	<div class="product_detail" id="detial_fif">
		<div class="contant_title">50強</div>
    <ul class="store_list_a">
      <asp:Literal ID="ltlSuppliersTag2" runat="server"></asp:Literal>	      
    </ul>
        <asp:Literal ID="ltlProducts2" runat="server"></asp:Literal>	                
    </div>
	<!-- ################## detial fifty-->
</asp:Content>

