﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sales_Report.aspx.cs" Inherits="Admin_Sales_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>
<html lang="en">
    <head id="Head1" runat="server">
         <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
        <title>Quick Car Wash</title>
      

              <script type="text/javascript">

                  $(document).ready(function () {

                      $(".selectpicker").selectpicker();

                  });

                 </script>


        <!-- Bootstrap -->
          <script src="bootstrap/js/jquery-3.1.1.min.js"></script>

          <script src="bootstrap/js/bootstrap-select.js"></script>
           <link href="bootstrap/css/bootstrap-select.css" rel="stylesheet" />
           <link rel="stylesheet" type="text/css" media="screen" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.7.5/css/bootstrap-select.min.css">
         <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
        <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet">
        <link href="css/waves.min.css" type="text/css" rel="stylesheet">
        <!--        <link rel="stylesheet" href="css/nanoscroller.css">-->
        <link href="css/menu.css" type="text/css" rel="stylesheet">
        <link href="css/style.css" type="text/css" rel="stylesheet">
        <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet">
        <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
          <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
        <![endif]-->

        <style>
            .completionList {
        border:solid 1px Gray;
        margin:0px;
        padding:3px;
        height: 120px;
        overflow:auto;
        background-color:#FAEBD7;     
        } 
        .listItem {
        color: #191919;
        } 
        .itemHighlighted {
        background-color: #ADD6FF;       
        }
            .red
            {
                text-align:center;
            }
            .goo
            {
               color:#13c4a5;
            }
            .goo:hover
            {
                color:#3a5a7a;
            }
            .color
            {
                color:#555555;
                height:30px;
            }
        .dropbox
        {
            width:100%;
            height:30px;
        display: block;
        font-size:16px;
        font-family: 'Open Sans',"HelveticaNeue", "Helvetica Neue", Helvetica, Arial,sans-serif;
   
 }
        .gvwCasesPager
        {
           
          color:black;
          margin-right:20px;
          text-align:right;
          padding:20px;
        }
        .gvwCasesPager a
            {
               
                margin-left:10px;
                margin-right:10px;
                font-size:20px;
                
                 padding:10px;
                
              
              
            }

         .dropbox1
        {
            width:10%;
            height:30px;
           
           
            
        }
        
        .see
        {
           height:400px; 
           margin-top:-60px;
        }
        .see1
        {
            margin-top:-20px;
        }
         .see2
        {
          
            margin-left:-15px;
            margin-bottom:30px;
        }
        
          @media (max-width: 767px)
        {
             .see
        {
           height:400px; 
           margin-top:-10px;
        }
         .see1
        {
            margin-top:-40px;
        }
         .see2
        {
            margin-top:50px;
            
        }
      
        }
        
        </style>
    </head>
    <body>
        <!-- Static navbar -->
 <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        
</asp:ToolkitScriptManager>
    <div>
        <nav class="navbar navbar-inverse yamm navbar-fixed-top">
            <div class="container-fluid">
                <button type="button" class="navbar-minimalize minimalize-styl-2  pull-left "><i class="fa fa-bars"></i></button>
                <span class="search-icon"><i class="fa fa-search"></i></span>
                <div class="search" style="display: none;">
                    <form role="form">
                        <input type="text" class="form-control" autocomplete="off" placeholder="Write something and press enter">
                        <span class="search-close"><i class="fa fa-times"></i></span>
                    </form>
                </div>
                  <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="#">Quick Car Wash</a>
                </div>
                <div id="navbar" class="navbar-collapse collapse">
<%--                    <ul class="nav navbar-nav">
                        <li class="dropdown">
                           
                          <li class="dropdown">
                            <a href="#" class="dropdown-toggle button-wave" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
