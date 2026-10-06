using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using Ganini.Lib;
#region Crypt
using System.Security.Cryptography;
using Ganini.Security;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Collections;
using System.IO;
#endregion

public partial class WCFService : System.Web.UI.Page
{ 
    #region Declaration
        ProcessBus Bus = new ProcessBus();
        UserAccess user = new UserAccess();
    
        DateTime FromDate = DateTime.Today;
        DateTime ToDate = DateTime.Today;
        public static string ProgramName = string.Empty;
        #region webRef
        WebActService.Service1Client cli = new WebActService.Service1Client();
        private static WebActService.ClientChk ServerKey = new WebActService.ClientChk();
        WebActService.ClientChk Cliinf = new WebActService.ClientChk();
        WebActService.TransactMsg Tran = new WebActService.TransactMsg();
        List<WebActService.TransactMsg> TranList = new List<WebActService.TransactMsg>();
        private static List<ActMasterMsg> ActList = new List<ActMasterMsg>();
        Ganini.Lib.Util.Security Secur = new Ganini.Lib.Util.Security();
        string FirstColumn = "";
        KeyGen Key = new KeyGen();
        #endregion
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
        {
            btnStart.Enabled = true;
            pnlAdd.Visible = false;
            btnGo.Enabled = false;
        }

    }
    protected void btnStart_Click(object sender, EventArgs e)
    {
        LoadLocalMaxVersion();
        if (txtYourVersion.Text.Length > 0)
        {
            LoadWebACTMaster();
        }
        btnStart.Enabled = false;
       
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        
        //if (txtWebVersion.Text != txtYourVersion.Text)
        //{
            SaveWebACTDataLocally(ActList);
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "synchronised Successfully" + "');", true);
            txtYourVersion.Text = txtWebVersion.Text;

        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Your Version is already Synchronised" + "');", true);
        //}
        btnGo.Enabled = false;
    }
    //protected void btnPost_Click(object sender, EventArgs e)
    //{
    //    //  Coupons printed for the day from Local system

    //    if (IsView() == 0)
    //    {

    //        LoadCliCouponHdr(FromDate, ToDate);

    //    }
    //}
    //protected void GrdUpLoad_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    //int WRowIndex = Convert.ToInt32(e.CommandArgument.ToString());
    //    //GridViewRow row = GrdUpLoad.Rows[WRowIndex];
    //    //Int64 CoupHdrId = Convert.ToInt64(((Label)row.FindControl("lblCouponHdrId")).Text);
    //    //List<WCFcouponReference.Coupon> PCoupList = new List<WCFcouponReference.Coupon>();
    //    //PCoupList = Bus.WCFGetPrintedList(CoupHdrId); // Read Data from Coupon dataa from Local Server for Upload


    //    try
    //    {
    //        //string Result = cli.GetPrintedData(PCoupList); // send data to WCF on PrintedCoupon.
    //        //if (Result == "0")
    //        //{

    //        //    string Updated = Bus.WCFUpdateCouponHdrForUpload(CoupHdrId);
    //        //    LoadCliCouponHdr(FromDate, ToDate);
    //        //    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Uploaded Successfully" + "');", true);
    //        //}
    //        //else
    //        //{
    //        //    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + Result + "');", true);
    //        //}
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionHandling eh = new ExceptionHandling();
    //        eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
    //    }
    //}
    #endregion
    #region Methods
    private void LoadLocalMaxVersion()
    {
        try
        {
            string MaxVersion = Bus.ActMaxVersionSelect();
            txtYourVersion.Text = MaxVersion;
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Error while fetching Max Version on Local System" + "');", true);
            txtYourVersion.Text = "";
        }


    }
    private void LoadWebACTMaster()
    {
        string Result = CliInfo(); // Establish Handshake with Webservice 
        if (Result == "OK") // Authorised to access
        {
            try
            {
                
                TranList = cli.SynchActMasterData(ServerKey); // Generate ACT Data
                if (TranList.Count == 0) // No data for ACT
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "WebServer Returned No data. Possibly your are Synchronized.." + "');", true);
                    txtWebVersion.Text = ""; 
                    pnlAdd.Enabled = false;
                    btnGo.Enabled = false;
                }
                else // data from grid decrypt and store data in list and display in grid
                {
                    ActList = DecryptACTData(TranList); //Decrypt and split the data
                    if (FirstColumn == "0")
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "On Decryption No data found " + "');", true);
                    }
                    else
                    {
                        pnlAdd.Visible = true;
                        btnGo.Enabled = true;
                    }
                    
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Web Access Not OK.. Pls Check Internet Connection and Retry " + "');", true);
            }
               
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Web Access Not OK.. Access May Not be from Valid System " + "');", true);
        }

    }
    private void ActDownLoadedWebUpdate()
    {
        try
        {
            string UpdateResult = cli.ActDataDownLoaded(ServerKey); // Generate ACT Data
            if (UpdateResult == "0") // No data for ACT
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Act DownLoad Updated Web Successfully" + "');", true);
            }
            else 
            {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + UpdateResult + "');", true);
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Web Access Not OK.. Pls Check Internet Connection and Retry " + "');", true);
        }
    }
  

    private void SaveWebACTDataLocally(List<ActMasterMsg> PActList)
    {
        string LocalSaveResult = "";
        LocalSaveResult = Bus.WCFACTDataInsert(PActList); //Update local data base for merged status
        if (LocalSaveResult == "0")
        {
            ActDownLoadedWebUpdate(); //update web for having downloaded.
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Saving Web ACT Data to Local System Successful" + "\r\n" + "');", true);
        }
        else //Payment Advice Download FAILED on Local System 
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Saving Web ACT Data to Local System FAILED" + "\r\n" + "');", true);
        }
        btnGo.Enabled = false;
       
    }
    #endregion
    #region WCF
    private string CliInfo()
    { //Only when Server Key is OK we can do processing
        string SKey = "NO";
        try
        {
            string ClientName = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ClGud"]);
            Cliinf.ClGud = Key.EncryptPwd(ClientName); // Client Key Information to be sent
            string Bhadram = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Bhadram"]);// (Config.GetAppsetting("Bhadram").ToString());
            string BhadramEncrypt = Secur.Encrypt(Bhadram);
            //string BhadramDecrypt = Secur.Decrypt(BhadramEncrypt);
            ServerKey = cli.CliDet(Cliinf, BhadramEncrypt); //Send your conn data
            if (ServerKey.Result == "1")
            {
                if (ServerKey.ClientId.Trim().Length > 0 && ServerKey.GuId.Trim().Length > 0 && ServerKey.ClGud == Cliinf.ClGud)
                {
                    SKey = "OK"; // transact with server 
                }
                else
                {
                    SKey = "NO"; // do not do any transaction server key is not found
                }
            }
            else
            {
                SKey = "NO"; // do not do any transaction Client is not valid
            }
        }
        catch 
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Web Access Not OK.. Pls Check Internet Connection " + "');", true);
            //SKey = "NO";
        }
        return SKey;
    }
    private List<ActMasterMsg> DecryptACTData(List<WebActService.TransactMsg> tCp)
    {
        List<ActMasterMsg> PActList = new List<ActMasterMsg>();
        foreach (WebActService.TransactMsg Tmsg in tCp)
        {
            string CoupMsg = "";
            CoupMsg = DecryptData(Tmsg.TransactString);
            string[] split = CoupMsg.Split(new char[] { '^' });
            ActMasterMsg CMsg = new ActMasterMsg(); // Do not change the order below
            FirstColumn = (split[0] == null ? string.Empty : split[0].ToString());
            if (FirstColumn != "")
            {
                CMsg.ActId = Convert.ToInt32(FirstColumn);
                CMsg.ActName = (split[1] == null ? string.Empty : split[1].ToString());
                CMsg.ClassificationAct = (split[2] == null ? string.Empty : split[2].ToString());
                CMsg.ActDtlId = Convert.ToInt32(split[3] == null ? string.Empty : split[3].ToString());
                CMsg.Chapter = split[4] == null ? string.Empty : split[4].ToString();
                CMsg.Head = (split[5] == null ? string.Empty : split[5].ToString());
                CMsg.Section = (split[6] == null ? string.Empty : split[6].ToString());

                CMsg.ActRule = (split[7] == null ? string.Empty : split[7].ToString());
                CMsg.Description = (split[8] == null ? string.Empty : split[8].ToString());
                CMsg.Frequency = (split[9] == null ? string.Empty : split[9].ToString());
                CMsg.Implication = (split[10] == null ? string.Empty : split[10].ToString());
                CMsg.ImplicationSection = (split[11] == null ? string.Empty : split[11].ToString());
                CMsg.Liability = (split[12] == null ? string.Empty : split[12].ToString());
                CMsg.AffectedPerson = (split[13] == null ? string.Empty : split[13].ToString());
                CMsg.Importance = split[14] == null ? string.Empty : split[14].ToString();
                CMsg.VersionNumber = (split[15] == null ? string.Empty : split[15].ToString());
                CMsg.EffectiveDate = Convert.ToDateTime(split[16] == null ? string.Empty : split[16].ToString());
                CMsg.ExpiryDate = Convert.ToDateTime(split[17] == null ? string.Empty : split[17].ToString());

                CMsg.IsActive = Convert.ToBoolean(split[18] == null ? string.Empty : split[18].ToString());
                CMsg.ValidityStatus = Convert.ToBoolean(split[19] == null ? string.Empty : split[19].ToString());
                CMsg.IsNew = Convert.ToBoolean(split[20] == null ? string.Empty : split[20].ToString());
                CMsg.IsUpdated = Convert.ToBoolean(split[21] == null ? string.Empty : split[21].ToString());
                CMsg.MaxVersion = (split[22] == null ? string.Empty : split[22].ToString()); //
                CMsg.AuditUpLoadId = Convert.ToInt32(split[23] == null ? string.Empty : split[23].ToString()); //
                txtWebVersion.Text = CMsg.MaxVersion;
                PActList.Add(CMsg);

            }
            else /// empty data not allowed shows some error 
            {
                FirstColumn = "0";
                break;
            }
        }
        return PActList;
    }
    private string EncryptData(string clearText)
    {
        string EncryptionKey = "AdhanP@d1Nada";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
    private string DecryptData(string cipherText)
    {
        string EncryptionKey = "AdhanP@d1Nada";
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
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