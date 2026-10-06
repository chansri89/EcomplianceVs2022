using System;
using System.Data;
using System.Net;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Web.SessionState;

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
        Session["EmployeeCode"]=null;
        Session["EmployeeName"] = null;
        Session["HitCounter"] = null;
        Session["CompanyCode"] = null;
        Session["CompanyName"] = null;
        Session["EmployeeMailId"] = null;
        Session["ProgramMsgList"] = null;
        Session["IsHomePageAction"] = null;
    }
    public string EmployeeCode
    {
        get
        {
            return Session["EmployeeCode"]== null ? "" : Session["EmployeeCode"].ToString();
        }
        set
        {
            Session["EmployeeCode"] = value;
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
    //added by abinayaa for db size checking --110513
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
    //added by abinayaa for db size checking --110513
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
