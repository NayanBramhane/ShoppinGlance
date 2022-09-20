<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ShoppingSite.Contact" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contact</title>

    <meta charset="utf-8" >
    <meta name="viewport" content="width=device-width, initial-scale=1" >
    <meta http-equiv="X-UA-Compatible" content="IE-edge" >

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

    <link rel="stylesheet" type="text/css" target="_blank" href="css/mystyle.css">

</head>
<body style="background-image: url('BackgroundImage/polygon-img.png'); background-repeat: no-repeat; background-attachment: fixed; 
background-size: cover;">
    <form id="form1" runat="server">
        <br /><br /><br />
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
                            <%--<li>
                                <button runat="server" id="btnCart" class="btn btn-primary navbar-btn" type="button">
                                    
                                    Cart<span class="badge" id="pCount" runat="server"></span>

                                </button>

                            </li>
                            <li id="btnSignUp" runat="server"><a href="SignUp.aspx">SignUp</a></li>
                            <li id="btnSignIn" runat="server"><a href="SignIn.aspx">SignIn</a></li>
                            <li>
                                <asp:Button ID="btnlogout" CssClass="btn btn-default navbar-btn" runat="server" Text="Sign Out" OnClick="btnlogout_Click"/>
                            </li>--%>
                        </ul>
                    </div>

                </div>
            </div>

        </div>

        <!-- Contact page start-->
        <div class="center-page contact_form">
            <div class="row row-contact">
                <asp:Label ID="Label1" CssClass="required contact" runat="server" Text="Your name:"></asp:Label><br />
                <asp:TextBox ID="TextBox1" CssClass="text-area" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="text-danger" runat="server" ErrorMessage="Enter your name" 
                    ControlToValidate="TextBox1" ForeColor="Red"></asp:RequiredFieldValidator>

            </div>
        
            <div class="row row-contact">
                <asp:Label ID="Label2" CssClass="required contact" runat="server" Text="Your e-mail:"></asp:Label><br />
                <asp:TextBox ID="TextBox2" CssClass="text-area" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="text-danger" runat="server" ErrorMessage="Enter your e-mail" 
                    ControlToValidate="TextBox2" ForeColor="Red"></asp:RequiredFieldValidator>

            </div>
        
            <div class="row row-contact">
                <asp:Label ID="Label3" CssClass="required contact" runat="server" Text="Your message:"></asp:Label><br />
                <textarea id="TextArea1" class="text-area" runat="server" cols="20" rows="2"></textarea>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="text-danger" runat="server" ErrorMessage="Enter your message" 
                    ControlToValidate="TextArea1" ForeColor="Red"></asp:RequiredFieldValidator>

            </div>

            <asp:Button ID="Button1" CssClass="contact-button" runat="server" Text="Send email" OnClick="Button1_Click" />
            <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
        </div>
        <!-- Contact page start-->

        <!--Footer Content Start-->
        <hr style="clear: both; visibility: hidden;"/>
        <footer class="footer-pos">
            <div class="container">
                <p class="pull-right">
                    <a href="#">Back to Top</a>
                </p>
                <p">&copy;2022 Nayan Bramhane &middot;
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
