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
public partial class MasterPage3 : System.Web.UI.MasterPage
{
    ProcessBus Bus = new ProcessBus();
    EmployeeMasterMsg emp = new EmployeeMasterMsg();
    List<ProgramMsg> ProgramList = new List<ProgramMsg>(); 
    BaseClass AccessBase = new BaseClass();
    public static string wcurVal;
   
    public static string currValue = "1";
    public static string Mast3CurVal = "2";
    public static string Viwstateval = "";
    public static string Mast1CurVal = "";
    public static string Img3CurVal = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (AccessBase.SessionVar1 != "")
        {
            Mast3CurVal = Session["Sess1curVal"].ToString();
        }
        if (Mast3CurVal != "")
        {
            Session["Sess1curVal"] = Mast3CurVal;
            Session["Sess3curVal"] = Mast3CurVal;
        }
        if (AccessBase.EmployeeName == string.Empty)
        {
            Response.Redirect(string.Format("~/Login.aspx?IsSessionTimeOutFlag={0}", "Y"));
        }
        //lbluname.Text = Session["uname"].ToString();
        lbluname.Text = AccessBase.EmployeeName;
        lblDate.Text = Convert.ToString(System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm"));
        //lblVersionNumber.Text = Config.GetAppsetting("VERSION");
        if (!Page.IsPostBack)
        {
            
            emp.EmployeeCode = AccessBase.EmployeeCode;
            ProgramList = Bus.AdmUserAccessProgramsSelect(emp);
            //actMasterMsgList = Bus.ActMasterSelect(emp);
            AccessBase.ProgramMsgList = ProgramList;
            LoadMenu(ProgramList);

        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        //if (ddlActivity.SelectedValue == "--Select ACT Please--")
        //{

        //}
        //else
        //{
        //    Session["ActName"] = ddlActivity.SelectedValue;
        //    Response.Redirect("Default2.aspx");
        //}


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
            if (tnode.Value != Mast3CurVal)
            {
                tnode.Collapse();
            }
            else
            {
                Viwstateval = Mast3CurVal;
                tnode.Expand();
            }
        }
  
    }
    protected void lnkLogOut_Click1(object sender, EventArgs e)
    {
        LoginInfoMsg Login = new LoginInfoMsg();
        Login.UserName = AccessBase.EmployeeCode;
        Login.UserSessionId = AccessBase.UserSessionId;
        Bus.UpdateUserLogoffInfo(Login);
        Session.Clear();
        Session.RemoveAll();
        Response.Redirect("~/Login.aspx");

    }
    protected void SelectedNodeChanged(object sender, EventArgs e)
    {
        // scs 161114 do not delete this method as only with this the below TreeView2_SelectedNodeChanged method works
    }
    protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
    {
        // Abi/SCS 090313
        //Since Upoad does not work where there is Script mgr we are using Two Master pages Master1 and Master 3
        // Get Masterpage3 Session for Menu to be loaded appropriately to Current page menu if we are switching from master3 to master1
        // Aso get the Image position for EXp/Colapse button also
        // assign it to the local variabe to do the corresponding menu in UI
       
        Mast3CurVal = TreeView2.SelectedNode.Value;       
       
        //Session["Sess3imgVal"] = imgExp.ImageUrl; // assign the Masterpage3 exp/colapse button name to maintain correct UI abi/scs 090113
        foreach (TreeNode tnode in TreeView2.Nodes)
        {
            if (tnode.Value != Mast3CurVal)
            {
                tnode.Collapse();
            }

            else
            {
                Viwstateval = Mast3CurVal;
                tnode.Expand();
                Session["Sess3curVal"] = Mast3CurVal;
                Session["Sess1curVal"] = Session["Sess3curVal"];
                AccessBase.SessionVar1 = Mast3CurVal;
            }
        }

    }
    protected void imgExp_Click1(object sender, ImageClickEventArgs e)
    {

        if (imgExp.ImageUrl == "~/Images/colapse1.JPG")
        {
            td1.Visible = false;
            imgExp.ImageUrl = "~/Images/Expand1.JPG";
        }
        else
        {
            td1.Visible = true;
            //TreeView2.ExpandAll();
            imgExp.ImageUrl = "~/Images/colapse1.JPG";
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
