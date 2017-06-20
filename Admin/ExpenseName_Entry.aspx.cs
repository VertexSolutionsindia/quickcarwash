﻿#region " Using "
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
using System.Drawing;
#endregion


public partial class Admin_ExpenseName_Entry : System.Web.UI.Page
{
    DataTable dt = null;
    public static int company_id = 0;
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

            getinvoiceno();
            SearchServicename();
            showrating();
            BindData();
          
            active();
            created();


           

        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton IMG = (ImageButton)sender;
        GridViewRow ROW = (GridViewRow)IMG.NamingContainer;
        Label29.Text = ROW.Cells[1].Text;
        TextBox16.Text = ROW.Cells[2].Text;

        this.ModalPopupExtender3.Show();
    }
    protected void Button16_Click(object sender, EventArgs e)
    {


        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("update ExpenceName_Add set Expense_Name='" + HttpUtility.HtmlDecode(TextBox16.Text) + 
            "' where ExpName_Code='" + Label29.Text + "'  and Com_Id='" + company_id + "' ", CON);
        CON.Open();
        cmd.ExecuteNonQuery();
        CON.Close();
        Label31.Text = "updated successfuly";

        this.ModalPopupExtender3.Hide();
        BindData();
        getinvoiceno();
    }
    protected void Button17_Click(object sender, EventArgs e)
    {
       

        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd1 = new SqlCommand("delete from ExpenceName_Add where ExpName_Code='" + Label29.Text + "' and Com_Id='" + company_id + "' ", con1);
        con1.Open();
        cmd1.ExecuteNonQuery();
        con1.Close();

       

        Label31.Text = "Deleted successfuly";

        this.ModalPopupExtender3.Hide();
        BindData();
        getinvoiceno();

    }
    protected void Button14_Click(object sender, EventArgs e)
    {
      
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
                SqlCommand cmd = new SqlCommand("delete from Product_entry where code='" + usrid+"'  and Com_Id='" + company_id + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();

               

            }
        }
        BindData();
        getinvoiceno();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        if (TextBox4.Text != "")
        {


            SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1 = new SqlCommand("select * from ExpenceName_Add where Expense_Name='" + TextBox4.Text + "' ", con1);
            con1.Open();
            SqlDataReader dr1;
            dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Service already exist')", true);
                TextBox4.Text = "";
            }
            else
            {
                SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("insert into ExpenceName_Add values(@ExpName_Code,@Expense_Name,@Com_Id)", CON);
                cmd.Parameters.AddWithValue("@ExpName_Code", Label1.Text);
                cmd.Parameters.AddWithValue("@Expense_Name", TextBox4.Text);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);

                CON.Open();
                cmd.ExecuteNonQuery();
                CON.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Expense Name Added successfully')", true);
                BindData();
                SearchServicename();
                getinvoiceno();

                TextBox4.Text = "";

            }

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter Expense name')", true);

        }
    }
    private void SaveDetail(GridViewRow row)
    {
        

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox4.Text = "";
        getinvoiceno();
        BindData();


      
       
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

    private void created()
    {

    }
    protected void BindData()
    {
       

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from ExpenceName_Add where Com_Id='" + company_id + "' ORDER BY ExpName_Code asc", con);
        DataTable dt1 = new DataTable();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();

    }
    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {
      

        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.NamingContainer;
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("delete from ExpenceName_Add where ExpName_Code='" + row.Cells[1].Text + "' and Com_Id='" + company_id + "' ", con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

       
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Expense Name entry deleted successfully')", true);

        BindData();

        getinvoiceno();


    }
    
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]



    private void SearchServicename()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from ExpenceName_Add where Com_Id='" + company_id + "' ORDER BY ExpName_Code asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList2.DataSource = ds;
        DropDownList2.DataTextField = "Expense_Name";
        DropDownList2.DataValueField = "ExpName_Code";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("All", "0"));
        con.Close();  
       }


    private void show_category()
    {
       

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from category where Com_Id='" + company_id + "' ORDER BY category_id asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


     
    
     
     
        DropDownList2.Items.Insert(0, new ListItem("All", "0"));


       

      
        con.Close();
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

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from ExpenceName_Add where Expense_Name='" + TextBox1.Text + "' and Com_Id='" + company_id + "'", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }
    private void getinvoiceno()
    {
       
        int a;

        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        con1.Open();
        string query = "Select COUNT(ExpName_Code) from ExpenceName_Add where Com_Id='" + company_id + "'";
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
                a = a + 1;
                Label1.Text = a.ToString();
            }
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
       
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedItem.Text == "All")
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand CMD = new SqlCommand("select * from ExpenceName_Add where  Com_Id='" + company_id + "' ORDER BY ExpName_Code asc", con1);
            DataTable dt1 = new DataTable();
            con1.Open();
            SqlDataAdapter da1 = new SqlDataAdapter(CMD);
            da1.Fill(dt1);
            GridView1.DataSource = dt1;
            GridView1.DataBind();
        }
        else
        {
        
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from ExpenceName_Add where Expense_Name='" + DropDownList2.SelectedItem.Text + "' and Com_Id='" + company_id + "' ORDER BY ExpName_Code asc", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        DropDownList2.Items.Clear();
        SearchServicename();
        BindData();
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct Expense_Name from ExpenceName_Add where Com_Id=@Com_Id and " +
                "Expense_Name like @Expense_Name + '%'";
                cmd.Parameters.AddWithValue("@Expense_Name", prefixText);
                cmd.Parameters.AddWithValue("@Com_id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Expense_Name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
}