<asp:Button ID="Button4" runat="server"  Text="ADD" class="btn btn-primary"></asp:Button> <span aria-hidden="true" class="glyphicon glyphicon-plus"></span> </a>
                            <ul class="dropdown-menu">
                                <li><a href="Main.aspx"><i class="fa fa-home fa-2x" aria-hidden="true"></i>&nbsp;&nbsp&nbsp;Category</a></li>
                                   <li role="separator" class="divider"></li>
                                <li><a href="Billing_entry.aspx"><i class="fa fa-hdd-o" aria-hidden="true"></i>&nbsp;&nbsp&nbsp;Sub Category </a></li>
                                 <li role="separator" class="divider"></li>
                                <li><a href="Service_entry.aspx"><i class="fa fa-building" aria-hidden="true"></i>&nbsp;&nbsp&nbsp;Product Entry </a></li>
                                   <li role="separator" class="divider"></li>
                                <li><a href="Expense_entry.aspx"><i class="fa fa-check-square-o" aria-hidden="true"></i>&nbsp;&nbsp&nbsp;Purchase Entry </a></li>
                                  <li role="separator" class="divider"></li>
                                <li><a href="Stock_Inventory.aspx"><i class="fa fa-edit"></i> &nbsp;&nbsp&nbsp;Stock / Inventory </a></li>
                                 <li role="separator" class="divider"></li>
                                <li><a href="Customer-Entry.aspx"><i class="fa fa-lightbulb-o" aria-hidden="true"></i>  &nbsp;&nbsp&nbsp;New Customer Entry</a></li>

                                <li role="separator" class="divider"></li>
                                <li><a href="Vendor.aspx"><i class="fa fa-thumbs-o-up" aria-hidden="true"></i> &nbsp;&nbsp&nbsp;Supplier Entry </a></li>
                               
                                  <li role="separator" class="divider"></li>
                                <li><a href="Department-Entry.aspx"><i class="fa fa-ticket" aria-hidden="true"></i>&nbsp;&nbsp&nbsp;New Department Entry  </a></li>
                                <li role="separator" class="divider"></li>
                                <li><a href="Sales_entry.aspx"><i class="fa fa-ticket" aria-hidden="true"></i>&nbsp;&nbsp&nbsp;Sales Entry </a></li>
                               
                            </ul>
                        </li>
                    </ul>--%>
                          
                    <ul class="nav navbar-nav navbar-right navbar-top-drops">
                        <li class="dropdown"><a href="#" class="dropdown-toggle button-wave" data-toggle="dropdown">

