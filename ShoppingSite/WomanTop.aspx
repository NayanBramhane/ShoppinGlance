<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="WomanTop.aspx.cs" Inherits="ShoppingSite.WomanTop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Woman Top</title>
    <link href="css/mystyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container">
  <h2>Woman's Top</h2>  

    <div class="panel panel-primary">
      <div class="panel-heading">Shirts</div>
      <div class="panel-body">

          <asp:TextBox ID="txtFilterGrid1Record" CssClass="form-control" runat="server" 
              placeholder="Search Products...." AutoPostBack="true" 
              ontextchanged="txtFilterGrid1Record_TextChanged" ></asp:TextBox>
      <br />
      <hr />
      <asp:repeater ID="rptrProducts" runat="server">
           <ItemTemplate>
        <div class="col-sm-3 col-md-3">
            <a href="ProductView.aspx?PID=<%# Eval("PID") %>" style="text-decoration:none;">
          <div class="thumbnail">              
              <img src="Images/ProductImages/<%# Eval("PID") %>/<%# Eval("ImageName") %><%# Eval("Extention") %>" alt="<%# Eval("ImageName") %>"/>
              <div class="caption"> 
                   <div class="pro-brand"><%# Eval ("BrandName") %>  </div>
                   <div class="pro-name"> <%# Eval ("PName") %> </div>
                   <div class="pro-price"> <span class="pro-og-price" > <%# Eval ("PPrice","{0:0,00}") %> </span> <%# Eval ("PSellPrice","{0:c}") %> <span class="pro-price-discount"> (<%# Eval("DiscAmount","{0:0,00}") %>off) </span></div> 
                   
              </div>
          </div>
                </a>
        </div>
               
               </ItemTemplate>
       </asp:repeater>
      
      </div>
      <div class="panel-footer">
      </div>
    </div>
    
</div>

</asp:Content>
