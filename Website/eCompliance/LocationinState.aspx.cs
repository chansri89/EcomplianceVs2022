using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Ganini.Lib;
using Resources;
public partial class LocationinState: System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    List<LocationinStateMasterMsg> LocStateList = new List<LocationinStateMasterMsg>();  
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
            LoadGrdLocState();
            if (LocStateList.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.MsgForGrdnotLoad + "');", true);
                Pnlgv.Visible = false;
                pnlAdd.Visible = true;

            }

        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
      

        if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.CreatePermissionRestricted + "');", true);
        }
        else
        {
            if (IsValidSave() == 0)
            {
                LocSave();
            }
        }
    }
    #endregion
    #region GrdEdit

    protected void GrdLocState_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdLocState.EditIndex = e.NewEditIndex;
        LoadGrdLocState();
        GridViewRow row = GrdLocState.Rows[GrdLocState.EditIndex];
        TextBox LocationinState = (TextBox)row.FindControl("txtLocationinState");
        LocationinState.Focus();
    }
    protected void GrdLocState_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        GridViewRow row = GrdLocState.Rows[UpdateIndex];
        string LocationinState = ((TextBox)row.FindControl("txtLocationinState")).Text;
        if (IsValidGrid(LocationinState.Trim()) == 0)
        {
            LocUpdate();
            
        }
    }
    protected void GrdLocState_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DeleteIndex = e.RowIndex;
        LocDelete();
        
    }
    protected void GrdLocState_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdLocState.EditIndex = -1;
        LoadGrdLocState();
    }

    #endregion
    #region Methods
    private void LoadGrdLocState()
    {
        LocationinStateMasterMsg Location = new LocationinStateMasterMsg();
        Location.Flag = "R";
        LocStateList = Bus.MasLocationinStateInsertUpdateandDelete(Location);
        GrdLocState.DataSource = "";
        GrdLocState.DataSource = LocStateList;
        GrdLocState.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdLocState.Columns[GrdLocState.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdLocState.Columns[GrdLocState.Columns.Count - 1].Visible = false;
        }
    }
    private void LocSave()
    {
        LocationinStateMasterMsg Location = new LocationinStateMasterMsg();
        Location.Flag = "I";
        Location.LocationinState = txtLocationinState.Text.Trim();
        Location.CreatedBy = BaseMsg.EmployeeCode;
        LocStateList = Bus.MasLocationinStateInsertUpdateandDelete(Location);
        //Output Dispay
        foreach (LocationinStateMasterMsg LocSave in LocStateList)
        {
            if (LocSave.LocationinStateResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.SuccessFullySaved + "');", true);
                
                AllClear();
                if (LocStateList.Count > 0)
                {
                    Pnlgv.Visible = true;
                    pnlAdd.Visible = true;
                }
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + LocSave.LocationinStateResult + "');", true);
                break;
            }
        }
    }
    public void LocUpdate()
    {
        GridViewRow row = GrdLocState.Rows[UpdateIndex];
        LocationinStateMasterMsg Location = new LocationinStateMasterMsg();
        Location.Flag = "U";
        TextBox txtLocationinStateId = (TextBox)row.FindControl("txtLocationinStateId");
        TextBox txtLocationinState = (TextBox)row.FindControl("txtLocationinState");
        CheckBox chkActive = (CheckBox)row.FindControl("chkIsActive");

        Location.LocationinStateId = Convert.ToInt32(txtLocationinStateId.Text.Trim());
        Location.LocationinState = txtLocationinState.Text.Trim();
        Location.IsActive = chkActive.Checked;
        Location.CreatedBy = BaseMsg.EmployeeCode;
        LocStateList = Bus.MasLocationinStateInsertUpdateandDelete(Location);
        GrdLocState.EditIndex = -1;

        foreach (LocationinStateMasterMsg Locpdate in LocStateList)
        {
            if (Locpdate.LocationinStateResult == "0")
            {
                //Commented by Madhavi on 10/10/2012
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.UpdatedSuccessfully + "');", true);

                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + Locpdate.LocationinStateResult + "');", true);
                break;
            }
        }
    }
    public void LocDelete()
    {
        LocationinStateMasterMsg Location = new LocationinStateMasterMsg();
        Location.Flag = "D";
        GridViewRow gvr = GrdLocState.Rows[DeleteIndex];
        Label lblLocationinStateId = (Label)gvr.FindControl("lblLocationinStateId");
        Label lblLocationinState = (Label)gvr.FindControl("lblLocationinState");
        CheckBox chkActive = (CheckBox)gvr.FindControl("chkActive");

        Location.LocationinStateId = Convert.ToInt32(lblLocationinStateId.Text.Trim());
        Location.LocationinState = lblLocationinState.Text.Trim();        
        Location.CreatedBy = BaseMsg.EmployeeCode;
        LocStateList = Bus.MasLocationinStateInsertUpdateandDelete(Location);
        foreach (LocationinStateMasterMsg LocDelete in LocStateList)
        {
            if (LocDelete.LocationinStateResult == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.DeletedSuccessfully + "');", true);
                LoadGrdLocState();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + LocDelete.LocationinStateResult + "');", true);
                break;
            }
        }
    }
    #endregion
    #region Clear
    public void AllClear()
    {

        txtLocationinState.Text = "";
        LoadGrdLocState();

    }
    #endregion
    #region Validation
    private int IsValidSave()
    {

        int Error = 0;
        if (txtLocationinState.Text.Trim() == "" || Convert.ToInt32(txtLocationinState.Text.Length.ToString().Trim()) == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.ErrLocationinState + "');", true);
            Error = 1;
        }

        return Error;
    }
    private int IsValidGrid(string LocationinState)
    {
        int Error = 0;

        if (LocationinState.Trim() == "" || Convert.ToInt32(LocationinState.Trim().ToString().Length) == 0)
        {
            Error = 1;
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.ErrLocationinState + "');", true);
            return Error;
        }
        return Error;
    }
   
    #endregion


    
}