</a>

                            
                        <li class="dropdown profile-dropdown">
                            <a href="#" class="dropdown-toggle button-wave" data-toggle="dropdown" role="button" ><img src="../default-profile-pic.png" alt="" width="25px"><%=User.Identity.Name%></b></span>  <span class="fa fa-caret-down" aria-hidden="true" style=""></a>
                            <ul class="dropdown-menu">
                                <li><a href="Profile_main.aspx"><i class="fa fa-user"></i>My Profile</a></li>
                               
                                <li class="divider"></li>
                               
                                 <li ><a href="#" ><asp:LinkButton id="LoginLink" Text="Log Out"  class="fa fa-sign-out" aria-hidden="true"
                      OnClick="LoginLink_OnClick" runat="server" /></a></li>
                            </ul>
                        </li>
                    </ul>
                </div><!--/.nav-collapse -->
            </div><!--/.container-fluid -->
        </nav>
        <section class="page">

                <nav class="navbar-aside navbar-static-side" role="navigation">
                <div class="sidebar-collapse nano">
                    <div class="nano-content">
                        <ul class="nav metismenu" id="side-menu">

                            <li class="active">
                                <a href="Dashboard.aspx"><i class="fa fa-home fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Home </span><span class="fa arrow"></span></a>
                           <ul class="nav nav-second-level collapse">
                                    <li><a href="Dashboard.aspx">Dashboard </a></li>
                           </ul>
                            </li>
              <li>
                                <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Master </span><span class="fa arrow"></span></a>
                          
                          <ul class="nav nav-second-level collapse">
                                    <li><a href="Customer-Entry.aspx">Customer</a></li>
                           </ul>
                       <ul class="nav nav-second-level collapse">
                                    <li><a href="Service_entry.aspx">Service Type</a></li>
                           </ul>
                            <ul class="nav nav-second-level collapse">
                                    <li><a href="Partners_entry.aspx">Partners Type</a></li>
                           </ul>
                       <ul class="nav nav-second-level collapse">
                                    <li><a href="ExpenseName_Entry.aspx">Expense Name Entry</a></li>
                           </ul>
                                 <ul class="nav nav-second-level collapse">
                                    <li><a href="CostofServiceName_Entry.aspx">Cost of Service Name Entry</a></li>
                           </ul>
                                 <ul class="nav nav-second-level collapse">
                                    <li><a href="Workshop_Entry.aspx">Workshop Entry</a></li>
                           </ul>
                       
                           
                            </li>
                           


                           

                    

                                 <li>
                                <a href="Account_ledger.aspx"><i class="fa fa-line-chart fa-2x" aria-hidden="true"></i><span class="nav-label">&nbsp;&nbsp; Accounts </span><span class="fa arrow"></span></a>
                             <ul class="nav nav-second-level collapse">
                                  
                                      <li><a href="Cost_of_Service_entry.aspx">Cost of Service Entry</a></li>  
                                    <li><a href="Expense_entry.aspx">Expense Entry</a></li>
                                  <li><a href="Account_ledger.aspx">Account ledger</a></li>
                                     <li><a href="Profit_and_Loss.aspx">Profit and Loss</a></li>
                                       <li><a href="Sales_payment_outstanding.aspx">Workshop Outstanding</a></li>
                           </ul>
                          
                               
                            
                               
                            
                             <li>
                                <a href="Staff-Entry.aspx"><i class="fa fa-users fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp; Staff </span><span class="fa arrow"></span></a>
                             <ul class="nav nav-second-level collapse">
                                    <li><a href="Staff-Entry.aspx">Entry</a></li>
                           </ul>
                          <ul class="nav nav-second-level collapse">
                                    <li><a href="Attendance_entry.aspx">Attendance Entry</a></li>
                           </ul>
                               <ul class="nav nav-second-level collapse">
                                    <li><a href="SalaryEntry.aspx">Salary Entry</a></li>
                           </ul>
                            
                               
                            </li>
                            
                             <li>
                                <a href="Billing_entry.aspx"><i class="fa fa-file-text-o fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp; Billing </span><span class="fa arrow"></span></a>
                             <ul class="nav nav-second-level collapse">
                                    <li><a href="Billing_entry.aspx">Billing Entry</a></li>
                                      <li><a href="Sales_Report.aspx">Billing Report</a></li>
                                  <li><a href="WorkshopBilling_entry.aspx">Workshop Billing Entry</a></li>
                                    <li><a href="WorkShop_Report.aspx">Workshop Report</a></li>
                           </ul>
                          
                               
                            </li>
                  
                                            
                        </ul>

                    </div>
                </div>
                
            </nav>
            <div id="wrapper">
                <div class="content-wrapper container">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title see2">
                                <h2>Sales report
                                 </h2>
                             
                             
  



                                
                            </div>
                            
                        </div>
                    </div><!-- end .page title-->
                     <div class="row">
                    <div class="col-md-12">
                  




                    <div class="row see"  >
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    <br />

                                    <br />
                    <div class="container">
                    
                           <div class="container">
                         <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                      <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBox1" Format="dd-MM-yyyy"></asp:CalendarExtender>
                                      <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                      <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="TextBox2" Format="dd-MM-yyyy"></asp:CalendarExtender>
                                        <asp:DropDownList ID="DropDownList5" runat="server" Height="30px" >
                                   <asp:ListItem>PDF</asp:ListItem>
                                   <asp:ListItem>WORD</asp:ListItem>
                                   <asp:ListItem>EXCEL</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button ID="Button8" runat="server" class="btn-primary"  Width="70px" Height="30px" 
                                    Text="Report" onclick="Button8_Click" />
 
  <div class="panel panel-default">
 



  
<div class="col-lg-12">


<hr />
</div>


