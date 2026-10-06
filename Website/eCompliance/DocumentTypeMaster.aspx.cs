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

public partial class DocumentTypeMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    List<DocumentTypeMasterMsg> DocumList = new List<DocumentTypeMasterMsg>();
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
            LoadGrdDocumentTypeMaster();
            if (DocumList.Count == 0)
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
                DocumentSave();
            }
        }
    }
    #region GrdEdit

    protected void GrdDepartmentMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
         GridViewRow row = GrdDocumentMaster.Rows[UpdateIndex];
         TextBox txtDocumname = (TextBox)row.FindControl("txtDocumName");
        TextBox txtDocumshname = (TextBox)row.FindControl("txtDocumhName");
        if (IsValidGridSave(txtDocumname.Text.Trim(), txtDocumshname.Text.Trim()) == 0)
        {
            DocumentUpdate();
        }
    }
    protected void GrdDepartmentMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DeleteIndex = e.RowIndex;
        DocumentDelete();
    }
    protected void GrdDepartmentMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdDocumentMaster.EditIndex = -1;
        LoadGrdDocumentTypeMaster();
    }
    protected void GrdDepartmentMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdDocumentMaster.EditIndex = e.NewEditIndex;
        LoadGrdDocumentTypeMaster();
        GridViewRow row = GrdDocumentMaster.Rows[GrdDocumentMaster.EditIndex];
        TextBox DocumName = (TextBox)row.FindControl("txtDocumName");
        DocumName.Focus();
    }

    #endregion
    #endregion
    #region Methods
    private void LoadGrdDocumentTypeMaster()
    {
        ActivityForCompanyMasterMsg ActivityComp = new ActivityForCompanyMasterMsg();
        DocumentTypeMasterMsg Docum = new DocumentTypeMasterMsg();
        Docum.Flag = "R";
        DocumList = Bus.MasDocumentTypeInsertUpdateandDelete(Docum);
        GrdDocumentMaster.DataSource = "";
        GrdDocumentMaster.DataSource = DocumList;
        GrdDocumentMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdDocumentMaster.Columns[GrdDocumentMaster.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdDocumentMaster.Columns[GrdDocumentMaster.Columns.Count - 1].Visible = false;
        }
    }

    private void DocumentSave()
    {
        ActivityForCompanyMasterMsg ActivityComp = new ActivityForCompanyMasterMsg();
        DocumentTypeMasterMsg Docum = new DocumentTypeMasterMsg();
        Docum.Flag = "I";

        Docum.DocumentTypeName = txtDocumentTypetName.Text.Trim();
        Docum.DocumentTypeShortName = txtDocumentTypeShortName.Text.Trim();
        Docum.CreatedBy = BaseMsg.EmployeeCode;
        DocumList = Bus.MasDocumentTypeInsertUpdateandDelete(Docum);
        //Output Dispay
        foreach (DocumentTypeMasterMsg DocumSave in DocumList)
        {
            if (DocumSave.DocTypeResult== "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.SuccessFullySaved + "');", true);
                GrdDocumentMaster.Visible = true;
                AllClear();
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DocumSave.DocTypeResult + "');", true);
                break;
            }
        }
    }
    public void DocumentUpdate()
    {
        ActivityForCompanyMasterMsg ActivityComp = new ActivityForCompanyMasterMsg();
        GridViewRow row = GrdDocumentMaster.Rows[UpdateIndex];
        DocumentTypeMasterMsg Docum = new DocumentTypeMasterMsg();
        Docum.Flag = "U";

        TextBox txtDocumId = (TextBox)row.FindControl("txtDocumId");
        TextBox txtDocumname = (TextBox)row.FindControl("txtDocumName");
        TextBox txtDocumshname = (TextBox)row.FindControl("txtDocumhName");
        CheckBox chkActive = (CheckBox)row.FindControl("chkIsActive");

        Docum.DocumentTypeId = Convert.ToInt32(txtDocumId.Text.Trim());
        Docum.DocumentTypeName = txtDocumname.Text.Trim();
        Docum.DocumentTypeShortName = txtDocumshname.Text.Trim();
        Docum.IsActive = chkActive.Checked;
        Docum.CreatedBy = BaseMsg.EmployeeCode;
        DocumList = Bus.MasDocumentTypeInsertUpdateandDelete(Docum);
        GrdDocumentMaster.EditIndex = -1;

        foreach (DocumentTypeMasterMsg DocumUpdate in DocumList)
        {
            if (DocumUpdate.DocTypeResult == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.DocumentTypeUpdatedSuccessfully + "');", true);

                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DocumUpdate.DocTypeResult + "');", true);
                break;
            }
        }
    }
    public void DocumentDelete()
    {
        ActivityForCompanyMasterMsg ActivityComp = new ActivityForCompanyMasterMsg();
        DocumentTypeMasterMsg Docum = new DocumentTypeMasterMsg();
        Docum.Flag = "D";
        GridViewRow gvr = GrdDocumentMaster.Rows[DeleteIndex];

        Label lblDocumId = (Label)gvr.FindControl("lblDocumId");
        Label lblDocumName = (Label)gvr.FindControl("lblDocumName");
        Label lblDocumshName = (Label)gvr.FindControl("lblDocumShName");
        CheckBox chkActive = (CheckBox)gvr.FindControl("chkIsActive");

        Docum.DocumentTypeId = Convert.ToInt32(lblDocumId.Text.Trim());
        Docum.DocumentTypeName = lblDocumName.Text.Trim();
        Docum.DocumentTypeShortName = lblDocumshName.Text.Trim();
        Docum.CreatedBy = BaseMsg.EmployeeCode;
        DocumList = Bus.MasDocumentTypeInsertUpdateandDelete(Docum);
        //Output Dispay
        foreach (DocumentTypeMasterMsg DocumDelete in DocumList)
        {
            if (DocumDelete.DocTypeResult == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.DocumentTypeDeletedSuccessfully + "');", true);
                LoadGrdDocumentTypeMaster();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DocumDelete.DocTypeResult + "');", true);
                break;
            }
        }
    }
    #region Clear
    public void AllClear()
    {

        txtDocumentTypetName.Text = "";
        txtDocumentTypeShortName.Text = "";
        LoadGrdDocumentTypeMaster();

    }
    #endregion
    #endregion    
    #region Validation
    private int IsValidSave()
    {

        int Error = 0;
        string DisplayError = "";
        if (txtDocumentTypetName.Text.Trim() == "" || txtDocumentTypetName.Text.Trim().Length.ToString() == "0")
        {
            DisplayError = DisplayError + StackResource.ErrDocumentTypeName;
            Error = 1;
        }
        if (txtDocumentTypeShortName.Text.Trim() == "" || txtDocumentTypeShortName.Text.Trim().Length.ToString() == "0")
        {
            DisplayError = DisplayError + "--" + StackResource.ErrDocumentTypeShortName;
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }

    private int IsValidGridSave(string DocumentTypeName,string DocumentTypShortName)
    {

        int Error = 0;
        string DisplayError = "";
        if (DocumentTypeName.Trim() == "" || DocumentTypeName.Trim().Length.ToString() == "0")
        {
            DisplayError = DisplayError + StackResource.ErrDocumentTypeName;
            Error = 1;
        }
        if (DocumentTypShortName.Trim() == "" || DocumentTypShortName.Trim().Length.ToString() == "0")
        {
            DisplayError = DisplayError + "--" + StackResource.ErrDocumentTypeShortName;
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