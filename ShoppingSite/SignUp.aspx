<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="ShoppingSite.SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SignUp</title>
    <meta charset="utf-8" >
    <meta name="viewport" content="width=device-width, initial-scale=1" >
    <meta http-equiv="X-UA-Compatible" content="IE-edge" >

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

    <link rel="stylesheet" type="text/css" target="_blank" href="css/mystyle.css">

</head>
<body>
    <form id="form1" runat="server">
        <div>

            <div class="navbar navbar-default navbar-fixed-top" role="navigation">
                <div class="container">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                            <span class="sr-only">Toggle Navigation</span>
                            <span class="icon-bar"></span>
                        </button>
                        <a class="navbar-brand" href="Default.aspx">
                            <span>
                                <img src="icons/setting.png" alt="Shoppinglance" height="30" />
                            </span>Shoppinglance
                        </a>

                    </div>
                    <div class="navbar-collapse collapse">
                        <ul class="nav navbar-nav navbar-right">
                            <li><a href="Default.aspx">Home</a></li>
                            <li><a href="#">About</a></li>
                            <li><a href="#">Contact</a></li>
                            <li><a href="#">Blog</a></li>
                            <li class="dropdown">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">Products<b class="caret"></b></a>
                                <ul class="dropdown-menu">
                                    <li class="dropdown-header">Men</li>
                                    <li role="separator" class="divider"></li>
                                    <li><a href="#">Shirts</a></li>
                                    <li><a href="#">Pants</a></li>
                                    <li><a href="#">Denims</a></li>
                                    <li role="separator" class="divider"></li>
                                    <li class="dropdown-header">Women</li>
                                    <li role="separator" class="divider"></li>
                                    <li><a href="#">Top</a></li>
                                    <li><a href="#">Leggings</a></li>
                                    <li><a href="#">Denims</a></li>

                                </ul>
                            </li>
                            <li class="active"><a href="SignUp.aspx">SignUp</a></li>
                            <li><a href="SignIn.aspx">SignIn</a></li>
                        </ul>
                    </div>

                </div>
            </div>

        </div>

        <!--SignUp Page Start-->

        <div class="center-page">
            <label class="col-xs-11">Username:</label>
            <div class="col-xs-11">
                <asp:TextBox ID="txtUname" runat="server" class="form-control" placeholder="Enter Your Username"></asp:TextBox>
            </div>

            <label class="col-xs-11">Enter Your Full Name:</label>
            <div class="col-xs-11">
                <asp:TextBox ID="txtName" runat="server" class="form-control" placeholder="Enter Your Name"></asp:TextBox>
            </div>

            <label class="col-xs-11">Password:</label>
            <div class="col-xs-11">
                <asp:TextBox ID="txtPass" runat="server" class="form-control" TextMode="Password" placeholder="Enter Your Password"></asp:TextBox>
            </div>

            <label class="col-xs-11">Confirm Password:</label>
            <div class="col-xs-11">
                <asp:TextBox ID="txtConPass" runat="server" class="form-control" TextMode="Password" placeholder="Enter Your Password Again"></asp:TextBox>
            </div>

            <label class="col-xs-11">E-mail:</label>
            <div class="col-xs-11">
                <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Enter Your E-mail" TextMode="Email"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ErrorMessage="Enter valid E-mail address" 
                    ControlToValidate="txtEmail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                </asp:RegularExpressionValidator>
            </div>

            <label class="col-xs-11"></label>
            <div class="col-xs-11">
                <asp:Button ID="txtsignup" class="btn btn-success" runat="server" Text="Sign Up" OnClick="txtsignup_Click" />
            </div>
        </div>

        <!--SignUp Page End-->

        <!--Footer Content Start-->
        <hr />
        <footer class="footer-pos">
            <div class="container">
                <p class="pull-right">
                    <a href="#">Back to Top</a>
                </p>
                <p>&copy;2022 Nayan Bramhane &middot;
                    <a href="Default.aspx">Home</a> &middot;
                    <a href="#">About</a> &middot;
                    <a href="#">Contact</a> &middot;
                    <a href="#">Products</a>
                </p>
            </div>
        </footer>

        <!--Footer Content End-->
    </form>
</body>
</html>
