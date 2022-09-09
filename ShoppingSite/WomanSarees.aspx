<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="WomanSarees.aspx.cs" Inherits="ShoppingSite.WomanSarees" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Woman Sarees</title>
    <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.0/css/jquery.dataTables.css" />  
        <script type="text/javascript" charset="utf8" src="//code.jquery.com/jquery-1.10.2.min.js"></script>  
        <script type="text/javascript" charset="utf8" src="//cdn.datatables.net/1.10.0/js/jquery.dataTables.js"></script>
    <link href="css/mystyle.css" runat="server" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
  <h2>Woman Sarees</h2>  

    <div class="panel panel-primary">
      <div class="panel-heading">Woman Sarees</div>
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
              <img src="Images/ProductImages/<%# Eval("PID") %>/<%# Eval("ImageName") %><%# Eval("Extension") %>" alt="<%# Eval("ImageName") %>"/>
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
      <div class="panel-footer">  </div>
    </div>
    
</div>

</asp:Content>
