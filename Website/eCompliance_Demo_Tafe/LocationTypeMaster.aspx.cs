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

public partial class LocationTypeMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    List<LocationTypeMasterMsg> LocationTypeList = new List<LocationTypeMasterMsg>();
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
            LoadGrdLocationTypeMaster();
            if (LocationTypeList.Count == 0)
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
                LocationTypeSave();
            }
        }
    }
    #endregion
    #region GrdEdit

    protected void GrdLocationTypeMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdLocationTypeMaster.EditIndex = e.NewEditIndex;
        LoadGrdLocationTypeMaster();
        GridViewRow row = GrdLocationTypeMaster.Rows[GrdLocationTypeMaster.EditIndex];
        TextBox LocationTypeName = (TextBox)row.FindControl("txtLocationTypeName");
        LocationTypeName.Focus();
    }
    protected void GrdLocationTypeMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        GridViewRow row = GrdLocationTypeMaster.Rows[UpdateIndex];
        TextBox txtlocationTypeName = (TextBox)row.FindControl("txtLocationTypeName");
        TextBox txtlocationTypeshortName = (TextBox)row.FindControl("txtLocationTypeshortName");
        if (IsValidGridSave(txtlocationTypeName.Text.Trim(), txtlocationTypeshortName.Text.Trim()) == 0)
        {
            LocationTypeUpdate();
        }
    }
    protected void GrdLocationTypeMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DeleteIndex = e.RowIndex;
        LocationTypeDelete();
    }
    protected void GrdLocationTypeMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdLocationTypeMaster.EditIndex = -1;
        LoadGrdLocationTypeMaster();
    }
    #endregion
    #region Methods
    private void LoadGrdLocationTypeMaster()
    {
        LocationTypeMasterMsg LocationType = new LocationTypeMasterMsg();
        LocationType.Flag = "R";
        LocationTypeList = Bus.MasLocationTypeInsertUpdateandDelete(LocationType);
        GrdLocationTypeMaster.DataSource = "";
        GrdLocationTypeMaster.DataSource = LocationTypeList;
        GrdLocationTypeMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdLocationTypeMaster.Columns[GrdLocationTypeMaster.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdLocationTypeMaster.Columns[GrdLocationTypeMaster.Columns.Count - 1].Visible = false;
        }
    }
 
    private void LocationTypeSave()
    {
        LocationTypeMasterMsg LocationType = new LocationTypeMasterMsg();
        LocationType.Flag = "I";
        LocationType.LocationTypeName = txtLocationType.Text.Trim();
        LocationType.LocationTypeShortName = txtLocationTypeShName.Text.Trim();
        //LocationType.IsActive = true;
        LocationType.CreatedBy = BaseMsg.EmployeeCode;
        LocationTypeList = Bus.MasLocationTypeInsertUpdateandDelete(LocationType);
        //Output Dispay
        foreach (LocationTypeMasterMsg CatgSave in LocationTypeList)
        {
            if (CatgSave.LocationTypeResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.SuccessFullySaved + "');", true);
                AllClear();
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + CatgSave.LocationTypeResult + "');", true);
                break;
            }
        }
    }
    public void LocationTypeUpdate()
    {
        GridViewRow row = GrdLocationTypeMaster.Rows[UpdateIndex];
        LocationTypeMasterMsg LocationType = new LocationTypeMasterMsg();
        LocationType.Flag = "U";
        TextBox txtLocationTypeId = (TextBox)row.FindControl("txtLocationTypeId");
        TextBox txtLocationTypeName = (TextBox)row.FindControl("txtLocationTypeName");
        TextBox txtLocationTypeshortName = (TextBox)row.FindControl("txtLocationTypeshortName");
        CheckBox chkActive = (CheckBox)row.FindControl("chkIsActive");

        LocationType.LocationTypeName = txtLocationTypeName.Text.Trim();
        LocationType.LocationTypeShortName = txtLocationTypeshortName.Text.Trim();
        LocationType.LocationTypeId = Convert.ToInt32(txtLocationTypeId.Text.Trim());
        LocationType.IsActive = chkActive.Checked;
        LocationType.CreatedBy = BaseMsg.EmployeeCode;
        LocationTypeList = Bus.MasLocationTypeInsertUpdateandDelete(LocationType);
        GrdLocationTypeMaster.EditIndex = -1;

        foreach (LocationTypeMasterMsg LocationTypeUpdate in LocationTypeList)
        {
            if (LocationTypeUpdate.LocationTypeResult == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.LocationTypeUpdatedSuccessfully + "');", true);

                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + LocationTypeUpdate.LocationTypeResult + "');", true);
                break;
            }
        }
    }
    public void LocationTypeDelete()
    {
        LocationTypeMasterMsg LocationType = new LocationTypeMasterMsg();
        LocationType.Flag = "D";
        GridViewRow gvr = GrdLocationTypeMaster.Rows[DeleteIndex];
        Label lblLocationTypeId = (Label)gvr.FindControl("lblLocationTypeId");
        Label lblLocationTypeName = (Label)gvr.FindControl("lblLocationTypeName");
        Label lblLocationTypeshortName = (Label)gvr.FindControl("lblLocationTypeshortName");
        CheckBox chkActive = (CheckBox)gvr.FindControl("chkActive");

        LocationType.LocationTypeId = Convert.ToInt32(lblLocationTypeId.Text.Trim());
        LocationType.LocationTypeName = lblLocationTypeName.Text.Trim();
        LocationType.LocationTypeShortName = lblLocationTypeshortName.Text.Trim();
        LocationType.CreatedBy = BaseMsg.EmployeeCode;
        LocationTypeList = Bus.MasLocationTypeInsertUpdateandDelete(LocationType);
        foreach (LocationTypeMasterMsg LocationTypeDelete in LocationTypeList)
        {
            if (LocationTypeDelete.LocationTypeResult == "0")
            {
                // ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.LocationTypeDeletedSuccessfully + "');", true);
                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + LocationTypeDelete.LocationTypeResult + "');", true);
                break;
            }
        }
    }
    #endregion
    #region Clear
    public void AllClear()
    {

        txtLocationType.Text = "";
        txtLocationTypeShName.Text = "";
        LoadGrdLocationTypeMaster();
        
    }
    #endregion
    #region Validation
    private int IsValidSave()
    {
        
        string DisplayError = "";
        int Error = 0;
        if (txtLocationType.Text.Trim() == "" || Convert.ToInt32(txtLocationType.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + StackResource.ErrLocationTypeName;
            Error = 1;
        }
        if (txtLocationTypeShName.Text.Trim() == "" || Convert.ToInt32(txtLocationTypeShName.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--"+StackResource.ErrLocationTypeshName;
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }

    private int IsValidGridSave(string LocationTypeName, string LocationTypeshName)
    {

        int Error = 0;
        string DisplayError = "";
        if (LocationTypeName.Trim() == "" || Convert.ToInt32(LocationTypeName.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + StackResource.ErrLocationTypeName;
            Error = 1;
        }
        if (LocationTypeshName.Trim() == "" || Convert.ToInt32(LocationTypeshName.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrLocationTypeshName;
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
