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
				<!--最後一組請記得在li之處要加上 store_item_a_last 這個class-->
			</ul>

            <asp:Literal ID="ltlDiscounts" runat="server"></asp:Literal>
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
					<td rowspan="3"
							class="discount_list_type discount_list_type_3G" >3G</td>
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
					<td rowspan="3"
							class="discount_list_type discount_list_type_4G" >4G</td>					
				</tr>
				<tr class="discount_list_contant">
					<td class="discount_list_pro">383月租半價192(30)</td>
					<td class="discount_list_dis">3000</td>
					<td class="discount_list_pre">2500</td>
					<td class="discount_list_tran">依量計價</td>
					<td class="discount_list_con">383抵通話</td>
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

		<!-- ################### fareastone ################### -->
			<%--<table class="discount_list border">
				<tr class="discount_list_title border">
					<th class="discount_list_img" style="background-image:url('imgs/logo/logo_fet.png')">
						<a name="pro_2" id="pro_2"></a></th>
					<th class="discount_list_pro bg_disfrom_pro"></th>
					<th class="discount_list_dis bg_disfrom_dis"></th>
					<th class="discount_list_pre bg_disfrom_pre"></th>
					<th class="discount_list_tran bg_disfrom_tran"></th>
					<th class="discount_list_con bg_disfrom_con"></th>
				</tr>

				<!--     3G     -->
				<tr class="discount_list_contant discount_list_contant-last">
					<td rowspan="1"
							class="discount_list_type discount_list_type_3G" >3G</td>
					<td class="discount_list_pro">398(月租半價)+499(30)可升級4G吃到飽</td>
					<td class="discount_list_dis">8500</td>
					<td class="discount_list_pre">3600</td>
					<td class="discount_list_tran">上網吃到飽</td>
					<td class="discount_list_con">網內前5分免費/網外43分</td>
				</tr>

				<!--     4G     -->
				<tr class="discount_list_contant">
					<td rowspan="9"
							class="discount_list_type discount_list_type_4G" >4G</td>
					<td class="discount_list_pro">599月租半價299(30)</td>
					<td class="discount_list_dis">4500</td>
					<td class="discount_list_pre">1000</td>
					<td class="discount_list_tran">1G</td>
					<td class="discount_list_con">網內前5分免費/網外送30分</td>
				</tr>
				<tr class="discount_list_contant">
					<td class="discount_list_pro">599(30)攜碼免學生證</td>
					<td class="discount_list_dis">8000</td>
					<td class="discount_list_pre">500</td>
					<td class="discount_list_tran">5G</td>
					<td class="discount_list_con">網內前5分免費/網外送30分</td>
				</tr>
				<tr class="discount_list_contant">
					<td class="discount_list_pro">799(30)攜碼免學生證</td>
					<td class="discount_list_dis">10000</td>
					<td class="discount_list_pre">5000</td>
					<td class="discount_list_tran">7G</td>
					<td class="discount_list_con">網內前10分免費/網外送40分</td>
				</tr>
				<tr class="discount_list_contant">
					<td class="discount_list_pro">399(30)</td>
					<td class="discount_list_dis">5000</td>
					<td class="discount_list_pre">2400</td>
					<td class="discount_list_tran">2G</td>
					<td class="discount_list_con">網內前3分免費/網外送20分</td>
				</tr>
				<tr class="discount_list_contant">
					<td class="discount_list_pro">599(30)首年4G上網吃到飽</td>
					<td class="discount_list_dis">8000</td>
					<td class="discount_list_pre">5000</td>
					<td class="discount_list_tran">6G</td>
					<td class="discount_list_con">網內前5分免費/網外送30分</td>
				</tr>
				<tr class="discount_list_contant discount_list_contant-lastTR">
					<td class="discount_list_pro">799(30)</td>
					<td class="discount_list_dis">11000</td>
					<td class="discount_list_pre">6000</td>
					<td class="discount_list_tran">9G</td>
					<td class="discount_list_con">網內前10分免費/網外送40分</td>
				</tr>
				<tr class="discount_list_contant discount_list_contant-lastTR">
					<td class="discount_list_pro">999(30)</td>
					<td class="discount_list_dis">13000</td>
					<td class="discount_list_pre">8000</td>
					<td class="discount_list_tran">16G</td>
					<td class="discount_list_con">網內前10分免費/網外送50分</td>
				</tr>
				<tr class="discount_list_contant discount_list_contant-lastTR">
					<td class="discount_list_pro">1199(30)</td>
					<td class="discount_list_dis">15000</td>
					<td class="discount_list_pre">10000</td>
					<td class="discount_list_tran">26G</td>
					<td class="discount_list_con">網內免費/網外送70分</td>
				</tr>
				<tr class="discount_list_contant discount_list_contant-lastTR">
					<td class="discount_list_pro">1399(30)</td>
					<td class="discount_list_dis">折扣下殺</td>
					<td class="discount_list_pre">12000</td>
					<td class="discount_list_tran">4G吃到飽</td>
					<td class="discount_list_con">網內免費/網外送100分</td>
				</tr>
			</table>--%>
		<!-- ################### fareastone ################### -->




	</div>
	<!-- ################## Contant-->
</asp:Content>

