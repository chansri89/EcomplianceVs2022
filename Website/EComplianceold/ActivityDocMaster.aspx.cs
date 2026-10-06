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
public partial class ActivityDocMaster : System.Web.UI.Page
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

            LoadCompanyName();
            LoadActName();
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        //if (IsValidSave() == 0)
        //{
            //if (Emp.ActId == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert(' No Data For The Act');", true);
            //}
            //else
            //{
                GrdCompActivities.Visible = true;
                LoadGrdActivityforCompanyMaster();
            //}
        //}
        if (ddlActName.SelectedIndex > 0)
        {
            btnCompActivitiesHide.Visible = true;
        }
    }
    protected void GrdCompActivities_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int WRowIndex = Convert.ToInt32(e.CommandArgument.ToString());
        //since is a TemplateField we have to use "((Label)row.FindControl("lblActDtlId")).Text"

        if (e.CommandName == "Select")
        {
             GridViewRow row = GrdCompActivities.Rows[WRowIndex];
          
            txtCmpName.Text = ddlCompanyName.SelectedItem.Text;
            txtAct.Text = ddlActName.SelectedItem.Text;
            txtActivities.Text = ((Label)row.FindControl("lblActivityName")).Text;
            txtActivityId.Text = ((Label)row.FindControl("lblActivityId")).Text;
            //when Grid is selected ActivityDocumentId is 0--
            txtActDocId.Text = "0";
            pnlAdd.Visible = true;
            //pnlCompanyAct.Enabled = false;
            LoadLocation();
            LoadState();
            LoadDocumentType();
            LoadGrdDocumentMaster();
        }
    }
    #endregion
    #region Methods
    public void LoadGrdActivityforCompanyMaster()
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        //Emp.ActId = Convert.ToInt32(ddlActName.SelectedItem.Value);
        Emp.ActId = Convert.ToInt32(ddlActName.SelectedValue);
        Emp.CompanyCode = "0"; // ddlCompanyName.SelectedItem.Value;
        ActivityForCompanyMasterMsg ActivityCompany = new ActivityForCompanyMasterMsg();
        ActivityForCmpnyList = Bus.ActivityForCompanyMasterSelect(Emp);
        if (ActivityForCmpnyList.Count == 0)
        {
            GrdCompActivities.DataSource = "";
            GrdCompActivities.DataBind();
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert(' " + EComplianceResource.ErrNoDataforAct + ");", true);
        }
        else
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
        ddlActName.Items.Insert(0,new ListItem("-- All --","0"));
    }
    public DataTable LoadMonth()
    {
        DataTable dtMonth = new DataTable();
        dtMonth.Columns.Add("MonthName", typeof(string));
        dtMonth.Columns.Add("Month", typeof(int));
        DataRow dr0 = dtMonth.NewRow();
        dr0["MonthName"] = "NA";
        dr0["Month"] = 0;
        dtMonth.Rows.Add(dr0);
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
        ddlState.Items.Insert(0,"-- Select Please --");
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
    #endregion
    #region Validation
    //private int IsValidSave()
    //{
    //    int Error = 0;
    //    string DisplayError = "";


    //    if (ddlActName.SelectedIndex == 0) //|| ddlActName.SelectedIndex == 0 || ddlActName.SelectedIndex > 0)//(ddlCompanyName.SelectedIndex == 0 &&ddlCompanyName.SelectedIndex > 0&& (ddlCompanyName.SelectedIndex == 0 && )
    //    {
    //        DisplayError = DisplayError + EComplianceResource.ErrActivityNameselect;
    //        Error = 1;
    //    }

    //    if (Error == 1)
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
    //    }
    //    return Error;
    //}
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
            DisplayError = DisplayError +"--"+ EComplianceResource.ErrSelectDocumentType;
            Error = 1;
        }
        //if (ddlLocation.SelectedIndex == 0)
        //{
        //    DisplayError = DisplayError + "--" + EComplianceResource.ErrLoccation;
        //    Error = 1;
        //}
        //if (ddlState.SelectedIndex == 0)
        //{
        //    DisplayError = DisplayError + "--" + EComplianceResource.ErrState;
        //    Error = 1;
        //}
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    #endregion
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
    private void DocumentSave()
    {
        ActivityDocumentMsg ActivityDoc = new ActivityDocumentMsg();
        if (HidUpdateCount.Value == "1")
        {
            ActivityDoc.Flag = "U";
            ActivityDoc.ActivityDocumentId =Convert.ToInt32(txtActDocId.Text);
           
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
    protected void GrdDocument_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
       // GrdDocument.Enabled = false;
        //LoadGrdDocumentMaster();
    }
    //protected void GrdDocument_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    //UpdateIndex = e.RowIndex;
    //    //String jsScript = "";
    //    ////Asking a alert Message to Update or not
    //    //jsScript += "var answer=confirm(\'" + "Are you sure, You want to Update?" + "\');\n";
    //    //jsScript += "if (answer){\n";
    //    ////If answer is OK then Updating the HiddenField(control Available) HidUpdateCount as '1' and calling the btnSave_Click to call the Method CompanyUpdate
    //    //jsScript += "document.getElementById(\"ContentPlaceHolder1_HidUpdateCount\").value='" + "1" + "';\n";
    //    //jsScript += "document.getElementById(\"ContentPlaceHolder1_btnSave\").click();\n";
    //    //jsScript += "}\n";
    //    //jsScript += "else{\n";
    //    ////If answer is CANCEL then Updating the HiddenField(control Available) HidUpdateCount as '0' 
    //    //jsScript += "document.getElementById(\"ContentPlaceHolder1_HidUpdateCount\").value='" + "0" + "';\n";
    //    //jsScript += "}\n";
    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", jsScript, true);
    //}
    //protected void GrdDocument_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    GrdCompActivities.EditIndex = -1;
    //    LoadGrdDocumentMaster();
    //}
    //protected void GrdDocument_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    DeleteIndex = e.RowIndex;
    //    String jsScript = "";
    //    //Asking a alert Message to Delete or not
    //    jsScript += "var answer=confirm(\'" + "Are you sure, You want to Delete?" + "\');\n";
    //    jsScript += "if (answer){\n";
    //    //If answer is OK then Updating the HiddenField(control Available) HidDeleteCount as '1' and calling the btnSave_Click to call the Method CompanyDelete
    //    jsScript += "document.getElementById(\"ContentPlaceHolder1_HidDeleteCount\").value='" + "1" + "';\n";
    //    jsScript += "document.getElementById(\"ContentPlaceHolder1_btnSave\").click();\n";
    //    jsScript += "}\n";
    //    jsScript += "else{\n";
    //    //If answer is CANCEL then Updating the HiddenField(control Available) HidDeleteCount as '0' 
    //    jsScript += "document.getElementById(\"ContentPlaceHolder1_HidDeleteCount\").value='" + "0" + "';\n";
    //    jsScript += "}\n";
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", jsScript, true);
    //}
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
    protected void btnCompActivitiesHide_Click(object sender, EventArgs e)
    {
        if (btnCompActivitiesHide.Text == "- Collapse")
        {
            pnlGrdCompActivities.Visible = false;
            btnCompActivitiesHide.Text = "+ Expand";
        }
        else
        {
            //LoadCalendar(1, 200, 200);
            pnlGrdCompActivities.Visible = true;
            btnCompActivitiesHide.Text = "- Collapse";

        }
    }
}
