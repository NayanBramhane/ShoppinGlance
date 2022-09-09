using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

namespace ShoppingSite
{
    public partial class User : System.Web.UI.MasterPage
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] != null)
                {
                    //lblSuccess.Text = "Login Success, Welcome " + Session["Username"].ToString();
                    btnlogout.Visible = true;
                    btnLogin.Visible = false;
                    BindCartNumber();
                    Button1.Text = "Welcome: " + Session["Username"].ToString().ToUpper();
                }
            }
            else
            {
                btnlogout.Visible = false;
                btnLogin.Visible = false;
                Response.Redirect("~/Default.aspx");
            }
        }
        protected void btnlogout_Click(Object sender, EventArgs e)
        {
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
                            pCount.InnerText = 0.ToString();
                        }
                    }
                }
            }
        }
    }
}