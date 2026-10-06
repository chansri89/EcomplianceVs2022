using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using Ganini.Lib;

public partial class WCFService : System.Web.UI.Page
{ 
    #region Declaration
        ProcessBus Bus = new ProcessBus();
        //List<WCFcouponReference.Coupon> CoupList = new List<WCFcouponReference.Coupon>();
        //List<BCouponMsg> CliCoupList = new List<BCouponMsg>();
        //List<CliHdrMsg> CliCpLst = new List<CliHdrMsg>();
        //BaseClass BaseMsg = new BaseClass();
        //WCFcouponReference.Service1Client cli = new WCFcouponReference.Service1Client();
        UserAccess user = new UserAccess();
    
        DateTime FromDate = DateTime.Today;
        DateTime ToDate = DateTime.Today;
        public static string ProgramName = string.Empty;
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
        {
            //DateTime LastMonthFromtDate = DateTime.Today.AddDays(0 - DateTime.Today.Day);
            //FromDate = Convert.ToDateTime(LastMonthFromtDate.AddDays(1 - LastMonthFromtDate.Day));
            //txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");

            //DateTime LastMonthLastDate = DateTime.Today.AddDays(0 - DateTime.Today.Day);
            //ToDate = Convert.ToDateTime(LastMonthLastDate);
            //txtToDate.Text = ToDate.ToString("dd/MM/yyyy");
        }
       
       
       
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        //CoupList = cli.GetMergedData(); // Receiving data from web serive on Merged Coupon
        //string Result = Bus.WCFMergedCouponUpdate(CoupList); //Update local data base for merged status
        //if (Result == "0")
        //{
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Uploaded Successfully" + "');", true);
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + Result + "');", true);
        //}
        if (txtWebVersion.Text != txtYourVersion.Text)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "synchronised Successfully" + "');", true);
            txtYourVersion.Text = txtWebVersion.Text;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Your Version is already Synchronised" + "');", true);
        }
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        //  Coupons printed for the day from Local system
        
        if (IsView() == 0)
        {
            
            LoadCliCouponHdr(FromDate,ToDate);

        }
    }
    protected void GrdUpLoad_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //int WRowIndex = Convert.ToInt32(e.CommandArgument.ToString());
        //GridViewRow row = GrdUpLoad.Rows[WRowIndex];
        //Int64 CoupHdrId = Convert.ToInt64(((Label)row.FindControl("lblCouponHdrId")).Text);
        //List<WCFcouponReference.Coupon> PCoupList = new List<WCFcouponReference.Coupon>();
        //PCoupList = Bus.WCFGetPrintedList(CoupHdrId); // Read Data from Coupon dataa from Local Server for Upload
       
       
        try
        {
            //string Result = cli.GetPrintedData(PCoupList); // send data to WCF on PrintedCoupon.
            //if (Result == "0")
            //{
         
            //    string Updated = Bus.WCFUpdateCouponHdrForUpload(CoupHdrId);
            //    LoadCliCouponHdr(FromDate, ToDate);
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Uploaded Successfully" + "');", true);
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + Result + "');", true);
            //}
        }
        catch (Exception ex)
        {
            ExceptionHandling eh = new ExceptionHandling();
            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
        }
    }
    #endregion
    #region Methods
    private void  LoadCliCouponHdr(DateTime FromDate, DateTime ToDate)
    {
        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd/MM/yyyy";
        //FromDate = Convert.ToDateTime(txtFromDate.Text, dateinfo);
        //ToDate = Convert.ToDateTime(txtToDate.Text, dateinfo);
        //CliCpLst = Bus.WCFPrintedCouponHdrSelect(FromDate, ToDate);
        //GrdUpLoad.DataSource = "";
        //GrdUpLoad.DataSource = CliCpLst;
        //GrdUpLoad.DataBind();
       
        //if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        //{
        //    GrdUpLoad.Columns[GrdUpLoad.Columns.Count - 1].Visible = false;
        //}     
    }
 
    #endregion
    private int IsView()
    {

        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd/MM/yyyy";
        int Error = 0;
        string DisplayError = "";


        //if (Convert.ToDateTime(txtFromDate.Text.ToString(), dateinfo) > (Convert.ToDateTime(txtToDate.Text.ToString(), dateinfo)))
        //{
        //    DisplayError = DisplayError + "--" + StackResource.ErrFromToDate;
        //    Error = 1;
        //}



        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }

}