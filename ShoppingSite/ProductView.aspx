<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="ProductView.aspx.cs" Inherits="ShoppingSite.ProductView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br /><br /><br />

    <button id="btnCart2" runat="server" class="btn btn-primary navbar-btn pull-right" onserverclick="btnCart2_ServerClick" type="button">
                        Cart <span id="CartBadge" runat="server" class="badge">0</span>
    </button>
    <br />

    <div style="padding-top:50px">

        <!--- Success Alert --->
        <div id="divSuccess" runat="server" class="alert alert-success alert-dismissible fade in h4">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <strong>Success! </strong>Item successfully added to cart. <a href="Cart.aspx">View Cart</a>
        </div>

        <div class="col-md-5 thumbnail">
            <div class="box" style="max-width:40%">
                <!------------------------------------- Product Image Slider Start ------------------------------------>

                <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
  <!-- Indicators -->
  <ol class="carousel-indicators">
    <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
    <li data-target="#carousel-example-generic" data-slide-to="1"></li>
    <li data-target="#carousel-example-generic" data-slide-to="2"></li>
      <li data-target="#carousel-example-generic" data-slide-to="3"></li>
      <li data-target="#carousel-example-generic" data-slide-to="4"></li>
  </ol>

  <!-- Wrapper for slides -->
  <div class="carousel-inner" role="listbox">

      <asp:Repeater ID="rptrImage" runat="server">
          <ItemTemplate>
           <div class="item <%# GetActiveImgClass(Container.ItemIndex) %>">
                <img src="Images/ProductImages/<%# Eval("PID") %>/<%# Eval("Name") %><%# Eval("Extension") %>" alt="<%# Eval("Name") %>" onerror="this.src='Images/NoImg.jpeg'"/>
            
           </div>
          </ItemTemplate>
      </asp:Repeater>

  </div>

  <!-- Controls -->
  <a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
    <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
    <span class="sr-only">Previous</span>
  </a>
  <a class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">
    <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
    <span class="sr-only">Next</span>
  </a>
</div>

                <!---------------------------------- Product Image Slider End ------------------------------------------->
            </div>
        </div>
        <div class="col-md-5">

            <asp:Repeater ID="rptrProductDetails" runat="server" OnItemDataBound="rptrProductDetails_ItemDataBound" 
                OnItemCommand="rptrProductDetails_ItemCommand">
            <ItemTemplate>

            <div class="div-det1">
                <h1 class="pro-name-view"><%# Eval("PName") %></h1>
                <span class="pro-og-price-view">Rs. <%# Eval("PPrice") %></span><span class="pro-price-discount-view">(<%# string.Format("{0}",Convert.ToInt64(Eval("PPrice"))-Convert.ToInt64(Eval("PSellPrice"))) %>OFF)</span>
                <p class="pro-price-view">Rs. <%# Eval("PSellPrice") %></p>
            </div>
            <div>
                <h5 class="size">SIZE</h5>
                <div>
                    <asp:RadioButtonList CssClass="radio-btn-items" ID="rblSize" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="S" Text="S"></asp:ListItem>
                        <asp:ListItem Value="M" Text="M"></asp:ListItem>
                        <asp:ListItem Value="L" Text="L"></asp:ListItem>
                        <asp:ListItem Value="XL" Text="XL"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="divDet1">
                <asp:Button ID="btnAddToCart" CssClass="main-button" runat="server" Text="Add To Cart" onClick="btnAddToCart_Click"/>
                <asp:Label ID="lblError" CssClass="text-danger" runat="server"></asp:Label>
            </div>
            <div class="div-det1">
                <h5 class="size">Description</h5>
                <p><%# Eval("PDescription") %></p>

                <h5 class="size">Product Details</h5>
                <p><%# Eval("PProductDetails") %></p>

            </div>

            <div>
                <p><%# ((int)Eval("FreeDelivery")==1)? "Free Delivery":"" %></p>
                <p><%# ((int)Eval("30DayRet")==1)? "30 Days Return":"" %></p>
                <p><%# ((int)Eval("COD")==1)? "Cash on Delivery":"" %></p>

            </div>

        </ItemTemplate>
        </asp:Repeater>

            <asp:HiddenField ID="hfCatID" runat="server" Value='<%# Eval("PCategoryID") %>' />
            <asp:HiddenField ID="hfSubCatID" runat="server" Value='<%# Eval("PSubCatID") %>' />
            <asp:HiddenField ID="hfGenderID" runat="server" Value='<%# Eval("PGender") %>' />
            <asp:HiddenField ID="hfBrandID" runat="server" Value='<%# Eval("PBrandID") %>' />

        </div>
    </div>

</asp:Content>
