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


public partial class Admin_Billing_entry : System.Web.UI.Page
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
            TextBox8.Text = Convert.ToDateTime(date).ToString("MM-dd-yyyy");

            this.Form.DefaultButton = Button1.UniqueID;
            DropDownList3.Focus();
            DropDownList3.Attributes.Add("onkeypress", "return controlEnter('" + TextBox3.ClientID + "', event)");
          
          
            getinvoiceno();
            show_category();
            show_VehicleNO();
            SearchMobileno();
            SearchCustomer();
            showrating();
            BindData();
            active();
            created();

            Button4.Visible = false;
        }

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Button4.Visible = true;
        TextBox8.Focus();
        ImageButton IMG = (ImageButton)sender;
        GridViewRow ROW = (GridViewRow)IMG.NamingContainer;
        Label1.Text = ROW.Cells[1].Text;
        TextBox8.Text = ROW.Cells[2].Text;
        DropDownList3.SelectedItem.Text = ROW.Cells[3].Text;
        TextBox6.Text = ROW.Cells[3].Text;
        TextBox3.Text = ROW.Cells[4].Text;
        TextBox2.Text = ROW.Cells[5].Text;
        TextBox4.Text = ROW.Cells[6].Text;
        DropDownList1.SelectedItem.Text = ROW.Cells[7].Text;
        TextBox14.Text = ROW.Cells[7].Text;
        TextBox5.Text = ROW.Cells[8].Text;
    }
    protected void Button16_Click(object sender, EventArgs e)
    {

    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        int value = 0;
        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("update Billing_Entry set date='" + TextBox8.Text +
            "', Customer_name='" + HttpUtility.HtmlDecode(TextBox3.Text) +
            "', Mobile_No='" + HttpUtility.HtmlDecode(TextBox2.Text) +
            "', Address='" + HttpUtility.HtmlDecode(TextBox4.Text) +
            "', Amount='" + HttpUtility.HtmlDecode(TextBox5.Text) +
             "', Com_Id='" + company_id +
             "', Customer_VehNo='" + HttpUtility.HtmlDecode(TextBox6.Text) +
             "', Service_Name='" + HttpUtility.HtmlDecode(TextBox14.Text) +
             "', status='" + HttpUtility.HtmlDecode("Sales-" + TextBox14.Text) +
             "', value='" + value +
            "' where Invoice_id='" + Label1.Text + "'  and Com_Id='" + company_id + "' ", CON);

        CON.Open();
        cmd.ExecuteNonQuery();
        CON.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Invoice Updated successfully')", true);

        BindData();
        getinvoiceno();
        show_category();
        show_VehicleNO();
        TextBox3.Text = "";
        TextBox2.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        Button4.Visible = false;
        DateTime date = DateTime.Now;
        TextBox8.Text = Convert.ToDateTime(date).ToString("MM-dd-yyyy");

    }

    protected void Button17_Click(object sender, EventArgs e)
    {
        
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd1 = new SqlCommand("delete from Billing_Entry where Invoice_id='" + Label29.Text + "' and Com_Id='" + company_id + "' ", con1);
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
                SqlCommand cmd = new SqlCommand("delete from Billing_Entry where Invoice_id='" + usrid + "' and Com_Id='" + company_id + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Invoice deleted successfully')", true);
        BindData();
        getinvoiceno();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        if (DropDownList3.SelectedItem.Text == "All")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Select valid Vehicle No')", true);
        }
        else if (DropDownList1.SelectedItem.Text == "All")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Select valid Service Type')", true);
        }
        else
        {
            int value = 0;
            SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd = new SqlCommand("insert into Billing_Entry values(@Invoice_id,@date,@Customer_name,@Mobile_No,@Address,@Amount,@Com_Id,@Customer_VehNo,@Service_Name,@status,@value)", CON);
            cmd.Parameters.AddWithValue("@Invoice_id", Label1.Text);
            cmd.Parameters.AddWithValue("@date", TextBox8.Text);    
            cmd.Parameters.AddWithValue("@Customer_name", HttpUtility.HtmlDecode(TextBox3.Text));
            cmd.Parameters.AddWithValue("@Mobile_No", HttpUtility.HtmlDecode(TextBox2.Text));
            cmd.Parameters.AddWithValue("@Address", HttpUtility.HtmlDecode(TextBox4.Text));
            cmd.Parameters.AddWithValue("@Amount", HttpUtility.HtmlDecode(TextBox5.Text));
            cmd.Parameters.AddWithValue("@Com_Id", company_id);
            cmd.Parameters.AddWithValue("@Customer_VehNo", HttpUtility.HtmlDecode(TextBox6.Text));
            cmd.Parameters.AddWithValue("@Service_Name", HttpUtility.HtmlDecode(TextBox14.Text));
            cmd.Parameters.AddWithValue("@status","Sales-" +TextBox14.Text);
            cmd.Parameters.AddWithValue("@value", value);
            CON.Open();
            cmd.ExecuteNonQuery();
            CON.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Invoice created successfully')", true);
            BindData();
            getinvoiceno();
            show_category();
            show_VehicleNO();
          
            TextBox3.Text = "";
            TextBox2.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            DateTime date = DateTime.Now;
            TextBox8.Text = Convert.ToDateTime(date).ToString("MM-dd-yyyy");
           
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        DateTime date = DateTime.Now;
        TextBox8.Text = Convert.ToDateTime(date).ToString("MM-dd-yyyy");

        TextBox3.Text = "";
        TextBox2.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox1.Text = "";
        getinvoiceno();
        show_category();
        show_VehicleNO();
        Button4.Visible = false;
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
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from Billing_Entry where Com_Id='" + company_id + "' ORDER BY Invoice_id asc", con);
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
        SqlCommand cmd = new SqlCommand("delete from Billing_Entry where Invoice_id='" + row.Cells[1].Text + "' and Com_Id='" + company_id + "' ", con);
        cmd.ExecuteNonQuery();
        con.Close();

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Invoice Deleted successfully')", true);

        BindData();
        getinvoiceno();
    }


    private void getinvoiceno()
    {

      
        int a;

        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        con1.Open();
        string query = "Select COUNT(Invoice_id) from Billing_Entry where Com_Id='" + company_id + "' ";
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
    private void show_VehicleNO()
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

        DropDownList2.DataSource = ds;
        DropDownList2.DataTextField = "Customer_VehNo";
        DropDownList2.DataValueField = "Custom_Code";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("All", "0"));

        DropDownList4.DataSource = ds;
        DropDownList4.DataTextField = "Customer_VehNo";
        DropDownList4.DataValueField = "Custom_Code";
        DropDownList4.DataBind();
        DropDownList4.Items.Insert(0, new ListItem("All", "0"));

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


        DropDownList6.DataSource = ds;
        DropDownList6.DataTextField = "Mobile_no";
        DropDownList6.DataValueField = "Custom_Code";
        DropDownList6.DataBind();
        DropDownList6.Items.Insert(0, new ListItem("All", "0"));

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


        DropDownList5.DataSource = ds;
        DropDownList5.DataTextField = "Custom_Name";
        DropDownList5.DataValueField = "Custom_Code";
        DropDownList5.DataBind();
        DropDownList5.Items.Insert(0, new ListItem("All", "0"));
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
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        con.Open();
        SqlCommand cmd2 = new SqlCommand("Select * from Customer_Entry where Custom_Code='" + DropDownList3.SelectedItem.Value + "' and Com_Id='" + company_id + "'", con);
        SqlDataReader dr1;
        dr1 = cmd2.ExecuteReader();
        if (dr1.Read())
        {
            TextBox3.Text = dr1["Custom_Name"].ToString();
            TextBox2.Text = dr1["Mobile_no"].ToString();
            TextBox4.Text = dr1["Custom_Add"].ToString();
            TextBox6.Text = dr1["Customer_VehNo"].ToString();
            
        }
        con.Close();
    }




    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

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



    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
       

        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from Billing_Entry where Customer_VehNo='" + TextBox1.Text + "' and Com_Id='" + company_id + "'", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {
       
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
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from Billing_Entry where Customer_VehNo='" + DropDownList2.SelectedItem.Text + "' and Com_Id='" + company_id + "' ORDER BY Invoice_id asc", con1);
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
        SqlCommand CMD = new SqlCommand("select * from Billing_Entry where Customer_name='" + DropDownList5.SelectedItem.Text + "' and Com_Id='" + company_id + "' ORDER BY Invoice_id asc", con1);
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
        SqlCommand CMD = new SqlCommand("select * from Billing_Entry where Mobile_No='" + DropDownList6.SelectedItem.Text + "' and Com_Id='" + company_id + "' ORDER BY Invoice_id asc", con1);
        DataTable dt1 = new DataTable();
        con1.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
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
                    using (SqlCommand cmd = new SqlCommand("billing1", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@No", int.Parse(TextBox13.Text));
                        cmd.Parameters.AddWithValue("@Com_id", (company_id));
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
            report.ReportPath = Server.MapPath("~/Admin/Report.rdlc");
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
}