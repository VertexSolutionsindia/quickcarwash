#region " Using "
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Security;
using Microsoft.Reporting.WebForms;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Permissions;
#endregion


public partial class Admin_WorkshopBilling_entry : System.Web.UI.Page
{
    
    public static int  company_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (User.Identity.IsAuthenticated)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con);
                SqlDataReader dr;
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    company_id = Convert.ToInt32(dr["com_id"].ToString());
                }
                con.Close();
            }

            DateTime date = DateTime.Now;
            TextBox8.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");

        
            TextBox15.Focus();
          
          
           
           

            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from currentfinancialyear where no='1'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                Label8.Text = dr10["financial_year"].ToString();
            }
            con10.Close();
            BindData();
            getinvoiceno();
            getinvoiceno1();
            show_category();
            show_VehicleNO();
            SearchMobileno();
            SearchCustomer();
            showrating();

            active();
            created();

           
        }

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                company_id = Convert.ToInt32(dr["com_id"].ToString());
             
                TextBox8.Focus();
                ImageButton IMG = (ImageButton)sender;
                GridViewRow ROW = (GridViewRow)IMG.NamingContainer;
                Label1.Text = ROW.Cells[1].Text;

                SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd10 = new SqlCommand("select * from WorkshopBilling_Entry where Invoice_id='" + Label1.Text + "' and year='" + Label8.Text + "' and Com_Id='" + company_id + "'", con10);
                SqlDataReader dr10;
                con10.Open();
                dr10 = cmd10.ExecuteReader();
                if (dr10.Read())
                {

                    TextBox8.Text = Convert.ToDateTime(dr10["date"]).ToString("MM/dd/yyyy");
                    TextBox15.Text = dr10["Customer_VehNo"].ToString();
                    TextBox2.Text = dr10["Mobile_No"].ToString();
                    TextBox3.Text = dr10["Workshop_name"].ToString();
                    TextBox4.Text = dr10["Address"].ToString();
                    DropDownList1.SelectedItem.Text = dr10["Service_Name"].ToString();
                    TextBox5.Text = dr10["Amount"].ToString();
                }

            }
            con.Close();
        }

       
    }
    protected void Button16_Click(object sender, EventArgs e)
    {

    }

  
    protected void Button17_Click(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                company_id = Convert.ToInt32(dr["com_id"].ToString());

                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd1 = new SqlCommand("delete from WorkshopBilling_Entry where Invoice_id='" + Label29.Text + "' and year='" + Label8.Text + "' and Com_Id='" + company_id + "' ", con1);
                con1.Open();
                cmd1.ExecuteNonQuery();
                con1.Close();


                Label31.Text = "Deleted successfuly";

                this.ModalPopupExtender3.Hide();
                BindData();
                getinvoiceno();
            }
            con.Close();
        }


    }
    protected void Button14_Click(object sender, EventArgs e)
    {
         if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con1);
            SqlDataReader dr1;
            con1.Open();
            dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                company_id = Convert.ToInt32(dr1["com_id"].ToString());

      
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            //Finiding checkbox control in gridview for particular row
            CheckBox chkdelete = (CheckBox)gvrow.FindControl("CheckBox3");
            //Condition to check checkbox selected or not
            if (chkdelete.Checked)
            {
                //Getting UserId of particular row using datakey value
                int usrid = Convert.ToInt32(gvrow.Cells[1].Text);
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

                con.Open();
                SqlCommand cmd = new SqlCommand("delete from WorkshopBilling_Entry where Invoice_id='" + usrid + "' and year='" + Label8.Text + "' and Com_Id='" + company_id + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert(' Workshop Invoice deleted successfully')", true);
        BindData();
        getinvoiceno();
            }
            con1.Close();
        }

    }
    
   
    private void active()
    {

    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;


        LinkButton Lnk = (LinkButton)sender;
        string name = Lnk.Text;
        Session["name"] = name;
        Response.Redirect("Account_show.aspx");


    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void created()
    {

    }
    protected void BindData()
    {
          
         if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                company_id = Convert.ToInt32(dr10["com_id"].ToString());

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from WorkshopBilling_Entry where year='" + Label8.Text + "' and Com_Id='" + company_id + "' ORDER BY Invoice_id asc", con);
        DataTable dt1 = new DataTable();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
            }
            con10.Close();
        }
    }
    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {
          if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                company_id = Convert.ToInt32(dr10["com_id"].ToString());
        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.NamingContainer;
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

        con.Open();
        SqlCommand cmd = new SqlCommand("delete from WorkshopBilling_Entry where Invoice_id='" + row.Cells[1].Text + "' and year='" + Label8.Text + "' and Com_Id='" + company_id + "' ", con);
        cmd.ExecuteNonQuery();
        con.Close();

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Worksop Invoice Deleted successfully')", true);

        BindData();
        getinvoiceno();
            }
            con10.Close();
        }
    }


    private void getinvoiceno()
    {
        
          if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                company_id = Convert.ToInt32(dr10["com_id"].ToString());
      
        int a;

        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        con1.Open();
        string query = "Select Max(Invoice_id) from WorkshopBilling_Entry where Com_Id='" + company_id + "' and year='" + Label8.Text + "' ";
        SqlCommand cmd1 = new SqlCommand(query, con1);
        SqlDataReader dr = cmd1.ExecuteReader();
        if (dr.Read())
        {
            string val = dr[0].ToString();
            if (val == "")
            {
                Label1.Text = "1";
            }
            else
            {
                a = Convert.ToInt32(dr[0].ToString());
                TextBox13.Text = a.ToString();
                a = a + 1;
                Label1.Text = a.ToString();
            }
        }
        con1.Close();
            }
            con10.Close();
        }
    }
    private void getinvoiceno1()
    {

        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                company_id = Convert.ToInt32(dr10["com_id"].ToString());

                int a;

                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                con1.Open();
                string query = "Select Max(WorkShop_id) from Workshop_Entry where Com_Id='" + company_id + "'";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        Label9.Text = "1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        a = a + 1;
                        Label9.Text = a.ToString();
                    }
                }
                con1.Close();
            }
            con10.Close();
        }
    }
   
    private void show_category()
    {
       if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                company_id = Convert.ToInt32(dr10["com_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Product_entry where Com_Id='" + company_id + "' ORDER BY code asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Service_name";
        DropDownList1.DataValueField = "code";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new ListItem("All", "0"));

        DropDownList7.DataSource = ds;
        DropDownList7.DataTextField = "Service_name";
        DropDownList7.DataValueField = "code";
        DropDownList7.DataBind();
        DropDownList7.Items.Insert(0, new ListItem("All", "0"));

        con.Close();
            }
            con10.Close();
        }
    }
    private void show_VehicleNO()
    {
         if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                company_id = Convert.ToInt32(dr10["com_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from WorkshopBilling_Entry where Com_Id='" + company_id + "' ORDER BY Invoice_id asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


     

        DropDownList2.DataSource = ds;
        DropDownList2.DataTextField = "Customer_VehNo";
        DropDownList2.DataValueField = "Invoice_id";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("All", "0"));

        DropDownList4.DataSource = ds;
        DropDownList4.DataTextField = "Customer_VehNo";
        DropDownList4.DataValueField = "Invoice_id";
        DropDownList4.DataBind();
        DropDownList4.Items.Insert(0, new ListItem("All", "0"));

        con.Close();
            }
            con10.Close();
        }
    }

    private void SearchMobileno()
    {
          if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                company_id = Convert.ToInt32(dr10["com_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Workshop_Entry where Com_Id='" + company_id + "'  ORDER BY WorkShop_id asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList6.DataSource = ds;
        DropDownList6.DataTextField = "Mobile_no";
        DropDownList6.DataValueField = "WorkShop_id";
        DropDownList6.DataBind();
        DropDownList6.Items.Insert(0, new ListItem("All", "0"));

        con.Close();
            }
            con10.Close();
        }
    }

    private void SearchCustomer()
    {
          if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                company_id = Convert.ToInt32(dr10["com_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Workshop_Entry where Com_Id='" + company_id + "' ORDER BY WorkShop_id asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList5.DataSource = ds;
        DropDownList5.DataTextField = "WorkShop_Name";
        DropDownList5.DataValueField = "WorkShop_id";
        DropDownList5.DataBind();
        DropDownList5.Items.Insert(0, new ListItem("All", "0"));
        con.Close();
            }
            con10.Close();
        }
    }

    protected void LoginLink_OnClick(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("~/login.aspx");

    }

    protected void btnRandom_Click(object sender, EventArgs e)
    {
        Session["name1"] = "";
        Response.Redirect("~/Admin/Category_Add.aspx");
    }

    private void showcustomertype()
    {

    }
    private void showrating()
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (GridView1.PageIndex + 1) + " of " + GridView1.PageCount;
        }


    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
         
    }




    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
          if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                company_id = Convert.ToInt32(dr10["com_id"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        con.Open();
        SqlCommand cmd2 = new SqlCommand("Select * from Product_entry where code='" + DropDownList1.SelectedItem.Value + "' and Com_Id='" + company_id + "'", con);
        SqlDataReader dr1;
        dr1 = cmd2.ExecuteReader();
        if (dr1.Read())
        {
            TextBox5.Text = dr1["Amount"].ToString();
            TextBox14.Text = dr1["Service_name"].ToString();
        }
        con.Close();
            }
            con10.Close();
        }
    }



    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                company_id = Convert.ToInt32(dr10["com_id"].ToString());

        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from Billing_Entry where Customer_VehNo='" + TextBox1.Text + "' and year='" + Label8.Text + "' and Com_Id='" + company_id + "'", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
            }
            con10.Close();
        }
    }
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                company_id = Convert.ToInt32(dr10["com_id"].ToString());
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                con.Open();
                SqlCommand cmd2 = new SqlCommand("Select * from Workshop_Entry where WorkShop_Name='" + TextBox3.Text + "' and Com_Id='" + company_id + "'", con);
                SqlDataReader dr1;
                dr1 = cmd2.ExecuteReader();
                if (dr1.Read())
                {

                    TextBox2.Text = dr1["Mobile_no"].ToString();
                    TextBox4.Text = dr1["WorkShop_Add"].ToString();


                }
                con.Close();
            }
            con10.Close();
        }
    }
    protected void TextBox3_Load(object sender, EventArgs e)
    {
       
    }

    protected void TextBox8_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TextBox14_TextChanged(object sender, EventArgs e)
    {

    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
         if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                company_id = Convert.ToInt32(dr10["com_id"].ToString());
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from WorkshopBilling_Entry where Customer_VehNo='" + DropDownList2.SelectedItem.Text + "' and year='" + Label8.Text + "' and Com_Id='" + company_id + "' ORDER BY Invoice_id asc", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
                  }
            con10.Close();
        }
    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
         if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                company_id = Convert.ToInt32(dr10["com_id"].ToString());
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from WorkshopBilling_Entry where Workshop_name='" + DropDownList5.SelectedItem.Text + "' and  year='" + Label8.Text + "' and Com_Id='" + company_id + "' ORDER BY Invoice_id asc", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
                  }
            con10.Close();
        }
    }
    protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
    {
         if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                company_id = Convert.ToInt32(dr10["com_id"].ToString());
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from WorkshopBilling_Entry where Mobile_No='" + DropDownList6.SelectedItem.Text + "' and year='" + Label8.Text + "' and Com_Id='" + company_id + "' ORDER BY Invoice_id asc", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
            }
            con10.Close();
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
      
        DropDownList2.Items.Clear();
        DropDownList5.Items.Clear();
        DropDownList6.Items.Clear();
        TextBox1.Text = "";
        show_VehicleNO();
        SearchMobileno();
        SearchCustomer();
        BindData();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchWorkshopName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Workshop_name from Workshop_Entry where Com_Id=@Com_Id and " +
                "Workshop_name like @Workshop_name + '%'";
                cmd.Parameters.AddWithValue("@Workshop_name", prefixText);
                cmd.Parameters.AddWithValue("@Com_id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Workshop_name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }

     #region " [ Button Event ] "
    protected void Button6_Click(object sender, EventArgs e)
    {
        if (TextBox13.Text != "")
        {
            // select appropriate contenttype, while binary transfer it identifies filetype
            string contentType = string.Empty;
            if (DropDownList8.SelectedValue.Equals(".pdf"))
                contentType = "application/pdf";
            if (DropDownList8.SelectedValue.Equals(".doc"))
                contentType = "application/ms-word";
            if (DropDownList8.SelectedValue.Equals(".xls"))
                contentType = "application/xls";

            DataTable dsData = new DataTable();

            DataSet ds = null;
            SqlDataAdapter da = null;



            try
            {
                string constring = ConfigurationManager.AppSettings["connection"];
                using (SqlConnection con = new SqlConnection(constring))
                {
                    using (SqlCommand cmd = new SqlCommand("Workshop_billing", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@No", int.Parse(TextBox13.Text));
                        //cmd.Parameters.AddWithValue("@Com_id", (company_id));
                        da = new SqlDataAdapter(cmd);
                        ds = new DataSet();
                        con.Open();
                        da.Fill(ds);
                        con.Close();

                    }
                }
            }
            catch
            {
                throw;
            }



            dsData = ds.Tables[0];

            string FileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + DropDownList8.SelectedValue;
            string extension;
            string encoding;
            string mimeType;
            string[] streams;
            Warning[] warnings;

            LocalReport report = new LocalReport();
            report.ReportPath = Server.MapPath("~/Admin/Report2.rdlc");
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet2";//This refers to the dataset name in the RDLC file
            rds.Value = dsData;
            report.DataSources.Add(rds);

            Byte[] mybytes = report.Render(DropDownList8.SelectedItem.Text, null,
                            out extension, out encoding,
                            out mimeType, out streams, out warnings); //for exporting to PDF
            using (FileStream fs = File.Create(Server.MapPath("~/img/") + FileName))
            {
                fs.Write(mybytes, 0, mybytes.Length);
            }

            Response.ClearHeaders();
            Response.ClearContent();
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = contentType;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
            Response.WriteFile(Server.MapPath("~/img/" + FileName));
            Response.Flush();
            Response.Close();
            Response.End();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Enter the INVOICE NUMBER')", true);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }

     #endregion



    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=gvtoexcel.xls");
        Response.ContentType = "application/excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GridView1.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void TextBox15_TextChanged(object sender, EventArgs e)
    {
       
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers1(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Customer_VehNo from Customer_Entry where Com_Id=@Com_Id and " +
                "Customer_VehNo like @Customer_VehNo + '%'";
                cmd.Parameters.AddWithValue("@Customer_VehNo", prefixText);
                cmd.Parameters.AddWithValue("@Com_id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Customer_VehNo"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                company_id = Convert.ToInt32(dr10["com_id"].ToString());

                if (TextBox15.Text == "All")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Enter valid Vehicle No')", true);
                }
                else if (DropDownList1.SelectedItem.Text == "All")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Select valid Service Type')", true);
                }
                else
                {
                    SqlConnection con11 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd11 = new SqlCommand("select * from Workshop_Entry where WorkShop_Name='" + TextBox3.Text + "' ", con11);
                    con11.Open();
                    SqlDataReader dr11;
                    dr11 = cmd11.ExecuteReader();
                    if (dr11.HasRows)
                    {

                    }
                    else
                    {

                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("insert into Workshop_Entry values(@WorkShop_id,@WorkShop_Name,@WorkShop_Add,@Mobile_no,@Com_Id)", CON);
                        cmd.Parameters.AddWithValue("@WorkShop_id", Label9.Text);
                        cmd.Parameters.AddWithValue("@WorkShop_Name", TextBox3.Text);
                        cmd.Parameters.AddWithValue("@WorkShop_Add", TextBox4.Text);
                        cmd.Parameters.AddWithValue("@Mobile_no", TextBox2.Text);
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);

                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();

                    }
                    con11.Close();

                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from WorkshopBilling_Entry where Invoice_id='" + Label1.Text + "' and year='" + Label8.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        int value = 0;
                         SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("update WorkshopBilling_Entry set date=@date,Workshop_name=@Workshop_name,Mobile_no=@Mobile_No,Address=@Address,Amount=@Amount,Com_Id=@Com_Id,Customer_VehNo=@Customer_VehNo,Service_Name=@Service_Name,status=@status,value=@value,year=@year where Invoice_id=@Invoice_id and year=@year and Com_Id=@Com_Id", CON);
        cmd.Parameters.AddWithValue("@Invoice_id", Label1.Text);
        cmd.Parameters.AddWithValue("@date",Convert.ToDateTime( TextBox8.Text).ToString("MM-dd-yyyy"));
        cmd.Parameters.AddWithValue("@Workshop_name", HttpUtility.HtmlDecode(TextBox3.Text));
        cmd.Parameters.AddWithValue("@Mobile_No", HttpUtility.HtmlDecode(TextBox2.Text));
        cmd.Parameters.AddWithValue("@Address", HttpUtility.HtmlDecode(TextBox4.Text));
        cmd.Parameters.AddWithValue("@Amount", HttpUtility.HtmlDecode(TextBox5.Text));
        cmd.Parameters.AddWithValue("@Com_Id", company_id);
        cmd.Parameters.AddWithValue("@Customer_VehNo", HttpUtility.HtmlDecode(TextBox15.Text));
        cmd.Parameters.AddWithValue("@Service_Name", HttpUtility.HtmlDecode(DropDownList1.SelectedItem.Text));
        cmd.Parameters.AddWithValue("@status", "Sales-" + TextBox14.Text);
        cmd.Parameters.AddWithValue("@value", value);
        cmd.Parameters.AddWithValue("@year", Label8.Text);
       
        CON.Open();
        cmd.ExecuteNonQuery();
        CON.Close();

        float b11 = 0;
        float f11 = 0;
        float c11 = 0;

        SqlConnection con100 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
        SqlCommand check_User_Name100 = new SqlCommand("SELECT * FROM receive_amount_status WHERE Buyer = @Buyer and Com_Id='" + company_id + "' and year='" + Label8.Text + "' ", con100);
        check_User_Name100.Parameters.AddWithValue("@Buyer", TextBox3.Text);
        con100.Open();
        SqlDataReader reader100 = check_User_Name100.ExecuteReader();
        if (reader100.HasRows)
        {
            SqlConnection con111 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
            SqlCommand cmd111 = new SqlCommand("Select * from receive_amount where Buyer='" + TextBox3.Text + "' and invoice_no='" + Label1.Text + "'  and Com_Id='" + company_id + "' and year='" + Label8.Text + "'", con111);
            con111.Open();
            SqlDataReader dr111;
            dr111 = cmd111.ExecuteReader();
            if (dr111.Read())
            {

                b11 = float.Parse(dr111["pending_amount"].ToString());






                SqlConnection con27 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                SqlCommand cd27 = new SqlCommand("update receive_amount_status set pending_amount=pending_amount-@pending_amount where Buyer='" + TextBox3.Text + "' and Com_Id='" + company_id + "' and year='" + Label8.Text + "'", con27);
                cd27.Parameters.AddWithValue("@pending_amount", b11);
                con27.Open();
                cd27.ExecuteNonQuery();
                con27.Close();

                SqlConnection con272 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                SqlCommand cd272 = new SqlCommand("update receive_amount set pending_amount=pending_amount-@pending_amount,outstanding=outstanding-@outstanding where Buyer='" + TextBox3.Text + "' and  invoice_no='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label8.Text + "' ", con272);
                cd272.Parameters.AddWithValue("@pending_amount", b11);
                cd272.Parameters.AddWithValue("@outstanding", b11);
                con272.Open();
                cd272.ExecuteNonQuery();
                con272.Close();

                SqlConnection con271 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                SqlCommand cd271 = new SqlCommand("update receive_amount_status set pending_amount=pending_amount+@pending_amount where Buyer='" + TextBox3.Text + "' and Com_Id='" + company_id + "' and year='" + Label8.Text + "'", con271);
                cd271.Parameters.AddWithValue("@pending_amount", float.Parse(TextBox5.Text));
                con271.Open();
                cd271.ExecuteNonQuery();
                con271.Close();



                float paid_amount = 0;
                string status1 = "Bill";
                SqlConnection con26 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                SqlCommand cmd26 = new SqlCommand("update receive_amount set Estimate_value=@Estimate_value,address=@address,total_amount=@total_amount,pay_amount=@pay_amount,pending_amount=@pending_amount,outstanding=outstanding+@outstanding,status=@status where Buyer='" + TextBox3.Text + "' AND invoice_no='" + Label1.Text + "' and year='" + Label8.Text + "'", con26);


                cmd26.Parameters.AddWithValue("@Estimate_value", float.Parse(TextBox5.Text));
                cmd26.Parameters.AddWithValue("@address", TextBox3.Text);

                cmd26.Parameters.AddWithValue("@total_amount", float.Parse(TextBox5.Text));
                cmd26.Parameters.AddWithValue("@pay_amount", paid_amount);
                cmd26.Parameters.AddWithValue("@pending_amount", float.Parse(TextBox5.Text));
                cmd26.Parameters.AddWithValue("@outstanding", float.Parse(TextBox5.Text));
                cmd26.Parameters.AddWithValue("@status", status1);


                con26.Open();
                cmd26.ExecuteNonQuery();
                con26.Close();



            }
            con111.Close();
        }

        con100.Close();




        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Workshop Invoice updated successfully')", true);
        BindData();
        getinvoiceno();
        show_category();
        show_VehicleNO();

        TextBox3.Text = "";
        TextBox2.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox15.Text = "";
        DateTime date = DateTime.Now;
        TextBox8.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");

                    }
                    else
                    {
                        int value = 0;
                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("insert into WorkshopBilling_Entry values(@Invoice_id,@date,@Workshop_name,@Mobile_No,@Address,@Amount,@Com_Id,@Customer_VehNo,@Service_Name,@status,@value,@year)", CON);
                        cmd.Parameters.AddWithValue("@Invoice_id", Label1.Text);
                        cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(TextBox8.Text).ToString("MM-dd-yyyy"));
                        cmd.Parameters.AddWithValue("@Workshop_name", HttpUtility.HtmlDecode(TextBox3.Text));
                        cmd.Parameters.AddWithValue("@Mobile_No", HttpUtility.HtmlDecode(TextBox2.Text));
                        cmd.Parameters.AddWithValue("@Address", HttpUtility.HtmlDecode(TextBox4.Text));
                        cmd.Parameters.AddWithValue("@Amount", HttpUtility.HtmlDecode(TextBox5.Text));
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        cmd.Parameters.AddWithValue("@Customer_VehNo", HttpUtility.HtmlDecode(TextBox15.Text));
                        cmd.Parameters.AddWithValue("@Service_Name", HttpUtility.HtmlDecode(DropDownList1.SelectedItem.Text));
                        cmd.Parameters.AddWithValue("@status", "Sales-" + TextBox14.Text);
                        cmd.Parameters.AddWithValue("@value", value);
                        cmd.Parameters.AddWithValue("@year", Label8.Text);
                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();


                        int a111 = 0;
                        float b11 = 0;
                        float f11 = 0;
                        float c11 = 0;
                        SqlConnection con100 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                        SqlCommand cmd100 = new SqlCommand("SELECT * FROM receive_amount_status WHERE Buyer = @Buyer and Com_Id='" + company_id + "'", con100);
                        cmd100.Parameters.AddWithValue("@Buyer", TextBox3.Text);
                        con100.Open();
                        SqlDataReader reader1 = cmd100.ExecuteReader();
                        if (reader1.HasRows)
                        {
                            SqlConnection con111 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                            SqlCommand cmd111 = new SqlCommand("Select * from receive_amount_status where Buyer='" + TextBox3.Text + "' and  Com_Id='" + company_id + "' ", con111);
                            con111.Open();
                            SqlDataReader dr111;
                            dr111 = cmd111.ExecuteReader();
                            if (dr111.Read())
                            {

                                b11 = float.Parse(dr111["pending_amount"].ToString());


                                f11 = float.Parse(TextBox5.Text);

                                c11 = (b11 + f11);




                                float paid_amount = 0;
                                string status1 = "Bill";
                                SqlConnection con24 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                                SqlCommand cmd24 = new SqlCommand("insert into receive_amount values(@Buyer,@Pay_date,@Estimate_value,@address,@total_amount,@pay_amount,@pending_amount,@outstanding,@invoice_no,@Com_Id,@status,@year)", con24);
                                cmd24.Parameters.AddWithValue("@Buyer", TextBox3.Text);
                                cmd24.Parameters.AddWithValue("@pay_date", TextBox8.Text);
                                cmd24.Parameters.AddWithValue("@Estimate_value", float.Parse(TextBox5.Text));
                                cmd24.Parameters.AddWithValue("@address", TextBox4.Text);

                                cmd24.Parameters.AddWithValue("@total_amount", float.Parse(string.Format("{0:0.00}", TextBox5.Text)));
                                cmd24.Parameters.AddWithValue("@pay_amount", paid_amount);
                                cmd24.Parameters.AddWithValue("@pending_amount", float.Parse(string.Format("{0:0.00}", TextBox5.Text)));
                                cmd24.Parameters.AddWithValue("@outstanding", float.Parse(string.Format("{0:0.00}", c11)));

                                cmd24.Parameters.AddWithValue("@invoice_no", Label1.Text);
                                cmd24.Parameters.AddWithValue("@Com_Id", company_id);
                                cmd24.Parameters.AddWithValue("@status", status1);
                                cmd24.Parameters.AddWithValue("@year", Label8.Text);
                                con24.Open();
                                cmd24.ExecuteNonQuery();
                                con24.Close();


                                SqlConnection con23 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                                SqlCommand cmd23 = new SqlCommand("update receive_amount_status set address=@address,total_amount=total_amount+@total_amount,pending_amount=pending_amount+@pending_amount where Buyer='" + TextBox3.Text + "' and Com_Id='" + company_id + "' and year='"+Label8.Text+"'", con23);

                                cmd23.Parameters.AddWithValue("@address", TextBox4.Text);

                                cmd23.Parameters.AddWithValue("@total_amount", float.Parse(string.Format("{0:0.00}", TextBox5.Text)));

                                cmd23.Parameters.AddWithValue("@pending_amount", float.Parse(string.Format("{0:0.00}", TextBox5.Text)));

                                con23.Open();
                                cmd23.ExecuteNonQuery();
                                con23.Close();


                            }

                            con111.Close();






                        }
                        else
                        {
                            float paid_amount = 0;
                            string status1 = "Bill";
                            SqlConnection con23 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                            SqlCommand cmd23 = new SqlCommand("insert into receive_amount_status values(@Buyer,@address,@total_amount,@pending_amount,@paid_amount,@Com_Id,@year)", con23);
                            cmd23.Parameters.AddWithValue("@Buyer", TextBox3.Text);
                            cmd23.Parameters.AddWithValue("@address", TextBox4.Text);

                            cmd23.Parameters.AddWithValue("@total_amount", float.Parse(string.Format("{0:0.00}", TextBox5.Text)));

                            cmd23.Parameters.AddWithValue("@pending_amount", float.Parse(string.Format("{0:0.00}", TextBox5.Text)));
                            cmd23.Parameters.AddWithValue("@paid_amount", paid_amount);
                            cmd23.Parameters.AddWithValue("@Com_Id", company_id);
                            cmd23.Parameters.AddWithValue("@year", Label8.Text);
                            con23.Open();
                            cmd23.ExecuteNonQuery();
                            con23.Close();
                            string return_by = "";
                            int value1 = 0;
                            SqlConnection con24 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                            SqlCommand cmd24 = new SqlCommand("insert into receive_amount values(@Buyer,@Pay_date,@Estimate_value,@address,@total_amount,@pay_amount,@pending_amount,@outstanding,@invoice_no,@Com_Id,@status,@year)", con24);
                            cmd24.Parameters.AddWithValue("@Buyer", TextBox3.Text);
                            cmd24.Parameters.AddWithValue("@pay_date", TextBox8.Text);
                            cmd24.Parameters.AddWithValue("@Estimate_value", float.Parse(TextBox5.Text));
                            cmd24.Parameters.AddWithValue("@address", TextBox4.Text);

                            cmd24.Parameters.AddWithValue("@total_amount", float.Parse(string.Format("{0:0.00}", TextBox5.Text)));
                            cmd24.Parameters.AddWithValue("@pay_amount", paid_amount);
                            cmd24.Parameters.AddWithValue("@pending_amount", float.Parse(string.Format("{0:0.00}", TextBox5.Text)));
                            cmd24.Parameters.AddWithValue("@outstanding", float.Parse(string.Format("{0:0.00}", TextBox5.Text)));
                            cmd24.Parameters.AddWithValue("@invoice_no", Label1.Text);
                            cmd24.Parameters.AddWithValue("@Com_Id", company_id);
                            cmd24.Parameters.AddWithValue("@status", status1);
                            cmd24.Parameters.AddWithValue("@year", Label8.Text);
                            con24.Open();
                            cmd24.ExecuteNonQuery();
                            con24.Close();


                        }
                        con100.Close();





                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Workshop Invoice created successfully')", true);
                        BindData();
                        getinvoiceno();
                        show_category();
                        show_VehicleNO();

                        TextBox3.Text = "";
                        TextBox2.Text = "";
                        TextBox4.Text = "";
                        TextBox5.Text = "";
                        TextBox6.Text = "";
                        TextBox15.Text = "";
                        DateTime date = DateTime.Now;
                        TextBox8.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");
                    }
                    con1.Close();







                }

            }
            con10.Close();
        }
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        DateTime date = DateTime.Now;
        TextBox8.Text = Convert.ToDateTime(date).ToString("MM-dd-yyyy");

        TextBox3.Text = "";
        TextBox2.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox1.Text = "";
        TextBox15.Text = "";
        getinvoiceno();
        show_category();
        show_VehicleNO();
        
    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        DateTime date = DateTime.Now;
        TextBox8.Text = Convert.ToDateTime(date).ToString("MM-dd-yyyy");

        TextBox3.Text = "";
        TextBox2.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox1.Text = "";
        TextBox15.Text = "";
        getinvoiceno();
        show_category();
        show_VehicleNO();
        
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());

                if (Convert.ToInt32(Label1.Text) > Convert.ToInt32(1))
                {
                  
                    Label1.Text = (Convert.ToInt32(Label1.Text) - 1).ToString();
                }

                SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("select * from WorkshopBilling_Entry where Invoice_id='" + Label1.Text + "' and Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {

                    TextBox8.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-MM-yyyy");
                    TextBox6.Text = dr2["Mobile_no"].ToString();

                    TextBox3.Text = dr2["Workshop_name"].ToString();
                    TextBox2.Text = dr2["Mobile_No"].ToString();
                    TextBox4.Text = dr2["Address"].ToString();
                    TextBox15.Text = dr2["Customer_VehNo"].ToString();
                    DropDownList1.SelectedItem.Text = dr2["Service_Name"].ToString();
                    TextBox5.Text = dr2["Amount"].ToString();
              


                }
                con2.Close();


             
            }
            con1000.Close();
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());

                SqlConnection con21 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd21 = new SqlCommand("select max(Invoice_id) from WorkshopBilling_Entry where  Com_Id='" + company_id + "' and year='" + Label8.Text + "' ", con21);
                SqlDataReader dr21;
                con21.Open();
                dr21 = cmd21.ExecuteReader();
                if (dr21.Read())
                {
                    int value = Convert.ToInt32(dr21[0].ToString());
                    if (Convert.ToInt32(Label1.Text) < Convert.ToInt32(value + 1))
                    {
                       
                        Label1.Text = (Convert.ToInt32(Label1.Text) + 1).ToString();
                    }
                }
                con21.Close();
                SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("select * from WorkshopBilling_Entry where Invoice_id='" + Label1.Text + "' and Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {

                    TextBox8.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-MM-yyyy");
                    TextBox6.Text = dr2["Mobile_no"].ToString();

                    TextBox3.Text = dr2["Workshop_name"].ToString();
                    TextBox2.Text = dr2["Mobile_No"].ToString();
                    TextBox4.Text = dr2["Address"].ToString();
                    TextBox15.Text = dr2["Customer_VehNo"].ToString();
                    DropDownList1.SelectedItem.Text = dr2["Service_Name"].ToString();
                    TextBox5.Text = dr2["Amount"].ToString();
              



                  
                }
                else
                {


                    DateTime date = DateTime.Now;
                    TextBox8.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");

                    TextBox3.Text = "";
                    TextBox2.Text = "";
                    TextBox4.Text = "";
                    TextBox5.Text = "";
                    TextBox6.Text = "";
                    TextBox1.Text = "";
                    TextBox15.Text = "";
                    getinvoiceno();
                    show_category();
                    show_VehicleNO();


                }
                con2.Close();
            }
            con1000.Close();
        }
    }
}