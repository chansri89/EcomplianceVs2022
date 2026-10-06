using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using Ganini.Lib;
using System.Data.OleDb;
using System.Data;
using Resources;

public partial class UpLoadActs : System.Web.UI.Page
{
    #region Declaration
    ExcelUploadMsg exceUploadMsg = new ExcelUploadMsg();
    ProcessBus Bus = new ProcessBus();
    BaseClass BaseMsg = new BaseClass();
    UserAccess user = new UserAccess();
    public static string ProgramName = string.Empty;
    //public static List<WeighmentMsg> WeighmentUploadList = new List<WeighmentMsg>();
    public static int OperationCount;
    //int DBError = 0;
    string XLfilepath = "";
    string filename = "";
    string Validmsg = "";
    string ConnectionString = "";
    string Query = "";
    string Filetype = "";
    int ExFormat = 0;
    string OraFlag = "U";
    int ChkResult = 0;
    #endregion Declaration
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
        {
            lblMessage.Text = "";
            lblMessage.Visible = false;
        }
        lblMessage.Visible = false;
        btnSave.Visible = false;
        btnUpload.Enabled = true;
        PnlWtstatus.Visible = false;
        PnlWtOK.Visible = false;
    }
  
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
        if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.CreatePermissionRestricted + "');", true);
        }
        else
        {
            lblMessage.Visible = false;
            System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
            dateinfo.ShortDatePattern = "dd/MM/yyyy";
            filename = Path.GetFileName(FlUpdExcel.FileName);
            string ValidMsg = ValidateUploadedFile();
            if (ValidMsg == "")
            {
                uploadFile();
                if (lblMessage.Text.Length == 0) //error while uploading
                {
                    UpLoadType();
                }
                //lblMessage.Visible = false;
            }
            else
            {
                lblMessage.Text = ValidMsg;
                lblMessage.Visible = true;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //string OraResult = "";
        //Int64 WparamId = Convert.ToInt64(txtWParameterId.Text);
        //UploadResultMsg UpResult = Bus.WeighmentSave(WparamId); //saving the correct data
        //if (UpResult.Result == "0")
        //{
        //    OraFlag = "O";
        //    //List<WeighmentMsg> WeighLst = new List<WeighmentMsg>();
        //   // WeighLst = Bus.WeighmentUploadSelect(WparamId, OraFlag); // Read from SQL
        //    //WeighLst = Bus.WeighmentPendingUploadSelect();// Read from SQL db all records not uploaded till then
        //    //OraResult = Bus.WeighmentOracleConnSave(WeighLst); //write to Oracle
        //   if (OraResult == "0")
        //   {
        //       OraResult = Bus.UpdateSQLDB();
        //       if (OraResult == "0")
        //       {
        //            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Uploaded Successfully" + "');", true);
        //       }
        //   }
        //   else
        //   {
        //       //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Upload in Oracle Failed" + "');", true);
        //       lblMessage.Text = OraResult;
        //       lblMessage.Visible = true;
        //   }
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + UpResult.Result + "');", true);
        //}
    }
   
    #endregion Events

    #region Methods
  
  
            
    #endregion Methods
    #region Excelupl
    //public void DeleteuploadedFile() //// not used in program
    //{
    //    HttpPostedFile UploadFile = FlUpdExcel.PostedFile;

    //    XLfilepath = ConfigurationManager.AppSettings["FolderPath"].ToString(); // scs 0703
    //    if (File.Exists(@XLfilepath + filename))
    //    {
    //        File.Delete(@XLfilepath + filename);
    //    }
    //    FlUpdExcel.SaveAs(XLfilepath + filename);
    //} 
    public void uploadFile()
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
        string WDate = System.DateTime.UtcNow.AddMinutes(330).ToString("yyyyMMdd_HHmm");
        filename = WDate + filename;
        try
        {
            XLfilepath = ConfigurationManager.AppSettings["FolderPath"].ToString(); // scs 0703
            if (File.Exists(@XLfilepath + filename))
            {
                File.Delete(@XLfilepath + filename);
            }
            FlUpdExcel.SaveAs(XLfilepath + filename);
        }
        catch
        {
            lblMessage.Text = "Check if File is being Used or you have Permission";
            lblMessage.Visible = true;
        }
    }
    public void UpLoadType()
    {//Though we could have the method as in below. this is kept to generalize UploadPortalFile on a latter date scs 300613
        Validmsg = "";
        ExFormat = Convert.ToInt32(rbtExcelType.SelectedValue);
        if (rbtExcelType.SelectedValue == "2")
        {
            ExFormat = 2;
            Filetype = "AudiActs";
           // NormalCSVUpload();
        }
        else
        {
            ExFormat = 1;
            Filetype = "AudiActs"; // "CreditDays";
            XLUpload();
        }



    }
    public void XLUpload()
    {
        #region XlRead
        lblMessage.Visible = true;
        lblMessage.Text = "XLUpload Begins-";
        List<ExcelUploadMsg> excelUploadMsgList = new List<ExcelUploadMsg>();
        HttpPostedFile UploadFile = FlUpdExcel.PostedFile;
        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();

        dateinfo.ShortDatePattern = "dd/MM/yyyy";
        DataTable excelData = new DataTable("ExcelData");
        Query = "SELECT * FROM [" + "Sheet1" + "$]";
        //Query = "SELECT * FROM [" + ConfigurationManager.AppSettings["ExcelFileSheetName"].ToString() + "$]";
        //ConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + @XLfilepath + filename + ";Extended Properties=Excel 8.0";  //scs 250214 to read all columns as String
        //ConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + @XLfilepath + filename + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
        //if (ConfigurationManager.AppSettings["ExcelExtension"].ToString() == ".xls")
        //{
            ConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + @XLfilepath + filename + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
        //}
        //else
        //{
        //    ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @XLfilepath + @filename + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1'"; //HDR =Yes measns data is having column heading
            //}
            #region scs select column
            //string ExcelConnection = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source =" + @XLfilepath + filename + " ; Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';";
            //string ExcelConnection = @"Provider=Microsoft.Jet.OleDb.4.0;Data Source =" + @XLfilepath + filename + " ; Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';";
            //OleDbDataAdapter Id = new OleDbDataAdapter("SELECT MAX(len(Description)) FROM [Sheet1$H1:H1000]", ExcelConnection);
            //DataSet id = new DataSet();
            //Id.Fill(id);
            //string ExcelConnection = @"Provider=Microsoft.Jet.OleDb.4.0;Data Source =" + @XLfilepath + filename + " ; Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';";
            //string Command = "SELECT (Description), (Len(Description)) as WLen FROM [Sheet1$H1:H1000]";

            //try
            //{
            //    OleDbConnection conn = new OleDbConnection(ExcelConnection);
            //    OleDbCommand cmd = new OleDbCommand();
            //    OleDbDataAdapter da = new OleDbDataAdapter();

            //    using (conn)
            //    {
            //        conn.Open();
            //        cmd = new OleDbCommand(Command, conn);
            //        da = new OleDbDataAdapter(cmd);


            //        DataSet id = new DataSet();
            //        da.Fill(id);

            //        DataTable idtable = id.Tables[0];
            //        idtable.DefaultView.Sort = idtable.Columns[1].ColumnName + " " + "DESC";
            //        idtable = idtable.DefaultView.ToTable();
            //        Console.WriteLine(idtable.Rows[0][0]);
            //        conn.Close();
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
            #endregion
            OleDbConnection conne = new OleDbConnection(@ConnectionString);
            OleDbCommand command = new OleDbCommand(Query, conne);
        OleDbDataReader dbdr;
        #endregion
        #region XLtoMemory
        try
        {
            lblMessage.Text = lblMessage.Text + " XLUpload Execute reader-"; //scs 251215
            OraFlag = "U";
            conne.Open();
            dbdr = command.ExecuteReader(CommandBehavior.CloseConnection);
            //int a = dbdr.VisibleFieldCount; //scs to get number of columns in xl
            excelData.Load(dbdr); //09122012
            excelUploadMsgList = OperationUpLoad(excelData);
        }
        catch (Exception ex)
        {
            lblMessage.Text = lblMessage.Text+ ex.Message.ToString();
            //lblMessage.Text = ConfigurationManager.AppSettings["FileError"].ToString();
            lblMessage.Visible = true;
            //ExceptionHandling eh = new ExceptionHandling();
            //eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Error --> File not proper or extension not csv or xls  " + "');", true);

        }
        finally
        {
            if (conne != null)
            {
                ConnectionString = "";
                command.Dispose();
                conne.Close();
            }
        }
        #endregion
        #region TempInsert
         UploadResultMsg UpMsg = new UploadResultMsg();
        try
        {
            if (excelUploadMsgList != null && excelUploadMsgList.Count > 0)
            {
                lblMessage.Text = lblMessage.Text + " chk xl data begin-"; //scs 251215
                    ChkResult = 0; // set to OK condition
                    List<DataErrorMsg> ChkData = CheckData(excelUploadMsgList);
                    if (ChkResult == 0) //Data fine
                    {
                        lblMessage.Text = lblMessage.Text + " Tempinsert begin-"; //scs 251215
                       UpMsg = Bus.ExceUploadTempInsert(excelUploadMsgList, Filetype);
                       lblMessage.Text = lblMessage.Text + " Tempinsert Over-"; //scs 251215
                    }
                    else
                    {
                        LoadGrdUpLoadNotOK(ChkData);
                    }
              }
        }
        catch (Exception ex)
        {
            lblMessage.Text = lblMessage.Text + ex.Message.ToString(); //scs 251215
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "UpLoad Failed On Temp Table Insert- "+ex + "');", true);
        }

     #endregion
        #region AfterTempInsert
        try
        {
            if (ChkResult == 0)
            {
                if (UpMsg.Result.Substring(0, 1) == "0")
                {
                    List<ORGUpLoadStatus> MUpLoadList = new List<ORGUpLoadStatus>();
                    Filetype = ""; //reinitialise
                    Int64 WParameterId = UpMsg.WParameterId;
                    //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Successfully Uploaded File with " + UpMsg.Result.Substring(1, (UpMsg.Result.Length - 1)) + "');", true);
                    UploadResultMsg Uploadinsert = new UploadResultMsg();
                    Uploadinsert = Bus.ActMasterInsert(WParameterId);

                    if (Uploadinsert.Result.Substring(0, 1) == "0")
                    {
                        MUpLoadList = Bus.ActMasterUploadSelect(WParameterId);
                        LoadGrdUpLoadStatus(MUpLoadList);         //LoadGrdUpLoadDup(WeighmentUploadList);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "UpLoad Failed while inserting to Mech Master " + "');", true);
                    }
                }
                else
                {
                    //Error      //UploadtoDestination(FileName, Ke30ErrorFolder, Info.Extension);            //lblMessage.Text = "Error --> Check Data in the file  ";
                    Filetype = ""; //reinitialise
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "UpLoad Failed while inserting in Temp Table" + "');", true);
                }
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "UpLoad Failed On Temp Table Insert " + "');", true);
        }

        #endregion
    }
    public string ValidateUploadedFile()
    {
        HttpPostedFile UploadFile = FlUpdExcel.PostedFile;

        if (FlUpdExcel.FileName == "")
        {
            Validmsg = Validmsg + " Please select File. ";
        }
        else
        {
            filename = Path.GetFileName(FlUpdExcel.FileName);
            if (UploadFile.FileName == null)
            {
                Validmsg = Validmsg + " FileName Not valid and Uploaded. ";
            }
            else if (UploadFile.ContentLength > 0)
            {
                Validmsg = "";
            }
            else
            {
                Validmsg = Validmsg + " Data not found Please check the data. ";
            }

            string fileExt = System.IO.Path.GetExtension(UploadFile.FileName);
            if (fileExt.ToLower() != ConfigurationManager.AppSettings["ExcelExtension"].ToString() &&
                (fileExt.ToLower() != ConfigurationManager.AppSettings["CSVExtension"].ToLower().ToString()))
            {
                Validmsg = Validmsg + " Check File Extension for xls or csv. ";
            }
 
            else
            {
                Validmsg = "";
                //UploadPortalCustomer = 0;
            }
        } // Validate for File extension,data count scs 280613
        return Validmsg;
    }
    public List<ExcelUploadMsg> OperationUpLoad(DataTable dtCoup)
    {
        List<ExcelUploadMsg> excelUploadList = new List<ExcelUploadMsg>();
        int linecount = 0;
        foreach (DataRow dr in dtCoup.Rows)
        {
            if (linecount >= 0) // first row contains heading in xl
            {
                if (dr[1].ToString() != string.Empty) // ACT name  column should not be empty 
                {
                 //  Slno	Acts	Chapter	Head	Section ActRule	Classificationofact
                    //  	Description	Freq	Implication	implicationsection	Liability	Affectedperson	
                    //Importance	Startdate	Expirydate	Validitystatus	VersionNumber	IsNew	IsUpdated
                    ExcelUploadMsg excelUpload = new ExcelUploadMsg();
                    excelUpload.Column1 = dr[0] == null ? string.Empty : dr[0].ToString(); //slno
                    excelUpload.Column2 = dr[1] == null ? string.Empty : dr[1].ToString(); //Acts
                    excelUpload.Column3 = dr[2] == null ? string.Empty : dr[2].ToString();//Chapter
                    excelUpload.Column4 = dr[3] == null ? string.Empty : dr[3].ToString();//Head
                    excelUpload.Column5 = dr[4] == null ? string.Empty : dr[4].ToString(); //Section
                    excelUpload.Column6 = dr[5] == null ? string.Empty : dr[5].ToString(); //Act Rule
                    excelUpload.Column7 = dr[6] == null ? string.Empty : dr[6].ToString(); // Classification of act
                 //  	Description	Freq	Implication	implicationsection	Liability	Affectedperson	
                    //Importance	Startdate	Expirydate	Validitystatus	VersionNumber	IsNew	IsUpdated
                    excelUpload.Column8 = dr[7] == null ? string.Empty : dr[7].ToString(); //Description
                    excelUpload.Column9 = dr[8] == null ? string.Empty : dr[8].ToString(); // Freq
                    excelUpload.Column10 = dr[9] == null ? string.Empty : dr[9].ToString(); //Implication
                    excelUpload.Column11 = dr[10] == null ? string.Empty : dr[10].ToString(); //implication section
                    excelUpload.Column12 = dr[11] == null ? string.Empty : dr[11].ToString(); //Liability
                    excelUpload.Column13 = dr[12] == null ? string.Empty : dr[12].ToString(); //Affected person	
                    //Importance	Startdate	Expirydate	Validitystatus	VersionNumber	IsNew	IsUpdated
                    excelUpload.Column14 = dr[13] == null ? string.Empty : dr[13].ToString();//Importance
                    excelUpload.Column15 = dr[14] == null ? string.Empty : dr[14].ToString(); // Start date
                    excelUpload.Column16 = dr[15] == null ? string.Empty : dr[15].ToString(); //Expiry date	
                    excelUpload.Column17 = dr[16] == null ? string.Empty : dr[16].ToString(); //	Validity status
                    excelUpload.Column18 = dr[17] == null ? string.Empty : dr[17].ToString(); //Version Number	
                    excelUpload.Column19 = dr[18] == null ? string.Empty : dr[18].ToString(); //Is New
                    excelUpload.Column20 = dr[19] == null ? string.Empty : dr[19].ToString(); //Is Updated

                        //excelUpload.Column21 = dr[20] == null ? string.Empty : dr[20].ToString(); //district
                        //excelUpload.Column22 = dr[21] == null ? string.Empty : dr[21].ToString(); //state
                        //excelUpload.Column23 = dr[22] == null ? string.Empty : dr[22].ToString(); //region
                        //excelUpload.Column24 = dr[23] == null ? string.Empty : dr[23].ToString(); //Town 
                        //excelUpload.Column25 = dr[24] == null ? string.Empty : dr[24].ToString(); //Pincode
                        //excelUpload.Column26 = dr[25] == null ? string.Empty : dr[25].ToString(); //Alt telno
                        //excelUpload.Column27 = dr[26] == null ? string.Empty : dr[26].ToString(); //Nominee
                        //excelUpload.Column28 = dr[27] == null ? string.Empty : dr[27].ToString(); //Relation
                        //excelUpload.Column29 = dr[28] == null ? string.Empty : dr[28].ToString(); //mechtype
                        //excelUpload.Column30 = dr[29] == null ? string.Empty : dr[29].ToString(); //comp name
                        excelUpload.Column31 = filename.ToString(); //fname
                        excelUpload.Column32 = ExFormat.ToString(); //exformat
                        excelUpload.UploadType = ExFormat; //used for upload file type
                        excelUploadList.Add(excelUpload);    
                }
            }
            linecount++;
        }
        return excelUploadList;
    }
    private List<DataErrorMsg> CheckData(List<ExcelUploadMsg> ExData)
    {
        List<DataErrorMsg> DataErr = new List<DataErrorMsg>();

        int RCount = 0;
        ChkResult = 0; // set OK
        foreach (ExcelUploadMsg ExMsg in ExData)
        {
            DataErrorMsg dtMsg = new DataErrorMsg();
            int ChkResult1 = 0;
            if (RCount == 0) //skip the first row dummy -- to ensure more than 255 character in xl column is read properly.
            {
                if (ExMsg.Column1 != "0" || ExMsg.Column8.Length < 256 || ExMsg.Column10.Length < 256)
                {
                    dtMsg.OrganizationCode = ExMsg.Column1;
                    dtMsg.OrganizationName = ExMsg.Column2;
                    dtMsg.Result = dtMsg.Result + "First Row to be dummy - Serial Number to be 0 with columns Description and Implication section having More than 256 dummy Characters  ";
                    ChkResult1 = 1;
                    ChkResult = ChkResult + ChkResult1;
                    DataErr.Add(dtMsg);
                }
                    RCount = RCount + 1;
            }
            else
            {
               
                RCount = RCount + 1;

                string RKount = Convert.ToString(RCount);
                dtMsg.RKount = RKount;
                dtMsg.OrganizationCode = ExMsg.Column1;
                dtMsg.OrganizationName = ExMsg.Column2;
                dtMsg.Result = "";
               
                // string ChkDate ="";
                //Acts	Chapter	Head	Section ActRule	Classificationofact
                if (ExMsg.Column2.Trim() == "" || ExMsg.Column2.Length == 0 || ExMsg.Column2.Length > 128)
                {
                    dtMsg.Result = dtMsg.Result + "ACT is Mandatory Or Length is More than 128 Characters";
                    ChkResult1 = 1;
                }
                if ((ExMsg.Column3.Length != 0 && ExMsg.Column3.Length > 32) ||
                    (ExMsg.Column4.Length != 0 && ExMsg.Column4.Length > 32) ||
                    (ExMsg.Column5.Length != 0 && ExMsg.Column5.Length > 32) ||
                    (ExMsg.Column6.Length != 0 && ExMsg.Column6.Length > 32))
                {
                    dtMsg.Result = dtMsg.Result + "- Chapter or Head or Section or ACTRule  cannot be empty or More than 32 Characters";
                    ChkResult1 = 1;
                }
                if (ExMsg.Column7.Length != 0 && ExMsg.Column7.Length > 64)
                {
                    dtMsg.Result = dtMsg.Result + " - Classification of ACT cannot be empty or More than 64 Characters";
                    ChkResult1 = 1;
                }

                //  	8Description 9Freq	10Implication	11implicationsection	12Liability	13Affectedperson	
                if ((ExMsg.Column8.Trim().Length != 0 && ExMsg.Column8.Length > 512) ||
                    (ExMsg.Column10.Trim().Length != 0 && ExMsg.Column10.Length > 512))
                {
                    dtMsg.Result = dtMsg.Result + "- Description or Implication cannot be empty or More than 512 Characters";
                    ChkResult1 = 1;
                }
                if ((ExMsg.Column9.Length != 0 && ExMsg.Column9.Length > 32) ||
                     (ExMsg.Column11.Length != 0 && ExMsg.Column11.Length > 64) ||
                    (ExMsg.Column12.Length != 0 && ExMsg.Column12.Length > 32) ||
                    (ExMsg.Column13.Length != 0 && ExMsg.Column13.Length > 32))
                {
                    dtMsg.Result = dtMsg.Result + "- Frequency or Implication Section or Liability or Affected Person cannot be empty or Length is More than 32 Characters";
                    ChkResult1 = 1;
                }
                //14Importance	15Startdate	16Expirydate	17Validitystatus	18VersionNumber	IsNew	IsUpdated
                if (ExMsg.Column14.Trim().Length != 0 && ExMsg.Column14.Trim().Length > 8)
                {
                    dtMsg.Result = dtMsg.Result + "- Importance cannot be empty or Length is More than 32 Characters";
                    ChkResult1 = 1;
                }
                if (ExMsg.Column15.Trim().Length != 0)
                {
                    string StDate = ConvDateFormat(ExMsg.Column15);
                    if (StDate.Substring(0, 1) == "*") // error in date format
                    {
                        dtMsg.Result = dtMsg.Result + "-" + StDate;
                        ChkResult1 = 1;
                    }
                    else
                    {
                        ExMsg.Column15 = StDate;
                    }
                }
                if (ExMsg.Column16.Trim().Length != 0)
                {
                    string EndDate = ConvDateFormat(ExMsg.Column16);
                    if (EndDate.Substring(0, 1) == "*") // error in date format
                    {
                        dtMsg.Result = dtMsg.Result + "-" + EndDate;
                        ChkResult1 = 1;
                    }
                    else
                    {
                        ExMsg.Column16 = EndDate;
                    }
                }
                if (ExMsg.Column19.Trim().Length == 0 || ExMsg.Column19.Trim().Length == 0)
                {
                    dtMsg.Result = dtMsg.Result + "-" + " IsNew and IsUpdated column should be Y or N and cannot be empty ";
                    ChkResult1 = 1;
                }
                else
                {
                    if ((ExMsg.Column19.Trim().ToUpper() != "Y" && ExMsg.Column19.Trim().ToUpper() != "N") ||
                        (ExMsg.Column19.Trim().ToUpper() != "Y" && ExMsg.Column19.Trim().ToUpper() != "N"))
                    {
                        dtMsg.Result = dtMsg.Result + "-" + " IsNew and IsUpdated column should be Y or N ";
                        ChkResult1 = 1;
                    }
                }
                if ((ExMsg.Column19 == "Y" && ExMsg.Column20 == "Y") ||
                    (ExMsg.Column19 == "N" && ExMsg.Column20 == "N"))
                {
                    dtMsg.Result = dtMsg.Result + "-" + " Cannot Have IsNew and IsUpdated same value Y or N ";
                    ChkResult1 = 1;
                }
                if (ChkResult1 != 1)
                {
                    dtMsg.Result = "OK";
                }
                ChkResult = ChkResult + ChkResult1;
                DataErr.Add(dtMsg);
            }
        }
        
        return DataErr;
    }

    private void LoadGrdUpLoadNotOK(List<DataErrorMsg> UploadList)
    {//GrdWtOk
        
        GrdWtOK.DataSource = "";
        GrdWtOK.DataSource = UploadList;
        GrdWtOK.DataBind();
        foreach (GridViewRow gvr in GrdWtOK.Rows)
        {
            Label LResult = (Label)gvr.FindControl("lblResult");
            string txtResult = LResult.Text.Trim();
            if (txtResult != "OK")
            {
                gvr.ForeColor = System.Drawing.Color.Red;
            }
        }
        PnlWtOK.Visible = true;
       
    }
    private void LoadGrdUpLoadStatus(List<ORGUpLoadStatus> UploadList)
    {//GrdWtOk

        GrdWtStatus.DataSource = "";
        GrdWtStatus.DataSource = UploadList;
        GrdWtStatus.DataBind();
        foreach (GridViewRow gvr in GrdWtStatus.Rows)
        {

            string RecStatus = Convert.ToString(((Label)gvr.FindControl("lblReason")).Text);
            if (RecStatus != "OK")
            {
                gvr.ForeColor = System.Drawing.Color.Red;
            }
        }
        PnlWtstatus.Visible = true;
        lblMessage.Text = "";
    }

    private string ConvDateFormat(string WDate)
    {
        string Error = "";
        string WDOB = "";
        char SepChar = '.';
        if (WDate.IndexOf('.', 1) > 0)
        {
            SepChar = '.';
        }
        else if (WDate.IndexOf('/', 1) > 0)
        {
            SepChar = '/';
        }
        else if (WDate.IndexOf('-', 1) > 0)
        {
            SepChar = '-';
        }
        else
        {
            Error = Error + "* Not Valid Date Separator"; // * indicates error in the return string
        }

        string[] split = WDate.Split(new char[] { SepChar });
        string dd = split[0];

        string WMMYYYY = WDate.Substring(WDate.IndexOf(SepChar, 1)); // get last digits after the 2nd date separator -yyyy ---getting /YYYY
        string DD = WDate.Substring(0, WDate.Length - WMMYYYY.Length);
        string WYYYY = WMMYYYY.Substring(WMMYYYY.IndexOf(SepChar, 1)); // we get the first dd/mm character after excluding last / in WDate getting 9/9
        string MM = WMMYYYY.Substring(1, WMMYYYY.Length - WYYYY.Length - 1); //get last digits after the 1st date separator --MM /9

        string YYYY = WYYYY.Substring(1, 4);
      
       
       // string MMYYYY = WDate.Substring((dd.Length + 1), (WDate.Length - split[0].ToString().Length - 1));
        try
        {
            if (Convert.ToInt32(dd) > 0)
                if (dd.Length == 1)
                {
                    dd = "0" + dd;
                }
                else if (dd.Length == 2)
                {

                }
                else
                {
                    Error = Error + "* Error in Format to be DD"; // * indicates error in the return string

                }
            else
            {
                Error = Error + "* Error in Date DD Format"; // * indicates error in the return string
            }
                       
            if (Convert.ToInt32(MM) > 0)
                if (MM.Length == 1)
                {

                    MM = "0" + MM;
                }
                else if (MM.Length == 2)
                {

                }
                else
                {

                    Error = Error + "* Error in Month Format to be MM"; // * indicates error in the return string
                }
            else
            {
                Error = Error + "* Error in Date MM Format";// * indicates error in the return string
            }

            if (Convert.ToInt32(YYYY) > 0)
            {
                if (YYYY.Length > 3)
                {
                    YYYY = YYYY.Substring(0, 4);
                }
                else
                {
                    Error = Error + "* Error in Year Format to be YYYY";
                }
              
            }
            else
            {
                Error = Error + "* Error in Date YYYY Format";// * indicates error in the return string
            }
            if ((Convert.ToInt32(MM) == 1 || Convert.ToInt32(MM) == 3 || Convert.ToInt32(MM) == 5 || Convert.ToInt32(MM) == 7 || Convert.ToInt32(MM) == 8 || Convert.ToInt32(MM) == 10
          || Convert.ToInt32(MM) == 12) && Convert.ToInt32(DD) > 31)
            {
                Error = Error + "* Error in Date Format";
            }
            if ((Convert.ToInt32(MM) == 4 || Convert.ToInt32(MM) == 6 || Convert.ToInt32(MM) == 9 || Convert.ToInt32(MM) == 11) && Convert.ToInt32(DD) > 30)
            {
                Error = Error + "* Error in Date Format";
            }
            if (Convert.ToInt32(MM) == 2)
            {
                int lpyr = Convert.ToInt32(YYYY) % 4;
                if (((lpyr == 0) && Convert.ToInt32(DD) > 29) || ((lpyr > 0) && Convert.ToInt32(DD) > 28))
                {
                    Error = Error + "* Error in Date Format";
                }
            }
            if (Error.Length > 0)
            {
                WDOB = Error;
            }
            else
            {
                WDOB = dd + "." + MM + "." + YYYY;
            }
            
        }
        catch
        {
            Error = Error + "* Error in Date Format only Numbers allowed";// * indicates error in the return string
            WDOB = Error;
        }

        return WDOB;
    }

    //private string VerifyDate(string WDate)
    //{
    //    string Res = "";
    //    char SepChar = '.';
    //    if (WDate.Substring(3, 1) == ".")
    //    {
    //        SepChar = '.';
    //    }
    //    else
    //    {
    //        SepChar = '/';
    //    }
    //    string[] split = WDate.Split(new char[] { SepChar });


    //    if (split[0] == "" || split[0].Length != 2 || (Convert.ToInt32(split[0]) == 0 || Convert.ToInt32(split[0]) > 31))
    //    {
    //        Res = Res+ " Check DD. ";
    //    }
    //    string MMYYYY = WDate.Substring((split[0].ToString().Length+1), (WDate.Length - split[0].ToString().Length-1));
    //    if (MMYYYY.Contains(".") || MMYYYY.Contains("/"))
    //    {
    //        string[] MM = MMYYYY.Split(new char[] { SepChar });
    //        string YYYY = MMYYYY.ToString().Substring(MM[0].ToString().Length + 1, MMYYYY.Length - MM[0].ToString().Length - 1);
    //         if (MM[0] == "" || MM[0].Length != 2 || (Convert.ToInt32(MM[0]) == 0 || Convert.ToInt32(MM[0]) > 12))
    //         {
    //             Res = Res + " Check MM. ";
    //         }
    //        if (YYYY.Length != 4)
    //        {
    //             Res = Res + " Year to have 4 digits. ";
    //        }
    //    }
    //    else
    //    {
    //        Res = Res + " Date Format should be DD.MM.YYYY. ";

    //    }
      
    //    return Res;
    //}
       //public void NormalCSVUpload()
    //{
    //    System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
    //    dateinfo.ShortDatePattern = "MM/dd/yyyy";
    //    HttpPostedFile UploadFile = FlUpdExcel.PostedFile;

    //    StreamReader InputData = null;
    //    try
    //    {
    //        lblMessage.Visible = true;
    //        List<ExcelUploadMsg> csvlist = new List<ExcelUploadMsg>();
    //        string FileName = string.Empty;
    //        //if (Config.GetAppsetting("IsServer") == "Y")
    //        //{
    //        if (ConfigurationManager.AppSettings["IsServer"].ToString().ToUpper() == "Y")
    //        {
    //            FileName = @XLfilepath + UploadFile.FileName;
    //        }
    //        else
    //        {
    //            FileName = UploadFile.FileName;
    //        }


    //        InputData = new StreamReader(FileName);

    //        int linecount = 0;
    //        int lineRead = 0;
    //        while (InputData.Peek() != -1)
    //        {
    //            string line = InputData.ReadLine();
    //            string[] split = line.Split(new char[] { ',' });
    //            if (Config.GetAppsetting("IsGRNColumn").ToString() == "Y")
    //            {
    //                lineRead = -1;
    //            }
    //            if (linecount > lineRead) //To leave the first line which contains heading
    //            {
    //                ExcelUploadMsg csv= new ExcelUploadMsg();
    //                csv.Column1 = split[0].ToString().Trim() == "" ? "0" : split[0].ToString().Trim();
    //                csv.Column2 = split[1].ToString().Trim() == "" ? "0" : split[1].ToString().Trim().Replace("'", "");
    //                csv.Column3 = split[2].ToString().Trim() == "" ? "0" : split[2].ToString().Trim();
    //                csv.Column4 = spli0t[3].ToString().Trim() == "" ? "0" : split[3].ToString().Trim();
    //                csv.Column5 = split0[4].ToString().Trim() == "" ? "0" : split[4].ToString().Trim();
    //                csv.Column6 = split[5].ToString().Trim() == "" ? "0" : split[5].ToString().Trim();
    //                csv.Column7 = split[6].ToString().Trim() == "" ? "0" : split[6].ToString().Trim();
    //                csv.Column8 = split[7].ToString().Trim() == "" ? "0" : split[7].ToString().Trim();
    //                csv.Column9 = split[8].ToString().Trim() == "" ? "0" : split[8].ToString().Trim();
    //                csv.Column10 = "0"; //split[9].ToString().Trim() == "" ? "0" : split[9].ToString().Trim();
    //                csv.Column11 = split[9].ToString().Trim() == "" ? "0" : split[9].ToString().Trim();
    //                csv.Column12 = split[10].ToString().Trim() == "" ? "0" : split[10].ToString().Trim();
    //                csv.Column13 = split[11].ToString().Trim() == "" ? "0" : split[11].ToString().Trim();
    //                csv.Column14 = "Dummy1";//split[13].ToString().Trim() == "" ? "0" : split[13].ToString().Trim();
    //                csv.Column15 = split[12].ToString().Trim() == "" ? "0" : split[12].ToString().Trim();
    //                csv.Column16 = split[13].ToString().Trim() == "" ? "0" : split[13].ToString().Trim();
    //                csv.Column17 = "Dummy2"; //split[15].ToString().Trim() == "" ? "0" : split[15].ToString().Trim();
    //                csv.Column18 = BaseMsg.EmployeeCode;
    //                csv.UploadType = ExFormat;
    //                //Deduction.Amount = Convert.ToDouble(split[12].ToString().Trim() == "" ? "0" : split[12].ToString().Trim());
    //                //Deduction.DocumentDate = split[2].ToString().Trim() == "" ? System.DateTime.Now.Date : Convert.ToDateTime(split[2].ToString().Trim().Replace('.', '/'), dateinfo);
    //                csvlist.Add(csv);
    //            }
    //            linecount = 1; //To leave the first line which contains heading
    //        }
    //        InputData.Close();
    //        int RecordCount = csvlist.Count;
    //        UploadResultMsg UpMsg = new UploadResultMsg();
    //        UpMsg = Bus.ExceUploadTempInsert(csvlist, Filetype);
    //       //if (Result == true)
    //        if (UpMsg.Result == "0")
    //        {
    //            OraFlag = "U";
    //           Filetype = ""; //reinitialise
    //           Int64 WParameterId = UpMsg.WParameterId;
    //            //WeighmentUploadList= Bus.WeighmentUploadSelect(WParameterId,OraFlag);
    //            //LoadGrdUpLoadOK(WeighmentUploadList);
    //            //LoadGrdUpLoadDup(WeighmentUploadList);
    //       }
    //       else
    //       {
    //           //Error
    //           //UploadtoDestination(FileName, Ke30ErrorFolder, Info.Extension);
    //           //lblMessage.Text = "Error --> Check Data in the file  ";
    //           Filetype = ""; //reinitialise
    //           ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + UpMsg.Result + "');", true);
    //       }
    //    }
    //    catch (Exception ex)
    //    {
    //        //if (connection != null)
    //        //{
    //        //    connection.Close();
    //        //}
    //        if (InputData != null)
    //        {
    //            InputData.Close();
    //        }

    //        lblMessage.Text = ex.Message.ToString();

    //        //lblMessage.Text = ConfigurationManager.AppSettings["FileError"].ToString();
    //        //lblMessage.Visible = true;
    //        //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + lblMessage.Text + "');", true);
    //    }
    //}


    //private void LoadGrdUpLoadDup(List<WeighmentMsg> WtUploadList)
    //{//GrdWtDup
    //    WtUploadList = (from wtup in WtUploadList where wtup.WType == "DUP" select wtup).ToList();
    //    GrdWtDup.DataSource = "";
    //    GrdWtDup.DataSource = WtUploadList;
    //    GrdWtDup.DataBind();
    //    if (WtUploadList.Count > 0)
    //    {
    //        PnlWtDup.Visible = true;
    //    }
    //}
