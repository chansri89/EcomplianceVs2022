using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Xml.Linq;
using System.Drawing;
using AjaxControlToolkit;
using System.IO;

public partial class NonComplianceAction : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    List<ActivityForCompanyMasterMsg> ActivityForCmpnyList = new List<ActivityForCompanyMasterMsg>();
    List<ActivityDocumentTypeMasterMsg> ActivityForDocList = new List<ActivityDocumentTypeMasterMsg>();
    List<ActMasterMsg> ActList = new List<ActMasterMsg>();
    List<FrequencyMasterMsg> FncyList = new List<FrequencyMasterMsg>();
    List<DocumentTypeMasterMsg> DocumentList = new List<DocumentTypeMasterMsg>();
    List<ActivityActionMsg> ActionList = new List<ActivityActionMsg>();
    List<DocumentTypeActionMsg> DocmActionList = new List<DocumentTypeActionMsg>();
    List<ActivityActionMsg> ActivityActionList = new List<ActivityActionMsg>();
    ActivityActionMsg ActivtyAction = new ActivityActionMsg();
    List<ActivityDocumentMsg> ActiondocList = new List<ActivityDocumentMsg>();
    List<TranNonComplianceMsg> tranNonComplianceList = new List<TranNonComplianceMsg>();
    public static EmployeeMasterMsg Emp = new EmployeeMasterMsg();
    BaseClass BaseMsg = new BaseClass();
    public static int DeleteIndex = 0;
    public static int UpdateIndex = 0;
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadCompanyName();
            LoadActName();
            lblMsg.Visible = true;
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (IsValidSave() == 0)
        {
            GrdCompActivities.Visible = true;
            LoadGrdActivityforCompanyMaster();
            if (ActivityActionList.Count > 0)
            {
                btnCompActivitiesHide.Visible = true;
            }
            else
            {
                btnCompActivitiesHide.Visible = false;
            }

        }
        lblMsg.Visible = false;
        //btnCompActivitiesHide.Visible = true;
    }
    protected void btnCompActivitiesHide_Click(object sender, EventArgs e)
    {
        if (btnCompActivitiesHide.Text == "- Collapse")
        {
            pnlGridCompActivities.Visible = false;
            btnCompActivitiesHide.Text = "+ Expand";
        }
        else
        {
            //LoadCalendar(1, 200, 200);
            pnlGridCompActivities.Visible = true;
            btnCompActivitiesHide.Text = "- Collapse";

        }
    }
    #endregion
    #region Methods
    public void LoadGrdActivityforCompanyMaster()
    {
        //EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.ActId = Convert.ToInt32(ddlActName.SelectedValue);
        Emp.CompanyCode = ddlCompanyName.SelectedValue;
        Emp.EmployeeCode = BaseMsg.EmployeeCode;
        ActivityActionMsg ActivityActionmsg = new ActivityActionMsg();
        ActivityActionList = Bus.ActivityActionSelect(Emp,ActivityActionmsg);
        ActivityActionList = (from NonCompliance in ActivityActionList
                              where (NonCompliance.NonComplianceTaskAvailableFlag.ToUpper() == "Y" && NonCompliance.ActivityActionId!=0)
                              select NonCompliance).Distinct().ToList();
        LoadGridAction(ActivityActionList);
    }
    public void LoadGridAction(List<ActivityActionMsg> ActivityActionList)
    {
        GrdCompActivities.DataSource = "";
        GrdCompActivities.DataSource = ActivityActionList;
        GrdCompActivities.DataBind();
    }
    private void LoadGrdEDC()
    {
        TranNonComplianceMsg TranNoncomlMsg = new TranNonComplianceMsg();
        TranNoncomlMsg.Flag = "R";
        TranNoncomlMsg.ActivityForCompanyId =Convert.ToInt32(txtActivityForCompanyId.Text);
        tranNonComplianceList = Bus.TranEDCInsertUpdate(TranNoncomlMsg);
        LoadGrid(tranNonComplianceList);
    }

    public void LoadGrid(List<TranNonComplianceMsg> tranNonComplianceList)
    {
        GrdEDC.DataSource = "";
        GrdEDC.DataSource = tranNonComplianceList;
        GrdEDC.DataBind();
    }


    private void LoadCompanyName()
    {
        //EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.EmployeeCode = BaseMsg.EmployeeCode;
        List<CompanyMessage> CompanyList = Bus.CompanyMasterSelect(Emp);
        CompanyList = (from ActiveCompany in CompanyList
                       where ActiveCompany.IsActive == true
                       select ActiveCompany).ToList();
        ddlCompanyName.DataSource = CompanyList;
        ddlCompanyName.DataTextField = "CompanyName";
        ddlCompanyName.DataValueField = "CompanyCode";
        ddlCompanyName.DataBind();
        ddlCompanyName.Items.Insert(0,new ListItem("-- All --","0"));
    }
    private void LoadActName()
    {
        // EmployeeMasterMsg emp = new EmployeeMasterMsg();
        ActMasterMsg Act = new ActMasterMsg();
        Act.Flag = "R";
        ActList = Bus.MasActsInsertUpdateandDelete(Act);
        var ddlActList = (from ActName in ActList
                          where ActName.IsActive == true
                          select new { ActName.ActName, ActName.ActId }).Distinct().ToList();
        ddlActName.DataSource = ddlActList;
        ddlActName.DataTextField = "ActName";
        ddlActName.DataValueField = "ActId";
        ddlActName.DataBind();
        ddlActName.Items.Insert(0, new ListItem("-- All --", "0"));
    }
    public DataTable LoadMonth()
    {
        DataTable dtMonth = new DataTable();
        dtMonth.Columns.Add("MonthName", typeof(string));
        dtMonth.Columns.Add("Month", typeof(int));
        DataRow dr1 = dtMonth.NewRow();
        dr1["MonthName"] = "Jan";
        dr1["Month"] = 1;
        dtMonth.Rows.Add(dr1);
        DataRow dr2 = dtMonth.NewRow();
        dr2["MonthName"] = "Feb";
        dr2["Month"] = 2;
        dtMonth.Rows.Add(dr2);
        DataRow dr3 = dtMonth.NewRow();
        dr3["MonthName"] = "Mar";
        dr3["Month"] = 3;
        dtMonth.Rows.Add(dr3);
        DataRow dr4 = dtMonth.NewRow();
        dr4["MonthName"] = "Apr";
        dr4["Month"] = 4;
        dtMonth.Rows.Add(dr4);
        DataRow dr5 = dtMonth.NewRow();
        dr5["MonthName"] = "May";
        dr5["Month"] = 5;
        dtMonth.Rows.Add(dr5);
        DataRow dr6 = dtMonth.NewRow();
        dr6["MonthName"] = "Jun";
        dr6["Month"] = 6;
        dtMonth.Rows.Add(dr6);
        DataRow dr7 = dtMonth.NewRow();
        dr7["MonthName"] = "Jul";
        dr7["Month"] = 7;
        dtMonth.Rows.Add(dr7);
        DataRow dr8 = dtMonth.NewRow();
        dr8["MonthName"] = "Aug";
        dr8["Month"] = 8;
        dtMonth.Rows.Add(dr8);
        DataRow dr9 = dtMonth.NewRow();
        dr9["MonthName"] = "Sep";
        dr9["Month"] = 9;
        dtMonth.Rows.Add(dr9);
        DataRow dr10 = dtMonth.NewRow();
        dr10["MonthName"] = "Oct";
        dr10["Month"] = 10;
        dtMonth.Rows.Add(dr10);
        DataRow dr11 = dtMonth.NewRow();
        dr11["MonthName"] = "Nov";
        dr11["Month"] = 11;
        dtMonth.Rows.Add(dr11);
        DataRow dr12 = dtMonth.NewRow();
        dr12["MonthName"] = "Dec";
        dr12["Month"] = 12;
        dtMonth.Rows.Add(dr12);
        return dtMonth;
    }
    public DataTable LoadDays()
    {
        DataTable dtDays = new DataTable();
        dtDays.Columns.Add("Day", typeof(string));
        dtDays.Columns.Add("Number", typeof(int));
        DataRow dr1 = dtDays.NewRow();
        dr1["Day"] = "Monday";
        dr1["Number"] = 1;
        dtDays.Rows.Add(dr1);
        DataRow dr2 = dtDays.NewRow();
        dr2["Day"] = "Tuesday";
        dr2["Number"] = 2;
        dtDays.Rows.Add(dr2);
        DataRow dr3 = dtDays.NewRow();
        dr3["Day"] = "Wednesday";
        dr3["Number"] = 3;
        dtDays.Rows.Add(dr3);
        DataRow dr4 = dtDays.NewRow();
        dr4["Day"] = "Thursday";
        dr4["Number"] = 4;
        dtDays.Rows.Add(dr4);
        DataRow dr5 = dtDays.NewRow();
        dr5["Day"] = "Friday";
        dr5["Number"] = 5;
        dtDays.Rows.Add(dr5);
        DataRow dr6 = dtDays.NewRow();
        dr6["Day"] = "Saturday";
        dr6["Number"] = 6;
        dtDays.Rows.Add(dr6);
        DataRow dr7 = dtDays.NewRow();
        dr7["Day"] = "Sunday";
        dr7["Number"] = 7;
        dtDays.Rows.Add(dr7);

        return dtDays;
    }
    public DataTable LoadReminYear()
    {

        int PYear, CYear, NYear;
        PYear = Convert.ToInt32(System.DateTime.Now.ToString("yyyy")) - 1;
        CYear = Convert.ToInt32(System.DateTime.Now.ToString("yyyy"));
        NYear = Convert.ToInt32(System.DateTime.Now.ToString("yyyy")) + 1;
        DataTable dtYear = new DataTable();

        dtYear.Columns.Add("YearName", typeof(string));
        dtYear.Columns.Add("DueYear", typeof(int));
        DataRow dr1 = dtYear.NewRow();
        dr1["YearName"] = PYear;
        dr1["DueYear"] = PYear;
        dtYear.Rows.Add(dr1);
        DataRow dr2 = dtYear.NewRow();
        dr2["YearName"] = CYear;
        dr2["DueYear"] = CYear;
        dtYear.Rows.Add(dr2);
        DataRow dr3 = dtYear.NewRow();
        dr3["YearName"] = NYear;
        dr3["DueYear"] = NYear;
        dtYear.Rows.Add(dr3);
        return dtYear;
    }
    #region ddlLoad
    public List<EmployeeMasterMsg> getExecutionPerson()
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        EmpMsg.CompanyCode = ddlCompanyName.SelectedItem.Value;
        EmpList = Bus.ExecutionerSelect(EmpMsg);
        if (EmpList.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.ErrEmpnotAssignedforcmp + "');", true);
        }
        return EmpList;

    }
    public List<EmployeeMasterMsg> getReviewPerson()
    {
        return EmpList;
    }
    public List<EmployeeMasterMsg> getHeadPerson()
    {
        return EmpList;
    }
    public List<FrequencyMasterMsg> getFrequencey()
    {
        FncyList = Bus.FrequencyMasterSelect(Emp);
        return FncyList;
    }
    #endregion
    #endregion
    #region Validation
    private int IsValidSave()
    {
        int Error = 0;
        string DisplayError = "";


        if (ddlActName.SelectedIndex == 0)//|| ddlActName.SelectedIndex > 0
        {
            DisplayError = DisplayError + StackResource.ErrActionActSelect;
            Error = 1;
        }

        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
   
    #endregion
    protected void GrdCompActivities_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells[4].Text == "")//(lblActivityActionId)Cells[4]="" denots No Action Enter for the Activity
        {
            if (((Label)e.Row.FindControl("lblActivityActionId")).Text == "0")
            {
                e.Row.ForeColor = Color.Blue;
                e.Row.Font.Bold = true;

            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
       
        if (IsNonComplianceSave() == 0)
        {
            NonComplianceSave();
        }

    }
    public void EDCUpdate()
    {
        GridViewRow row = GrdEDC.Rows[UpdateIndex];
        TranNonComplianceMsg TranNoncomlMsg = new TranNonComplianceMsg();
       
        //System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        //dateinfo.ShortDatePattern = "dd/MM/yyyy";
        Label ActivityNonComplianceTaskId = (Label)row.FindControl("lblActivityNonComplianceTaskId");
        Label ActivityNonComplianceTaskMasterId = (Label)row.FindControl("lblActivityNonComplianceTaskMasterId");
        //Label lblActivityActionId = Convert.ToInt32(txtActivityActionId.Text.Trim());
       
        TextBox ComplianceTaskName = (TextBox)row.FindControl("txtComplianceTaskName");
        TextBox EDC = (TextBox)row.FindControl("txtEDC");
        TextBox CompletedDate = (TextBox)row.FindControl("txtCompletedDate");

        if (ActivityNonComplianceTaskId.Text == "0")
        {
            TranNoncomlMsg.Flag = "I";
            TranNoncomlMsg.ActivityNonComplianceTaskId = 0;
        }
        else
        {
            TranNoncomlMsg.Flag ="U";
            TranNoncomlMsg.ActivityNonComplianceTaskId = Convert.ToInt32(ActivityNonComplianceTaskId.Text);
        }
        TranNoncomlMsg.ActivityNonComplianceTaskMasterId = Convert.ToInt32(ActivityNonComplianceTaskMasterId.Text);
        TranNoncomlMsg.ActivityActionId = Convert.ToInt32(txtActivityActionId.Text.Trim());
        TranNoncomlMsg.ComplianceTaskName = ComplianceTaskName.Text;
        TranNoncomlMsg.ExpectedDateOfCompletion = EDC.Text;
        TranNoncomlMsg.CompletedDate =CompletedDate.Text;
        TranNoncomlMsg.CreatedBy = BaseMsg.EmployeeCode;
        TranNoncomlMsg.ActivityForCompanyId = Convert.ToInt32(txtActivityForCompanyId.Text);
       tranNonComplianceList = Bus.TranEDCInsertUpdate(TranNoncomlMsg);
        GrdEDC.EditIndex = -1;

        foreach (TranNonComplianceMsg TranComplUpdate in tranNonComplianceList)
        {
            if (TranComplUpdate.NonComplianceResult == "0")
            {
               // ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.SuccessFullySaved + "');", true);
                LoadGrid(tranNonComplianceList);
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + TranComplUpdate.NonComplianceResult + "');", true);
                break;
            }
        }
    }
    public void NonComplianceSave()
    {
        TranNonComplianceMsg tranNonCompliance = new TranNonComplianceMsg();
        tranNonCompliance.Flag = "I";
        tranNonCompliance.ActivityActionRootCauseId = 0;
        tranNonCompliance.ActivityActionId = Convert.ToInt32(txtActivityActionId.Text);
        tranNonCompliance.CorrectiveAction = txtCorrectiveAction.Text;
        tranNonCompliance.PreventiveAction = txtPreventiveAction.Text;
        tranNonCompliance.RootCause = txtRootCause.Text;
        tranNonCompliance.ApprovedBy = txtApprovedBy.Text;
        tranNonCompliance.CreatedBy = BaseMsg.EmployeeCode;
        TranNonComplianceMsg NonComplianceSave = Bus.MasActivityactionRootCauseInsertUpdateandSelect(tranNonCompliance);
        if (NonComplianceSave.NonComplianceResult == "0")
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.SuccessFullySaved + "');", true);
            ClearAll();
            txtApprovedBy.Text = NonComplianceSave.ApprovedBy;
            txtCorrectiveAction.Text = NonComplianceSave.CorrectiveAction;
            txtPreventiveAction.Text = NonComplianceSave.PreventiveAction;
            txtRootCause.Text = NonComplianceSave.RootCause;
            btnSave.Text = "Update";
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + NonComplianceSave.NonComplianceResult + "');", true);
        }
    }
    public void ClearAll()
    {
        txtCorrectiveAction.Text = "";
        txtPreventiveAction.Text = "";
        txtRootCause.Text = "";
        txtApprovedBy.Text = "";
        btnSave.Text = "Save";
                //LoadDocumentType();
    }
    protected void GrdCompActivities_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        TranNonComplianceMsg tranComplianceMsg = new TranNonComplianceMsg();
        int WRowIndex = Convert.ToInt32(e.CommandArgument.ToString());
        //since is a TemplateField we have to use "((Label)row.FindControl("lblActDtlId")).Text"
        ClearAll();
        if (e.CommandName == "Select")
        {
            GridViewRow row = GrdCompActivities.Rows[WRowIndex];
            txtActivityActionId.Text = ((Label)row.FindControl("lblActivityActionId")).Text;
            txtActivityForCompanyId.Text = ((Label)row.FindControl("lblActivityForCompanyId")).Text;
            TranNonComplianceMsg tran = new TranNonComplianceMsg();
            tran.Flag = "R";
            tran.ActivityActionId = Convert.ToInt32(txtActivityActionId.Text.Trim());
            tranComplianceMsg = Bus.MasActivityactionRootCauseInsertUpdateandSelect(tran);
            if (tranComplianceMsg != null && tranComplianceMsg.ActivityActionRootCauseId>0)
            {
                txtApprovedBy.Text = tranComplianceMsg.ApprovedBy;
                txtCorrectiveAction.Text = tranComplianceMsg.CorrectiveAction;
                txtPreventiveAction.Text = tranComplianceMsg.PreventiveAction;
                txtRootCause.Text = tranComplianceMsg.RootCause;
                btnSave.Text = "Update";
            }
            pnlRoocause.Visible = true;
            pnlSave.Visible = true;
            pnlNonCompiance.Visible = true;
            LoadGrdEDC();
           
        }
    }

    #region Vaidation
    private int IsNonComplianceSave()
    {
        int Error = 0;
        string DisplayError = "";
        if (txtCorrectiveAction.Text.Trim() == "" || txtCorrectiveAction.Text.Length.ToString().Trim() == "0")
        {
            DisplayError = DisplayError + StackResource.ErrCorrectiveAction;
            Error = 1;
        }
        if (txtPreventiveAction.Text.Trim() == "" || txtPreventiveAction.Text.Length.ToString().Trim() == "0")
        {
            DisplayError = DisplayError + "--" + StackResource.ErrPreventiveAction;
            Error = 1;
        }
        if (txtRootCause.Text.Trim() == "" || txtRootCause.Text.Length.ToString().Trim() == "0")
        {
            DisplayError = DisplayError + "--" + StackResource.ErrRootCause;
            Error = 1;
        }
        if (txtApprovedBy.Text.Trim() == "" || txtApprovedBy.Text.Length.ToString().Trim() == "0")
        {
            DisplayError = DisplayError + "--" + StackResource.ErrApprovedBy;
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
      private int IsValidGrid(string EDC,string CompletedDate)//string txtDocName,
    {

        int Error = 0;
        string DisplayError = "";
        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd/MM/yyyy";
        //if (txtDocName.Trim() == "" || Convert.ToInt32(txtDocName.Trim().Length.ToString()) == 0)
        //{
        //    DisplayError = DisplayError + StackResource.ErrFrequenceName;
        //    Error = 1;
        //}
        if (EDC.Trim() == "" || Convert.ToInt32(EDC.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrEDC;
            Error = 1;
        }
       
        if (CompletedDate.Trim() == "" || CompletedDate.Length.ToString().Trim() == "0")
        {
            DisplayError = DisplayError + StackResource.ErrCalander;
            Error = 1;
        }

        else if (Convert.ToDateTime(CompletedDate, dateinfo) > System.DateTime.Now.Date)
        {
            DisplayError = DisplayError + StackResource.ErrClanderSelect;
            Error = 1;
        }


        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }

        return Error;
    }

     
    #endregion
   
    protected void GrdDocument_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdEDC.EditIndex = e.NewEditIndex;
        LoadGrdEDC();
        GridViewRow row = GrdEDC.Rows[GrdEDC.EditIndex];
        TextBox EDC = (TextBox)row.FindControl("txtEDC");
        EDC.Focus();
    }
    protected void GrdEDC_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdEDC.EditIndex = -1;
        LoadGrdEDC();
    }
    protected void GrdEDC_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DeleteIndex = e.RowIndex;
        String jsScript = "";
        //Asking a alert Message to Delete or not
        jsScript += "var answer=confirm(\'" + "Are you sure, You want to Delete?" + "\');\n";
        jsScript += "if (answer){\n";
        //If answer is OK then Updating the HiddenField(control Available) HidDeleteCount as '1' and calling the btnSave_Click to call the Method CompanyDelete
        jsScript += "document.getElementById(\"ContentPlaceHolder1_HidDeleteCount\").value='" + "1" + "';\n";
        jsScript += "document.getElementById(\"ContentPlaceHolder1_btnSave\").click();\n";
        jsScript += "}\n";
        jsScript += "else{\n";
        //If answer is CANCEL then Updating the HiddenField(control Available) HidDeleteCount as '0' 
        jsScript += "document.getElementById(\"ContentPlaceHolder1_HidDeleteCount\").value='" + "0" + "';\n";
        jsScript += "}\n";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", jsScript, true);
    }
    protected void GrdEDC_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        GridViewRow row = GrdEDC.Rows[UpdateIndex];
        //TextBox txtDocName = (TextBox)row.FindControl("txtFreqName");
        string EDC = ((TextBox)row.FindControl("txtEDC")).Text;
        string CompletedDate = ((TextBox)row.FindControl("txtCompletedDate")).Text;
        if (IsValidGrid(EDC, CompletedDate) == 0)//txtDocName.Text.Trim(),
        {
            EDCUpdate();
        }

    }
    protected void GrdCompActivities_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}