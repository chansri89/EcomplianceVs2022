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

public partial class LocationDepartmentMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    public static List<LocationDepartmentMasterMsg> LocationList = new List<LocationDepartmentMasterMsg>();
    public static List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    List<DepartmentMsg> DepList = new List<DepartmentMsg>();
    List<CompanyMessage> CompanyList = new List<CompanyMessage>();
    public static int DeleteIndex = 0;
    public static int UpdateIndex = 0;
    public static string CompanyCode = "0";
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
            LoadGrdLocationDeptMaster();
            LoadCompanyName();
            LoadDepartmentName();
            getEmp();
            LoadReview();
            LoadHead();
            LoadExecutionPerson();
            LoadUltimate();
            if (LocationList.Count == 0)
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
                LocationDeptSave();
            }
        }
    }
   
    protected void btnGo_Click(object sender, EventArgs e)
    {
        GrdLocationDeptMaster.Visible = true;
        LoadGrdLocationDeptMaster();
    }
    #endregion
    #region GridEditing
    protected void GrdLocationDeptMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdLocationDeptMaster.EditIndex = e.NewEditIndex;
        GridViewRow row = GrdLocationDeptMaster.Rows[GrdLocationDeptMaster.EditIndex];
        Label lblCompanyName = (Label)row.FindControl("lblCompanyName");       
        Label lblDepartment = (Label)row.FindControl("lblDepartment");
        Label lblExecutionPerson = (Label)row.FindControl("lblExecutionPerson");
        Label lblReviewPerson = (Label)row.FindControl("lblReviewPerson");
        Label lblHeadPerson = (Label)row.FindControl("lblHeadPerson");
        Label lblUtimate = (Label)row.FindControl("lblUtimate");
        CompanyCode = ((Label)row.FindControl("lblCompanyCode")).Text;
        LoadGrdLocationDeptMaster();
        
        foreach (GridViewRow gvr in GrdLocationDeptMaster.Rows)
        {
            if (gvr.RowIndex == GrdLocationDeptMaster.EditIndex)
            {
                
                //DropDownList ddlCompanyName = (DropDownList)gvr.FindControl("ddlCompanyName");                
                //foreach (LocationDepartmentMasterMsg Location in LocationList)
                //{
                //    if (Location.CompanyName == lblCompanyName.Text)
                //    {
                //        ddlCompanyName.SelectedValue = Location.CompanyCode;
                //        ddlCompanyName.Focus();
                //        break;
                //    }
                //    ddlCompanyName.SelectedIndex = 0;
                //}
                DropDownList ddlDepartment = (DropDownList)gvr.FindControl("ddlDepartment");                
                foreach (LocationDepartmentMasterMsg Location in LocationList)
                {
                    if (Location.DepartmentName.Trim()== lblDepartment.Text.Trim())
                    {
                        ddlDepartment.SelectedValue = Location.DepartmentId.ToString();
                        ddlDepartment.Focus();
                        break;
                    }
                    ddlDepartment.SelectedIndex = 0;
                }
                DropDownList ddlExecutionPerson = (DropDownList)gvr.FindControl("ddlExecutionPerson");            
                foreach (LocationDepartmentMasterMsg Location in LocationList)
                {

                    if (Location.ExecutionEmployeeName.Trim() == lblExecutionPerson.Text.Trim())
                    {
                        ddlExecutionPerson.SelectedValue = Location.ExecutionEmployeeCode.ToString();
                        break;
                    }
                    ddlExecutionPerson.SelectedIndex = 0;
                }
                DropDownList ddlReviewPerson = (DropDownList)gvr.FindControl("ddlReviewPerson");
                foreach (LocationDepartmentMasterMsg Location in LocationList)
                {
                    if (Location.reviewEmployeeName.Trim() == lblReviewPerson.Text.Trim())
                    {
                        ddlReviewPerson.SelectedValue = Location.reviewEmployeeCode.ToString();
                        break;
                    }
                    ddlReviewPerson.SelectedIndex = 0;
                }

                DropDownList ddlHeadPerson = (DropDownList)gvr.FindControl("ddlHeadPerson");
                foreach (LocationDepartmentMasterMsg Location in LocationList)
                {

                    if (Location.HeadEmployeeName.Trim() == lblHeadPerson.Text.Trim())
                    {
                        ddlHeadPerson.SelectedValue = Location.HeadEmployeeCode.ToString();
                        break;
                    }
                    ddlHeadPerson.SelectedIndex = 0;
                }
                DropDownList ddlUtimate = (DropDownList)gvr.FindControl("ddlUtimate");
                foreach (LocationDepartmentMasterMsg Location in LocationList)
                {
                    if (Location.UltimateEmployeeName.Trim() == lblUtimate.Text.Trim())
                    {
                        ddlUtimate.SelectedValue = Location.UltimateEmployeeCode.ToString();
                        break;
                    }
                    ddlUtimate.SelectedIndex = 0;
                }
            }
        }
    }
    protected void GrdLocationDeptMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdLocationDeptMaster.EditIndex = -1;
        LoadGrdLocationDeptMaster();
    }    
    protected void GrdLocationDeptMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        GridViewRow row = GrdLocationDeptMaster.Rows[UpdateIndex];
        string CompanyName = ((Label)row.FindControl("lblCompanyName")).Text; 
        string Department = ((DropDownList)row.FindControl("ddlDepartment")).Text;
        string ExecutionPerson = ((DropDownList)row.FindControl("ddlExecutionPerson")).Text;
        string ReviewPerson = ((DropDownList)row.FindControl("ddlReviewPerson")).Text;
        string HeadPerson = ((DropDownList)row.FindControl("ddlHeadPerson")).Text;
        string Utimate = ((DropDownList)row.FindControl("ddlUtimate")).Text;
        TextBox RespGrpName = (TextBox)row.FindControl("txtResponsibleGrpName");
        if (IsValidGridSave(RespGrpName.Text.Trim()) == 0)
        {
            LocationDeptUpdate();
        }
    }
    #endregion
    #region Methods
    private void LoadGrdLocationDeptMaster()
    {
        LocationDepartmentMasterMsg LocationDept = new LocationDepartmentMasterMsg();
        LocationDept.Flag = "R";
        //LocationDept.CompanyCode = CompanyCode;// ddlLocationName.SelectedValue;//"0"; 
        LocationDept.EmployeeCode = BaseMsg.EmployeeCode;
        LocationList = Bus.MasLocationDeptInsertUpdateand(LocationDept);
        GrdLocationDeptMaster.DataSource = "";
        GrdLocationDeptMaster.DataSource = LocationList;
        GrdLocationDeptMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdLocationDeptMaster.Columns[GrdLocationDeptMaster.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdLocationDeptMaster.Columns[GrdLocationDeptMaster.Columns.Count - 1].Visible = false;
        }
    }
  
    private void LocationDeptSave()
    {
        LocationDepartmentMasterMsg LocationDept = new LocationDepartmentMasterMsg();
        LocationDept.Flag = "I";
        LocationDept.LocationDeptId = 0;
        LocationDept.CompanyCode = ddlLocationName.SelectedValue;
        LocationDept.ResponsibleGrpName = txtResponsibleGrpName.Text.Trim();
        LocationDept.DepartmentId =Convert.ToInt32(ddlDepartment.SelectedValue);
        LocationDept.ExecutionEmployeeCode = ddlResponsablePerson.SelectedValue;
        LocationDept.reviewEmployeeCode = ddlReportingOfficer.SelectedValue;
        LocationDept.HeadEmployeeCode = ddlReviewingOfficer.SelectedValue;
        LocationDept.UltimateEmployeeCode = ddlCompanyHead.SelectedValue;
        LocationDept.EmployeeCode = BaseMsg.EmployeeCode; 
        LocationList = Bus.MasLocationDeptInsertUpdateand(LocationDept);
        //Output Dispay
        foreach (LocationDepartmentMasterMsg LocationDeptSave in LocationList)
        {
            if (LocationDeptSave.LocationDeptResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.SuccessFullySaved + "');", true);
                GrdLocationDeptMaster.Visible = true;
                AllClear();                
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + LocationDeptSave.LocationDeptResult + "');", true);
                break;
            }
        }
    }
    public void LocationDeptUpdate()
    {
        GridViewRow row = GrdLocationDeptMaster.Rows[UpdateIndex];
        LocationDepartmentMasterMsg LocationDept = new LocationDepartmentMasterMsg();
        LocationDept.Flag = "U";

        TextBox txtLocationDeptId = (TextBox)row.FindControl("txtLocationDeptId");
        TextBox txtResponsibleGrpName = (TextBox)row.FindControl("txtResponsibleGrpName");
        TextBox txtCompanyCode = (TextBox)row.FindControl("txtCompanyCode");
        DropDownList ddlDepartment = (DropDownList)row.FindControl("ddlDepartment");
        DropDownList ddlExecutionPerson = (DropDownList)row.FindControl("ddlExecutionPerson");
        DropDownList ddlReviewPerson = (DropDownList)row.FindControl("ddlReviewPerson");
        DropDownList ddlHeadPerson = (DropDownList)row.FindControl("ddlHeadPerson");
        DropDownList ddlUtimate = (DropDownList)row.FindControl("ddlUtimate");

        LocationDept.LocationDeptId = Convert.ToInt32(txtLocationDeptId.Text);
        LocationDept.CompanyCode = txtCompanyCode.Text;// ddlCompanyName.SelectedValue;
        LocationDept.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
        LocationDept.ExecutionEmployeeCode = ddlExecutionPerson.SelectedValue;
        LocationDept.reviewEmployeeCode = ddlReviewPerson.SelectedValue;
        LocationDept.HeadEmployeeCode = ddlHeadPerson.SelectedValue;
        LocationDept.UltimateEmployeeCode = ddlUtimate.SelectedValue;
        LocationDept.ResponsibleGrpName = txtResponsibleGrpName.Text.Trim();
        LocationDept.EmployeeCode = BaseMsg.EmployeeCode;
        LocationList = Bus.MasLocationDeptInsertUpdateand(LocationDept);

        GrdLocationDeptMaster.EditIndex = -1;

        foreach (LocationDepartmentMasterMsg LocationDeptUpdate in LocationList)
        {
            if (LocationDeptUpdate.LocationDeptResult == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.LocationDeptNameUpdatedSuccessfully + "');", true);

                AllClear();
               
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + LocationDeptUpdate.LocationDeptResult + "');", true);
                break;
            }
        }
    }   
    //public List<LocationDepartmentMasterMsg> getExecutionPerson()
    //{
    //    LocationDepartmentMasterMsg Location = new LocationDepartmentMasterMsg();
    //    Location.CompanyCode = CompanyCode;
    //    LocationList = Bus.MasLocEmployeeListSelect(Location);      
    //    LocationList = (from ActiveEmp in LocationList
    //               where ActiveEmp.IsActive == true
    //               select ActiveEmp).ToList();
    //    return LocationList;

    //}
    //public List<LocationDepartmentMasterMsg> getReviewPerson()
    //{
    //    LocationDepartmentMasterMsg Location = new LocationDepartmentMasterMsg();
    //    Location.CompanyCode = CompanyCode;//ddlLocationName.SelectedItem.Value;
    //    LocationList = Bus.MasLocEmployeeListSelect(Location);
    //    LocationList = (from ActiveEmp in LocationList
    //                    where ActiveEmp.IsActive == true
    //                    select ActiveEmp).ToList();
    //    return LocationList;
    //}
    //public List<LocationDepartmentMasterMsg> getHeadPerson()
    //{
    //    LocationDepartmentMasterMsg Location = new LocationDepartmentMasterMsg();
    //    Location.CompanyCode = CompanyCode;//ddlLocationName.SelectedItem.Value;
    //    LocationList = Bus.MasLocEmployeeListSelect(Location);
    //    LocationList = (from ActiveEmp in LocationList
    //               where ActiveEmp.IsActive == true
    //               select ActiveEmp).ToList();
    //    return LocationList;
    //}
    //public List<LocationDepartmentMasterMsg> getUltimate()
    //{
    //    LocationDepartmentMasterMsg Location = new LocationDepartmentMasterMsg();
    //    //Location.CompanyCode = ddlLocationName.SelectedItem.Value;
    //    Location.CompanyCode = CompanyCode;
    //    LocationList = Bus.MasLocEmployeeListSelect(Location);
    //    LocationList = (from Emp in LocationList
    //                    where Emp.IsActive == true
    //                    select Emp).ToList();
    //    return LocationList;
    //}
    public List<CompanyMessage> getCompany()
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.EmployeeCode = BaseMsg.EmployeeCode;
        CompanyList = Bus.CompanyMasterSelect(Emp);
        CompanyList = (from Comp in CompanyList
                       where Comp.IsActive == true
                       select Comp).ToList();
        return CompanyList;
    }  
    private void LoadCompanyName()
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.EmployeeCode = BaseMsg.EmployeeCode;
        CompanyList = Bus.CompanyMasterSelect(Emp);
        CompanyList = (from Comp in CompanyList
                       where Comp.IsActive == true
                       select Comp).ToList();
        ddlLocationName.DataSource = CompanyList;
        ddlLocationName.DataTextField = "CompanyName";
        ddlLocationName.DataValueField = "CompanyCode";
        ddlLocationName.DataBind();
        ddlLocationName.Items.Insert(0, new ListItem("-- Sleact Please --", "0"));

    }
    private void LoadDepartmentName()
    {
        DepartmentMsg Dept = new DepartmentMsg();
        EmployeeMasterMsg emp = new EmployeeMasterMsg();
        DepList = Bus.DepartmentMasterSelect(emp);
        DepList = (from ActiveDept in DepList
                    where ActiveDept.IsActive == true
                    select ActiveDept).ToList();
        ddlDepartment.DataTextField = "DepartmentName";
        ddlDepartment.DataValueField = "DepartmentId";
        ddlDepartment.DataSource = DepList;
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem("-- Select Please --", "0"));
    }
    private void LoadExecutionPerson()
    {
        //LocationDepartmentMasterMsg Location = new LocationDepartmentMasterMsg();
        ////Location.CompanyCode = ddlLocationName.SelectedItem.Value;
        //EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        //EmpList = Bus.MasLocEmployeeListSelectsp(Emp);
        ////EmpList = (from Emp in EmpList                       commended by Abinayaa 100113.after 090113 meeting
        ////                where Emp.IsActive == true
        ////                select Emp).ToList();
        ddlResponsablePerson.DataTextField = "EmployeeName";
        ddlResponsablePerson.DataValueField = "EmployeeCode";
        ddlResponsablePerson.DataSource = EmpList;
        ddlResponsablePerson.DataBind();
        ddlResponsablePerson.Items.Insert(0, new ListItem("-- Select Please --", "0"));
    }
    private void LoadReview()
    {
       // LocationDepartmentMasterMsg Location = new LocationDepartmentMasterMsg();
       //// Location.CompanyCode = ddlLocationName.SelectedValue;
        //EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        //EmpList = Bus.MasLocEmployeeListSelectsp(Emp);
        ////EmpList = (from Emp in EmpList                         commended by Abinayaa 100113.after 090113 meeting
        ////                where Emp.IsActive == true
        ////                select Emp).ToList();
        ddlReportingOfficer.DataTextField = "EmployeeName";
        ddlReportingOfficer.DataValueField = "EmployeeCode";
        ddlReportingOfficer.DataSource = EmpList;
        ddlReportingOfficer.DataBind();
        ddlReportingOfficer.Items.Insert(0, new ListItem("-- Select Please --", "0"));
    }
    private void LoadHead()
    {
       // LocationDepartmentMasterMsg Location = new LocationDepartmentMasterMsg();
       //// Location.CompanyCode = ddlLocationName.SelectedItem.Value;
        //EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        //EmpList = Bus.MasLocEmployeeListSelectsp(Emp);
        ////EmpList = (from Emp in EmpList                         commended by Abinayaa 100113.after 090113 meeting
        ////                where Emp.IsActive == true
        ////                select Emp).ToList();
        ddlReviewingOfficer.DataTextField = "EmployeeName";
        ddlReviewingOfficer.DataValueField = "EmployeeCode";
        ddlReviewingOfficer.DataSource = EmpList;
        ddlReviewingOfficer.DataBind();
        ddlReviewingOfficer.Items.Insert(0, new ListItem("-- Select Please --", "0"));
    }
    private void LoadUltimate()
    {
        //LocationDepartmentMasterMsg Location = new LocationDepartmentMasterMsg();
        ////Location.CompanyCode = ddlLocationName.SelectedItem.Value;
        //EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        //EmpList = Bus.MasLocEmployeeListSelectsp(Emp);
        ////EmpList = (from Emp in EmpList                         commended by Abinayaa 100113.after 090113 meeting
        ////                where Emp.IsActive == true
        ////                select Emp).ToList();
        ddlCompanyHead.DataTextField = "EmployeeName";
        ddlCompanyHead.DataValueField = "EmployeeCode";
        ddlCompanyHead.DataSource = EmpList;
        ddlCompanyHead.DataBind();
        ddlCompanyHead.Items.Insert(0, new ListItem("-- Select Please --", "0"));
    }
    private void getEmp()
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.EmployeeCode = BaseMsg.EmployeeCode;
        EmpList = Bus.MasLocEmployeeListSelectsp(Emp);      
    }
    public List<DepartmentMsg> getDepartment()
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        DepList = Bus.DepartmentMasterSelect(EmpMsg);
        DepList = (from ActiveDept in DepList
                   where ActiveDept.IsActive == true
                   select ActiveDept).ToList();
        return DepList;
    }
    #endregion
    #region ddlLoad
    public List<EmployeeMasterMsg> getExecutionPerson()
    {

        //EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        ////Location.CompanyCode = CompanyCode;// ddlLocationName.SelectedItem.Value;
        //EmpList = Bus.MasLocEmployeeListSelectsp(Emp);        
        ////EmpList = (from ActiveEmp in EmpList
        ////           where ActiveEmp.IsActive == true
        ////           select ActiveEmp).ToList();
        return EmpList;

    }
    public List<EmployeeMasterMsg> getReviewPerson()
    {
        ////LocationDepartmentMasterMsg Location = new LocationDepartmentMasterMsg();
        ////Location.CompanyCode = CompanyCode;// ddlLocationName.SelectedItem.Value;
        //EmpList = Bus.MasLocEmployeeListSelectsp(Location);
        //EmpList = (from ActiveEmp in EmpList
        //           where ActiveEmp.IsActive == true
        //           select ActiveEmp).ToList();
        return EmpList;
    }
    public List<EmployeeMasterMsg> getHeadPerson()
    {
        ////LocationDepartmentMasterMsg Location = new LocationDepartmentMasterMsg();
        ////Location.CompanyCode = CompanyCode;// ddlLocationName.SelectedItem.Value;
        //EmpList = Bus.MasLocEmployeeListSelectsp(Location);
        ////EmpList = (from ActiveEmp in EmpList
        ////           where ActiveEmp.IsActive == true
        ////           select ActiveEmp).ToList();
        return EmpList;
    }
    public List<EmployeeMasterMsg> getUtimate()
    {
        ////LocationDepartmentMasterMsg Location = new LocationDepartmentMasterMsg();
        ////Location.CompanyCode = CompanyCode;// ddlLocationName.SelectedItem.Value;
        //EmpList = Bus.MasLocEmployeeListSelectsp(Location);
        ////EmpList = (from ActiveEmp in EmpList
        ////           where ActiveEmp.IsActive == true
        ////           select ActiveEmp).ToList();
        return EmpList;
    }
    #endregion
    #region Clear
    public void AllClear()
    {
        LoadCompanyName();
        LoadDepartmentName();
        getEmp();
        LoadReview();
        LoadHead();
        LoadExecutionPerson();
        LoadUltimate();
        ddlLocationName.SelectedIndex = 0;
        txtResponsibleGrpName.Text = "";
        ddlDepartment.SelectedIndex = 0;
        ddlCompanyHead.SelectedIndex = 0;
        ddlResponsablePerson.SelectedIndex = 0;
        ddlReportingOfficer.SelectedIndex = 0;
        ddlReviewingOfficer.SelectedIndex = 0;
        LoadGrdLocationDeptMaster();
        //ddlResponsablePerson.Items.Clear();//commwnded by Abinayaa 100113 after 090113 meeting.Load Emp's not Based on the selected Cmp.
        //ddlResponsablePerson.DataSource = null;
        //ddlResponsablePerson.DataBind();

        //ddlReportingOfficer.Items.Clear();
        //ddlReportingOfficer.DataSource = null;
        //ddlReportingOfficer.DataBind();

        //ddlReviewingOfficer.Items.Clear();
        //ddlReviewingOfficer.DataSource = null;
        //ddlReviewingOfficer.DataBind();

        //ddlCompanyHead.Items.Clear();
        //ddlCompanyHead.DataSource = null;
        //ddlCompanyHead.DataBind();
        
    }
    #endregion
    #region Validation

    private int IsValidSave()
    {
        int Error = 0;
        string DisplayError = "";
        if (ddlLocationName.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrLocationName;
            Error = 1;
        }
        if (txtResponsibleGrpName.Text.Trim().Length== 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrResponsibleGrpName;
            txtResponsibleGrpName.Text = "";
            Error = 1;
        }
        if (ddlDepartment.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrDept;
            Error = 1;
        }
        if (ddlResponsablePerson.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrResponsablePerson;
            Error = 1;
        }
        if (ddlReportingOfficer.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrReportingOfficer;
            Error = 1;
        }
        if (ddlReviewingOfficer.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrReviewingOfficer;
            Error = 1;
        }
        if (ddlCompanyHead.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrCompanyHead;
            Error = 1;
        }
        
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }

    private int IsValidGridSave(string RespGrpName)
    {
        int Error = 0;
        string DisplayError = "";
        if (RespGrpName.Trim() == "" || Convert.ToInt32(RespGrpName.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + StackResource.ErrResponsibleGrpName;
            Error = 1;
        }
     
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    #endregion
    //protected void ddlLocationName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlLocationName.SelectedIndex >0)
    //    {
    //        LoadReview();
    //        LoadHead();
    //        LoadExecutionPerson();
    //        LoadUltimate();
    //    }
    //    if (ddlLocationName.SelectedIndex == 0)
    //    {
    //        ddlResponsablePerson.Items.Clear();
    //        ddlResponsablePerson.DataSource = null;
    //        ddlResponsablePerson.DataBind();

    //        ddlReportingOfficer.Items.Clear();
    //        ddlReportingOfficer.DataSource = null;
    //        ddlReportingOfficer.DataBind();

    //        ddlReviewingOfficer.Items.Clear();
    //        ddlReviewingOfficer.DataSource = null;
    //        ddlReviewingOfficer.DataBind();

    //        ddlCompanyHead.Items.Clear();
    //        ddlCompanyHead.DataSource = null;
    //        ddlCompanyHead.DataBind();
    //    }
    //}    
}