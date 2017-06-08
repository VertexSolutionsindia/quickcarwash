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

public partial class Admin_SalaryEntry : System.Web.UI.Page
{
    //float tot = 0;
    float Salary_tot = 0;
    float TotalPaidAmt_tot = 0;
  
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
                //TextBox3.Text = Convert.ToDateTime(dr10["start_date"]).ToString("MM/dd/yyyy");
            }
            DateTime date = DateTime.Now;
            TextBox8.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");
            showrating();
            getinvoiceno();
            BindData();
            //BindData1();
            //BindData2();
            active();
            created();





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
    protected void Button1_Click(object sender, EventArgs e)
    {


        if (TextBox3.Text != "")
        {
            if (TextBox4.Text != "")
            {
                if (TextBox1.Text != "")
                {
                    //        BindData();
                    //        BindData1();
                    BindData2();


                    //        //----------------------------------------------Finding Balance

                    int Tot_Income = Convert.ToInt32(TextBox6.Text);

                    int Tol_CostofService = Convert.ToInt32(TextBox7.Text);

                    int Gross_profit = Tot_Income - Tol_CostofService;

                    TextBox2.Text = Convert.ToString(Gross_profit);
                    //        //-------------------------------------------------------------------
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Select Staff Name')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Select To Date')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Select From Date')", true);
        }

        float old = float.Parse(TextBox10.Text);
        float balnace = float.Parse(TextBox2.Text);
        TextBox2.Text = (old + balnace).ToString();
    }

    protected void BindData()
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
                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from Salary_Entry where Com_Id='1' ORDER BY Sal_id asc", con1);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
            con.Close();
        }
    }
    protected void BindData1()
    {
     //   if (User.Identity.IsAuthenticated)
     //       {
     //           SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
     //           SqlCommand cmd = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con);
     //           SqlDataReader dr;
     //           con.Open();
     //           dr = cmd.ExecuteReader();
     //           if (dr.Read())
     //           {
     //   SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
     //   //SqlCommand CMD = new SqlCommand("SELECT  CONVERT(datetime,date,101) as Date, status as Particulars,sum(paid_amount) as Debit,isnull(sum(value),0) as Credit FROM sales_entry as a where date='" + TextBox3.Text + "' and Com_Id='" + company_id + "' group by date,status,paid_amount,value union SELECT DISTINCT date as Date, status as Particulars,sum(paid_amount) as Debit,isnull(sum(value),0) as Credit FROM purchase_entry as a where date='" + TextBox3.Text + "' and Com_Id='" + company_id + "' group by date,status,paid_amount,value union SELECT DISTINCT date as Date, status as Particulars,sum(amount) as Debit,isnull(sum(value),0) as Credit FROM purchase_amount as a where date='" + TextBox3.Text + "' and Com_Id='" + company_id + "' group by date,status,amount,value", con1);
     //   SqlCommand CMD2 = new SqlCommand("select CostofService_Name,Sum(Amount) as Amount from CostOfService_Entry where date between '" + TextBox3.Text + "' and '" + TextBox4.Text + "' and Com_Id='" + company_id + "' and year='"+Label1.Text+"' group by CostofService_Name", con2);
     //   DataTable dt2 = new DataTable();
     //   con2.Open();
     //   SqlDataAdapter da2 = new SqlDataAdapter(CMD2);
     //   da2.Fill(dt2);
     //   GridView2.DataSource = dt2;
     //   GridView2.DataBind();
     //}
     //       con.Close();
     //   }
    }
    protected void BindData2()
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
                if ((TextBox3.Text != "") && (TextBox4.Text != ""))
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand CMD = new SqlCommand("select * from Attendance_Entry where Staff_Name='" + TextBox1.Text + "' AND date between '" + Convert.ToDateTime(TextBox3.Text).ToString("MM-dd-yyyy") + "' and '" + Convert.ToDateTime(TextBox4.Text).ToString("MM-dd-yyyy") + "' AND Com_Id='1' ORDER BY No asc", con1);
                    DataTable dt1 = new DataTable();
                    SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                    da1.Fill(dt1);
                    GridView3.DataSource = dt1;
                    GridView3.DataBind();
                }
                else
                {
                    GridView3.DataSource = null;
                    GridView3.DataBind();
                }
            }
            con.Close();
        }

    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchStaffName(string prefixText, int count)
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
        //GridView1.PageIndex = e.NewPageIndex;
        //BindData();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    e.Row.Cells[0].Text = "Page " + (GridView1.PageIndex + 1) + " of " + GridView1.PageCount;
        //}


        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    tot = tot + float.Parse(e.Row.Cells[1].Text);

        //}
        //TextBox1.Text = tot.ToString();
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GridView2.PageIndex = e.NewPageIndex;
        //BindData1();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    e.Row.Cells[0].Text = "Page " + (GridView2.PageIndex + 1) + " of " + GridView2.PageCount;
        //}

        //  if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Costofservice_tot = Costofservice_tot + float.Parse(e.Row.Cells[1].Text);

        //}
        //  TextBox2.Text = Costofservice_tot.ToString();
        
    }
    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        BindData2();
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (GridView3.PageIndex + 1) + " of " + GridView3.PageCount;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Salary_tot = Salary_tot + float.Parse(e.Row.Cells[2].Text);

        }
        TextBox6.Text = Salary_tot.ToString();

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalPaidAmt_tot = TotalPaidAmt_tot + float.Parse(e.Row.Cells[3].Text);

        }
        TextBox7.Text = TotalPaidAmt_tot.ToString();



    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
         SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1 = new SqlCommand("select * from staff_pending where staff_name='" + TextBox1.Text + "' and Com_Id='"+company_id+"'", con10);
            SqlDataReader dr;
            con10.Open();
            dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                TextBox10.Text= dr["amount"].ToString();
               
                
                
            }
            con10.Close();
    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
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

      



    }
    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {
       
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Tell the compiler that the control is rendered
         * explicitly by overriding the VerifyRenderingInServerForm event.*/
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox9.Text = "";
        //BindData();
        //BindData1();
        BindData2();
    }



    protected void TextBox5_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //------------------------------------------------Finding pending amount
            int Total_Grossprofit = Convert.ToInt32(TextBox2.Text);

            int Total_Expense = Convert.ToInt32(TextBox5.Text);

            int NetProfit = Total_Grossprofit - Total_Expense;

            TextBox9.Text = Convert.ToString(NetProfit);

            //----------------------------------------------------------------------
        }
        catch (Exception we)
        { }
    }

    protected void Button2_Click(object sender, EventArgs e)
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

                SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("insert into Salary_Entry values(@Sal_id,@date,@from_date,@To_date,@Staff_Name,@Total_Salary,@TotalPaid_Amount,@Balance_Amount,@ActualPaid_Amount,@Pending_Amount,@Com_id,@year)", CON);
                cmd.Parameters.AddWithValue("@Sal_id", Label2.Text);
                cmd.Parameters.AddWithValue("@date", TextBox8.Text);
                cmd.Parameters.AddWithValue("@from_date", TextBox3.Text);
                cmd.Parameters.AddWithValue("@To_date", TextBox4.Text);
                cmd.Parameters.AddWithValue("@Staff_Name", TextBox1.Text);
                cmd.Parameters.AddWithValue("@Total_Salary", TextBox6.Text);
                cmd.Parameters.AddWithValue("@TotalPaid_Amount", TextBox7.Text);
                cmd.Parameters.AddWithValue("@Balance_Amount", TextBox2.Text);
                cmd.Parameters.AddWithValue("@ActualPaid_Amount", TextBox5.Text);
                cmd.Parameters.AddWithValue("@Pending_Amount", TextBox9.Text);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Parameters.AddWithValue("@year", Label1.Text);

                CON.Open();
                cmd.ExecuteNonQuery();
                CON.Close();
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Salary entry Added successfully')", true);

                 SqlConnection con100= new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                 SqlCommand cmd100 = new SqlCommand("select * from staff_pending where staff_name='" + TextBox1.Text + "' and Com_Id='"+company_id+"'", con100);
            SqlDataReader dr100;
            con100.Open();
            dr100 = cmd100.ExecuteReader();
            if (dr100.HasRows)
            {
                SqlConnection Con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("update staff_pending set amount=@amount where staff_name='"+TextBox1.Text+"' and Com_id='"+company_id+"'", Con1);
               
                cmd2.Parameters.AddWithValue("@amount", TextBox9.Text);


                Con1.Open();
                cmd2.ExecuteNonQuery();
                Con1.Close();
            }
            else
            {

                SqlConnection Con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("insert into staff_pending values(@staff_name,@amount,@Com_Id)", Con1);
                cmd2.Parameters.AddWithValue("@staff_name", TextBox1.Text);
                cmd2.Parameters.AddWithValue("@amount", TextBox9.Text);
                cmd2.Parameters.AddWithValue("@Com_Id", company_id);

                Con1.Open();
                cmd2.ExecuteNonQuery();
                Con1.Close();
            }
            con100.Close();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Salary entry Added successfully')", true);
               
                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox4.Text = "";
                TextBox5.Text = "";
                TextBox6.Text = "";
                TextBox7.Text = "";
                TextBox9.Text = "";
                TextBox10.Text = "";
                BindData2();
                BindData();
                getinvoiceno();
                DateTime date = DateTime.Now;
                TextBox8.Text = Convert.ToDateTime(date).ToString("MM-dd-yyyy");


            }
            con10.Close();
        }
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
                string query = "Select max(Sal_id) from Salary_Entry where Com_Id='" + company_id + "'";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        Label2.Text = "1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        a = a + 1;
                        Label2.Text = a.ToString();
                    }
                }
                con1.Close();
            }
            con10.Close();
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox9.Text = "";
        TextBox10.Text = "";
        BindData2();
        BindData();
        getinvoiceno();
        DateTime date = DateTime.Now;
        TextBox8.Text = Convert.ToDateTime(date).ToString("MM-dd-yyyy");
    }
    protected void TextBox8_TextChanged(object sender, EventArgs e)
    {

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
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;


                SqlConnection Con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("update staff_pending set amount=amount-@amount where staff_name='" + row.Cells[3].Text + "' and Com_id='" + company_id + "'", Con1);

                cmd2.Parameters.AddWithValue("@amount", row.Cells[8].Text);


                Con1.Open();
                cmd2.ExecuteNonQuery();
                Con1.Close();
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("delete from Salary_Entry where Sal_id='" + row.Cells[0].Text + "' and Com_Id='" + company_id + "' ", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Salary entry deleted successfully')", true);

                BindData();
                getinvoiceno();
            }
            con10.Close();
        }
    }
}
