using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ShoppingSite
{
    public partial class SubCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMainCat();
                BindSubCategoryRepeater();
            }
        }

        private void BindSubCategoryRepeater()
        {
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("select A.*, B.* from tblSubCategory A inner join tblCategory B on " +
                    "B.CatID = A.MainCatID;", con))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        rptrSubCat.DataSource = dt;
                        rptrSubCat.DataBind();
                    }
                }
            }
        }

        private void BindMainCat()
        {
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from tblCategory", con);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if(dt.Rows.Count != 0)
                {
                    ddlMainCatID.DataSource = dt;
                    ddlMainCatID.DataTextField = "CatName";
                    ddlMainCatID.DataValueField = "CatID";
                    ddlMainCatID.DataBind();
                    ddlMainCatID.Items.Insert(0, new ListItem("-Select-", "0"));
                }
            }
        }

        protected void btnAddSubCategory_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("Insert into tblSubCategory(SubCatName,MainCatID) Values('" + txtSubCategory.Text + "','" +
                    ddlMainCatID.SelectedItem.Value + "')", con);
                cmd.ExecuteNonQuery();

                Response.Write("<script> alert('SubCategory Added Successfully ');  </script>");
                txtSubCategory.Text = string.Empty;

                con.Close();
                ddlMainCatID.ClearSelection();
                ddlMainCatID.Items.FindByValue("0").Selected = true;

            }
            BindSubCategoryRepeater();
        }
    }
}