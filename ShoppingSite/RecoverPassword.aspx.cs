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
    public partial class RecoverPassword : System.Web.UI.Page
    {
        String GUIDvalue;

        DataTable dt = new DataTable();
        int Uid;
        protected void Page_Load(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                con.Open();
                GUIDvalue = Request.QueryString["id"];
                if(GUIDvalue != null)
                {
                    MySqlCommand cmd = new MySqlCommand("select * from ForgotPass where  Id=@Id", con);
                    cmd.Parameters.AddWithValue("@Id", GUIDvalue);
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(dt);
                    if (dt.Rows.Count != 0)
                    {
                        Uid = Convert.ToInt32(dt.Rows[0][1]);
                    }
                    else
                    {
                        lblMsg.Text = "Your password reset link is expired or invalid... Please try again";
                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            if(!IsPostBack)
            {
                if(dt.Rows.Count != 0)
                {
                    txtConfirmPass.Visible = true;
                    txtNewPass.Visible = true;
                    lblNewPassword.Visible = true;
                    lblConfirmPass.Visible = true;
                    btnResetPass.Visible = true;
                    RequiredFieldValidatorNewPass.Visible = true;
                    RequiredFieldValidatorConfirmPass.Visible = true;
                    CompareValidatorConfirmNewPass.Visible = true;
                }
                else
                {
                    lblMsg.Text = "Your password reset link is expired or invalid... Please try again";
                }
            }
        }

        protected void btnResetPass_Click(object sender, EventArgs e)
        {
            if (txtNewPass.Text != "" || txtConfirmPass.Text != "" || txtNewPass == txtConfirmPass) 
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("Update tblUsers set Password=@p where Uid='" + Uid + "'", con);
                    cmd.Parameters.AddWithValue("@p", txtNewPass.Text.Trim());
                    cmd.Parameters.AddWithValue("@Uid", Uid);
                    cmd.ExecuteNonQuery();

                    MySqlCommand cmd2 = new MySqlCommand("Delete from ForgotPass where Uid=@Uid", con);
                    cmd2.ExecuteNonQuery();
                    Response.Write("<script> alert ('Password reset successfully done');   </script>");
                    Response.Redirect("~/ SignIn.aspx");
                }
            }
            else
            {
                Response.Write("<script> alert ('Password doesn't match');   </script>");
            }
        }
    }
}