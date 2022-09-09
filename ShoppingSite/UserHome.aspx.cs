using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingSite
{
    public partial class UserHome : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] != null)
                {
                    btnlogout.Visible = true;
                    btnLogin.Visible = false;
                    lblSuccess.Text = "Login Success, Welcome <b>" + Session["Username"].ToString() + "</b>";
                    Button1.Text = "Welcome: " + Session["Username"].ToString().ToUpper();
                    BindCartNumber();
                }
            }
            else
            {
                btnlogout.Visible = false;
                btnLogin.Visible = true;
                Response.Redirect("SignIn.aspx");
            }
        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            //Session.Abandon();
            Session["Username"] = null;
            Response.Redirect("~/Default.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SignIn.aspx");
        }
        //public void BindCartNumber()
        //{
        //    if (Request.Cookies["CartPID"] != null)
        //    {
        //        string CookiePID = Request.Cookies["CartPID"].Value.Split('=')[1];
        //        string[] ProductArray = CookiePID.Split(',');
        //        int ProductCount = ProductArray.Length;
        //        pCount.InnerText = ProductCount.ToString();
        //    }
        //    else
        //    {
        //        pCount.InnerText = 0.ToString();
        //    }
        //}
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
                            pCount.InnerText = CartQuantity;
                        }
                        else
                        {
                            //_ = CartBadge.InnerText == 0.ToString();
                            pCount.InnerText = "0";

                        }
                    }
                }
            }
        }
    }
}