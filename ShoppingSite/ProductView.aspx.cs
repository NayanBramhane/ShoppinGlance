using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Globalization;
using System.Threading;

namespace ShoppingSite
{
    public partial class ProductView : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString;
        readonly Int32 myQty = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["PID"] != null)
            {
                if (!IsPostBack)
                {
                    BindProductImage();
                    BindProductDetails();
                    BindCartNumber();
                    divSuccess.Visible = false;
                }
            }
            else
            {
                Response.Redirect("~/Products.aspx");
            }
        }

        private void BindProductDetails()
        {
            Int64 PID = Convert.ToInt64(Request.QueryString["PID"]);
            using (MySqlConnection con = new MySqlConnection(CS))
            {
                MySqlCommand cmd = new MySqlCommand("SP_BindProductDetails", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("_PID", PID);
                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    rptrProductDetails.DataSource = dt;
                    rptrProductDetails.DataBind();
                    Session["CartPID"] = Convert.ToInt32(dt.Rows[0]["PID"].ToString());
                    Session["myPName"] = dt.Rows[0]["PName"].ToString();
                    Session["myPPrice"] = dt.Rows[0]["PPrice"].ToString();
                    Session["myPSellPrice"] = dt.Rows[0]["PSellPrice"].ToString();
                }
            }
        }

        //private void BindProductImage()
        //{
        //    Int64 PID = Convert.ToInt64(Request.QueryString["PID"]);
        //    using (MySqlConnection con = new MySqlConnection(CS))
        //    {
        //        using (MySqlCommand cmd = new MySqlCommand("Select * from tblProductImages where PID='"+PID+"'", con))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
        //            {
        //                DataTable dt = new DataTable();
        //                sda.Fill(dt);
        //                rptrImage.DataSource = dt;
        //                rptrImage.DataBind();
        //            }
        //        }
        //    }
        //}
        private void BindProductImage()
        {
            Int64 PID = Convert.ToInt64(Request.QueryString["PID"]);
            using (MySqlConnection con = new MySqlConnection(CS))
            {
                MySqlCommand cmd = new MySqlCommand("SP_BindProductImages", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("_PID", PID);
                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    rptrImage.DataSource = dt;
                    rptrImage.DataBind();
                }
            }
        }

        protected string GetActiveImgClass(int ItemIndex)
        {
            if (ItemIndex==0)
            {
                return "active";
            }
            else
            {
                return "";
            }
        }

        protected void rptrProductDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType==ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string BrandID = (e.Item.FindControl("hfBrandID") as HiddenField).Value;
                string CatID = (e.Item.FindControl("hfCatID") as HiddenField).Value;
                string SubCatID = (e.Item.FindControl("hfSubCatID") as HiddenField).Value;
                string GenderID = (e.Item.FindControl("hfGenderID") as HiddenField).Value;

                RadioButtonList rblSize = e.Item.FindControl("rblSize") as RadioButtonList;

                using (MySqlConnection con = new MySqlConnection(CS))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Select * from tblSizes where BrandID='" + BrandID + "' and CategoryID='"+ CatID + 
                        "' and SubCategoryID='"+SubCatID+"' and GenderID='"+ GenderID + "'", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            rblSize.DataSource = dt;
                            rblSize.DataTextField = "SizeName";
                            rblSize.DataValueField = "SizeID";
                            rblSize.DataBind();
                        }
                    }
                }
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            string SelectedSize = string.Empty;
            foreach (RepeaterItem item in rptrProductDetails.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var rbList = item.FindControl("rblSize") as RadioButtonList;
                    SelectedSize = rbList.SelectedValue;
                    var lblError = item.FindControl("lblError") as Label;
                    lblError.Text = "Please Select a Size";
                }
            }

            if (SelectedSize!="")
            {
                Int64 PID = Convert.ToInt64(Request.QueryString["PID"]);
                //if (Request.Cookies["CartPID"]!=null)
                //{
                //    string CookiePID = Request.Cookies["CartPID"].Value.Split('=')[1];
                //    CookiePID = CookiePID + "," + PID + "-" + SelectedSize;

                //    HttpCookie CartProducts = new HttpCookie("CartPID");
                //    CartProducts.Values["CartPID"] = PID.ToString();
                //    CartProducts.Expires = DateTime.Now.AddDays(30);
                //    Response.Cookies.Add(CartProducts);
                //}
                //else
                //{
                //    HttpCookie CartProducts = new HttpCookie("CartPID");
                //    CartProducts.Values["CartPID"] = PID.ToString();
                //    CartProducts.Expires = DateTime.Now.AddDays(30);
                //    Response.Cookies.Add(CartProducts);
                //}
                AddToCartProduction();
                Response.Redirect("~/ProductView.aspx?PID=" + PID);
            }
            else
            {
                foreach (RepeaterItem item in rptrProductDetails.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        var lblError = item.FindControl("lblError") as Label;
                        lblError.Text = "Please Select a Size";
                    }
                }
            }
        }
        protected override void InitializeCulture()
        {
            CultureInfo ci = new CultureInfo("en-IN");
            ci.NumberFormat.CurrencySymbol = "₹";
            Thread.CurrentThread.CurrentCulture = ci;

            base.InitializeCulture();
        }
        public void BindCartNumber()
        {
            if (Session["USERID"] != null)
            {
                string UserIDD = Session["USERID"].ToString();
                DataTable dt = new DataTable();
                using (MySqlConnection con = new MySqlConnection(CS))
                {
                    MySqlCommand cmd = new MySqlCommand("SP_BindCartNumberz", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_UserID", UserIDD);
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            string CartQuantity = dt.Compute("Sum(Qty)", "").ToString();
                            CartBadge.InnerText = CartQuantity;

                        }
                        else
                        {
                            CartBadge.InnerText = 0.ToString();
                        }
                    }
                }
            }
        }
        private void AddToCartProduction()
        {
            if (Session["Username"] != null)
            {
                Int32 UserID = Convert.ToInt32(Session["USERID"].ToString());
                Int64 PID = Convert.ToInt64(Request.QueryString["PID"]);
                using (MySqlConnection con = new MySqlConnection(CS))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_IsProductExistInCart", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_UserID", UserID);
                    cmd.Parameters.AddWithValue("_PID", PID);
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Int32 updateQty = Convert.ToInt32(dt.Rows[0]["Qty"].ToString());
                            MySqlCommand myCmd = new MySqlCommand("SP_UpdateCart", con)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            myCmd.Parameters.AddWithValue("_Quantity", updateQty + 1);
                            myCmd.Parameters.AddWithValue("_CartPID", PID);
                            myCmd.Parameters.AddWithValue("_UserID", UserID);
                            Int64 CartID = Convert.ToInt64(myCmd.ExecuteScalar());
                            BindCartNumber();
                            divSuccess.Visible = true;
                        }
                        else
                        {
                            MySqlCommand myCmd = new MySqlCommand("SP_InsertCart", con)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            myCmd.Parameters.AddWithValue("_UID", UserID);
                            myCmd.Parameters.AddWithValue("_PID", Session["CartPID"].ToString());
                            myCmd.Parameters.AddWithValue("_PName", Session["myPName"].ToString());
                            myCmd.Parameters.AddWithValue("_PPrice", Session["myPPrice"].ToString());
                            myCmd.Parameters.AddWithValue("_PSellPrice", Session["myPSellPrice"].ToString());
                            myCmd.Parameters.AddWithValue("_Qty", myQty);
                            Int64 CartID = Convert.ToInt64(myCmd.ExecuteScalar());
                            con.Close();
                            BindCartNumber();
                            divSuccess.Visible = true;
                        }
                    }
                }
            }
            else
            {
                Int64 PID = Convert.ToInt64(Request.QueryString["PID"]);
                Response.Redirect("Signin.aspx?rurl=" + PID);
            }
        }
        //protected void btnCart2_ServerClick(object sender, EventArgs e)
        //{
        //    Response.Redirect("Cart.aspx");
        //}
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cart.aspx");
        }
    }
}