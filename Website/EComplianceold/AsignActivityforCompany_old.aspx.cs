using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Windows.forms;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using Resources;
using System.Drawing;
using Ganini.Lib;

public partial class AsignActivityforCompany : System.Web.UI.Page
{

    #region Declaration
    ProcessBus Bus = new ProcessBus();
    List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    List<ActivityForCompanyMasterMsg> ActivityForCmpnyList = new List<ActivityForCompanyMasterMsg>();
    List<ActMasterMsg> ActList = new List<ActMasterMsg>();
    List<FrequencyMasterMsg> FncyList = new List<FrequencyMasterMsg>();
    List<ActivityDocumentTypeMasterMsg> ActivityForDocList = new List<ActivityDocumentTypeMasterMsg>();
    List<NonComplianceTaskMasterMsg> NonCompList = new List<NonComplianceTaskMasterMsg>();
    UserAccess user = new UserAccess();
    public static string ProgramName = string.Empty;
    BaseClass BaseMsg = new BaseClass();
    public static int DeleteIndex = 0;
    public static int UpdateIndex = 0;
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
       
         if (!Page.IsPostBack)
       {
           LoadCompanyName();
           LoadActName();
           GrdCompActivities.Visible = false;
           //lblMsg.Visible = true;
           
       }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        
        if (HidUpdateCount.Value == "1")
        {
            CompanyActivityUpdate();
            HidUpdateCount.Value = "0";
            return;
        }
        if (HidDocUpdateCount.Value == "1")
        {
            DocumentUpdate();
            HidDocUpdateCount.Value = "0";
            return;
        }

        if (HidTaskUpdateCount.Value == "1")
        {
            TaskUpdate();
            HidTaskUpdateCount.Value = "0";
            return;
        }
        //if (IsValidSave() == 0)
        //{
           GrdCompActivities.Visible = true;
            LoadGrdActivityforCompanyMaster();
            //if (ActivityForCmpnyList.Count > 0)
            //{
            //    //btnCompActivitiesHide.Visible = true;
            //}
            //else
            //{
            //    //btnCompActivitiesHide.Visible = false;
            //}
        //}

