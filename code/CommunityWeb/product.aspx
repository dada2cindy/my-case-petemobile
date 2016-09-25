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
                        <asp:Literal ID="ltlSummary" runat="server"></asp:Literal>\
                        <li>
                            市價：<span class="product_item_priceDis">$<asp:Literal ID="ltlPrice" runat="server"></asp:Literal></span>
						    <br/>會員單機價：<span class="product_item_price">$<asp:Literal ID="ltlSellPrice" runat="server"></asp:Literal></span>
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
    <%--<ul class="store_list_a">
      <a href="#pro_1"><li class="store_item_a store_item_logo border"
          style="background-image: url('imgs/logo/logo_hinet.png');">
          <div class="corner_bg corner_img"></div>
      </li></a>
      <a href="#pro_2"><li class="store_item_a store_item_logo border"
          style="background-image: url('imgs/logo/logo_fet.png');">
          <div class="corner_bg corner_img"></div>
      </li></a>
      <a href="#pro_3"><li class="store_item_a store_item_logo border"
          style="background-image: url('imgs/logo/logo_twm.png');">
          <div class="corner_bg corner_img"></div>
      </li></a>
      <a href="#pro_4"><li class="store_item_a store_item_logo border"
          style="background-image: url('imgs/logo/logo_tstartel.png');">
          <div class="corner_bg corner_img"></div>
      </li></a>
      <a href="#pro_5"><li class="store_item_a store_item_logo border store_item_a_last"
          style="background-image: url('imgs/logo/logo_aptg.png');">
          <div class="corner_bg corner_img"></div>
      </li></a>
      <!--最後一組請記得在li之處要加上 store_item_a_last 這個class-->
    </ul>--%>
    <!-- ################### Hinet ################### -->
			<%--<table class="discount_list border">
				<tr class="discount_list_title border">
					<th class="discount_list_img" style="background-image:url('imgs/logo/logo_hinet.png')">
						<a name="pro_1" id="pro_1"></a></th>
						<th class="discount_list_pro bg_disfrom_pro"></th>
						<th class="discount_list_dis bg_disfrom_dis"></th>
						<th class="discount_list_pre bg_disfrom_pre"></th>
						<th class="discount_list_tran bg_disfrom_tran"></th>
						<th class="discount_list_con bg_disfrom_con"></th>
				</tr>

				<!--     3G     -->
				<tr class="discount_list_contant">
					<td rowspan="6"
							class="discount_list_type discount_list_type_3G" >3G</td>
					<td class="discount_list_pro">383月租半價192(30)</td>
					<td class="discount_list_dis">3000</td>
					<td class="discount_list_pre">2500</td>
					<td class="discount_list_tran">依量計價</td>
					<td class="discount_list_con">383抵通話</td>
				</tr>
				<tr class="discount_list_contant">
					<td class="discount_list_pro">383月租半價192(30)</td>
					<td class="discount_list_dis">3000</td>
					<td class="discount_list_pre">2500</td>
					<td class="discount_list_tran">依量計價</td>
					<td class="discount_list_con">383抵通話</td>
				</tr>
				<tr class="discount_list_contant">
					<td class="discount_list_pro">383(24)</td>
					<td class="discount_list_dis">4000</td>
					<td class="discount_list_pre">4000</td>
					<td class="discount_list_tran">依量計價</td>
					<td class="discount_list_con">383抵通話</td>
				</tr>
				<tr class="discount_list_contant">
					<td class="discount_list_pro">583/589/699(24)</td>
					<td class="discount_list_dis">5000</td>
					<td class="discount_list_pre">5000</td>
					<td class="discount_list_tran">依量計價</td>
					<td class="discount_list_con">583抵通話</td>
				</tr>
				<tr class="discount_list_contant">
					<td class="discount_list_pro">583/589/699(24)</td>
					<td class="discount_list_dis">5000</td>
					<td class="discount_list_pre">5000</td>
					<td class="discount_list_tran">依量計價</td>
					<td class="discount_list_con">589抵網內外行動語音共122分</td>
				</tr>
				<tr class="discount_list_contant discount_list_contant-last">
					<td class="discount_list_pro">583/589/699(24)</td>
					<td class="discount_list_dis">5000</td>
					<td class="discount_list_pre">5000</td>
					<td class="discount_list_tran">依量計價</td>
					<td class="discount_list_con">699抵網內外市話各699元</td>
				</tr>

				<!--     4G     -->
				<tr class="discount_list_contant">
					<td rowspan="6"
							class="discount_list_type discount_list_type_4G" >4G</td>
					<td class="discount_list_pro">383月租半價192(30)</td>
					<td class="discount_list_dis">3000</td>
					<td class="discount_list_pre">2500</td>
					<td class="discount_list_tran">依量計價</td>
					<td class="discount_list_con">383抵通話</td>
				</tr>
				<tr class="discount_list_contant">
					<td class="discount_list_pro">383月租半價192(30)</td>
					<td class="discount_list_dis">3000</td>
					<td class="discount_list_pre">2500</td>
					<td class="discount_list_tran">依量計價</td>
					<td class="discount_list_con">383抵通話</td>
				</tr>
				<tr class="discount_list_contant">
					<td class="discount_list_pro">383(24)</td>
					<td class="discount_list_dis">4000</td>
					<td class="discount_list_pre">4000</td>
					<td class="discount_list_tran">依量計價</td>
					<td class="discount_list_con">383抵通話</td>
				</tr>
				<tr class="discount_list_contant">
					<td class="discount_list_pro">583/589/699(24)</td>
					<td class="discount_list_dis">5000</td>
					<td class="discount_list_pre">5000</td>
					<td class="discount_list_tran">依量計價</td>
					<td class="discount_list_con">583抵通話</td>
				</tr>
				<tr class="discount_list_contant">
					<td class="discount_list_pro">583/589/699(24)</td>
					<td class="discount_list_dis">5000</td>
					<td class="discount_list_pre">5000</td>
					<td class="discount_list_tran">依量計價</td>
					<td class="discount_list_con">589抵網內外行動語音共122分</td>
				</tr>
				<tr class="discount_list_contant discount_list_contant-last">
					<td class="discount_list_pro">583/589/699(24)</td>
					<td class="discount_list_dis">5000</td>
					<td class="discount_list_pre">5000</td>
					<td class="discount_list_tran">依量計價</td>
					<td class="discount_list_con">699抵網內外市話各699元</td>
				</tr>
			</table>--%>
		<!-- ################### Hinet ################### -->
	</div>
	<!-- ################## detial discount-->

	<!-- detial product ##################-->
	<div class="product_detail" id="detial_pro">
		<div class="contant_title">產品介紹</div>
        <asp:Literal ID="ltlHtmlContent" runat="server"></asp:Literal>        
	</div>
	<!-- ################## detial product-->

	<!-- detial comment ##################-->
	<div class="product_detail" id="detial_com">
		<div class="contant_title">評論分享</div>
		<%--<div class="comment_form border">
			<form>
				<div class="comment_right">
					<textarea class="comment_textarea border"></textarea>
				</div>
				<div class="comment_right comment_title" style="letter-spacing: 5px;">留言與建議</div>
				<div class="comment_item"><span class="comment_title">姓名</span><input class="bg_gray" type="text" /></div>
				<div class="comment_item"><span class="comment_title">電話</span><input class="bg_gray" type="text" /></div>
				<div class="comment_item"><span class="comment_title">電子信箱</span><input class="bg_gray" type="text" /></div>
				<input class="comment_submit themecolor_main" type="submit" value="送出">
			</form>
		</div>

		<!--  user messages -->
		<div class="comment_msg border">
			<span class="comment_msg_time">留言時間： 2016-09-19 13:40:30</span>
			<div class="comment_msg_title txt_themecolor_main">陳**</div>
			<div class="comment_msg_msg">請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～</div>
			<div class="comment_reply bg_gray" >
				<div class="comment_msg_title txt_highline">品讚 於 2016-09-19 13:40:30 回覆 ：</div>
				<div>
					請問～～～～
				</div>
			</div>
		</div>
		<!--  user messages -->
		<div class="comment_msg border">
			<span class="comment_msg_time">留言時間： 2016-09-19 13:40:30</span>
			<div class="comment_msg_title txt_themecolor_main">陳**</div>
			<div class="comment_msg_msg">請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～請問～～～～</div>
		</div>

		<div class="comment_pagger">
			<i class="fa fa-angle-left fa-1x comment_pagger_angle" aria-hidden="true"></i>
			<i class="fa fa-1x border comment_pagger_item comment_pagger_item-avtive" aria-hidden="true">1</i>
			<i class="fa fa-1x border comment_pagger_item" aria-hidden="true">2</i>
			<i class="fa fa-1x border comment_pagger_item" aria-hidden="true">3</i>
			<i class="fa fa-1x border comment_pagger_item" aria-hidden="true">...</i>
			<i class="fa fa-angle-right fa-1x comment_pagger_angle" aria-hidden="true"></i>
		</div>
		<div class="comment_info">顯示 1 - 5 項，共 34 項</div>--%>



	</div>
	<!-- ################## detial comment -->

	<!-- detial fifty ##################-->
	<div class="product_detail" id="detial_fif">
		<div class="contant_title">50強</div>
    <%--<ul class="store_list_a">
      <a href="#pro_1"><li class="store_item_a store_item_logo border"
          style="background-image: url('imgs/logo/logo_apple.png');">
          <div class="corner_bg corner_img"></div>
      </li></a>
      <a href="#pro_2"><li class="store_item_a store_item_logo border"
          style="background-image: url('imgs/logo/logo_asus.png');">
          <div class="corner_bg corner_img"></div>
      </li></a>
      <a href="#pro_3"><li class="store_item_a store_item_logo border"
          style="background-image: url('imgs/logo/logo_htc.png');">
          <div class="corner_bg corner_img"></div>
      </li></a>
      <a href="#pro_4"><li class="store_item_a store_item_logo border"
          style="background-image: url('imgs/logo/logo_lg.png');">
          <div class="corner_bg corner_img"></div>
      </li></a>
      <a href="#pro_5"><li class="store_item_a store_item_logo border"
          style="background-image: url('imgs/logo/logo_samsung.png');">
          <div class="corner_bg corner_img"></div>
      </li></a>
      <a href="#pro_6"><li class="store_item_a store_item_logo border"
          style="background-image: url('imgs/logo/logo_sony.png');">
          <div class="corner_bg corner_img"></div>
      </li></a>
      <a href="#pro_7"><li class="store_item_a store_item_logo border"
          style="background-image: url('imgs/logo/logo_infocus.png');">
          <div class="corner_bg corner_img"></div>
      </li></a>
      <a href="#pro_8"><li class="store_item_a store_item_logo border"
          style="background-image: url('imgs/logo/logo_wuawei.png');">
          <div class="corner_bg corner_img"></div>
      </li></a>
      <a href="#pro_9"><li class="store_item_a store_item_logo border"
          style="background-image: url('imgs/logo/logo_sharp.png');">
          <div class="corner_bg corner_img"></div>
      </li></a>
      <a href="#pro_10"><li class="store_item_a store_item_logo border"
          style="background-image: url('imgs/logo/logo_oppo.png');">
          <div class="corner_bg corner_img"></div>
      </li></a>
      <a href="#pro_11"><li class="store_item_a store_item_logo border"
          style="background-image: url('imgs/logo/logo_pin.png');">
          <div class="corner_bg corner_img"></div>
      </li></a>
      <a href="#pro_12"><li class="store_item_a store_item_logo border"
          style="background-image: url('imgs/logo/logo_jbl.png');">
          <div class="corner_bg corner_img"></div>
      </li></a>
      <a href="#pro_12"><li class="store_item_a store_item_logo border store_item_a_last"
          style="background-image: url('imgs/logo/logo_mi.png');">
          <div class="corner_bg corner_img"></div>
      </li></a>

      <!--最後一組請記得在li之處要加上 store_list_a_last 這個class-->
    </ul>--%>

    <!-- apple -->
    <%--<div class="product_ov">
      <div class="product_ov_img">
        <a name="pro_1" id="A1"><div style="background-image:url('imgs/logo/logo_apple.png')"></div></a>
      </div>
      <div class="product_ov_item">
        <div class="product_ov_item_name">
          <a href="products_detail.php?idept=1&amp;pk=527">iphone se 16GB</a>
        </div>
        <div class="product_ov_item_price">$13800</div>
      </div>
      <div class="product_ov_item">
        <div class="product_ov_item_name">
          <a href="products_detail.php?idept=1&amp;pk=527">iphone se 16GB</a>
        </div>
        <div class="product_ov_item_price">$13800</div>
      </div>
      <div class="product_ov_item product_ov_item-last">
        <div class="product_ov_item_name">
          <a href="products_detail.php?idept=1&amp;pk=527">iphone se 16GB</a>
        </div>
        <div class="product_ov_item_price">$13800</div>
      </div>
    </div>--%>
	</div>
	<!-- ################## detial fifty-->
</asp:Content>