<div class="panel-body">
   <div class="col-md-6">

                             <div class="form-group"><label class="col-lg-3 control-label">From Date</label>

                                    <div class="col-lg-9">
                                     <asp:UpdatePanel ID="UpdatePanel5" runat="server">
   <ContentTemplate>
  
                                    <asp:TextBox ID="TextBox3" runat="server" class="form-control input-x2 dropbox"  AutoPostBack="true"
                                        ontextchanged="TextBox3_TextChanged"></asp:TextBox>
                                      <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                        TargetControlID="TextBox3" Format="MM-dd-yyyy" TodaysDateFormat="dd-MM-yyyy"></asp:CalendarExtender>
                                      </ContentTemplate>
                                      </asp:UpdatePanel></div></div>
                                      
                                      <br/>
                                       <br/>
                                      
                              <div class="form-group"><label class="col-lg-3 control-label">To Date</label>

                                    <div class="col-lg-9">
                                     <asp:UpdatePanel ID="UpdatePanel6" runat="server">
   <ContentTemplate>
  <asp:TextBox ID="TextBox4" runat="server" class="form-control input-x2 dropbox" AutoPostBack="true" 
           ontextchanged="TextBox4_TextChanged"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" 
           runat="server" TargetControlID="TextBox4" Format="dd-MM-yyyy"></asp:CalendarExtender>
                                      </ContentTemplate>
                                      </asp:UpdatePanel></div></div>            
                                      <br />
                                     
                                      
                                      
                                      
                                      
                                      
                                      
                                      
                                      
                                      </div>




  

                    
                              
                                      
 <div class="col-md-6">

                        <div class="form-group"><label class="col-lg-3 control-label">Vehicle No</label>

                                    <div class="col-lg-9">
                                     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
   <ContentTemplate>
<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" 
           class="form-control input-x2 dropbox" 
           onselectedindexchanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
                               <asp:TextBox ID="TextBox6" runat="server"  class="form-control input-x2 dropbox" AutoPostBack="true" ontextchanged="TextBox6_TextChanged" ></asp:TextBox>
                                         <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" MinimumPrefixLength="1" ServiceMethod="SearchCustomers1" FirstRowSelected = "false" CompletionInterval="0" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox6"  CompletionListCssClass="completionList"
     CompletionListItemCssClass="listItem"
     CompletionListHighlightedItemCssClass="itemHighlighted">
      </asp:AutoCompleteExtender>
                                      </ContentTemplate>
                                      </asp:UpdatePanel></div></div>
                                       <br/>
                                       <br/>
                                        <div class="form-group"><label class="col-lg-3 control-label">Customer Name</label>

                                    <div class="col-lg-9">
                                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
   <ContentTemplate>
<asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true"  
           class="form-control input-x2 dropbox" 
           onselectedindexchanged="DropDownList2_SelectedIndexChanged"></asp:DropDownList>
                               <asp:TextBox ID="TextBox5" runat="server"  class="form-control input-x2 dropbox" AutoPostBack="true" ontextchanged="TextBox5_TextChanged" ></asp:TextBox>
                                         <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" MinimumPrefixLength="1" ServiceMethod="SearchCustomers" FirstRowSelected = "false" CompletionInterval="0" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox5"  CompletionListCssClass="completionList"
     CompletionListItemCssClass="listItem"
     CompletionListHighlightedItemCssClass="itemHighlighted">
      </asp:AutoCompleteExtender>
                                      </ContentTemplate>
                                      </asp:UpdatePanel></div></div>
                                      
                                      
                                      
                                      </div>





<div class="col-lg-6">
                  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                           </asp:UpdatePanel>

</div>

</div>



</div>
</div>








<div class="container">

  <div class="panel panel-default">
  <div class="panel-body">
   <div class="col-md-12">
     <asp:UpdatePanel ID="UpdatePanel7" runat="server">
   <ContentTemplate>
   
