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

public partial class UserActivities : System.Web.UI.Page
{
    string ActId;
    List<ActivityMasterMsg> ActivityMasterList = new List<ActivityMasterMsg>();
    ProcessBus Bus = new ProcessBus();
    BaseClass AccessBase = new BaseClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (AccessBase.LoginResult == "1")
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.DBsize + "');", true);

        }
       
        if (!IsPostBack)
        {
            if (Session["ActId"] != null && Session["ActId"].ToString() != string.Empty)
            {
                ActId = Session["ActId"].ToString();
                LoadActivity(Convert.ToInt32(ActId));
                //added by abinayaa for db size checking --130513
                if (!Page.IsPostBack)
                {
                    if (AccessBase.LoginResult == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.DBsize + "');", true);

                    }
                }
                //added by abinayaa for db size checking --130513
            }
            else
            {
                LoadActivity(0);
            }
        }
    }
    public void LoadActivity(int ActId)
    {
        EmployeeMasterMsg emp=new EmployeeMasterMsg();
        emp.EmployeeCode = AccessBase.EmployeeCode;
        ActivityMasterList = Bus.ActivityMasterSelect(emp, ActId);
        GrdActivity.DataSource = ActivityMasterList;
        GrdActivity.DataBind();
    }
    //protected void GrdActivity_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    //GrdActivity.PageIndex = e.NewPageIndex;
    //}

    protected void GrdActivity_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdActivity.PageIndex = e.NewPageIndex;
        LoadActivity(0);

        
    }
}