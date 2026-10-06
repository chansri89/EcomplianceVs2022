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
public partial class SeverityMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    public static List<SeverityMasterMsg> SevList = new List<SeverityMasterMsg>();
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
            LoadGrdSeverityMaster();
            if (SevList.Count == 0)
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
                SeveritySave();
            }
        }
    }
    #region GrdEdit
    protected void GrdSeverityMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        GridViewRow row = GrdSeverityMaster.Rows[UpdateIndex];
        TextBox txtSevtyname = (TextBox)row.FindControl("txtSevName");
        TextBox txtSevtyshname = (TextBox)row.FindControl("txtSevShName");
        TextBox txtSevtyRemarks = (TextBox)row.FindControl("txtRemarks");
        if (IsValidGridSave(txtSevtyname.Text.Trim(), txtSevtyshname.Text.Trim(), txtSevtyRemarks.Text.Trim()) == 0)
        {
            SeverityUpdate();
        }
    }
    protected void GrdSeverityMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DeleteIndex = e.RowIndex;
        SeverityDelete();
    }
    protected void GrdSeverityMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdSeverityMaster.EditIndex = -1;
        LoadGrdSeverityMaster();
    }
    protected void GrdSeverityMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdSeverityMaster.EditIndex = e.NewEditIndex;
       // LoadGrdSeverityMaster();
        LoadGrdData(SevList);
        GridViewRow row = GrdSeverityMaster.Rows[GrdSeverityMaster.EditIndex];
        TextBox SevName = (TextBox)row.FindControl("txtSevName");
        SevName.Focus();
    }

    #endregion
    #endregion
    #region Methods
    private void LoadGrdSeverityMaster()
    {
        SeverityMasterMsg Sevty = new SeverityMasterMsg();
        Sevty.Flag = "R";
        SevList = Bus.MasSeverityInsertUpdateandDelete(Sevty);
        LoadGrdData(SevList);
        //GrdSeverityMaster.DataSource = "";
        //GrdSeverityMaster.DataSource = SevList;
        //GrdSeverityMaster.DataBind();
        //if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        //{
        //    GrdSeverityMaster.Columns[GrdSeverityMaster.Columns.Count - 2].Visible = false;
        //}
        //if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        //{
        //    GrdSeverityMaster.Columns[GrdSeverityMaster.Columns.Count - 1].Visible = false;
        //}
    }
    private void LoadGrdData(List<SeverityMasterMsg> SevList)//added by sai 280913 to avoid double time loading the grid
    {
        GrdSeverityMaster.DataSource = "";
        GrdSeverityMaster.DataSource = SevList;
        GrdSeverityMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdSeverityMaster.Columns[GrdSeverityMaster.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdSeverityMaster.Columns[GrdSeverityMaster.Columns.Count - 1].Visible = false;
        }
    }

    private void SeveritySave()
    {
        SeverityMasterMsg Sevty = new SeverityMasterMsg();
        Sevty.Flag = "I";
        Sevty.SeverityName = txtSeverityName.Text.Trim();
        Sevty.SeverityShortName = txtSeverityShort.Text.Trim();
        Sevty.Remarks = txtRemarks.Text.Trim();
        Sevty.CreatedBy = BaseMsg.EmployeeCode;
        SevList = Bus.MasSeverityInsertUpdateandDelete(Sevty);
        //Output Dispay
        foreach (SeverityMasterMsg SevtySave in SevList)
        {
            if (SevtySave.SeverityResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.SuccessFullySaved + "');", true);
                AllClear();
                GrdSeverityMaster.Visible = true;
                Pnlgv.Visible = true;
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + SevtySave.SeverityResult + "');", true);
                LoadGrdSeverityMaster();
                break;
            }
        }
    }
    public void SeverityUpdate()
    {
        GridViewRow row = GrdSeverityMaster.Rows[UpdateIndex];
        SeverityMasterMsg Sevty = new SeverityMasterMsg();
        Sevty.Flag = "U";
        TextBox txtSevtytId = (TextBox)row.FindControl("txtSevId");
        TextBox txtSevtyname = (TextBox)row.FindControl("txtSevName");
        TextBox txtSevtyshname = (TextBox)row.FindControl("txtSevShName");
        TextBox txtSevtyRemarks = (TextBox)row.FindControl("txtRemarks");
        CheckBox chkActive = (CheckBox)row.FindControl("chkIsActive");

        Sevty.SeverityId = Convert.ToInt32(txtSevtytId.Text.Trim());
        Sevty.SeverityName = txtSevtyname.Text.Trim();
        Sevty.SeverityShortName = txtSevtyshname.Text.Trim();
        Sevty.Remarks = txtSevtyRemarks.Text.Trim();
        Sevty.IsActive = chkActive.Checked;
        Sevty.CreatedBy = BaseMsg.EmployeeCode;
        SevList = Bus.MasSeverityInsertUpdateandDelete(Sevty);
      

        foreach (SeverityMasterMsg SevtyUpdate in SevList)
        {
            if (SevtyUpdate.SeverityResult== "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.SeverityUpdatedSuccessfully + "');", true);
                GrdSeverityMaster.EditIndex = -1;
                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + SevtyUpdate.SeverityResult + "');", true);
                txtSevtyname.Focus();
                break;
            }
        }
    }
    public void SeverityDelete()
    {
        SeverityMasterMsg Sevty = new SeverityMasterMsg();
        Sevty.Flag = "D";
        GridViewRow gvr = GrdSeverityMaster.Rows[DeleteIndex];
        Label lblSevtyId = (Label)gvr.FindControl("lblSevtId");
        Label lblSevtyName = (Label)gvr.FindControl("lblSevName");
        Label lblSevtyshName = (Label)gvr.FindControl("lblSevShName");
        Label lblSevtyRemarks = (Label)gvr.FindControl("lblRemarks");
        CheckBox chkActive = (CheckBox)gvr.FindControl("chkIsActive");

        Sevty.SeverityId = Convert.ToInt32(lblSevtyId.Text.Trim());
        Sevty.SeverityName = lblSevtyName.Text.Trim();
        Sevty.SeverityShortName = lblSevtyshName.Text.Trim();
        Sevty.Remarks = lblSevtyRemarks.Text.Trim();
        Sevty.CreatedBy = BaseMsg.EmployeeCode;
        SevList = Bus.MasSeverityInsertUpdateandDelete(Sevty);
        foreach (SeverityMasterMsg SevtyDelete in SevList)
        {
            if (SevtyDelete.SeverityResult == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.SeverityDeletedSuccessfully+ "');", true);
                //LoadGrdSeverityMaster();
                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + SevtyDelete.SeverityResult + "');", true);
                LoadGrdSeverityMaster();
                break;
            }
        }
    }
    #region Clear
    public void AllClear()
    {

        txtSeverityName.Text = "";
        txtSeverityShort.Text = "";
        txtRemarks.Text = "";
        //LoadGrdSeverityMaster();
        LoadGrdData(SevList);
    }
    #endregion
    #endregion   
    #region Validation
    private int IsValidSave()
    {

        int Error = 0;
        string DisplayError = "";
        if (txtSeverityName.Text.Trim() == "" || Convert.ToInt32(txtSeverityName.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + EComplianceResource.ErrSeverityName;
            Error = 1;
        }
        if (txtSeverityShort.Text.Trim() == "" || Convert.ToInt32(txtSeverityShort.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + EComplianceResource.ErrSeverityShortName;
            Error = 1;
        }
        if (txtRemarks.Text.Trim() == "" || Convert.ToInt32(txtRemarks.Text.Trim().Length.ToString()) == 0)
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

    private int IsValidGridSave(string SeverityName,string SeverityShortName,string Remarks)
    {

        int Error = 0;
        string DisplayError = "";
        if (SeverityName.Trim() == "" || Convert.ToInt32(SeverityName.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + EComplianceResource.ErrSeverityName;
            Error = 1;
        }
        if (SeverityShortName.Trim() == "" || Convert.ToInt32(SeverityShortName.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + EComplianceResource.ErrSeverityShortName;
            Error = 1;
        }
        if (Remarks.Trim() == "" || Convert.ToInt32(Remarks.Trim().Length.ToString()) == 0)
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
   
    
}