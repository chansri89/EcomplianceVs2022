using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Resources;

public partial class DepartmentMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    public static  List<DepartmentMsg> DeptList = new List<DepartmentMsg>();
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
            LoadGrdDepartmentMaster();
            if (DeptList.Count == 0)
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
                DeptSave();
            }
        }
    }
    #region GrdEdit

    protected void GrdDepartmentMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        GridViewRow row = GrdDepartmentMaster.Rows[UpdateIndex];
        TextBox txtdeptname = (TextBox)row.FindControl("txtDeptName");
        TextBox txtdeptshname = (TextBox)row.FindControl("txtDeptShName");
        if (IsValidGridSave(txtdeptname.Text.Trim(), txtdeptshname.Text.Trim()) == 0)
        {
            DeptUpdate();
        }
    }
    protected void GrdDepartmentMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DeleteIndex = e.RowIndex;
        DeptDelete();
    }
    protected void GrdDepartmentMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdDepartmentMaster.EditIndex = -1;
        LoadGrdDepartmentMaster();
        //LoadGrdData(DeptList);
    }
    protected void GrdDepartmentMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdDepartmentMaster.EditIndex = e.NewEditIndex;
        //LoadGrdDepartmentMaster();
        LoadGrdData(DeptList);
        GridViewRow row = GrdDepartmentMaster.Rows[GrdDepartmentMaster.EditIndex];
        TextBox DeptName = (TextBox)row.FindControl("txtDeptName");
        DeptName.Focus();
    }

    #endregion
    #endregion
    #region Methods
    private void LoadGrdDepartmentMaster()
    {
        DepartmentMsg Dept = new DepartmentMsg();
        Dept.Flag = "R";
        DeptList = Bus.MasDepartmentInsertUpdateandDelete(Dept);
        LoadGrdData(DeptList);
        //GrdDepartmentMaster.DataSource = "";
        //GrdDepartmentMaster.DataSource = DeptList;
        //GrdDepartmentMaster.DataBind();
        //if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        //{
        //    GrdDepartmentMaster.Columns[GrdDepartmentMaster.Columns.Count - 2].Visible = false;
        //}
        //if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        //{
        //    GrdDepartmentMaster.Columns[GrdDepartmentMaster.Columns.Count - 1].Visible = false;
        //}
    }
    private void LoadGrdData(List<DepartmentMsg> DeptList)//added by abinayaa 110913
    {
        GrdDepartmentMaster.DataSource = "";
        GrdDepartmentMaster.DataSource = DeptList;
        GrdDepartmentMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdDepartmentMaster.Columns[GrdDepartmentMaster.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdDepartmentMaster.Columns[GrdDepartmentMaster.Columns.Count - 1].Visible = false;
        }
    }
    private void DeptSave()
    {
         DepartmentMsg Dept = new DepartmentMsg();
         Dept.Flag = "I";
         Dept.DepartmentName = txtDepartmentName.Text.Trim();
         Dept.DepartmentShortName = txtDepartmentShortName.Text.Trim();
         Dept.CreatedBy = BaseMsg.EmployeeCode;
         DeptList = Bus.MasDepartmentInsertUpdateandDelete(Dept);
        //Output Dispay
         foreach (DepartmentMsg  DeptSave in DeptList)
        {
            if (DeptSave.DeptResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.SuccessFullySaved + "');", true);
                AllClear();
                GrdDepartmentMaster.Visible = true; 
                Pnlgv.Visible = true;

                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DeptSave.DeptResult + "');", true);
                LoadGrdDepartmentMaster();
                break;
            }
        }
    }
    public void DeptUpdate()
    {
        GridViewRow row = GrdDepartmentMaster.Rows[UpdateIndex];
        DepartmentMsg Dept = new DepartmentMsg();
        Dept.Flag = "U";
        TextBox txtdeptId = (TextBox)row.FindControl("txtDeptId");
        TextBox txtdeptname = (TextBox)row.FindControl("txtDeptName");
        TextBox txtdeptshname = (TextBox)row.FindControl("txtDeptShName");
        CheckBox chkActive = (CheckBox)row.FindControl("chkIsActive");

        Dept.DepartmentId = Convert.ToInt32(txtdeptId.Text.Trim());
        Dept.DepartmentName = txtdeptname.Text.Trim();
        Dept.DepartmentShortName = txtdeptshname.Text.Trim();
        Dept.IsActive = chkActive.Checked;
        Dept.CreatedBy = BaseMsg.EmployeeCode;
        DeptList = Bus.MasDepartmentInsertUpdateandDelete(Dept);
        

        foreach (DepartmentMsg DeptUpdate in DeptList)
        {
            if (DeptUpdate.DeptResult== "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.DeptUpdatedSuccessfully + "');", true);
                GrdDepartmentMaster.EditIndex = -1;
                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DeptUpdate.DeptResult + "');", true);
                txtdeptname.Focus();
                //LoadGrdDepartmentMaster();
                break;
            }
        }
    }
    public void DeptDelete()
    {
        DepartmentMsg Dept = new DepartmentMsg();
        Dept.Flag = "D";
        GridViewRow gvr = GrdDepartmentMaster.Rows[DeleteIndex];
        Label lbldeptId = (Label)gvr.FindControl("lblDeptId");
        Label lbldeptName = (Label)gvr.FindControl("lblDeptName");
        Label lbldeptshName = (Label)gvr.FindControl("lblDeptShName");
        CheckBox chkActive = (CheckBox)gvr.FindControl("chkIsActive");

        Dept.DepartmentId = Convert.ToInt32(lbldeptId.Text.Trim());
        Dept.DepartmentName = lbldeptName.Text.Trim();
        Dept.DepartmentShortName = lbldeptshName.Text.Trim();
        Dept.CreatedBy = BaseMsg.EmployeeCode;
        DeptList = Bus.MasDepartmentInsertUpdateandDelete(Dept);
        foreach (DepartmentMsg DeptDelete in DeptList)
        {
            if (DeptDelete.DeptResult == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.DeptDeletedSuccessfully + "');", true);
                //LoadGrdDepartmentMaster();
                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DeptDelete.DeptResult + "');", true);
                LoadGrdDepartmentMaster();
                break;
            }
        }
    }
    #region Clear
    public void AllClear()
    {

        txtDepartmentName.Text = "";
        txtDepartmentShortName.Text = "";
        //LoadGrdDepartmentMaster();
        LoadGrdData(DeptList);
    }
    #endregion
    #endregion   
    #region Validation
    private int IsValidSave()
    {
        int Error = 0;
        string DisplayError = "";
        if (txtDepartmentName.Text.Trim() == "" || Convert.ToInt32(txtDepartmentName.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + EComplianceResource.ErrDeptName;
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ErrDeptName + "');", true);
            Error = 1;
        }
        if (txtDepartmentShortName.Text.Trim() == "" || Convert.ToInt32(txtDepartmentShortName.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + EComplianceResource.ErrDeptShortName;
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.ErrDeptShortName + "');", true);
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }

    private int IsValidGridSave(string DepartmentName,string DepartmentShortName)
    {
        int Error = 0;
        string DisplayError = "";
        if (DepartmentName.Trim() == "" || Convert.ToInt32(DepartmentName.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + EComplianceResource.ErrDeptName;
            Error = 1;
        }
        if (DepartmentShortName.Trim() == "" || Convert.ToInt32(DepartmentShortName.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + EComplianceResource.ErrDeptShortName;
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