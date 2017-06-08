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
using System.Drawing;
#endregion

public partial class Admin_WorkShop_Report : System.Web.UI.Page
{
    float tot = 0;
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

            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from currentfinancialyear where no='1'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                Label1.Text = dr10["financial_year"].ToString();
                TextBox3.Text =Convert.ToDateTime(  dr10["start_date"]).ToString("dd-MM-yyyy");
            }
            showrating();
          

            active();
            created();
            getinvoiceno();

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
                string query = "Select Max(invoice_no) from workshop_report where Com_Id='" + company_id + "' and year='" + Label1.Text + "' ";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        Label3.Text = "1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        TextBox13.Text = a.ToString();
                        a = a + 1;
                        Label3.Text = a.ToString();
                    }
                }
                con1.Close();
            }
            con10.Close();
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

    private void created()
    {

    }
   

   

   
    private void show_category()
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("SELECT  CONVERT(date,date,101) as Date, status as Particulars,sum(paid_amount) as Debit,isnull(sum(value),0) as Credit FROM sales_entry as a where Com_Id='" + company_id + "'  group by date,status,paid_amount,value union SELECT DISTINCT date as Date, status as Particulars,sum(paid_amount) as Debit,isnull(sum(value),0) as Credit FROM purchase_entry where Com_Id='" + company_id + "'  group by date,status,paid_amount,value union SELECT DISTINCT date as Date, status as Particulars,sum(amount) as Debit,isnull(sum(value),0) as Credit FROM purchase_amount where Com_Id='" + company_id + "'  group by date,status,amount,value", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }
    protected void BindData()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from WorkshopBilling_Entry where Com_Id='" + company_id + "' and year='" + Label1.Text + "' ORDER BY Invoice_id asc", con);
        DataTable dt1 = new DataTable();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();

    }
    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {

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
        show_category();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (GridView1.PageIndex + 1) + " of " + GridView1.PageCount;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tot = tot + float.Parse(e.Row.Cells[7].Text);

        }
        TextBox7.Text = tot.ToString();
        TextBox8.Text = tot.ToString();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


 
    
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
       



    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {
       
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        //SqlCommand CMD = new SqlCommand("SELECT  CONVERT(datetime,date,101) as Date, status as Particulars,sum(paid_amount) as Debit,isnull(sum(value),0) as Credit FROM sales_entry as a where date='" + TextBox3.Text + "' and Com_Id='" + company_id + "' group by date,status,paid_amount,value union SELECT DISTINCT date as Date, status as Particulars,sum(paid_amount) as Debit,isnull(sum(value),0) as Credit FROM purchase_entry as a where date='" + TextBox3.Text + "' and Com_Id='" + company_id + "' group by date,status,paid_amount,value union SELECT DISTINCT date as Date, status as Particulars,sum(amount) as Debit,isnull(sum(value),0) as Credit FROM purchase_amount as a where date='" + TextBox3.Text + "' and Com_Id='" + company_id + "' group by date,status,amount,value", con1);

        SqlCommand CMD = new SqlCommand("select * from WorkshopBilling_Entry where date='" + TextBox3.Text + "' and Com_Id='" + company_id + "' and year='" + Label1.Text + "' ORDER BY Invoice_id asc", con1);
       
        
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }
    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {
      
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        //SqlCommand CMD = new SqlCommand("SELECT CONVERT(datetime,date,101) as Date, status as Particulars,sum(paid_amount) as Debit,isnull(sum(value),0) as Credit FROM sales_entry as a where date between '" + TextBox3.Text + "' and '" + TextBox4.Text + "' and Com_Id='" + company_id + "' group by date,status,paid_amount,value union SELECT DISTINCT date as Date, status as Particulars,sum(paid_amount) as Debit,isnull(sum(value),0) as Credit FROM purchase_entry as a where date between '" + TextBox3.Text + "' and '" + TextBox4.Text + "' and Com_Id='" + company_id + "' group by date,status,paid_amount,value union SELECT DISTINCT date as Date, status as Particulars,sum(amount) as Debit,isnull(sum(value),0) as Credit FROM purchase_amount as a where date between '" + TextBox3.Text + "' and '" + TextBox4.Text + "' and Com_Id='" + company_id + "'   group by date,status,amount,value", con1);
        SqlCommand CMD = new SqlCommand("select * from WorkshopBilling_Entry where date between '" + TextBox3.Text + "' and '" + TextBox4.Text + "' and Com_Id='" + company_id + "' and year='" + Label1.Text + "' ORDER BY Invoice_id asc", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {
       
    }
    protected void Button1_Click(object sender, EventArgs e)
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

   

    
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
       
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
                cmd.CommandText = "select distinct Customer_VehNo from WorkshopBilling_Entry where Com_Id=@Com_Id and " +
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
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers1(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct WorkShop_Name from Workshop_Entry where Com_Id=@Com_Id and " +
                "WorkShop_Name like @WorkShop_Name + '%'";
                cmd.Parameters.AddWithValue("@WorkShop_Name", prefixText);
                cmd.Parameters.AddWithValue("@Com_id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["WorkShop_Name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        //SqlCommand CMD = new SqlCommand("SELECT CONVERT(datetime,date,101) as Date, status as Particulars,sum(paid_amount) as Debit,isnull(sum(value),0) as Credit FROM sales_entry as a where date between '" + TextBox3.Text + "' and '" + TextBox4.Text + "' and Com_Id='" + company_id + "' group by date,status,paid_amount,value union SELECT DISTINCT date as Date, status as Particulars,sum(paid_amount) as Debit,isnull(sum(value),0) as Credit FROM purchase_entry as a where date between '" + TextBox3.Text + "' and '" + TextBox4.Text + "' and Com_Id='" + company_id + "' group by date,status,paid_amount,value union SELECT DISTINCT date as Date, status as Particulars,sum(amount) as Debit,isnull(sum(value),0) as Credit FROM purchase_amount as a where date between '" + TextBox3.Text + "' and '" + TextBox4.Text + "' and Com_Id='" + company_id + "'   group by date,status,amount,value", con1);
        SqlCommand CMD = new SqlCommand("select * from WorkshopBilling_Entry where Customer_VehNo='" + TextBox1.Text + "'  and Com_Id='" + company_id + "' and year='" + Label1.Text + "' ORDER BY Invoice_id asc", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();

    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        //SqlCommand CMD = new SqlCommand("SELECT CONVERT(datetime,date,101) as Date, status as Particulars,sum(paid_amount) as Debit,isnull(sum(value),0) as Credit FROM sales_entry as a where date between '" + TextBox3.Text + "' and '" + TextBox4.Text + "' and Com_Id='" + company_id + "' group by date,status,paid_amount,value union SELECT DISTINCT date as Date, status as Particulars,sum(paid_amount) as Debit,isnull(sum(value),0) as Credit FROM purchase_entry as a where date between '" + TextBox3.Text + "' and '" + TextBox4.Text + "' and Com_Id='" + company_id + "' group by date,status,paid_amount,value union SELECT DISTINCT date as Date, status as Particulars,sum(amount) as Debit,isnull(sum(value),0) as Credit FROM purchase_amount as a where date between '" + TextBox3.Text + "' and '" + TextBox4.Text + "' and Com_Id='" + company_id + "'   group by date,status,amount,value", con1);
        SqlCommand CMD = new SqlCommand("select * from WorkshopBilling_Entry where Workshop_name='" + TextBox2.Text + "'  and Com_Id='" + company_id + "' and year='" + Label1.Text + "' ORDER BY Invoice_id asc", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();


        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        con.Open();
        SqlCommand cmd2 = new SqlCommand("Select * from Workshop_Entry where WorkShop_Name='" + TextBox2.Text + "' and Com_Id='" + company_id + "'", con);
        SqlDataReader dr1;
        dr1 = cmd2.ExecuteReader();
        if (dr1.Read())
        {

            TextBox5.Text = dr1["Mobile_no"].ToString();
            TextBox6.Text = dr1["WorkShop_Add"].ToString();


        }
        con.Close();



    }
    
    protected void Button5_Click(object sender, EventArgs e)
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
                foreach (GridViewRow row in GridView1.Rows)
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

                    SqlCommand cmd = new SqlCommand("INSERT INTO workshop_report_details VALUES(@invoice_no,@Invoice_id,@date,@Workshop_name,@Mobile_No,@Address,@Customer_VehNo,@Service_Name,@Amount,@Com_Id,@year)", con);

                    cmd.Parameters.AddWithValue("@invoice_no", Label3.Text);
                    cmd.Parameters.AddWithValue("@Invoice_id", row.Cells[0].Text);
                    cmd.Parameters.AddWithValue("@date", row.Cells[1].Text);
                    cmd.Parameters.AddWithValue("@Workshop_name", row.Cells[3].Text);
                    cmd.Parameters.AddWithValue("@Mobile_No", row.Cells[4].Text);
                    cmd.Parameters.AddWithValue("@Address", row.Cells[5].Text);

                    cmd.Parameters.AddWithValue("@Customer_VehNo", row.Cells[2].Text);
                    cmd.Parameters.AddWithValue("@Service_Name", row.Cells[6].Text);
                    cmd.Parameters.AddWithValue("@Amount", row.Cells[7].Text);
                    cmd.Parameters.AddWithValue("@Com_Id", company_id);
                    cmd.Parameters.AddWithValue("@year", Label1.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }



                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd1 = new SqlCommand("select * from workshop_report where invoice_no='" + Label1.Text + "' and year='" + Label1.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                con1.Open();
                SqlDataReader dr1;
                dr1 = cmd1.ExecuteReader();
                if (dr1.HasRows)
                {
                    int value = 0;
                    SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd = new SqlCommand("update workshop_report set date=@date,from_date=@from_date,to_date=@to_date,workshop_name=@workshop_name,work_add,@work_add,work_mobile=@work_mobile,vehicleno=@vehicleno,total_Amount=@total_Amount,grand_total=@grand_total where invoice_no=@invoice_no and Com_Id=@Com_id and year=@year", CON);
                    cmd.Parameters.AddWithValue("@invoice_no", Label3.Text);
                    cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(TextBox9.Text).ToString("MM-dd-yyyy"));
                    cmd.Parameters.AddWithValue("@From_date", Convert.ToDateTime(TextBox3.Text).ToString("MM-dd-yyyy"));
                    cmd.Parameters.AddWithValue("@to_date", Convert.ToDateTime(TextBox4.Text).ToString("MM-dd-yyyy"));
                    cmd.Parameters.AddWithValue("@workshop_name", HttpUtility.HtmlDecode(TextBox2.Text));
                    cmd.Parameters.AddWithValue("@work_add", HttpUtility.HtmlDecode(TextBox5.Text));
                    cmd.Parameters.AddWithValue("@work_mobile", HttpUtility.HtmlDecode(TextBox6.Text));
                    cmd.Parameters.AddWithValue("@vehicleno", HttpUtility.HtmlDecode(TextBox1.Text));
                    cmd.Parameters.AddWithValue("@total_Amount", HttpUtility.HtmlDecode(TextBox7.Text));
                    cmd.Parameters.AddWithValue("@grand_total", TextBox8.Text);
                    cmd.Parameters.AddWithValue("@Com_Id", company_id);
                    cmd.Parameters.AddWithValue("@year", Label1.Text);

                    CON.Open();
                    cmd.ExecuteNonQuery();
                    CON.Close();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Workshop entry updated successfully')", true);
                    getinvoiceno();
                    BindData();
                    TextBox1.Text = "";
                    TextBox16.Text = "";
                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    TextBox4.Text = "";
                    TextBox5.Text = "";
                    TextBox6.Text = "";
                    TextBox7.Text = "";
                    TextBox9.Text = "";
                    TextBox8.Text = "";
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
                else
                {
                    int value = 0;
                    SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd = new SqlCommand("insert into workshop_report values(@invoice_no,@date,@from_date,@to_date,@workshop_name,@work_add,@work_mobile,@vehicleno,@total_Amount,@grand_total,@Com_Id,@year)", CON);
                    cmd.Parameters.AddWithValue("@invoice_no", Label3.Text);
                    cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(TextBox9.Text).ToString("MM-dd-yyyy"));
                    cmd.Parameters.AddWithValue("@From_date", Convert.ToDateTime(TextBox3.Text).ToString("MM-dd-yyyy"));
                    cmd.Parameters.AddWithValue("@to_date", Convert.ToDateTime(TextBox4.Text).ToString("MM-dd-yyyy"));
                    cmd.Parameters.AddWithValue("@workshop_name", HttpUtility.HtmlDecode(TextBox2.Text));
                    cmd.Parameters.AddWithValue("@work_add", HttpUtility.HtmlDecode(TextBox5.Text));
                    cmd.Parameters.AddWithValue("@work_mobile", HttpUtility.HtmlDecode(TextBox6.Text));
                    cmd.Parameters.AddWithValue("@vehicleno", HttpUtility.HtmlDecode(TextBox1.Text));
                    cmd.Parameters.AddWithValue("@total_Amount", HttpUtility.HtmlDecode(TextBox7.Text));
                    cmd.Parameters.AddWithValue("@grand_total", TextBox8.Text);
                    cmd.Parameters.AddWithValue("@Com_Id",company_id);
                    cmd.Parameters.AddWithValue("@year", Label1.Text);
                    CON.Open();
                    cmd.ExecuteNonQuery();
                    CON.Close();
                }


                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Workshop entry created successfully')", true);
                getinvoiceno();
                BindData();
                TextBox1.Text = "";
                TextBox16.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox4.Text = "";
                TextBox5.Text = "";
                TextBox6.Text = "";
                TextBox7.Text = "";
                TextBox9.Text = "";
                TextBox8.Text = "";
                GridView1.DataSource = null;
                GridView1.DataBind();
            } con10.Close();
          
        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        getinvoiceno();
        BindData();
        TextBox1.Text = "";
        TextBox16.Text = "";
        TextBox2.Text = "";
    
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox9.Text = "";
        TextBox8.Text = "";
        GridView1.DataSource = null;
        GridView1.DataBind();
    
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        getinvoiceno();
        BindData();
        TextBox1.Text = "";
        TextBox16.Text = "";
        TextBox2.Text = "";
        TextBox8.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox9.Text = "";
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
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

                if (Convert.ToInt32(Label3.Text) > Convert.ToInt32(1))
                {
                    Label3.Text = (Convert.ToInt32(Label3.Text) - 1).ToString();
                }

                SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("select * from workshop_report where invoice_no='" + Label3.Text + "' and Com_Id='" + company_id + "' and year='" + Label1.Text + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                   
                    TextBox9.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-MM-yyyy");
                    TextBox3.Text = Convert.ToDateTime(dr2["from_date"]).ToString("dd-MM-yyyy");
                    TextBox4.Text = Convert.ToDateTime(dr2["to_date"]).ToString("dd-MM-yyyy");
                    TextBox7.Text = dr2["total_Amount"].ToString();
                    TextBox8.Text = dr2["grand_total"].ToString();
                    TextBox1.Text = dr2["vehicleno"].ToString();
                    TextBox2.Text = dr2["workshop_name"].ToString();
                    TextBox5.Text = dr2["work_add"].ToString();
                    TextBox6.Text = dr2["work_mobile"].ToString();

                }
                con2.Close();


                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from workshop_report_details where invoice_no='" + Label3.Text + "' and Com_Id='" + company_id + "' and year='" + Label1.Text + "' ", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
              
            }
            con1000.Close();
        }
    }
    #region " [ Button Event ] "
    protected void Button3_Click(object sender, EventArgs e)
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
                using (SqlCommand cmd = new SqlCommand("workshop_report_pro", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@No", Convert.ToInt32(TextBox13.Text));
                    cmd.Parameters.AddWithValue("@Com_id", Convert.ToInt32(company_id));
                    cmd.Parameters.AddWithValue("@year", Label1.Text);
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
        report.ReportPath = Server.MapPath("~/Admin/workshop_report.rdlc");
        ReportDataSource rds = new ReportDataSource();
        rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file
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
    #endregion
    protected void Button4_Click(object sender, EventArgs e)
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
                SqlCommand cmd21 = new SqlCommand("select max(invoice_no) from workshop_report where  Com_Id='" + company_id + "' and year='" + Label1.Text + "' ", con21);
                SqlDataReader dr21;
                con21.Open();
                dr21 = cmd21.ExecuteReader();
                if (dr21.Read())
                {
                    int value = Convert.ToInt32(dr21[0].ToString());
                    if (Convert.ToInt32(Label3.Text) < Convert.ToInt32(value + 1))
                    {
                        Label3.Text = (Convert.ToInt32(Label3.Text) + 1).ToString();
                    }
                }
                con21.Close();
                SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("select * from workshop_report where invoice_no='" + Label3.Text + "' and Com_Id='" + company_id + "' and year='" + Label1.Text + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                   
                    TextBox9.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-MM-yyyy");
                    TextBox3.Text = Convert.ToDateTime(dr2["from_date"]).ToString("dd-MM-yyyy");
                    TextBox4.Text = Convert.ToDateTime(dr2["to_date"]).ToString("dd-MM-yyyy");
                    TextBox7.Text = dr2["total_Amount"].ToString();
                    TextBox8.Text = dr2["grand_total"].ToString();
                    TextBox1.Text = dr2["vehicleno"].ToString();
                    TextBox2.Text = dr2["workshop_name"].ToString();
                    TextBox5.Text = dr2["work_add"].ToString();
                    TextBox6.Text = dr2["work_mobile"].ToString();

              

                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from workshop_report_details where invoice_no='" + Label3.Text + "' and Com_Id='" + company_id + "' and year='" + Label1.Text + "' ", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
                }
                else
                {

                    getinvoiceno();
                    BindData();
                    TextBox1.Text = "";
                    TextBox16.Text = "";
                    TextBox2.Text = "";
                    TextBox8.Text = "";
                    TextBox4.Text = "";
                    TextBox5.Text = "";
                    TextBox6.Text = "";
                    TextBox7.Text = "";
                    TextBox9.Text = "";
                    GridView1.DataSource = null;
                    GridView1.DataBind();


                }
                con2.Close();
            }
            con1000.Close();
        }
    }
}