using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Xml.Linq;
using System.Drawing;
using AjaxControlToolkit;
using System.IO;
using Ganini.Lib;
using System.Text;
using System.Net.Mail;
using GeneratingMail;
public partial class ActivityActionMail : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    string ErrorFlag = null;
    string FromMail = ConfigurationManager.AppSettings["FromMail"].ToString();
    string IP = ConfigurationManager.AppSettings["IP"].ToString();
    int _Port = Convert.ToInt32(ConfigurationManager.AppSettings["_Port"].ToString());

    int QuarterlyMonth = 0;
    bool QuarterlyTrigger = false;
    int HalfyearlyMonth = 0;
    bool HalfyearlyTrigger = false;
    int RedAlertDay = Convert.ToInt32(Config.GetAppsetting("RedAlertDay"));
    string Message = "Activity Action posted";
    MailingClass mail = new MailingClass();
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ActivityActionInsert();
            lblMessage.Text = Message;
        }
    }
    #endregion

    #region Methods

    public void ActivityActionInsert()
    {
        try
        {
            mail.CreateNewActivityAction();
            mail.CreateNewActivityActionWeekly();
        }
        catch (Exception ex)
        {
            ExceptionHandling eh = new ExceptionHandling();
            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
        }
        finally
        {
        }
    }
    
    #endregion 
}