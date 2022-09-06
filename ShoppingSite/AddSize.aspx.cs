using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ShoppingSite
{
    public partial class AddSize : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBrand();
                BindMainCategory();
                BindGender();
                BindrptrSize();
            }
        }

        private void BindrptrSize()
        {
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("select A.*,B.*,C.*,D.*,E.* from tblSizes A inner join tblCategory B on B.CatID = " +
                    "A.CategoryID inner join tblBrands C on C.BrandID = A.BrandID inner join tblSubCategory D on D.SubCatID = A.SubCategoryID inner " +
                    "join tblGender E on E.GenderID = A.GenderID;", con))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        rptrSize.DataSource = dt;
                        rptrSize.DataBind();
                    }
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

        private void BindGender()
        {
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from tblGender", con);
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

        protected void btnAddSize_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("Insert into tblSizes (SizeName, BrandID, CategoryID, SubCategoryID, GenderID) " +
                    "Values('" + txtSize.Text + "','" + ddlBrand.SelectedItem.Value + "','" + ddlCategory.SelectedItem.Value + "','" + 
                    ddlSubCat.SelectedItem.Value + "','" + ddlGender.SelectedItem.Value + "')", con);
                cmd.ExecuteNonQuery();

                Response.Write("<script> alert('Size Added Successfully ');  </script>");
                txtSize.Text = string.Empty;

                con.Close();
                ddlBrand.ClearSelection();
                ddlBrand.Items.FindByValue("0").Selected = true;

                ddlCategory.ClearSelection();
                ddlCategory.Items.FindByValue("0").Selected = true;

                ddlSubCat.ClearSelection();
                ddlSubCat.Items.FindByValue("0").Selected = true;

                ddlGender.ClearSelection();
                ddlGender.Items.FindByValue("0").Selected = true;

            }
            BindrptrSize();
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int MainCategoryID = Convert.ToInt32(ddlCategory.SelectedItem.Value);

            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from tblSubCategory where MainCatID='" + MainCategoryID + "'", con);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    ddlSubCat.DataSource = dt;
                    ddlSubCat.DataTextField = "SubCatName";
                    ddlSubCat.DataValueField = "SubCatID";
                    ddlSubCat.DataBind();
                    ddlSubCat.Items.Insert(0, new ListItem("-Select-", "0"));
                }
            }
        }
    }
}