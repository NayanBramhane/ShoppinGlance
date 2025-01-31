﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ShoppingSite.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ShoppinGlance</title>

    <script src="https://code.jquery.com/jquery-3.5.1.js" integrity="sha256-QWo7LDvxbWT2tbbQ97B53yJnYU3WhH/C8ycbRAkjPDc=" crossorigin="anonymous"></script>

    <meta charset="utf-8" >
    <meta name="viewport" content="width=device-width, initial-scale=1" >
    <meta http-equiv="X-UA-Compatible" content="IE-edge" >

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

    <link runat="server" rel="stylesheet" type="text/css" target="_blank" href="css/mystyle.css">

    <script type="text/javascript" src="js/btnCart.js"></script>

</head>
<body style="margin:0;padding:0">
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
                                <img src="icons/ShoppinGlance.png" alt="Shoppinglance" height="30" />
                            </span>Shoppinglance
                        </a>

                    </div>
                    <div class="navbar-collapse collapse">
                        <ul class="nav navbar-nav navbar-right">
                            <li class="active"><a href="Default.aspx">Home</a></li>
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
                                    <li><a href="WomanLegging.aspx">Legging</a></li>
                                    <li><a href="WomanSarees.aspx">Sarees</a></li>

                                </ul>
                            </li>
                            <li>
                                <button runat="server" id="btnCart" class="btn btn-primary navbar-btn" type="button">
                                    
                                    Cart<span class="badge" id="pCount" runat="server"></span>

                                </button>

                            </li>
                            <li id="btnSignUp" runat="server"><a href="SignUp.aspx">SignUp</a></li>
                            <li id="btnSignIn" runat="server"><a href="SignIn.aspx">SignIn</a></li>
                            <li>
                                <asp:Button ID="btnlogout" CssClass="btn btn-default navbar-btn" runat="server" Text="Sign Out" OnClick="btnlogout_Click"/>
                            </li>
                        </ul>
                    </div>

                </div>
            </div>

            <!--Image Slider-->

            <div class="container"> 
                <div id="myCarousel" class="carousel slide" data-ride="carousel">
                    <!-- Indicators -->
                    <ol class="carousel-indicators">
                        <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                        <li data-target="#myCarousel" data-slide-to="1"></li>
                        <li data-target="#myCarousel" data-slide-to="2"></li>
                        <li data-target="#myCarousel" data-slide-to="3"></li>
                        <li data-target="#myCarousel" data-slide-to="4"></li>
                    </ol>
                    
                    <!-- Wrapper for slides -->
                    <div class="carousel-inner">

                        <div class="item active">
                            
                            <img src="ImageSlider/Suit%202.jpg" alt="Suit 2"/>

                            <div class="carousel-caption">
                                <h3>Fashionable</h3>
                                <p>Modernize Your Life</p>
                                <p><a class="btn btn-lg btn-primary" href="Products.aspx" role="button">Buy Now</a></p>
                            </div>
                        </div>

                        <div class="item">
                            
                            <img src="ImageSlider/Girl%202.jpg" alt="Girl 2"/>
                                
                            <div class="carousel-caption">
                                <h3>Stylish</h3>
                                <p>95% OFF On Ladies Top</p>
                                <p><a class="btn btn-lg btn-primary" href="Products.aspx" role="button">Buy Now</a></p>
                            </div>
                        </div>

                        <div class="item">
                            
                            <img src="ImageSlider/Shoes.jpg" alt="Shoes"/>
                                
                            <div class="carousel-caption">
                                <h3>Contemporary</h3>
                                <p>70% Discount On Shoes</p>
                                <p><a class="btn btn-lg btn-primary" href="Products.aspx" role="button">Buy Now</a></p>
                            </div>
                        </div>

                        <div class="item">
                            
                            <img src="ImageSlider/Suit.jpg" alt="Shoes"/>
                                
                            <div class="carousel-caption">
                                <h3>Classic</h3>
                                <p>90% Discount On Suits</p>
                                <p><a class="btn btn-lg btn-primary" href="Products.aspx" role="button">Buy Now</a></p>
                            </div>
                        </div>

                        <div class="item">
                            
                            <img src="ImageSlider/Girl.jpg" alt="Shoes"/>
                                
                            <div class="carousel-caption">
                                <h3>Exotic</h3>
                                <p>Live like a Queen</p>
                                <p><a class="btn btn-lg btn-primary" href="Products.aspx" role="button">Buy Now</a></p>
                            </div>
                        </div>

                    </div>
                    
                    <!-- Left and right controls -->
                    <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                        <span class="glyphicon glyphicon-chevron-left"></span>
                        <span class="sr-only">Previous</span>

                    </a>
                    <a class="right carousel-control" href="#myCarousel" data-slide="next">
                        <span class="glyphicon glyphicon-chevron-right"></span>
                        <span class="sr-only">Next</span>

                    </a>

                </div>

            </div>

            <!--Image Slider End-->


        </div>

        <!--Middle Content-->

        <hr  style="clear: both; visibility: hidden;"/>
        <div class="container center">
            <div class="row">
                <div class="col-lg-4">
                    <img class="img-circle" src="CircularImages/Mobiles-circle.png" alt="thumb" width="140" height="140" />
                    <h2>Mobiles</h2>
                    <p>Buy Smartphones from Wide range of smartphones from popular brands like Samsung, Apple, Micromax, 
                        Motorola, HTC & more.
                        With the Pixel Camera that captures the moment just how you experienced it. 
                        Buy now. Get complimentary 3 months of YouTube Premium & Google One subscription. 
                        T&C apply. Dual rear camera. Fingerprint Unlock. With IP67 protection. Adaptive Battery. 
                        Titan M2™ chip.
                    </p>
                    <p><a class="btn btn-default" href="#" role="button">View More &raquo;</a></p>

                </div>
                <div class="col-lg-4">
                    <img class="img-circle" src="CircularImages/Shoes-circle.jpg" alt="thumb" width="140" height="140" />
                    <h2>Footwear</h2>
                    <p>Nike shoes are a popular brand of trendy shoes that offer a relaxed feel with style. 
                        The recognised brand is known for its variety of footwear options for men, women, 
                        and children for sports and casual wear. Find a range of sneakers, flip flops, slides, 
                        walking shoes, sandals, etc., in casual footwear. Sport based shoes are crafted by the 
                        brand for basketball, football, tennis, running, training and gym, motorsport, cricket, etc. 
                        Court Lite 2 Hard Court Tennis Shoes, Mercurial Superfly 7 Club TF Artificial Turf Soccer Shoes, 
                        Fly By Mid Basketball Shoes, Domain 2 Cricket Shoes, Legend Essential 2 Training and Gym Shoes, 
                        etc. are some models in the sport based shoes.</p>
                    <p><a class="btn btn-default" href="#" role="button">View More &raquo;</a></p>

                </div>
                <div class="col-lg-4">
                    <img class="img-circle" src="CircularImages/Dress-circle.jpeg" alt="thumb" width="140" height="140" />
                    <h2>Clothings</h2>
                    <p>Women's Rayon Full Sleeve A-Line Knee-Length Western Dresses for Women or Girls · 
                        Women's Cotton A-Line 
                        Stylish collared dresses for fashion-conscious women
                        AND Women's Nylon Fit Flare Above The Knee Dress ... 
                        AND Women's Synthetic Knee-Length A-Line Dress...</p>
                    <p><a class="btn btn-default" href="#" role="button">View More &raquo;</a></p>

                </div>
            </div>

            <div class="panel panel-primary">
            <div class="panel-heading">
                BLACK FRIDAY DEAL</div>
            <div class="panel-body">
                <div class="row" style="padding-top: 50px">
                    <asp:Repeater ID="rptrProducts" runat="server">
                        <ItemTemplate>
                            <div class="col-sm-3 col-md-3">
                                <a href="ProductView.aspx?PID=<%# Eval("PID") %>" style="text-decoration: none;">
                                    <div class="thumbnail">
                                        <img src="Images/ProductImages/<%# Eval("PID") %>/<%# Eval("ImageName") %><%# Eval("Extension") %>"
                                            alt="<%# Eval("ImageName") %>" />
                                        <div class="caption">
                                            <div class="pro-brand">
                                                <%# Eval ("BrandName") %>
                                            </div>
                                            <div class="pro-name">
                                                <%# Eval ("PName") %>
                                            </div>
                                            <div class="pro-price">
                                                <span class="pro-og-price">
                                                    <%# Eval ("PPrice","{0:0,00}") %>
                                                </span>
                                                <%# Eval ("PSellPrice","{0:c}") %>
                                                <span class="pro-price-discount">(<%# Eval("DiscAmount","{0:0,00}") %>
                                                    off) </span>
                                            </div>
                                        </div>
                                    </div>
                                </a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="panel-footer">
                Buy 50 mobiles and get a gift card</div>
        </div>

        </div>

        <!--Middle Content End-->

        <!--Footer Content Start-->
        <hr />
        <footer>
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
