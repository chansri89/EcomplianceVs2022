using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Ganini;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms; //required for creating local report scs 231213

public partial class rptActionComplete : System.Web.UI.Page
{
    ProcessBus Bus = new ProcessBus();
    ReportMsg reportMsg = new ReportMsg();
    BaseClass BaseMsg = new BaseClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ReportViewer.Visible = false;
            reportMsg.EmployeeCode = BaseMsg.EmployeeCode;
            LoadReport(reportMsg);
        }
    }

 
    public void LoadReport(ReportMsg reportMsg)
    {
        string ReportTitle = "Pending Activity Status Report: ";
        ReportTitle = ReportTitle + "--" + System.DateTime.Today.ToString("dd-MM-yyyy");
        DataTable dt = new DataTable();
        //dt = Bus.rptPlantwiseActivityMIS(reportMsg);
        try
        {
            dt = Bus.DashBoardActivityActionSelect(reportMsg);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Microsoft.Reporting.WebForms.ReportDataSource rptDatasource = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt);
                ReportViewer.LocalReport.DataSources.Clear();
                ReportViewer.LocalReport.DataSources.Add(rptDatasource);
                LocalReport rep = ReportViewer.LocalReport;
                string reppath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["RptPath"]);

                rep.ReportPath = reppath + "DashBoardPendingActivityAction.rdlc";

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
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Data notavbl. Pls Proceed Using ECompliance" + "');", true);
                ReportViewer.Visible = false;
            }
            pnlMsg.Visible = false;
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "May be timed out . Check Error Message.."+ex.ToString() + "');", true);
            pnlMsg.Visible = true;
            ReportViewer.Visible = false;
            lblMsg.Text = "IF ERROR, REPORT TO IT SUPPORT AND PROCEED WITH ECOMPLIANCE. ........................." + ex.ToString().Substring(0,ex.ToString().Length/2);
        }
    }
}  