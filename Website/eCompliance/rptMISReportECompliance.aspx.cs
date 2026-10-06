using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Ganini;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms; //required for creating local report scs 231213

public partial class rptMISReportECompliance : System.Web.UI.Page
{
    ProcessBus Bus = new ProcessBus();
    ReportMsg reportMsg = new ReportMsg();
    List<ActMasterMsg> ActList = new List<ActMasterMsg>();
    List<ActivityMasterMsg> ActivityList = new List<ActivityMasterMsg>();
    List<SeverityMasterMsg> SevList = new List<SeverityMasterMsg>();
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
            DateTime LastMonthFromtDate = DateTime.Today.AddDays(0 - DateTime.Today.Day);
            frmDate = Convert.ToDateTime(LastMonthFromtDate.AddDays(1 - LastMonthFromtDate.Day));
            txtFromDate.Text = frmDate.ToString("dd/MM/yyyy");

            //DateTime LastMonthLastDate = DateTime.Today.AddDays(0 - DateTime.Today.Day);
            //EndDate = Convert.ToDateTime(LastMonthLastDate);
            
            txtEndDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            reportMsg.FromDate = (txtFromDate.Text);
            reportMsg.ToDate = (txtEndDate.Text);
            Loadcompany();
            //LoadAct();
            //LoadSeverity();
            ReportViewer.Visible = false;
          }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        reportMsg.FromDate = (txtFromDate.Text);
        reportMsg.ToDate = (txtEndDate.Text);
        if (ddlLocation.SelectedIndex == 0)
        {
            reportMsg.CompanyCode = "0";
        }
        else
        {

            reportMsg.CompanyCode = (ddlLocation.SelectedValue);
        }

        if (ddlComplianceStatus.SelectedIndex == 0)
        {
            reportMsg.ComplianceStatus = "ALL";
        }
        else
        {

            reportMsg.ComplianceStatus = (ddlComplianceStatus.SelectedItem.ToString());
        }
        if (ddlActionStatus.SelectedIndex > 0)
        {
            reportMsg.ActionStatus = ddlActionStatus.SelectedValue;
        }
        else
        {
            reportMsg.ActionStatus = "ALL";
        }
        reportMsg.EmployeeCode = BaseMsg.EmployeeCode;
        LoadReport(reportMsg);
    }
    private void Loadcompany()
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.EmployeeCode = BaseMsg.EmployeeCode;
        List<CompanyMessage> CompList = new List<CompanyMessage>();
        CompList = Bus.CompanyMasterSelect(Emp);

        var CList = (from cmp in CompList where cmp.IsActive == true  select new { cmp.CompanyCode, cmp.CompanyShortName }).ToList();
        if (CList.Count > 0)
        {
            ddlLocation.DataTextField = "CompanyShortName";
            ddlLocation.DataValueField = "CompanyCode";
            ddlLocation.DataSource = CList;
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, "-- ALL --");
        }
        else
        {
            ddlLocation.Items.Insert(0, "-- ALL --");
            ddlLocation.Enabled = false;
        }
    }
    //private void LoadAct()
    //{
    //    ActMasterMsg Act = new ActMasterMsg();
        
    //    Act.Flag = "R";
    //    Act.ActId = 0;
    //    Act.CreatedBy = BaseMsg.EmployeeCode;
    //    ActList = Bus.MasActsInsertUpdateandDelete(Act);
    //    var Actlst = (from act in ActList where act.IsActive == true select new { act.ActName, act.ActId }).Distinct().ToList();
    //    if (ActList.Count > 0)
    //    {
    //        ddlAct.DataTextField = "ActName";
    //        ddlAct.DataValueField = "ActId";
    //        ddlAct.DataSource = Actlst;
    //        ddlAct.DataBind();
    //        ddlAct.Items.Insert(0, "-- ALL --");
    //    }
    //    else
    //    {
    //        ddlAct.Items.Insert(0, "-- ALL --");
    //    }
    //}
    //private void LoadSeverity()
    //{
    //    SeverityMasterMsg Sevty = new SeverityMasterMsg();
    //    Sevty.Flag = "R";
    //    SevList = Bus.MasSeverityInsertUpdateandDelete(Sevty);
    //    var sevlst = (from act in SevList where act.IsActive == true select new { act.SeverityId, act.SeverityName }).Distinct().ToList();
    //    if (sevlst.Count > 0)
    //    {
    //        ddlSeverity.DataTextField = "SeverityName";
    //        ddlSeverity.DataValueField = "SeverityId";
    //        ddlSeverity.DataSource = sevlst;
    //        ddlSeverity.DataBind();
    //        ddlSeverity.Items.Insert(0, "-- ALL --");
    //    }

    //    else
    //    {
    //        ddlSeverity.Items.Insert(0, "-- ALL --");
    //    }

       
    //}
    public void LoadReport(ReportMsg reportMsg)
    {
       
        //string Location = ddlLocation.SelectedItem.ToString();
        string ReportTitle = " MIS Report of E Complinace : ";
        //string Locationwise = "  For Location:  " + Location;
        string selParam = " For the Period:  " + txtFromDate.Text + " To " + txtEndDate.Text;// + ddlAct.SelectedItem;
        string Wstatus = " Compliance Status: " + ddlComplianceStatus.SelectedItem + ".  Action Status: " + ddlActionStatus.SelectedItem;
        ReportTitle = ReportTitle + "--" + selParam + " " + Wstatus + ".";
        DataTable dt = new DataTable();
        //dt = Bus.rptPlantwiseActivityMIS(reportMsg);
        dt = Bus.rptMISReportECompliance(reportMsg);
       
        if (dt != null && dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            Microsoft.Reporting.WebForms.ReportDataSource rptDatasource = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt);
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(rptDatasource);
            LocalReport rep = ReportViewer.LocalReport;
            string reppath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["RptPath"]);

            rep.ReportPath = reppath + "MISeCompliance.rdlc";

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