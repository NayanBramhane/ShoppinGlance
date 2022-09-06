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
    public partial class AddCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindCategoryRepeater();
        }

        private void BindCategoryRepeater()
        {
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("Select * from tblCategory", con))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        rptrCategory.DataSource = dt;
                        rptrCategory.DataBind();
                    }
                }
            }
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                con.Open();


                MySqlCommand cmd = new MySqlCommand("Insert into tblCategory (CatName) Values('" + txtCategory.Text + "')", con);
                cmd.ExecuteNonQuery();
                Response.Write("<script> alert('Category Added Successfully');   </script>");
                txtCategory.Text = String.Empty;


                con.Close();
                txtCategory.Focus();
            }
        }
    }
}