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

public partial class CompanyACT : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    //public static List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    public static List<CompanyMessage> CompanyList = new List<CompanyMessage>();
    public static List<CompanyMessage> ActiveCmpList = new List<CompanyMessage>();
    public static int DeleteIndex = 0;
    public static int UpdateIndex = 0;
    UserAccess user = new UserAccess();
    public static string ProgramName = string.Empty;
    BaseClass BaseMsg = new BaseClass();
    public static List<ActMasterMsg> ActList = new List<ActMasterMsg>();
    public static string CompanyCode = "";
    #endregion
    
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
        {
            LoadGridCompany();
            PnlComp.Enabled = true;
            
            if (CompanyList.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.MsgForGrdnotLoad + "');", true);
                PnlComp.Visible = false;
            }
            else
            {
                PnlComp.Visible = true;
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
                CompanyActSave();
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

        pnlAct.Visible = false;
        PnlComp.Enabled = true;
        PnlComp.Visible = true;
        btnCollapse.Text = "-";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.CreatePermissionRestricted + "');", true);
        }
        else
        {
            lblCompanySelected.Text = "Company = ";
            int Result = 0; //set to 0 for compnay checked or not
            foreach (GridViewRow gvr in GrdCompanyMaster.Rows)
            {
              
                bool CompChecked = ((CheckBox)gvr.FindControl("chkActive")).Checked;
                if (CompChecked == true)
                {
                    //CompanyActMsg CmpAct = new CompanyActMsg();
                    //CmpAct.CompanyCode = Convert.ToString((Label)gvr.FindControl("lblCompanyCode"));

                    lblCompanySelected.Text = lblCompanySelected.Text + ((Label)gvr.FindControl("lblCompanyShortName")).Text.ToString()+". ";
                    Result = 1;
                    //break;
                }
            }
            if (Result == 1) //atleast one company selected.
            {
                string CompCode = "0";
                LoadGrdActMaster(CompCode);
                //lblCompanySelected.Text = ""; scs 080216 show selected company
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Fresh List of Acts for Each Company Selected will be Created" + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Atleast One Company should be Selected" + "');", true);
            }
            
        }
    }
    protected void btnCollapse_Click(object sender, EventArgs e)
    {
        if (btnCollapse.Text == "-")
        {
            btnCollapse.Text = "+";
            PnlComp.Visible = false;
        }
        else
        {
            btnCollapse.Text = "-";
            PnlComp.Visible = true;
        }

    }
    #region GridEditing
    protected void GrdCompanyMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //GrdCompanyMaster.EditIndex = e.NewEditIndex;
        int WRowIndex = Convert.ToInt32(e.CommandArgument.ToString());
        GridViewRow row = GrdCompanyMaster.Rows[WRowIndex];
        //GridViewRow row = GrdCompanyMaster.Rows[GrdCompanyMaster.EditIndex];
        bool CompChecked = ((CheckBox)row.FindControl("chkActive")).Checked;
        
        if (CompChecked == true)
        {
            Label LCompanyCode = ((Label)row.FindControl("lblCompanyCode"));
            lblCompanySelected.Text = "Company Selected = " + ((Label)row.FindControl("lblCompanyShortName")).Text.ToString();
            CompanyCode = LCompanyCode.Text.ToString();
            LoadGrdActMaster(CompanyCode);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Select the Company by clicking on the box in Grid" + "');", true);
        }

    }
  
    #endregion
    #endregion
    #region Methods
    private void LoadGridCompany()//To load data into grid.
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        CompanyMessage Cmp = new CompanyMessage();
        Cmp.Flag = "R";
        EmpMsg.EmployeeCode = BaseMsg.EmployeeCode;
        CompanyList = Bus.MasCompanyInsertUpdateandDelete(Cmp, EmpMsg);
        LoadCompanyList(CompanyList);
    }
    private void LoadCompanyList(List<CompanyMessage> CmpList)
    {
        ActiveCmpList = (from gradeFilter in CmpList
                              where gradeFilter.IsActive = true select gradeFilter).ToList();
        GrdCompanyMaster.DataSource = "";
        GrdCompanyMaster.DataSource = ActiveCmpList;
        GrdCompanyMaster.DataBind();
        pnlAct.Visible = false;
        PnlComp.Enabled = true;
        PnlComp.Visible = true;
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdCompanyMaster.Columns[GrdCompanyMaster.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdCompanyMaster.Columns[GrdCompanyMaster.Columns.Count - 1].Visible = false;
        }
    }
    private void LoadGrdActMaster(string CompCode)
    {
        List<CompanyActMsg> CActLst = new List<CompanyActMsg>();

        CompanyActMsg CAct = new CompanyActMsg();
        CAct.CompanyCode = CompCode;
        CActLst = Bus.MasCompanyActSelect(CAct);
        LoadGrdData(CActLst);
    }
    private void LoadGrdData(List<CompanyActMsg> CActList)//added by abinayaa 110913
    {
        if (CActList.Count > 0)
        {
            GrdActMaster.DataSource = "";
            GrdActMaster.DataSource = CActList;
            GrdActMaster.DataBind();
            if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
            {
                GrdActMaster.Columns[GrdActMaster.Columns.Count - 1].Visible = false;
            }
            //if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
            //{
            //    GrdActMaster.Columns[GrdActMaster.Columns.Count - 1].Visible = false;
            //}
            pnlAct.Visible = true;
            PnlComp.Enabled = false;
            PnlComp.Visible = false;
            btnCollapse.Text = "+";
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Check if Acts are not created" + "');", true);
        }
    }

    public void CompanyActSave()
    {
        List<CompanyActMsg> CActList = new List<CompanyActMsg>();    
        foreach (GridViewRow gvr in GrdCompanyMaster.Rows) //select Checked Companies
        {
            bool CompChecked = ((CheckBox)gvr.FindControl("chkActive")).Checked;
            Label SCompanyCode = ((Label)gvr.FindControl("lblCompanyCode"));
            
            if (CompChecked == true)
            {
                foreach (GridViewRow Actgvr in GrdActMaster.Rows)
                {
                    bool ActChecked = ((CheckBox)Actgvr.FindControl("chkActActive")).Checked;
                    Label SActId = ((Label)Actgvr.FindControl("lblActId"));
                    if (ActChecked == true) //Select Only checked Acts
                    {
                        CompanyActMsg CActMsg = new CompanyActMsg();
                        CActMsg.CompanyCode = SCompanyCode.Text.ToString();
                        CActMsg.ActId = Convert.ToInt32(SActId.Text.ToString());
                        CActMsg.CreatedBy = BaseMsg.EmployeeCode;
                        CActList.Add(CActMsg);
                    }    //End of If ActChecked
                }       //End of Act Grid loop
            }         // End of If CompanyChecked
        }           //End of Grd Company Loop

        string Result = Bus.MasCompanyActInsertUpdate(CActList);
       
            if (Result == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Successfully Saved" + "');", true);
                lblCompanySelected.Text = "";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + Result + "');", true);
            
            }
            pnlAct.Visible = false;
            PnlComp.Visible = true;
            PnlComp.Enabled = true;
            btnCollapse.Text = "-";
    } //end of Method CompanyAct Save

    #region Validation

    private int IsValidSave()
    {
        int Error = 0;
        string DisplayError = "";
        int result = 0;
        foreach (GridViewRow gvr in GrdActMaster.Rows)
        {
            bool ActChecked = ((CheckBox)gvr.FindControl("chkActActive")).Checked;
            if (ActChecked == true)
            {
                result = 1;
                break;
            }
        }

        if (result == 0)
        {
            DisplayError = "Atleast One Act should be Selected";
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
    public void AllClear()
    {
        
        LoadGridCompany();       
       
    }
    #endregion
    #endregion        
    }