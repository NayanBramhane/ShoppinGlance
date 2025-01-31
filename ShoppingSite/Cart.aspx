﻿<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="ShoppingSite.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Cart</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <link href="css/mystyle.css" rel="stylesheet" runat="server"/>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br /><br />

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container">
                <br />
                <br />
                    <%--<button id="btnCart2" runat="server" class="btn btn-primary navbar-btn pull-right" onserverclick="btnCart2_ServerClick" type="button">
                        Cart <span id="CartBadge" runat="server" class="badge">0</span>
                    </button>--%>
                <asp:LinkButton ID="btnCart2" CssClass="btn btn-primary navbar-btn pull-right" runat="server" OnClick="btnCart2_Click">
                    Cart <span id="CartBadge" runat="server" class="badge">0</span>
                </asp:LinkButton>

    <div style="padding-bottom:50px">


        <div class="col-md-9">
            <h4 class="pro-name-view-cart" runat="server" id="h4NoItems"></h4>
            <div id="divQtyError" runat="server" class="alert alert-success alert-dismissible fade in h4">
                <a href="#" class="close" data-dismiss="alert" aria-label="close"> &times;</a>
                <strong>Oops! </strong>Quantity cannot be less than 1.
            </div>

            <asp:Repeater ID="rptrCartProducts" OnItemCommand="rptrCartProducts_ItemCommand" runat="server">
                <ItemTemplate>

            <%--Show Cart Details Start--%>

            <div class="media" style="border:1px solid black; background-color:#FBF2CF">
                <div class="media-left">
                    <a href="ProductView.aspx?PID=<%# Eval("PID") %>" target="_blank">
                        <img class="media-object" width="100" src="Images/ProductImages/<%# Eval("PID") %>/<%# Eval("Name") %><%# Eval("Extension") %>" alt="<%# Eval("Name") %>" onerror="this.src='Images/NoImg.jpeg'"/>
                    </a>
                </div>
                <div class="media-body">
                    <h4 class="media-heading pro-name-view-cart"><%# Eval("PName") %></h4>
                    <%--<p class="pro-price-discount-view">Size : <%# Eval("PSizeName") %></p>--%>
                    <span class="pro-price-view">Rs.&nbsp; <%# Eval("PSellPrice","{0:0.00}") %></span>
                    <span class="pro-og-price-view">Rs.&nbsp; <%# Eval("PPrice","{0:0.00}") %></span>

                    <span class="pro-price-discount-view"> Off Rs.<%# string.Format("{0}",Convert.ToInt64(Eval("PPrice"))-Convert.ToInt64(Eval("PSellPrice"))) %></span>

                    <div class="pull-right form-inline">
                        <asp:Label ID="lblQty" runat="server" Text="Qty:" Font-Size="Large"></asp:Label>
                        <asp:Button ID="BtnMinus" CommandArgument='<%# Eval("PID") %>' CommandName="DoMinus" Font-Size="Large" 
                            runat="server" Text="-" />&nbsp;
                        <asp:TextBox ID="txtQty" runat="server" Width="40" Font-Size="Large" TextMode="SingleLine" 
                            Style="text-align: center" Text='<%# Eval("Qty") %>'></asp:TextBox>&nbsp;
                        <asp:Button ID="BtnPlus" CommandArgument='<%# Eval("PID") %>' CommandName="DoPlus" runat="server" 
                            Text="+" Font-Size="Large" />&nbsp;&nbsp;&nbsp;
                    </div>
                    <br />

                    <p>
                        <asp:Button CommandArgument='<%#Eval("CartID") %>' CommandName="RemoveThisCart" ID="btnRemoveCart" 
                            CssClass="remove-cart-button" runat="server" Text="Remove"/>
                        <br />
                        <span class="pro-name-view-cart pull-right">SubTotal: Rs.&nbsp; <%# Eval("SubSAmount","{0:0.00}") %></span>
                    </p>
                </div>
            </div>

            </ItemTemplate>
            </asp:Repeater>
            <%--Show Cart Details End--%>

        </div>

        <%--<div class="col-md-3" runat="server" id="divpricedetails">

            <%----------------------------------------------------------------------------------%>

  <%--          <div>

                <h5>Price Details</h5>

                <div>
                    <label>Cart Total</label>
                    <span class="pull-right price-gray" runat="server" id="spanCartTotal">Rs. </span>
                </div>

                <div>
                    <label>Cart Discount</label>
                    <span class="pull-right price-green" id="spanDiscount" runat="server">Rs. </span>
                </div>

            </div>              --%>

            <%----------------------------------------------------------------------------------%>

 <%--           <div>
                <div class="pro-price-view">
                    <label>Cart Total</label>
                    <span class="pull-right" runat="server" id="spanTotal">Rs. </span>
                </div>

                <div>
                    <asp:Button ID="btnBuy" CssClass="buy-now-btn" runat="server" Text="Buy Now" OnClick="btnBuy_Click"/>
                </div>
            </div>

        </div>--%>

         <div class="col-md-3" runat="server" id="divAmountSect">

             <div>

                 <h5 class="pro-name-view-cart">Price Details</h5>
                 <div>
                     <label class=" ">Total</label>
                     <span class="pull-right priceGray" runat="server" id="spanCartTotal"></span>

                 </div>

                 <div>
                     <label class=" ">Cart Discount</label>
                     <span class="pull-right priceGreen" runat="server" id="spanDiscount"></span>

                 </div>

             </div>

             <div>

                 <div class="cartTotal">

                     <label>Cart Total</label>
                     <span class="pull-right " runat="server" id="spanTotal"></span>

                     <div>
                         <asp:Button ID="btnBuyNow" CssClass="buy-now-btn" runat="server" OnClick="btnBuyNow_Click" Text="Buy Now" />
                     </div>

                 </div>
             </div>

         </div>

    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
