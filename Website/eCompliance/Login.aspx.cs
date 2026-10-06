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
using Ganini.Security;
using Resources;
using System.Text;
using System.Net.Mail;
using System.Management; //to get CPUID
using System.Web.Configuration;
#region LDAP
using System.DirectoryServices;
using System.Collections;
#endregion
#region Crypt
//using System.Security.Cryptography;
//using Ganini.Security;
//using System.Text;
//using System.ServiceModel;
//using System.ServiceModel.Channels;
//using System.Collections;
//using System.IO;
#endregion

public partial class Login : System.Web.UI.Page
{
    ProcessBus Bus = new ProcessBus();
   
    EmployeeMasterMsg emp = new EmployeeMasterMsg();
    List<ProgramMsg> ProgramMsgList = new List<ProgramMsg>();
    LoginInfoMsg LoginInfo = new LoginInfoMsg();
    KeyGen KeyGen = new KeyGen();
    BaseClass BaseInfoMsg = new BaseClass();
    string _Ip = Config.GetAppsetting("IP");
    int _Port = Convert.ToInt32(Config.GetAppsetting("_Port"));
    public static string PortalLogin = "";
    string DomainName = "";
    string EmailId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.QueryString["IsSessionTimeOutFlag"] != null && Request.QueryString["IsSessionTimeOutFlag"].ToString() == "Y")
        //{
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Due to Session time out, your login was expired. Please re-login." + "');", true);
        //} //scs 170617 firsttime login not ok gives session time out mesage above
        if (!Page.IsPostBack)
        {
            string HasLicence = "Y"; // CheckLicence(); //commented scs02122016 for virtual server processor board number cannot be taken

            if (HasLicence == "Y")
            {
                //get Machine IP
                System.Net.Dns.GetHostName(); //Get HostName Ex: Sys-4
                System.Net.IPAddress[] MachineIP = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());
                for (int IP = 0; IP < MachineIP.Length; IP++)
                {
                    LoginInfo.MachineIP = MachineIP[IP].ToString(); //Get MAchineIP Address
                }
                //

                PortalLogin = ConfigurationManager.AppSettings["PortalLogin"].ToString();
                if (PortalLogin == "Q" && Request.QueryString["EmailID"] != null) //Portal access with EmailID from Portal
                {

                    PortalLoginCheck();
                }
                else if (PortalLogin == "SSO") // scs 220201 SSO Login
                {
                    SSOLogin();
                }
                else //// if (PortalLogin != "Y" && Request.QueryString["EmailID"] == null) // Not a Portal Access but a Direct Application access
                {
                    if (PortalLogin == "Y")
                    {
                        LoadDomain();
                        lblDomain.Visible = true;
                        ddlDomain.Visible = true;
                    }
                    else
                    {
                        lblDomain.Visible = false;
                        ddlDomain.Visible = false;
                    }
                    txtUsername.Focus();
                    Panel3.Visible = true;
                    Panel1.Visible = true;
                }
            } // HAs licence check ends
            else // Licence is not valid
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Not able to Read DB. Pls Contact your Admin" + "');", true);
                Response.Redirect("CallAdmin.aspx");
            }
        }
    }
    protected void lnkForgot_Click(object sender, EventArgs e)
    {
        if (ForgotValidation() == 0)
        {
            ForgotPwd();
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (LogValidation() == 0)
        {
           
            if (PortalLogin == "N") //Norml login 
            {
                LoginInfo.UserName = txtUsername.Text.Trim();
                //LoginInfo.Password = txtPwd.Text.Trim();
                LoginInfo.Password = KeyGen.EncryptPwd(txtPwd.Text.Trim()); // encrypt password and send scs 180615
                string x = KeyGen.EncryptPwd(txtPwd.Text.Trim());
                LoginInfo = Bus.CheckLogin(LoginInfo);
                if (LoginInfo.Result == "0" || LoginInfo.Result == "1")//added by abinayaa for db size checking --230513 "|| LoginInfo.Result == "1" "
                {
                    LoadBaseInfo();
                    Response.Redirect("Default.aspx");//Abinayaa 200812 Added for First Screen
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + LoginInfo.Result + "');", true);
                    txtUsername.Text = string.Empty;
                    txtPwd.Text = string.Empty;
                }
            }
            else // this is portal login enabled
            {
                // Write LDAP Method to get users EmailID
                //EmailId = LDAPEmailID
                EmailId = "scs@abc.com";
                PortalLoginCheck();
            }
        }
    }
    private string CheckLicence()
    {
        KeyGen Key = new KeyGen();
        string OK = "";
        string ProcId = "tafeprocessor"; //GetProcessorId(); //virtual machine will not give processor board
        string BoardId = "tafeBoardId"; //GetBoardProductId();
        string ProcBoardId = Key.EncryptPwd(ProcId + BoardId);
        string ClGud = Bus.AdmGetHoldingCompanySp();
        string ClGudKey = ClGud.Substring(0, 1); // this should be 9 for fresh installation harcoded.
        ClGud = ClGud.Substring(1, ClGud.Length - 1);
        string FreshKey = "G2n1n1EC0mpl1ance"; //Key denoting Fresh Instalation. // fJljc27RBCPdh6StNRfKjSBuEguUaIWwBCO/uoJ92vA=
        FreshKey = Key.EncryptPwd(FreshKey);
        if (ClGud == FreshKey && ClGudKey=="9") // for fresh installation and lastslno in companyparameter is 9 get processor ID and BoardID of CPU
        {
            string Result = Bus.CompanyParameterUpdate(ProcBoardId);
            if (Result =="0")
            {
                OK = "Y";
            }
            else 
            {
                OK = "N";
            }
        }
        else if (ProcBoardId == ClGud) 
        {
            OK = "Y";
        }
        else
        {
            OK = "N";
        }
        return OK;
    }
    public int LogValidation()
    {
        string ErrorMsg = "";
        int Error = 0;
        if (txtUsername.Text.Trim() == "")
        {
            ErrorMsg =ErrorMsg + StackResource.ErrUserName+". ";
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.ErrUserName + "');", true);
            txtUsername.Focus();
            Error = 1;
        }
        if (txtPwd.Text.Trim() == "")
        {
            ErrorMsg = ErrorMsg + StackResource.ErrEmployeePassword + ". ";
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.ErrEmployeePassword+ "');", true);
            txtPwd.Focus();
            Error = 1;
        }
        if (PortalLogin == "Y" && ddlDomain.SelectedIndex == 0)
        {
            ErrorMsg = ErrorMsg + "Domain Name should be selected for Portal Login. ";
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Domain Name should be selected for Portal Login" + "');", true);
            ddlDomain.Focus();
            Error = 1;
        }

        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + ErrorMsg + "');", true);
        }

        return Error;
    }
    public int ForgotValidation()
    {
        int Error = 0;
        if (txtUsername.Text.Trim() == "" || Convert.ToInt32(txtUsername.Text.Length.ToString().Trim()) == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.ErrForgotPwd + "');", true);
            Error = 1;
        }

        return Error;
    }
    #region Methods
    private void LoadDomain()
    {        
        List<DomainMsg> DomList = new List<DomainMsg>();
        DomList = Bus.DomainSelect();
        ddlDomain.DataTextField = "DomainName";
        ddlDomain.DataValueField = "DomainName";
        ddlDomain.DataSource = DomList;
        ddlDomain.DataBind();
        ddlDomain.Items.Insert(0, new ListItem("-- Select Please --", "0"));
       
    }
    private void PortalLoginCheck()
    {
        if (PortalLogin == "Q")
        {
            Uri theRealURL = new Uri(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.RawUrl);

            string Scheme = HttpContext.Current.Request.Url.Scheme;
            string author = HttpContext.Current.Request.Url.Authority;
            string rawurl = HttpContext.Current.Request.RawUrl;

            EmailId = HttpUtility.ParseQueryString(theRealURL.Query).Get("EmailID");
        }
     
        //EmailId = "scs@abc.com"; //for testing Offline when Emailid is passed from another portal scs100116
        //EmpName.Text = "Emp Name: " + HttpUtility.ParseQueryString(theRealURL.Query).Get("EmpName");
        //scheme.Text = "SCHEME: " + Scheme;
        //Author.Text = "AUthor: " + author;
        //Raw.Text = "RAw: " + rawurl;
        if (EmailId != "" )
        {
            LoginInfo.EmailID = EmailId;
            LoginInfo = Bus.PortalLoginCheck(LoginInfo);
            if (LoginInfo.Result == "0" || LoginInfo.Result == "1")//added by abinayaa for db size checking --230513 "|| LoginInfo.Result == "1" "
            {
                LoadBaseInfo();
                Response.Redirect("Default.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + LoginInfo.Result +". Ask your Admin"+ "');", true);
                Panel3.Visible = false;
                Panel1.Visible = false;
            }
        }
        else
        {
            Panel3.Visible = false;
            Panel1.Visible = false;
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Not a Valid EmailID or User is Not Valid" + "');", true);
        }

    }
    private void SSOLogin()
    {
        Uri theRealURL = new Uri(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.RawUrl);
        string UserName = Request.ServerVariables["HTTP_USERNAME"];
        string QUserName = Request.QueryString["UserName"];
        if (UserName == null )
        {
            if (QUserName != null)
            {
                UserName = QUserName;
            }
        }

        if (UserName != null)
        {
            LoginInfo.UserName = UserName; //txtUsername.Text.Trim();
            LoginInfo.Password = "SSO"; // Harcoded and sent to DB for Login check
            LoginInfo = Bus.CheckLogin(LoginInfo);
            if (LoginInfo.Result == "0" || LoginInfo.Result == "1")//added by abinayaa for db size checking --230513 "|| LoginInfo.Result == "1" "
            {
                LoadBaseInfo();
                Response.Redirect("Default.aspx");//Abinayaa 200812 Added for First Screen
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + LoginInfo.Result + "');", true);
                Response.Redirect("CallAdmin.aspx");
                //txtUsername.Text = string.Empty;
                //txtPwd.Text = string.Empty;
            }
        }
        else
        {   ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + LoginInfo.Result + "');", true);
            Response.Redirect("CallAdmin.aspx");
         
        }      
    }
    private void LoadBaseInfo()
    {
        BaseInfoMsg.EmployeeName = LoginInfo.EmployeeName;
        BaseInfoMsg.EmployeeCode = LoginInfo.EmployeeCode;
        BaseInfoMsg.CompanyName = LoginInfo.CompanyName;
        BaseInfoMsg.CompanyCode = LoginInfo.CompanyCode;
        BaseInfoMsg.UserSessionId = LoginInfo.UserSessionId;
        BaseInfoMsg.LoginResult = LoginInfo.Result;//added by abinayaa for db size checking --230513
        //BaseInfoMsg.IsAuditor = LoginInfo.IsAuditor;
        BaseInfoMsg.IsCompanyAdmin = LoginInfo.IsCompanyAdmin;
        BaseInfoMsg.PassPolicy = LoginInfo.PassPolicy;
        BaseInfoMsg.ExecutiveRole = LoginInfo.ExecutiveRole; //scs 140217 for dash board of executive role
 
    }
    public void ForgotPwd()
    {
        //commeted by abinayaa 130713
        EmployeeMasterMsg Employee = new EmployeeMasterMsg();
        emp.EmployeeCode = txtUsername.Text;
        Employee = Bus.GetForgotPassword(emp);
        MailMessage mail = new MailMessage();
        if (Employee.EmployeeResult == "0")
        {
            if (SendMail(Employee.EmployeeName, Employee.Password, Employee.EmailId, Employee.EmployeeCode))
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Your Password is sent to your Email Id" + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Mail Not Sent. Please Contact you Admin." + "');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Invalid User Name" + "');", true);
        }
        //commeted by abinayaa 130713

        //string Result;
        //emp.EmployeeCode = txtUsername.Text;
        //Result = Bus.GetForgotPassword(Employee);
        //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + Result + "');", true);
    }
    public static string GetProcessorId()
    {

        ManagementClass mc = new ManagementClass("win32_processor");
        ManagementObjectCollection moc = mc.GetInstances();
        String Id = String.Empty;
        foreach (ManagementObject mo in moc)
        {

            Id = mo.Properties["processorID"].Value.ToString();
            break;
        }
        return Id;

    }
    public static string GetBoardProductId()
    {

        ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");

        foreach (ManagementObject wmi in searcher.Get())
        {
            try
            {
                return wmi.GetPropertyValue("Product").ToString();

            }

            catch { }

        }

        return "Product: Unknown";

    }
    public bool SendMail(string EmployeeName, string Password, string EmailId, string EmployeeCode)
    {
        bool IsSuccess = false;
        MailMessage mail = new MailMessage(Config.GetAppsetting("FromMail"), EmailId);
        try
        {
            SmtpClient Client = new SmtpClient(_Ip, _Port);
            mail.Subject = Config.GetAppsetting("Subject");
            StringBuilder sb = new StringBuilder();
            sb.Append("<p> Dear " + EmployeeName + ",<p>");
            sb.Append("<br/>");
            sb.Append("<style type=" + '"' + "text/css" + '"' + ">           table {font-size: 75%;}           </style><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Config.GetAppsetting("BodyMessage") + "<p>");
            sb.Append("<table border=" + '"' + 1 + '"' + ">");
            sb.Append("<tr style=" + '"' + "color:Blue" + '"' + "> <td width=" + '"' + "90px" + '"' + "> User Name : </td> <td width=" + '"' + "90px" + '"' + ">" + EmployeeCode + " </td> </tr>");
            sb.Append("<tr style=" + '"' + "color:Blue" + '"' + "> <td width=" + '"' + "90px" + '"' + "> Password : </td> <td width=" + '"' + "90px" + '"' + ">" + Password + " </td> </tr>");
            sb.Append("</table>");
            sb.Append("<p> Please note: Your username & password are both case sensitive.<p>");
            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append("<p>" + Config.GetAppsetting("BodyMessage2") + "<p>");
            mail.Body = sb.ToString();
            mail.IsBodyHtml = true;
            Client.UseDefaultCredentials = true;
            Client.Send(mail);
            IsSuccess = true;
        }
        catch (Exception ex)
        {
            IsSuccess = false;
            ExceptionHandling eh = new ExceptionHandling();
            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.UI);
        }
        return IsSuccess;
    }
    #endregion
}