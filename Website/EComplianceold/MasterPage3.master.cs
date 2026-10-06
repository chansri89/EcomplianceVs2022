using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ganini.Lib;

public partial class MasterPage3 : System.Web.UI.MasterPage
{
    ProcessBus Bus = new ProcessBus();
    EmployeeMasterMsg emp = new EmployeeMasterMsg();
    List<ProgramMsg> ProgramList = new List<ProgramMsg>();
    List<ActMasterMsg> actMasterMsgList = new List<ActMasterMsg>();
    BaseClass AccessBaseClass = new BaseClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        //pnlGrd.Visible = false;
        Uri u = Request.Url;
        if (AccessBaseClass.EmployeeName == string.Empty)
        {
            Response.Redirect(string.Format("~/Login.aspx?IsSessionTimeOutFlag={0}", "Y"));
        }
        lbluname.Text = AccessBaseClass.EmployeeName;
        lblDate.Text = Convert.ToString(System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm"));
        lblVersionNumber.Text =Config.GetAppsetting("VERSION");
        if (!Page.IsPostBack)
        {

            emp.EmployeeCode = AccessBaseClass.EmployeeCode;
            ProgramList = Bus.AdmUserAccessProgramsSelect(emp);
            actMasterMsgList = Bus.ActMasterSelect(emp);
            AccessBaseClass.ProgramMsgList = ProgramList;
            LoadMenu(ProgramList);
            LoadActName(actMasterMsgList);
            if (u.ToString().Contains("UserActivities.aspx"))
            {
                if (Session["ActId"] != null)
                {
                    ddlActivity.SelectedValue = Session["ActId"].ToString();
                }
            }
            if (AccessBaseClass.IsHomePageAction == "Y")
            {
                TreeView2.Enabled = false;
                ddlActivity.Enabled = false;
                btnGo.Enabled = false;
                SiteMapPath.Enabled = false;
            }
            if (u.ToString().Contains("ActivityAction.aspx"))//&& Request.QueryString["IsHomepageAction"] == null)
            {
                if (imgExpand.ImageUrl != "~/Images/Expand1.JPG")
                {
                    td1.Visible = false;
                    imgExpand.ImageUrl = "~/Images/Expand1.JPG";
                }

            }
            //else
            //{
            //    if (imgExpand.ImageUrl != "~/Images/Expand1.JPG")
            //    {
            //        td1.Visible = false;
            //        imgExpand.ImageUrl = "~/Images/Expand1.JPG";
            //    }
            //}
        }
  

    }
    public void LoadActName(List<ActMasterMsg> ActMasterList)
    {
        if (ActMasterList != null && ActMasterList.Count > 0)
        {
            var ActMaster = (from ActMsg in ActMasterList
                             select new { ActMsg.ActId, ActMsg.ActName }).Distinct().ToList();
            ddlActivity.DataSource = ActMaster;
            ddlActivity.DataBind();
            ddlActivity.Items.Insert(0, "--Select Act Please--");
            ddlActivity.SelectedIndex = 0;
        }
        else
        {
            ddlActivity.Items.Insert(0, "--Select Act Please--");
            ddlActivity.SelectedIndex = 0;
        }
    }
    public void LoadMenu(List<ProgramMsg> programList)
    {
        var pMainMenu = (from pMain in programList
                         select new { pMain.MainMenu }).Distinct().ToList();
        foreach (var pMain in pMainMenu)
        {
            TreeNode Node = new TreeNode();
            Node.Text = pMain.MainMenu;
            var pSubMenu = (from pSub in programList
                            where pSub.MainMenu.Equals(pMain.MainMenu)
                            select new { pSub.MainMenu, pSub.SubMenu }).Distinct().ToList();
            foreach (var pSub in pSubMenu)
            {
                List<ProgramMsg> program = (from programfilter in programList
                                            where programfilter.MainMenu.Equals(pSub.MainMenu) && programfilter.SubMenu.Equals(pSub.SubMenu)
                                            select programfilter).ToList();
                TreeNode SubMenu = new TreeNode();
                SubMenu.Text = pSub.SubMenu;
                foreach (ProgramMsg pChildMenu in program)
                {
                    TreeNode ChildMenu = new TreeNode();
                    ChildMenu.Text = pChildMenu.ChildMenu;
                    ChildMenu.NavigateUrl = pChildMenu.ProgramAccessPath;
                    if (pSub.SubMenu != string.Empty)
                    {
                        SubMenu.ChildNodes.Add(ChildMenu);
                    }
                    else
                    {
                        Node.ChildNodes.Add(ChildMenu);
                    }
                }
                if (pSub.SubMenu != string.Empty)
                {
                    Node.ChildNodes.Add(SubMenu);
                }
            }
            TreeView2.Nodes.Add(Node);
        }
    }
    protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        LoginInfoMsg Login = new LoginInfoMsg();
        Login.UserName = AccessBaseClass.EmployeeCode;
        Login.UserSessionId = AccessBaseClass.UserSessionId;
        Bus.UpdateUserLogoffInfo(Login);
        Session.Clear();
        Session.RemoveAll();
        Response.Redirect("~/Login.aspx");
    }
    protected void imgExpand_Click(object sender, ImageClickEventArgs e)
    {
        if (imgExpand.ImageUrl == "~/Images/colapse1.JPG")
        {

            td1.Visible = false;
            imgExpand.ImageUrl = "~/Images/Expand1.JPG";

        }
        else
        {

            td1.Visible = true;
            TreeView2.ExpandAll();
            imgExpand.ImageUrl = "~/Images/colapse1.JPG";
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {

        if (ddlActivity.SelectedIndex == 0)
        {
            Session["ActId"] = 0;
        }
        else
        {
            Session["ActId"] = ddlActivity.SelectedValue;
        }
        Response.Redirect("UserActivities.aspx");
    }
}
