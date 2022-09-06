using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace ShoppingSite
{
    public partial class EditSize : System.Web.UI.Page
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
            MySqlCommand cmd = new MySqlCommand("select SizeName,BrandID,CategoryID,SubCategoryID,GenderID from tblSizes where SizeID=_ID", con);
            cmd.Parameters.AddWithValue("_ID", Convert.ToInt32(txtID.Text));
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(ds, "dt");
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                string BrandID = ds.Tables[0].Rows[0]["BrandID"].ToString();
                BindBrand();
                ddlBrand.SelectedValue = BrandID;

                string SizeName = ds.Tables[0].Rows[0]["SizeName"].ToString();
                txtSize.Text = SizeName;

                string MainCID = ds.Tables[0].Rows[0]["CategoryID"].ToString();
                BindMainCategory();
                ddlCategory.SelectedValue = MainCID;

                string SubCID = ds.Tables[0].Rows[0]["SubCategoryID"].ToString();
                subcategory();
                ddlSubCategory.SelectedValue = SubCID;

                string GenderID = ds.Tables[0].Rows[0]["SubCategoryID"].ToString();
                BindGender();
                ddlGender.SelectedValue = GenderID;
            }
            else
            {

            }
            con.Close();
        }
        private void BindGridview()
        {
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString);
            if (con.State == ConnectionState.Closed) { con.Open(); }
            MySqlDataAdapter da = new MySqlDataAdapter("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED ;  select t1.SizeID,t1.SizeName,t2.Name " +
                "as Brand,t3.CatName as Category,t4.SubCatName as SubCategory,t5.GenderName as Gender from tblSizes as t1 inner join tblBrands " +
                "as t2 on t2.BrandID=t1.BrandID inner join tblCategory as t3 on t3.CatID=t1.CategoryID inner join tblSubCategory as on " +
                "t4.SubCatID=t1.SubCategoryID inner join tblGender as t5 on t5.GenderID=t1.GenderID;  COMMIT ;  SET TRANSACTION ISOLATION LEVEL " +
                "REPEATABLE READ ;", con);
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

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int MainCategoryID = Convert.ToInt32(ddlCategory.SelectedItem.Value);

            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from tblSubCategory where MainCatID='" + ddlCategory.SelectedValue + "'", con);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    ddlSubCategory.DataSource = dt;
                    ddlSubCategory.DataTextField = "SubCatName";
                    ddlSubCategory.DataValueField = "SubCatID";
                    ddlSubCategory.DataBind();
                    ddlSubCategory.Items.Insert(0, new ListItem("-Select-", "0"));

                }
            }
        }

        protected void btnUpdateSubCategory_Click(object sender, EventArgs e)
        {
            if (txtID.Text != string.Empty && txtSize.Text != string.Empty && ddlCategory.SelectedIndex != -1)
            {
                MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString);
                if (con.State == ConnectionState.Closed) { con.Open(); }
                MySqlCommand cmd = new MySqlCommand("update tblSizes set SizeName=_SizeName,BrandID=_BrandID,CategoryID=_CategoryID," +
                    "SubCategoryID=_SubCategoryID,GenderID=_GenderID where SizeID=_SizeID", con);
                cmd.Parameters.AddWithValue("_SizeID", Convert.ToInt32(txtID.Text));
                cmd.Parameters.AddWithValue("_CategoryID", ddlCategory.SelectedValue);
                cmd.Parameters.AddWithValue("_SubCategoryID", ddlSubCategory.SelectedValue);
                cmd.Parameters.AddWithValue("_BrandID", ddlBrand.SelectedValue);
                cmd.Parameters.AddWithValue("_GenderID", ddlGender.SelectedValue);
                cmd.Parameters.AddWithValue("_SizeName", txtSize.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Update successfully')</script>");
                BindGridview();
                txtID.Text = string.Empty;
                ddlBrand.SelectedIndex = -1;
                ddlCategory.SelectedIndex = -1;
                ddlSubCategory.SelectedIndex = -1;
                ddlGender.SelectedIndex = -1;
                txtSize.Text = string.Empty;

            }
        }
        private void BindBrand()
        {
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from tblBrands", con);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    ddlBrand.DataSource = dt;
                    ddlBrand.DataTextField = "Name";
                    ddlBrand.DataValueField = "BrandID";
                    ddlBrand.DataBind();
                    ddlBrand.Items.Insert(0, new ListItem("-Select-", "0"));

                }
            }
        }
        private void BindMainCategory()
        {
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from tblCategory", con);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    ddlCategory.DataSource = dt;
                    ddlCategory.DataTextField = "CatName";
                    ddlCategory.DataValueField = "CatID";
                    ddlCategory.DataBind();
                    ddlCategory.Items.Insert(0, new ListItem("-Select-", "0"));

                }
            }
        }

        private void BindGender()
        {
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED ;  Select * from tblGender  COMMIT ;  " +
                    "SET TRANSACTION ISOLATION LEVEL REPEATABLE READ ;", con);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    ddlGender.DataSource = dt;
                    ddlGender.DataTextField = "GenderName";
                    ddlGender.DataValueField = "GenderID";
                    ddlGender.DataBind();
                    ddlGender.Items.Insert(0, new ListItem("-Select-", "0"));

                }
            }
        }

        private void subcategory()
        {
            int MainCategoryID = Convert.ToInt32(ddlCategory.SelectedItem.Value);
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                MySqlCommand cmd = new MySqlCommand("Select * from tblSubCategory where MainCatID='" + ddlCategory.SelectedValue + "'", con);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count != 0)
                {
                    ddlSubCategory.DataSource = dt;
                    ddlSubCategory.DataTextField = "SubCatName";
                    ddlSubCategory.DataValueField = "SubCatID";
                    ddlSubCategory.DataBind();
                    ddlSubCategory.Items.Insert(0, new ListItem("-Select-", "0"));

                }
            }
        }
    }
}