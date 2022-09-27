<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="ShoppingSite.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Products</title>
    <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.0/css/jquery.dataTables.css" />  
        <script type="text/javascript" charset="utf8" src="//code.jquery.com/jquery-1.10.2.min.js"></script>  
        <script type="text/javascript" charset="utf8" src="//cdn.datatables.net/1.10.0/js/jquery.dataTables.js"></script>
    <link href="css/mystyle.css" runat="server" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br /><br /><br />
      <div class="row">
      <div class="col-md-12">
          <%--<button id="btnCart2" runat="server" class="btn btn-primary navbar-btn pull-right" onserverclick="btnCart2_ServerClick" type="button">
              Cart <span id="CartBadge" runat="server" class="badge"> 0 </span>
          </button>--%>
          <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary navbar-btn pull-right" runat="server" OnClick="LinkButton1_Click">
              Cart <span id="CartBadge" runat="server" class="badge">0</span>
          </asp:LinkButton>

                    <h3>
                        <asp:Label ID="Label1" runat="server" Text="Showing All Products"></asp:Label>
                    </h3>
                    <hr />
                    
      </div>
    </div>

    <div class="row" style="padding-top:50px">
        
        <asp:TextBox ID="txtFilterGrid1Record" CssClass="form-control" runat="server" placeholder="Search Products...." AutoPostBack="true" 
              ontextchanged="txtFilterGrid1Record_TextChanged" ></asp:TextBox>
        <br />
        <hr />

        <asp:Repeater ID="rptrProducts" runat="server">
            <ItemTemplate>

                <div class="col-sm-3 col-md-3">
                   <a href="ProductView.aspx?PID=<%# Eval("PID") %>" >
                    
                    <div class="thumbnail" style="border: 2px solid rebeccapurple;">
                        <img src="Images/ProductImages/<%# Eval("PID") %>/<%# Eval("ImageName") %><%# Eval("Extension") %>" alt="<%# Eval("ImageName") %>"/>
                        <div class="caption">
                            <div class="pro-brand"><%# Eval("BrandName") %></div>
                            <div class="pro-name"><%# Eval("PName") %></div>
                            <div class="pro-price">
                                <span class="pro-og-price">Rs.&nbsp;<%# Eval("PPrice") %></span><br />Rs.&nbsp;<%# Eval("PSellPrice") %><span id="pro-price-discount">(<%# Eval("DiscAmount") %>)OFF</span>
                            </div>
                        </div>
                    </div>
                   </a>
                </div>

            </ItemTemplate>
        </asp:Repeater>

    </div>

</asp:Content>