<div class="col-md-12">
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
   <asp:GridView ID="GridView1" runat="server" class="Grd1" Width="100%" CellPadding="3" 
         Font-Size="16px" 
           AutoGenerateColumns="False" AllowPaging="True" 
        onpageindexchanging="GridView1_PageIndexChanging" 
        onrowdatabound="GridView1_RowDataBound" PageSize="20" BackColor="White" 
           BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
       <Columns>
         
           <asp:BoundField HeaderText="Invoice No" DataField="Invoice_id"  >
             <HeaderStyle CssClass="Grd1" />
           </asp:BoundField>
             <asp:BoundField HeaderText="Date" DataField="date" DataFormatString="{0:dd/MM/yyyy}" >
              <HeaderStyle CssClass="Grd1" />
           </asp:BoundField>
              <asp:BoundField HeaderText="Vehicle No" DataField="Customer_VehNo" >
             <HeaderStyle CssClass="Grd1" />
           </asp:BoundField>
             <asp:BoundField HeaderText="Customer Name" DataField="Customer_name" >
             <HeaderStyle CssClass="Grd1" />
           </asp:BoundField>
            <asp:BoundField HeaderText="Address" DataField="Address">
              <HeaderStyle CssClass="Grd1" />
           </asp:BoundField>
             <asp:BoundField HeaderText="Mobile No" DataField="Mobile_No" >
            <HeaderStyle CssClass="Grd1" />
           </asp:BoundField>
           
              <asp:BoundField HeaderText="Service Name" DataField="Service_Name">
             <HeaderStyle CssClass="Grd1" />
           </asp:BoundField>
             <asp:BoundField HeaderText="Amount" DataField="Amount">

           <HeaderStyle CssClass="Grd1" />
           </asp:BoundField>

           <asp:TemplateField>
          <ItemTemplate>
              <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/delete3.png" Height="20px" Width="20px"  onclick="ImageButton9_Click" OnClientClick="return confirm('Do you want to delete this entry')" />
          
          </ItemTemplate>
          
          </asp:TemplateField>

  
       </Columns>
       <FooterStyle BackColor="White" ForeColor="#000066" />
       <HeaderStyle Height="40px" BackColor="#006699" Font-Bold="True" CssClass="red" 
           ForeColor="White" />
       <PagerSettings FirstPageText="First" LastPageText="Last" />
       <PagerStyle Wrap="true" BorderStyle="Solid" Width="100%" 
           CssClass="gvwCasesPager" BackColor="White" ForeColor="#000066" 
           HorizontalAlign="Left" />
       <RowStyle Height="40px" ForeColor="#000066" />
       <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
       <SortedAscendingCellStyle BackColor="#F1F1F1" />
       <SortedAscendingHeaderStyle BackColor="#007DBB" />
       <SortedDescendingCellStyle BackColor="#CAC9C9" />
       <SortedDescendingHeaderStyle BackColor="#00547E" />
       </asp:GridView>
  </ContentTemplate>
    <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridView1"  />
               
                   <asp:AsyncPostBackTrigger ControlID="DropDownList2" EventName="SelectedIndexChanged"  />
                     <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged"  />
                               </Triggers>
    </asp:UpdatePanel>



     <asp:UpdatePanel ID="UpdatePanel9" runat="server">
   <ContentTemplate>
        <asp:Button ID="Button15" runat="server" Text="Button" style="display:none"  />
  
  
    <asp:Panel ID="Panel2" runat="server" class="panel1" BorderColor="Black" BorderStyle="Solid" BackColor="White" Direction="LeftToRight" style="display:none;" 
                         HorizontalAlign="Left" ScrollBars="Both" Width="500px" Height="300px"  >
    
         <div style="padding:12px; border:1px solid #e5e5e5;   border-radius:10px; background-color:#E6E6FA;color:#233445; font-size:15px; font-weight:400px; font-family: 'Open Sans',"HelveticaNeue", "Helvetica Neue", Helvetica, Arial,sans-serif; ">
                     <h3 style="font-size:20px; " class="control-label"> Update brand  <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/exit11.png" width="30px" height="30px" style="float:right" /></h3>
  
     
  
           
        </div>
        <div class="tablestyles">
        <table>
       
        <tr>
        <td>
            <asp:Label ID="Label28" runat="server" class="col-lg-3 control-label" Width="200px" Text="SubCategory Id"></asp:Label></td>
        <td>
            <asp:Label ID="Label29" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
        <td >
            <asp:Label ID="Label2" runat="server" class="col-lg-3 control-label" Width="200px"  Text="Category Name"></asp:Label></td>
        <td>
            <asp:DropDownList ID="DropDownList4" class="form-control input-x2 dropbox" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
        <td>
            <asp:Label ID="Label30" runat="server" class="col-lg-3 control-label" Width="200px" Text="Brand Name"></asp:Label></td>
        <td>
            <asp:TextBox ID="TextBox16" runat="server"  class="form-control input-x2 dropbox"></asp:TextBox></td>
        </tr>
     
                    
       
        </tr>
            <tr>
            <td></td>
                <td>
                 
                </td>
                <td>
              
                    <asp:Label ID="Label31" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
       </div>

        </asp:Panel>
       <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Button15" PopupControlID="Panel2" CancelControlID="ImageButton6" BackgroundCssClass="modelbackground">
        </asp:ModalPopupExtender>


        </ContentTemplate>
    <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridView1"  />
            
                </Triggers>
    </asp:UpdatePanel>

  
