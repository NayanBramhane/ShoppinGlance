<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AddBrand.aspx.cs" Inherits="ShoppingSite.AddBrand" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
            <div class="form-horizontal">
                <br /><br /><br />
                <h2>Add Brand</h2>
                <hr />
                <div class="form-group">
                    <asp:Label ID="Label1" CssClass="col-md-2 control-label" runat="server" Text="Brand Name"></asp:Label>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtBrand" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorBrandName" runat="server" CssClass="text-danger" 
                            ErrorMessage="Enter Brand name" ControlToValidate="txtBrand" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>

                

               

                <div class="form-group">
                    <div class="col-md-2"></div>
                    <div class="col-md-6">
                        <asp:Button ID="btnAddBrand" CssClass="btn btn-success" runat="server" Text="Add" OnClick="btnAddBrand_Click"/>
                    </div>
                </div>

                

                

            </div>

        <h1>Brand</h1>
        <hr />
        <div class="panel panel-default">

            <div class="panel-heading">All Brands</div>

            <asp:Repeater ID="rptrBrand" runat="server">

                <HeaderTemplate>

                    <table class="table">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Brand</th>
                                <%--<th>Edit</th>--%>
                            </tr>
                        </thead>
                        
                        <tbody>

                </HeaderTemplate>

                <ItemTemplate>

                            <tr>
                                <th> <%# Eval("BrandID") %> </th>
                                <td> <%# Eval("Name") %> </td>
                                <%--<td>Edit</td>--%>
                            </tr>

                </ItemTemplate>

                <FooterTemplate>

                        </tbody>
                    </table>

                </FooterTemplate>

            </asp:Repeater>

            
                    
                

        </div>

        </div>

</asp:Content>
