using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Ganini.Lib;
using Resources;
using System.IO;
public partial class ActivityDocumentMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus();   
    public static List<ActivityMasterMsg> ActivityList;
    List<DepartmentMsg> DeptList = new List<DepartmentMsg>();
    List<CategoryMasterMsg> CtgryList = new List<CategoryMasterMsg>();
    List<SeverityMasterMsg> SvrtyList = new List<SeverityMasterMsg>();   
    UserAccess user = new UserAccess();
    public static string ProgramName = string.Empty;   
    List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    List<ActivityForCompanyMasterMsg> ActivityForCmpnyList = new List<ActivityForCompanyMasterMsg>();
    List<ActivityDocumentTypeMasterMsg> ActivityForDocList = new List<ActivityDocumentTypeMasterMsg>();
    List<ActMasterMsg> ActList = new List<ActMasterMsg>();
    List<FrequencyMasterMsg> FncyList = new List<FrequencyMasterMsg>();
    List<DocumentTypeMasterMsg> DocumentList = new List<DocumentTypeMasterMsg>();
    List<ActivityActionMsg> ActionList = new List<ActivityActionMsg>();
    List<DocumentTypeActionMsg> DocmActionList = new List<DocumentTypeActionMsg>();
    List<ActivityDocumentMsg> ActivityDocList = new List<ActivityDocumentMsg>();
    BaseClass BaseMsg = new BaseClass();
    public static int DeleteIndex = 0;
    public static int UpdateIndex = 0;
    string Docfilepath = ConfigurationManager.AppSettings["DocFolderPath"].ToString();
    string filename;
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //LoadGrdActivityMaster();
            LoadCompanyName();
            LoadActName();
            //if (EmployeeMasterMsg.Count == 0 && StateList.Count == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.MsgForGrdnotLoad + "');", true);
            //    Pnlgv.Visible = false;
            //    pnlAdd.Visible = true;

            //}
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();

        GrdDocumentMaster.Visible = true;
        LoadGrdActivityMaster();
        if (ddlActName.SelectedIndex > 0)
        {
            btnCompActivitiesHide.Visible = true;
        }
    }
    protected void btnCompActivitiesHide_Click(object sender, EventArgs e)
    {
        if (btnCompActivitiesHide.Text == "- Collapse")
        {
            pnlDocMaster.Visible = false;
            btnCompActivitiesHide.Text = "+ Expand";
        }
        else
        {
            pnlDocMaster.Visible = true;
            btnCompActivitiesHide.Text = "- Collapse";

        }
    }
    protected void GrdDocument_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int WRowIndex = Convert.ToInt32(e.CommandArgument.ToString());
        //since is a TemplateField we have to use "((Label)row.FindControl("lblActDtlId")).Text"

        if (e.CommandName == "Doc_Edit")
        {
            GridViewRow row = GrdDocument.Rows[WRowIndex];
            TextBox txtActivityId = (TextBox)row.FindControl("txtActivityId");
            //TextBox txtActivityDocId = (TextBox)row.FindControl("txtActivityDocumentId");
            //txtActDocId.Text=((Label)row.FindControl("")).t;
            txtActDocId.Text = ((Label)row.FindControl("lblActivityDocumentId")).Text;
            if (txtActDocId.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ErrDocument + "');", true);
            }
            else
            {

                ddlDocType.SelectedValue = ((Label)row.FindControl("lblDocumentTypeId")).Text;
                ddlLocation.SelectedValue = ((Label)row.FindControl("lblLocationId")).Text;
                ddlState.SelectedValue = ((Label)row.FindControl("lblStateId")).Text;
                chkSaveIsActive.Checked = ((CheckBox)row.FindControl("chkDocIsActive")).Checked;
                btnSave.Text = "Update";

                String jsScript = "";
                //Asking a alert Message to Update or not
                jsScript += "var answer=confirm(\'" + "Are you sure, You want to Edit?" + "\');\n";
                jsScript += "if (answer){\n";
                //If answer is OK then Updating the HiddenField(control Available) HidUpdateCount as '1' and calling the btnSave_Click to call the Method CompanyUpdate
                jsScript += "document.getElementById(\"ContentPlaceHolder1_HidUpdateCount\").value='" + "1" + "';\n";
                // jsScript += "document.getElementById(\"ContentPlaceHolder1_btnSave\").click();\n";
                jsScript += "}\n";
                jsScript += "else{\n";
                //If answer is CANCEL then Updating the HiddenField(control Available) HidUpdateCount as '0' 
                jsScript += "document.getElementById(\"ContentPlaceHolder1_HidUpdateCount\").value='" + "0" + "';\n";
                jsScript += "}\n";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", jsScript, true);
            }
        }
    }
    protected void GrdActivityMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        int WRowIndex = Convert.ToInt32(e.CommandArgument.ToString());
        //since is a TemplateField we have to use "((Label)row.FindControl("lblActDtlId")).Text"

        if (e.CommandName == "Select")
        {
            GridViewRow row = GrdDocumentMaster.Rows[WRowIndex];

            //txtCmpName.Text = ddlCompanyName.SelectedItem.Text;
            //txtAct.Text = ddlActName.SelectedItem.Text;
            txtActivities.Text = ((Label)row.FindControl("lblActivityName")).Text;
            txtActivityId.Text = ((Label)row.FindControl("lblActivityId")).Text;
            //when Grid is selected ActivityDocumentId is 0--
            txtActDocId.Text = "0";
            pnlAdd.Visible = true;
            //pnlCompanyAct.Enabled = true;
            LoadLocation();
            LoadState();
            LoadDocumentType();
            LoadGrdDocumentMaster();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        filename = Path.GetFileName(fluAccessPath.FileName);
        txtDocumentName.Text = filename;

        if (HidUpdateCount.Value == "1")
        {
            //DocumentSave();
            //HidUpdateCount.Value = "0";
            btnSave.Text = "Save";// Rename Save Button after Updating.
            //return;
        }

        if (IsDocSave() == 0)
        {
            DocumentSave();
        }
        LoadGrdDocumentMaster();
        GrdDocument.Enabled = true;
    }
    #endregion
    #region Methods
    public void LoadGrdActivityMaster()
    {
        ActivityMasterMsg Activity = new ActivityMasterMsg();
        Activity.Flag = "R";
        Activity.EmployeeCode = BaseMsg.EmployeeCode;
        ActivityList = Bus.MasActivitiesInsertUpdateandDelete(Activity);
        GrdDocumentMaster.DataSource = "";
        GrdDocumentMaster.DataSource = ActivityList;
        GrdDocumentMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdDocumentMaster.Columns[GrdDocumentMaster.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdDocumentMaster.Columns[GrdDocumentMaster.Columns.Count - 1].Visible = false;
        }
    }
    private void LoadCompanyName()
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.EmployeeCode = BaseMsg.EmployeeCode;
        ddlCompanyName.DataSource = Bus.CompanyMasterSelect(Emp);
        ddlCompanyName.DataTextField = "CompanyName";
        ddlCompanyName.DataValueField = "CompanyCode";
        ddlCompanyName.DataBind();
        ddlCompanyName.Items.Insert(0, "-- Select Please --");
    }
    private void LoadActName()
    {
        // EmployeeMasterMsg emp = new EmployeeMasterMsg();
        ActMasterMsg Act = new ActMasterMsg();
        Act.Flag = "R";
        ActList = Bus.MasActsInsertUpdateandDelete(Act);
        var ddlActList = (from ActName in ActList
                          select new { ActName.ActName, ActName.ActId }).Distinct().ToList();
        ddlActName.DataSource = ddlActList;
        ddlActName.DataTextField = "ActName";
        ddlActName.DataValueField = "ActId";
        ddlActName.DataBind();
        ddlActName.Items.Insert(0, new ListItem("-- All --", "0"));
    }
    public List<ActivityMasterMsg> getActName()
    {
        int ActId = 0;//For Dummy Purpose
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        EmpMsg.EmployeeCode = BaseMsg.EmployeeCode;
        ActivityList = Bus.ActivityMasterSelect(EmpMsg, ActId);
        return ActivityList;
    }
    public List<DepartmentMsg> getDepartmentName()
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        DeptList = Bus.DepartmentMasterSelect(EmpMsg);
        return DeptList;
    }
    public List<CategoryMasterMsg> getCategoryName()
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        CtgryList = Bus.CategoryMasterSelect(EmpMsg);
        return CtgryList;
    }
    public List<SeverityMasterMsg> getSeverityName()
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        SvrtyList = Bus.SeverityMasterSelect(EmpMsg);
        return SvrtyList;
    }
    private void DocumentSave()
    {
        ActivityDocumentMsg ActivityDoc = new ActivityDocumentMsg();
        if (HidUpdateCount.Value == "1")
        {
            ActivityDoc.Flag = "U";
            ActivityDoc.ActivityDocumentId = Convert.ToInt32(txtActDocId.Text);

        }
        else
        {
            HidUpdateCount.Value = "0";
            ActivityDoc.Flag = "I";
            ActivityDoc.ActivityDocumentId = 0;
        }
        //if (Directory.Exists(Docfilepath + txtActivityId.Text) == false)
        //{
        //    Directory.CreateDirectory(Docfilepath + txtActivityId.Text);
        //}
        ActivityDoc.ActivityId = Convert.ToInt32(txtActivityId.Text);

        string Path = Docfilepath + txtActivityId.Text + "\\";
        ActivityDoc.AccessPath = Docfilepath;
        ActivityDoc.DocumentName = filename;
        //ActivityDoc.DocumentTypeId = Convert.ToInt32(ddlDocType.SelectedItem.Value);
        ActivityDoc.DocumentTypeId = Convert.ToInt32(ddlDocType.SelectedValue);
        ActivityDoc.LocationId = Convert.ToInt32(ddlLocation.SelectedValue);
        ActivityDoc.StateId = Convert.ToInt32(ddlState.SelectedValue);
        ActivityDoc.CreatedBy = BaseMsg.EmployeeCode;
        ActivityDoc.IsActive = chkSaveIsActive.Checked;
        string FileDetails = (ActivityDoc.DocumentTypeId * 10000 + ActivityDoc.StateId * 10 + ActivityDoc.LocationId).ToString() + "-";
        FileUpload(Path, filename, FileDetails);
        ActivityDocList = Bus.MasActivityDocumentInsertUpdateandDelete(ActivityDoc);
        //Output Dispay
        foreach (ActivityDocumentMsg ActivityDocSave in ActivityDocList)
        {
            if (ActivityDocSave.ActivityDocResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.SuccessFullySaved + "');", true);
                ClearAll();
                LoadGrdDocumentMaster();
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + ActivityDocSave.ActivityDocResult + "');", true);
                break;
            }
        }
    }
    public void ClearAll()
    {
        txtDocumentName.Text = "";
        ddlDocType.SelectedItem.Text = "";
        //fluAccessPath.FileName = "";
        ddlLocation.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        chkSaveIsActive.Checked = false;
        LoadDocumentType();
    }
    public void LoadGrdDocumentMaster()
    {

        ActivityDocumentMsg Activitydoc = new ActivityDocumentMsg();
        Activitydoc.Flag = "R";
        Activitydoc.ActivityId = Convert.ToInt32(txtActivityId.Text);
        Activitydoc.ActivityDocumentId = Convert.ToInt32(txtActDocId.Text);
        ActivityDocList = Bus.MasActivityDocumentInsertUpdateandDelete(Activitydoc);
        LoadGridDocument(ActivityDocList);
    }
    public void LoadGridDocument(List<ActivityDocumentMsg> ActivityDocList)
    {
        GrdDocument.DataSource = "";
        GrdDocument.DataSource = ActivityDocList;
        GrdDocument.DataBind();
    }
    public void LoadLocation()
    {
        LocationMsg LocMsg = new LocationMsg();
        ddlLocation.DataSource = Bus.LocationSelect();
        ddlLocation.DataTextField = "LocationName";
        ddlLocation.DataValueField = "LocationId";
        ddlLocation.DataBind();
        ddlLocation.Items.Insert(0, "-- Select Please --");
    }
    private void LoadState()
    {
        List<StateMasterMsg> StateList = new List<StateMasterMsg>();
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        StateList = Bus.StateMasterSelect(EmpMsg);
        ddlState.DataTextField = "StateName";
        ddlState.DataValueField = "StateId";
        ddlState.DataSource = StateList;
        ddlState.DataBind();
        ddlState.Items.Insert(0, "-- Select Please --");
    }
    public void LoadDocumentType()
    {

        DocumentTypeMasterMsg Doc = new DocumentTypeMasterMsg();
        ddlDocType.DataTextField = "DocumentTypeName";
        ddlDocType.DataValueField = "DocumentTypeId";
        ddlDocType.DataSource = Bus.DocumentTypeMasterSelectSp();
        ddlDocType.DataBind();
        ddlDocType.Items.Insert(0, "-- Select Please --");
    }
    public void FileUpload(string Path, string FileName, string FileDetails)
    {
        if (!Directory.Exists(Path))
        {
            Directory.CreateDirectory(Path);
        }
        if (File.Exists(Path + FileDetails + filename))
        {
            File.Move(Path + FileDetails + filename, Path + FileDetails + filename + "_old");
            fluAccessPath.SaveAs(Path + FileDetails + filename);
            File.Delete(Path + FileDetails + filename + "_old");
        }
        fluAccessPath.SaveAs(Path + FileDetails + filename);
    }
    public void DocumentUpdate()
    {

        GridViewRow row = GrdDocument.Rows[UpdateIndex];
        ActivityDocumentMsg ActivityDocMsg = new ActivityDocumentMsg();
        //Document.Flag = "U";
        TextBox txtActivityId = (TextBox)row.FindControl("txtActivityId");
        TextBox txtActivityDocId = (TextBox)row.FindControl("txtActivityDocumentId");
        //TextBox txtAccessPath = (TextBox)row.FindControl("txtAccessPath");
        //TextBox txtDocName = (TextBox)row.FindControl("txtDocumentName");
        //TextBox txtDocumentType = (TextBox)row.FindControl("txtDocumentType");
        //TextBox txtLocation = (TextBox)row.FindControl("txtLocation");
        //TextBox txtState = (TextBox)row.FindControl("txtState");
        DropDownList ddlDocumentType = (DropDownList)row.FindControl("ddlDocumentType");
        DropDownList ddlLocation = (DropDownList)row.FindControl("ddlLocation");
        DropDownList ddlState = (DropDownList)row.FindControl("ddlState");
        CheckBox IsActive = (CheckBox)row.FindControl("chkIsActiveStates");

        ActivityDocMsg.ActivityId = Convert.ToInt32(txtActivityId.Text);
        ActivityDocMsg.ActivityDocumentId = Convert.ToInt32(txtActivityDocId.Text);
        //ActivityDocMsg.DocumentName = txtDocName.Text;
        ActivityDocMsg.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedItem.Value);
        ActivityDocMsg.LocationId = Convert.ToInt32(ddlLocation.SelectedItem.Value);
        ActivityDocMsg.StateId = Convert.ToInt32(ddlState.SelectedItem.Value);
        ActivityDocMsg.AccessPath = Docfilepath;
        ActivityDocMsg.IsActive = Convert.ToBoolean(IsActive.Checked);
        ActivityDocMsg.CreatedBy = BaseMsg.EmployeeCode;
        ActivityDocList = Bus.MasActivityDocumentInsertUpdateandDelete(ActivityDocMsg);
        GrdDocument.EditIndex = -1;
        foreach (ActivityDocumentMsg DocumentUpdate in ActivityDocList)
        {
            if (DocumentUpdate.ActivityDocResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.Update + "');", true);
                LoadGridDocument(ActivityDocList);
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DocumentUpdate.ActivityDocResult + "');", true);
                break;
            }
        }
    }
    #endregion
    #region Validation
    private int IsDocSave()
    {
        int Error = 0;
        string DisplayError = "";
        if (txtDocumentName.Text.Trim() == "" || txtDocumentName.Text.Length.ToString().Trim() == "0")
        {
            DisplayError = DisplayError + EComplianceResource.ErrDocumentName;
            Error = 1;
        }
        if (ddlDocType.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "--" + EComplianceResource.ErrSelectDocumentType;
            Error = 1;
        }
      
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    #endregion
    
     
}