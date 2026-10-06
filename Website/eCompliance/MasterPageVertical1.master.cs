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





public partial class MasterPage1 : System.Web.UI.MasterPage
{
    ProcessBus Bus = new ProcessBus();
    EmployeeMasterMsg emp = new EmployeeMasterMsg();
    List<ProgramMsg> ProgramList = new List<ProgramMsg>();
    BaseClass AccessBaseClass = new BaseClass();
    public static string currValue="1";
    public static string wcurVal = "1";
    public static string Viwstateval = "1";
    protected void Page_Load(object sender, EventArgs e)
    {
       
              
        Uri u = Request.Url;
        if (AccessBaseClass.EmployeeName == string.Empty)
        {
            Response.Redirect(string.Format("~/Login.aspx?IsSessionTimeOutFlag={0}", "Y"));
        }
        lbluname.Text = AccessBaseClass.EmployeeName;
        lblDate.Text = Convert.ToString(System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm"));
        lblVersionNumber.Text = Config.GetAppsetting("VERSION");
        if (!Page.IsPostBack)
        {
            Session["Sess1curVal"] = "";
            emp.EmployeeCode = AccessBaseClass.EmployeeCode;

            string PassPolicy = ConfigurationManager.AppSettings["PasswordPolicy"].ToString();
            if (PassPolicy == "Y")
            {
                PassPolicy = AccessBaseClass.PassPolicy; //scs070116 Coming as from CheckLoginSP
            }
            else
            {
                PassPolicy = "0"; // rest to 0 if there is no Password policy set up in web config.
            }

            ProgramList = Bus.AdmUserAccessProgramsSelect(emp, PassPolicy); // based on PassPolicy, the Program fetch will show Change password on expiry or First Login.
            AccessBaseClass.ProgramMsgList = ProgramList;
            LoadMenu(ProgramList);
            if (PassPolicy != "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + PassPolicy + "');", true);
            }
           // AccessBaseClass.PassPolicy = "0"; //rest as this will prompt to login every time on the screen scs 160216
        }
        //if (currValue != "1")
            if (AccessBaseClass.SessionVar1 != "")
            {
                Session["Sess1curVal"] = wcurVal;
                Session["Sess3curVal"] = wcurVal;
                AccessBaseClass.SessionVar1 = wcurVal;
            }
        
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
       
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
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
        //starts ----added by abinayaa 270313 for expanding selected nodes and sub menus only ----
 
            foreach (TreeNode tnode in TreeView2.Nodes)
            {
                if (tnode.Value != wcurVal)
                {
                    tnode.Collapse();
                }
                else
                {
                    Viwstateval = wcurVal;
                    tnode.Expand();
                }
            }
            //starts ----added by abinayaa 270313 for expanding selected nodes and sub menus only ----
        
    }
    protected void SelectedNodeChanged(object sender, EventArgs e)
    {
        //  scs 161114 do not delete this method as only with this the below TreeView2_SelectedNodeChanged method works
    }
    protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
    { 
        //starts ----added by abinayaa 270313 for expanding selected nodes and sub menus only ----
        
        wcurVal = TreeView2.SelectedNode.Value;
        
        foreach (TreeNode tnode in TreeView2.Nodes)
        {
            if (tnode.Value != wcurVal)
            {
                tnode.Collapse();
            }
            else
            {
                Viwstateval = wcurVal;
                Session["Sess1curVal"] = wcurVal; //scs 161114
                Session["Sess3curVal"] = Session["Sess1curVal"];
                AccessBaseClass.SessionVar1 = Session["Sess1curVal"].ToString();
                currValue = wcurVal;
                tnode.Expand();
            }
        }
        //starts ----added by abinayaa 270313 for expanding selected nodes and sub menus only ----
    }
    //protected void lnkLogOut_Click(object sender, EventArgs e) RoutedEventArgs 
     protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        LoginInfoMsg Login=new LoginInfoMsg();
        Login.UserName=AccessBaseClass.EmployeeCode;
        Login.UserSessionId=AccessBaseClass.UserSessionId;
        Bus.UpdateUserLogoffInfo(Login);
        Session.Clear();
        Session.RemoveAll();
        string PortalLogin = ConfigurationManager.AppSettings["PortalLogin"].ToString();
        if (PortalLogin != "Y")
        {
            Response.Redirect("~/Login.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Close_Window", "self.close();", true);
            
   
        }
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
            imgExpand.ImageUrl = "~/Images/colapse1.JPG";
        }
        //starts ----added by abinayaa 270313 for expanding selected nodes and sub menus only ----

        foreach (TreeNode tnode in TreeView2.Nodes)
        {
            if (tnode.Value != wcurVal)
            {
                tnode.Collapse();
            }
            else
            {
                Viwstateval = wcurVal;
                tnode.Expand();
            }
        }
        //starts ----added by abinayaa 270313 for expanding selected nodes and sub menus only ----

    }
}
