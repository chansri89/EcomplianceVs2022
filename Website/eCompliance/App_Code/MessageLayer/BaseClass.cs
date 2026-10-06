using System.Collections.Generic;

/// <summary>
/// Summary description for BaseClass
/// </summary>
public class BaseClass : System.Web.UI.Page
{
    public BaseClass()
    {
    }
    public void clear()
    {
        Session["UserId"] = null;
        Session["EmployeeCode"] = null;
        Session["PassPolicy"] = null;
        Session["IsHomePageAction"] = null;
        Session["EmployeeName"] = null;
        Session["EmployeeMailId"] = null;
        Session["HitCounter"] = null;
        Session["CompanyCode"] = null;
        Session["CompanyName"] = null;
        Session["IsGroupLevel"] = false;
        Session["AdminFlag"] = null;
        Session["UserSessionId"] = null;
        Session["IsAuditor"] = false;
        Session["IsCompanyAdmin"] = false;
        Session["ExecutiveRole"] = 0;
        Session["LoginResult"] = null;
        Session["ProgramMsgList"] = null;
        Session["ProgramMsgList"] = null; // scs added since this list is causing problem sometimes 180814
    }

    public string UserId
    {
        get
        {
            return Session["UserId"].ToString();
        }
        set
        {
            Session["UserId"] = value;
        }
    }
    public string EmployeeCode
    {
        get
        {
            return Session["EmployeeCode"] == null ? "" : Session["EmployeeCode"].ToString();
        }
        set
        {
            Session["EmployeeCode"] = value;
        }
    }
    public string PassPolicy
    {
        get
        {
            return Session["PassPolicy"] == null ? "" : Session["PassPolicy"].ToString();
        }
        set
        {
            Session["PassPolicy"] = value;
        }
    }
    public string IsHomePageAction
    {
        get
        {
            return Session["IsHomePageAction"] == null ? "" : Session["IsHomePageAction"].ToString();
        }
        set
        {
            Session["IsHomePageAction"] = value;
        }
    }
    public string EmployeeName
    {
        get
        {
            return Session["EmployeeName"] == null ? "" : Session["EmployeeName"].ToString();
        }
        set
        {
            Session["EmployeeName"] = value;
        }
    }
    public string EmployeeMailId
    {
        get
        {
            return Session["EmployeeMailId"] == null ? "" : Session["EmployeeMailId"].ToString();
        }
        set
        {
            Session["EmployeeMailId"] = value;
        }
    }
    public string HitCounter
    {
        get
        {
            return Session["HitCounter"] == null ? "" : Session["HitCounter"].ToString();
        }
        set
        {
            Session["HitCounter"] = value;
        }
    }
    public string CompanyCode
    {
        get
        {
            return Session["CompanyCode"] == null ? "" : Session["CompanyCode"].ToString();
        }
        set
        {
            Session["CompanyCode"] = value;
        }
    }
    public string CompanyName
    {
        get
        {
            return Session["CompanyName"] == null ? "" : Session["CompanyName"].ToString();
        }
        set
        {
            Session["CompanyName"] = value;
        }
    }
    public bool IsGroupLevel
    {
        get
        {
            return Session["IsGroupLevel"] == null ? false : (bool)(Session["IsGroupLevel"]);
        }
        set
        {
            Session["IsGroupLevel"] = value;
        }
    }
    public string AdminFlag
    {
        get
        {
            return Session["AdminFlag"] == null ? "" : Session["AdminFlag"].ToString();
        }
        set
        {
            Session["AdminFlag"] = value;
        }
    }
    public int UserSessionId
    {
        get
        {
            return (int)Session["UserSessionId"] == null ? 0 : (int)Session["UserSessionId"];
        }
        set
        {
            Session["UserSessionId"] = value;
        }
    }
    public bool IsAuditor
    {
        get
        {
            return (bool)Session["IsAuditor"] == null ? false : (bool)Session["IsAuditor"];
        }
        set
        {
            Session["IsAuditor"] = value;
        }
    }
    public bool IsCompanyAdmin
    {
        get
        {
            return (bool)Session["IsCompanyAdmin"] == null ? false : (bool)Session["IsCompanyAdmin"];
        }
        set
        {
            Session["IsCompanyAdmin"] = value;
        }
    }
    public string ExecutiveRole
    {
        get
        {
            return Session["ExecutiveRole"] == null ? "0" : Session["ExecutiveRole"].ToString();
        }
        set
        {
            Session["ExecutiveRole"] = value;
        }
    }
    public string LoginResult
    {
        get
        {
            return Session["LoginResult"] == null ? "" : Session["LoginResult"].ToString();
        }
        set
        {
            Session["LoginResult"] = value;
        }
    }
    public string SessionVar1
    {
        get
        {
            return Session["SessionVar1"] == null ? "" : Session["SessionVar1"].ToString();
        }
        set
        {
            Session["SessionVar1"] = value;
        }
    }
    public List<ProgramMsg> ProgramMsgList
    {
        get
        {
            return (List<ProgramMsg>)Session["ProgramMsgList"];
        }
        set
        {
            Session["ProgramMsgList"] = value;
        }
    }
}
