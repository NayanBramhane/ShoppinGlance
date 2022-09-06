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
    public partial class EditSubCategory : System.Web.UI.Page
    {
        string ID = "";
        string SCName = "";
        string MainCID = "";
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

        private void BindGridview()
        {
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString);
            if (con.State == ConnectionState.Closed) { con.Open(); }
            MySqlDataAdapter da = new MySqlDataAdapter("select t1.SubCatID as ID,t2.CatName as MainCategory,t1.SubCatName as SubCategory " +
                "from tblSubCategory as t1 inner join tblCategory as t2 on t2.CatID=t1.MainCatID;", con);
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

        protected void txtID_TextChanged(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString);
            if (con.State == ConnectionState.Closed) { con.Open(); }
            MySqlCommand cmd = new MySqlCommand("select SubCatID,SubCatName,MainCatID from tblSubCategory where SubCatID=_ID;", con);
            cmd.Parameters.AddWithValue("_ID", Convert.ToInt32(txtID.Text));
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(ds, "dt");
            con.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {

                ID = ds.Tables[0].Rows[0]["SubCatID"].ToString();
                SCName = ds.Tables[0].Rows[0]["SubCatName"].ToString();
                MainCID = ds.Tables[0].Rows[0]["MainCatID"].ToString();
                BindMainCat();
                txtSubCategory.Text = SCName;

            }
            else
            {
                ID = "";
                SCName = "";
                MainCID = "";
            }
            con.Close();
        }
        private void BindMainCat()
        {
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                MySqlCommand cmd = new MySqlCommand("Select * from tblCategory;", con);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count != 0)
                {
                    ddlMainCategory.DataSource = dt;
                    ddlMainCategory.DataTextField = "CatName";
                    ddlMainCategory.DataValueField = "CatID";
                    ddlMainCategory.DataBind();
                    ddlMainCategory.Items.Insert(0, new ListItem("-Select-", "0"));
                    ddlMainCategory.SelectedValue = MainCID;

                }
            }
        }
        protected void btnUpdateSubCategory_Click(object sender, EventArgs e)
        {
            if (txtID.Text != string.Empty && txtSubCategory.Text != string.Empty && ddlMainCategory.SelectedIndex != -1)
            {
                MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString);
                if (con.State == ConnectionState.Closed) { con.Open(); }
                MySqlCommand cmd = new MySqlCommand("update tblSubCategory set SubCatName=_SCN , MainCatID=_MCI where SubCatID=_ID", con);
                cmd.Parameters.AddWithValue("_ID", Convert.ToInt32(txtID.Text));
                cmd.Parameters.AddWithValue("_MCI", ddlMainCategory.SelectedValue);
                cmd.Parameters.AddWithValue("_SCN", txtSubCategory.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Update successfully')</script>");
                BindGridview();
                txtID.Text = string.Empty;
                ddlMainCategory.SelectedIndex = -1;
                txtSubCategory.Text = string.Empty;
            }
        }
    }
}