using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace ShoppingSite
{
    public partial class Payment : System.Web.UI.Page
    {
        public static Int32 OrderNumber = 1;
        public static String CS = ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                if (!IsPostBack)
                {
                    //BindPriceData();
                    genAutoNum();
                    BindCartNumber();
                    BindOrderProducts();
                }
            }
            else
            {
                Response.Redirect("~/SignIn.aspx");
            }
        }

        private void BindOrderProducts()
        {
            string UserIDD = Session["USERID"].ToString();
            DataTable dt = new DataTable();
            using (MySqlConnection con0 = new MySqlConnection(CS))
            {
                MySqlCommand cmd0 = new MySqlCommand("SP_BindCartProducts", con0)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd0.Parameters.AddWithValue("_UID", UserIDD);
                using (MySqlDataAdapter sda0 = new MySqlDataAdapter(cmd0))
                {
                    sda0.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataColumn PID in dt.Columns)
                        {
                            using (MySqlConnection con = new MySqlConnection(CS))
                            {
                                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM tblCart C WHERE C.PID=" + PID + " AND UID ='" + UserIDD + 
                                    "'", con))
                                {
                                    cmd.CommandType = CommandType.Text;
                                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                                    {
                                        DataTable dtProducts = new DataTable();
                                        sda.Fill(dtProducts);
                                        gvProducts.DataSource = dtProducts;
                                        gvProducts.DataBind();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void genAutoNum()
        {
            Random r = new Random();
            int num = r.Next(Convert.ToInt32("231965"), Convert.ToInt32("987654"));
            string ChkOrderNum = num.ToString();
            using (MySqlConnection con = new MySqlConnection(CS))
            {
                MySqlCommand cmd = new MySqlCommand("SP_FindOrderNumber", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("_FindOrderNumber", ChkOrderNum);
                if (con.State == ConnectionState.Closed) { con.Open(); }
                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        genAutoNum();
                    }
                    else
                    {
                        OrderNumber = Convert.ToInt32(num.ToString());
                    }
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
                        }
                    }
                }
            }
        }

        private void BindPriceData()
        {
            if (Request.Cookies["CartPID"] != null)
            {
                string CookieData = Request.Cookies["CartPID"].Value.Split('=')[1];
                string[] CookieDataArray = CookieData.Split(',');
                if (CookieDataArray.Length > 0)
                {
                    DataTable dt = new DataTable();
                    Int64 CartTotal = 0;
                    Int64 Total = 0;
                    for (int i = 0; i < CookieDataArray.Length; i++)
                    {
                        string PID = CookieDataArray[i].ToString().Split('-')[0];
                        string SizeID = CookieDataArray[i].ToString().Split('-')[1];

                        if (hdPidSizeID.Value != null || hdPidSizeID.Value != "")
                        {
                            hdPidSizeID.Value += "," + PID + "-" + SizeID;
                        }
                        else
                        {
                            hdPidSizeID.Value += PID + "-" + SizeID;
                        }

                        using (MySqlConnection con = new MySqlConnection(CS))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("Select A.*,getSizeName(" + SizeID + ") as SizeNamee," + SizeID + " as SizeIDD, " +
                                "SizeData.Name, SizeData, Extension from tblProducts A join lateral(B.Name,Extension from tblProductImages B where" +
                                " B.PID=A.PID limit 1) SizeData where A.PID='" + PID + "'", con))
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
                    divPriceDetails.Visible = true;

                    spanCartTotal.InnerText = CartTotal.ToString();
                    spanTotal.InnerText = "Rs. " + Total.ToString();
                    spanDiscount.InnerText = "-" + (CartTotal - Total).ToString();

                    hdCartAmount.Value = CartTotal.ToString();
                    hdCartDiscount.Value = (CartTotal - Total).ToString();
                    hdTotalPayed.Value = Total.ToString();
                }
                else
                {
                    //Show Empty Cart
                    Response.Redirect("~/Products.aspx");
                }
            }
            else
            {
                //Show Empty Cart
                Response.Redirect("~/Products.aspx");
            }
        }

        protected void btnPaytm_Click(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                string USERID = Session["USERID"].ToString();
                string PaymentType = "Paytm";
                string PaymentStatus = "Not Paid";
                string EMAILID = Session["USERMAIL"].ToString();

                using (MySqlConnection con = new MySqlConnection(CS))
                {
                    MySqlCommand cmd = new MySqlCommand("Insert into tblPurchase (UserID, PIDSizeID, CartAmount, CartDiscount, TotalPayed, " +
                        "PaymentType, PaymentStatus, DateOfPurchase, Name, Address, Pincode, MobileNumber) values ('" + USERID + "','" + 
                        hdPidSizeID.Value + "','" + hdCartAmount.Value + "','" + hdCartDiscount.Value + "','" + hdTotalPayed.Value + "','" + 
                        PaymentType + "','" + PaymentStatus + "',SYSDATE(),'" + txtName.Text.Trim() + "','" + txtAddress.Text + "','" + txtPinCode.Text + 
                        "','" + txtMobileNumber.Text.Trim() + "')Select LAST_INSERT_ID()", con);

                    con.Open();
                    Int64 PurchaseID = Convert.ToInt64(cmd.ExecuteScalar());
                    InsertOrderProducts();
                }
            }
            else
            {
                Response.Redirect("~/SignIn.aspx");
            }
        }

        //protected void btnCart2_ServerClick(object sender, EventArgs e)
        //{
        //    Response.Redirect("Cart.aspx");
        //}

        protected void BtnPlaceNPay_Click(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                Session["Address"] = txtAddress.Text;
                Session["Mobile"] = txtMobileNumber.Text;
                Session["OrderNumber"] = OrderNumber.ToString();
                Session["PayMethod"] = "Place n Pay";

                string USERID = Session["USERID"].ToString();
                string PaymentType = "PnP";
                string PaymentStatus = "NotPaid";
                string EMAILID = Session["USEREMAIL"].ToString();
                string OrderStatus = "Pending";
                string FullName = Session["getFullName"].ToString();
                using (MySqlConnection con = new MySqlConnection(CS))
                {
                    MySqlCommand cmd = new MySqlCommand("SP_InsertOrder", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_UserID", USERID);
                    cmd.Parameters.AddWithValue("_Email", EMAILID);
                    cmd.Parameters.AddWithValue("_CartAmount", hdCartAmount.Value);
                    cmd.Parameters.AddWithValue("_CartDiscount", hdCartDiscount.Value);
                    cmd.Parameters.AddWithValue("_TotalPaid", hdTotalPayed.Value);
                    cmd.Parameters.AddWithValue("_PaymentType", PaymentType);
                    cmd.Parameters.AddWithValue("_PaymentStatus", PaymentStatus);
                    cmd.Parameters.AddWithValue("_DateOfPurchase", DateTime.Now);
                    cmd.Parameters.AddWithValue("_Name", FullName);
                    cmd.Parameters.AddWithValue("_Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("_MobileNumber", txtMobileNumber.Text);
                    cmd.Parameters.AddWithValue("_OrderStatus", OrderStatus);
                    cmd.Parameters.AddWithValue("_OrderNumber", OrderNumber.ToString());
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    //Int64 OrderID = Convert.ToInt64(cmd.ExecuteScalar());
                    //InsertOrderProducts();
                    Response.Redirect("Success.aspx");
                }
            }
            else
            {
                Response.Redirect("SignIn.aspx?RtPP=yes");
            }
        }

        private void InsertOrderProducts()
        {
            string USERID = Session["USERID"].ToString();
            using (MySqlConnection con = new MySqlConnection(CS))
            {
                foreach (GridViewRow gvr in gvProducts.Rows)
                {
                    MySqlCommand myCmd = new MySqlCommand("SP_InsertOrderProducts", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    myCmd.Parameters.AddWithValue("_OrderID", OrderNumber.ToString());
                    myCmd.Parameters.AddWithValue("_UserID", USERID);
                    myCmd.Parameters.AddWithValue("_PID", gvr.Cells[0].Text);
                    myCmd.Parameters.AddWithValue("_Products", gvr.Cells[1].Text);
                    myCmd.Parameters.AddWithValue("_Quantity", gvr.Cells[2].Text);
                    myCmd.Parameters.AddWithValue("_OrderDate", DateTime.Now.ToString("yyyy-MM-dd"));
                    myCmd.Parameters.AddWithValue("_Status", "Pending");
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    Int64 OrderProID = Convert.ToInt64(myCmd.ExecuteScalar());
                    con.Close();
                    EmptyCart();
                    Response.Redirect("Success.aspx");
                }
            }
        }
        private void EmptyCart()
        {
            Int32 CartUIDD = Convert.ToInt32(Session["USERID"].ToString());
            using (MySqlConnection con = new MySqlConnection(CS))
            {
                MySqlCommand cmdU = new MySqlCommand("SP_EmptyCart", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmdU.Parameters.AddWithValue("_UserID", CartUIDD);
                if (con.State == ConnectionState.Closed) { con.Open(); }
                cmdU.ExecuteNonQuery();
                con.Close();
            }
        }

        protected void btnCart2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cart.aspx");
        }
    }
}