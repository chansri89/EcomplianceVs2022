using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    BaseClass BaseInfoMsg = new BaseClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (BaseInfoMsg.PassPolicy == "0")
        {
            if (BaseInfoMsg.AdminFlag == "A" || BaseInfoMsg.ExecutiveRole == "Y")
            {
                Response.Redirect("DashBoard.aspx");
            }
            else if (BaseInfoMsg.IsCompanyAdmin == true)
            {
                Response.Redirect("DashBoard.aspx");
            }
            else
            {
                Response.Redirect("DashBoard.aspx");
            }
        }
    }
}