using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Ganini;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms; //required for creating local report scs 231213

public partial class rptActActivityAssigned : System.Web.UI.Page
{
    ProcessBus Bus = new ProcessBus();
    ReportMsg reportMsg = new ReportMsg();
    List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
   
    BaseClass BaseMsg = new BaseClass();
     //public static List<CompanyMsg> CompList = new List<CompanyMsg>();
    public static int EstateGroupCount = 0;
    public static int EstateCount = 0;
   // int NoOfYears = 5; //Convert.ToInt32(Config.GetAppsetting("NoOfYears"));
    //int ParameterId = 0;
    //bool IsActualValue = true;
    DateTime FromDate = DateTime.Today;
    //DateTime ToDate = DateTime.Today;
    DateTime frmDate;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Loadcompany();

            ddlExecutionEmployee.Enabled = false;
            ReportViewer.Visible = false;
          }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedIndex > 0)
        {
            if (ddlExecutionEmployee.SelectedIndex > 0)
            {
                reportMsg.EmployeeCode = ddlExecutionEmployee.SelectedValue.ToString();
            }
            else
            {
                reportMsg.EmployeeCode = "ALL";
            }
            reportMsg.CompanyCode = ddlLocation.SelectedValue.ToString();

            LoadReport(reportMsg);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Select the Location.." + "');", true);
            ddlExecutionEmployee.Enabled = false;
        }
        
    }
    protected void ddlLocation_Changed(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedIndex > 0)
        {
            LoadEmployees();
            ddlExecutionEmployee.Enabled = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Select the Location.." + "');", true);
            ddlExecutionEmployee.Enabled = false;
        }
    }
    private void Loadcompany()
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.EmployeeCode = BaseMsg.EmployeeCode;
        List<CompanyMessage> CompList = new List<CompanyMessage>();
        CompList = Bus.CompanyMasterSelect(Emp);

        //var CList = (from cmp in CompList where cmp.IsActive == true && (cmp.CompanyFlag.ToLower() == "ho" || cmp.CompanyFlag.ToLower() == "co") 
          //             select new { cmp.CompanyCode, cmp.CompanyShortName }).ToList();
        var CList = (from cmp in CompList where cmp.IsActive == true select new { cmp.CompanyCode, cmp.CompanyShortName }).ToList();
        if (CList.Count > 0)
        {
            ddlLocation.DataTextField = "CompanyShortName";
            ddlLocation.DataValueField = "CompanyCode";
            ddlLocation.DataSource = CList;
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, "-- Select --");
        }
        else
        {
            ddlLocation.Items.Insert(0, "-- Select --");
            ddlLocation.Enabled = false;
        }
    }
    public void LoadEmployees() //To load data into grid.
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.Flag = "R";
        Emp.EmployeeCode = BaseMsg.EmployeeCode; //scs 120813
        EmpList = Bus.MasEmployeeInsertUpdateandDelete(Emp);
        var EList = (from emp in EmpList where emp.IsActive == true && emp.CompanyCode == ddlLocation.SelectedValue.ToString()
                      select new { emp.EmployeeCode, emp.EmployeeName }).ToList();
        if (EList.Count > 0)
        {
            ddlExecutionEmployee.DataSource = "";
            ddlExecutionEmployee.DataTextField = "EmployeeName";
            ddlExecutionEmployee.DataValueField = "EmployeeCode";
            ddlExecutionEmployee.DataSource = EList;
            ddlExecutionEmployee.DataBind();
            ddlExecutionEmployee.Items.Insert(0, "-- ALL --");
            ddlExecutionEmployee.Enabled = true;
        }
        else
        {
            //ddlExecutionEmployee.Items.Insert(0, "-- ALL --");
            ddlExecutionEmployee.Enabled = false;
        }
    }
    public void LoadReport(ReportMsg reportMsg)
    {
       
        //string Location = ddlLocation.SelectedItem.ToString();
        string ReportTitle = " Activity Assigned Report: ";
        if (ddlExecutionEmployee.SelectedIndex > 0)
        {
            ReportTitle = ReportTitle + " For " + ddlLocation.SelectedItem + " " + ddlExecutionEmployee.SelectedItem + " Employee";
        }
        else
        {
            ReportTitle = ReportTitle + " For " + ddlLocation.SelectedItem + " " + ddlExecutionEmployee.SelectedItem + " Employees";
        }
        DataTable dt = new DataTable();
        dt = Bus.rptActivityAssigned(reportMsg);
       
        if (dt != null && dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            Microsoft.Reporting.WebForms.ReportDataSource rptDatasource = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt);
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(rptDatasource);
            LocalReport rep = ReportViewer.LocalReport;
            string reppath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["RptPath"]);

            rep.ReportPath = reppath + "ActivityAssigned.rdlc";

            ReportParameter rp1 = new ReportParameter();
            rp1.Name = "ReportTitle";
            rp1.Values.Add(ReportTitle);
            ReportViewer.LocalReport.SetParameters(rp1);

            //ReportParameter rp2 = new ReportParameter();
            //rp2.Name = "SelectParam";
            //rp2.Values.Add(selParam);
            //ReportViewer.LocalReport.SetParameters(rp2);

            ReportViewer.Visible = true;
            ReportViewer.LocalReport.Refresh();
        }
        else
        {
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "No Data Available for the Selected Period." + "');", true);
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Data notavbl." + "');", true);
            ReportViewer.Visible = false;
        }
    }
}