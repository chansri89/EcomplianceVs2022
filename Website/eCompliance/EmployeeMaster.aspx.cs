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
using Ganini.Security;

public partial class EmployeeMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    List<CompanyMessage> CompList = new List<CompanyMessage>();
    public static List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    public static int DeleteIndex = 0;
    public static int UpdateIndex = 0;
    UserAccess user = new UserAccess();
    public static string ProgramName = string.Empty;
    BaseClass BaseMsg = new BaseClass();
    KeyGen KeyGen = new KeyGen();
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
       {
           LoadFilterEmployee();
           LoadGridEmployees();
           LoadCompanyName();
           LoadManagerName();
           if (EmpList.Count == 0)
           {
               ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.MsgForGrdnotLoad + "');", true);
               Pnlgv.Visible = false;
               pnlAdd.Visible = true;

           }
       }
       
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       // For Deleting a Record from Grid-Starts
        if (HidDeleteCount.Value == "1")
        {
            EmployeeDelete();
            HidDeleteCount.Value = "0";
            return;
        }

        ///For Deleting a Record from Grid-Ends
        ///For Updating a Record from Grid-Starts
        if (HidUpdateCount.Value == "1")
        {
            EmployeeUpdate();
            HidUpdateCount.Value = "0";
            return;
        }
        ///For Deleting a Record from Grid-Ends
        if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.CreatePermissionRestricted + "');", true);
        }
        else
        {
            if (IsValidSave() == 0)
            {
                EmployeeSave();
            }
        }
        
    }
    protected void GrdEmployeeMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdEmployeeMaster.EditIndex = e.NewEditIndex;
        GridViewRow row = GrdEmployeeMaster.Rows[GrdEmployeeMaster.EditIndex];
        Label lblCompname = (Label)row.FindControl("lblCompanyName");
        Label lblManagername = (Label)row.FindControl("lblManagerName");
        EmployeeMasterMsg emp = new EmployeeMasterMsg();
       
        if (ddlLoadEmp.SelectedIndex == 0)
        {
            LoadGridEmployees();
        }
        else
        {
            LoadFilterEmployees();
        }
        foreach (GridViewRow gvr in GrdEmployeeMaster.Rows)
        {
            if (gvr.RowIndex == GrdEmployeeMaster.EditIndex)
            {
                DropDownList ddlcmpname = (DropDownList)gvr.FindControl("ddlCompanyName");
                // ddlcmpname.Items.Insert(0, new ListItem("--Parent--"));
                foreach (EmployeeMasterMsg Emp in EmpList)
                {
                    if (Emp.CompanyName == lblCompname.Text)
                    {
                        ddlcmpname.SelectedValue = Emp.CompanyCode;
                        ddlcmpname.Focus();
                        break;
                    }
                    ddlcmpname.SelectedIndex = 0;
                }
                DropDownList ddlmanagername = (DropDownList)gvr.FindControl("ddlManagerName");

                foreach (EmployeeMasterMsg EmpEdit in EmpList)
                {
                    if (EmpEdit.ManagerName.Trim() == lblManagername.Text.Trim() && EmpEdit.ManagerName.Trim() != "")
                    {
                        ddlmanagername.SelectedValue = EmpEdit.ManagerCode.ToString();
                        break;
                    }                 
                }
                if (lblManagername.Text.Trim() == string.Empty)
                {
                    ddlmanagername.Items.Insert(0, new ListItem("--Not Applicable--", "0"));
                    ddlmanagername.SelectedIndex = 0;
                }

            }
        }
        
    }
    protected void GrdEmployeeMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdEmployeeMaster.EditIndex = -1;
        LoadGridEmployees();
    }
    protected void GrdEmployeeMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DeleteIndex = e.RowIndex;
        EmployeeDelete();
    }
    protected void GrdEmployeeMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        GridViewRow row = GrdEmployeeMaster.Rows[UpdateIndex];
        TextBox txtEmpCode = (TextBox)row.FindControl("txtEmployeeCode");
        TextBox txtEmpname = (TextBox)row.FindControl("txtEmpname");
        TextBox txtEmailId = (TextBox)row.FindControl("txtEmailId");
        TextBox txtActivityDesg = (TextBox)row.FindControl("txtActivityDesignation");
        if (IsValidGridSave(txtEmpCode.Text.Trim(),txtEmpname.Text.Trim(),txtEmailId.Text.Trim(),txtActivityDesg.Text.Trim()) == 0)
        {
            EmployeeUpdate();
        }
    }   
    protected void btnGo_Click(object sender, EventArgs e)
    {
        GrdEmployeeMaster.Visible = true;
        
        if (ddlLoadEmp.SelectedIndex == 0)
        {
            LoadGridEmployees();
        }
        else
        {
            LoadFilterEmployees();
        }
    }
    #endregion
    #region Methods
    public void LoadGridEmployees() //To load data into grid.
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.Flag = "R";
        Emp.EmployeeCode =BaseMsg.EmployeeCode; //scs 120813
        EmpList = Bus.MasEmployeeInsertUpdateandDelete(Emp);
        GrdEmployeeMaster.DataSource = "";
        GrdEmployeeMaster.DataSource = EmpList;       
        GrdEmployeeMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdEmployeeMaster.Columns[GrdEmployeeMaster.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdEmployeeMaster.Columns[GrdEmployeeMaster.Columns.Count - 1].Visible = false;
        }
    }
    public void LoadFilterEmployees() //To load data into grid.
    {
        //EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        //Emp.Flag = "R";
        //Emp.EmployeeCode = ddlLoadEmp.SelectedValue;4
        //EmpList = Bus.MasEmployeeInsertUpdateandDelete(Emp);
        var ddlEmplist = (from empname in EmpList
                          where empname.EmployeeName.ToLower().Contains(ddlLoadEmp.SelectedItem.Text.ToLower())
                          select empname).ToList();
        GrdEmployeeMaster.DataSource = "";
        GrdEmployeeMaster.DataSource = ddlEmplist;
        GrdEmployeeMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdEmployeeMaster.Columns[GrdEmployeeMaster.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdEmployeeMaster.Columns[GrdEmployeeMaster.Columns.Count - 1].Visible = false;
        }
    }
    private void LoadCompanyName()
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        EmpMsg.EmployeeCode = BaseMsg.EmployeeCode;
        CompList = Bus.CompanyMasterSelect(EmpMsg);
        CompList = (from Comp in CompList
                    where Comp.IsActive == true
                    select Comp).ToList();
        ddlCompanyName.DataSource = CompList;
        ddlCompanyName.DataTextField = "CompanyName";
        ddlCompanyName.DataValueField = "CompanyCode";
        ddlCompanyName.DataBind();
        ddlCompanyName.Items.Insert(0, "-- Select Please --");
        ddlCompanyName.SelectedIndex = 0;
    }
    private void LoadManagerName()
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();

        ddlManagerName.DataSource = EmpList;
        ddlManagerName.DataTextField = "EmployeeName";
        ddlManagerName.DataValueField = "EmployeeCode";
        ddlManagerName.DataBind();
        ddlManagerName.Items.Insert(0,new ListItem("-- Select Please --","0"));
        ddlManagerName.SelectedIndex = 0;
    }
    public List<CompanyMessage> getCompanyName()
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        EmpMsg.EmployeeCode = BaseMsg.EmployeeCode;
        List<CompanyMessage> CompList = new List<CompanyMessage>();
        CompList = Bus.CompanyMasterSelect(EmpMsg);
        CompList = (from Comp in CompList
                    where Comp.IsActive == true
                    select Comp).ToList();
        return CompList;

    }
    public List<EmployeeMasterMsg> getManagerName()
    {
       
        return EmpList;

    }
    private void EmployeeSave()
    {

        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.Flag = "I";
        Emp.EmployeeCode = txtEmployeeCode.Text.Trim();
        Emp.EmployeeName = txtEmpname.Text.Trim();
        Emp.EmailId = txtEmailId.Text.Trim();
        Emp.Password = KeyGen.EncryptPwd(txtPassword.Text.Trim());//txtPassword.Text.Trim();
        Emp.CompanyCode = ddlCompanyName.SelectedItem.Value;
        Emp.ManagerCode = ddlManagerName.SelectedItem.Value;
        Emp.ActivityDesignation = txtActivityDesignation.Text.Trim();
        Emp.IsActive = chkIsActive.Checked;
        Emp.IsCompanyAdmin = ChkIsCompanyAdmin.Checked;
        Emp.IsGroupLevel = IsGroupLevel.Checked; //SCS 100315 added column RDC PO 100315
        Emp.CreatedBy = BaseMsg.EmployeeCode;
        EmpList = Bus.MasEmployeeInsertUpdateandDelete(Emp);
        //Output Dispay
        foreach (EmployeeMasterMsg EmpMsg in EmpList)
        {
            if (EmpMsg.EmployeeResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.SuccessFullySaved + "');", true);
                ClearAll();
                LoadFilterEmployee();
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EmpMsg.EmployeeResult + "');", true);
                break;
            }
        }
    }
    public void EmployeeUpdate()
    {
        GridViewRow row = GrdEmployeeMaster.Rows[UpdateIndex];
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.Flag = "U";
        TextBox txtEmpCode = (TextBox)row.FindControl("txtEmployeeCode");
        TextBox txtEmpname = (TextBox)row.FindControl("txtEmpname");
        TextBox txtEmailId = (TextBox)row.FindControl("txtEmailId");
        DropDownList ddlcmpname = (DropDownList)row.FindControl("ddlCompanyName");
        DropDownList ddlmanagername = (DropDownList)row.FindControl("ddlManagerName");
        TextBox txtActivityDesg = (TextBox)row.FindControl("txtActivityDesignation");
        CheckBox chkCompanyAdmin = (CheckBox)row.FindControl("chkCompanyAdmin");
        CheckBox chkActive = (CheckBox)row.FindControl("chkIsActive");
        CheckBox chkGroupLevel = (CheckBox)row.FindControl("chkIsGroupLevel"); //SCS 100315 

        Emp.EmployeeCode = txtEmpCode.Text.Trim();
        Emp.EmployeeName = txtEmpname.Text.Trim();
        Emp.EmailId = txtEmailId.Text.Trim();
        Emp.CompanyCode = ddlcmpname.SelectedItem.Value;
        Emp.ManagerCode = ddlmanagername.SelectedItem.Value;
        Emp.ActivityDesignation = txtActivityDesg.Text.Trim();
        //scs 100315 Group Level
        Emp.IsGroupLevel = chkGroupLevel.Checked;
        //scs 120813 over
        //scs 120813 company admin power
        Emp.IsCompanyAdmin = chkCompanyAdmin.Checked;
        Emp.IsActive = chkActive.Checked;
        Emp.CreatedBy = BaseMsg.EmployeeCode;
        Emp.Password = string.Empty;
        EmpList = Bus.MasEmployeeInsertUpdateandDelete(Emp);
        GrdEmployeeMaster.EditIndex = -1;

        foreach (EmployeeMasterMsg EmpUpdate in EmpList)
        {
            if (EmpUpdate.EmployeeResult == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.EmployeeUpdatedSuccessfully + "');", true);

                ClearAll();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EmpUpdate.EmployeeResult + "');", true);
                break;
            }
        }

    }
    public void EmployeeDelete()
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.Flag = "D";
        GridViewRow gvr = GrdEmployeeMaster.Rows[DeleteIndex];
        Label lblEmpCode = (Label)gvr.FindControl("lblEmployeeCode");
        Label lblEmpName = (Label)gvr.FindControl("lblEmployeeName");
        Label lblEmaiId = (Label)gvr.FindControl("lblEmailid");
        Label lblCompname = (Label)gvr.FindControl("lblCompanyName");
        Label lblManagername = (Label)gvr.FindControl("lblManagerName");
        Label lblAcitivityDesg = (Label)gvr.FindControl("lblActivityDesignation");

        Emp.EmployeeCode = lblEmpCode.Text.Trim();
        Emp.EmployeeName = lblEmpName.Text.Trim();
        Emp.EmailId = lblEmaiId.Text.Trim();
        //Emp.CompanyCode = lblCompname.SelectedItem.Value;
        //Emp.ManagerCode = lblManagername.SelectedItem.Value;
        Emp.CompanyCode = "0";
        Emp.ManagerCode = "0";
        Emp.ActivityDesignation = txtActivityDesignation.Text.Trim();
        Emp.CreatedBy = BaseMsg.EmployeeCode;
        Emp.Password = string.Empty;
        EmpList = Bus.MasEmployeeInsertUpdateandDelete(Emp);
        foreach (EmployeeMasterMsg EmpUpdate in EmpList)
        {
            if (EmpUpdate.EmployeeResult == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.EmployeeDeletedSuccessfully + "');", true);
                ClearAll();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EmpUpdate.EmployeeResult + "');", true);
                break;
            }
        }
    }
    private void LoadFilterEmployee()
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.EmployeeCode = BaseMsg.EmployeeCode;
        EmpList = Bus.EmployeeMasterSelect(Emp);
        //EmpList = Bus.EmployeeMasterUserSelect(Emp);
        var ddlActList = (from emp in EmpList                         
                          select new { emp.EmployeeName}).Distinct().ToList();
        ddlLoadEmp.DataSource = ddlActList;
        ddlLoadEmp.DataTextField = "EmployeeName";
        //ddlLoadEmp.DataValueField = "EmployeeCode";
        ddlLoadEmp.DataBind();
        ddlLoadEmp.Items.Insert(0,"--All--");
    }   
    #endregion   
    #region Validation

    private int IsValidSave()
    {

        int Error = 0;
        string DisplayError = "";
        if (txtEmployeeCode.Text.Trim() == "" || Convert.ToInt32(txtEmployeeCode.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + StackResource.ErrEmployeeCode;
           // ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.ErrEmployeeCode + "');", true);
            Error = 1;
        }
        if (txtEmpname.Text.Trim() == "" || Convert.ToInt32(txtEmpname.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--"+StackResource.ErrEmployeeName;
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.ErrEmployeeName + "');", true);
            Error = 1;
        }
        if (txtEmailId.Text.Trim() == "" || Convert.ToInt32(txtEmailId.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrEmailId;
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.ErrEmailId + "');", true);
            Error = 1;
        }
        if (txtPassword.Text.Trim() == "" || Convert.ToInt32(txtPassword.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrEmployeePassword;
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.ErrEmployeePassword + "');", true);
            Error = 1;
        }
        if (ddlCompanyName.SelectedIndex==0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrorCompanyName;
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.ErrorCompanyName+ "');", true);
            Error = 1;
        }

        if (txtActivityDesignation.Text.Trim() == "" || Convert.ToInt32(txtActivityDesignation.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrActivityDesignation;
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.ErrActivityDesignation + "');", true);
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }

    private int IsValidGridSave(string EmployeeCode,string EmpName,string EmailId,string ActivityDesignation)
    {

        int Error = 0;
        string DisplayError = "";
        if (EmployeeCode.Trim() == "" || Convert.ToInt32(EmployeeCode.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + StackResource.ErrEmployeeCode;
            Error = 1;
        }
        if (EmpName.Trim() == "" || Convert.ToInt32(EmpName.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrEmployeeName;
            Error = 1;
        }
        if (EmailId.Trim() == "" || Convert.ToInt32(EmailId.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrEmailId;
            Error = 1;
        }
        if (ActivityDesignation.Trim() == "" || Convert.ToInt32(ActivityDesignation.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrActivityDesignation;
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    #endregion
    #region Clear
    public void ClearAll()
    {
        txtEmployeeCode.Text = "";
        txtEmpname.Text = "";
        txtPassword.Text = "";
        txtEmailId.Text = "";
        if (ddlLoadEmp.SelectedIndex == 0)
        {
            LoadGridEmployees();
        }
        else
        {
            LoadFilterEmployees();
        }
        //LoadGridEmployees();
        LoadManagerName();
        ddlCompanyName.SelectedIndex = 0;
        ddlManagerName.SelectedIndex = 0;
        txtActivityDesignation.Text = "";
        chkIsActive.Checked = false;
        ChkIsCompanyAdmin.Checked = false;
    }
    #endregion
}