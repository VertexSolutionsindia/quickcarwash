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

#endregion

public partial class RabbitDashboard : System.Web.UI.Page
{
    public static int company_id = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
        if (!IsPostBack)
        {
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from currentfinancialyear where no='1'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                Label3.Text = dr10["financial_year"].ToString();
                TextBox1.Text = Convert.ToDateTime(dr10["start_date"]).ToString("dd-MM-yyyy");
            }
            con10.Close();
            if (User.Identity.IsAuthenticated)
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd1 = new SqlCommand("select * from user_details where Name='" + User.Identity.Name + "'", con1);
                SqlDataReader dr;
                con1.Open();
                dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    company_id = Convert.ToInt32(dr["com_id"].ToString());
                }
                con1.Close();
            }





            float value1 = 0;
            float value2 = 0;
            SqlConnection con22 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd22 = new SqlCommand("select date,sum(Amount) as Credit  from Billing_Entry where  Com_Id='" + company_id + "' and year='" + Label3.Text + "' group by date ", con22);
            SqlDataReader dr22;
            con22.Open();
            dr22 = cmd22.ExecuteReader();
            if (dr22.Read())
            {
                value1 = float.Parse(dr22["Credit"].ToString());
            }
            con22.Close();

            SqlConnection con23 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd23 = new SqlCommand("select date,sum(Amount) as Credit  from WorkshopBilling_Entry where  Com_Id='" + company_id + "' and year='" + Label3.Text + "' group by date ", con23);
            SqlDataReader dr23;
            con23.Open();
            dr23 = cmd23.ExecuteReader();
            if (dr23.Read())
            {
                value2 = float.Parse(dr23["Credit"].ToString());
            }
            con23.Close();

            Label4.Text = (value1 + value2).ToString();

            SqlConnection con24 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd24 = new SqlCommand("select date,sum(Amount) as Credit  from Expence_Entry where  Com_Id='" + company_id + "' and year='" + Label3.Text + "' group by date ", con24);
            SqlDataReader dr24;
            con24.Open();
            dr24 = cmd24.ExecuteReader();
            if (dr24.Read())
            {
                Label5.Text = dr24["Credit"].ToString();
            }
            con24.Close();


        }


    }
    protected void LoginLink_OnClick(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("~/login.aspx");

    }
    protected void BindData()
    {
        
        //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        //SqlCommand CMD = new SqlCommand("select * from product_stock where Com_Id='" + company_id + "' ORDER BY Product_code asc", con);
        //DataTable dt1 = new DataTable();
        //SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        //da1.Fill(dt1);
        //GridView1.DataSource = dt1;
        //GridView1.DataBind();

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {
        float value1 = 0;
        float value2 = 0;
        SqlConnection con22 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd22 = new SqlCommand("select date,sum(Amount) as Credit  from Billing_Entry where date between '" + Convert.ToDateTime(TextBox1.Text).ToString("MM-dd-yyyy") + "' and '" + Convert.ToDateTime(TextBox2.Text).ToString("MM-dd-yyyy") + "' and Com_Id='" + company_id + "' and year='" + Label3.Text + "' group by date ", con22);
        SqlDataReader dr22;
        con22.Open();
        dr22 = cmd22.ExecuteReader();
        if (dr22.Read())
        {
            value1   =float.Parse( dr22["Credit"].ToString());
        }
        con22.Close();

        SqlConnection con23 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd23 = new SqlCommand("select date,sum(Amount) as Credit  from WorkshopBilling_Entry where date between '" + Convert.ToDateTime(TextBox1.Text).ToString("MM-dd-yyyy") + "' and '" + Convert.ToDateTime(TextBox2.Text).ToString("MM-dd-yyyy") + "' and Com_Id='" + company_id + "' and year='" + Label3.Text + "' group by date ", con23);
        SqlDataReader dr23;
        con23.Open();
        dr23 = cmd23.ExecuteReader();
        if (dr23.Read())
        {
            value2 = float.Parse(dr23["Credit"].ToString());
        }
        con23.Close();

        Label4.Text = (value1 + value2).ToString();

        SqlConnection con24 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd24 = new SqlCommand("select date,sum(Amount) as Credit  from Expence_Entry where date between '" + Convert.ToDateTime(TextBox1.Text).ToString("MM-dd-yyyy") + "' and '" + Convert.ToDateTime(TextBox2.Text).ToString("MM-dd-yyyy") + "' and  Com_Id='" + company_id + "' and year='" + Label3.Text + "' group by date ", con24);
        SqlDataReader dr24;
        con24.Open();
        dr24 = cmd24.ExecuteReader();
        if (dr24.Read())
        {
            Label5.Text = dr24["Credit"].ToString();
        }
        con24.Close();
    }
}