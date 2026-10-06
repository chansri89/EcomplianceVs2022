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
public partial class ActivityAction : System.Web.UI.Page
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
    public static List<ActivityActionMsg> ActivityActionList = new List<ActivityActionMsg>();
    ActivityActionMsg ActivtyAction = new ActivityActionMsg();
    List<ActivityDocumentMsg> ActiondocList = new List<ActivityDocumentMsg>();
    public static EmployeeMasterMsg Emp=new EmployeeMasterMsg();
    BaseClass BaseMsg = new BaseClass();
    public static int DeleteIndex = 0;
    public static int UpdateIndex = 0;
    string Docfilepath = ConfigurationManager.AppSettings["ActionFolderPath"].ToString();
    string filename;
    private bool True;
    UserAccess user = new UserAccess();
    public static string ProgramName = string.Empty;

    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadCompanyName();
            LoadActName();
            LoadRemiderYear();
            LoadRemiderMonth();
            LoadRemiderDay();
            LoadTriggerMonth();
            LoadTriggerDay();
            //LoadDocumentType();
            if (Request.QueryString["IsHomePageAction"] != null)
            {
                BaseMsg.IsHomePageAction = Request.QueryString["IsHomePageAction"];
            }
            if (BaseMsg.IsHomePageAction == "Y") //After Login  ActivityAction Page as Requested in WebConfig setting "IsHomePageAction" is Displayed
            {
                ddlCompanyName.SelectedValue = "0";//Get Actions for all the company , login user assigned
                pnlGrdAsonwhen.Visible = false;
                txtasonwhen.Visible = false;
                LoadGrdActivityforCompanyMaster();
                pnlGrdCompActivities.Visible = true;
                PnlActionSummary.Visible = true;               
                btnAsonwhenConfirm.Visible = true;
                pnlNext.Visible = true;
                btnAsonwhenConfirm.Focus();
                //added by abinayaa for db size checking --130513
                if (!Page.IsPostBack)
                {
                    if (BaseMsg.LoginResult == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.DBsize + "');", true);
                    }
                }
                //added by abinayaa for db size checking --130513
            }
            else
            {
                //If  ActivityAction Page is not requested in WebConfig setting "IsHomePageAction" Home page goes to UserAcivites Page
                //When ActivityAction is Selected from Menu Show Default Companycode and ActName as Apprentice act having ID=1.
                ddlCompanyName.SelectedValue = BaseMsg.CompanyCode;
                ddlActName.SelectedValue = "1";//ActName as Apprentice act having ID=1.
                LoadGrdActivityforCompanyMaster();
                pnlGrdAsonwhen.Visible = true;
                //txtasonwhen.Visible = true;
                pnlGrdCompActivities.Visible = true;
                pnlNext.Visible = false;
                PnlActionSummary.Visible = false;
                pnlasonwhen.Visible = true;
                tblTitle.Visible = true;
            }
            //GrdCompActivities.Visible = false;
            
        }
       // pnlCompany.Enabled = false;
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        //if (IsValidSave() == 0)
        //{
        
                GrdCompActivities.Visible = true;
                LoadGrdActivityforCompanyMaster();          
                GrdAsonWhen.Visible = true;
                PnlActionSummary.Visible = false;
                pnlCompanyAct.Visible = false;
                pnlActionMonth.Visible = false;
                pnlCompletedDate.Visible = false;
                pnlUpload.Visible = false;
                PnlSearch.Visible = true;
                PnlSearch.Enabled = true;
                txtActivityNameFilter.Enabled = true;
                lblActivityNameFiter.Enabled = true;
                btnSearch.Enabled = true;
                txtActivityNameFilter.Text = "";
                btnSearchClear.Visible = true;
                txtTitle.Visible = false;
                btnCollapse.Visible = false;
          
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.CreatePermissionRestricted + "');", true);
        }
        else
        {
            if (IsActionValidSave() == 0)
            {
                ActionSave();
            }
        }
    }    
    protected void btnAsonwhenConfirm_Click(object sender, EventArgs e)
    {
        BaseMsg.IsHomePageAction = "N";
        btnAsonwhenConfirm.Visible = false;
        //tdasConfirm.Visible = false;
        Response.Redirect("ActivityAction.aspx");

    }
    protected void GrdCompActivities_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int WRowIndex = Convert.ToInt32(e.CommandArgument.ToString());
        //since is a TemplateField we have to use "((Label)row.FindControl("lblActDtlId")).Text"
        if (e.CommandName == "Select")
        {
            //if (ddlCompanyName.SelectedIndex == 0 || ddlActName.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ErrAssignAllEdit + "');", true);
            //    return;
            //}
            GridViewRow row = GrdCompActivities.Rows[WRowIndex];
            txtActivityActionId.Text = ((Label)row.FindControl("lblActivityActionId")).Text;
            if (txtActivityActionId.Text == "0")
            {
                // ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ErrGridSelect + "');", true);
                pnlCompanyAct.Visible = false;
                pnlCompletedDate.Visible = false;
                pnlUpload.Visible = false;
                pnlActionMonth.Enabled = true;
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ActivityAssigned + "');", true);
                pnlCompanyAct.Visible = true;
                pnlCompletedDate.Visible = true;
                //pnlUpload.Visible = true;--commented by abinayaa 161112
                pnlActionMonth.Enabled = false;
            }
            txtCmpName.Text = ddlCompanyName.SelectedItem.Text;
            txtAct.Text = ddlActName.SelectedItem.Text;
            txtCompanyActivityId.Text = ((Label)row.FindControl("lblActivityForCompanyId")).Text;
            txtActivities.Text = ((Label)row.FindControl("lblActivityName")).Text;
            // txtExecutioner.Text = ((Label)row.FindControl("lblExecutionPerson")).Text;
            txtExecutioner.Text = ((Label)row.FindControl("lblExecutionEmployeeCode")).Text;
            txtReminderYear.Text = ((Label)row.FindControl("lblReminderYear")).Text;
            txtRemainderDate.Text = ((Label)row.FindControl("lblReminderDate")).Text;
            txtRemainderMonth.Text = ((Label)row.FindControl("lblReminderMonth")).Text;
            txtRemainderDay.Text = ((Label)row.FindControl("lblReminderDay")).Text;
            txtActivityActionId.Text = ((Label)row.FindControl("lblActivityActionId")).Text;
            txttriggDate.Text = ((Label)row.FindControl("lblTrigDate")).Text;
            txttriggDay.Text = ((Label)row.FindControl("lblTrigDay")).Text;
            txttriggMonth.Text = ((Label)row.FindControl("lblTrigMonth")).Text;
            DataTable dtDays = LoadDays();
            ddlRemaingerYear.SelectedValue = ((Label)row.FindControl("lblReminderYear")).Text;
            ddlReminMonth.SelectedValue = ((Label)row.FindControl("lblReminderMonth")).Text;
            ddlReminDate.SelectedValue = ((Label)row.FindControl("lblReminderDate")).Text;
            foreach (DataRow dr in dtDays.Rows)
            {
                if (dr["Day"].ToString() == ((Label)row.FindControl("lblReminderDay")).Text)
                {
                    ddlReminDay.SelectedValue = dr["Number"].ToString();
                    break;
                }
            }
            //ddlReminDay.SelectedItem.Text = ((Label)row.FindControl("lblReminderDay")).Text;
            ddltrigMonth.SelectedValue = ((Label)row.FindControl("lblTrigMonth")).Text;
            ddltrigDate.SelectedValue = ((Label)row.FindControl("lblTrigDate")).Text;
            //ddltrigDay.SelectedValue = ((Label)row.FindControl("lblTrigDay")).Text;

            System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
            dateinfo.ShortDatePattern = "MM/dd/yyyy";
            string Date = Convert.ToDateTime(((Label)row.FindControl("lblCompletedDate")).Text, dateinfo).ToString("dd/MM/yyyy");
            txtCompletedDate.Text = Date;//row.Cells[20].Text;//Completed date cell index is 20
            txtRemarks.Text = ((Label)row.FindControl("lblRemarks")).Text;

            pnlCompanyAct.Visible = true;
            pnlCompletedDate.Visible = true;
            //pnlUpload.Visible = true;--commented by abinayaa 161112

            pnlCompanyAct.Enabled = false;
            //pnlDocType.Enabled = false;
            //fluDocfile.Enabled = false;
            pnlDocType.Enabled = true;
            fluDocfile.Enabled = true;
            pnlActionMonth.Visible = true;
            //pnlSave.Visible = true;
            LoadGrdDocument();
            LoadDocumentType(txtCompanyActivityId.Text.Trim());
            //btnCompActivitiesHide.Visible = true;
            //btnCompActivitiesHide.Visible = false;
            pnlGrdAsonwhen.Visible = false;
            GrdAsonWhen.Visible = false;
            txtTitle.Visible = true;
            btnCollapse.Visible = true; 
            pnlUpload.Visible = false;
            PnlSearch.Enabled = false;
            //added by abinayaa for removing colapse btn 240413
            GrdCompActivities.Visible = true;
            pnlGrdCompActivities.Visible = true;
            pnlGridCompActivities.Visible = true;
            btnCollapse.Text = "+";          
            btnGo.Enabled = false;
            //added by abinayaa for removing colapse btn 240413

        }

        
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        filename = Path.GetFileName(fluDocfile.FileName);
        txtDocumentName.Text = filename;
        //GridViewRow row = GrdCompActivities.SelectedRow;
        //txtActivityActionId.Text = ((Label)row.FindControl("lblActivityActionId")).Text;

        
        if (GrdCompActivities.Visible == true)
        {
            GridViewRow row = GrdCompActivities.SelectedRow;
            txtActivityActionId.Text = ((Label)row.FindControl("lblActivityActionId")).Text.Trim();
        }
        else
        {
            GridViewRow row = GrdAsonWhen.SelectedRow;
            txtActivityActionId.Text = ((Label)row.FindControl("lblActivityActionId")).Text.Trim();
        }

        if (txtDocumentName.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ErrDocumentName + "');", true);
        }
        else
        {
            if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.CreatePermissionRestricted + "');", true);
            }
            else
            {
                if (IsValiddocSave() == 0)
                {
                    DocumentSave();
                    //GrdDocument.Enabled = true;
                }
            }
        }
       
    }
    //protected void btnCompActivitiesHide_Click(object sender, EventArgs e)
    //{
    //    if (btnCompActivitiesHide.Text == "- Collapse")
    //    {
    //        pnlGrdCompActivities.Visible = false;
    //        btnCompActivitiesHide.Text = "+ Expand";
    //        lblActivityGrdColorMsg.Visible = false;
    //        lblActivityGrdColorgreen.Visible = false;
    //        lblActionColorRed.Visible = false;
    //        lblActionColorViolet.Visible = false;
    //        tblTitle.Visible = false;
    //    }
    //    else
    //    {
    //        //LoadCalendar(1, 200, 200);
    //        pnlGrdCompActivities.Visible = true;
    //        btnCompActivitiesHide.Text = "- Collapse";
    //        lblActivityGrdColorMsg.Visible = true;
    //        lblActivityGrdColorgreen.Visible = true;
    //        lblActionColorRed.Visible = true;
    //        lblActionColorViolet.Visible = false;
    //        tblTitle.Visible = true;
    //    }
    //}
    protected void GrdCompActivities_RowDataBound(object sender, GridViewRowEventArgs e)
    {
              
        if (e.Row.Cells[4].Text == "")//(lblActivityActionId)Cells[4]="" denots No Action Enter for the Activity
        {
            int cDate = Convert.ToInt32(System.DateTime.Now.ToString("dd"));
            int cMonth = Convert.ToInt32(System.DateTime.Now.ToString("MM"));
            int cYear = Convert.ToInt32(System.DateTime.Now.ToString("yyyy"));
            int CurrentDate = cYear * 10000 + cMonth * 100 + cDate;
            int DueDate = Convert.ToInt32(((Label)e.Row.FindControl("lblReminderDate")).Text);
            int DueMonth = Convert.ToInt32(((Label)e.Row.FindControl("lblReminderMonth")).Text);
            int DueYear = Convert.ToInt32(((Label)e.Row.FindControl("lblReminderYear")).Text);
            int ReminderDate = DueYear * 10000 + DueMonth * 100 + DueDate;
            int ActivityActionId = Convert.ToInt32(((Label)e.Row.FindControl("lblActivityActionId")).Text);
            if ((((Label)e.Row.FindControl("lblRemarks")).Text == string.Empty && ReminderDate >= CurrentDate) || ActivityActionId==0)
            {
                e.Row.ForeColor = Color.Blue;
                e.Row.Font.Bold = true;
                lblActivityGrdColorMsg.Visible = true;
                lblActivityGrdColorgreen.Visible = true;
                lblActionColorRed.Visible = true;
                lblActionColorViolet.Visible = true;
                e.Row.Cells[22].Text=string.Empty;               
            }
            else
            {
                if (e.Row.Cells[4].Text == "")
                {
                    if (((Label)e.Row.FindControl("lblRemarks")).Text != "")// If Remarks Availability indicates Action Taken Already
                    {
                        e.Row.ForeColor = Color.Green;
                        e.Row.Font.Bold = true;
                        lblActivityGrdColorMsg.Visible = true;
                        lblActivityGrdColorgreen.Visible = true;
                        lblActionColorRed.Visible = true; 
                        lblActionColorViolet.Visible = true;
                         //added by Abinayaa 280213--Check doc is available or not 
                        if (((Label)e.Row.FindControl("lblCompletDate")).Text != string.Empty && Convert.ToInt16(((Label)e.Row.FindControl("lbldocAvbl")).Text)== 0 && ((Label)e.Row.FindControl("lblIsdocReq")).Text == "Y")
                        {
                            e.Row.ForeColor = Color.BlueViolet;//MediumPurple;//MediumVioletRed;
                            e.Row.Font.Bold = true;
                            lblActivityGrdColorMsg.Visible = true;
                            lblActivityGrdColorgreen.Visible = true;
                            lblActionColorRed.Visible = true;
                            lblActionColorViolet.Visible = true;
                        }
                    }
                    else
                    {
                            //added by Abinayaa 040213--Check doc is available or not 
                            ((Label)e.Row.FindControl("lblCompletDate")).Text = string.Empty;//If Activityactionid=0, Completed date should be dispaly Empty
                            if (ReminderDate < CurrentDate) //// If Remarks not Available and current date>DueDate indicates Action Pending Beyond DueDate
                            {
                                if (ActivityActionId > 0)
                                {
                                    e.Row.ForeColor = Color.Red;
                                    e.Row.Font.Bold = true;
                                    lblActivityGrdColorMsg.Visible = true;
                                    lblActivityGrdColorgreen.Visible = true;
                                    lblActionColorRed.Visible = true;
                                    lblActionColorViolet.Visible = true;
                                }

                            }                       
                    }
                }
            }
        }
     
    }
    protected void GrdDocument_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int WRowIndex = Convert.ToInt32(e.CommandArgument.ToString());
        //since is a TemplateField we have to use "((Label)row.FindControl("lblActDtlId")).Text"
        if (e.CommandName == "EditDoc")
        {
            //LoadDocumentType();
            GridViewRow row = GrdDocument.Rows[WRowIndex];
            txtActivityActionId.Text = ((Label)row.FindControl("lblDocActivityActionId")).Text;
            txtActivityActionDocId.Text = ((Label)row.FindControl("lblActivityActionDocId")).Text;
            ddlDocumentType.SelectedValue = ((Label)row.FindControl("lblDocTypeId")).Text;
            txtNotes.Text = ((Label)row.FindControl("lblRemarks")).Text;
            btnAdd.Text = "Update";           
        }

        if (e.CommandName == "View")
        {
            GridViewRow row=GrdDocument.Rows[WRowIndex];
            string Path = Docfilepath;
            string FileName = string.Empty;
            if (((Label)row.FindControl("lblDocName")).Text != string.Empty)
            {
                Path = Path + ((Label)row.FindControl("lblDocActivityActionId")).Text + "\\";
                FileName = ((Label)row.FindControl("lblActivityActionDocId")).Text + " - " + ((Label)row.FindControl("lblDocName")).Text;
                FileDownload(Path, FileName, ((Label)row.FindControl("lblDocName")).Text);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "No File to View" + "');", true);
            }
        }
    }
    protected void GrdAsonWhen_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int WRowIndex = Convert.ToInt32(e.CommandArgument.ToString());
        //since is a TemplateField we have to use "((Label)row.FindControl("lblActDtlId")).Text"
        if (e.CommandName == "Select")
        {
            //if (ddlCompanyName.SelectedIndex == 0 || ddlActName.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ErrAssignAllEdit + "');", true);
            //    return;
            //}
            GridViewRow row = GrdAsonWhen.Rows[WRowIndex];
            txtActivityActionId.Text = ((Label)row.FindControl("lblActivityActionId")).Text;
            if (txtActivityActionId.Text == "0")
            {
                // ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ErrGridSelect + "');", true);
                pnlCompanyAct.Visible = false;
                pnlCompletedDate.Visible = false;
                pnlUpload.Visible = false;
                //pnlActionMonth.Enabled = true;//Commeted by abinayaa for As and When we don't no the due day and month --080513
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ActivityAssigned + "');", true);
                pnlCompanyAct.Visible = true;
                pnlCompletedDate.Visible = true;
                //pnlUpload.Visible = true;--commented by abinayaa 161112
                //pnlActionMonth.Enabled = false;//Commeted by abinayaa for As and When we don't no the due day and month --080513
            }
            txtCmpName.Text = ddlCompanyName.SelectedItem.Text;
            txtAct.Text = ddlActName.SelectedItem.Text;
            txtCompanyActivityId.Text = ((Label)row.FindControl("lblActivityForCompanyId")).Text;
            txtActivities.Text = ((Label)row.FindControl("lblActivityName")).Text;
            // txtExecutioner.Text = ((Label)row.FindControl("lblExecutionPerson")).Text;
            txtExecutioner.Text = ((Label)row.FindControl("lblExecutionEmployeeCode")).Text;
            txtReminderYear.Text = ((Label)row.FindControl("lblReminderYear")).Text;
            txtRemainderDate.Text = ((Label)row.FindControl("lblReminderDate")).Text;
            txtRemainderMonth.Text = ((Label)row.FindControl("lblReminderMonth")).Text;
            txtRemainderDay.Text = ((Label)row.FindControl("lblReminderDay")).Text;
            txtActivityActionId.Text = ((Label)row.FindControl("lblActivityActionId")).Text;
            txttriggDate.Text = ((Label)row.FindControl("lblTrigDate")).Text;
            txttriggDay.Text = ((Label)row.FindControl("lblTrigDay")).Text;
            txttriggMonth.Text = ((Label)row.FindControl("lblTrigMonth")).Text;
            DataTable dtDays = LoadDays();
            ddlRemaingerYear.SelectedValue = ((Label)row.FindControl("lblReminderYear")).Text;
            ddlReminMonth.SelectedValue = ((Label)row.FindControl("lblReminderMonth")).Text;
            ddlReminDate.SelectedValue = ((Label)row.FindControl("lblReminderDate")).Text;
            foreach (DataRow dr in dtDays.Rows)
            {
                if (dr["Day"].ToString() == ((Label)row.FindControl("lblReminderDay")).Text)
                {
                    ddlReminDay.SelectedValue = dr["Number"].ToString();
                    break;
                }
            }
            //ddlReminDay.SelectedItem.Text = ((Label)row.FindControl("lblReminderDay")).Text;
            ddltrigMonth.SelectedValue = ((Label)row.FindControl("lblTrigMonth")).Text;
            ddltrigDate.SelectedValue = ((Label)row.FindControl("lblTrigDate")).Text;
            //ddltrigDay.SelectedValue = ((Label)row.FindControl("lblTrigDay")).Text;

            System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
            dateinfo.ShortDatePattern = "MM/dd/yyyy";
            string Date = Convert.ToDateTime(((Label)row.FindControl("lblCompletedDate")).Text, dateinfo).ToString("dd/MM/yyyy");
            txtCompletedDate.Text = Date;//row.Cells[20].Text;//Completed date cell index is 20
            txtRemarks.Text = ((Label)row.FindControl("lblRemarks")).Text;

            pnlCompanyAct.Visible = true;
            pnlCompletedDate.Visible = true;
            //pnlUpload.Visible = true;--commented by abinayaa 161112

            pnlCompanyAct.Enabled = false;
            //pnlDocType.Enabled = false;
            //fluDocfile.Enabled = false;
            pnlDocType.Enabled = true;
            fluDocfile.Enabled = true;
            pnlActionMonth.Visible = true;
            pnlActionMonth.Enabled = false;//Added by abinayaa for As and When we don't no the due day and month --080513
            //pnlSave.Visible = true;
            LoadGrdDocument();
            LoadDocumentType(txtCompanyActivityId.Text.Trim());
            ////btnCompActivitiesHide.Visible = true;
            pnlGrdCompActivities.Visible = false;
            pnlGridCompActivities.Visible = false;
            GrdCompActivities.Visible = false;
            txtTitle.Visible = true;
            btnCollapse.Visible = true;
            btnCollapse.Text = "+";
        }
    }
    protected void GrdAsonWhen_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells[4].Text == "")//(lblActivityActionId)Cells[4]="" denots No Action Enter for the Activity
        {
            if (((Label)e.Row.FindControl("lblActivityActionId")).Text == "0")
            {
                e.Row.ForeColor = Color.Blue;
                e.Row.Font.Bold = true;
                lblActivityGrdColorMsg.Visible = true;
                lblActivityGrdColorgreen.Visible = true;
                lblActionColorRed.Visible = true;
                lblActionColorViolet.Visible = true;
                e.Row.Cells[22].Text = string.Empty;
            }
            else
            {
                if (e.Row.Cells[4].Text == "")
                {
                    if (((Label)e.Row.FindControl("lblRemarks")).Text != "")// If Remarks Availability indicates Action Taken Already
                    {
                        e.Row.ForeColor = Color.Green;
                        e.Row.Font.Bold = true;
                        lblActivityGrdColorMsg.Visible = true;
                        lblActivityGrdColorgreen.Visible = true;
                        lblActionColorRed.Visible = true;
                        lblActionColorViolet.Visible = true;
                    }
                    else
                    {
                        int cDate = Convert.ToInt32(System.DateTime.Now.ToString("dd"));
                        int cMonth = Convert.ToInt32(System.DateTime.Now.ToString("MM"));
                        int cYear = Convert.ToInt32(System.DateTime.Now.ToString("yyyy"));
                        int CurrentDate = cYear * 10000 + cMonth * 100 + cDate;
                        int DueDate = Convert.ToInt32(((Label)e.Row.FindControl("lblReminderDate")).Text);
                        int DueMonth = Convert.ToInt32(((Label)e.Row.FindControl("lblReminderMonth")).Text);
                        int DueYear = Convert.ToInt32(((Label)e.Row.FindControl("lblReminderYear")).Text);
                        int ReminderDate = DueYear * 10000 + DueMonth * 100 + DueDate;
                        ((Label)e.Row.FindControl("lblCompletDate")).Text = string.Empty;


                        if (ReminderDate < CurrentDate) //// If Remarks not Available and current date>DueDate indicates Action Pending Beyond DueDate
                        {
                            e.Row.ForeColor = Color.Red;
                            e.Row.Font.Bold = true;
                            lblActivityGrdColorMsg.Visible = true;
                            lblActivityGrdColorgreen.Visible = true;
                            lblActionColorRed.Visible = true;
                            lblActionColorViolet.Visible = true;
                        }
                    }
                }
            }
        }
        //lblActionColorViolet.Visible = false;
    }
    protected void btnCollapse_Click(object sender, EventArgs e)
    {
        if (btnCollapse.Text == "+")
        {
            btnCollapse.Text = "-";
            pnlGrdAsonwhen.Visible = true;
            GrdAsonWhen.Visible = true;
            //btnCollapse.Enabled = true;
            btnCollapse.ToolTip = "Click this button to view the As and When Details";
            pnlCompanyAct.Visible = false;
            pnlActionMonth.Visible = false;
            pnlCompletedDate.Visible = false;
            pnlUpload.Visible = false;
            pnlGrdCompActivities.Visible = true;
            pnlGridCompActivities.Visible = true;
            GrdCompActivities.Visible = true;
            //////btnCompActivitiesHide.Visible = false;
            btnSearchClear.Visible = true;
            //added by abinayaa for removing colapse btn 240413
            txtTitle.Visible = false;
            btnCollapse.Visible = false;
            txtActivityNameFilter.Enabled = true;
            btnSearch.Enabled = true;
            btnSearchClear.Enabled = true;
            btnSearch.Visible = true;
            PnlSearch.Enabled = true;
            txtActivityNameFilter.Visible = true;
            lblActivityNameFiter.Visible = true;           
            btnGo.Enabled = true;
            //added by abinayaa for removing colapse btn 240413
        }
        else
        {
            //if(btnCollapse.ToolTip=="Click this button to view the As and When Details")
            //{
            //    pnlCompanyAct.Visible = true;
            //    pnlActionMonth.Visible = true;
            //    pnlCompletedDate.Visible = true;
            //    pnlGrdCompActivities.Visible = true;
            //    pnlGridCompActivities.Visible = true;
            //    GrdCompActivities.Visible = true;
            //} 
            //added by abinayaa for removing colapse btn 240413
                    //btnCollapse.ToolTip = "Click this button to view the ActivityAction Details";
                    //btnCollapse.Text = "+";
                    //pnlGrdAsonwhen.Visible = false;
                    //GrdAsonWhen.Visible = false;
                    //btnCollapse.Enabled = true;
                    //pnlCompanyAct.Visible = true;
                    //pnlActionMonth.Visible = true;
                    //pnlCompletedDate.Visible = true;
                    //pnlUpload.Visible = true;
                    ////btnCompActivitiesHide.Visible = true;
                    //btnCompActivitiesHide.Visible = false;
                    //btnSearchClear.Visible = false;          
                    //btnCollapse.Enabled = true;
            //added by abinayaa for removing colapse btn 240413
        }
    }
    protected void GrdActionSummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((Label)e.Row.FindControl("lblSeverity")).Text.ToLower() == "total")
            {
                e.Row.Font.Bold = true;
            }
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        PnlActionSummary.Visible = false;
        pnlGrdCompActivities.Visible = false;
        pnlGrdAsonwhen.Visible = true;
        pnlConirm.Visible = true;
        divGrdAswhen.Style.Add("height", "500px");
        pnlGrdAsonwhen.Style.Add("height", "500px");
        txtasonwhen.Visible = true;

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        PnlActionSummary.Visible = true;
        pnlGrdCompActivities.Visible = true;
        pnlGrdAsonwhen.Visible = false;
        pnlConirm.Visible = false;
    }
    #endregion
    #region Methods

    public void FileDownload(string Path, string FileName, string OrgFileName)
    {
        try
        {
            //If the file exist means downloading the file from the Path which is passed else showing Could not find Msg
            if (File.Exists(@Path + FileName))
            {
                Response.ContentType = "application/txt";
                Response.AddHeader("content-disposition", "attachment; filename=" + OrgFileName);
                Response.WriteFile(@Path + FileName);
                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Could not find file " + OrgFileName + "');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Access denied" + "');", true);
            Response.End();
        }
    }
    public void LoadGrdActivityforCompanyMaster()
    {
        //EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.ActId = Convert.ToInt32(ddlActName.SelectedValue);
        Emp.CompanyCode = ddlCompanyName.SelectedValue;
        Emp.EmployeeCode = BaseMsg.EmployeeCode;
        ActivityActionMsg ActivityActionmsg = new ActivityActionMsg();
        ActivityActionmsg.AsandwhenFlag = false;
        ActivityActionList = Bus.ActivityActionSelect(Emp,ActivityActionmsg);

        List<ActivityActionMsg> ActivityActionWithoutAsWhen = (from ActivitywithoutAsWhen in ActivityActionList
                                                               where ActivitywithoutAsWhen.AsandwhenFlag == false && ActivitywithoutAsWhen.ActivityActionId > 0 //added by abinayaa 060313.when back ground service is run action automatically created.
                                                               select ActivitywithoutAsWhen).ToList();       
        if (BaseMsg.IsHomePageAction == "Y")
        {
            ActionSummaryLoad(ActivityActionWithoutAsWhen);
          
        }
        LoadGridAction(ActivityActionWithoutAsWhen);
        List<ActivityActionMsg> ActivityActionWithAsWhen = (from ActivitywithAsWhen in ActivityActionList
                                                               where ActivitywithAsWhen.AsandwhenFlag == true
                                                               select ActivitywithAsWhen).ToList();
        LoadGridAsonwhen(ActivityActionWithAsWhen);
        if ( pnlGrdAsonwhen.Visible == true)
        {
            if (ActivityActionWithAsWhen.Count > 0)
            {
                txtasonwhen.Visible = true;
            }
            else
            {
                txtasonwhen.Visible = false;
            }
        }

    }
    public void LoadGridAction(List<ActivityActionMsg> ActivityActionList)
    {
        GrdCompActivities.DataSource = "";
        GrdCompActivities.DataSource = ActivityActionList;
        GrdCompActivities.DataBind();
        if (BaseMsg.IsHomePageAction == "Y")
        {
            GrdCompActivities.Columns[0].Visible = false;
        }
        if (BaseMsg.IsHomePageAction == "Y" && ActivityActionList.Count == 0)
        {
            //BaseMsg.IsHomePageAction = "N";
            //Response.Redirect("UserActivities.aspx");
        }
    }
    private void LoadFiterGrid()
    {
        List<ActivityActionMsg> ActivityListFilter = new List<ActivityActionMsg>();

        ActivityListFilter = (from gradeFilter in ActivityActionList
                              where gradeFilter.ActivityName.ToLower().Contains(txtActivityNameFilter.Text.ToLower().Trim())
                           select gradeFilter).ToList();
        if (ActivityListFilter == null || ActivityListFilter.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ErrGrdSelect + "');", true);
            //txtGrdname.Text = "";
            LoadGridAction(ActivityActionList);
        }
        else
        {
            LoadGridAction(ActivityListFilter);
        }

    }
    public void LoadGrdAsonwhen()
    {
      
        Emp.ActId = Convert.ToInt32(ddlActName.SelectedValue);
        Emp.CompanyCode = ddlCompanyName.SelectedValue;
        Emp.EmployeeCode = BaseMsg.EmployeeCode;
        ActivityActionMsg ActivityActionmsg = new ActivityActionMsg();
        ActivityActionmsg.AsandwhenFlag = true;
        ActivityActionList = Bus.ActivityActionSelect(Emp, ActivityActionmsg);
        //ActivityActionList = (from action in ActivityActionList
        //                      where action.FrequencyName == "As and When" //Added by abinayaa 161112
        //                      select action).ToList();
        LoadGridAsonwhen(ActivityActionList);
    }
    public void LoadGridAsonwhen(List<ActivityActionMsg> ActivityActionList)
    {
        GrdAsonWhen.DataSource = "";
        GrdAsonWhen.DataSource = ActivityActionList;
        GrdAsonWhen.DataBind();
        if (BaseMsg.IsHomePageAction == "Y")
        {
            GrdAsonWhen.Columns[0].Visible = false;
        }
        if (BaseMsg.IsHomePageAction == "Y" && ActivityActionList.Count == 0 && GrdCompActivities.Rows.Count==0)
        {
            BaseMsg.IsHomePageAction = "N";
            Response.Redirect("UserActivities.aspx");
        }
        //txtasonwhen.Visible = true;
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
                          where ActName.IsActive==true
                          select new { ActName.ActName, ActName.ActId }).Distinct().ToList();
        ddlActName.DataSource = ddlActList;
        ddlActName.DataTextField = "ActName";
        ddlActName.DataValueField = "ActId";
        ddlActName.DataBind();
        ddlActName.Items.Insert(0, new ListItem("-- All --","0"));
    }
    public void LoadRemiderYear()
    {
        ddlRemaingerYear.DataSource = LoadReminYear();
        ddlRemaingerYear.DataBind();
        ddlRemaingerYear.Items.Insert(0, new ListItem("-- Select Please --", "0"));
    }
    public void LoadRemiderMonth()
    {
        ddlReminMonth.DataSource = LoadMonth();
        ddlReminMonth.DataBind();
        ddlReminMonth.Items.Insert(0, new ListItem("-- Select Please --", "0"));
    }
    public void LoadRemiderDay()
      {

          ddlReminDay.DataSource = LoadDays();
          ddlReminDay.DataBind();
          ddlReminDay.Items.Insert(0, new ListItem("-- Select Please --","0"));
      }
    public void LoadTriggerMonth()
      {
          ddltrigMonth.DataSource = LoadMonth();
          ddltrigMonth.DataBind();
          ddltrigMonth.Items.Insert(0, new ListItem("-- Select Please --", "0"));
      }
    public void LoadTriggerDay()
      {
          ddltrigDay.DataSource = LoadDays();
          ddltrigDay.DataBind();
          ddltrigDay.Items.Insert(0, new ListItem("-- Select Please --", "0"));
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
        PYear = Convert.ToInt32(System.DateTime.Now.ToString("yyyy"))-1;
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
    public void LoadDocumentType(string CompanyActivityId)
    {

        ActivityForCompanyMasterMsg ActivityComp = new ActivityForCompanyMasterMsg();
        ActivityDocumentTypeMasterMsg ActivityDoc = new ActivityDocumentTypeMasterMsg();
        ActivityComp.CompanyActivityId = Convert.ToInt32(CompanyActivityId);
        ActivityForDocList = Bus.ActivityDocumentTypeMasterSelect(ActivityComp);

        //DocumentTypeMasterMsg Doc = new DocumentTypeMasterMsg();
        //List<DocumentTypeMasterMsg> DocumentTypeMastList = Bus.DocumentTypeMasterSelectSp();
        ActivityForDocList = (from ActiveDocType in ActivityForDocList
                                where ActiveDocType.IsActive == true && (ActiveDocType.ToBeMaintained.ToUpper()=="Y" || ActiveDocType.ToBeSubmitted.ToUpper()=="Y")
                                select ActiveDocType).ToList();
        if (ActivityForDocList != null && ActivityForDocList.Count > 0)
        {
            ddlDocumentType.DataTextField = "DocumentTypeName";
            ddlDocumentType.DataValueField = "ActDocTypeId";           
            ddlDocumentType.DataSource = ActivityForDocList;
            ddlDocumentType.DataBind();
        }
        //else// commeted by Abinayaa 280213.it is not necessery to show the msg.
        //{
        //    ddlDocumentType.Items.Clear();
        //    ddlDocumentType.DataSource = null;
        //    ddlDocumentType.DataBind();
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Please Assign document type for the Activity" + "');", true);
           
           
        //}
        ddlDocumentType.Items.Insert(0, new ListItem("-- Select Please --","0"));

    }   
    private void ActionSave()
    {

        ActivtyAction.ActivityActionId = Convert.ToInt64(txtActivityActionId.Text);
        if (ActivtyAction.ActivityActionId == 0)
        {
            ActivtyAction.Flag = "I";
            ActivtyAction.ExecutionEmployeeCode = txtExecutioner.Text.Trim();
            ActivtyAction.ReminderYear = Convert.ToInt32(ddlRemaingerYear.SelectedValue);
            ActivtyAction.ReminderDate = Convert.ToInt32(ddlReminDate.SelectedValue);
            ActivtyAction.ReminderMonth = Convert.ToInt32(ddlReminMonth.SelectedValue);
            ActivtyAction.ReminderDay = ddlReminDay.SelectedValue;
            ActivtyAction.TriggerDate = 0;// Convert.ToInt32(ddltrigDate.SelectedValue);
            ActivtyAction.TriggerMonth = 0;// Convert.ToInt32(ddltrigMonth.SelectedValue);
            ActivtyAction.TriggerDay = "0";// ddltrigDay.SelectedValue;
            pnlActionMonth.Enabled = true;
            pnlActionMonth.Visible = true;
        }
        else
        {
            ActivtyAction.Flag = "U";
        }
        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd/MM/yyyy";
        ActivtyAction.ActivityForCompanyId = Convert.ToInt32(txtCompanyActivityId.Text.Trim());
        //string Date = Convert.ToDateTime(txtCompletedDate.Text,dateinfo).ToString();
        ActivtyAction.CompletedDate = Convert.ToDateTime(txtCompletedDate.Text.Trim(), dateinfo);
        ActivtyAction.Remarks = txtRemarks.Text;
        ActivtyAction.CreatedBy = BaseMsg.EmployeeCode;

        ActivityActionList = Bus.MasActivityActionInsert(ActivtyAction, Emp);
        //Output Dispay
        foreach (ActivityActionMsg ActionSave in ActivityActionList)
        {
            if (ActionSave.ActionResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.SuccessFullySaved + "');", true);
                LoadGridAction(ActivityActionList);
                //LoadGrdActivityforCompanyMaster();
                if (txtActivityNameFilter.Text == "")
                {
                    LoadGrdActivityforCompanyMaster();
                }
                else
                {
                    LoadFiterGrid();
                }
                LoadDocumentType(txtCompanyActivityId.Text.Trim());
                if (ActivityForDocList.Count == 0)
                {
                    pnlUpload.Visible = false;
                }
                else
                {
                    pnlUpload.Visible = true;
                }
                //AllClear();
                
                pnlDocType.Visible = true;
                PnlDoc.Visible = true;
                GrdDocument.Visible = true;
                PnlSearch.Enabled = true;
                txtActivityNameFilter.Enabled = false;
                lblActivityNameFiter.Enabled = false;
                btnSearch.Enabled = false;
                txtActivityNameFilter.Visible = false;
                lblActivityNameFiter.Visible = false;
                btnSearch.Visible = false;
                btnSearchClear.Enabled = true;
                btnSearchClear.Visible = true;
                //added by abinayaa for removing colapse btn 240413                
                txtActivityNameFilter.Enabled = true;
                btnSearch.Enabled = false;
                btnSearchClear.Enabled = false;
                btnSearch.Visible = true;
                PnlSearch.Enabled = false;
                txtActivityNameFilter.Visible = true;
                lblActivityNameFiter.Visible = true;
                //added by abinayaa for removing colapse btn 240413
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + ActionSave.ActionResult + "');", true);
            }

        }
    }
    public void LoadGrdDocument()
    {
       
        DocumentTypeActionMsg docAction = new DocumentTypeActionMsg();
        ActivityDocumentMsg ActivityActon = new ActivityDocumentMsg();
        docAction.Flag = "R";
        docAction.ActivityActionId = Convert.ToInt32(txtActivityActionId.Text);
        Emp.EmployeeCode = BaseMsg.EmployeeCode;
        DocmActionList = Bus.ActionDocumentTypeInsertUpdatesp(docAction,Emp);
        GrdDocument.DataSource = "";
        GrdDocument.DataSource = DocmActionList;
        GrdDocument.DataBind();
        //LoadGridDocument(DocmActionList);
    }
    public void LoadGridDocument(List<DocumentTypeActionMsg> DocmActionList)
    {
        //GrdDocument.DataSource = "";
        //GrdDocument.DataSource = DocmActionList;
        //GrdDocument.DataBind();
    }
    public void FileUpload(string Path, string FileName, string FileDetails)
    {
        if (!Directory.Exists(Path))
        {
            Directory.CreateDirectory(Path);
        }
        if (File.Exists(Path + FileDetails + filename))
        {
           
            //File.Move(Path + FileDetails + filename, Path + FileDetails + filename + "_old");
            fluDocfile.SaveAs(Path + FileDetails + filename);
            //File.Delete(Path + FileDetails + filename + "_old");
        }
        fluDocfile.SaveAs(Path + FileDetails + filename);
    }
    public void ActionSummaryLoad(List<ActivityActionMsg> ActivityActionList)
    {
        PnlActionSummary.Visible = false;
        int cDate = Convert.ToInt32(System.DateTime.Now.ToString("dd"));
        int cMonth = Convert.ToInt32(System.DateTime.Now.ToString("MM"));
        int cYear = Convert.ToInt32(System.DateTime.Now.ToString("yyyy"));
        int CurrentDate = cYear * 10000 + cMonth * 100 + cDate;
        //var ActionSummaryData = (from ActionSummary in ActivityActionList
        //                         orderby ActionSummary.SeverityName ascending
        //                         group ActionSummary by ActionSummary.SeverityName into Action
        //                         select new
        //                         {
        //                             SeverityName = Action.Key,
        //                             BlueCount = Action.Sum(x => ((x.Remarks == string.Empty && (x.ReminderYear * 10000 + x.ReminderMonth * 100 + x.ReminderDate) >= CurrentDate) || (x.ActivityActionId == 0 && x.Remarks == string.Empty)) ? 1 : 0),
        //                             GreenCount = Action.Sum(x => (x.Remarks != string.Empty  && x.ActivityActionId > 0 && x.docAvbl != 0) ? 1 : 0),//&& x.docAvbl != 0 Added by Abinayaa 280213 for showing only Green color
        //                             RedCount = Action.Sum(x => (x.ReminderYear * 10000 + x.ReminderMonth * 100 + x.ReminderDate) < CurrentDate && x.Remarks == string.Empty && x.ActivityActionId >0 ? 1 : 0),
        //                             violetCount = Action.Sum(x => x.Remarks != string.Empty && x.docAvbl == 0 && x.IsdocReq == "Y" ? 1 : 0)//Added by Abinayaa 280213 for showing Violet color also in grid
        //                         }).ToList();
        //var ActionSummaryTotlaData = (from ActionSummary in ActivityActionList
        //                              orderby ActionSummary.SeverityName ascending
        //                              select new
        //                              {
        //                                  SeverityName = "Total",
        //                                  BlueCount = ActivityActionList.Sum(x => ((x.Remarks == string.Empty && (x.ReminderYear * 10000 + x.ReminderMonth * 100 + x.ReminderDate) >= CurrentDate) || (x.ActivityActionId == 0 && x.Remarks == string.Empty)) ? 1 : 0),
        //                                  GreenCount = ActivityActionList.Sum(x => (x.Remarks != string.Empty && x.ActivityActionId > 0 && x.docAvbl != 0) ? 1 : 0),//&& x.docAvbl != 0 Added by Abinayaa 280213 for showing only Green color
        //                                  RedCount = ActivityActionList.Sum(x => (x.ReminderYear * 10000 + x.ReminderMonth * 100 + x.ReminderDate) < CurrentDate && x.Remarks == string.Empty && x.ActivityActionId > 0 ? 1 : 0),
        //                                  violetCount = ActivityActionList.Sum(x => x.Remarks != string.Empty && x.docAvbl == 0 && x.IsdocReq == "Y" ? 1 : 0)//Added by Abinayaa 280213 for showing Violet color Total also in grid
        //                              }).Distinct().ToList();
        var ActionSummaryData = (from ActionSummary in ActivityActionList // Modified  by Abinayaa 060313
                                 orderby ActionSummary.SeverityName ascending
                                 group ActionSummary by ActionSummary.SeverityName into Action
                                 select new
                                 {
                                     SeverityName = Action.Key,
                                     BlueCount = Action.Sum(x => ((x.Remarks == string.Empty && (x.ReminderYear * 10000 + x.ReminderMonth * 100 + x.ReminderDate) >= CurrentDate) || (x.ActivityActionId == 0 && x.Remarks == string.Empty)) ? 1 : 0),
                                     GreenCount = Action.Sum(x => (x.Remarks != string.Empty && (x.IsdocReq == "N"||(x.docAvbl != 0 && x.IsdocReq == "Y"))) ? 1 : 0),//&& x.docAvbl != 0 Added by Abinayaa 280213 for showing only Green color
                                     RedCount = Action.Sum(x => (x.ReminderYear * 10000 + x.ReminderMonth * 100 + x.ReminderDate) < CurrentDate && x.ActivityActionId > 0 && x.Remarks == string.Empty ? 1 : 0),//&& x.ActivityActionId > 0 
                                     violetCount = Action.Sum(x => x.Remarks != string.Empty && x.docAvbl == 0 && x.IsdocReq == "Y" ? 1 : 0)//Added by Abinayaa 280213 for showing Violet color also in grid
                                 }).ToList();
        var ActionSummaryTotlaData = (from ActionSummary in ActivityActionList
                                      orderby ActionSummary.SeverityName ascending
                                      select new
                                      {
                                          SeverityName = "Total",
                                          BlueCount = ActivityActionList.Sum(x => ((x.Remarks == string.Empty && (x.ReminderYear * 10000 + x.ReminderMonth * 100 + x.ReminderDate) >= CurrentDate) || (x.ActivityActionId == 0 && x.Remarks == string.Empty)) ? 1 : 0),
                                          GreenCount = ActivityActionList.Sum(x => (x.Remarks != string.Empty && (x.IsdocReq == "N" || (x.docAvbl != 0 && x.IsdocReq == "Y"))) ? 1 : 0),//&& x.docAvbl != 0 Added by Abinayaa 280213 for showing only Green color
                                          RedCount = ActivityActionList.Sum(x => (x.ReminderYear * 10000 + x.ReminderMonth * 100 + x.ReminderDate) < CurrentDate && x.ActivityActionId > 0 && x.Remarks == string.Empty ? 1 : 0),//&& x.ActivityActionId > 0
                                          violetCount = ActivityActionList.Sum(x => x.Remarks != string.Empty && x.docAvbl == 0 && x.IsdocReq == "Y" ? 1 : 0)//Added by Abinayaa 280213 for showing Violet color Total also in grid
                                      }).Distinct().ToList();
        var ActionSummaryunion = ActionSummaryData.Union(ActionSummaryTotlaData).ToList();
        if (ActionSummaryunion != null && ActionSummaryunion.Count > 0)
        {
            PnlActionSummary.Visible = true;
            GrdActionSummary.DataSource = "";
            GrdActionSummary.DataSource = ActionSummaryunion;
            GrdActionSummary.DataBind();
        }
    }
    /// <summary>
    /// File Upload
    /// </summary>
    /// <param name="Path"></param>
    /// <param name="FileName"></param>
    public void FileUpload(string Path, string FileName)
    {
        // If the directory does not exist means creating a new directory else saving the file.
        if (!Directory.Exists(Path))
        {
            Directory.CreateDirectory(Path);
        }
        // if the file exist means renaming the old file and and uploading the new file and deleted the old file else Uploading the file
        if (File.Exists(Path + filename))
        {
            File.Move(Path + filename, Path + filename + "_old");
            fluDocfile.SaveAs(Path + filename);
            File.Delete(Path + filename + "_old");
        }
        fluDocfile.SaveAs(Path + filename);
    }
    public void DocumentSave()
    {
        
        DocumentTypeActionMsg docAction = new DocumentTypeActionMsg();
        //if (HidUpdateCount.Value == "1")Abinayaa 151012
        if(btnAdd.Text!="Add")
        {
            docAction.Flag = "U";
            docAction.ActivityActionDocId = Convert.ToInt64(txtActivityActionDocId.Text.Trim());
            btnAdd.Text = "Add";// Rename Save Button after Updating.
        }
        else
        {
           // HidUpdateCount.Value = "0";
            docAction.Flag = "I";
            docAction.ActivityActionDocId = 0;
            //docAction.ActivityActionId = 0; //scs 140712
        }
        docAction.ActivityActionId = Convert.ToInt64(txtActivityActionId.Text.Trim());
      
        //string Path = Docfilepath + txtCompanyActivityId.Text + "\\"; // Fie path common as avbl in web config + Company activityId is concatenated to create individua path
        docAction.StoragePath = Docfilepath;
        docAction.DocumentName = filename;
        docAction.DocumentTypeId =Convert.ToInt32(ddlDocumentType.SelectedValue);
        docAction.DocumentTypeName = ddlDocumentType.SelectedItem.Text.Trim();//241112
        docAction.Remarks = txtNotes.Text.Trim();
        docAction.CreatedBy = BaseMsg.EmployeeCode;
        //string FileDetails = (ActivtyAction.ActivityActionId + "-");
        //Commented by Sathish and Added the logic after the Successfully Save
        //string FileDetails = (docAction.ActivityActionDocId + "-"); // file Name is concatenated with ActivityActionDocId to have uniqueness in FileName
        //FileUpload(Path, filename, FileDetails);
        DocmActionList = Bus.ActionDocumentTypeInsertUpdatesp(docAction,Emp);
        foreach (DocumentTypeActionMsg ActionDocSave in DocmActionList)
        {
            if (ActionDocSave.DocuActionResult == "0")
            {
                //if the activity was successfully Saved means then uploading the file.
                //For uniqueness adding the ActivityActionId with the Path and ActivityActionDocId with the filename
                //string Path = Docfilepath + "\\" + ActionDocSave.ActivityActionId.ToString() + "\\";
                //filename = ActionDocSave.ActivityActionDocId.ToString() + " - " + filename;

                string Path = Docfilepath + "\\" + ActionDocSave.ActivityActionId.ToString() + "\\";//changed by Abinayaa 261112
                filename = ActionDocSave.ActivityDocId.ToString() + " - " + filename;
                //Calling File Upload method to upload file
                FileUpload(Path, filename);
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.SuccessFullySaved + "');", true);
                ClearDocument();
                LoadDocumentType(txtCompanyActivityId.Text.Trim());
                //LoadGrdDocument(); //scs 140712
                //PnlSearch.Visible = false;
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + ActionDocSave.DocuActionResult + "');", true);
                break;
            }
        }
    }
    //private void LoadDocumentType()
    //{
    //    throw new NotImplementedException();
    //}
    #region Clear
    public void AllClear()
    {
        txtCompletedDate.Text = "";
        txtRemarks.Text = "";
        ddlRemaingerYear.SelectedIndex = 0;
        ddlReminDate.SelectedIndex= 0;
        ddlReminDay.SelectedIndex = 0;
        ddlReminMonth.SelectedIndex = 0;
        ddltrigDate.SelectedIndex = 0;
        ddltrigDay.SelectedIndex = 0;
        ddltrigMonth.SelectedIndex = 0;

    }
    public void ClearDocument()
    {
        ddlDocumentType.SelectedIndex = 0;
        txtNotes.Text = string.Empty;
        LoadGrdDocument();
        LoadDocumentType(txtCompanyActivityId.Text.Trim());

    }
    #endregion
    #region GrdDocument
    //protected void GrdDocument_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    //GrdDocument.EditIndex = e.NewEditIndex;
    //    pnlDocType.Enabled = true;
    //    //GrdDocument.Enabled = false;
    //    fluDocfile.Enabled = true;
    //    LoadGrdDocument();

       
    //}
    #endregion
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
        EmpList = (from ActiveEmp in EmpList
                   where ActiveEmp.IsActive == true
                   select ActiveEmp).ToList();
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
    public List<EmployeeMasterMsg> getUtimate()
    {
        return EmpList;
    }
    public List<FrequencyMasterMsg> getFrequencey()
    {
        //EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        FncyList = Bus.FrequencyMasterSelect(Emp);
        FncyList = (from ActiveFncy in FncyList
                    where ActiveFncy.IsActive == true
                    select ActiveFncy).ToList();
        return FncyList;
    }
    
    #endregion
    #endregion 
    #region Validation
    //private int IsValidSave()
    //{
    //    int Error = 0;
    //    string DisplayError = "";


    //    if (ddlActName.SelectedIndex == 0 )//|| ddlActName.SelectedIndex > 0
    //    {
    //        DisplayError = DisplayError + EComplianceResource.ErrActionActSelect;
    //        Error = 1;
    //    }

    //    if (Error == 1)
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
    //    }
    //    return Error;
    //}
    private int IsActionValidSave()
    {
        int Error = 0;
        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd/MM/yyyy";
        string DisplayError = "";

        if (txtCompletedDate.Text.Trim() == "" || txtCompletedDate.Text.Length.ToString().Trim() == "0")
        {
            DisplayError = DisplayError + EComplianceResource.ErrCalander;
            Error = 1;
        }

        else if (Convert.ToDateTime(txtCompletedDate.Text, dateinfo) > System.DateTime.Now.Date)
        {
            DisplayError = DisplayError + EComplianceResource.ErrClanderSelect;
            Error = 1;
        }
        if (txtRemarks.Text.Trim() == "" || Convert.ToInt32(txtRemarks.Text.Length.ToString().Trim()) == 0)
        {
            DisplayError = DisplayError + EComplianceResource.ErrRemarks;
            Error = 1;
        }
        //if (ddlDocumentType.SelectedIndex == 0)
        //{
        //    DisplayError = DisplayError + "--" + EComplianceResource.ErrDepartmentNameselect;
        //    Error = 1;
        //}
        //if (txtNotes.Text.Trim() == "" || Convert.ToInt32(txtNotes.Text.Length.ToString().Trim()) == 0)
        //{
        //    DisplayError = DisplayError + "--" + EComplianceResource.ErrActivityName;
        //    Error = 1;
        //}

        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    public int IsValiddocSave()
    {
        int Error = 0;        
        string DisplayError = "";
        if (ddlDocumentType.SelectedIndex == 0)
        {
            DisplayError = DisplayError +  EComplianceResource.ErrSelectDocumentType;
            Error = 1;
        }
        if (txtNotes.Text.Trim() == "" || Convert.ToInt32(txtNotes.Text.Length.ToString().Trim()) == 0)
        {
            DisplayError = DisplayError + "--" + EComplianceResource.ErrRemarks;
            Error = 1;
        }

        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    #endregion
    #region Calendar
   
    protected void LoadCalendar(int CalPlace, int CalLeft, int CalTop)  //Ex:(1,672,114)
    {
        try
        {
            //txtCompletedDate.Text = CalPlace.ToString();
            clndrGetdate.SelectedDate = DateTime.Now;
            LayerCal.Style["left"] = CalLeft.ToString() + "px";
            LayerCal.Style["top"] = CalTop.ToString() + "px";
        }
        catch
        {
        }
    }
    protected void clndrGetdate_SelectionChanged(object sender, EventArgs e)
    {
        UnLoadCalendar();
        clndrGetdate.Visible = false;
        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd/MM/yyyy";
        //if (Convert.ToDateTime(txtCompletedDate.Text, dateinfo) > System.DateTime.Now.Date)
        //{
        //   txtCompletedDate.Text = System.DateTime.Now.Date.ToString("dd-MM-yyyy");
        //}
    }
    protected void UnLoadCalendar()
    {
        txtCompletedDate.Text = clndrGetdate.SelectedDate.ToString(ConfigurationManager.AppSettings["IndianDateFormat"].ToString());
        txtCompletedDate.Text = clndrGetdate.SelectedDate.ToString("dd-MM-yyyy");
        clndrGetdate.Visible = false;
    }
    protected void ImgbtnDate_Click(object sender, ImageClickEventArgs e)
    {
        ////////if (btnCompActivitiesHide.Text == "+ Expand")
        ////////{
        ////////    LoadCalendar(1, 658, 250);
        ////////}
        ////////else
        ////////{
        ////////    LoadCalendar(1, 658, 500);
        ////////}
        clndrGetdate.Visible = true;
        clndrGetdate.SelectedDate = DateTime.Now;
    }
    #endregion                 
    public bool False { get; set; }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadFiterGrid();
    }
    protected void btnSearchClear_Click(object sender, EventArgs e)
    {
        LoadGridAction(ActivityActionList);
        txtActivityNameFilter.Text = "";
        PnlSearch.Enabled = true;
        txtActivityNameFilter.Enabled = true;
        lblActivityNameFiter.Enabled = true;
        btnSearch.Enabled = true;

    }
 
}