</div>

        </ContentTemplate>
                                     <Triggers>
              
              
                    
                  
                  </Triggers>

                           </asp:UpdatePanel>
                           
                           <asp:Button ID="Button1" runat="server" 
           Text="Export to excel" onclick="Button1_Click"></asp:Button>
           

                </div>
                </div>
                </div>
                </div>
                </div>
                </div>
                </div>
                </div>
                </div>
                </div>
                
                   
                  
                           
        </section>

        <script type="text/javascript" src="js/jquery.min.js"></script>
        <script type="text/javascript" src="bootstrap/js/bootstrap.min.js"></script>
        <script src="js/metisMenu.min.js"></script>
        <script src="js/jquery-jvectormap-1.2.2.min.js"></script>
        <!-- Flot -->
        <script src="js/flot/jquery.flot.js"></script>
        <script src="js/flot/jquery.flot.tooltip.min.js"></script>
        <script src="js/flot/jquery.flot.resize.js"></script>
        <script src="js/flot/jquery.flot.pie.js"></script>
        <script src="js/chartjs/Chart.min.js"></script>
        <script src="js/pace.min.js"></script>
        <script src="js/waves.min.js"></script>
        <script src="js/jquery-jvectormap-world-mill-en.js"></script>
        <!--        <script src="js/jquery.nanoscroller.min.js"></script>-->
        <script type="text/javascript" src="js/custom.js"></script>
        <script type="text/javascript">
            $(function () {

                var barData = {
                    labels: ["January", "February", "March", "April", "May", "June", "July"],
                    datasets: [
                        {
                            label: "My First dataset",
                            fillColor: "rgba(220,220,220,0.5)",
                            strokeColor: "rgba(220,220,220,0.8)",
                            highlightFill: "rgba(220,220,220,0.75)",
                            highlightStroke: "rgba(220,220,220,1)",
                            data: [65, 59, 80, 81, 56, 55, 40]
                        },
                        {
                            label: "My Second dataset",
                            fillColor: "rgba(14, 150, 236,0.5)",
                            strokeColor: "rgba(14, 150, 236,0.8)",
                            highlightFill: "rgba(14, 150, 236,0.75)",
                            highlightStroke: "rgba(14, 150, 236,1)",
                            data: [28, 48, 40, 19, 86, 27, 90]
                        }
                    ]
                };

                var barOptions = {
                    scaleBeginAtZero: true,
                    scaleShowGridLines: true,
                    scaleGridLineColor: "rgba(0,0,0,.05)",
                    scaleGridLineWidth: 1,
                    barShowStroke: true,
                    barStrokeWidth: 2,
                    barValueSpacing: 5,
                    barDatasetSpacing: 1,
                    responsive: true
                };


                var ctx = document.getElementById("barChart").getContext("2d");
                var myNewChart = new Chart(ctx).Bar(barData, barOptions);

            });
        </script>
        <!-- Google Analytics:  -->
        <script>
            (function (i, s, o, g, r, a, m) {
                i['GoogleAnalyticsObject'] = r;
                i[r] = i[r] || function () {
                    (i[r].q = i[r].q || []).push(arguments);
                }, i[r].l = 1 * new Date();
                a = s.createElement(o),
                        m = s.getElementsByTagName(o)[0];
                a.async = 1;
                a.src = g;
                m.parentNode.insertBefore(a, m)
            })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');
            ga('create', 'UA-3560057-28', 'auto');
            ga('send', 'pageview');
        </script>
        </form>
    </body>
</html>



