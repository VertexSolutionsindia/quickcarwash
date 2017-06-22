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


public partial class Admin_Customer_Entry : System.Web.UI.Page
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
                    Label8.Text = dr["company_name"].ToString();
                }
                con.Close();
            }


            TextBox3.Attributes.Add("onkeypress", "return controlEnter('" + TextBox2.ClientID + "', event)");
            TextBox2.Attributes.Add("onkeypress", "return controlEnter('" + TextBox9.ClientID + "', event)");

            getinvoiceno();
            Searchvehicle();
            SearchMobileno();
            SearchCustomer();
            SearchBrand();
            SearchMake();
            showrating();
            BindData();
           // show_type();
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
        TextBox6.Text = ROW.Cells[3].Text;
        TextBox10.Text = ROW.Cells[4].Text;
    
        TextBox7.Text = ROW.Cells[5].Text;
        TextBox8.Text = ROW.Cells[6].Text;
        TextBox12.Text = ROW.Cells[7].Text;
        TextBox13.Text = ROW.Cells[8].Text;
        this.ModalPopupExtender3.Show();
    }

    private void Searchvehicle()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Customer_Entry where Com_Id='" + company_id + "' ORDER BY Custom_Code asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList3.DataSource = ds;
        DropDownList3.DataTextField = "Customer_VehNo";
        DropDownList3.DataValueField = "Custom_Code";
        DropDownList3.DataBind();
        DropDownList3.Items.Insert(0, new ListItem("All", "0"));

        con.Close();
    }


    private void SearchMobileno()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Customer_Entry where Com_Id='" + company_id + "' ORDER BY Custom_Code asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Mobile_no";
        DropDownList1.DataValueField = "Custom_Code";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new ListItem("All", "0"));

        con.Close();
    }

    private void SearchCustomer()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Customer_Entry where Com_Id='" + company_id + "' ORDER BY Custom_Code asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList2.DataSource = ds;
        DropDownList2.DataTextField = "Custom_Name";
        DropDownList2.DataValueField = "Custom_Code";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("All", "0"));
        con.Close();
    }

    private void SearchBrand()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Customer_Entry where Com_Id='" + company_id + "' ORDER BY Custom_Code asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList5.DataSource = ds;
        DropDownList5.DataTextField = "Vehicle_Brand";
        DropDownList5.DataValueField = "Custom_Code";
        DropDownList5.DataBind();
        DropDownList5.Items.Insert(0, new ListItem("All", "0"));
        con.Close();
    }

    private void SearchMake()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Customer_Entry where Com_Id='" + company_id + "' ORDER BY Custom_Code asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList6.DataSource = ds;
        DropDownList6.DataTextField = "Vehicle_Make";
        DropDownList6.DataValueField = "Custom_Code";
        DropDownList6.DataBind();
        DropDownList6.Items.Insert(0, new ListItem("All", "0"));
        con.Close();
    }

    protected void Button16_Click(object sender, EventArgs e)
    {
        

        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("update Customer_Entry set Custom_Name='" + HttpUtility.HtmlDecode(TextBox16.Text) + "',Custom_Add='" + HttpUtility.HtmlDecode(TextBox6.Text) + "',Mobile_no='" + HttpUtility.HtmlDecode(TextBox10.Text) + "',Emailid='" + HttpUtility.HtmlDecode(TextBox7.Text) + "',Customer_VehNo='" + HttpUtility.HtmlDecode(TextBox8.Text) + "',Vehicle_Brand='" + HttpUtility.HtmlDecode(TextBox12.Text) + "',Vehicle_Make='" + HttpUtility.HtmlDecode(TextBox13.Text) + "' where Custom_Code='" + Label29.Text + "'  and Com_Id='" + company_id + "'", CON);

        CON.Open();
        cmd.ExecuteNonQuery();
        CON.Close();
        Label31.Text = "Updated successfuly";

        this.ModalPopupExtender3.Hide();
        BindData();
        getinvoiceno();


    }
    protected void Button17_Click(object sender, EventArgs e)
    {
      

        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd1 = new SqlCommand("delete from Customer_Entry where Custom_Code='" + Label29.Text + "'  and Com_Id='" + company_id + "' ", con1);
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
                SqlCommand cmd = new SqlCommand("delete from Customer_Entry where Custom_Code='" + usrid+"' and Com_Id='" + company_id + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
        BindData();
        getinvoiceno();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        if (TextBox3.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter customer name')", true);
        }
        else if (TextBox9.Text == "")
            {
             ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter Mobile No')", true);
        }
        else
        {
                SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("insert into Customer_Entry values(@Custom_Code,@Custom_Name,@Custom_Add,@Mobile_no,@Emailid,@Customer_VehNo,@Com_Id,@Vehicle_Brand,@Vehicle_Make)", CON);
                cmd.Parameters.AddWithValue("@Custom_Code", Label1.Text);
                cmd.Parameters.AddWithValue("@Custom_Name", HttpUtility.HtmlDecode(TextBox3.Text));
                cmd.Parameters.AddWithValue("@Custom_Add", HttpUtility.HtmlDecode(TextBox2.Text));
                cmd.Parameters.AddWithValue("@Mobile_no", HttpUtility.HtmlDecode(TextBox9.Text));
                cmd.Parameters.AddWithValue("@Emailid", HttpUtility.HtmlDecode(TextBox4.Text));
                cmd.Parameters.AddWithValue("@Customer_VehNo", HttpUtility.HtmlDecode(TextBox11.Text));
                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Parameters.AddWithValue("@Vehicle_Brand", HttpUtility.HtmlDecode(TextBox15.Text));
                cmd.Parameters.AddWithValue("@Vehicle_Make", HttpUtility.HtmlDecode(TextBox17.Text));



                CON.Open();
                cmd.ExecuteNonQuery();
                CON.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Customer Entry created successfully')", true);
                BindData();
                show_category();
                getinvoiceno();
                Searchvehicle();
                SearchMobileno();
                SearchCustomer();
                SearchBrand();
                SearchMake();
                TextBox3.Text = "";
                TextBox2.Text = "";
                TextBox4.Text = "";
                // show_type();
                TextBox9.Text = "";
                TextBox11.Text = "";
                TextBox15.Text = "";
                TextBox17.Text = "";

            }
           
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox3.Text = "";
        TextBox2.Text = "";
        TextBox4.Text = "";       
        TextBox9.Text = "";
        getinvoiceno();
        show_category();
        TextBox12.Text = "";
        TextBox13.Text = "";
      
        TextBox11.Text = "";
        TextBox17.Text = "";
        TextBox15.Text = "";
       
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
        SqlCommand CMD = new SqlCommand("select * from Customer_Entry where Com_Id='" + company_id + "' ORDER BY Custom_Code asc", con);
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

           con.Open();
           SqlCommand cmd = new SqlCommand("delete from Customer_Entry where Custom_Code='" + row.Cells[1].Text + "' and Com_Id='" + company_id + "' ", con);
           cmd.ExecuteNonQuery();
           con.Close();

           ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Customer Details deleted successfully')", true);

           BindData();
           show_category();
           getinvoiceno();
      

    }
    private void getinvoiceno()
    {
       
        int a;

        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        con1.Open();
        string query = "Select Max(Custom_Code) from Customer_Entry where Com_Id='" + company_id + "'";
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
        SqlCommand cmd = new SqlCommand("Select * from Customer_Entry where Com_Id='" + company_id + "' ORDER BY Custom_Code asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


     

        con.Close();
    }

    private void show_type()
    {
       
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from customer_type where Com_Id='" + company_id + "' ORDER BY type_id asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


       

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
    
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }

    protected void Button5_Click(object sender, EventArgs e)
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
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Tell the compiler that the control is rendered
         * explicitly by overriding the VerifyRenderingInServerForm event.*/
    }
    protected void TextBox9_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
      
    }
 


    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from Customer_Entry where Customer_VehNo='" + DropDownList3.SelectedItem.Text + "' and Com_Id='" + company_id + "' ORDER BY Custom_Code asc", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from Customer_Entry where Mobile_no='" + DropDownList1.SelectedItem.Text + "' and Com_Id='" + company_id + "' ORDER BY Custom_Code asc", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from Customer_Entry where Custom_Name='" + DropDownList2.SelectedItem.Text + "' and Com_Id='" + company_id + "' ORDER BY Custom_Code asc", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from Customer_Entry where Vehicle_Brand='" + DropDownList5.SelectedItem.Text + "' and Com_Id='" + company_id + "' ORDER BY Custom_Code asc", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }
    protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from Customer_Entry where Vehicle_Make='" + DropDownList6.SelectedItem.Text + "' and Com_Id='" + company_id + "' ORDER BY Custom_Code asc", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }

 
    protected void Button7_Click(object sender, EventArgs e)
    {
        BindData();
      

        DropDownList3.Items.Clear();
        DropDownList1.Items.Clear();
        DropDownList2.Items.Clear();
        DropDownList5.Items.Clear();
        DropDownList6.Items.Clear();

        Searchvehicle();
        SearchMobileno();
        SearchCustomer();
        SearchBrand();
        SearchMake();
      
    }

}
