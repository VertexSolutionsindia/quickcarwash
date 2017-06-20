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

public partial class Admin_Main : System.Web.UI.Page
{
    
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

            TextBox3.Focus();
           
          
            getinvoiceno();
            show_category();
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
        Label16.Text = ROW.Cells[1].Text;
        TextBox11.Text = ROW.Cells[2].Text;
        this.ModalPopupExtender2.Show();

    }
    protected void Button9_Click(object sender, EventArgs e)
    {

        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("update category set categoryname=@categoryname where category_id='" +Label16.Text + "' and Com_Id='" + company_id + "'  ", CON);
        cmd.Parameters.AddWithValue("@categoryname", TextBox11.Text);
        CON.Open();
        cmd.ExecuteNonQuery();
        CON.Close();
        Label18.Text = "updated successfuly";
        this.ModalPopupExtender2.Show();
        showcustomertype();
        show_category();
        BindData();
        getinvoiceno();
       
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
      
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("delete from category where category_id='" + Label16.Text + "' and Com_Id='" + company_id + "' ", con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        Label18.Text = "Deleted successfuly";
        BindData();

        getinvoiceno();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox3.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter category name')", true);
        }
        else
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1 = new SqlCommand("select * from category where categoryname='" + TextBox3.Text + "' AND Com_Id='" + company_id + "'  ", con1);
            con1.Open();
            SqlDataReader dr1;
            dr1=cmd1.ExecuteReader();
            if(dr1.HasRows)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Category already exist')", true);
                TextBox3.Text = "";
            }
            else
            {

          
            SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd = new SqlCommand("insert into category values(@category_id,@categoryname,@Com_Id)", CON);
            cmd.Parameters.AddWithValue("@category_id", Label1.Text);
            cmd.Parameters.AddWithValue("@categoryname", HttpUtility.HtmlDecode(TextBox3.Text));
            cmd.Parameters.AddWithValue("@Com_Id", company_id);
            CON.Open();
            cmd.ExecuteNonQuery();
            CON.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Category created successfully')", true);
            BindData();
            show_category();
            getinvoiceno();
            TextBox3.Text = "";
            }
            con1.Close();

        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox3.Text = "";
        getinvoiceno();
        show_category();
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
        SqlCommand CMD = new SqlCommand("select * from category where Com_Id='"+company_id+"' ORDER BY category_id asc", con);
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
        SqlCommand cmd = new SqlCommand("delete from category where category_id='" + row.Cells[1].Text + "' and Com_Id='" + company_id + "' ", con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Category deleted successfully')", true);

        BindData();
        show_category();
        getinvoiceno();

       
    }
    private void getinvoiceno()
    {

      
        int a;

        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        con1.Open();
        string query = "Select COUNT(category_id) from category where Com_Id='" + company_id + "' ";
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
    private void show_category()
    {
      
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from category where Com_Id='" + company_id + "' ORDER BY categoryname  asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList2.DataSource = ds;
        DropDownList2.DataTextField = "categoryname";
        DropDownList2.DataValueField = "category_id";
        DropDownList2.DataBind();
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
    protected void Button11_Click(object sender, EventArgs e)
    {
       
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            //Finiding checkbox control in gridview for particular row
            CheckBox chkdelete = (CheckBox)gvrow.FindControl("CheckBox2");
            //Condition to check checkbox selected or not
            if (chkdelete.Checked)
            {
                //Getting UserId of particular row using datakey value
                int usrid = Convert.ToInt32(gvrow.Cells[1].Text);
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

                con.Open();
                SqlCommand cmd = new SqlCommand("delete from category where category_id='"+ usrid+"' and Com_Id='" + company_id + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
        BindData();
        getinvoiceno();

    }
    private void showcustomertype()
    {
       
    }
    private void showrating()
    {
       
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    
    public static List<string> SearchCustomers2(string prefixText, int count)
    {

        
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = "select categoryname from category where  Com_Id=@Com_Id and  " +
                "categoryname like @categoryname + '%' ";
                cmd.Parameters.AddWithValue("@categoryname", prefixText);
                cmd.Parameters.AddWithValue("@Com_Id",company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["categoryname"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
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
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {
       
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        

        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from category where categoryname='" + TextBox1.Text + "' and Com_Id='" + company_id + "' ", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }
}