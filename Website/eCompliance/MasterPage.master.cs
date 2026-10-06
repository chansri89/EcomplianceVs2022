using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;



public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string PortalLogin = ConfigurationManager.AppSettings["PortalLogin"].ToString();
        //if (PortalLogin == "SSO")
        //{
        //    Uri theRealURL = new Uri(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.RawUrl);
        //    string xyz = Request.ServerVariables["HTTP_USERNAME"];
        //    Response.Redirect("login.aspx?UserName="+xyz);
        //}
        //else
        //{
        //}
    }
}
