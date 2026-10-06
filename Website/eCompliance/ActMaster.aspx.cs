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

public partial class ActMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    public static List<ActMasterMsg> ActList = new List<ActMasterMsg>();
    public static List<StateMasterMsg> StList = new List<StateMasterMsg>();
    public static int DeleteIndex = 0;
    public static int UpdateIndex = 0;
    UserAccess user = new UserAccess();
    public static string ProgramName = string.Empty;
    BaseClass BaseMsg = new BaseClass();
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
        {
            btnCollapse.Text = "Collapse";
            ddlActName.Enabled = true;
            txtActName.Enabled = true;
            LoadFilterAct();
            LoadActName();
            LoadGrdActNameMaster();           
            //LoadActName();
            LoadddlClassification();
            if (ActList.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.MsgForGrdnotLoad + "');", true);
                Pnlgv.Visible = false;
                pnlAdd.Visible = true;
                ddlActName.Visible = false;
                pnlActLoad.Visible = false;
            }
            else
            {
                ddlActName.Visible = true;
                pnlActLoad.Visible = true;
            }
            //if (ActList.Count == 0)
            //{
            //    ddlActName.Visible = false;
            //}
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region just ignore
        ///For Deleting a Record from Grid-Starts
        //if (HidDeleteCount.Value == "1")
        //{
        //    ActDelete();
        //    HidDeleteCount.Value = "0";
        //    return;
        //}

        /////For Deleting a Record from Grid-Ends
        /////For Updating a Record from Grid-Starts
        //if (HidUpdateCount.Value == "1")
        //{
        //    ActUpdate();
        //    HidUpdateCount.Value = "0";
        //    return;
        //}
        ///For Deleting a Record from Grid-Ends
        #endregion
        if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.CreatePermissionRestricted + "');", true);
        }
        else
        {
            if (IsValidSave() == 0)
            {
                if (btnSave.Text == "Save")
                {
                    ActSave();
                }
                else
                {
                    if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.EditPermissionRestricted + "');", true);
                    }
                    else
                    {
                        ActUpdate();
                    }
                }
            }
        }
    }
    protected void ddlActName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlActName.SelectedIndex > 0)
        {
            txtActName.Text = ddlActName.SelectedItem.Text;
            txtActName.Enabled = false;
            var Classification = (from Act in ActList
                                  where Act.ActId == Convert.ToInt32(ddlActName.SelectedValue)
                                  select new { Act.ClassificationAct });
            foreach (var ClassAct in Classification)
            {
                ddlClassificationAct.SelectedItem.Text = ClassAct.ClassificationAct;
                ddlClassificationAct.Enabled = false;

            }
        }
        else
        {

        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        GrdActMaster.Visible = true;
        LoadGrdActNameMaster();
        btnCollapse.Text = "Collapse";
        Pnlgv.Visible = true;
        //AllClear();
    }
    protected void btnCollapse_Click(object sender, EventArgs e)
    {
        if (btnCollapse.Text == "Expand")
        {
            btnCollapse.Text = "Collapse";
            Pnlgv.Visible = true;
        }
        else
        {
            btnCollapse.Text = "Expand";
            Pnlgv.Visible = false;
        }
       
    }
    #endregion
    #region GridEditing
    protected void GrdActMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdActMaster.EditIndex = e.NewEditIndex;
        UpdateIndex = e.NewEditIndex;
        LoadtxtBox(UpdateIndex);
        ddlActName.SelectedItem.Text = txtActName.Text;
       
        ddlActName.Enabled = false;
        txtActName.Enabled = false;
        GrdActMaster.EditIndex = -1;
        LoadGrdData(ActList);
        //btnCollapse.Text = "Expand";
        //Pnlgv.Visible = false;
        #region can Del
        //GrdActMaster.EditIndex = e.NewEditIndex;
        ////LoadGrdActNameMaster();
        //LoadGrdData(ActList);
        //GridViewRow row = GrdActMaster.Rows[GrdActMaster.EditIndex];
        //Label lblActName = (Label)row.FindControl("lblActName");
        //Label lblClassification = (Label)row.FindControl("lblClassification");             
        //TextBox txtActName = (TextBox)row.FindControl("txtActName");
        //txtActName.Focus();
        
        ////foreach (GridViewRow gvr in GrdActMaster.Rows)
        ////{
        ////    if (gvr.RowIndex == GrdActMaster.EditIndex)
        ////    {

        ////        DropDownList ddlClassificationAct = (DropDownList)row.FindControl("ddlClassificationAct");
        ////        //ddlClassificationAct.Focus();
        ////        DataTable dt = LoadClassification();

        ////        foreach (DataRow dr in dt.Rows)
        ////        {
        ////            if (dr["ClassificationAct"].ToString().Trim() == lblClassification.Text.Trim())
        ////            {
        ////                ddlClassificationAct.SelectedItem.Text = dr["ClassificationAct"].ToString();
        ////                ddlClassificationAct.Focus();
        ////                break;
        ////            }
        ////            //ddlClassificationAct.SelectedIndex = 0;
        ////        }
        ////    }
        ////}

        #endregion
    }
    //protected void GrdCompanyMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    GrdActMaster.EditIndex = -1;
    //    //LoadGrdActNameMaster();
    //    LoadGrdData(ActList);
    //}
    #region Rowdelete commented
    //protected void GrdCompanyMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    DeleteIndex = e.RowIndex;
    //    ActDelete();
    //}
    #endregion
    #region GrdCompanyMaster_RowUpdating commented
    //protected void GrdCompanyMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    UpdateIndex = e.RowIndex;
    //    GridViewRow row = GrdActMaster.Rows[UpdateIndex];
    //    TextBox txtActname = (TextBox)row.FindControl("txtActname");
    //    TextBox txtChapter = (TextBox)row.FindControl("txtChapter");
    //    TextBox txtHead = (TextBox)row.FindControl("txtHead");
    //    TextBox txtSection = (TextBox)row.FindControl("txtSection");
    //    TextBox txtActRule = (TextBox)row.FindControl("txtActRule");
    //    TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
    //    if (IsValidGridSave(txtActname.Text.Trim(), txtChapter.Text.Trim(), txtHead.Text.Trim(), txtSection.Text.Trim(), txtActRule.Text.Trim(), txtDescription.Text.Trim()) == 0)
    //    {
    //        ActUpdate();
    //    }
    //}
    #endregion
    #region Rowcommand commented
    //protected void GrdActMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "Edit")
    //    {
    //        int WRowIndex = Convert.ToInt32(e.CommandArgument.ToString());
    //        UpdateIndex = WRowIndex;
    //        LoadtxtBox(UpdateIndex);
    //        GrdActMaster.EditIndex = -1;
    //        LoadGrdData(ActList);
    //    }
    //}
    #endregion
 
    #endregion
    #region Methods
    private void LoadGrdActNameMaster()
    {
        ActMasterMsg Act = new ActMasterMsg();
        Act.Flag = "R";
        Act.ActId = Convert.ToInt32(ddlLoadAct.SelectedValue);
        Act.CreatedBy = BaseMsg.EmployeeCode; //scs040416
        ActList = Bus.MasActsInsertUpdateandDelete(Act);
        LoadGrdData(ActList);
        //GrdActMaster.DataSource = "";
        //GrdActMaster.DataSource = ActList;
        //GrdActMaster.DataBind();
        //if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        //{
        //    GrdActMaster.Columns[GrdActMaster.Columns.Count - 2].Visible = false;
        //}
        //if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        //{
        //    GrdActMaster.Columns[GrdActMaster.Columns.Count - 1].Visible = false;
        //}
    }
    private void LoadGrdData(List<ActMasterMsg> ActList)//added by abinayaa 110913
    {
        GrdActMaster.DataSource = "";
        GrdActMaster.DataSource = ActList;
        GrdActMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            //GrdActMaster.Columns[GrdActMaster.Columns.Count - 2].Visible = false;
            GrdActMaster.Columns[0].Visible = false; //edit moved to first column scs 180316
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdActMaster.Columns[GrdActMaster.Columns.Count - 1].Visible = false;
        }
        
        foreach (GridViewRow gvr in GrdActMaster.Rows)
        {
            DateTime WToDate = DateTime.Today; //.AddDays(0 - DateTime.Today.Day);
            Label txtEffDate = ((Label)gvr.FindControl("lblEffectiveDate"));
            Label txtExpDate = ((Label)gvr.FindControl("lblExpiryDate"));
            //ACTS Not yet Effective as on date show in Blue
            if (txtEffDate.Text == string.Empty)
            {
                
            }
            else
            {              
                if (Convert.ToDateTime(txtEffDate.Text) > Convert.ToDateTime(WToDate)) //scs070416 effective date is not yest arrived make it blue
                {
                    gvr.ForeColor = System.Drawing.Color.Blue;

                }
            }
            // expired ACts show in RED
            if (txtExpDate.Text == string.Empty)
            {

            }
            else
            {
                if (Convert.ToDateTime(txtExpDate.Text) <= Convert.ToDateTime(WToDate))
                {
                    gvr.ForeColor = System.Drawing.Color.Red;
                    gvr.Enabled = false; //scs 070416
                }
            }

          

            
        }
    }
    private void LoadtxtBox(int RIndex)
    {
        GridViewRow row = GrdActMaster.Rows[RIndex];
        txtActDetailId.Text = ((Label)row.FindControl("lblActdtlId")).Text;
        txtActId.Text = ((Label)row.FindControl("lblActId")).Text;
        txtActName.Text = ((Label)row.FindControl("lblActName")).Text;
        if (((Label)row.FindControl("lblClassification")).Text != string.Empty)
        {
            ddlClassificationAct.SelectedItem.Text = ((Label)row.FindControl("lblClassification")).Text;
        }
        txtChapter.Text = ((Label)row.FindControl("lblChapter")).Text;
        txtHead.Text = ((Label)row.FindControl("lblHead")).Text;
        txtSection.Text = ((Label)row.FindControl("lblSection")).Text;
        txtActRule.Text = ((Label)row.FindControl("lblActRule")).Text;
        txtDescription.Text = ((Label)row.FindControl("lblDescription")).Text;

        txtFrequency.Text = ((Label)row.FindControl("lblFrequency")).Text;
        txtImplicationSection.Text = ((Label)row.FindControl("lblImplicationSection")).Text;
        txtImplication.Text = ((Label)row.FindControl("lblImplication")).Text;
        txtLiability.Text = ((Label)row.FindControl("lblLiability")).Text;
        txtAffectedPerson.Text = ((Label)row.FindControl("lblAffectedPerson")).Text;
        txtImportance.Text = ((Label)row.FindControl("lblImportance")).Text;
        txtVersionNumber.Text = ((Label)row.FindControl("lblVersionNumber")).Text;
        txtEffectiveDate.Text = ((Label)row.FindControl("lblEffectiveDate")).Text;
        txtOldFDate.Text = txtEffectiveDate.Text;
        txtExpiryDate.Text = ((Label)row.FindControl("lblExpiryDate")).Text;
        txtOldTDate.Text = txtExpiryDate.Text;
        ChkIsNew.Checked = ((CheckBox)row.FindControl("ChkIsNew")).Checked;
        ChkValidityStatus.Checked = ((CheckBox)row.FindControl("ChkValidityStatus")).Checked;
        btnSave.Text = "Update";
        pnlAdd.Visible = true;
        RIndex = -1;
    }
    private void ActSave()
    {
        ActMasterMsg Act = new ActMasterMsg();
        Act.Flag = "I";
        Act.AuditUpLoadId = 0; //this is from program hence no upload scs 130316
        Act.ActName = txtActName.Text.Trim();

        //if (txtActName.Text == string.Empty)//Error Condition for Empty Text And ddlActName in Seect Please Already taken Care
        //{
            if (ddlActName.SelectedIndex > 0)
            {
                Act.ActId = Convert.ToInt32(ddlActName.SelectedValue);
                Act.ActName = ddlActName.SelectedItem.Text.Trim();
            }
            else
            {
                Act.ActId = 0;
            }
        //}
        Act.ActDtlId = 0;
        Act.ClassificationAct = ddlClassificationAct.SelectedItem.Text.Trim();
        Act.Chapter = txtChapter.Text.Trim();
        Act.Head = txtHead.Text.Trim();
        Act.Section = txtSection.Text.Trim();
        Act.ActRule = txtActRule.Text.Trim();
        Act.Description = txtDescription.Text.Trim();
  
  // SCS 010316 added after discussion with Auditor
        Act.Frequency = txtFrequency.Text.Trim();
        Act.ImplicationSection = txtImplicationSection.Text.Trim();
        Act.Implication = txtImplication.Text.Trim();
        Act.Liability = txtLiability.Text.Trim();
        Act.AffectedPerson = txtAffectedPerson.Text.Trim();
        Act.Importance = txtImportance.Text.Trim();
        Act.VersionNumber = txtVersionNumber.Text.Trim();
        Act.ValidityStatus = ChkValidityStatus.Checked;
       
        if (ChkIsNew.Checked == true)
        {
            Act.IsNew = true;
            Act.IsUpdated = false;
        }
        else
        {
            Act.IsNew = false;
            Act.IsUpdated = true;
        }
        Act.EffectiveDate = Convert.ToDateTime(txtEffectiveDate.Text.Trim());
        if (( txtExpiryDate.Text == string.Empty || txtExpiryDate.Text == null))
        {
            //for new acts where expiry not specified add three years to effective date.
            Act.ExpiryDate = (Convert.ToDateTime(txtEffectiveDate.Text.Trim()).AddYears(3));
        }
        else
        {
            Act.ExpiryDate = Convert.ToDateTime(txtExpiryDate.Text.Trim());
        }
       // scs 010316 Over
        Act.CreatedBy = BaseMsg.EmployeeCode;
        ActList = Bus.MasActsInsertUpdateandDelete(Act);
        //Output Dispay
        foreach (ActMasterMsg ActSave in ActList)
        {
            if (ActSave.ActResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.SuccessFullySaved + "');", true);
                AllClear();
                ddlActName.Visible = true;
                GrdActMaster.Visible = true;
                pnlActLoad.Visible = true;
                LoadFilterAct();              
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + ActSave.ActResult + "');", true);
                break;
            }
        }
    }
    public void ActUpdate()
    {
        try
        {
            //GridViewRow row = GrdActMaster.Rows[UpdateIndex];
            ActMasterMsg Act = new ActMasterMsg();
            Act.Flag = "U";
            Act.AuditUpLoadId = 0; //this is from program hence no upload scs 130316
            #region can be deleted
            //TextBox txtActId = (TextBox)row.FindControl("txtActId");
            //TextBox txtActDtlId = (TextBox)row.FindControl("txtActDtlId");
            //TextBox txtActname = (TextBox)row.FindControl("txtActname");
            //DropDownList ddlClassificationAct=(DropDownList)row.FindControl("ddlClassificationAct");
            //TextBox txtChapter = (TextBox)row.FindControl("txtChapter");
            //TextBox txtHead = (TextBox)row.FindControl("txtHead");
            //TextBox txtSection = (TextBox)row.FindControl("txtSection");
            //TextBox txtActRule = (TextBox)row.FindControl("txtActRule");
            //TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
            //CheckBox chkActive = (CheckBox)row.FindControl("chkIsActive");
            //// scs 010316
            //TextBox txtFrequency = (TextBox)row.FindControl("txtFrequency");
            //TextBox txtImplicationSection = (TextBox)row.FindControl("txtImplicationSection");
            //TextBox txtImplication = (TextBox)row.FindControl("txtImplication");
            //TextBox txtLiability = (TextBox)row.FindControl("txtLiability");
            //TextBox txtAffectedPerson = (TextBox)row.FindControl("txtAffectedPerson");
            //TextBox txtImportance = (TextBox)row.FindControl("txtImportance");
            //TextBox txtVersionNumber = (TextBox)row.FindControl("txtVersionNumber");
            //TextBox txtExpiryDate = (TextBox)row.FindControl("txtExpiryDate");
            //TextBox txtEffectiveDate = (TextBox)row.FindControl("txtEffectiveDate");
            //TextBox txtValidityStatus = (TextBox)row.FindControl("txtValidityStatus");
            ////if (Convert.ToString((TextBox)row.FindControl("txtValidityStatus"))=="Y")
            ////{
            ////}
            ////else{
            ////}
            //TextBox txtIsNew = (TextBox)row.FindControl("txtIsNew");
            //TextBox txtIsUpdated = (TextBox)row.FindControl("txtIsUpdated");
            ////
            #endregion
            Act.ActId = Convert.ToInt32(txtActId.Text.Trim());
            Act.ActDtlId = Convert.ToInt32(txtActDetailId.Text.Trim());
            Act.ActName = txtActName.Text.Trim();
            Act.ClassificationAct = ddlClassificationAct.SelectedItem.Text.Trim();
            Act.Chapter = txtChapter.Text.Trim();
            Act.Head = txtHead.Text.Trim();
            Act.Section = txtSection.Text.Trim();
            Act.ActRule = txtActRule.Text.Trim();
            Act.Description = txtDescription.Text.Trim();
           
            // SCS 010316 added after discussion with Auditor
            Act.Frequency = txtFrequency.Text.Trim();
            Act.ImplicationSection = txtImplicationSection.Text.Trim();
            Act.Implication = txtImplication.Text.Trim();
            Act.Liability = txtLiability.Text.Trim();
            Act.AffectedPerson = txtAffectedPerson.Text.Trim();
            Act.Importance = txtImportance.Text.Trim();
            Act.VersionNumber = txtVersionNumber.Text.Trim();
            Act.ValidityStatus = ChkValidityStatus.Checked;
           if (ChkIsNew.Checked == true)
           {
                Act.IsNew = true;
                Act.IsUpdated = false;
                Act.IsActive = true; // chkActive.Checked;
            }
            else
            {
                Act.IsNew = false;
                Act.IsUpdated = true;
                Act.IsActive = false; // chkActive.Checked;
            }
            Act.EffectiveDate = Convert.ToDateTime(txtEffectiveDate.Text.Trim());
            if ((txtExpiryDate.Text == string.Empty || txtExpiryDate.Text == null))
            {
                //for new acts where expiry not specified add three years to effective date.
                Act.ExpiryDate = (Convert.ToDateTime(txtEffectiveDate.Text.Trim()));
            }
            else
            {
                Act.ExpiryDate = Convert.ToDateTime(txtExpiryDate.Text.Trim());
            }
            // scs 010316 Over

            Act.CreatedBy = BaseMsg.EmployeeCode;

            ActList = Bus.MasActsInsertUpdateandDelete(Act);


            //if (ActList.Count > 0)
            //{
            foreach (ActMasterMsg ActUpdate in ActList)
            {
                if (ActUpdate.ActResult == "0")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.ActNameUpdatedSuccessfully + "');", true);
                    AllClear();
                    break;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + ActUpdate.ActResult + "');", true);
                    break;
                }
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" +"Update Failure"+ "');", true);
        }
        GrdActMaster.EditIndex = -1;
        LoadGrdData(ActList);
    }
    public void ActDelete()
    {
        ActMasterMsg Act = new ActMasterMsg();
        Act.Flag = "D";
        GridViewRow gvr = GrdActMaster.Rows[DeleteIndex];

        Label lblActId = (Label)gvr.FindControl("lblActId");
        Label lblActDtlId = (Label)gvr.FindControl("lblActDtlId");
        Label lblActName = (Label)gvr.FindControl("lblActName");
        Label lblClassification = (Label)gvr.FindControl("lblClassification");
        Label lblChapter = (Label)gvr.FindControl("lblChapter");
        Label lblHead = (Label)gvr.FindControl("lblHead");
        Label lblSection = (Label)gvr.FindControl("lblSection");
        Label lblActRule = (Label)gvr.FindControl("lblActRule");
        Label lblDescription = (Label)gvr.FindControl("lblDescription");
        CheckBox chkActive = (CheckBox)gvr.FindControl("chkIsActive");

        Act.ActId = Convert.ToInt32(lblActId.Text.Trim());
        Act.ActDtlId = Convert.ToInt32(lblActDtlId.Text.Trim());
        Act.ActName = lblActName.Text.Trim();
        Act.ClassificationAct = lblClassification.Text.Trim();
        Act.Chapter = lblChapter.Text.Trim();
        Act.Head = lblHead.Text.Trim();
        Act.Section = lblSection.Text.Trim();
        Act.ActRule = lblActRule.Text.Trim();
        Act.Description = lblDescription.Text.Trim();
        Act.CreatedBy = BaseMsg.EmployeeCode;
        ActList = Bus.MasActsInsertUpdateandDelete(Act);
        //Output Dispay
        foreach (ActMasterMsg ActDelete in ActList)
        {
            if (ActDelete.ActResult == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.ActNameDeletedSuccessfully + "');", true);
                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + ActDelete.ActResult + "');", true);
                break;
            }
        }
    }
    private void LoadActName()
    {
        //List<ActMasterMsg> ddlActList = new List<ActMasterMsg>();
        var ddlActList = (from ActName in ActList
                          where ActName.IsActive == true
                          select new { ActName.ActName, ActName.ActId }).Distinct().ToList();
        ddlActName.DataSource = ddlActList;
        ddlActName.DataTextField = "ActName";
        ddlActName.DataValueField = "ActId";
        ddlActName.DataBind();
        ddlActName.Items.Insert(0, "-- Select Please --");
    }
    private void LoadFilterAct()
    {        
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.LoginEmployeeCode = BaseMsg.EmployeeCode; //scs040416 
        ActList = Bus.ActMasterSelect(Emp);
        var ddlActList = (from Act in ActList
                          where Act.IsActive == true
                          select new { Act.ActName, Act.ActId }).Distinct().ToList();
        ddlLoadAct.DataSource = ddlActList;
        ddlLoadAct.DataTextField = "ActName";
        ddlLoadAct.DataValueField = "ActId";
        ddlLoadAct.DataBind();
        ddlLoadAct.Items.Insert(0, new ListItem("--All--","0"));
        ddlLoadAct.SelectedIndex = 1; //scs 180315 added to reduce network traffic when fetch all data
    }
    public List<StateMasterMsg> LoadClassification()
    {//scs 140316 changed below comment to notify state
        //DataTable dtClassification = new DataTable();
        //dtClassification.Columns.Add("ClassificationAct", typeof(string));
        ////dtClassification.Columns.Add("Id", typeof(string));
        //DataRow dr1 = dtClassification.NewRow();
        //dr1["ClassificationAct"] = System.Configuration.ConfigurationManager.AppSettings["Central"].ToString();
        ////dr1["Id"] = System.Configuration.ConfigurationManager.AppSettings["Central"].ToString();
        //dtClassification.Rows.Add(dr1);
        //DataRow dr2 = dtClassification.NewRow();
        //dr2["ClassificationAct"] = System.Configuration.ConfigurationManager.AppSettings["State"].ToString();
        ////dr2["Id"] = System.Configuration.ConfigurationManager.AppSettings["State"].ToString();
        //dtClassification.Rows.Add(dr2);
       // return dtClassification;
        EmployeeMasterMsg emp = new EmployeeMasterMsg();
        emp.EmployeeCode = BaseMsg.EmployeeCode;
        StList = Bus.StateMasterSelect(emp);
        return StList;

    }
    public void LoadddlClassification()
    {
        //ddlClassificationAct.DataSource = LoadClassification();
        //ddlClassificationAct.DataBind();
       // ddlClassificationAct.Items.Insert(0, new ListItem("-- Select Please --", "0"));
        ddlClassificationAct.DataSource =LoadClassification();
        ddlClassificationAct.DataTextField = "StateName";
        ddlClassificationAct.DataValueField = "StateName";
        ddlClassificationAct.DataBind();
        ddlClassificationAct.Items.Insert(0, "Central");
    }
    #endregion
    #region Clear
    public void AllClear()
    {
        LoadActName();
        ClearTxtBox();
    }
    private void ClearTxtBox()
    {
        txtActName.Text = "";
        ddlActName.SelectedIndex = 0;
        txtChapter.Text = "";
        txtHead.Text = "";
        txtSection.Text = "";
        txtActRule.Text = "";
        txtDescription.Text = "";
        txtActName.Enabled = true;
        ddlClassificationAct.Enabled = true;
        ddlLoadAct.SelectedIndex = 0;
        txtAffectedPerson.Text = "";
        txtEffectiveDate.Text = "";
        txtExpiryDate.Text = "";
        txtFrequency.Text = "";
        txtImplication.Text = "";
        txtImplicationSection.Text = "";
        txtImportance.Text = "";
        txtLiability.Text = "";
        txtOldFDate.Text = System.DateTime.UtcNow.AddYears(-1).ToString("dd/MM/yyyy"); //""; when a New Act is created this gives error
        txtOldTDate.Text = "";
        txtVersionNumber.Text = "";
       
        LoadGrdData(ActList);
        LoadddlClassification();
        ddlActName.Enabled = true;
        txtActName.Enabled = true;
        //LoadFilterAct();

    }
    #endregion
    #region Validation

    private int IsValidSave()
    {
        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd/MM/yyyy";
        int Error = 0;
        string DisplayError = "";
        if (ddlActName.SelectedIndex == 0)
        {
            if ((txtActName.Text.Trim() == "" || Convert.ToInt32(txtActName.Text.Trim().Length.ToString()) == 0) && (ddlActName.SelectedIndex == 0))
            {

                DisplayError = DisplayError + "--" + StackResource.ErrActName;
                Error = 1;
            }
            if ((txtActName.Text.Trim() != "" || Convert.ToInt32(txtActName.Text.Trim().Length.ToString()) > 0) && (ddlActName.SelectedIndex > 0))
            {
                DisplayError = DisplayError + "--" + StackResource.ErrActNameDual;
                Error = 1;
            }
            if (txtVersionNumber.Text.Trim().Length > 8)
            {
                DisplayError = DisplayError + "-- Version Number Lenght is Maximum 8";
                Error = 1;
            }
            //if (ddlClassificationAct.SelectedIndex == 0)
            //{
            //    DisplayError = DisplayError + "--" + StackResource.ErrClassificationAct;
            //    Error = 1;
            //}
            if (txtExpiryDate.Text.Length > 0)
            {
                try
                {
                    if (Convert.ToDateTime(txtEffectiveDate.Text) > Convert.ToDateTime(txtExpiryDate.Text))
                    {
                        DisplayError = DisplayError + "-- Effective From Date Cannot be greater Than Expiry Date";
                        Error = 1;
                    }

                  //  if (Convert.ToDateTime(txtOldFDate.Text, dateinfo) > Convert.ToDateTime(txtEffectiveDate.Text, dateinfo))
                    if (txtOldFDate.Text.Trim().Length > 0 && Convert.ToDateTime(txtOldFDate.Text, dateinfo) > Convert.ToDateTime(txtEffectiveDate.Text, dateinfo))
                    {
                        DisplayError = DisplayError + "-- Previous From Date= " + txtOldFDate.Text + " New Effective Date = " + txtEffectiveDate.Text + ". Check Date";
                        Error = 1;
                    }
                }
                catch
                {
                    DisplayError = DisplayError + "-- Entered Date not Proper ";
                    Error = 1;
                }
            }
            
        }
       
 
        if ((txtSection.Text.Trim() == "" || Convert.ToInt32(txtSection.Text.Trim().Length.ToString()) == 0) && (txtActRule.Text.Trim().Length ==0))
        {
            DisplayError = DisplayError + "--" + StackResource.ErrSection + " OR " + StackResource.ErrActRule;
            Error = 1;
        }
      
        if (txtDescription.Text.Trim() == "" || Convert.ToInt32(txtDescription.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrDescription;
            Error = 1;
        }
        if (txtFrequency.Text.Trim() == "" || (txtFrequency.Text.Length == 0))
        {
            DisplayError = DisplayError + "--" + "Frequency of the Activity Required to be specified for the ACT";
            Error = 1;
        }

        
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }

    //private int IsValidGridSave(string ActName,string Chapater,string Head,string Section,string ActRule,string Description)
    //{
    //    int Error = 0;
    //    string DisplayError = "";
    //    if (ActName.Trim() == "" || Convert.ToInt32(ActName.Trim().Length.ToString()) == 0)
    //    {
    //        DisplayError = DisplayError + "--" + StackResource.ErrActName;
    //        Error = 1;
    //    }
    //    if (Chapater.Trim() == "" || Convert.ToInt32(Chapater.Trim().Length.ToString()) == 0)
    //    {
    //        DisplayError = DisplayError + "--" + StackResource.ErrChapter;
    //        Error = 1;
    //    }
    //    if (Head.Trim() == "" || Convert.ToInt32(Head.Trim().Length.ToString()) == 0)
    //    {
    //        DisplayError = DisplayError + "--" + StackResource.ErrHead;
    //        Error = 1;
    //    }
    //    if (Section.Trim() == "" || Convert.ToInt32(Section.Trim().Length.ToString()) == 0)
    //    {
    //        DisplayError = DisplayError + "--" + StackResource.ErrSection;
    //        Error = 1;
    //    }
    //    if (ActRule.Trim() == "" || Convert.ToInt32(ActRule.Trim().Length.ToString()) == 0)
    //    {
    //        DisplayError = DisplayError + "--" + StackResource.ErrActRule;
    //        Error = 1;
    //    }
    //    if (Description.Trim() == "" || Convert.ToInt32(Description.Trim().Length.ToString()) == 0)
    //    {
    //        DisplayError = DisplayError + "--" + StackResource.ErrDescription;
    //        Error = 1;
    //    }
    //    if (Error == 1)
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
    //    }
    //    return Error;
    //}
    #endregion

    
}