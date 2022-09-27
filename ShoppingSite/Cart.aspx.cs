using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Globalization;
using System.Threading;

namespace ShoppingSite
{
    public partial class Cart : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            divQtyError.Visible = false;
            if (!IsPostBack)
            {
                if (Session["Username"] != null)
                {
                    BindProductCart();
                    BindCartNumber();
                }
                else
                {
                    Response.Redirect("Signin.aspx");
                }
            }
        }

        private void BindCartNumber()
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
                            //_ = CartBadge.InnerText == 0.ToString();
                            CartBadge.InnerText = "0";

                        }
                    }
                }
            }
        }

        private void BindProductCart()
        {
            /*if (Request.Cookies["CartPID"] != null)
            {                
                string CookieData = Request.Cookies["CartPID"].Value.Split('=')[1];
                string[] CookieDataArray = CookieData.Split(',');
                if(CookieDataArray.Length > 0)
                {
                    h4NoItems.InnerText="My Cart ("+ CookieDataArray.Length + " Items)";
                    DataTable dt = new DataTable();
                    Int64 CartTotal = 0;
                    Int64 Total = 0;
                    for (int i=0; i < CookieDataArray.Length; i++)
                    {
                        string PID = CookieDataArray[i].ToString().Split('-')[0];
                        string SizeID = CookieDataArray[i].ToString().Split('-')[1];

                        using (MySqlConnection con = new MySqlConnection(CS))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("Select A.*,getSizeName("+SizeID+") as SizeNamee,"+SizeID+" as SizeIDD, " +
                                "SizeData.Name, SizeData, Extension from tblProducts A join lateral(B.Name,Extension from tblProductImages B where" +
                                " B.PID=A.PID limit 1) SizeData where A.PID='" + PID +"'", con))
                            {
                                cmd.CommandType = CommandType.Text;
                                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                                {
                                    sda.Fill(dt);
                                }
                            }
                        }
                        CartTotal += Convert.ToInt64(dt.Rows[i]["PPrice"]);
                        Total += Convert.ToInt64(dt.Rows[i]["PSellPrice"]);
                    }
                    rptrCartProducts.DataSource = dt;
                    rptrCartProducts.DataBind();
                    divAmountSect.Visible = true;

                    spanCartTotal.InnerText = CartTotal.ToString();
                    spanTotal.InnerText = "Rs. "+Total.ToString();
                    spanDiscount.InnerText = "-" + (CartTotal - Total).ToString();
                }
                else
                {
                    //Show Empty Cart
                    h4NoItems.InnerText = "Your shopping cart is empty";
                    divAmountSect.Visible = false;
                }
            }
            else
            {
                //Show Empty Cart
                h4NoItems.InnerText = "Your shopping cart is empty";
                divAmountSect.Visible = false;
            }*/
            string UserIDD = Session["USERID"].ToString();
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(CS))
            {
                MySqlCommand cmd = new MySqlCommand("SP_BindUserCart", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("_UserID", UserIDD);
                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                    rptrCartProducts.DataSource = dt;
                    rptrCartProducts.DataBind();
                    if (dt.Rows.Count > 0)
                    {
                        string Total = dt.Compute("Sum(SubSAmount)", "").ToString();
                        string CartTotal = dt.Compute("Sum(SubPAmount)", "").ToString();
                        string CartQuantity = dt.Compute("Sum(Qty)", "").ToString();
                        h4NoItems.InnerText = "My Cart ( " + CartQuantity + " Item(s) )";
                        int Total1 = Convert.ToInt32(dt.Compute("Sum(SubSAmount)", ""));
                        int CartTotal1 = Convert.ToInt32(dt.Compute("Sum(SubPAmount)", ""));
                        spanTotal.InnerText = "Rs. " + string.Format("{0:#,###.##}", double.Parse(Total)) + ".00";
                        spanCartTotal.InnerText = "Rs. " + string.Format("{0:#,###.##}", double.Parse(CartTotal)) + ".00";
                        spanDiscount.InnerText = "- Rs. " + (CartTotal1 - Total1).ToString() + ".00";
                    }
                    else
                    {
                        h4NoItems.InnerText = "Your Shopping Cart is Empty.";
                        divAmountSect.Visible = false;

                    }
                }
            }
        }

        //protected void btnRemoveCart_Click(object sender, EventArgs e)
        //{
        //    string CookiePID = Request.Cookies["CartPID"].Value.Split('=')[1];

        //    Button btn = (Button)(sender);

        //    string PIDSIZE = btn.CommandArgument;

        //    List<string> CookiePIDList = CookiePID.Split(',').Select(i=>i.Trim()).Where(i=>i!=string.Empty).ToList();
        //    CookiePIDList.Remove(PIDSIZE);
        //    string CookiePIDUpdated = String.Join(",", CookiePIDList.ToArray());
        //    if(CookiePIDUpdated == "")
        //    {
        //        HttpCookie CartProducts = Request.Cookies["CartPID"];
        //        CartProducts.Values["CartPID"] = null;
        //        CartProducts.Expires = DateTime.Now.AddDays(-1);
        //        Response.Cookies.Add(CartProducts);
        //    }
        //    else
        //    {
        //        HttpCookie CartProducts = Request.Cookies["CartPID"];
        //        CartProducts.Values["CartPID"] = CookiePIDUpdated;
        //        CartProducts.Expires = DateTime.Now.AddDays(30);
        //        Response.Cookies.Add(CartProducts);
        //    }
        //    Response.Redirect("~/Cart.aspx");
        //}

        protected override void InitializeCulture()
        {
            CultureInfo ci = new CultureInfo("en-IN");
            ci.NumberFormat.CurrencySymbol = "₹";
            Thread.CurrentThread.CurrentCulture = ci;
            base.InitializeCulture();
        }
        protected void rptrCartProducts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Int32 UserID = Convert.ToInt32(Session["USERID"].ToString());
            //This will add +1 to current quantity using PID
            if (e.CommandName == "DoPlus")
            {
                string PID = (e.CommandArgument.ToString());
                using (MySqlConnection con = new MySqlConnection(CS))
                {
                    MySqlCommand cmd = new MySqlCommand("SP_getUserCartItem", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_PID", PID);
                    cmd.Parameters.AddWithValue("_UserID", UserID);
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
                            con.Open();
                            Int64 CartID = Convert.ToInt64(myCmd.ExecuteScalar());
                            con.Close();
                            BindProductCart();
                            BindCartNumber();
                        }
                    }

                }
            }
            else if (e.CommandName == "DoMinus")
            {
                string PID = (e.CommandArgument.ToString());
                using (MySqlConnection con = new MySqlConnection(CS))
                {
                    MySqlCommand cmd = new MySqlCommand("SP_getUserCartItem", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_PID", PID);
                    cmd.Parameters.AddWithValue("_UserID", UserID);
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Int32 myQty = Convert.ToInt32(dt.Rows[0]["Qty"].ToString());
                            if (myQty <= 1)
                            {
                                divQtyError.Visible = true;
                            }
                            else
                            {
                                MySqlCommand myCmd = new MySqlCommand("SP_UpdateCart", con)
                                {
                                    CommandType = CommandType.StoredProcedure
                                };
                                myCmd.Parameters.AddWithValue("_Quantity", myQty - 1);
                                myCmd.Parameters.AddWithValue("_CartPID", PID);
                                myCmd.Parameters.AddWithValue("_UserID", UserID);
                                con.Open();
                                Int64 CartID = Convert.ToInt64(myCmd.ExecuteScalar());
                                con.Close();
                                BindProductCart();
                                BindCartNumber();

                            }
                        }

                    }
                }
            }
            else if (e.CommandName == "RemoveThisCart")
            {
                int CartPID = Convert.ToInt32(e.CommandArgument.ToString().Trim());
                using (MySqlConnection con = new MySqlConnection(CS))
                {
                    MySqlCommand myCmd = new MySqlCommand("SP_DeleteThisCartItem", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    myCmd.Parameters.AddWithValue("_CartID", CartPID);
                    con.Open();
                    myCmd.ExecuteNonQuery();
                    con.Close();
                    BindProductCart();
                    BindCartNumber();
                }
            }
        }
        //protected void btnCart2_ServerClick(object sender, EventArgs e)
        //{
        //    Response.Redirect("Cart.aspx");
        //}

        protected void btnCart2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cart.aspx");
        }

        protected void btnBuyNow_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Payment.aspx");
        }
    }
}