﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecoverPassword.aspx.cs" Inherits="ShoppingSite.RecoverPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password</title>
    <meta charset="utf-8" >
    <meta name="viewport" content="width=device-width, initial-scale=1" >
    <meta http-equiv="X-UA-Compatible" content="IE-edge" >

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

    <link rel="stylesheet" type="text/css" target="_blank" href="css/mystyle.css">

</head>
<body id="BackgroundImg"><%--style="background-color:burlywood"--%>
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
                                <a href="Products.aspx" class="dropdown-toggle" data-toggle="dropdown">Products<b class="caret"></b></a>
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
                            <li><a href="SignIn.aspx">SignIn</a></li>
                        </ul>
                    </div>

                </div>
            </div>

        </div>

        <div class="container">
            <div class="form-horizontal">
                <br />
                <br />
                
                <h2>Reset Password</h2>
                <hr  style="clear: both; visibility: hidden;"/>
                <h3></h3>
                <div class="form-group">
                    <asp:Label ID="lblMsg" CssClass="col-md-2 control-label" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="Red">
                    </asp:Label>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblNewPassword" CssClass="col-md-2 control-label" runat="server" Text="Enter your new password" Visible="False">
                    </asp:Label>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtNewPass" CssClass="form-control" TextMode="Password" runat="server" Visible="False"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNewPass" CssClass="text-danger" runat="server" 
                            ErrorMessage="Enter your new password" ControlToValidate="txtNewPass" ForeColor="Red" Visible="False">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblConfirmPass" CssClass="col-md-2 control-label" placeholder="Enter your new password" 
                        runat="server" Text="Confirm your new password" Visible="False"></asp:Label> 
                    <div class="col-md-3">
                        <asp:TextBox ID="txtConfirmPass" CssClass="form-control" placeholder="Enter your password again" 
                            TextMode="Password" runat="server" Visible="False"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorConfirmPass" CssClass="text-danger" runat="server" 
                            ErrorMessage="Enter correct password" ControlToValidate="txtConfirmPass" ForeColor="Red" Visible="False">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidatorConfirmNewPass" CssClass="text-danger" runat="server" 
                            ErrorMessage="Password doesn't match...  Please try again" ForeColor="Red" Visible="False" 
                            ControlToCompare="txtConfirmPass" ControlToValidate="txtNewPass"></asp:CompareValidator>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-md-2"> </div>
                    <div class="col-md-6">
                        <asp:Button ID="btnResetPass" CssClass="btn btn-default" runat="server" Text="Reset" Visible="False" OnClick="btnResetPass_Click"/>
                    </div>
                </div>
            </div>
        </div>

    </form>

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

</body>
</html>
