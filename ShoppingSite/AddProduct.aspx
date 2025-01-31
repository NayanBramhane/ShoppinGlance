﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="ShoppingSite.AddProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--Script to check if at least one checkbox is selected starts--%>

    <script type="text/javascript">
        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById("<%=cblSize.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }
    </script>

    <%--Script ends--%>

    <div class="container">
        <div class="form-horizontal">

            <br /><br /><br />
            <h2>Add Product</h2>
            <hr />
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" CssClass="col-md-2 control-label" Text="Product Name"></asp:Label>
                <div class="col-md-3">

                    <asp:TextBox ID="txtProductName" CssClass="form-control" runat="server"></asp:TextBox>                    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtProductName" runat="server" CssClass ="text-danger" 
                            ErrorMessage="Enter Product Name" ControlToValidate="txtProductName" ForeColor="Red"></asp:RequiredFieldValidator>

                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label2" runat="server" CssClass="col-md-2 control-label" Text="Price"></asp:Label>
                <div class="col-md-3">

                    <asp:TextBox ID="txtPrice" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtPrice" runat="server" CssClass ="text-danger" 
                            ErrorMessage="Enter Price" ControlToValidate="txtPrice" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Enter Correct Price" CssClass ="text-danger"
                        ControlToValidate="txtPrice" ForeColor="Red" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label3" runat="server" CssClass="col-md-2 control-label" Text="Selling Price"></asp:Label>
                <div class="col-md-3">

                    <asp:TextBox ID="txtSellPrice" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtSellPrice" runat="server" CssClass ="text-danger" 
                            ErrorMessage="Enter Selling Price" ControlToValidate="txtSellPrice" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Enter Correct Price" CssClass ="text-danger"
                        ControlToValidate="txtSellPrice" ForeColor="Red" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                    <%--<asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Selling Price should be less than or equal to Original Price" 
                        ControlToCompare="txtPrice" ControlToValidate="txtSellPrice" ForeColor="Red" Operator="LessThan" CssClass ="text-danger"></asp:CompareValidator>--%>

                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label4" runat="server" CssClass="col-md-2 control-label" Text="Brand"></asp:Label>
                <div class="col-md-3">

                    <asp:DropDownList ID="ddlBrand" CssClass ="form-control" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlBrand" runat="server" CssClass ="text-danger" 
                            ErrorMessage="Select Brand" ControlToValidate="ddlBrand" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label5" runat="server" CssClass="col-md-2 control-label" Text="Category"></asp:Label>
                <div class="col-md-3">

                    <asp:DropDownList ID="ddlCategory" CssClass="form-control" autopostback="true" runat="server" 
                        OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlCategory" runat="server" CssClass ="text-danger" 
                            ErrorMessage="Select Category" ControlToValidate="ddlCategory" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label6" runat="server" CssClass="col-md-2 control-label" Text="Sub Category"></asp:Label>
                <div class="col-md-3">

                    <asp:DropDownList ID="ddlSubCat" CssClass ="form-control" runat="server" AutoPostBack="True" 
                        OnSelectedIndexChanged="ddlSubCat_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlSubCat" runat="server" CssClass ="text-danger" 
                            ErrorMessage="Select SubCategory" ControlToValidate="ddlSubCat" ForeColor="Red"></asp:RequiredFieldValidator>

                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label19" runat="server" CssClass="col-md-2 control-label" Text="Gender"></asp:Label>
                <div class="col-md-3">

                    <asp:DropDownList ID="ddlGender" CssClass ="form-control" runat="server" AutoPostBack="True" 
                        OnSelectedIndexChanged="ddlGender_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlGender" runat="server" CssClass ="text-danger" 
                            ErrorMessage="Select Gender" ControlToValidate="ddlGender" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label7" runat="server" CssClass="col-md-2 control-label" Text="Size"></asp:Label>
                <div class="col-md-3">

                    <asp:CheckBoxList ID="cblSize" CssClass="form-control" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                    <%----------------------Check if at least one checkbox is selected-------------------------%>
                    <asp:CustomValidator ID="CustomValidatorcblSize" runat="server" CssClass="text-danger" ErrorMessage="Select at least one checkbox" 
                        ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList"></asp:CustomValidator>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorcblSize" runat="server" CssClass ="text-danger" 
                            ErrorMessage="Select Size" ControlToValidate="cblSize" ForeColor="Red"></asp:RequiredFieldValidator>--%>

                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label20" runat="server" CssClass="col-md-2 control-label" Text="Quantity"></asp:Label>
                <div class="col-md-3">

                    <asp:TextBox ID="txtQuantity" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtQuantity" runat="server" CssClass ="text-danger" 
                            ErrorMessage="Enter Quantity" ControlToValidate="txtQuantity" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Enter whole numbers only" CssClass ="text-danger"
                        ControlToValidate="txtQuantity" ForeColor="Red" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>

                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label8" runat="server" CssClass="col-md-2 control-label" Text="Description"></asp:Label>
                <div class="col-md-3">

                    <asp:TextBox ID="txtDescription" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>

                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label9" runat="server" CssClass="col-md-2 control-label" Text="Product Details"></asp:Label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtPDetail" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>

                </div>
            </div>

<%--            <div class="form-group">
                <asp:Label ID="Label10" runat="server" CssClass="col-md-2 control-label" Text="Materials and Care"></asp:Label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtMatCare" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>

                </div>
            </div>          --%>

            <div class="form-group">
                <asp:Label ID="Label11" runat="server" CssClass="col-md-2 control-label" Text="Upload Image"></asp:Label>
                <div class="col-md-3">

                    <asp:FileUpload ID="fuImg01" CssClass="form-control" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorfuImg01" runat="server" CssClass ="text-danger" 
                            ErrorMessage="Upload one image in First File Upload field" ControlToValidate="fuImg01" ForeColor="Red">
                    </asp:RequiredFieldValidator>

                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label12" runat="server" CssClass="col-md-2 control-label" Text="Upload Image"></asp:Label>
                <div class="col-md-3">
                    <asp:FileUpload ID="fuImg02" CssClass="form-control" runat="server" />

                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label13" runat="server" CssClass="col-md-2 control-label" Text="Upload Image"></asp:Label>
                <div class="col-md-3">
                    <asp:FileUpload ID="fuImg03" CssClass="form-control" runat="server" />

                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label14" runat="server" CssClass="col-md-2 control-label" Text="Upload Image"></asp:Label>
                <div class="col-md-3">
                    <asp:FileUpload ID="fuImg04" CssClass="form-control" runat="server" />

                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label15" runat="server" CssClass="col-md-2 control-label" Text="Upload Image"></asp:Label>
                <div class="col-md-3">
                    <asp:FileUpload ID="fuImg05" CssClass="form-control" runat="server" />

                </div>
            </div>

             <div class="form-group">
                <asp:Label ID="Label16" runat="server" CssClass="col-md-2 control-label" Text="Free Delivery"></asp:Label>
                <div class="col-md-3">
                    <asp:CheckBox ID="chFD" runat="server" />

                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label17" runat="server" CssClass="col-md-2 control-label" Text="30 Days Return"></asp:Label>
                <div class="col-md-3">
                    <asp:CheckBox ID="ch30Ret" runat="server" />

                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label18" runat="server" CssClass="col-md-2 control-label" Text="Cash On Delivery"></asp:Label>
                <div class="col-md-3">
                    <asp:CheckBox ID="cbCOD" runat="server" />

                </div>
            </div>

            <div class="form-group">
                <div class="col-md-2"></div>
                <div class="col-md-6">
                    <asp:Button ID="btnAdd" CssClass="btn btn-success" runat="server" Text="Add Product" OnClick="btnAdd_Click" CausesValidation="true"/>
                </div>
            </div>

        </div>

    </div>

    <div class="container">

        <hr />
    <div class="panel panel-primary">
      <div class="panel-heading"><h2>Product Report</h2> </div>
        <div class="panel-body">
           <div class="col-md-12">
              <div class="form-group">
                <div class="table table-responsive">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table" AutoGenerateColumns="false">
                    <Columns>  
                        <asp:BoundField DataField="PID" HeaderText="S.No." />  
                        <asp:BoundField DataField="PName" HeaderText="PName" />  
                        <asp:BoundField DataField="PPrice" HeaderText="Price" />  
                        <asp:BoundField DataField="PSellPrice" HeaderText="SellPrice" />  
                        <asp:BoundField DataField="Brand" HeaderText="Brand" />  
                        <asp:BoundField DataField="CatName" HeaderText="Category" />  
                        <asp:BoundField DataField="SubCatName" HeaderText="SubCategory" />

                        <asp:BoundField DataField="gender" HeaderText="gender" />  
                        <asp:BoundField DataField="SizeName" HeaderText="SizeName" />  
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        
                        <%--<asp:TemplateField HeaderText="Photo">
                        <ItemTemplate>  
                        <img src="Images/ProductImages/<%# Eval("PID") %>/<%# Eval("ImageName") %><%# Eval("Extention") %>" alt="<%# Eval("ImageName") %>" style=" height:150px; width:150px;"/>
                        </ItemTemplate>  
                        </asp:TemplateField>--%>

                       <%-- <asp:CommandField ShowEditButton="true" />  
                        <asp:CommandField ShowDeleteButton="true" />--%>
                        
                         </Columns> 
                    </asp:GridView>
                </div>
              
              </div>
           
           </div>
      
      
        </div>
      <div class="panel-footer">Panel Footer</div>
    </div>

    </div>

</asp:Content>
