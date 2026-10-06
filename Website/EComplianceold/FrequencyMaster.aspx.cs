using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using Ganini.Lib;
using System.Data;
using Resources;

public partial class FrequencyMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    public static List<FrequencyMasterMsg> FreqList = new List<FrequencyMasterMsg>();
    public static int DeleteIndex = 0;
    public static int UpdateIndex = 0;
    UserAccess user = new UserAccess();
    public static string ProgramName = string.Empty;
    Validation valid = new Validation();
    BaseClass BaseMsg = new BaseClass();
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
        {
            LoadGrdFrequenceMaster();
            if (FreqList.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.MsgForGrdnotLoad + "');", true);
                Pnlgv.Visible = false;
                pnlAdd.Visible = true;

            }
        }     
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.CreatePermissionRestricted + "');", true);
        }
        else
        {
            if (IsValidSave() == 0)
            {
                FrequenceSave();
            }
        }
    }
    #region GrdEdit
    protected void GrdFrequencyMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdFrequencyMaster.EditIndex = e.NewEditIndex;
        //LoadGrdFrequenceMaster();
        LoadGrdData(FreqList);
        GridViewRow row = GrdFrequencyMaster.Rows[GrdFrequencyMaster.EditIndex];
        TextBox FreqName = (TextBox)row.FindControl("txtFreqName");
        FreqName.Focus();
    }
    protected void GrdFrequencyMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DeleteIndex = e.RowIndex;
        FrequenceDelete();
    }
    protected void GrdFrequencyMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        GridViewRow row = GrdFrequencyMaster.Rows[UpdateIndex];
        TextBox txtFreqname = (TextBox)row.FindControl("txtFreqName");
        TextBox txtFreqshname = (TextBox)row.FindControl("txtFreqShName");
        TextBox txtFreqDays = (TextBox)row.FindControl("txtFreqDays");
        if (IsValidGridSave(txtFreqname.Text.Trim(),txtFreqshname.Text.Trim(),txtFreqDays.Text.Trim()) == 0)
        {
            FrequenceUpdate();
        }
    }
    protected void GrdFrequencyMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdFrequencyMaster.EditIndex = -1;
        LoadGrdFrequenceMaster();
    }
    #endregion
    #endregion
    #region Methods
    private void LoadGrdFrequenceMaster()
    {
        FrequencyMasterMsg Freq = new FrequencyMasterMsg();
        Freq.Flag = "R";
        FreqList = Bus.MasFrequencyInsertUpdateandDelete(Freq);
        LoadGrdData(FreqList);
        //GrdFrequencyMaster.DataSource = "";
        //GrdFrequencyMaster.DataSource = FreqList;
        //GrdFrequencyMaster.DataBind();
        //if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        //{
        //    GrdFrequencyMaster.Columns[GrdFrequencyMaster.Columns.Count - 2].Visible = false;
        //}
        //if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        //{
        //    GrdFrequencyMaster.Columns[GrdFrequencyMaster.Columns.Count - 1].Visible = false;
        //}
    }
    private void LoadGrdData(List<FrequencyMasterMsg> FreqList)//added by sai 280913 to avoid double time loading the grid
    {
        GrdFrequencyMaster.DataSource = "";
        GrdFrequencyMaster.DataSource = FreqList;
        GrdFrequencyMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdFrequencyMaster.Columns[GrdFrequencyMaster.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdFrequencyMaster.Columns[GrdFrequencyMaster.Columns.Count - 1].Visible = false;
        }
    }
    private void FrequenceSave()
    {
        FrequencyMasterMsg Freq = new FrequencyMasterMsg();
        Freq.Flag = "I";
        Freq.FrequencyName = txtFrequencyName.Text.Trim();
        Freq.FrequencyShortName = txtFrequencyShortName.Text.Trim();
        Freq.FrequencyDays = Convert.ToInt32(txtFrequencyDays.Text.Trim());
        Freq.CreatedBy = BaseMsg.EmployeeCode;
        FreqList = Bus.MasFrequencyInsertUpdateandDelete(Freq);
        //Output Dispay
        foreach (FrequencyMasterMsg FreqSave in FreqList)
        {
            if (FreqSave.FrequencyResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.SuccessFullySaved + "');", true);
                AllClear();
                GrdFrequencyMaster.Visible = true;
                Pnlgv.Visible = true;
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + FreqSave.FrequencyResult + "');", true);
                LoadGrdFrequenceMaster();
                break;
            }
        }
    }
    public void FrequenceUpdate()
    {
        GridViewRow row = GrdFrequencyMaster.Rows[UpdateIndex];
        FrequencyMasterMsg Freq = new FrequencyMasterMsg();
        Freq.Flag = "U";
        TextBox txtFreqId = (TextBox)row.FindControl("txtFreqId");
        TextBox txtFreqname = (TextBox)row.FindControl("txtFreqName");
        TextBox txtFreqshname = (TextBox)row.FindControl("txtFreqShName");
        TextBox txtFreqDays = (TextBox)row.FindControl("txtFreqDays");
        CheckBox chkActive = (CheckBox)row.FindControl("chkIsActive");

        Freq.FrequencyId = Convert.ToInt32(txtFreqId.Text.Trim());
        Freq.FrequencyName = txtFreqname.Text.Trim();
        Freq.FrequencyShortName = txtFreqshname.Text.Trim();
        Freq.FrequencyDays = Convert.ToInt32(txtFreqDays.Text.Trim());
        Freq.IsActive = chkActive.Checked;
        Freq.CreatedBy = BaseMsg.EmployeeCode;
        FreqList = Bus.MasFrequencyInsertUpdateandDelete(Freq);
      

        foreach (FrequencyMasterMsg FreqUpdate in FreqList)
        {
            if (FreqUpdate.FrequencyResult == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.FrequenceUpdatedSuccessfully + "');", true);
                GrdFrequencyMaster.EditIndex = -1;
                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + FreqUpdate.FrequencyResult + "');", true);
                txtFreqname.Focus();
                break;
            }
        }
    }
    public void FrequenceDelete()
    {
        FrequencyMasterMsg Freq = new FrequencyMasterMsg();

        Freq.Flag = "D";
        GridViewRow gvr = GrdFrequencyMaster.Rows[DeleteIndex];
        Label lblFreqId = (Label)gvr.FindControl("lblFreqId");
        Label lblFreqName = (Label)gvr.FindControl("lblFreqName");
        Label lblFreqshName = (Label)gvr.FindControl("lblFreqShName");
        Label lblFreqDays = (Label)gvr.FindControl("lblFreqDays");
        CheckBox chkActive = (CheckBox)gvr.FindControl("chkIsActive");

        Freq.FrequencyId = Convert.ToInt32(lblFreqId.Text.Trim());
        Freq.FrequencyName = lblFreqName.Text.Trim();
        Freq.FrequencyShortName = lblFreqshName.Text.Trim();
        Freq.FrequencyDays = Convert.ToInt32(lblFreqDays.Text.Trim());
        Freq.CreatedBy = BaseMsg.EmployeeCode;
        FreqList = Bus.MasFrequencyInsertUpdateandDelete(Freq);
        foreach (FrequencyMasterMsg FreqDelete in FreqList)
        {
            if (FreqDelete.FrequencyResult == "0")
            {
               // ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.FrequenceDeletedSuccessfully + "');", true);
                //LoadGrdFrequenceMaster();
                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + FreqDelete.FrequencyResult + "');", true);
                LoadGrdFrequenceMaster();
                break;
            }
        }
    }
    #region Clear
    public void AllClear()
    {

        txtFrequencyName.Text = "";
        txtFrequencyShortName.Text = "";
        txtFrequencyDays.Text = "";
       // LoadGrdFrequenceMaster();
        LoadGrdData(FreqList);

    }
    #endregion
    
    #endregion  
    #region Validation
    private int IsValidSave()
    {

        int Error = 0;
        string DisplayError = "";
        if (txtFrequencyName.Text.Trim() == "" || Convert.ToInt32(txtFrequencyName.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError +EComplianceResource.ErrFrequenceName;
            Error = 1;
        }
        if (txtFrequencyShortName.Text.Trim() == "" || Convert.ToInt32(txtFrequencyShortName.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + EComplianceResource.ErrFrequenceShortName;
            Error = 1;
        }
        if (txtFrequencyDays.Text.Trim() == "" || Convert.ToInt32(txtFrequencyDays.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + EComplianceResource.ErrFrequenceDays;
            Error = 1;
        }
        if(!valid.IsPositiveNumber(txtFrequencyDays.Text.Trim()))
        {
            DisplayError = DisplayError + "--" + EComplianceResource.ErrPositiveNumbers;
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
       
        return Error;
    }

    private int IsValidGridSave(string FrequencyName,string FrequencyShortName,string FrequencyDays)
    {

        int Error = 0;
        string DisplayError = "";
        if (FrequencyName.Trim() == "" || Convert.ToInt32(FrequencyName.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + EComplianceResource.ErrFrequenceName;
            Error = 1;
        }
        if (FrequencyShortName.Trim() == "" || Convert.ToInt32(FrequencyShortName.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + EComplianceResource.ErrFrequenceShortName;
            Error = 1;
        }
        if (FrequencyDays.Trim() == "" || Convert.ToInt32(FrequencyDays.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + EComplianceResource.ErrFrequenceDays;
            Error = 1;
        }
        if (!valid.IsPositiveNumber(FrequencyDays.Trim()))
        {
            DisplayError = DisplayError + "--" + EComplianceResource.ErrPositiveNumbers;
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