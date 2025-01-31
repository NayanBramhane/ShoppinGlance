﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserHome.aspx.cs" Inherits="ShoppingSite.UserHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Home Page</title>

    <script src="https://code.jquery.com/jquery-3.5.1.js" integrity="sha256-QWo7LDvxbWT2tbbQ97B53yJnYU3WhH/C8ycbRAkjPDc=" crossorigin="anonymous"></script>

    <meta charset="utf-8" >
    <meta name="viewport" content="width=device-width, initial-scale=1" >
    <meta http-equiv="X-UA-Compatible" content="IE-edge" >

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

    <script>

        $(document).ready(function myfunction() {
            $("#btnCart").click(function myfunction() {
                window.location.href = "Cart.aspx";
            });
        });

    </script>

    <link rel="stylesheet" type="text/css" target="_blank" href="css/mystyle.css">

</head>
<body  id="BackgroundImg"><%--style="background-color:burlywood"--%>
    <form id="form1" runat="server">

         <div>
            <div class="navbar navbar-default navbar-fixed-top" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                            <span class="sr-only">Toggle Navigation</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <a class="navbar-brand" href="Default.aspx">
                            <span>
                                <img src="icons/ShoppinGlance.png" alt="ShoppinGlance" height="30" />
                            </span>ShoppinGlance
                        </a>

                    </div>

                    <div class="navbar-collapse collapse">
                        <ul class="nav navbar-nav navbar-right">
                            <li><a href="Default.aspx">Home</a></li>
                            <li><a href="About.aspx">About</a></li>
                            <li><a href="Contact.aspx">Contact</a></li>
                            <%--<li><a href="#">Blog</a></li>--%>
                            <li class="dropdown">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">Products<b class="caret"></b></a>
                                <ul class="dropdown-menu">
                                    <li><a href="Products.aspx">All Products</a></li>
                                    <li role="separator" class="divider"></li>
                                    <li class="dropdown-header">Men</li>
                                    <li role="separator" class="divider"></li>
                                    <li><a href="ManShirt.aspx">Shirts</a></li>
                                    <li><a href="ManPants.aspx">Pants</a></li>
                                    <li><a href="ManDenims.aspx">Denims</a></li>
                                    <li role="separator" class="divider"></li>
                                    <li class="dropdown-header">Women</li>
                                    <li role="separator" class="divider"></li>
                                    <li><a href="WomanTop.aspx">Top</a></li>
                                    <li><a href="WomanLegging.aspx">Leggings</a></li>
                                    <li><a href="WomanSarees.aspx">Sarees</a></li>

                                </ul>
                            </li>
                            <li>
                                <button id="btnCart" class="btn btn-primary navbar-btn" type="button">
                                    
                                    Cart<span class="badge" id="pCount" runat="server"></span>

                                </button>

                            </li>
                            <li>
                                <asp:Button ID="btnLogin" CssClass="btn btn-default navbar-btn" runat="server" Text="Sign In" OnClick="btnLogin_Click"/>
                                <asp:Button ID="btnlogout" CssClass="btn btn-default navbar-btn" runat="server" Text="Sign Out" OnClick="btnlogout_Click"/>
                                
                            </li>
                            <li>
                                <asp:Button ID="Button1" CssClass ="btn btn-link navbar-btn " runat="server" Text=""  />
                            </li>
                        </ul>
                    </div>

                </div>
            </div>
        </div>
        <br /><br /><br />
        
        <asp:Label ID="lblSuccess" CssClass="text-success" runat="server"></asp:Label>

        <!-- Middle Content Starts -->
        <br /><br /><br />
        <div class="container center">
            <div class="row">

                <div class="col-lg-4 UH">
                    <img src="ProductImages/iphone-14-pro-storage-select-202209-6-7inch-deeppurple.jpg" alt="iphone" height="400" width="450"/>
                    <p><a class="btn btn-default" href="Products.aspx" role="button">View More &raquo;</a></p>
                </div>

                <div class="col-lg-4 UH">
                    <img src="ProductImages/JL03373-YGP9EB_1_lar.jpg" alt="Necklace"/>
                    <p><a class="btn btn-default" href="Products.aspx" role="button">View More &raquo;</a></p>
                </div>
                <div class="col-lg-4 UH">
                    <img id="third-image" src="ProductImages/Rolex-watch.png" alt="Rolex Watch"/>
                    <p><a class="btn btn-default" href="Products.aspx" role="button">View More &raquo;</a></p>
                </div>

            </div>
        </div>

        <!-- Middle Content Ends -->
        
        <!--Footer Content Start-->

        <hr  style="clear: both; visibility: hidden;"/>
        <footer class="footer-pos">
            <div class="container">
                <p class="pull-right">
                    <a href="#">Back to Top</a>
                </p>
                <p>&copy;2022 Nayan Bramhane &middot;
                    <a href="Default.aspx">Home</a> &middot;
                    <a href="About.aspx">About</a> &middot;
                    <a href="Contact.aspx">Contact</a> &middot;
                    <a href="Products.aspx">Products</a>
                </p>
            </div>
        </footer>

        <!--Footer Content End-->

    </form>
</body>
</html>
