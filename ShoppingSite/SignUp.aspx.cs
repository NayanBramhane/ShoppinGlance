using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace ShoppingSite
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void txtsignup_Click(object sender, EventArgs e)
        {
            if (isformvalid())
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
                {
                    con.Open();


                    MySqlCommand cmd = new MySqlCommand("Insert into tblUsers(Username,Password,Email,Name,Usertype) Values('" + txtUname.Text + "','" +
                        txtPass.Text + "','" + txtEmail.Text + "','" + txtName.Text + "','User')", con);
                    cmd.ExecuteNonQuery();
                    Response.Write("<script> alert('Registration Successfully Done');   </script>");
                    clr();


                    con.Close();

                }
                Response.Redirect("~/SignIn.aspx");
            }
            else
            {
                Response.Write("<script> alert('Registration Failed');   </script>");
            }
        }

        private bool isformvalid()
        {
            if(txtUname.Text=="")
            {
                Response.Write("<script> alert('Username not valid');   </script>");
                txtName.Focus();
                return false;
            }
            else if(txtPass.Text=="")
            {
                Response.Write("<script> alert('Password not valid');   </script>");
                txtPass.Focus();
                return false;
            }
            else if(txtConPass.Text != txtPass.Text)
            {
                Response.Write("<script> alert('Password doesn't match');   </script>");
                txtConPass.Focus();
                return false;
            }
            else if (txtEmail.Text == "")
            {
                Response.Write("<script> alert('E-mail not valid');   </script>");
                txtEmail.Focus();
                return false;
            }
            else if (txtName.Text == "")
            {
                Response.Write("<script> alert('Name not valid');   </script>");
                txtName.Focus();
                return false;
            }
            return true;
        }

        private void clr()
        {
            txtName.Text = string.Empty;
            txtPass.Text = string.Empty;
            txtUname.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtConPass.Text = string.Empty;
        }

    }
}