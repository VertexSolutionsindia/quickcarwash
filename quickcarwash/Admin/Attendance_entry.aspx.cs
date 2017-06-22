#region " Using "
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Specialized;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
#endregion

public partial class Admin_Attendance_entry : System.Web.UI.Page
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
                    Label10.Text = dr["company_name"].ToString();
                }
                con.Close();
            }
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from currentfinancialyear where no='1'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                Label8.Text = dr10["financial_year"].ToString();
            }
            DateTime date = DateTime.Now;
            TextBox8.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");
            TextBox2.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");
            getinvoiceno();
          
            SearchExpense();
            BindData();
          
        }
    }
   

    protected void BindData()
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr;
            con10.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                company_id = Convert.ToInt32(dr["com_id"].ToString());
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from Attendance_Entry where Com_Id='" + company_id + "' and year='" + Label8.Text + "' ORDER BY No asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
            con10.Close();
        }
    }



    //A method that returns a string which calls the connection string from the web.config
    private string GetConnectionString()
    {
        //"DBConnection" is the name of the Connection String
        //that was set up from the web.config file
        return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    }

    //A method that Inserts the records to the database


    protected void Gridview1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void Gridview1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {

    }
    protected void Gridview1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {

    }
    protected void Gridview1_SelectedIndexChanged(object sender, System.EventArgs e)
    {


    }
    protected void Gridview1_RowUpdated(object sender, System.Web.UI.WebControls.GridViewUpdatedEventArgs e)
    {

    }

    protected void TextBox1_TextChanged(object sender, System.EventArgs e)
    {

    }
    protected void Gridview1_Load(object sender, System.EventArgs e)
    {

    }

    private void getinvoiceno()
    {

        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr1;
            con10.Open();
            dr1 = cmd.ExecuteReader();
            if (dr1.Read())
            {
                company_id = Convert.ToInt32(dr1["com_id"].ToString());
                int a;

                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                con1.Open();
                string query = "Select max(No) from Attendance_Entry where Com_Id='" + company_id + "'";
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
                con1.Close();
            }
            con10.Close();
        }
    }

    
    private void SearchExpense()
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr;
            con10.Open();
            dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                company_id = Convert.ToInt32(dr["com_id"].ToString());
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("Select * from Staff_Entry where Com_Id='" + company_id + "' ORDER BY Emp_Code asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


             

                DropDownList4.DataSource = ds;
                DropDownList4.DataTextField = "Emp_Name";
                DropDownList4.DataValueField = "Emp_Code";
                DropDownList4.DataBind();
                DropDownList4.Items.Insert(0, new ListItem("All", "0"));

                con.Close();
            }
            con10.Close();
        }
    }
    private void SaveDetail(GridViewRow row)
    {


    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox8.Text = "";
        TextBox12.Text = "";
        SearchExpense();
        BindData();
        DateTime date = DateTime.Now;
        TextBox8.Text = Convert.ToDateTime(date).ToString("MM-dd-yyyy");
        DropDownList1.SelectedItem.Text = "";
        TextBox1.Text = "";

    }
    private void active()
    {

    }


    private void created()
    {

    }

    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {

        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr;
            con10.Open();
            dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                company_id = Convert.ToInt32(dr["com_id"].ToString());
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("delete from Attendance_Entry where No='" + row.Cells[1].Text + "' and Com_Id='" + company_id + "' ", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Attendance entry deleted successfully')", true);

                BindData();
                getinvoiceno();
            }
            con10.Close();
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr;
            con10.Open();
            dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                company_id = Convert.ToInt32(dr["com_id"].ToString());
              

                ImageButton IMG = (ImageButton)sender;
                GridViewRow ROW = (GridViewRow)IMG.NamingContainer;
                Label4.Text = ROW.Cells[1].Text;
                TextBox2.Text = ROW.Cells[2].Text;
                DropDownList4.SelectedItem.Text = ROW.Cells[3].Text;
                DropDownList2.SelectedItem.Text = ROW.Cells[4].Text;
                TextBox4.Text = ROW.Cells[5].Text;
                TextBox3.Text = ROW.Cells[6].Text;

                this.ModalPopupExtender1.Show();
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
        // this.ModalPopupExtender2.Show();
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

        //this.ModalPopupExtender1.Show();
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        // this.ModalPopupExtender4.Show();
    }




    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
       

    }

    protected void TextBox2_TextChanged(object sender, System.EventArgs e)
    {
        // TextBox3.Focus();
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        
    }
   
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con10);
            SqlDataReader dr;
            con10.Open();
            dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                company_id = Convert.ToInt32(dr["com_id"].ToString());

                
                SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("select * from Attendance_Entry where date='" + Convert.ToDateTime(TextBox8.Text).ToString("MM-dd-yyyy") + "' and Staff_Name='"+TextBox6.Text+"'  and Com_Id='" + company_id + "' and year='" + Label8.Text + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.HasRows)
                {
                    
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Today Attendance already completed for this employee')", true);
                }
                else
                {
                int value = 0;
                   string status="Daily base amount";
                SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("insert into Attendance_Entry values(@No,@date,@Staff_Name,@option_name,@salary_amount,@Amount,@Com_id,@year,@status,@value)", CON);
                cmd.Parameters.AddWithValue("@No", Label1.Text);
                cmd.Parameters.AddWithValue("@date",Convert.ToDateTime(TextBox8.Text).ToString("MM-dd-yyyy"));
             
                cmd.Parameters.AddWithValue("@Staff_Name", TextBox6.Text);
                cmd.Parameters.AddWithValue("@option_name",  DropDownList1.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@salary_amount", TextBox1.Text);
                cmd.Parameters.AddWithValue("@Amount", TextBox12.Text);

                cmd.Parameters.AddWithValue("@Com_Id",company_id);
                cmd.Parameters.AddWithValue("@year", Label8.Text);
                cmd.Parameters.AddWithValue("@status", TextBox6.Text + "-"+ status);
                cmd.Parameters.AddWithValue("@value", value);
               
                CON.Open();
                cmd.ExecuteNonQuery();
                CON.Close();




                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Attendance entry Added successfully')", true);

                TextBox8.Text = "";
                TextBox12.Text = "";
                TextBox1.Text = "";
                TextBox6.Text = "";
                getinvoiceno();
                SearchExpense();
                BindData();
                DateTime date = DateTime.Now;
                TextBox8.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");
                SearchExpense();
              
            }

            }
            con10.Close();
        }

    }
    protected void Button7_Click(object sender, EventArgs e)
    {
       
    }

    protected void Button14_Click(object sender, EventArgs e)
    {

    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void Button5_Click(object sender, EventArgs e)
    {

        int value = 0;
        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("update Attendance_Entry set date=@date,Staff_Name=@Staff_Name,option_name=@option_name,salary_amount=@salary_amount,Amount=@Amount,Com_Id=@Com_Id,year=@year where No=@No", CON);
        cmd.Parameters.AddWithValue("@No", Label4.Text);
        cmd.Parameters.AddWithValue("@date",Convert.ToDateTime( TextBox2.Text).ToString("MM-dd-yyyy"));

        cmd.Parameters.AddWithValue("@Staff_Name", DropDownList4.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@option_name", DropDownList2.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@salary_amount", TextBox4.Text);
        cmd.Parameters.AddWithValue("@Amount", TextBox3.Text);

        cmd.Parameters.AddWithValue("@Com_Id", company_id);
        cmd.Parameters.AddWithValue("@year", Label8.Text);

        CON.Open();
        cmd.ExecuteNonQuery();
        CON.Close();
        Label6.Text = "Updated successfuly";
        TextBox1.Text = "";
        this.ModalPopupExtender1.Hide();
        BindData();
        getinvoiceno();
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
                SqlCommand cmd2 = new SqlCommand("Select * from Staff_Entry where Emp_Name='" + TextBox6.Text + "' and Com_Id='" + company_id + "'", con);
                SqlDataReader dr1;
                dr1 = cmd2.ExecuteReader();
                if (dr1.Read())
                {

                    if (DropDownList1.SelectedItem.Text == "Present")
                    {
                        TextBox1.Text = dr1["salary"].ToString();

                    }
                    if (DropDownList1.SelectedItem.Text == "Absent")
                    {
                        TextBox1.Text = "0";

                    }
                    if (DropDownList1.SelectedItem.Text == "Half day present")
                    {
                        float value=float.Parse( dr1["salary"].ToString());
                        TextBox1.Text = (value / 2).ToString();
                    }



                }
                con.Close();
            }
            con10.Close();
        }
    }
    protected void TextBox5_TextChanged(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from Attendance_Entry where date='" + Convert.ToDateTime(TextBox5.Text).ToString("MM-dd-yyyy") + "' and Com_Id='" + company_id + "'", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> Searchemployee(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Emp_Name from Staff_Entry where Com_Id=@Com_Id and " +
                "Emp_Name like @Emp_Name + '%'";
                cmd.Parameters.AddWithValue("@Emp_Name", prefixText);
                cmd.Parameters.AddWithValue("@Com_id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Emp_Name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
   
}