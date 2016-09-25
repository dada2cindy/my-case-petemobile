<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--Banner ##################-->
    <div class="cycle-slideshow banner_br"
        data-cycle-loader="true"
        data-cycle-fx="scrollHorz"
        data-cycle-prev=".pager_left"
        data-cycle-next=".pager_right"
        data-cycle-speed="600"
        data-cycle-pager=".banner_pager">


        <div class="banner_pager"></div>
        <div class="pager_left"><i class="fa fa-angle-left fa-5x" aria-hidden="true"></i></div>
        <div class="pager_right"><i class="fa fa-angle-right fa-5x" aria-hidden="true"></i></div>        
        <asp:Literal ID="ltlBannerTop" runat="server"></asp:Literal>
    </div>
    <!-- ################## Banner-->

    <!--Contant ##################-->

    <div class="contant_title font_title">最新商品</div>
    <div class="contant">
        <div class="contant_left">
            <ul class="product_list">
                <asp:Repeater ID="RepeaterNewProdct" runat="server">
                    <ItemTemplate>
                        <li class="product_br" style="cursor:pointer;" onclick="window.location='product.aspx?id=<%# Eval("PostId") %>';">
                            <div class="product_img"
                                style="background-image: url('upload/<%# Eval("PicFileName") %>')">
                                <img src="imgs/common/now_kill.png" />
                            </div>
                            <div class="product_info">
                                <div class="product_name"><%# Eval("Title") %></div>
                                <div class="product_price"><%# Eval("SellPrice") %></div>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>                                
            </ul>
            <div class="contant_title contant_title_hot font_title">熱門商品</div>
            <ul class="product_list">
                <asp:Repeater ID="RepeaterHotProdct" runat="server">
                    <ItemTemplate>
                        <li class="product_br" style="cursor:pointer;" onclick="window.location='product.aspx?id=<%# Eval("PostId") %>';">
                            <div class="product_img"
                                style="background-image: url('upload/<%# Eval("PicFileName") %>')">
                                <img src="imgs/common/now_kill.png" />
                            </div>
                            <div class="product_info">
                                <div class="product_name"><%# Eval("Title") %></div>
                                <div class="product_price"><%# Eval("SellPrice") %></div>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>

        </div>

        <div class="contant_right">
            <asp:Literal ID="ltlRight" runat="server"></asp:Literal>
            <iframe src="https://www.facebook.com/plugins/page.php?href=https%3A%2F%2Fwww.facebook.com%2F%E5%93%81%E8%AE%9A%E8%A1%8C%E5%8B%95%E9%80%9A%E8%A8%8A-1743733255899753%2F&tabs=timeline&width=300&height=370&small_header=false&adapt_container_width=true&hide_cover=false&show_facepile=true&appId=167136590020161" width="300" height="370" style="border: none; overflow: hidden" scrolling="no" frameborder="0" allowtransparency="true"></iframe>
        </div>
    </div>
    <!-- ################## Contant-->

    <!--Bottom ##################-->
    <div class="Bottom">
        <img class="thethree" src="imgs/thethree.png" alt="" />
    </div>
    <!-- ################## Bottom-->
</asp:Content>

