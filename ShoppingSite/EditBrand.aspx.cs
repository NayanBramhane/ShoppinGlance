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
    public partial class EditBrand : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                if (!IsPostBack)
                {
                    BindGridview();
                }
            }
            else
            {
                Response.Redirect("SignIn.aspx");
            }
        }

        protected void txtID_TextChanged(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString);
            if (con.State == ConnectionState.Closed) { con.Open(); }
            MySqlCommand cmd = new MySqlCommand("select Name from tblBrands where BrandID=_ID", con);
            cmd.Parameters.AddWithValue("_ID", Convert.ToInt32(txtID.Text));
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(ds, "dt");
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnUpdateBrand.Enabled = true;
                txtUpdateBrandName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            }
            else
            {
                btnUpdateBrand.Enabled = false;
                txtUpdateBrandName.Text = string.Empty;
            }
            con.Close();
        }

        protected void btnUpdateBrand_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString);
            if (con.State == ConnectionState.Closed) { con.Open(); }
            MySqlCommand cmd = new MySqlCommand("update tblBrands set Name=UPPER(_Name) where BrandID=_ID", con);
            cmd.Parameters.AddWithValue("_ID", Convert.ToInt32(txtID.Text));
            cmd.Parameters.AddWithValue("_Name", txtUpdateBrandName.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("<script>alert('Update successfully')</script>");
            BindGridview();
            txtID.Text = string.Empty;
            txtUpdateBrandName.Text = string.Empty;
        }

        private void BindGridview()
        {
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString);
            if (con.State == ConnectionState.Closed) { con.Open(); }
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tblBrands", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
    }
}