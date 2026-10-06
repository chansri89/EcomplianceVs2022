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
public partial class ActivityMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    // List<ActivityMasterMsg> ActivityList=new List<ActivityMasterMsg>();
    public static List<ActivityMasterMsg> ActivityList;
    List<ActMasterMsg> ActList = new List<ActMasterMsg>();
    List<DepartmentMsg> DeptList = new List<DepartmentMsg>();
    List<ActivityForCompanyMasterMsg> ActivityForCmpnyList = new List<ActivityForCompanyMasterMsg>();
    List<FrequencyMasterMsg> FncyList = new List<FrequencyMasterMsg>();
    List<CategoryMasterMsg> CtgryList = new List<CategoryMasterMsg>();
    public static HttpPostedFile PostedFile;
    public static List<LocationDepartmentMasterMsg> LocationDeptMasterList = new List<LocationDepartmentMasterMsg>();
    List<SeverityMasterMsg> SvrtyList = new List<SeverityMasterMsg>();
    public static int DeleteIndex = 0;
    public static int UpdateIndex = 0;
    UserAccess user = new UserAccess();
    public static string ProgramName = string.Empty;
    BaseClass BaseMsg = new BaseClass();
    string Docfilepath = ConfigurationManager.AppSettings["DocFolderPath"].ToString();
    public static string filename=string.Empty;
    public static string EditActivity;
    public static string FileAccessPath;
   public static int EditActivityIndex=-1;
   public static string FileChangedFlag = "Y";

    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
        {
            LoadFilterAct();
            if (ActList.Count > 0)
            {
                pnlActivityLoad.Visible = true;
            }
            else
            {
                pnlActivityLoad.Visible = false;
            }
            LoadGrdActivityMaster();
            LoadDepartmentName();
            LoadCategoryName();
            LoadSeverityName();
            LoadDocumentType();
            LoadFrequencyDropDowns();
            btnContinue.Visible = false;
            btnClear.Visible = false;
            if (ActivityList.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.AddActMsg + "');", true);
                Pnlgv.Visible = false;
                pnlAdd.Visible = true;
                pnlACt.Visible = false;
                pnlNew.Visible = false;
                //Response.Redirect("ActMaster.aspx");

            }
        }
        //pnlNew.Visible = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //filename = Path.GetFileName(fluAccessPath.FileName);
        //txtDocumentName.Text = filename;
        FileUpload file = new FileUpload();
        
        if (filename != string.Empty)
        {
           
            txtExistingPath.Text = Docfilepath;
        }
        else
        {
           
            txtExistingPath.Text = FileAccessPath;
        }
       
        if (HidUpdateCount.Value == "1" && EditActivityIndex >= 0)
        {
           
        }
        else if (HidUpdateCount.Value == "0" && EditActivityIndex >= 0) 
        {
            txtExistingPath.Text = "";
            EditActivityIndex = -1;
            return;
        }
        ///For Deleting a Record from Grid-Ends
         if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.CreatePermissionRestricted + "');", true);
        }
        else
        {
            if (IsValidSave() == 0 && IsValidFrequency()==0)
            {
                ActivitySave();
            }
        }
    }
    //protected void lnkForm_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        LinkButton lnkForm = (LinkButton)sender;
    //        if (File.Exists(@"D:\" + lnkForm.Text))
    //        {
    //            Response.ContentType = "application/txt";
    //            Response.AddHeader("content-disposition", "attachment; filename=" + lnkForm.Text);
    //            Response.WriteFile(@"D:\" + lnkForm.Text);
    //            Response.End();
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Could not find file " + lnkForm.Text + "');", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Access denied" + "');", true);
    //        Response.End();
    //    }
    //}    
    protected void btnExit_Click(object sender, EventArgs e)
    {
        AllClear();
        btnSave.Text = "Save";
        chkIsActive.Visible = false;
        GrdActivityMaster.Enabled = true;
        tblSave.Visible = false;
        tblContinue.Visible = true;
        btnClear.Enabled = false;
        PnlAssignResponsibleGroup.Visible = false;
        Pnlfrequency.Visible = false;
        btnBack.Visible = false;
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        GrdActivityMaster.Visible = true;
        LoadGrdActivityMaster();
        GrdActivityMaster.Enabled = true;
        pnlACt.Visible = false;
        AllClear();
        PnlAssignResponsibleGroup.Visible = false;
        Pnlfrequency.Visible = false;
        btnSave.Visible = false;
        btnExit.Visible = false;
        chkIsActive.Visible = false;
        lblActivityNamefilter.Visible = true;
        txtActivityNamefilter.Visible = true;
        btnSearch.Visible = true;
        btnSearchClear.Visible = true;
        lblActivityNamefilter.Enabled = true;
        txtActivityNamefilter.Enabled = true;
        btnSearch.Enabled = true;
        btnSearchClear.Enabled = true;
        txtActivityNamefilter.Text = "";
    }
    protected void ddlDepartmentName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LocationDepartmentMasterMsg Location=new LocationDepartmentMasterMsg();
        //if (ddlDepartmentName.SelectedIndex == 0)
        //{
        //    Location.DepartmentId=Convert.ToInt32(ddlDepartmentName.SelectedValue);
        //    List<LocationDepartmentMasterMsg> LocationDeptMasterList = Bus.SelectAssigneeMatrix(Location);
        //    LoadLocationDeptGrid(LocationDeptMasterList);
        //}
        //else
        //{
        //    LoadLocationDeptGrid(null);
        //}
    }
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        btnSave.Visible = false;
        btnExit.Visible = false;
        if (IsValidSave() == 0)
        {
            if (fluAccessPath.FileName != string.Empty)
            {
                filename = fluAccessPath.FileName;
                txtDocumentName.Text = fluAccessPath.FileName;
                PostedFile = fluAccessPath.PostedFile;
                FileChangedFlag = "Y";
            }
            else
            {
                filename = txtDocumentName.Text;
                FileChangedFlag = "N";
            }
            LoadResponsibleGroups(txtActivityId.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtActivityId.Text.Trim()));
            if (LocationDeptMasterList != null && LocationDeptMasterList.Count > 0)
            {
                //fluAccessPath.Enabled = false;
                pnlNew.Enabled = false;
                Pnlfrequency.Visible = true;
                if ((txtActivityId.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtActivityId.Text.Trim())) > 0)
                {
                    LoadFrequencyForActivity(txtActivityId.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtActivityId.Text.Trim()));
                }
                tblSave.Visible = true;
                btnContinue.Enabled = false;
                btnClear.Enabled = true;
                btnSave.Visible = true;
                btnExit.Visible = true;
                lblActivityNamefilter.Enabled = false;
                txtActivityNamefilter.Enabled = false;
                btnSearch.Enabled = false;
                btnSearchClear.Enabled = false;
            }
            else
            {
                btnClear.Enabled = false;
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Please Create the Resposible Group for the Department you Selected" + "');", true);
            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        pnlNew.Enabled = true;
        Pnlfrequency.Visible = false;
        tblSave.Visible = false;
        //fluAccessPath.Enabled = true;
        btnContinue.Enabled = true;
        PnlAssignResponsibleGroup.Visible = false;
        btnClear.Enabled = false;
    }
    #endregion
    #region Methods
    public void LoadGrdActivityMaster() 
    {
        ActivityMasterMsg Activity = new ActivityMasterMsg();
        Activity.Flag = "R";
        Activity.EmployeeCode = BaseMsg.EmployeeCode;
        Activity.ActId = Convert.ToInt32(ddlLoadActivity.SelectedValue); 
        ActivityList = Bus.MasActivitiesInsertUpdateandDelete(Activity);
        LoadActivityGrid(ActivityList);
        //GrdActivityMaster.DataSource = "";
        //GrdActivityMaster.DataSource = ActivityList;
        //GrdActivityMaster.DataBind();
        //if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        //{
        //    GrdActivityMaster.Columns[GrdActivityMaster.Columns.Count - 2].Visible = false;
        //}
        //if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        //{
        //    GrdActivityMaster.Columns[GrdActivityMaster.Columns.Count - 1].Visible = false;
        //}
    }
    private void ActivitySave()
    {
        ActivityMasterMsg Activity = new ActivityMasterMsg();

        if (txtActivityId.Text.Trim() != "0")
        {
            Activity.Flag = "U";
            Activity.ActivityId = Convert.ToInt32(txtActivityId.Text.Trim());
            HidUpdateCount.Value = "0";
            btnSave.Text = "Save";
            btnExit.Visible = false;
            GrdActivityMaster.Enabled = true;

        }
        else
        {
            //HidUpdateCount.Value = "0";
            Activity.Flag = "I";
            Activity.ActivityId = 0;
            //Activity.IsActive = chkIsActive.Checked;
            txtActivityDocumentId.Text = "0";//Added by Sathish: To insert the new document for the activity which is inserted newly
           
        }
        Activity.EmployeeCode = BaseMsg.EmployeeCode;
        Activity.ActName = txtActName.Text.Trim();
        Activity.Chapter = txtChapter.Text.Trim();
        Activity.Head = txtHead.Text.Trim();
        Activity.Section = txtSection.Text.Trim();
        Activity.ActRule = txtActRule.Text.Trim();
        Activity.ActDtlId = Convert.ToInt32(txtActDtlId.Text.Trim());
       // Activity.ActivityId = Convert.ToInt32(txtActivityId.Text);
        Activity.ActivityName = txtActivityName.Text.Trim();
        Activity.DepartmentId = Convert.ToInt32(ddlDepartmentName.SelectedItem.Value);
        //string Path = Docfilepath + txtActivityId.Text + "\\";
        Activity.AccessPath = txtExistingPath.Text.Trim(); //Docfilepath;
        Activity.ActivityDocumentId = Convert.ToInt32(txtActivityDocumentId.Text.Trim());
        if (Activity.ActivityDocumentId == 0)
        {
            Activity.DocumentFlag = "I";
        }
        else
        {
            Activity.DocumentFlag = "U";
        }
        if (filename != "")
        {
            Activity.DocumentName = filename;//txtDocumentName.Text;  //
        }
        else
        {
            Activity.DocumentName = txtDocumentName.Text.Trim();//txtDocumentName.Text;  //
        }
        Activity.EmployeeCode = BaseMsg.EmployeeCode;
        Activity.DocumentTypeId = Convert.ToInt32(ddlDocType.SelectedValue);
        Activity.CategoryId =Convert.ToInt32(ddlCategoryName.SelectedItem.Value);
        Activity.SeverityId= Convert.ToInt32(ddlSeverityName.SelectedItem.Value);
        Activity.NonComplianceTaskAvailableFlag = Convert.ToChar(txtNonComplianceTaskAvailableFlag.Text.Trim());
        Activity.IsStateSpecific = chkStateSpecific.Checked;
        Activity.IsLocationSpecific = chkLocationSpecific.Checked;
        Activity.IsRegularActivity = chkRegularActivity.Checked;
        Activity.IsActive = chkIsActive.Checked;
        Activity.CreatedBy = BaseMsg.EmployeeCode;
        //string FileDetails = (Activity.ActivityId.ToString() + "-");
        //FileUpload(Path, filename, FileDetails);
        ActivityList = Bus.MasActivitiesInsertUpdateandDelete(Activity);
        //Output Dispay
        foreach (ActivityMasterMsg ActivitySave in ActivityList)
        {
            if (ActivitySave.ActivitiesResult == "0")
            {
                string Path = Docfilepath + "\\" + ActivitySave.LastActivityId.ToString() + "\\";
                if (!filename.Contains(ActivitySave.LastDocumentId.ToString()))
                {
                    filename = ActivitySave.LastDocumentId.ToString() + " - " + filename;
                    //FileUploadFun(Path, filename);
                }
                if (FileChangedFlag == "Y")
                {
                    FileUploadFun(Path, filename);
                }
               // FileUploadFun(Path, filename);
                SaveRepsonsibleGroup(ActivitySave.LastActivityId);
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.SuccessFullySaved + "');", true);
                AllClear();
                //LoadGrdActivityMaster();
                if (txtActivityNamefilter.Text.Trim() == "")
                {
                    LoadGrdActivityMaster();
                }
                else
                {
                    LoadFiterGrid();
                }
                //LoadFiterGrid();
                GrdActivityMaster.Enabled = true;
                pnlNew.Enabled = true;
                Pnlfrequency.Visible = false;
                tblSave.Visible = false;
                PnlAssignResponsibleGroup.Visible = false;
                //fluAccessPath.Enabled = true;
                btnClear.Enabled = false;
                btnContinue.Enabled = false;
                pnlACt.Visible = false;
                PostedFile = null;
                btnBack.Visible = false;
                lblActivityNamefilter.Visible = true;
                txtActivityNamefilter.Visible = true;
                btnSearch.Visible = true;
                btnSearchClear.Visible = true;
                txtActivityNamefilter.Enabled = false;
                lblActivityNamefilter.Enabled = false;
                btnSearch.Enabled = false;
                btnSearchClear.Enabled = true;
                Pnlgv.Visible = true;
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + ActivitySave.ActivitiesResult + "');", true);
                break;
            }
        }
    }
    public bool SaveRepsonsibleGroup(int ActivityID)
    {
        bool Result = true;
        foreach (GridViewRow gvr in grdAssignResponsibleGroup.Rows)
        {
            if (Result)
            {
                ActivityForCompanyMasterMsg ActivityCompany = new ActivityForCompanyMasterMsg();
                ActivityCompany.ActivityId = ActivityID;
                ActivityCompany.CompanyActivityId = Convert.ToInt32(((Label)gvr.FindControl("lblCompanyActivityId")).Text.Trim());
                ActivityCompany.ActivityName = txtActivityName.Text.Trim();
                ActivityCompany.CompanyCode = ((Label)gvr.FindControl("lblCompanyCode")).Text.Trim();
                ActivityCompany.DueMonth = Convert.ToInt32(ddldueMonth.SelectedValue);
                string Duedate = ddlDueDate.SelectedItem.Text.Trim();
                if (ddlDueDate.SelectedItem.Text == "NA")
                {
                    Duedate = "0";
                }
                //ActivityCompany.DueDate = Convert.ToInt32(ddlduedate.SelectedItem.Text);
                ActivityCompany.DueDate = Convert.ToInt32(Duedate);
                ActivityCompany.DueDay = ddlDueDay.SelectedItem.Text.Trim();
                ActivityCompany.TriggerMonth = Convert.ToInt32(ddlTiggerMonth.SelectedValue);
                string TriggDate = ddlTriggerDate.SelectedItem.Text.Trim();
                if (ddlTriggerDate.SelectedItem.Text.Trim() == "NA")
                {
                    TriggDate = "0";
                }
                //ActivityCompany.TriggerDate = Convert.ToInt32(ddltrigdate.SelectedItem.Text);
                ActivityCompany.TriggerDate = Convert.ToInt32(TriggDate);
                ActivityCompany.TriggerDay = ddlTriggerDay.SelectedItem.Text.Trim();
                ActivityCompany.ExecutionEmployeeCode = "0";
                ActivityCompany.ReviewEmployeeCode = "0";
                ActivityCompany.HeadEmployeeCode = "0";
                ActivityCompany.UltimateEmployeeCode = "0";
                ActivityCompany.FrequencyId = Convert.ToInt32(ddlFrequency.SelectedItem.Value);
                ActivityCompany.FrqRemarks = txtFrqRemarks.Text.Trim();//added by Abinayaa 161112
                ActivityCompany.LocationDepartmentId = Convert.ToInt32(((Label)gvr.FindControl("lblLocationDeptId")).Text.Trim());
                //if (ddlFrequency.SelectedItem.Text == "As and When")
                //{
                //    if (txtFrqRemarks.Text == string.Empty)
                //    {
                //        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ErrFrqRemarks + "');", true);
                //        return;
                //    }
                //}
                ActivityCompany.IsActive = ((CheckBox)gvr.FindControl("chkLocationSpec")).Checked;
                ActivityCompany.CreatedBy = BaseMsg.EmployeeCode;
                Result = Bus.MasActivityForCompanyMasterInsertandUpdate(ActivityCompany);
            }
            else
            {
                break;
            }
        }
        return Result;
    }
    public void ActivityUpdate()
    {
        GridViewRow row = GrdActivityMaster.Rows[UpdateIndex];
        ActivityMasterMsg Activity = new ActivityMasterMsg();
        Activity.Flag = "U";

        TextBox txtActName = (TextBox)row.FindControl("txtActName");
        TextBox txtActivityId = (TextBox)row.FindControl("txtActivityId");
        TextBox txtActDtlId = (TextBox)row.FindControl("txtActDtlId");
        //DropDownList ddlactname = (DropDownList)row.FindControl("ddlActName");
        TextBox txtactivityname = (TextBox)row.FindControl("txtActivityName");
        DropDownList ddlDepname = (DropDownList)row.FindControl("ddlDepartName");
        DropDownList ddlcatgname = (DropDownList)row.FindControl("ddlCategName");
        DropDownList ddlsevtyname = (DropDownList)row.FindControl("ddlSevtyName");
        TextBox txtNonCompTaskAvable = (TextBox)row.FindControl("txtNonComplianceTaskAvailableFlag");
        CheckBox chkStateSpecific = (CheckBox)row.FindControl("chkStateSpecific");
        CheckBox chkLocationSpecific = (CheckBox)row.FindControl("chkLocationSpecific");
        CheckBox chkregularactivity = (CheckBox)row.FindControl("chkRegActivity");
        CheckBox chkActive = (CheckBox)row.FindControl("chkIsActive");

        Activity.ActName = txtActName.Text.Trim();
        Activity.ActDtlId = Convert.ToInt32(txtActDtlId.Text.Trim());
        Activity.ActivityId = Convert.ToInt32(txtActivityId.Text.Trim());
        Activity.ActivityName = txtactivityname.Text.Trim();
        Activity.DepartmentId = Convert.ToInt32(ddlDepname.SelectedItem.Value);
        Activity.CategoryId = Convert.ToInt32(ddlcatgname.SelectedItem.Value);
        Activity.SeverityId = Convert.ToInt32(ddlsevtyname.SelectedItem.Value);
        Activity.NonComplianceTaskAvailableFlag = Convert.ToChar(txtNonCompTaskAvable.Text.Trim());
        Activity.IsStateSpecific = Convert.ToBoolean(chkStateSpecific.Checked);
        Activity.IsLocationSpecific = Convert.ToBoolean(chkLocationSpecific.Checked);
        Activity.IsRegularActivity =  Convert.ToBoolean(chkregularactivity.Checked);
        Activity.IsActive = chkIsActive.Checked;
        Activity.CreatedBy = BaseMsg.EmployeeCode;
        ActivityList = Bus.MasActivitiesInsertUpdateandDelete(Activity);
        GrdActivityMaster.EditIndex = -1;

        foreach (ActivityMasterMsg ActivityUpdate in ActivityList)
        {
            if (ActivityUpdate.ActivitiesResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ActivityUpdatedSuccessfully + "');", true);

                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + ActivityUpdate.ActivitiesResult + "');", true);
                break;
            }
        }
    }    
    public List<ActivityMasterMsg> getActName()
    {
        int ActId = 0;//For Dummy Purpose
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        EmpMsg.EmployeeCode = BaseMsg.EmployeeCode;
        ActivityList = Bus.ActivityMasterSelect(EmpMsg,ActId);
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
    //public void FileUploadFun(string Path, string FileName, string FileDetails)
    //{
    //    if (!Directory.Exists(Path))
    //    {
    //        Directory.CreateDirectory(Path);
    //    }
    //    if (File.Exists(Path + FileDetails + filename))
    //    {
    //        File.Move(Path + FileDetails + filename, Path + FileDetails + filename + "_old");
    //        fluAccessPath.SaveAs(Path + FileDetails + filename);
    //        File.Delete(Path + FileDetails + filename + "_old");
    //    }
    //    fluAccessPath.SaveAs(Path + FileDetails + filename);
    //}
    public void FileUploadFun(string Path, string FileName)
    {
        // If the directory does not exist means creating a new directory else saving the file.
        if (!Directory.Exists(Path))
        {
            Directory.CreateDirectory(Path);
        }
        if (!File.Exists(Path + filename))
        {
            PostedFile.SaveAs(Path + filename);
        }
        // if the file exist means renaming the old file and and uploading the new file and deleted the old file else Uploading the file
        else
        {
            if (PostedFile != null)
            {
                File.Move(Path + filename, Path + filename + "_old");
                PostedFile.SaveAs(Path + filename);
                File.Delete(Path + filename + "_old");
            }
        }
        //// if the file exist means renaming the old file and and uploading the new file and deleted the old file else Uploading the file
        //if (File.Exists(Path + filename))
        //{
        //    File.Move(Path +  filename, Path + filename + "_old");
        //    PostedFile.SaveAs(Path + filename);
        //    File.Delete(Path +  filename + "_old");
        //}
        //PostedFile.SaveAs(Path + filename);
    }
    public void FileDownload(string Path, string FileName,string OrgFileName)
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
    private void LoadFilterAct()
    {
        //List<ActMasterMsg> ActList = new List<ActMasterMsg>();
        //var ddlActList = (from ActName in ActList
        //                  where ActName.IsActive == true
        //                  select new { ActName.ActName, ActName.ActId }).Distinct().ToList();
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        ActList = Bus.ActMasterSelect(Emp);
        var ddlActList = (from Act in ActList
                          where Act.IsActive == true
                          select new { Act.ActName, Act.ActId }).Distinct().ToList();
        ddlLoadActivity.DataSource = ddlActList;
        ddlLoadActivity.DataTextField = "ActName";
        ddlLoadActivity.DataValueField = "ActId";
        ddlLoadActivity.DataBind();
        ddlLoadActivity.Items.Insert(0, new ListItem("--All--", "0"));
    }
    public int ActivityId { get; set; }
    public void LoadResponsibleGroups(int ActivityId)
    {
        LocationDepartmentMasterMsg Location = new LocationDepartmentMasterMsg();
        Location.DepartmentId = Convert.ToInt32(ddlDepartmentName.SelectedValue);
        Location.ActivityId = ActivityId;
        Location.EmployeeCode = BaseMsg.EmployeeCode; ;
        LocationDeptMasterList = Bus.SelectAssigneeMatrix(Location);
        LoadLocationDeptGrid(LocationDeptMasterList);
    }
    public void LoadLocationDeptGrid(List<LocationDepartmentMasterMsg> LocationDeptMasterList)
    {
        if (LocationDeptMasterList != null && LocationDeptMasterList.Count > 0)
        {
            grdAssignResponsibleGroup.DataSource = LocationDeptMasterList;
            grdAssignResponsibleGroup.DataBind();
            PnlAssignResponsibleGroup.Visible = true;
        }
        else
        {
            grdAssignResponsibleGroup.DataSource = null;
            grdAssignResponsibleGroup.DataBind();
            PnlAssignResponsibleGroup.Visible = false;
        }
    }
    public void LoadFrequencyForActivity(int ActivityID)
    {
        ActivityForCompanyMasterMsg AcivityCompany = new ActivityForCompanyMasterMsg();
        AcivityCompany.ActivityId = ActivityID;
        ActivityForCompanyMasterMsg AcivityCompanyResult = new ActivityForCompanyMasterMsg();
        AcivityCompanyResult = Bus.ActivityForCompanyFreqeuncyMasterSelect(AcivityCompany);
        if (AcivityCompanyResult != null)
        {
            ddlFrequency.SelectedValue = AcivityCompanyResult.FrequencyId.ToString();
            txtFrqRemarks.Text = AcivityCompanyResult.FrqRemarks;
            ddlDueDate.SelectedValue = AcivityCompanyResult.DueDate.ToString();
            ddldueMonth.SelectedValue = AcivityCompanyResult.DueMonth.ToString();
            ddlTiggerMonth.SelectedValue = AcivityCompanyResult.TriggerMonth.ToString();
            ddlTriggerDate.SelectedValue = AcivityCompanyResult.TriggerDate.ToString();
            DataTable dtDays = LoadDays();
            foreach (DataRow dr in dtDays.Rows)
            {
                if (dr["Day"].ToString() == AcivityCompanyResult.TriggerDay)
                {
                    ddlTriggerDay.SelectedValue = dr["Number"].ToString();
                    break;
                }
            }
            foreach (DataRow dr in dtDays.Rows)
            {
                if (dr["Day"].ToString() == AcivityCompanyResult.DueDay)
                {
                    ddlDueDay.SelectedValue = dr["Number"].ToString();
                    break;
                }
            }
        }
    }
    #endregion
    #region Clear
    public void AllClear()
    {
        txtActivityName.Text = "";
        txtActName.Text = "";
        txtActivityName.Text = "";
        txtChapter.Text = "";
        txtHead.Text = "";
        txtSection.Text = "";
        txtActRule.Text = "";
        txtExistingPath.Text = "";
        txtDocumentName.Text = "";
        ddlDepartmentName.SelectedIndex = 0;
        ddlCategoryName.SelectedIndex = 0;
        ddlSeverityName.SelectedIndex = 0;
        ddlDocType.SelectedIndex = 0;
        txtNonComplianceTaskAvailableFlag.Text = "N";
        chkRegularActivity.Checked = false;
        chkStateSpecific.Checked = false;
        chkLocationSpecific.Checked = false;
        chkIsActive.Checked = false;
        txtActivityId.Text = string.Empty;
        txtActDtlId.Text = "";
        ddlDueDate.SelectedIndex = 0;
        txtFrqRemarks.Text = string.Empty;
        ddldueMonth.SelectedIndex = 0;
        ddlFrequency.SelectedIndex = 0;
        ddlDueDay.SelectedIndex = 0;
        ddlTriggerDate.SelectedIndex = 0;
        ddlTriggerDay.SelectedIndex = 0;
        ddlTiggerMonth.SelectedIndex = 0;
    }
    public void Clear()
    {
        txtActivityName.Text = "";
        txtExistingPath.Text = "";
        txtDocumentName.Text = "";
        ddlDepartmentName.SelectedIndex = 0;
        ddlCategoryName.SelectedIndex = 0;
        ddlSeverityName.SelectedIndex = 0;
        ddlDocType.SelectedIndex = 0;
        txtNonComplianceTaskAvailableFlag.Text = "N";
        chkRegularActivity.Checked = false;
        chkStateSpecific.Checked = false;
        chkLocationSpecific.Checked = false;
        chkIsActive.Checked = false;
    }
    #endregion
    #region LoadDropDowns
    private void LoadDepartmentName()
    {
        //ActivityMasterMsg Activity = new ActivityMasterMsg();
        DepartmentMsg Dept = new DepartmentMsg();
        EmployeeMasterMsg emp = new EmployeeMasterMsg();
        DeptList = Bus.DepartmentMasterSelect(emp);
        DeptList = (from ActiveDept in DeptList
                    where ActiveDept.IsActive == true
                    select ActiveDept).ToList();
        ddlDepartmentName.DataTextField = "DepartmentName";
        ddlDepartmentName.DataValueField = "DepartmentId";
        ddlDepartmentName.DataSource = DeptList;
        ddlDepartmentName.DataBind();
        ddlDepartmentName.Items.Insert(0,new ListItem("-- Select Please --","0"));
    }
    private void LoadFrequencyDropDowns()
    {
        //ActivityMasterMsg Activity = new ActivityMasterMsg();

        ddlFrequency.DataTextField = "FrequencyName";
        ddlFrequency.DataValueField = "FrequencyId";
        ddlFrequency.DataSource = (from Frequency in getFrequency()
                                            where Frequency.IsActive==true
                                            select Frequency).ToList();
        ddlFrequency.DataBind();
        ddlFrequency.Items.Insert(0, new ListItem("-- Select Please --", "0"));
        ddlFrequency.SelectedIndex = 0;

        ddlDueDate.DataSource = LoadDate();
        ddlDueDate.DataBind();
        ddlDueDay.DataSource = LoadDays();
        ddlDueDay.DataBind();
        ddldueMonth.DataSource = LoadMonth();
        ddldueMonth.DataBind();
        ddlTriggerDate.DataSource = LoadDate();
        ddlTriggerDate.DataBind();
        ddlTriggerDay.DataSource = LoadDays();
        ddlTriggerDay.DataBind();
        ddlTiggerMonth.DataSource = LoadMonth();
        ddlTiggerMonth.DataBind();
    }
    private void LoadCategoryName()
    {
        EmployeeMasterMsg emp = new EmployeeMasterMsg();
        CategoryMasterMsg Catgy = new CategoryMasterMsg();

        CtgryList = Bus.CategoryMasterSelect(emp);
        CtgryList = (from ActiveCtgry in CtgryList
                     where ActiveCtgry.IsActive == true
                     select ActiveCtgry).ToList();
        ddlCategoryName.DataSource = CtgryList;
        ddlCategoryName.DataTextField = "CategoryName";
        ddlCategoryName.DataValueField = "CategoryId";
        ddlCategoryName.DataBind();
        ddlCategoryName.Items.Insert(0, new ListItem("-- Select Please --","0"));
    }
    private void LoadSeverityName()
    {
        EmployeeMasterMsg emp = new EmployeeMasterMsg();
        SeverityMasterMsg Sevty = new SeverityMasterMsg();
        SvrtyList = Bus.SeverityMasterSelect(emp);
        SvrtyList = (from ActiveSvrty in SvrtyList
                     where ActiveSvrty.IsActive == true
                     select ActiveSvrty).ToList();
        ddlSeverityName.DataSource = SvrtyList;
        ddlSeverityName.DataTextField = "SeverityShortName";
        ddlSeverityName.DataValueField = "SeverityId";
        ddlSeverityName.DataBind();
        ddlSeverityName.Items.Insert(0, new ListItem("-- Select Please --","0"));
    }
    public void LoadDocumentType()
    {

        DocumentTypeMasterMsg Doc = new DocumentTypeMasterMsg();
        List<DocumentTypeMasterMsg> DocumentTypeMastList = Bus.DocumentTypeMasterSelectSp();
        DocumentTypeMastList = (from ActiveDocType in DocumentTypeMastList
                                where ActiveDocType.IsActive == true
                                select ActiveDocType).ToList();
        ddlDocType.DataTextField = "DocumentTypeName";
        ddlDocType.DataValueField = "DocumentTypeId";
        ddlDocType.DataSource = DocumentTypeMastList;
        ddlDocType.DataBind();
        ddlDocType.Items.Insert(0, new ListItem("-- Select Please --", "0"));
    }//Added for Document Master
     private void LoadFiterGrid()
    {
        List<ActivityMasterMsg> ActivityListFilter = new List<ActivityMasterMsg>();

        ActivityListFilter = (from ActivityFilter in ActivityList
                              where ActivityFilter.ActivityName.ToLower().Contains(txtActivityNamefilter.Text.ToLower().Trim())
                              select ActivityFilter).ToList();
        if (ActivityListFilter == null || ActivityListFilter.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ErrGrdSelect + "');", true);
            //txtGrdname.Text = "";
            LoadActivityGrid(ActivityList);
        }
        else
        {
            LoadActivityGrid(ActivityListFilter);
        }

    }
     private void LoadActivityGrid(List<ActivityMasterMsg> ActivityList)
     {
         GrdActivityMaster.DataSource = "";
         GrdActivityMaster.DataSource = ActivityList;
         GrdActivityMaster.DataBind();
         if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
         {
             GrdActivityMaster.Columns[GrdActivityMaster.Columns.Count - 2].Visible = false;
         }
         if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
         {
             GrdActivityMaster.Columns[GrdActivityMaster.Columns.Count - 1].Visible = false;
         }
     }
