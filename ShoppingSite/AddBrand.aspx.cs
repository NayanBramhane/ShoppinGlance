using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
using System.Data;

namespace ShoppingSite
{
    public partial class AddBrand : System.Web.UI.Page
    {
        char[] charsToTrim = { ' ', '\t' };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBrandRepeater();
            }
        }

        private void BindBrandRepeater()
        {
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("Select * from tblBrands",con))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        rptrBrand.DataSource = dt;
                        rptrBrand.DataBind();
                    }
                }
            }
        }

        protected void btnAddBrand_Click(object sender, EventArgs e)
        {
            if (txtBrand.Text != null && txtBrand.Text != "" && txtBrand.Text != string.Empty) 
            { 
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
                {
                    con.Open();


                    MySqlCommand cmd = new MySqlCommand("Insert into tblBrands (Name) Values('" + txtBrand.Text.Trim(charsToTrim) + "')", con);
                    cmd.ExecuteNonQuery();
                    Response.Write("<script> alert('Brand Added Successfully');   </script>");
                    txtBrand.Text = string.Empty;


                    con.Close();
                    txtBrand.Focus();
                }
            }
        }
    }
}