        ///For Updating a Record from Grid-Starts
        //btnCompActivitiesHide.Visible = true;
        //lblActivityGrdColorMsg.Visible = true; //commented by Abinayaa.Bcoz the Msg not shown -040213
        GrdCompActivities.Enabled = true;
        txtTitle.Visible = false;
        btnCollapse.Visible = false;
        pnDocumentGrd.Visible = false;
        pnlCompanyAct.Visible = false;
        lblNonComplianceTask.Visible = false;
        PnlTask.Visible = false;
        GrdCompActivities.Visible = true;
        pnlGridCompActivities.Visible = true;
        pnlGrdCompActivities.Visible = true;
        //lblMsg.Visible = false;
    }
    //protected void btnCompActivitiesHide_Click(object sender, EventArgs e)
    //{
    //    if (btnCompActivitiesHide.Text == "- Collapse")
    //    {
    //        pnlGrdCompActivities.Visible = false;
    //        btnCompActivitiesHide.Text = "+ Expand";
                    
    //    }
    //    else
    //    {
    //        pnlGrdCompActivities.Visible = true;
    //        btnCompActivitiesHide.Text = "- Collapse";
           
    //    }
    //}
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        
        if (TaskIsValidSave() == 0)
        {

            TaskSave();
        }
        LoadGrdTaskMaster();
      
    }
    protected void btnCollapse_Click(object sender, EventArgs e)
    {
        if (btnCollapse.Text == "-")
        {
            btnCollapse.Text = "-";
            pnlDocumentType.Visible = false;
            txtTitle.Visible = false;
            btnCollapse.Visible = false;
            GrdCompActivities.Enabled = true;
            LoadGrdActivityforCompanyMaster();
            GrdCompActivities.Visible = true;
            pnlGridCompActivities.Visible = true;
            pnlGrdCompActivities.Visible = true;
            //btnCollapse.Enabled = true;
            // btnCollapse.ToolTip = "Click this button to Edit or Select Activity for Company";
        }
        else
        {
            btnCollapse.Text = "-";
            pnlDocumentType.Visible = true;
            btnCollapse.Enabled = true;
            //btnCollapse.Enabled = true;
            GrdCompActivities.Visible = false;
            pnlGridCompActivities.Visible = false;
            pnlGrdCompActivities.Visible = false;

        }

    }
    #endregion
    #region CompActivityGrid
    protected void GrdCompActivities_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (ddlCompanyName.SelectedIndex == 0 || ddlActName.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ErrAssignCompanyAllEdit + "');", true);
            return;
        }
        GrdCompActivities.EditIndex = e.NewEditIndex;
        GridViewRow row = GrdCompActivities.Rows[GrdCompActivities.EditIndex];
        Label lbldueMonth = (Label)row.FindControl("lblDueMonth");
        Label lblDueDate = (Label)row.FindControl("lblDueDate");
        Label lblDueDay = (Label)row.FindControl("lblDueDay");
        Label lblTrigMonth = (Label)row.FindControl("lblTrigMonth");
        Label lblTrigDay = (Label)row.FindControl("lblTrigDay");
        Label lblExecutionPerson = (Label)row.FindControl("lblExecutionPerson");
        Label lblReviewPerson = (Label)row.FindControl("lblReviewPerson");
        Label lblHeadPerson = (Label)row.FindControl("lblHeadPerson");
        Label lblFrequencey = (Label)row.FindControl("lblFrequencey");
        Label lblTrigDate = (Label)row.FindControl("lblTrigDate");

        LoadGrdActivityforCompanyMaster();
        foreach (GridViewRow gvr in GrdCompActivities.Rows)
        {
            if (gvr.RowIndex == GrdCompActivities.EditIndex)
            {
                DropDownList ddldueMonth = (DropDownList)gvr.FindControl("ddldueMonth");
                DataTable dtMonth = LoadMonth();
                foreach (DataRow dr in dtMonth.Rows)
                {
                    if (dr["Month"].ToString().Trim() == lbldueMonth.Text.Trim())
                    {
                        ddldueMonth.SelectedValue = dr["Month"].ToString();
                        ddldueMonth.Focus();
                        break;
                    }
                }
                DropDownList ddlDueDate = (DropDownList)gvr.FindControl("ddlDueDate");
                //ddlTrigDate.SelectedItem.Text = lblTrigDate.Text;
                ddlDueDate.SelectedValue = lblDueDate.Text.Trim();
               // DropDownList ddlDueDate = (DropDownList)gvr.FindControl("ddlDueDate");
               // //ddlDueDate.SelectedItem.Text = lblDueDate.Text;
                //DataTable dtDate = LoadDate();
                //foreach (DataRow dr in dtDate.Rows)
                //{
                //    if (dr["Date"].ToString() == lblDueDate.Text)
                //    {
                //        ddlDueDate.SelectedValue = dr["Date"].ToString();
                //        break;
                //    }
                //}
                //ddlDueDate.SelectedValue = lblDueDate.Text;

                DropDownList ddlDueDay = (DropDownList)gvr.FindControl("ddlDueDay");
                DataTable dtDays = LoadDays();
                foreach (DataRow dr in dtDays.Rows)
                {
                    if (dr["Day"].ToString().Trim() == lblDueDay.Text.Trim())
                    {
                        ddlDueDay.SelectedValue = dr["Number"].ToString();
                        break;
                    }
                }
                DropDownList ddlTrigMonth = (DropDownList)gvr.FindControl("ddlTrigMonth");
                foreach (DataRow dr in dtMonth.Rows)
                {
                    if (dr["Month"].ToString().Trim() == lblTrigMonth.Text.Trim())
                    {
                        ddlTrigMonth.SelectedValue = dr["Month"].ToString();
                        break;
                    }
                }
                DropDownList ddlTrigDate = (DropDownList)gvr.FindControl("ddlTrigDate");
                //ddlTrigDate.SelectedItem.Text = lblTrigDate.Text;
                ddlTrigDate.SelectedValue = lblTrigDate.Text;
                DropDownList ddlTrigDay = (DropDownList)gvr.FindControl("ddlTrigDay");
                foreach (DataRow dr in dtDays.Rows)
                {
                    if (dr["Day"].ToString().Trim() == lblTrigDay.Text.Trim())
                    {
                        ddlTrigDay.SelectedValue = dr["Number"].ToString();
                        break;
                    }
                }
                DropDownList ddlExecutionPerson = (DropDownList)gvr.FindControl("ddlExecutionPerson");
                DropDownList ddlReviewPerson = (DropDownList)gvr.FindControl("ddlReviewPerson");
                DropDownList ddlHeadPerson = (DropDownList)gvr.FindControl("ddlHeadPerson");
                foreach (EmployeeMasterMsg EmpEdit in EmpList)
                {
                    if (EmpEdit.EmployeeName.Trim() == lblExecutionPerson.Text.Trim())
                    {
                        ddlExecutionPerson.SelectedValue = EmpEdit.EmployeeCode.ToString();
                    }
                    if (EmpEdit.EmployeeName.Trim() == lblReviewPerson.Text.Trim())
                    {
                        ddlReviewPerson.SelectedValue = EmpEdit.EmployeeCode.ToString();
                    }
                    if (EmpEdit.EmployeeName.Trim() == lblHeadPerson.Text.Trim())
                    {
                        ddlHeadPerson.SelectedValue = EmpEdit.EmployeeCode.ToString();
                    }
                }
                DropDownList ddlFrequencey = (DropDownList)gvr.FindControl("ddlFrequencey");
                foreach (FrequencyMasterMsg frequency in FncyList)
                {
                    if (frequency.FrequencyName.Trim() == lblFrequencey.Text.Trim())
                    {
                        ddlFrequencey.SelectedValue = frequency.FrequencyId.ToString();
                        break;
                    }
                }
                break;
            }
        }
    }
    protected void GrdCompActivities_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdCompActivities.EditIndex = -1;
        LoadGrdActivityforCompanyMaster();
        
    }
    protected void GrdCompActivities_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        if (IsValidValues(UpdateIndex) == 0)
        {
            CompanyActivityUpdate();
        }
    }
   
    protected void GrdCompActivities_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int WRowIndex = Convert.ToInt32(e.CommandArgument.ToString());
        //since is a TemplateField we have to use "((Label)row.FindControl("lblActDtlId")).Text"
        if (e.CommandName == "Select")
        {
            //if (ddlCompanyName.SelectedIndex == 0 || ddlActName.SelectedIndex == 0)//comments by Abinayaa 230113.Select all from ddl company and Act name don't Show the Msg,allow to enter Document Grid.
                    //{
                    //    //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ErrAssignAllEdit + "');", true);
                    //    ////pnlGridSelect.Visible = false;
                    //    ////GrdCompActivities.Enabled = true;
                    //    //return;
                    //}//comments by Abinayaa 230113.Select all from ddl company and Act name don't Show the Msg,allow to enter Document Grid.

            GridViewRow row = GrdCompActivities.Rows[WRowIndex];
            txtCompanyActivityId.Text = ((Label)row.FindControl("lblCompanyActivityId")).Text;
            txtCompliance.Text = ((Label)row.FindControl("lblNonComplianceTaskAvailableFlag")).Text;
            if (txtCompliance.Text.ToLower() == "n")
            {
                lblNonComplianceTask.Visible = true;
                PnlTask.Visible = false;
            }
            else
            {
                lblNonComplianceTask.Visible = false;
                PnlTask.Visible = true;
            }
            if (txtCompanyActivityId.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ErrGridSelect + "');", true);
                pnlGridSelect.Visible = false;
                GrdCompActivities.Enabled = true;
                txtTitle.Visible = false;
                btnCollapse.Visible = false;
                pnDocumentGrd.Visible = false;
                pnlCompanyAct.Visible = false;
                lblNonComplianceTask.Visible = true;
                PnlTask.Visible = false;
                //GrdCompActivities.Enabled = true;
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ActivityAssigned + "');", true);
                pnlGridSelect.Visible = true;
                pnlDocumentType.Visible = true;
                txtTitle.Visible = true;
                btnCollapse.Visible = true;
                pnDocumentGrd.Visible = true;
                pnlCompanyAct.Visible = true;
                //PnlTask.Visible = true;
                //lblNonComplianceTask.Visible = false;
                //lblNonComplianceTask.Visible = true;
                GrdCompActivities.Enabled = false;
                // pnlCompanyAct.Visible = true;
            }


            txtCmpName.Text = ddlCompanyName.SelectedItem.Text;
            txtAct.Text = ddlActName.SelectedItem.Text;
            txtActivities.Text = ((Label)row.FindControl("lblActivityName")).Text;
            LoadGrdDocumentMaster();
            pnlCompanyAct.Enabled = false;
            LoadGrdTaskMaster();
            pnlGridSelect.Visible = true;
            //pnlDocumentType.Visible = true;
            //txtTitle.Visible = true;
            //btnCollapse.Visible = true;
            //LoadGrdActivityforCompanyMaster();
            GrdCompActivities.Visible = false;
            pnlGridCompActivities.Visible = false;
            pnlGrdCompActivities.Visible = false;
        }
    }
    protected void GrdCompActivities_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.Cells[2].Text == "")
        //if(e.Row.RowType==GridView.
        {
            if (((Label)e.Row.FindControl("lblCompanyActivityId")).Text == "0")
            {
                e.Row.ForeColor = Color.Blue;
                e.Row.Font.Bold = true;
            }
        }
    }
    #endregion
    #region EdteDocumentgrid
    protected void GrdDocument_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdDocument.EditIndex = e.NewEditIndex;
        LoadGrdDocumentMaster();
        GridViewRow row = GrdDocument.Rows[GrdDocument.EditIndex];
        TextBox ToBeMaintained = (TextBox)row.FindControl("txtToBeMaintained");
        ToBeMaintained.Focus();
    }
    protected void GrdDocument_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdDocument.EditIndex = -1;
        LoadGrdDocumentMaster();
    }
    protected void GrdDocument_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        DocumentUpdate();
    }
   
    #endregion
    #region GrdTask
    protected void GrdTaskName_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdTaskName.EditIndex = e.NewEditIndex;
        LoadGrdTaskMaster();
        GridViewRow row = GrdTaskName.Rows[GrdTaskName.EditIndex];
        TextBox ComplianceTaskName = (TextBox)row.FindControl("txtComplianceTaskName");
        ComplianceTaskName.Focus();
    }
    protected void GrdTaskName_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdTaskName.EditIndex = -1;
        LoadGrdTaskMaster();
    }
    protected void GrdTaskName_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        GridViewRow row = GrdTaskName.Rows[UpdateIndex];
        TextBox txtComplianceTaskName = (TextBox)row.FindControl("txtComplianceTaskName");
        if (TaskIsValidGridSave(txtComplianceTaskName.Text.Trim()) == 0)
        {
            TaskUpdate();
        }
    }
    #endregion    
    #region Methods
    public void LoadGrdActivityforCompanyMaster()
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.ActId = Convert.ToInt32(ddlActName.SelectedItem.Value);
        Emp.CompanyCode = ddlCompanyName.SelectedItem.Value;
        Emp.EmployeeCode = BaseMsg.EmployeeCode;
        ActivityForCompanyMasterMsg ActivityCompany = new ActivityForCompanyMasterMsg();
        ActivityForCmpnyList = Bus.ActivityForCompanyMasterSelect(Emp);
        if (ActivityForCmpnyList.Count == 0)
        {
            GrdCompActivities.DataSource = "";
            GrdCompActivities.DataBind();
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert(' " + EComplianceResource.ErrNoDataforAct + ");", true);
              ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ErrNoDataforAct + "');", true);//modified by abinayaa 240413
        }
        else
        {
            LoadGrid(ActivityForCmpnyList);
            if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
            {
                GrdCompActivities.Columns[GrdCompActivities.Columns.Count - 2].Visible = false;
            }
        }
    }
    public void LoadGrid(List<ActivityForCompanyMasterMsg> ActivityForCmpnyList)
    {
        if (ActivityForCmpnyList != null && ActivityForCmpnyList.Count > 0)
        {
            GrdCompActivities.DataSource = "";
            GrdCompActivities.DataSource = ActivityForCmpnyList;
            GrdCompActivities.DataBind();
        }
    }
    private void LoadCompanyName()
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.EmployeeCode = BaseMsg.EmployeeCode;
        List<CompanyMessage> CompanyList = Bus.CompanyMasterSelect(Emp);
        CompanyList = (from ActiveCompany in CompanyList
                       where ActiveCompany.IsActive == true
                       select ActiveCompany).ToList();
        ddlCompanyName.DataSource = CompanyList;
        ddlCompanyName.DataTextField = "CompanyName";
        ddlCompanyName.DataValueField = "CompanyCode";
        ddlCompanyName.DataBind();
        ddlCompanyName.Items.Insert(0, new ListItem("-- All--", "0"));
    }
    private void LoadActName()
    {
       // EmployeeMasterMsg emp = new EmployeeMasterMsg();
        ActMasterMsg Act = new ActMasterMsg();
        Act.Flag = "R";
        ActList = Bus.MasActsInsertUpdateandDelete(Act);
        var ddlActList = (from ActName in ActList
                          where ActName.IsActive==true
                          select new { ActName.ActName, ActName.ActId }).Distinct().ToList();
        ddlActName.DataSource = ddlActList;
        ddlActName.DataTextField = "ActName";
        ddlActName.DataValueField = "ActId";
        ddlActName.DataBind();
        ddlActName.Items.Insert(0, new ListItem("-- All--", "0"));
    }
    public void CompanyActivityUpdate()
    {
        GridViewRow row = GrdCompActivities.Rows[UpdateIndex];
        ActivityForCompanyMasterMsg ActivityCompany = new ActivityForCompanyMasterMsg();
        //ActivityCompany.Flag = "U";
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.ActId = Convert.ToInt32(ddlActName.SelectedItem.Value);
        Emp.CompanyCode = ddlCompanyName.SelectedItem.Value;

        Label lblActivityId = (Label)row.FindControl("lblActivityId");
        Label lblcompanyactivityid = (Label)row.FindControl("lblCompanyActivityId");
        TextBox txtactivityname = (TextBox)row.FindControl("txtActivityName");
        ////DropDownList ddlcompcode = (DropDownList)row.FindControl("ddlCompanyCode");
        DropDownList ddlduemonth = (DropDownList)row.FindControl("ddlduemonth");
        DropDownList ddlduedate = (DropDownList)row.FindControl("ddlDueDate");
        DropDownList ddldueday = (DropDownList)row.FindControl("ddlDueDay");
        DropDownList ddltrigmonth = (DropDownList)row.FindControl("ddlTrigMonth");
        DropDownList ddltrigdate = (DropDownList)row.FindControl("ddlTrigDate");
        DropDownList ddltrigday = (DropDownList)row.FindControl("ddlTrigDay");
        DropDownList ddlexecutionperson = (DropDownList)row.FindControl("ddlExecutionPerson");
        DropDownList ddlreviewperson = (DropDownList)row.FindControl("ddlReviewPerson");
        DropDownList ddlheadperson = (DropDownList)row.FindControl("ddlHeadPerson");
       // DropDownList ddlultimate = (DropDownList)row.FindControl("ddlUtimate");
        DropDownList ddlfrequencey = (DropDownList)row.FindControl("ddlFrequencey");
        TextBox FrqRemarks = (TextBox)row.FindControl("txtFrqRemarks");//added by Abinayaa 161112
        CheckBox chkActive = (CheckBox)row.FindControl("chkIsActive");

        ActivityCompany.ActivityId = Convert.ToInt32(lblActivityId.Text);
        ActivityCompany.CompanyActivityId = Convert.ToInt32(lblcompanyactivityid.Text);
        ActivityCompany.ActivityName = txtactivityname.Text;
        //ActivityCompany.CompanyCode = ddlcompcode.SelectedItem.Value;
        ActivityCompany.CompanyCode = ddlCompanyName.SelectedItem.Value;
        ActivityCompany.DueMonth = Convert.ToInt32(ddlduemonth.SelectedValue);
        string Duedate = ddlduedate.SelectedItem.Text;
        if (ddlduedate.SelectedItem.Text == "NA")
        {
            Duedate = "0";
        }
        //ActivityCompany.DueDate = Convert.ToInt32(ddlduedate.SelectedItem.Text);
        ActivityCompany.DueDate = Convert.ToInt32(Duedate);
        ActivityCompany.DueDay = ddldueday.SelectedItem.Text;
        ActivityCompany.TriggerMonth = Convert.ToInt32(ddltrigmonth.SelectedValue);
        string TriggDate = ddltrigdate.SelectedItem.Text;
        if (ddltrigdate.SelectedItem.Text == "NA")
        {
            TriggDate = "0";
        }
        //ActivityCompany.TriggerDate = Convert.ToInt32(ddltrigdate.SelectedItem.Text);
        ActivityCompany.TriggerDate = Convert.ToInt32(TriggDate);
        ActivityCompany.TriggerDay = ddltrigday.SelectedItem.Text;
        ActivityCompany.ExecutionEmployeeCode = ddlexecutionperson.SelectedItem.Value;
        ActivityCompany.ReviewEmployeeCode = ddlreviewperson.SelectedItem.Value;
        ActivityCompany.HeadEmployeeCode = ddlheadperson.SelectedItem.Value;
        ActivityCompany.UltimateEmployeeCode = "0";
        ActivityCompany.FrequencyId = Convert.ToInt32(ddlfrequencey.SelectedItem.Value);
        ActivityCompany.FrqRemarks = FrqRemarks.Text;//added by Abinayaa 161112
        if (ddlfrequencey.SelectedItem.Text == "As and When")
        {
            if (FrqRemarks.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ErrFrqRemarks + "');", true);
                return;
            }
        }

        ActivityCompany.IsActive = chkActive.Checked;
        ActivityCompany.CreatedBy = BaseMsg.EmployeeCode;
        ActivityForCmpnyList = Bus.MasActivityForCompanyMasterInsertandUpdate(ActivityCompany, Emp);
        GrdCompActivities.EditIndex = -1;

        foreach (ActivityForCompanyMasterMsg ActivityCompanyUpdate in ActivityForCmpnyList)
        {
            if (ActivityCompanyUpdate.ActivityCompanyResult == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ActivityUpdatedSuccessfully + "');", true);
                LoadGrid(ActivityForCmpnyList);
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + ActivityCompanyUpdate.ActivityCompanyResult + "');", true);
                break;
            }
        }
    }
    public void LoadGrdDocumentMaster()
    {
        ActivityForCompanyMasterMsg ActivityComp = new ActivityForCompanyMasterMsg();
        ActivityDocumentTypeMasterMsg ActivityDoc = new ActivityDocumentTypeMasterMsg();
        ActivityComp.CompanyActivityId = Convert.ToInt32(txtCompanyActivityId.Text);
        ActivityForDocList = Bus.ActivityDocumentTypeMasterSelect(ActivityComp);
        LoadGridDocument(ActivityForDocList);
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdDocument.Columns[GrdDocument.Columns.Count - 2].Visible = false;
        }
    }
    #region ddlLoad

    public List<EmployeeMasterMsg> getExecutionPerson()
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        EmpMsg.CompanyCode = ddlCompanyName.SelectedItem.Value;
        EmpList = Bus.ExecutionerSelect(EmpMsg);
        if (EmpList.Count == 0)
        {


            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ErrEmpnotAssignedforcmp + "');", true);
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
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        FncyList = Bus.FrequencyMasterSelect(Emp);
        return FncyList;
    }
    #endregion
    public void LoadGridDocument(List<ActivityDocumentTypeMasterMsg> ActivityForDocList)
    {
        GrdDocument.DataSource = "";
        GrdDocument.DataSource = ActivityForDocList;
        GrdDocument.DataBind();
    }
    public DataTable LoadMonth()
    {
        DataTable dtMonth = new DataTable();
        dtMonth.Columns.Add("MonthName", typeof(string));
        dtMonth.Columns.Add("Month",typeof(int));
        DataRow dr0 = dtMonth.NewRow();
        dr0["MonthName"] = "NA";
        dr0["Month"] = 0;
        dtMonth.Rows.Add(dr0);
        DataRow dr1= dtMonth.NewRow();
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
        DataRow dr0 = dtDays.NewRow();
        dr0["Day"] = "NA";
        dr0["Number"] = 0;
        dtDays.Rows.Add(dr0);
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
    public DataTable LoadDate()
    {
        DataTable dtDate = new DataTable();
        dtDate.Columns.Add("Date", typeof(string));
        //dtDate.Columns.Add("Month", typeof(int));
        DataRow dr0 = dtDate.NewRow();
        dr0["Date"] = "NA";
        //dr0["Month"] = 0;
        dtDate.Rows.Add(dr0);
        DataRow dr1 = dtDate.NewRow();
        dr1["Date"] = "01";
       // dr1["Month"] = 1;
        dtDate.Rows.Add(dr1);
        DataRow dr2 = dtDate.NewRow();
        dr2["Date"] = "02";
        //dr2["Month"] = 2;
        dtDate.Rows.Add(dr2);
        DataRow dr3 = dtDate.NewRow();
        dr3["Date"] = "03";
        //dr3["Month"] = 3;
        dtDate.Rows.Add(dr3);
        DataRow dr4 = dtDate.NewRow();
        dr4["Date"] = "04";
        //dr4["Month"] = 4;
        dtDate.Rows.Add(dr4);
        DataRow dr5 = dtDate.NewRow();
        dr5["Date"] = "05";
        //dr5["Month"] = 5;
        dtDate.Rows.Add(dr5);
        DataRow dr6 = dtDate.NewRow();
        dr6["Date"] = "06";
        //dr6["Month"] = 6;
        dtDate.Rows.Add(dr6);
        DataRow dr7 = dtDate.NewRow();
        dr7["Date"] = "07";
        //dr7["Month"] = 7;
        dtDate.Rows.Add(dr7);
        DataRow dr8 = dtDate.NewRow();
        dr8["Date"] = "08";
        //dr8["Month"] = 8;
        dtDate.Rows.Add(dr8);
        DataRow dr9 = dtDate.NewRow();
        dr9["Date"] = "09";
        //dr9["Month"] = 9;
        dtDate.Rows.Add(dr9);
        DataRow dr10 = dtDate.NewRow();
        dr10["Date"] = "10";
        //dr10["Month"] = 10;
        dtDate.Rows.Add(dr10);
        DataRow dr11 = dtDate.NewRow();
        dr11["Date"] = "11";
        //dr11["Month"] = 11;
        dtDate.Rows.Add(dr11);
        DataRow dr12 = dtDate.NewRow();
        dr12["Date"] = "12";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr12);
        DataRow dr13 = dtDate.NewRow();
        dr13["Date"] = "13";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr13);
      
        DataRow dr14 = dtDate.NewRow();
        dr14["Date"] = "14";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr14);
        DataRow dr15 = dtDate.NewRow();
        dr15["Date"] = "15";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr15);
        DataRow dr16 = dtDate.NewRow();
        dr16["Date"] = "16";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr16);
        DataRow dr17 = dtDate.NewRow();
        dr17["Date"] = "17";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr17);
        DataRow dr18 = dtDate.NewRow();
        dr18["Date"] = "18";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr18);
        DataRow dr19 = dtDate.NewRow();
        dr19["Date"] = "19";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr19);
        DataRow dr20 = dtDate.NewRow();
        dr20["Date"] = "20";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr20);
        DataRow dr21 = dtDate.NewRow();
        dr21["Date"] = "21";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr21);
        DataRow dr22 = dtDate.NewRow();
        dr22["Date"] = "22";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr22);
        DataRow dr23 = dtDate.NewRow();
        dr23["Date"] = "23";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr23);
        DataRow dr24 = dtDate.NewRow();
        dr24["Date"] = "24";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr24);
        DataRow dr25 = dtDate.NewRow();
        dr25["Date"] = "25";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr25);
        DataRow dr26 = dtDate.NewRow();
        dr26["Date"] = "26";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr26);
        DataRow dr27 = dtDate.NewRow();
        dr27["Date"] = "27";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr27);
        DataRow dr28 = dtDate.NewRow();
        dr28["Date"] = "28";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr28);
        DataRow dr29 = dtDate.NewRow();
        dr29["Date"] = "29";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr29);
        DataRow dr30 = dtDate.NewRow();
        dr30["Date"] = "30";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr30);
        DataRow dr31 = dtDate.NewRow();
        dr31["Date"] = "31";
        //dr12["Month"] = 12;
        dtDate.Rows.Add(dr31);
        return dtDate;
    }
    public void LoadGrdTaskMaster()
    {
        ActivityForCompanyMasterMsg ActivityComp = new ActivityForCompanyMasterMsg();
        NonComplianceTaskMasterMsg NonComp = new NonComplianceTaskMasterMsg();
        ActivityComp.CompanyActivityId = Convert.ToInt32(txtCompanyActivityId.Text);
        NonCompList = Bus.NonComplianceTaskMasterSelect(ActivityComp);
        if (NonCompList.Count == 0)
        {
            pnlNonComplianceTaskMsg.Visible = true;
            pnlGrdTask.Visible = false;
        }
        else
        {
            pnlGrdTask.Visible = true;
            pnlNonComplianceTaskMsg.Visible = false;
            LoadTask(NonCompList);
            if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
            {
                GrdDocument.Columns[GrdDocument.Columns.Count - 2].Visible = false;
            }
        }
    }
    public void LoadTask(List<NonComplianceTaskMasterMsg> NonCompList)
    {
        GrdTaskName.DataSource = "";
        GrdTaskName.DataSource = NonCompList;
        GrdTaskName.DataBind();
    }
    private int TaskIsValidSave()
    {
        int Err = 0;
        if (txtTaskName.Text.Trim() == "" || txtTaskName.Text.Trim().Length.ToString() == "0")
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ErrTaskName + "');", true);
            Err = 1;
        }
        return Err;
    }
    private int TaskIsValidGridSave(string TaskName)
    {
        int Err = 0;
        if (TaskName.Trim() == "" || TaskName.Trim().Length.ToString() == "0")
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ErrTaskName + "');", true);
            Err = 1;
        }
        return Err;
    }
    public void TaskUpdate()
    {
        ActivityForCompanyMasterMsg ActivityComp = new ActivityForCompanyMasterMsg();
        GridViewRow row = GrdTaskName.Rows[UpdateIndex];
        NonComplianceTaskMasterMsg NonCompliance = new NonComplianceTaskMasterMsg();
        //Document.Flag = "U";

        TextBox txtActivityForCompanyId = (TextBox)row.FindControl("txtActivityForCompanyId");
        TextBox txtNonComplianceTaskId = (TextBox)row.FindControl("txtNonComplianceTaskId");
        TextBox txtComplianceTaskName = (TextBox)row.FindControl("txtComplianceTaskName");
        CheckBox chkDocIsActive = (CheckBox)row.FindControl("chkTaskIsActive");


        NonCompliance.ActivityForCompanyId = Convert.ToInt32(txtActivityForCompanyId.Text);
        NonCompliance.NonComplianceTaskId = Convert.ToInt32(txtNonComplianceTaskId.Text);
        NonCompliance.ComplianceTaskName = txtComplianceTaskName.Text;
        NonCompliance.IsActive = Convert.ToBoolean(chkDocIsActive.Checked);
        NonCompliance.CreatedBy = "Admin";
        NonCompList = Bus.MasNonComplianceTaskInsertUpdateandDelete(NonCompliance, ActivityComp);
        GrdTaskName.EditIndex = -1;

        foreach (NonComplianceTaskMasterMsg NoncompUpdate in NonCompList)
        {
            if (NoncompUpdate.NonCompResult == "0")
            {
                LoadTask(NonCompList);
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.Update + "');", true);

                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + NoncompUpdate.NonCompResult + "');", true);
                break;
            }
        }
    }
    public void DocumentUpdate()
    {
        ActivityForCompanyMasterMsg ActivityComp = new ActivityForCompanyMasterMsg();
        GridViewRow row = GrdDocument.Rows[UpdateIndex];
        ActivityDocumentTypeMasterMsg ActivityDocType = new ActivityDocumentTypeMasterMsg();
        //Document.Flag = "U";
        TextBox txtActivityDocTypeId = (TextBox)row.FindControl("txtActivityDocumentTypeId");
        //TextBox txtcompanyactivityid = (TextBox)row.FindControl("txtCompanyActivityId");

        TextBox txtDocTypeId = (TextBox)row.FindControl("txtDocumentTypeId");
        TextBox txtDocTypeName = (TextBox)row.FindControl("txtDocumentTypeName");
        //TextBox txtDocShortName = (TextBox)row.FindControl("txtDocumentShortName");
        //TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
        TextBox txtToBeMaintained = (TextBox)row.FindControl("txtToBeMaintained");
        TextBox txtToBeSubmitted = (TextBox)row.FindControl("txtToBeSubmitted");
        TextBox txtMasterPDFName = (TextBox)row.FindControl("txtMasterPDFName");
        TextBox txtAccessPath = (TextBox)row.FindControl("txtAccessPath");
        CheckBox chkDocIsActive = (CheckBox)row.FindControl("chkDocIsActive");

        ActivityDocType.ActDocTypeId = Convert.ToInt64(txtActivityDocTypeId.Text);
        ActivityDocType.ActivityForCompanyId = Convert.ToInt32(txtCompanyActivityId.Text);
        ActivityDocType.DocumentTypeId = Convert.ToInt32(txtDocTypeId.Text);
        ActivityDocType.DocumentTypeName = txtDocTypeName.Text;
        // Document.DocumentTypeShortName =txtDocShortName.Text;
        //Document.Remarks = txtRemarks.Text;
        ActivityDocType.ToBeMaintained = txtToBeMaintained.Text.ToString().Trim();
        ActivityDocType.ToBeSubmitted = txtToBeSubmitted.Text.ToString().Trim();
        ActivityDocType.MasterPDFName = txtMasterPDFName.Text;
        ActivityDocType.AccessPath = txtAccessPath.Text;
        ActivityDocType.IsActive = Convert.ToBoolean(chkDocIsActive.Checked);
        ActivityDocType.CreatedBy = BaseMsg.EmployeeCode;
        ActivityForDocList = Bus.MasActivityDocumentTypeInsertUpdateandDelete(ActivityDocType, ActivityComp);
        GrdDocument.EditIndex = -1;
        foreach (ActivityDocumentTypeMasterMsg DocumentUpdate in ActivityForDocList)
        {
            if (DocumentUpdate.ActivityDocResult == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.Update + "');", true);
                LoadGridDocument(ActivityForDocList);
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DocumentUpdate.ActivityDocResult + "');", true);
                break;
            }
        }
    }
    private void TaskSave()
    {
        ActivityForCompanyMasterMsg ActivityComp = new ActivityForCompanyMasterMsg();
        NonComplianceTaskMasterMsg NonCompTask = new NonComplianceTaskMasterMsg();
        //NonComp.Flag = "I";
        NonCompTask.ActivityForCompanyId = Convert.ToInt32(txtCompanyActivityId.Text);
        NonCompTask.ComplianceTaskName = txtTaskName.Text;
        NonCompTask.IsActive = chkTaskIsActive.Checked;
        NonCompTask.CreatedBy = BaseMsg.EmployeeCode;
        NonCompList = Bus.MasNonComplianceTaskInsertUpdateandDelete(NonCompTask, ActivityComp);
        //Output Dispay
        foreach (NonComplianceTaskMasterMsg NoncompSave in NonCompList)
        {
            if (NoncompSave.NonCompResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.SuccessFullySaved + "');", true);
                AllClear();
                LoadGrdTaskMaster();
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + NoncompSave.NonCompResult + "');", true);
                break;
            }
        }
    }
    #endregion   
    #region Vaidation
    
    private int IsValidValues(int RowIndex)
    {
        int Error = 0;
        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "MM/dd/yyyy";
        GridViewRow row = GrdCompActivities.Rows[RowIndex];
        DropDownList ddlexecutionperson = (DropDownList)row.FindControl("ddlExecutionPerson");
        DropDownList ddlreviewperson = (DropDownList)row.FindControl("ddlReviewPerson");
        DropDownList ddlheadperson = (DropDownList)row.FindControl("ddlHeadPerson");
        DropDownList ddlduemonth = (DropDownList)row.FindControl("ddlduemonth");
        DropDownList ddlduedate = (DropDownList)row.FindControl("ddlDueDate");
        DropDownList ddldueday = (DropDownList)row.FindControl("ddlDueDay");
        DropDownList ddltrigmonth = (DropDownList)row.FindControl("ddlTrigMonth");
        DropDownList ddltrigdate = (DropDownList)row.FindControl("ddlTrigDate");
        DropDownList ddltrigday = (DropDownList)row.FindControl("ddlTrigDay");
        DropDownList ddlfrequencey = (DropDownList)row.FindControl("ddlFrequencey");
        string ErrorMessage = string.Empty;

        if (ddlexecutionperson.SelectedValue == ddlreviewperson.SelectedValue)
        {
            if (ddlexecutionperson.SelectedValue == ddlheadperson.SelectedValue)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Execution,Review and Head Person should be in Different" + "');", true);
                return Error = 1;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Execution and Review Person should be in Different" + "');", true);
                return Error = 1;
            }
        }
        else if (ddlexecutionperson.SelectedValue == ddlheadperson.SelectedValue)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Execution and Head Person should be in Different" + "');", true);
            return Error = 1;
        }
        else if (ddlreviewperson.SelectedValue == ddlheadperson.SelectedValue)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Review and Head Person should be in Different" + "');", true);
            return Error = 1;
        }

        if (ddlfrequencey.SelectedItem.Text.Trim() == Config.GetAppsetting("Freq2"))
        {
            if (ddltrigmonth.SelectedIndex != 0 || ddltrigdate.SelectedIndex != 0 || ddltrigday.SelectedIndex != 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "When the frequecy is "+ddlfrequencey.SelectedItem.Text+" ,then Tirggermonth,TriggerDate and TriggerDay should be in NA" + "');", true);
                return Error = 1;
            }

            if (ddlduemonth.SelectedIndex != 0 || ddldueday.SelectedIndex != 0 || ddlduedate.SelectedIndex != 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "When the frequecy is " + ddlfrequencey.SelectedItem.Text + " ,then Duemonth,DueDate and DueDay should be in NA" + "');", true);
                return Error = 1;
            }
        }

        if (ddlfrequencey.SelectedItem.Text.Trim() == Config.GetAppsetting("Freq3"))
        {
            if (ddltrigmonth.SelectedIndex != 0 || ddltrigdate.SelectedIndex != 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "When the frequecy is " + ddlfrequencey.SelectedItem.Text + " ,then Tirggermonth and TriggerDate should be in NA" + "');", true);
                return Error = 1;
            }

            if (ddlduemonth.SelectedIndex != 0 || ddlduedate.SelectedIndex != 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "When the frequecy is " + ddlfrequencey.SelectedItem.Text + " ,then Duemonth and DueDate should be in NA" + "');", true);
                return Error = 1;
            }

            if (ddldueday.SelectedIndex == 0 || ddltrigday.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "When the frequecy is " + ddlfrequencey.SelectedItem.Text + " ,then TriggerDay and DueDay should not be in NA" + "');", true);
                return Error = 1;
            }
        }

        if (ddlfrequencey.SelectedItem.Text.Trim() == Config.GetAppsetting("Freq4") || ddlfrequencey.SelectedItem.Text == Config.GetAppsetting("Freq5") || ddlfrequencey.SelectedItem.Text == Config.GetAppsetting("Freq6"))
        {
            if (ddltrigday.SelectedIndex != 0 || ddldueday.SelectedIndex != 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "When the frequecy is " + ddlfrequencey.SelectedItem.Text + " ,then Tirggerday and DueDay should be in NA" + "');", true);
                return Error = 1;
            }

            if (ddltrigdate.SelectedIndex == 0 || ddltrigmonth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "When the frequecy is " + ddlfrequencey.SelectedItem.Text + " ,then Tirggermonth and TriggerDate should not be in NA" + "');", true);
                return Error = 1;
            }

            if (ddlduedate.SelectedIndex == 0 || ddlduemonth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "When the frequecy is " + ddlfrequencey.SelectedItem.Text + " ,then DueDate and DueMonth should not be in NA" + "');", true);
                return Error = 1;
            }
            int DueDate = Convert.ToInt32(ddlduedate.SelectedValue);
            int DueMonth = Convert.ToInt32(ddlduemonth.SelectedValue);
            int DueYear = System.DateTime.Now.Year;
            double FrequencyDays = 0;
            DateTime Todate = Convert.ToDateTime(DueYear.ToString() + "/" + DueMonth.ToString() + "/" + DueDate.ToString(), dateinfo);
            List<FrequencyMasterMsg> frequencyList = getFrequencey();
            var freq = (from Frequency in frequencyList
                        where Frequency.FrequencyName.Equals(ddlfrequencey.SelectedItem.Text)
                        select Frequency).ToList();
            foreach (var f in freq)
            {
                FrequencyDays = Convert.ToDouble("-" + f.FrequencyDays);
            }
            DateTime FromDate = Todate.AddDays(FrequencyDays);
            int TriggDate = Convert.ToInt32(ddltrigdate.SelectedValue);
            int TriggMonth = Convert.ToInt32(ddltrigmonth.SelectedValue);
            int TriggYear = System.DateTime.Now.Year;
            DateTime TriggerDate = Convert.ToDateTime(TriggYear.ToString() + "/" + TriggMonth.ToString() + "/" + TriggDate.ToString(), dateinfo);
            if (TriggerDate > Todate)
            {
                TriggerDate = TriggerDate.AddYears(-1);
            }
            if (TriggerDate < FromDate)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Triggerdate and Month should be in between the frequencydays and DueDate" + "');", true);
                return Error = 1;
            }
        }

        //int DueDate = Convert.ToInt32(ddlduedate.SelectedValue);
        //int DueMonth = Convert.ToInt32(ddlduemonth.SelectedValue);
        //int DueYear = System.DateTime.Now.Year;
        //DateTime Todate = Convert.ToDateTime(DueYear.ToString() + "/" + DueMonth.ToString() + "/" + DueDate.ToString());
        //List<FrequencyMasterMsg> frequencyList=getFrequencey();
        //string FrequencyDay=(from Frequency in frequencyList
        //                   where Frequency.Equals(ddlfrequencey.SelectedItem.Text)
        //                   select Frequency.FrequencyDays).ToString();
        //double FrequencyDays=Convert.ToDouble("-"+FrequencyDay);
        //DateTime FromDate = Todate.AddDays(FrequencyDays);
        //int TriggDate = Convert.ToInt32(ddlduedate.SelectedValue);
        //int TriggMonth = Convert.ToInt32(ddlduemonth.SelectedValue);
        //int TriggYear = System.DateTime.Now.Year;
        //DateTime TriggerDate = Convert.ToDateTime(TriggYear.ToString() + "/" + TriggMonth.ToString() + "/" + TriggDate.ToString());

        return Error;
    }
    #endregion
    #region Clear
     public void AllClear()
     {

         txtTaskName.Text = "";
         chkTaskIsActive.Checked = false;
         LoadGrdTaskMaster();

     }
     #endregion
    
}