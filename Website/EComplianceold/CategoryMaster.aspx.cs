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

public partial class CategoryMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    public static List<CategoryMasterMsg> CategoryList = new List<CategoryMasterMsg>();
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
            LoadGrdCategoryMaster();
            if (CategoryList.Count == 0)
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
                CategorySave();
            }
        }
    }
    #endregion
    #region GrdEdit

    protected void GrdCategoryMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdCategoryMaster.EditIndex = e.NewEditIndex;      
        //LoadGrdCategoryMaster();
        LoadGrdData(CategoryList);
        GridViewRow row = GrdCategoryMaster.Rows[GrdCategoryMaster.EditIndex];
        TextBox CategoryName = (TextBox)row.FindControl("txtCategoryName");
        CategoryName.Focus();
    }
    protected void GrdCategoryMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        GridViewRow row = GrdCategoryMaster.Rows[UpdateIndex];
        TextBox txtcategname = (TextBox)row.FindControl("txtCategoryName");
        TextBox txtcategoryfullName = (TextBox)row.FindControl("txtCategoryFullName");
        if (IsValidGridSave(txtcategname.Text.Trim(), txtcategoryfullName.Text.Trim()) == 0)
        {
            CategoryUpdate();
        }
    }
    protected void GrdCategoryMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DeleteIndex = e.RowIndex;
        CategoryDelete();
    }
    protected void GrdCategoryMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdCategoryMaster.EditIndex = -1;
        LoadGrdCategoryMaster();
    }
    #endregion
    #region Methods
    private void LoadGrdCategoryMaster()
    {
        CategoryMasterMsg Category = new CategoryMasterMsg();
        Category.Flag = "R";
        CategoryList = Bus.MasCategoryInsertUpdateandDelete(Category);
        LoadGrdData(CategoryList);
        //GrdCategoryMaster.DataSource = "";
        //GrdCategoryMaster.DataSource = CategoryList;
        //GrdCategoryMaster.DataBind();
        //if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        //{
        //    GrdCategoryMaster.Columns[GrdCategoryMaster.Columns.Count - 2].Visible = false;
        //}
        //if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        //{
        //    GrdCategoryMaster.Columns[GrdCategoryMaster.Columns.Count - 1].Visible = false;
        //}
    }
    private void LoadGrdData(List<CategoryMasterMsg> CategoryList)//added by abinayaa 110913
    {
        GrdCategoryMaster.DataSource = "";
        GrdCategoryMaster.DataSource = CategoryList;
        GrdCategoryMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdCategoryMaster.Columns[GrdCategoryMaster.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdCategoryMaster.Columns[GrdCategoryMaster.Columns.Count - 1].Visible = false;
        }
    }
    private void CategorySave()
    {
        CategoryMasterMsg Category = new CategoryMasterMsg();
        Category.Flag = "I";
        Category.CategoryName = txtCategoryName.Text.Trim();
        Category.CategoryFullName = txtCategoryfullName.Text.Trim();
        Category.CreatedBy = BaseMsg.EmployeeCode;
        CategoryList = Bus.MasCategoryInsertUpdateandDelete(Category);
        //Output Dispay
        foreach (CategoryMasterMsg CatgSave in CategoryList)
        {
            if (CatgSave.CategoryResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.SuccessFullySaved + "');", true);
                AllClear();
                GrdCategoryMaster.Visible = true;
                Pnlgv.Visible = true;
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + CatgSave.CategoryResult + "');", true);
                LoadGrdCategoryMaster();
                break;
            }
        }
    }
    public void CategoryUpdate()
    {
        GridViewRow row = GrdCategoryMaster.Rows[UpdateIndex];
        CategoryMasterMsg Category = new CategoryMasterMsg();
        Category.Flag = "U";
        TextBox txtcatgId = (TextBox)row.FindControl("txtCategoryId");
        TextBox txtcategname = (TextBox)row.FindControl("txtCategoryName");
        TextBox txtCategoryFullName = (TextBox)row.FindControl("txtCategoryFullName");
        CheckBox chkActive = (CheckBox)row.FindControl("chkIsActive");

        Category.CategoryName = txtcategname.Text.Trim();
        Category.CategoryFullName = txtCategoryFullName.Text.Trim();
        Category.CategoryId = Convert.ToInt32(txtcatgId.Text.Trim());
        Category.IsActive = chkActive.Checked;
        Category.CreatedBy = BaseMsg.EmployeeCode;
        CategoryList = Bus.MasCategoryInsertUpdateandDelete(Category);
        

        foreach (CategoryMasterMsg CategoryUpdate in CategoryList)
        {
            if (CategoryUpdate.CategoryResult == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.CategoryUpdatedSuccessfully + "');", true);
                GrdCategoryMaster.EditIndex = -1;
                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + CategoryUpdate.CategoryResult + "');", true);
                txtcategname.Focus();
                break;
            }
        }
    }
    public void CategoryDelete()
    {
        CategoryMasterMsg Category = new CategoryMasterMsg();
        Category.Flag = "D";
        GridViewRow gvr = GrdCategoryMaster.Rows[DeleteIndex];
        Label lblCatgId = (Label)gvr.FindControl("lblCategoryId");
        Label lblCatgName = (Label)gvr.FindControl("lblCategoryName");
        Label lblCategoryFullName = (Label)gvr.FindControl("lblCategoryFullName");
        CheckBox chkActive = (CheckBox)gvr.FindControl("chkActive");

        Category.CategoryId = Convert.ToInt32(lblCatgId.Text.Trim());
        Category.CategoryName = lblCatgName.Text.Trim();
        Category.CategoryFullName = lblCategoryFullName.Text.Trim();     
        Category.CreatedBy = BaseMsg.EmployeeCode;
        CategoryList = Bus.MasCategoryInsertUpdateandDelete(Category);
        foreach (CategoryMasterMsg CategoryDelete in CategoryList)
        {
            if (CategoryDelete.CategoryResult == "0")
            {
               // ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EComplianceResource.CategoryDeletedSuccessfully + "');", true);
                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + CategoryDelete.CategoryResult + "');", true);
                LoadGrdCategoryMaster();
                break;
            }
        }
    }
    #endregion
    #region Clear
    public void AllClear()
    {

        txtCategoryName.Text = "";
        txtCategoryfullName.Text = "";
        //LoadGrdCategoryMaster();
        LoadGrdData(CategoryList);

    }
    #endregion
    #region Validation
    private int IsValidSave()
    {
        
        string DisplayError = "";
        int Error = 0;
        if (txtCategoryName.Text.Trim() == "" || Convert.ToInt32(txtCategoryName.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + EComplianceResource.ErrCategoryName;
            Error = 1;
        }
        if (txtCategoryfullName.Text.Trim() == "" || Convert.ToInt32(txtCategoryfullName.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--"+EComplianceResource.ErrCategoryfullName;
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }

    private int IsValidGridSave(string CategoryName, string CategoryFullName)
    {

        int Error = 0;
        string DisplayError = "";
        if (CategoryName.Trim() == "" || Convert.ToInt32(CategoryName.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + EComplianceResource.ErrCategoryName;
            Error = 1;
        }
        if (CategoryFullName.Trim() == "" || Convert.ToInt32(CategoryFullName.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + EComplianceResource.ErrCategoryfullName;
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
