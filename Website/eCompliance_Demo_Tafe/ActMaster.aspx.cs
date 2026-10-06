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
            LoadFilterAct();
            LoadGrdActNameMaster();           
            LoadActName();
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
        if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.CreatePermissionRestricted + "');", true);
        }
        else
        {
            if (IsValidSave() == 0)
            {
                ActSave();
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
        //AllClear();
    }
    #endregion
    #region GridEditing
    protected void GrdActMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdActMaster.EditIndex = e.NewEditIndex;
        //LoadGrdActNameMaster();
        LoadGrdData(ActList);
        GridViewRow row = GrdActMaster.Rows[GrdActMaster.EditIndex];
        Label lblActName = (Label)row.FindControl("lblActName");
        Label lblClassification = (Label)row.FindControl("lblClassification");             
        TextBox txtActName = (TextBox)row.FindControl("txtActName");
        txtActName.Focus();
        
        //foreach (GridViewRow gvr in GrdActMaster.Rows)
        //{
        //    if (gvr.RowIndex == GrdActMaster.EditIndex)
        //    {

        //        DropDownList ddlClassificationAct = (DropDownList)row.FindControl("ddlClassificationAct");
        //        //ddlClassificationAct.Focus();
        //        DataTable dt = LoadClassification();

        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            if (dr["ClassificationAct"].ToString().Trim() == lblClassification.Text.Trim())
        //            {
        //                ddlClassificationAct.SelectedItem.Text = dr["ClassificationAct"].ToString();
        //                ddlClassificationAct.Focus();
        //                break;
        //            }
        //            //ddlClassificationAct.SelectedIndex = 0;
        //        }
        //    }
        //}
               
        


    }
    protected void GrdCompanyMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdActMaster.EditIndex = -1;
        //LoadGrdActNameMaster();
        LoadGrdData(ActList);
    }
    protected void GrdCompanyMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DeleteIndex = e.RowIndex;
        ActDelete();
    }
    protected void GrdCompanyMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        GridViewRow row = GrdActMaster.Rows[UpdateIndex];
        TextBox txtActname = (TextBox)row.FindControl("txtActname");
        TextBox txtChapter = (TextBox)row.FindControl("txtChapter");
        TextBox txtHead = (TextBox)row.FindControl("txtHead");
        TextBox txtSection = (TextBox)row.FindControl("txtSection");
        TextBox txtActRule = (TextBox)row.FindControl("txtActRule");
        TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
        if (IsValidGridSave(txtActname.Text.Trim(), txtChapter.Text.Trim(), txtHead.Text.Trim(), txtSection.Text.Trim(), txtActRule.Text.Trim(), txtDescription.Text.Trim()) == 0)
        {
            ActUpdate();
        }
    }
    #endregion
    #region Methods
    private void LoadGrdActNameMaster()
    {
        ActMasterMsg Act = new ActMasterMsg();
        Act.Flag = "R";
        Act.ActId = Convert.ToInt32(ddlLoadAct.SelectedValue);
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
            GrdActMaster.Columns[GrdActMaster.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdActMaster.Columns[GrdActMaster.Columns.Count - 1].Visible = false;
        }
    }
    private void ActSave()
    {
        ActMasterMsg Act = new ActMasterMsg();
        Act.Flag = "I";

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
        GridViewRow row = GrdActMaster.Rows[UpdateIndex];
        ActMasterMsg Act = new ActMasterMsg();
        Act.Flag = "U";

        TextBox txtActId = (TextBox)row.FindControl("txtActId");
        TextBox txtActDtlId = (TextBox)row.FindControl("txtActDtlId");
        TextBox txtActname = (TextBox)row.FindControl("txtActname");
        DropDownList ddlClassificationAct=(DropDownList)row.FindControl("ddlClassificationAct");
        TextBox txtChapter = (TextBox)row.FindControl("txtChapter");
        TextBox txtHead = (TextBox)row.FindControl("txtHead");
        TextBox txtSection = (TextBox)row.FindControl("txtSection");
        TextBox txtActRule = (TextBox)row.FindControl("txtActRule");
        TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
        CheckBox chkActive = (CheckBox)row.FindControl("chkIsActive");

        Act.ActId = Convert.ToInt32(txtActId.Text.Trim());
        Act.ActDtlId = Convert.ToInt32(txtActDtlId.Text.Trim());
        Act.ActName = txtActname.Text.Trim();
        Act.ClassificationAct = ddlClassificationAct.SelectedItem.Text.Trim();
        Act.Chapter = txtChapter.Text.Trim();
        Act.Head = txtHead.Text.Trim();
        Act.Section = txtSection.Text.Trim();
        Act.ActRule = txtActRule.Text.Trim();
        Act.Description = txtDescription.Text.Trim();
        Act.IsActive = chkActive.Checked;
        Act.CreatedBy = BaseMsg.EmployeeCode;
        ActList = Bus.MasActsInsertUpdateandDelete(Act);
        GrdActMaster.EditIndex = -1;

        foreach (ActMasterMsg ActUpdate in ActList)
        {
            if (ActUpdate.ActResult == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.ActNameUpdatedSuccessfully + "');", true);
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
        ActList = Bus.ActMasterSelect(Emp);
        var ddlActList = (from Act in ActList
                          where Act.IsActive == true
                          select new { Act.ActName, Act.ActId }).Distinct().ToList();
        ddlLoadAct.DataSource = ddlActList;
        ddlLoadAct.DataTextField = "ActName";
        ddlLoadAct.DataValueField = "ActId";
        ddlLoadAct.DataBind();
        ddlLoadAct.Items.Insert(0, new ListItem("--All--","0"));
    }
    public DataTable LoadClassification()
    {
        DataTable dtClassification = new DataTable();
        dtClassification.Columns.Add("ClassificationAct", typeof(string));
        //dtClassification.Columns.Add("Id", typeof(string));
        DataRow dr1 = dtClassification.NewRow();
        dr1["ClassificationAct"] = System.Configuration.ConfigurationManager.AppSettings["Central"].ToString();
        //dr1["Id"] = System.Configuration.ConfigurationManager.AppSettings["Central"].ToString();
        dtClassification.Rows.Add(dr1);
        DataRow dr2 = dtClassification.NewRow();
        dr2["ClassificationAct"] = System.Configuration.ConfigurationManager.AppSettings["State"].ToString();
        //dr2["Id"] = System.Configuration.ConfigurationManager.AppSettings["State"].ToString();
        dtClassification.Rows.Add(dr2);
        return dtClassification;
    }
    public void LoadddlClassification()
    {
        ddlClassificationAct.DataSource = LoadClassification();
        ddlClassificationAct.DataBind();
        ddlClassificationAct.Items.Insert(0, new ListItem("-- Select Please --", "0"));
    }
    #endregion
    #region Clear
    public void AllClear()
    {
        LoadActName();
        txtActName.Text = "";
        // ddlActName.SelectedItem.Text = "";
        ddlActName.SelectedIndex = 0;
        txtChapter.Text = "";
        txtHead.Text = "";
        txtSection.Text = "";
        txtActRule.Text = "";
        txtDescription.Text = "";
        txtActName.Enabled = true;
        ddlClassificationAct.Enabled = true;
        ddlLoadAct.SelectedIndex = 0;
        //LoadGrdActNameMaster();
        LoadGrdData(ActList);
        LoadddlClassification();
        //LoadFilterAct();

    }
    #endregion
    #region Validation

    private int IsValidSave()
    {
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
            if (ddlClassificationAct.SelectedIndex == 0)
            {
                DisplayError = DisplayError + "--" + StackResource.ErrClassificationAct;
                Error = 1;
            }
            
        }
       
       
        if (txtChapter.Text.Trim() == "" || Convert.ToInt32(txtChapter.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrChapter;
            Error = 1;
        }
        if (txtHead.Text.Trim() == "" || Convert.ToInt32(txtHead.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrHead;
            Error = 1;
        }
        if (txtSection.Text.Trim() == "" || Convert.ToInt32(txtSection.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrSection;
            Error = 1;
        }
        if (txtActRule.Text.Trim() == "" || Convert.ToInt32(txtActRule.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrActRule;
            Error = 1;
        }
        if (txtDescription.Text.Trim() == "" || Convert.ToInt32(txtDescription.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrDescription;
            Error = 1;
        }
        
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }

    private int IsValidGridSave(string ActName,string Chapater,string Head,string Section,string ActRule,string Description)
    {
        int Error = 0;
        string DisplayError = "";
        if (ActName.Trim() == "" || Convert.ToInt32(ActName.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrActName;
            Error = 1;
        }
        if (Chapater.Trim() == "" || Convert.ToInt32(Chapater.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrChapter;
            Error = 1;
        }
        if (Head.Trim() == "" || Convert.ToInt32(Head.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrHead;
            Error = 1;
        }
        if (Section.Trim() == "" || Convert.ToInt32(Section.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrSection;
            Error = 1;
        }
        if (ActRule.Trim() == "" || Convert.ToInt32(ActRule.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrActRule;
            Error = 1;
        }
        if (Description.Trim() == "" || Convert.ToInt32(Description.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrDescription;
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