#endregion
    #region Validation
    private int IsValidSave()
    {
        int Error = 0;
        string DisplayError = "";

        if (txtActDtlId.Text.Trim() == "" || Convert.ToInt32(txtActDtlId.Text.Length.ToString().Trim()) == 0 && Convert.ToInt32(txtActDtlId.Text) == 0)
        {
            DisplayError = DisplayError + EComplianceResource.ErrGridActName;
            Error = 1;
        }
        if (txtActivityName.Text.Trim() == "" || Convert.ToInt32(txtActivityName.Text.Length.ToString().Trim()) == 0)
        {
            DisplayError = DisplayError + "--" + EComplianceResource.ErrActivityName;
            Error = 1;
        }
        if (ddlDepartmentName.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "--" + EComplianceResource.ErrDepartmentNameselect;
            Error = 1;
        }
        if (ddlCategoryName.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "--" + EComplianceResource.ErrCategoryNameselect;
            Error = 1;
        }
        if (ddlSeverityName.SelectedIndex == 0)
        {

            DisplayError = DisplayError + "--" + EComplianceResource.ErrSeverityNameSelect;
            Error = 1;
        }
        //if (txtDocumentName.Text.Trim() == "" && fluAccessPath.FileName== "")// || txtDocumentName.Text.Length.ToString().Trim() == "0")
        //{
        //    DisplayError = DisplayError +"--"+ EComplianceResource.ErrDocumentName;
        //    Error = 1;
        //}
        //if (ddlDocType.SelectedIndex == 0)
        //{
        //    DisplayError = DisplayError + "--" + EComplianceResource.ErrSelectDocumentType;
        //    Error = 1;
        //}

        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    #endregion
    #region GrdEdit
    //protected void GrdActivityMaster_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    //GrdActivityMaster.EditIndex = e.NewEditIndex;
    //    //GridViewRow row = GrdActivityMaster.Rows[GrdActivityMaster.EditIndex];
    //    //Label lblActName = (Label)row.FindControl("lblActName");
    //    //Label lblDeparmentName = (Label)row.FindControl("lblDepartName");
    //    //Label lblCategoryName = (Label)row.FindControl("lblCategName");
    //    //Label lblSeverityName = (Label)row.FindControl("lblSevtyName");

    //    //ActivityMasterMsg Activity = new ActivityMasterMsg();
    //    //LoadGrdActivityMaster();
    //    //foreach (GridViewRow gvr in GrdActivityMaster.Rows)
    //    //{
    //    //    if (gvr.RowIndex == GrdActivityMaster.EditIndex)
    //    //    {
    //    //        DropDownList ddlActname = (DropDownList)gvr.FindControl("ddlActName");

    //    //        //foreach (ActivityMasterMsg ActivityEdit in ActivityList)
    //    //        //{
    //    //        //    if (ActivityEdit.ActivityName == lblActName.Text)
    //    //        //    {
    //    //        //        ddlActName.SelectedValue = Convert.ToString(ActivityEdit.ActivityId);
    //    //        //        break;
    //    //        //    }
    //    //        //    ddlActName.SelectedIndex = 0;
    //    //        //}
    //    //        DropDownList ddlDepartName = (DropDownList)gvr.FindControl("ddlDepartName");

    //    //        foreach (ActivityMasterMsg ActivityEdit in ActivityList)
    //    //        {
    //    //            if (ActivityEdit.DepartmentName == lblDeparmentName.Text)
    //    //            {
    //    //                ddlDepartName.SelectedValue = Convert.ToString(ActivityEdit.DepartmentId);
    //    //                break;
    //    //            }
    //    //            ddlDepartName.SelectedIndex = 0;
    //    //        }
    //    //        DropDownList ddlCategName = (DropDownList)gvr.FindControl("ddlCategName");

    //    //        foreach (ActivityMasterMsg ActivityEdit in ActivityList)
    //    //        {
    //    //            if (ActivityEdit.CategoryName == lblCategoryName.Text)
    //    //            {
    //    //                ddlCategName.SelectedValue = Convert.ToString(ActivityEdit.CategoryId);
    //    //                break;
    //    //            }
    //    //            ddlCategName.SelectedIndex = 0;
    //    //        }
    //    //        DropDownList ddlSevtyName = (DropDownList)gvr.FindControl("ddlSevtyName");

    //    //        foreach (ActivityMasterMsg ActivityEdit in ActivityList)
    //    //        {
    //    //            if (ActivityEdit.SeverityName == lblSeverityName.Text)
    //    //            {
    //    //                ddlSevtyName.SelectedValue = Convert.ToString(ActivityEdit.SeverityId);
    //    //                break;
    //    //            }
    //    //            ddlSevtyName.SelectedIndex = 0;
    //    //        }
    //    //    }
    //    //}
    //}
    //protected void GrdActivityMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    GrdActivityMaster.EditIndex = -1;
    //    LoadGrdActivityMaster();
    //}     
    protected void GrdActivityMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() != string.Empty)
        {
            int WRowIndex = Convert.ToInt32(e.CommandArgument.ToString());
            //since is a TemplateField we have to use "((Label)row.FindControl("lblActDtlId")).Text"
            if (e.CommandName == "Select")
            {
                btnSave.Text = "Save";
                Clear();
                GridViewRow row = GrdActivityMaster.Rows[WRowIndex];
                txtActivityId.Text = "0";
                txtActDtlId.Text = ((Label)row.FindControl("lblActDtlId")).Text.Trim();
                txtActName.Text = ((Label)row.FindControl("lnkActName")).Text.Trim();
                txtChapter.Text = ((Label)row.FindControl("lblChapter")).Text.Trim();
                txtHead.Text = ((Label)row.FindControl("lblHead")).Text.Trim();
                txtSection.Text = ((Label)row.FindControl("lblSection")).Text.Trim();
                txtActRule.Text = ((Label)row.FindControl("lblActRule")).Text.Trim();
                //txtActivityDocumentId.Text = ((Label)row.FindControl("lblActivityDocumentId")).Text; //Commented by Sathish OnClick of Select weare going to add new record for the document not update
                txtActivityDocumentId.Text = "0";
                pnlNew.Enabled = true;
                pnlACt.Visible = true;
                pnlNew.Visible = true;
                pnlACt.Visible = true;  
                btnClear.Enabled = false;
                btnContinue.Enabled = true;
                chkIsActive.Visible = false;
                GrdActivityMaster.Enabled = false;
                pnlNew.Enabled = true;
                btnBack.Visible = true;
                btnContinue.Visible = true;
                btnClear.Visible = true;
            }
            if (e.CommandName == "Activity_Edit")//Added for Single Document for One Activity
            {
                GridViewRow row = GrdActivityMaster.Rows[WRowIndex];

                Label lblAdministrator = (Label)row.FindControl("lblAdministrator");
                CheckBox chkActive = (CheckBox)row.FindControl("chkActive");
                //start-- added only Administrator can deactivate an activity 030913 abinayaa

                if (lblAdministrator.Text == "0" && chkActive.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Activity is disabled. Contact Administrator for Enabling" + "');", true);
                }
                else
                {
                    if (lblAdministrator.Text == "0" && chkActive.Checked == true)
                    {
                        pnlNew.Enabled = false;
                    }
                    if (lblAdministrator.Text != "0")
                    {
                        pnlNew.Enabled = true;
                    }
                //End-- added only Administrator can deactivate an activity 030913 abinayaa

                    btnExit.Visible = true;
                    GrdActivityMaster.Enabled = false;
                    btnSave.Text = "Update";
                    EditActivityIndex = WRowIndex;
                    //pnlNew.Enabled = true;
                    btnClear.Enabled = false;
                    btnContinue.Enabled = true;
                    btnContinue.Visible = true;
                    btnClear.Visible = true;
                    btnExit.Visible = false;
                    if (((Label)row.FindControl("lblActivityName")).Text.Trim() != string.Empty)
                    {
                        #region CommentedCodes
                        //GridViewRow row = GrdActivityMaster.Rows[WRowIndex];
                        //txtActName.Text = ((Label)row.FindControl("lnkActName")).Text;
                        //txtChapter.Text = ((Label)row.FindControl("lblChapter")).Text;
                        //txtHead.Text = ((Label)row.FindControl("lblHead")).Text;
                        //txtSection.Text = ((Label)row.FindControl("lblSection")).Text;
                        //txtActRule.Text = ((Label)row.FindControl("lblActRule")).Text;
                        //chkIsActive.Visible = true;

                        //txtActivityId.Text = ((Label)row.FindControl("lblActivityId")).Text;
                        //txtActDtlId.Text = ((Label)row.FindControl("lblActDtlId")).Text;
                        //txtActivityName.Text = ((Label)row.FindControl("lblActivityName")).Text;
                        //ddlDepartmentName.SelectedValue = ((Label)row.FindControl("lblDepartmentId")).Text;
                        //ddlCategoryName.SelectedValue = ((Label)row.FindControl("lblCategoryId")).Text;
                        //ddlSeverityName.SelectedValue = ((Label)row.FindControl("lblSeverityId")).Text;
                        //txtActivityDocumentId.Text = ((Label)row.FindControl("lblActivityDocumentId")).Text;
                        //txtDocumentName.Text = ((Label)row.FindControl("lblForm")).Text;
                        //txtExistingPath.Text = Docfilepath;
                        //ddlDocType.SelectedValue = ((Label)row.FindControl("lblDocumentTypeId")).Text;
                        //txtNonComplianceTaskAvailableFlag.Text = ((Label)row.FindControl("lblNonComplianceTaskAvailableFlag")).Text;
                        //chkRegularActivity.Checked = ((CheckBox)row.FindControl("chkRegularActivity")).Checked;
                        //chkStateSpecific.Checked = ((CheckBox)row.FindControl("chkStateSpec")).Checked;
                        //chkLocationSpecific.Checked = ((CheckBox)row.FindControl("chkLocationSpec")).Checked;
                        //chkIsActive.Checked = ((CheckBox)row.FindControl("chkActive")).Checked;
                        //btnSave.Text = "Update";
                        #endregion
                        //String jsScript = "";
                        ////Asking a alert Message to Update or not
                        //jsScript += "var answer=confirm(\'" + "Are you sure, You want to Edit?" + "\');\n";
                        //jsScript += "if (answer){\n";
                        ////If answer is OK then Updating the HiddenField(control Available) HidUpdateCount as '1' and calling the btnSave_Click to call the Method CompanyUpdate
                        //jsScript += "document.getElementById(\"ContentPlaceHolder1_HidUpdateCount\").value='" + "1" + "';\n";
                        //jsScript += "document.getElementById(\"ContentPlaceHolder1_btnSave\").click();\n";
                        //jsScript += "}\n";
                        //jsScript += "else{\n";
                        ////If answer is CANCEL then Updating the HiddenField(control Available) HidUpdateCount as '0' 
                        //jsScript += "document.getElementById(\"ContentPlaceHolder1_HidUpdateCount\").value='" + "0" + "';\n";
                        //jsScript += "document.getElementById(\"ContentPlaceHolder1_btnSave\").click();\n";
                        //jsScript += "}\n";
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", jsScript, true);


                        txtActName.Text = ((Label)row.FindControl("lnkActName")).Text.Trim();
                        txtChapter.Text = ((Label)row.FindControl("lblChapter")).Text.Trim();
                        txtHead.Text = ((Label)row.FindControl("lblHead")).Text.Trim();
                        txtSection.Text = ((Label)row.FindControl("lblSection")).Text.Trim();
                        txtActRule.Text = ((Label)row.FindControl("lblActRule")).Text.Trim();
                        //Label lblAdministrator = (Label)row.FindControl("lblAdministrator");
                     
                        chkIsActive.Visible = true;
                        txtActivityId.Text = ((Label)row.FindControl("lblActivityId")).Text.Trim();
                        txtActDtlId.Text = ((Label)row.FindControl("lblActDtlId")).Text.Trim();
                        txtActivityName.Text = ((Label)row.FindControl("lblActivityName")).Text.Trim();
                        ddlDepartmentName.SelectedValue = ((Label)row.FindControl("lblDepartmentId")).Text.Trim();
                        ddlCategoryName.SelectedValue = ((Label)row.FindControl("lblCategoryId")).Text.Trim();
                        ddlSeverityName.SelectedValue = ((Label)row.FindControl("lblSeverityId")).Text.Trim();
                        txtActivityDocumentId.Text = ((Label)row.FindControl("lblActivityDocumentId")).Text.Trim();
                        txtDocumentName.Text = ((Label)row.FindControl("lblForm")).Text.Trim();
                        //txtDocumentName.Text = ((LinkButton)row.FindControl("lnkForm")).Text;
                        string FilePath = Docfilepath;
                        string FileName = string.Empty;
                        if (((Label)row.FindControl("lblForm")).Text != string.Empty)
                        {
                            FilePath = FilePath + ((Label)row.FindControl("lblActivityId")).Text.Trim() + "\\";
                            FileName = ((Label)row.FindControl("lblActivityDocumentId")).Text.Trim() + " - " + ((Label)row.FindControl("lblForm")).Text.Trim();
                            FileAccessPath = @FilePath + FileName;
                            txtExistingPath.Text = FileAccessPath;
                        }
                        ddlDocType.SelectedValue = ((Label)row.FindControl("lblDocumentTypeId")).Text.Trim();
                        txtNonComplianceTaskAvailableFlag.Text = ((Label)row.FindControl("lblNonComplianceTaskAvailableFlag")).Text.Trim();
                        chkRegularActivity.Checked = ((CheckBox)row.FindControl("chkRegularActivity")).Checked;
                        chkStateSpecific.Checked = ((CheckBox)row.FindControl("chkStateSpec")).Checked;
                        chkLocationSpecific.Checked = ((CheckBox)row.FindControl("chkLocationSpec")).Checked;
                        chkIsActive.Checked = ((CheckBox)row.FindControl("chkActive")).Checked;
                        btnSave.Text = "Update";
                        EditActivityIndex = -1;
                        btnBack.Visible = true;

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "There is no Activity for this Act chapter,Head and Rule." + "');", true);
                        btnBack.Visible = true;
                    }
                }
            }

            if (e.CommandName.ToString() == "View")
            {
                string Path = Docfilepath;
                string FileName=string.Empty;
                GridViewRow row = GrdActivityMaster.Rows[WRowIndex];
                // if there is no form name then showing an msg No File to view
                if (((Label)row.FindControl("lblForm")).Text.Trim() != string.Empty)
                {
                    Path = Path + ((Label)row.FindControl("lblActivityId")).Text.Trim() + "\\";
                    FileName = ((Label)row.FindControl("lblActivityDocumentId")).Text.Trim() + " - " + ((Label)row.FindControl("lblForm")).Text.Trim();
                    //Calling a method to file download.
                    FileDownload(Path, FileName, ((Label)row.FindControl("lblForm")).Text.Trim());
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "No File to View" + "');", true);
                }
            }
        }
    }
    #endregion             
    #region Frequency
    public int IsValidFrequency()
    {
        int Error = 0;
        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "MM/dd/yyyy";
        if (ddlFrequency.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Select Frequency" + "');", true);
            return Error = 1;
        }
        if (ddlFrequency.SelectedItem.Text.ToUpper() == Config.GetAppsetting("Freq2") )//|| ddlFrequency.SelectedItem.Text == Config.GetAppsetting("Freq8"))Commented by abinayaa 110513 for yearly must select triggermonth,date,due month and due date
        {
            if (ddlTiggerMonth.SelectedIndex != 0 || ddlTriggerDate.SelectedIndex != 0 || ddlTriggerDay.SelectedIndex != 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "When the frequecy is " + ddlFrequency.SelectedItem.Text + " ,then Tirggermonth,TriggerDate and TriggerDay should be in NA" + "');", true);
                return Error = 1;
            }

            if (ddldueMonth.SelectedIndex != 0 || ddlDueDay.SelectedIndex != 0 || ddlDueDate.SelectedIndex != 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "When the frequecy is " + ddlFrequency.SelectedItem.Text + " ,then Duemonth,DueDate and DueDay should be in NA" + "');", true);
                return Error = 1;
            }
        }

        if (ddlFrequency.SelectedItem.Text.ToUpper() == Config.GetAppsetting("Freq3"))
        {
            if (ddlTiggerMonth.SelectedIndex != 0 || ddlTriggerDate.SelectedIndex != 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "When the frequecy is " + ddlFrequency.SelectedItem.Text + " ,then Tirggermonth and TriggerDate should be in NA" + "');", true);
                return Error = 1;
            }

            if (ddldueMonth.SelectedIndex != 0 || ddlDueDate.SelectedIndex != 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "When the frequecy is " + ddlFrequency.SelectedItem.Text + " ,then Duemonth and DueDate should be in NA" + "');", true);
                return Error = 1;
            }

            if (ddlDueDay.SelectedIndex == 0 || ddlTriggerDay.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "When the frequecy is " + ddlFrequency.SelectedItem.Text + " ,then TriggerDay and DueDay should not be in NA" + "');", true);
                return Error = 1;
            }
        }
        //start-added by abinayaa 08051 for As and when cheking
        if (ddlFrequency.SelectedItem.Text.ToUpper() == Config.GetAppsetting("Freq1"))
        {
           
            if (ddldueMonth.SelectedIndex != 0 || ddlDueDay.SelectedIndex != 0 || ddlDueDate.SelectedIndex != 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "When the frequecy is " + ddlFrequency.SelectedItem.Text + " ,then Duemonth,DueDate and DueDay should be in NA" + "');", true);
                return Error = 1;
            }
            if (ddlTiggerMonth.SelectedIndex != 0 || ddlTriggerDate.SelectedIndex != 0 || ddlTriggerDay.SelectedIndex != 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "When the frequecy is " + ddlFrequency.SelectedItem.Text + " ,then Tirggermonth,TriggerDate and TriggerDay should be in NA" + "');", true);
                return Error = 1;
            }

        }
        //End-added by abinayaa 08051 for As and when cheking
        if (ddlFrequency.SelectedItem.Text.ToUpper() == Config.GetAppsetting("Freq4") || ddlFrequency.SelectedItem.Text.ToUpper() == Config.GetAppsetting("Freq5") || ddlFrequency.SelectedItem.Text.ToUpper() == Config.GetAppsetting("Freq6") || ddlFrequency.SelectedItem.Text.ToUpper() == Config.GetAppsetting("Freq7") || ddlFrequency.SelectedItem.Text.ToUpper() == Config.GetAppsetting("Freq8"))//--Freq8 added by abinayaa 110513 for yearly must select triggermonth,date,due month and due date
        {
            if (ddlTriggerDay.SelectedIndex != 0 || ddlDueDay.SelectedIndex != 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "When the frequecy is " + ddlFrequency.SelectedItem.Text + " ,then Tirggerday and DueDay should be in NA" + "');", true);
                return Error = 1;
            }

            if (ddlTriggerDate.SelectedIndex == 0 || ddlTiggerMonth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "When the frequecy is " + ddlFrequency.SelectedItem.Text + " ,then Tirggermonth and TriggerDate should not be in NA" + "');", true);
                return Error = 1;
            }

            if (ddlDueDate.SelectedIndex == 0 || ddldueMonth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "When the frequecy is " + ddlFrequency.SelectedItem.Text + " ,then DueDate and DueMonth should not be in NA" + "');", true);
                return Error = 1;
            }
         
            int DueDate = Convert.ToInt32(ddlDueDate.SelectedValue);
            int DueMonth = Convert.ToInt32(ddldueMonth.SelectedValue);
            int DueYear = System.DateTime.Now.Year;
            double FrequencyDays = 0;
            DateTime Todate = Convert.ToDateTime(DueYear.ToString() + "/" + DueMonth.ToString() + "/" + DueDate.ToString(), dateinfo);
            List<FrequencyMasterMsg> frequencyList = getFrequency();
            var freq = (from Frequency in frequencyList
                        where Frequency.FrequencyName.Equals(ddlFrequency.SelectedItem.Text.ToUpper())
                        select Frequency).ToList();
            foreach (var f in freq)
            {
                FrequencyDays = Convert.ToDouble("-" + f.FrequencyDays);
            }
            DateTime FromDate = Todate.AddDays(FrequencyDays);
            int TriggDate = Convert.ToInt32(ddlTriggerDate.SelectedValue);
            int TriggMonth = Convert.ToInt32(ddlTiggerMonth.SelectedValue);
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
        return Error;
    }
    public List<FrequencyMasterMsg> getFrequency()
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        FncyList = Bus.FrequencyMasterSelect(Emp);
        return FncyList;
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
    public DataTable LoadDate()
    {
        DataTable dtDate = new DataTable();
        dtDate.Columns.Add("Date", typeof(string));
        dtDate.Columns.Add("Number", typeof(int));
        DataRow dr0 = dtDate.NewRow();
        dr0["Date"] = "NA";
        dr0["Number"] = 0;
        dtDate.Rows.Add(dr0);
        DataRow dr1 = dtDate.NewRow();
        dr1["Date"] = "1";
        dr1["Number"] = 1;
        dtDate.Rows.Add(dr1);
        DataRow dr2 = dtDate.NewRow();
        dr2["Date"] = "2";
        dr2["Number"] = 2;
        dtDate.Rows.Add(dr2);
        DataRow dr3 = dtDate.NewRow();
        dr3["Date"] = "3";
        dr3["Number"] = 3;
        dtDate.Rows.Add(dr3);
        DataRow dr4 = dtDate.NewRow();
        dr4["Date"] = "4";
        dr4["Number"] = 4;
        dtDate.Rows.Add(dr4);
        DataRow dr5 = dtDate.NewRow();
        dr5["Date"] = "5";
        dr5["Number"] = 5;
        dtDate.Rows.Add(dr5);
        DataRow dr6 = dtDate.NewRow();
        dr6["Date"] = "6";
        dr6["Number"] = 6;
        dtDate.Rows.Add(dr6);
        DataRow dr7 = dtDate.NewRow();
        dr7["Date"] = "7";
        dr7["Number"] = 7;
        dtDate.Rows.Add(dr7);
        DataRow dr8 = dtDate.NewRow();
        dr8["Date"] = "8";
        dr8["Number"] = 8;
        dtDate.Rows.Add(dr8);
        DataRow dr9 = dtDate.NewRow();
        dr9["Date"] = "9";
        dr9["Number"] = 9;
        dtDate.Rows.Add(dr9);
        DataRow dr10 = dtDate.NewRow();
        dr10["Date"] = "10";
        dr10["Number"] = 10;
        dtDate.Rows.Add(dr10);
        DataRow dr11 = dtDate.NewRow();
        dr11["Date"] = "11";
        dr11["Number"] = 11;
        dtDate.Rows.Add(dr11);
        DataRow dr12 = dtDate.NewRow();
        dr12["Date"] = "12";
        dr12["Number"] = 12;
        dtDate.Rows.Add(dr12);
        DataRow dr13 = dtDate.NewRow();
        dr13["Date"] = "13";
        dr13["Number"] = 13;
        dtDate.Rows.Add(dr13);
        DataRow dr14 = dtDate.NewRow();
        dr14["Date"] = "14";
        dr14["Number"] = 14;
        dtDate.Rows.Add(dr14);
        DataRow dr15 = dtDate.NewRow();
        dr15["Date"] = "15";
        dr15["Number"] = 15;
        dtDate.Rows.Add(dr15);
        DataRow dr16 = dtDate.NewRow();
        dr16["Date"] = "16";
        dr16["Number"] = 16;
        dtDate.Rows.Add(dr16);
        DataRow dr17 = dtDate.NewRow();
        dr17["Date"] = "17";
        dr17["Number"] = 17;
        dtDate.Rows.Add(dr17);
        DataRow dr18 = dtDate.NewRow();
        dr18["Date"] = "18";
        dr18["Number"] = 18;
        dtDate.Rows.Add(dr18);
        DataRow dr19 = dtDate.NewRow();
        dr19["Date"] = "19";
        dr19["Number"] = 19;
        dtDate.Rows.Add(dr19);
        DataRow dr20 = dtDate.NewRow();
        dr20["Date"] = "20";
        dr20["Number"] = 20;
        dtDate.Rows.Add(dr20);
        DataRow dr21 = dtDate.NewRow();
        dr21["Date"] = "21";
        dr21["Number"] = 21;
        dtDate.Rows.Add(dr21);
        DataRow dr22 = dtDate.NewRow();
        dr22["Date"] = "22";
        dr22["Number"] = 22;
        dtDate.Rows.Add(dr22);
        DataRow dr23 = dtDate.NewRow();
        dr23["Date"] = "23";
        dr23["Number"] = 23;
        dtDate.Rows.Add(dr23);
        DataRow dr24 = dtDate.NewRow();
        dr24["Date"] = "24";
        dr24["Number"] = 24;
        dtDate.Rows.Add(dr24);
        DataRow dr25 = dtDate.NewRow();
        dr25["Date"] = "25";
        dr25["Number"] = 25;
        dtDate.Rows.Add(dr25);
        DataRow dr26 = dtDate.NewRow();
        dr26["Date"] = "26";
        dr26["Number"] = 26;
        dtDate.Rows.Add(dr26);
        DataRow dr27 = dtDate.NewRow();
        dr27["Date"] = "27";
        dr27["Number"] = 27;
        dtDate.Rows.Add(dr27);
        DataRow dr28 = dtDate.NewRow();
        dr28["Date"] = "28";
        dr28["Number"] = 28;
        dtDate.Rows.Add(dr28);
        DataRow dr29 = dtDate.NewRow();
        dr29["Date"] = "29";
        dr29["Number"] = 29;
        dtDate.Rows.Add(dr29);
        DataRow dr30 = dtDate.NewRow();
        dr30["Date"] = "30";
        dr30["Number"] = 30;
        dtDate.Rows.Add(dr30);
        DataRow dr31 = dtDate.NewRow();
        dr31["Date"] = "31";
        dr31["Number"] = 31;
        dtDate.Rows.Add(dr31);
        return dtDate;
    }
    #endregion
    protected void btnBack_Click(object sender, EventArgs e)
    {
        GrdActivityMaster.Enabled = true;
        btnContinue.Enabled = false;
        btnClear.Enabled = false;
        btnBack.Visible = false;
        AllClear();
        Pnlfrequency.Visible = false;
        PnlAssignResponsibleGroup.Visible = false;
        btnSave.Visible = false;
        btnExit.Visible = false;
        pnlNew.Enabled = true;
        pnlACt.Visible = false;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadFiterGrid();
    }
    protected void btnSearchClear_Click(object sender, EventArgs e)
    {
        LoadActivityGrid(ActivityList);
        txtActivityNamefilter.Text = "";
        txtActivityNamefilter.Enabled = true;
        lblActivityNamefilter.Enabled = true;
        btnSearch.Enabled = true;
    }
}