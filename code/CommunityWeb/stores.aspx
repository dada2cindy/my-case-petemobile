<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="stores.aspx.cs" Inherits="stores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">  
    <link rel="stylesheet" href="css/shadowbox.css" type="text/css" /> 
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/shadowbox.js"></script> 
    <script type="text/javascript">
        Shadowbox.init();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="breadcrumb">
		<p>目前位置 <i class="fa fa-angle-right fa-1x comment_pagger_angle" aria-hidden="true"></i> <a href="index.aspx">首頁</a> <i class="fa fa-angle-right fa-1x comment_pagger_angle" aria-hidden="true"></i> <span class="txt_themecolor_main">門市據點</span></p>
	</div>
	<!--Contant ##################-->

	<div class="contant_title">請選擇門市</div>
	<div class="contant" style="height:1100px;">
		<div class="contant_left" style="width:744px;">
			<ul class="store_list_a">
                <asp:Repeater ID="RepeaterStoreTag" runat="server">
                    <ItemTemplate>
                        <a href="#shop_<%# Eval("PostId") %>"><li class="store_item_a border corner_bg"><%# Eval("Title") %></li></a>
                    </ItemTemplate>
                </asp:Repeater>
			</ul>
			<div class="store_info border corner_bg">
				PS.建議各位買家，欲來店購買前如尚有任何問題想洽詢或怕白跑一趟，歡迎您先來電詢問，線上會有專業的人員為您解說，符合您的需求再來店辦理即可，謝謝！
			</div>
            <asp:Repeater ID="RepeaterStore" runat="server">
                <ItemTemplate>
                    <div class="store_item_b">
                        <div class="store_item_b_img"
                            style="background-image: url('upload/<%# Eval("PicFileName") %>')">
                            <a name="shop_<%# Eval("PostId") %>" id="shop_<%# Eval("PostId") %>"></a>
                            <img src="imgs/store<%# Eval("SortNo") %>.png" />
                        </div>
                        <div class="store_item_b_info_br font_title bg_gray">
                            <a class="store_item_b_map" href="googlemap.aspx?id=<%# Eval("PostId") %>" rel="shadowbox;height=800;width=800">
                                <img src="imgs/common/map-icon.png" width="50px" /></a>
                            <ul class="store_item_b_info">
                                <li class="store_item_b_info_title">服務電話：</li>
                                <li class="store_item_b_info_info"><%# Eval("Phone") %></li>
                                <li class="store_item_b_info_title">傳真電話：</li>
                                <li class="store_item_b_info_info"><%# Eval("Fax") %></li>
                                <%--<li class="store_item_b_info_title">行動電話：</li>
                                <li class="store_item_b_info_info"><%# Eval("Mobile") %></li>--%>
                                <li class="store_item_b_info_title">E-mail：</li>
                                <li class="store_item_b_info_info"><a href="mailto:<%# Eval("CustomField1") %>"><%# Eval("CustomField1") %></a></li>
                                <%--<li class="store_item_b_info_title">Line ID：</li>
                                <li class="store_item_b_info_info"><%# Eval("CustomField2") %></li>--%>
                                <li class="store_item_b_info_title">店面位址：</li>
                                <li class="store_item_b_info_info"><%# Eval("Address") %></li>
                                <li class="store_item_b_info_title">營業時間：</li>
                                <li class="store_item_b_info_info"><%# Eval("Summary") %></li>
                            </ul>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>



        </div>

		<div class="contant_right">
            <asp:Literal ID="ltlRight" runat="server"></asp:Literal>
			<iframe src="https://www.facebook.com/plugins/page.php?href=https%3A%2F%2Fwww.facebook.com%2F%E5%93%81%E8%AE%9A%E8%A1%8C%E5%8B%95%E9%80%9A%E8%A8%8A-%E9%85%92%E6%B3%89%E5%BA%97-1733611060260552%2F&tabs=timeline&width=300&height=360&small_header=false&adapt_container_width=true&hide_cover=false&show_facepile=true&appId=167136590020161" width="300" height="360" style="border:none;overflow:hidden" scrolling="no" frameborder="0" allowTransparency="true"></iframe>
		</div>

	</div>
	<!-- ################## Contant-->    
</asp:Content>

