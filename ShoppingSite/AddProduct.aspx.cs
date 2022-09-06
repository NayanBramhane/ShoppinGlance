using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Data.SqlClient;

namespace ShoppingSite
{
    public partial class AddProduct : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyShoppingDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBrand();
                BindCategory();
                BindGender();
                ddlSubCat.Enabled = false;
                ddlGender.Enabled = false;

                BindGridView();
            }
        }

        private void BindGender()
        {
            using (MySqlConnection con = new MySqlConnection(CS))
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

        private void BindCategory()
        {
            using (MySqlConnection con = new MySqlConnection(CS))
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
            using (MySqlConnection con = new MySqlConnection(CS))
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

        

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSubCat.Enabled = true;
            int MainCategoryID = Convert.ToInt32(ddlCategory.SelectedItem.Value);

            using (MySqlConnection con = new MySqlConnection(CS))
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

        protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(CS))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from tblSizes where BrandID='" + ddlBrand.SelectedItem.Value + "'and CategoryID='" + 
                    ddlCategory.SelectedItem.Value + "'and SubCategoryID='" + ddlSubCat.SelectedItem.Value + 
                    "'and GenderID='" + ddlGender.SelectedItem.Value + "'", con);                
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    cblSize.DataSource = dt;
                    cblSize.DataTextField = "SizeName";
                    cblSize.DataValueField = "SizeID";
                    cblSize.DataBind();
                }
            }
        }

        protected void ddlSubCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlSubCat.SelectedIndex != 0)
            {
                ddlGender.Enabled = true;
            }
            else
            {
                ddlGender.Enabled = false;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(CS))
            {
                MySqlCommand cmd = new MySqlCommand("sp_InsertProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_PName",txtProductName.Text);
                cmd.Parameters.AddWithValue("_PPrice",txtPrice.Text);
                cmd.Parameters.AddWithValue("_PSellPrice", txtSellPrice.Text);
                cmd.Parameters.AddWithValue("_PBrandID", ddlBrand.SelectedItem.Value);
                cmd.Parameters.AddWithValue("_PCategoryID", ddlCategory.SelectedItem.Value);
                cmd.Parameters.AddWithValue("_PSubCatID", ddlSubCat.SelectedItem.Value);
                cmd.Parameters.AddWithValue("_PGender", ddlGender.SelectedItem.Value);
                cmd.Parameters.AddWithValue("_PDescription", txtDescription.Text);
                cmd.Parameters.AddWithValue("_PProductDetails", txtPDetail.Text);
                //cmd.Parameters.AddWithValue("_PMaterialCare", txtMatCare.Text);   
                if (chFD.Checked == true)
                {
                    cmd.Parameters.AddWithValue("_FreeDelivery", 1.ToString());
                }
                else
                {
                    cmd.Parameters.AddWithValue("_FreeDelivery", 0.ToString());
                }

                if (ch30Ret.Checked == true)
                {
                    cmd.Parameters.AddWithValue("_30DayRet", 1.ToString());
                }
                else
                {
                    cmd.Parameters.AddWithValue("_30DayRet", 0.ToString());
                }

                if (cbCOD.Checked == true)
                {
                    cmd.Parameters.AddWithValue("_COD", 1.ToString());
                }
                else
                {
                    cmd.Parameters.AddWithValue("_COD", 0.ToString());
                }
                if (con.State == ConnectionState.Closed) { con.Open(); }
                Int64 PID = Convert.ToInt64(cmd.ExecuteScalar());


                // Insert size quantity
                for (int i = 0; i < cblSize.Items.Count; i++)
                {
                    if (cblSize.Items[i].Selected == true)
                    {
                        Int64 SizeID = Convert.ToInt64(cblSize.Items[i].Value);
                        int Quantity = Convert.ToInt32(txtQuantity.Text);

                        MySqlCommand cmd2 = new MySqlCommand("Insert into tblProductSizeQuantity (PID, SizeID, Quantity) values ('" + PID + "','" + 
                            SizeID + "','" + Quantity + "')", con);
                        cmd2.ExecuteNonQuery();
                    }
                }
                // Insert and Upload images
                if (fuImg01.HasFile)
                {
                    string SavePath = Server.MapPath("~/Images/ProductImages/") + PID;
                    if (!Directory.Exists(SavePath))
                    {
                        Directory.CreateDirectory(SavePath);
                    }
                    string Extension = Path.GetExtension(fuImg01.PostedFile.FileName);
                    fuImg01.SaveAs(SavePath + "\\" + txtProductName.Text.ToString().Trim() + "01" + Extension);

                    MySqlCommand cmd3 = new MySqlCommand("Insert into tblProductImages (PID, Name, Extension) values ('" + PID + "','" +
                        txtProductName.Text.ToString().Trim() + "01" + "','" + Extension + "')", con);
                    cmd3.ExecuteNonQuery();
                }
                // 2nd File Upload
                if (fuImg02.HasFile)
                {
                    string SavePath = Server.MapPath("~/Images/ProductImages/") + PID;
                    if (!Directory.Exists(SavePath))
                    {
                        Directory.CreateDirectory(SavePath);
                    }
                    string Extension = Path.GetExtension(fuImg02.PostedFile.FileName);
                    fuImg02.SaveAs(SavePath + "\\" + txtProductName.Text.ToString().Trim() + "02" + Extension);

                    MySqlCommand cmd4 = new MySqlCommand("Insert into tblProductImages (PID, Name, Extension) values ('" + PID + "','" +
                        txtProductName.Text.ToString().Trim() + "02" + "','" + Extension + "')", con);
                    cmd4.ExecuteNonQuery();
                }
                // 3rd File Upload
                if (fuImg03.HasFile)
                {
                    string SavePath = Server.MapPath("~/Images/ProductImages/") + PID;
                    if (!Directory.Exists(SavePath))
                    {
                        Directory.CreateDirectory(SavePath);
                    }
                    string Extension = Path.GetExtension(fuImg03.PostedFile.FileName);
                    fuImg03.SaveAs(SavePath + "\\" + txtProductName.Text.ToString().Trim() + "03" + Extension);

                    MySqlCommand cmd5 = new MySqlCommand("Insert into tblProductImages (PID, Name, Extension) values ('" + PID + "','" +
                        txtProductName.Text.ToString().Trim() + "03" + "','" + Extension + "')", con);
                    cmd5.ExecuteNonQuery();
                }
                // 4th File Upload
                if (fuImg04.HasFile)
                {
                    string SavePath = Server.MapPath("~/Images/ProductImages/") + PID;
                    if (!Directory.Exists(SavePath))
                    {
                        Directory.CreateDirectory(SavePath);
                    }
                    string Extension = Path.GetExtension(fuImg04.PostedFile.FileName);
                    fuImg04.SaveAs(SavePath + "\\" + txtProductName.Text.ToString().Trim() + "04" + Extension);

                    MySqlCommand cmd6 = new MySqlCommand("Insert into tblProductImages (PID, Name, Extension) values ('" + PID + "','" +
                        txtProductName.Text.ToString().Trim() + "04" + "','" + Extension + "')", con);
                    cmd6.ExecuteNonQuery();
                }
                // 5th File Upload
                if (fuImg05.HasFile)
                {
                    string SavePath = Server.MapPath("~/Images/ProductImages/") + PID;
                    if (!Directory.Exists(SavePath))
                    {
                        Directory.CreateDirectory(SavePath);
                    }
                    string Extension = Path.GetExtension(fuImg05.PostedFile.FileName);
                    fuImg05.SaveAs(SavePath + "\\" + txtProductName.Text.ToString().Trim() + "05" + Extension);

                    MySqlCommand cmd7 = new MySqlCommand("Insert into tblProductImages (PID, Name, Extension) values ('" + PID + "','" +
                        txtProductName.Text.ToString().Trim() + "05" + "','" + Extension + "')", con);
                    cmd7.ExecuteNonQuery();
                }
            }
        }

        private void BindGridView()
        {
            MySqlConnection con = new MySqlConnection(CS);
            MySqlCommand cmd = new MySqlCommand(" select distinct t1.PID,t1.PName,t1.PPrice,t1.PSellPrice,t2.Name as Brand,t3.CatName,t4.SubCatName, t5.GenderName as gender,t6.SizeName,t8.Quantity from tblProducts as t1  inner join tblBrands as t2 on t2.BrandID=t1.PBrandID  inner join tblCategory as t3 on t3.CatID=t1.PCategoryID  inner join tblSubCategory as t4 on t4.SubCatID=t1.PSubCatID   inner join tblGender as t5 on t5.GenderID =t1.PGender   inner join tblSizes as t6 on t6.SubCategoryID=t1.PSubCatID  inner join tblProductSizeQuantity as t8 on t8.PID=t1.PID order by t1.PName", con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
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
    }
}