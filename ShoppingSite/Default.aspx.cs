using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace ShoppingSite
{
    public partial class Default : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["UserLogin"] == "YES")
            {
                Response.Redirect("UserHome.aspx?UserLogin=YES");
            }

            BindCartNumber();
            BindProductRepeater();
            if (Session["Username"] != null)
            {
                if (!this.IsPostBack)
                {
                    //lblSuccess.Text = "Login Success, Welcome " + Session["Username"].ToString();
                    btnSignUp.Visible = false;
                    btnSignIn.Visible = false;
                    btnlogout.Visible = true;
                    btnCart.Visible = true;
                }
            }
            else
            {
                BindProductRepeater();
                btnSignUp.Visible = true;
                btnSignIn.Visible = true;
                btnlogout.Visible = false;
                btnCart.Visible = false;
                //Response.Redirect("~/Default.aspx");
                Response.Write("<script type='text/javascript'>alert('Login plz')</script>");
            }
        }        

        public void BindCartNumber()
        {
            if(Request.Cookies["CartPID"] != null)
            {
                string CookiePID = Request.Cookies["CartPID"].Value.Split('=')[1];
                string[] ProductArray = CookiePID.Split(',');
                int ProductCount = ProductArray.Length;
                pCount.InnerText = ProductCount.ToString();
            }
            else
            {
                pCount.InnerText = 0.ToString();
            }
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["Username"] = null;
            Session.RemoveAll();
            Response.Redirect("~/Default.aspx");
        }
        private void BindProductRepeater()
        {
            using (MySqlConnection con = new MySqlConnection(CS))
            {
                using (MySqlCommand cmd = new MySqlCommand("procBindAllProducts", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        rptrProducts.DataSource = dt;
                        rptrProducts.DataBind();
                        if (dt.Rows.Count <= 0)
                        {
                            //Label1.Text = "Sorry! Currently no products in this category.";
                            pCount.InnerHtml = "0";
                        }
                        else
                        {
                            //Label1.Text = "Showing All Products";
                        }
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
    }
}