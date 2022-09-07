using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace ShoppingSite
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnResetPass_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString))
            {
                con.Open();


                MySqlCommand cmd = new MySqlCommand("Select * from tblUsers where Email=@Email", con);
                cmd.Parameters.AddWithValue("@Email", txtEmailID.Text);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    String myGUID = Guid.NewGuid().ToString();
                    int Uid = Convert.ToInt32(dt.Rows[0][0]);

                    //DateTime now = DateTime.Now;
                    //String dat = now.ToString("yyyy-MM-dd H:mm:ss");

                    MySqlCommand cmd1 = new MySqlCommand("Insert into ForgotPass (Id,Uid,RequestDateTime) values ('" + myGUID + "','" + Uid + 
                        "',SYSDATE())", con);
                    cmd1.ExecuteNonQuery();

                    //Send reset link via email start--------------------------------------------------------

                    String ToEmailAddress = dt.Rows[0][3].ToString();
                    String Username = dt.Rows[0][1].ToString();
                    String EmailBody = "Hi, " + Username + ", <br/><br/>Click the link given below to reset your password<br/> <br/> " +
                        "https://localhost:44329/RecoverPassword.aspx?id=" + myGUID;

                    MailMessage PassRecMail = new MailMessage("Your mail.com", ToEmailAddress);

                    PassRecMail.Body = EmailBody;
                    PassRecMail.IsBodyHtml = true;
                    PassRecMail.Subject = "Reset Password";

                    using (SmtpClient client = new SmtpClient())
                    {
                        client.EnableSsl = true;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential("Your mail.com", "Your Password");
                        client.Host = "smtp.gmail.com";
                        client.Port = 587;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Send(PassRecMail);
                    }

                    //SmtpClient SMTP = new SmtpClient("smtp.gmail.com", 587);
                    //SMTP.Credentials = new NetworkCredential()
                    //{
                    //    UserName = "Your mail.com",
                    //    Password = "Your Password"
                    //};

                    //SMTP.EnableSsl = true;
                    //SMTP.Send(PassRecMail);

                    //Send reset link via email end --------------------------------------------------------------

                    lblResetPassMsg.Text = "Reset Link sent! Check your e-mail to reset password";
                    lblResetPassMsg.ForeColor = System.Drawing.Color.Green;
                    txtEmailID.Text = string.Empty;
                }
                else
                {
                    lblResetPassMsg.Text = "OOps! This Email does not exist... Try again";
                    lblResetPassMsg.ForeColor = System.Drawing.Color.Red;
                    txtEmailID.Text = string.Empty;
                    txtEmailID.Focus();
                }
            }
        }
    }
}
