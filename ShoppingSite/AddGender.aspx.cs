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
    public partial class AddGender : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGenderRepeater();
        }
        private void BindGenderRepeater()
        {
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("Select * from tblGender", con))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        rptrGender.DataSource = dt;
                        rptrGender.DataBind();
                    }
                }
            }
        }

        protected void btnAddBrand_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("Insert into tblGender(GenderName) Values('" + txtGender.Text +"')", con);
                cmd.ExecuteNonQuery();

                Response.Write("<script> alert('Gender Added Successfully ');  </script>");
                txtGender.Text = string.Empty;

                con.Close();
                txtGender.Focus();

            }
            BindGenderRepeater();
        }
    }
}