#endregion
    #region can be deleted later
    //public ExcelUploadMsg OperationUpLoad(string[] SplitedData)
    //{

    //    ExcelUploadMsg excelUpload = new ExcelUploadMsg();
    //    excelUpload.Column1 = SplitedData[0] == null ? string.Empty : SplitedData[0].ToString();
    //    excelUpload.Column2 = SplitedData[1] == null ? string.Empty : SplitedData[1].ToString();
    //    excelUpload.Column3 = SplitedData[2] == null ? string.Empty : SplitedData[2].ToString();
    //    excelUpload.Column4 = SplitedData[3] == null ? string.Empty : SplitedData[3].ToString();
    //    excelUpload.Column5 = SplitedData[4] == null ? string.Empty : SplitedData[4].ToString();
    //    excelUpload.Column6 = SplitedData[5] == null ? string.Empty : SplitedData[5].ToString();
    //    excelUpload.Column7 = SplitedData[6] == null ? string.Empty : SplitedData[6].ToString();
    //    excelUpload.Column8 = SplitedData[7] == null ? string.Empty : SplitedData[7].ToString();
    //    excelUpload.Column9 = SplitedData[8] == null ? string.Empty : SplitedData[8].ToString();
    //    excelUpload.Column10 = SplitedData[9] == null ? string.Empty : SplitedData[9].ToString();
    //    excelUpload.Column11 = SplitedData[10] == null ? string.Empty : SplitedData[10].ToString();
    //    excelUpload.Column12 = SplitedData[11] == null ? string.Empty : SplitedData[11].ToString();
    //    excelUpload.Column13 = SplitedData[12] == null ? string.Empty : SplitedData[12].ToString();
    //    excelUpload.Column14 = SplitedData[13] == null ? string.Empty : SplitedData[13].ToString();
    //    excelUpload.Column15 = SplitedData[14] == null ? string.Empty : SplitedData[14].ToString();
    //    excelUpload.Column16 = SplitedData[15] == null ? string.Empty : SplitedData[15].ToString();
    //    return excelUpload;
    //}
    //public List<ExcelUploadMsg> AOPUpload(DataTable dtAOPSales)
    //{
    //    List<ExcelUploadMsg> excelUploadList = new List<ExcelUploadMsg>();
    //    int linecount = 0;
    //    foreach (DataRow dr in dtAOPSales.Rows)
    //    {
    //        if (linecount >0)
    //        {
    //            ExcelUploadMsg excelUpload = new ExcelUploadMsg();
    //            excelUpload.Column1 = dr[0] == null ? string.Empty : dr[0].ToString();
    //            excelUpload.Column2 = dr[1] == null ? string.Empty : dr[1].ToString();
    //            excelUpload.Column5 = dr[2] == null ? string.Empty : dr[2].ToString();
    //            excelUpload.Column6 = dr[3] == null ? string.Empty : dr[3].ToString();
    //            excelUpload.Column7 = dr[4] == null ? string.Empty : dr[4].ToString();
    //            excelUpload.Column8 = dr[5] == null ? string.Empty : dr[5].ToString();
    //            excelUpload.Column9 = dr[6] == null ? string.Empty : dr[6].ToString();
    //            excelUploadList.Add(excelUpload);
    //        }
    //        linecount = 1;
    //    }
    //    return excelUploadList;
    //}
    //public ExcelUploadMsg AOPUpload(string[] SplitedData)
    //{
    //    ExcelUploadMsg excelUpload = new ExcelUploadMsg();
    //    excelUpload.Column1 = SplitedData[0] == null ? string.Empty : SplitedData[0].ToString();
    //    excelUpload.Column2 = SplitedData[1] == null ? string.Empty : SplitedData[1].ToString();
    //    excelUpload.Column5 = SplitedData[2] == null ? string.Empty : SplitedData[2].ToString();
    //    excelUpload.Column6 = SplitedData[3] == null ? string.Empty : SplitedData[3].ToString();
    //    excelUpload.Column7 = SplitedData[4] == null ? string.Empty : SplitedData[4].ToString();
    //    excelUpload.Column8 = SplitedData[5] == null ? string.Empty : SplitedData[5].ToString();
    //    excelUpload.Column9 = SplitedData[6] == null ? string.Empty : SplitedData[6].ToString();
    //    return excelUpload;
    //}
    //public ExcelUploadMsg VersionUpload(string[] SplitedData)
    //{
    //    ExcelUploadMsg excelUpload = new ExcelUploadMsg();
    //    excelUpload.Column1 = SplitedData[0] == null ? string.Empty : SplitedData[0].ToString();
    //    excelUpload.Column2 = SplitedData[1] == null ? string.Empty : SplitedData[1].ToString();
    //    excelUpload.Column3 = SplitedData[2] == null ? string.Empty : SplitedData[2].ToString();
    //    excelUpload.Column4 = SplitedData[3] == null ? string.Empty : SplitedData[3].ToString();
    //    excelUpload.Column5 = SplitedData[4] == null ? string.Empty : SplitedData[4].ToString();
    //    excelUpload.Column6 = SplitedData[5] == null ? string.Empty : SplitedData[5].ToString();
    //    excelUpload.Column7 = SplitedData[6] == null ? string.Empty : SplitedData[6].ToString();
    //    excelUpload.Column8 = SplitedData[7] == null ? string.Empty : SplitedData[7].ToString();
    //    excelUpload.Column9 = SplitedData[8] == null ? string.Empty : SplitedData[8].ToString();
    //    excelUpload.Column10 = SplitedData[9] == null ? string.Empty : SplitedData[9].ToString();
    //    excelUpload.Column11 = SplitedData[10] == null ? string.Empty : SplitedData[10].ToString();
    //    excelUpload.Column12 = SplitedData[11] == null ? string.Empty : SplitedData[11].ToString();
    //    excelUpload.Column13 = SplitedData[12] == null ? string.Empty : SplitedData[12].ToString();
    //    if (SplitedData.Count() == 14)
    //    {
    //        excelUpload.Column14 = SplitedData[13] == null ? string.Empty : SplitedData[13].ToString();
    //    }
    //    else
    //    {
    //        excelUpload.Column14 = "0";
    //    }
    //    return excelUpload;
    //}
    //public List<ExcelUploadMsg> VersionUpload(DataTable dtVersion)
    //{
    //    List<ExcelUploadMsg> excelUploadList = new List<ExcelUploadMsg>();
    //    int linecount = 0;
    //    foreach (DataRow dr in dtVersion.Rows)
    //    {
    //        if (linecount > 0)
    //        {
    //            ExcelUploadMsg excelUpload = new ExcelUploadMsg();
    //            excelUpload.Column1 = dr[0] == null ? string.Empty : dr[0].ToString();
    //            excelUpload.Column2 = dr[1] == null ? string.Empty : dr[1].ToString();
    //            excelUpload.Column3 = dr[2] == null ? string.Empty : dr[2].ToString();
    //            excelUpload.Column4 = dr[3] == null ? string.Empty : dr[3].ToString();
    //            excelUpload.Column5 = dr[4] == null ? string.Empty : dr[4].ToString();
    //            excelUpload.Column6 = dr[5] == null ? string.Empty : dr[5].ToString();
    //            excelUpload.Column7 = dr[6] == null ? string.Empty : dr[6].ToString();
    //            excelUpload.Column8 = dr[7] == null ? string.Empty : dr[7].ToString();
    //            excelUpload.Column9 = dr[8] == null ? string.Empty : dr[8].ToString();
    //            excelUpload.Column10 = dr[9] == null ? string.Empty : dr[9].ToString();
    //            excelUpload.Column11 = dr[10] == null ? string.Empty : dr[10].ToString();
    //            excelUpload.Column12 = dr[11] == null ? string.Empty : dr[11].ToString();
    //            excelUpload.Column13 = dr[12] == null ? string.Empty : dr[12].ToString();
    //            if (dr.ItemArray.Count() == 14)
    //            {
    //                excelUpload.Column14 = dr[13] == null ? string.Empty : dr[13].ToString();
    //            }
    //            else
    //            {
    //                excelUpload.Column14 = "0";
    //            }
    //            excelUploadList.Add(excelUpload);
    //        }
    //        linecount = 1;
    //    }
    //    return excelUploadList;
    //}
    //public ExcelUploadMsg ZC020Upload(string[] SplitedData)
    //{
    //    ExcelUploadMsg excelUpload = new ExcelUploadMsg();
    //    excelUpload.Column1 = SplitedData[0] == null ? string.Empty : SplitedData[0].ToString();
    //    excelUpload.Column2 = SplitedData[1] == null ? string.Empty : SplitedData[1].ToString();
    //    excelUpload.Column3 = SplitedData[2] == null ? string.Empty : SplitedData[2].ToString();
    //    excelUpload.Column4 = SplitedData[3] == null ? string.Empty : SplitedData[3].ToString();
    //    excelUpload.Column5 = SplitedData[4] == null ? string.Empty : SplitedData[4].ToString();
    //    excelUpload.Column6 = SplitedData[5] == null ? string.Empty : SplitedData[5].ToString();
    //    excelUpload.Column7 = SplitedData[6] == null ? string.Empty : SplitedData[6].ToString();
    //    excelUpload.Column8 = SplitedData[7] == null ? string.Empty : SplitedData[7].ToString();
    //    excelUpload.Column9 = SplitedData[8] == null ? string.Empty : SplitedData[8].ToString();
    //    excelUpload.Column10 = SplitedData[9] == null ? string.Empty : SplitedData[9].ToString();
    //    excelUpload.Column11 = SplitedData[10] == null ? string.Empty : SplitedData[10].ToString();
    //    excelUpload.Column12 = SplitedData[11] == null ? string.Empty : SplitedData[11].ToString();
    //    excelUpload.Column13 = SplitedData[12] == null ? string.Empty : SplitedData[12].ToString();
    //    excelUpload.Column14 = SplitedData[13] == null ? string.Empty : SplitedData[13].ToString();
    //    excelUpload.Column15 = SplitedData[14] == null ? string.Empty : SplitedData[14].ToString();
    //    excelUpload.Column16 = SplitedData[15] == null ? string.Empty : SplitedData[15].ToString();
    //    excelUpload.Column17 = SplitedData[16] == null ? string.Empty : SplitedData[16].ToString();
    //    excelUpload.Column18 = SplitedData[17] == null ? string.Empty : SplitedData[17].ToString();
    //    excelUpload.Column19 = SplitedData[18] == null ? string.Empty : SplitedData[18].ToString();
    //    excelUpload.Column20 = SplitedData[19] == null ? string.Empty : SplitedData[19].ToString();
    //    excelUpload.Column21 = SplitedData[20] == null ? string.Empty : SplitedData[20].ToString();
    //    excelUpload.Column22 = SplitedData[21] == null ? string.Empty : SplitedData[21].ToString();
    //    return excelUpload;
    //}
    //public List<ExcelUploadMsg> ZC020Upload(DataTable dtZC020)
    //{
    //    List<ExcelUploadMsg> excelUploadList = new List<ExcelUploadMsg>();
    //    foreach (DataRow dr in dtZC020.Rows)
    //    {
    //        if (!dr[2].ToString().StartsWith("R"))
    //        {
    //            ExcelUploadMsg excelUpload = new ExcelUploadMsg();
    //            excelUpload.Column1 = dr[0] == null ? string.Empty : dr[0].ToString();
    //            excelUpload.Column2 = dr[1] == null ? string.Empty : dr[1].ToString();
    //            excelUpload.Column3 = dr[2] == null ? string.Empty : dr[2].ToString();
    //            excelUpload.Column4 = dr[3] == null ? string.Empty : dr[3].ToString();
    //            excelUpload.Column5 = dr[4] == null ? string.Empty : dr[4].ToString();
    //            excelUpload.Column6 = dr[5] == null ? string.Empty : dr[5].ToString();
    //            excelUpload.Column7 = dr[6] == null ? string.Empty : dr[6].ToString();
    //            excelUpload.Column8 = dr[7] == null ? string.Empty : dr[7].ToString();
    //            excelUpload.Column9 = dr[8] == null ? string.Empty : dr[8].ToString();
    //            excelUpload.Column10 = dr[9] == null ? string.Empty : dr[9].ToString();
    //            excelUpload.Column11 = dr[10] == null ? string.Empty : dr[10].ToString();
    //            excelUpload.Column12 = dr[11] == null ? string.Empty : dr[11].ToString();
    //            excelUpload.Column13 = dr[12] == null ? string.Empty : dr[12].ToString();
    //            excelUpload.Column14 = dr[13] == null ? string.Empty : dr[13].ToString();
    //            excelUpload.Column15 = dr[14] == null ? string.Empty : dr[14].ToString();
    //            excelUpload.Column16 = dr[15] == null ? string.Empty : dr[15].ToString();
    //            excelUpload.Column17 = dr[16] == null ? string.Empty : dr[16].ToString();
    //            excelUpload.Column18 = dr[17] == null ? string.Empty : dr[17].ToString();
    //            excelUpload.Column19 = dr[18] == null ? string.Empty : dr[18].ToString();
    //            excelUpload.Column20 = dr[19] == null ? string.Empty : dr[19].ToString();
    //            excelUpload.Column21 = dr[20] == null ? string.Empty : dr[20].ToString();
    //            excelUpload.Column22 = dr[21] == null ? string.Empty : dr[21].ToString();
    //            excelUploadList.Add(excelUpload);
    //        }
    //    }
    //    return excelUploadList;
    //}
    #endregion
 
   
    
}