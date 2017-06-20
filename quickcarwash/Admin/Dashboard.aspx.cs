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


            DataTable dt = new DataTable();
           using (SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select  CONVERT(VARCHAR(10),Month(date),101),sum(Amount) from Billing_Entry group by Month(date)", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            string[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == "1")
                {
                    x[i] = "Jan";
                }
                if (dt.Rows[i][0].ToString() == "2")
                {
                    x[i] = "Feb";
                }
                if (dt.Rows[i][0].ToString() == "3")
                {
                    x[i] = "Mar";
                }
                if (dt.Rows[i][0].ToString() == "3")
                {
                    x[i] = "Apr";
                }
                if (dt.Rows[i][0].ToString() == "5")
                {
                    x[i] = "May";
                }
                if (dt.Rows[i][0].ToString() == "6")
                {
                    x[i] = "Jun";
                }
                 if (dt.Rows[i][0].ToString() == "7")
                {
                    x[i] = "Jul";
                }
                 if (dt.Rows[i][0].ToString() == "8")
                 {
                     x[i] = "Aug";
                 }
                 if (dt.Rows[i][0].ToString() == "9")
                 {
                     x[i] = "Sep";
                 }
                 if (dt.Rows[i][0].ToString() == "10")
                 {
                     x[i] = "Oct";
                 }
                 if (dt.Rows[i][0].ToString() == "11")
                 {
                     x[i] = "Oct";
                 }
                 if (dt.Rows[i][0].ToString() == "12")
                 {
                     x[i] = "Dec";
                 }
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }
            Chart2.Series[0].Points.DataBindXY(x, y);
        }


      
        if(!IsPostBack)
{
DataTable dt = new DataTable();
using (SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]))
{
con.Open();
SqlCommand cmd = new SqlCommand("select CONVERT(VARCHAR(10),Month(date),101),sum(Invoice_id) from Billing_Entry group by Month(date)", con);
SqlDataAdapter da = new SqlDataAdapter(cmd);
da.Fill(dt);
con.Close();
}
string []x=new string[dt.Rows.Count];
int [] y = new int[dt.Rows.Count];
for(int i=0;i<dt.Rows.Count;i++)
{
    if (dt.Rows[i][0].ToString() == "1")
    {
        x[i] = "Jan";
    }
    if (dt.Rows[i][0].ToString() == "2")
    {
        x[i] = "Feb";
    }
    if (dt.Rows[i][0].ToString() == "3")
    {
        x[i] = "Mar";
    }
    if (dt.Rows[i][0].ToString() == "3")
    {
        x[i] = "Apr";
    }
    if (dt.Rows[i][0].ToString() == "5")
    {
        x[i] = "May";
    }
    if (dt.Rows[i][0].ToString() == "6")
    {
        x[i] = "Jun";
    }
    if (dt.Rows[i][0].ToString() == "7")
    {
        x[i] = "Jul";
    }
    if (dt.Rows[i][0].ToString() == "8")
    {
        x[i] = "Aug";
    }
    if (dt.Rows[i][0].ToString() == "9")
    {
        x[i] = "Sep";
    }
    if (dt.Rows[i][0].ToString() == "10")
    {
        x[i] = "Oct";
    }
    if (dt.Rows[i][0].ToString() == "11")
    {
        x[i] = "Oct";
    }
    if (dt.Rows[i][0].ToString() == "12")
    {
        x[i] = "Dec";
    }
y[i] = Convert.ToInt32(dt.Rows[i][1]);
}
Chart1.Series[0].Points.DataBindXY(x,y);
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
}