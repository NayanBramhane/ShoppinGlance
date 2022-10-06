using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace ShoppingSite
{
    public partial class EditCategory : System.Web.UI.Page
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
            MySqlCommand cmd = new MySqlCommand("select CatName from tblCategory where CatID = @ID", con);
            cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtID.Text));
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(ds, "dt");
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnUpdateBrand.Enabled = true;
                txtUpdateCatName.Text = ds.Tables[0].Rows[0]["CatName"].ToString();

            }
            else
            {
                btnUpdateBrand.Enabled = false;
                txtUpdateCatName.Text = string.Empty;
            }
            con.Close();
        }

        protected void btnUpdateBrand_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString);
            if (con.State == ConnectionState.Closed) { con.Open(); }
            MySqlCommand cmd = new MySqlCommand("update tblCategory set CatName = @Name where CatID = @ID", con);
            cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtID.Text.Trim()));
            cmd.Parameters.AddWithValue("@Name", txtUpdateCatName.Text.Trim());
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("<script>alert('Update successfully')</script>");
            BindGridview();
            txtID.Text = string.Empty;
            txtUpdateCatName.Text = string.Empty;
        }

        private void BindGridview()
        {
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString);
            if (con.State == ConnectionState.Closed) { con.Open(); }
            MySqlDataAdapter da = new MySqlDataAdapter("select CatID,CatName from tblCategory", con);
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