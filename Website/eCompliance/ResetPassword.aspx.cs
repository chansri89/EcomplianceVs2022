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
using Resources;
using Ganini.Security;
public partial class ForgotPassword : System.Web.UI.Page
{
#region Declaration 
    ProcessBus Bus = new ProcessBus();
    List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    EmployeeMasterMsg emp = new EmployeeMasterMsg();
    ChangePasswordMsg  ChangPass = new ChangePasswordMsg();
    public static List<PasswordPolicyMsg> PassPolicyList = new List<PasswordPolicyMsg>();
    BaseClass BaseInfoMsg = new BaseClass();
    KeyGen KeyGen = new KeyGen();
    public static int CapitalCharacter = 0;
    public static int SmallCharacter = 0;
    public static int SpecialCharacter = 0;
    public static int NumberCharacter = 0;
    public static int MaxCharacter = 0;
    public static int MinCharacter = 0;
    public static string PasswordPolicy = ConfigurationManager.AppSettings["PasswordPolicy"].ToString();
#endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!Page.IsPostBack)
        {
            EmployeeLoad();
            ddlEmployeeName.Focus();
            lblMessage.Text = "";
            LoadPasswordPolicy();
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (LogValidation() == 0)
        {
            ResetPwd();
        }

    }
    #endregion
    #region Validation
    public int LogValidation()
    {
        int Error = 0;
        if (ddlEmployeeName.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.ErrUserName + "');", true);
            ddlEmployeeName.Focus();
            Error = 1;
        }
       string Result = IsValidValues();
       if (Result == "")
       {
           Error = 0;
       }
       else
       {
           Error = 1;
           lblMessage.Text = Result;
           //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + Result + "');", true);
       }
        return Error;
    }
   
    #endregion
    #region Methods
    public void EmployeeLoad() //To load data into grid.
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.EmployeeCode = BaseInfoMsg.EmployeeCode; //scs 270714
        Emp.CreatedBy = BaseInfoMsg.EmployeeCode; //scs 23/09/15 empmaster expects it in createdby
        Emp.Flag = "R";
        //EmpList = Bus.MasEmployeeInsertUpdateandDelete(Emp);
        EmpList = Bus.EmployeeMasterUserSelect(Emp); //scs230117 to have companycode for identification
        ddlEmployeeName.DataSource = EmpList;
        ddlEmployeeName.DataTextField = "EmployeeName";
        ddlEmployeeName.DataValueField = "EmployeeCode";
        ddlEmployeeName.DataBind();

    }
    public void ResetPwd()
        {
            string Result;
            emp.EmployeeCode = ddlEmployeeName.SelectedValue;
            emp.LoginEmployeeCode = BaseInfoMsg.EmployeeCode;
            emp.Password = txtPassword.Text;
            emp.Password = KeyGen.EncryptPwd(txtPassword.Text.Trim());
            Result = Bus.AdmResetPaswordUpdateSp(emp);
             ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + Result + "');", true);
        }
    #endregion
    public string IsValidValues()
    {

        int Error = 0;
        int PassLength = 0;
        string ErrorMessage = string.Empty;
        if (txtPassword.Text.Trim() == string.Empty)
        {
            ErrorMessage = ErrorMessage + "New Password should not be Empty." + "\r\n";
            Error = 1;
        }
        else //scs 070116 password policy
        {
            if (PasswordPolicy == "Y")  //if password policy set to Y check
            {
                if (txtPassword.Text.Length < MinCharacter)
                {
                    ErrorMessage = ErrorMessage + "Password length to be greater than " + Convert.ToString(MinCharacter - 1) + " Characters. " + "\r\n";
                    Error = 1;
                }
                if (txtPassword.Text.Length > MaxCharacter)
                {
                    ErrorMessage = ErrorMessage + "Password length to be Less than " + Convert.ToString(MaxCharacter + 1) + " Characters. " + "\r\n";
                    Error = 1;
                }
                int Upper = HasUpperCase(txtPassword.Text);
                PassLength = PassLength + Upper; // to find special character find out Capital, small and numeric and minus from total length scs 050216
                if (Upper < CapitalCharacter)
                {
                    string Upp = "Password Should Contain Atleast " + Convert.ToString(CapitalCharacter) + " UpperCase Character. ";
                    ErrorMessage = ErrorMessage + Upp + "\r\n";
                    Error = 1;

                }
                int Lower = HasLowerCase(txtPassword.Text);
                PassLength = PassLength + Lower; // to find special character find out Capital, small and numeric and minus from total length scs 050216
                if (Lower < SmallCharacter)
                {
                    string Low = "Password Should Contain Atleast " + Convert.ToString(SmallCharacter) + " LowerCase Character. ";
                    ErrorMessage = ErrorMessage + Low + "\r\n";
                    Error = 1;

                }
                int Numer = HasNumeric(txtPassword.Text);
                PassLength = PassLength + Numer; // to find special character find out Capital, small and numeric and minus from total length scs 050216
                if (Numer < NumberCharacter)
                {
                    string Num = "Password Should Contain Atleast " + Convert.ToString(NumberCharacter) + " Numeric Case Character. ";
                    ErrorMessage = ErrorMessage + Num + "\r\n";
                    Error = 1;

                }
                int SpChar = txtPassword.Text.ToString().Length - PassLength;
                if (SpChar < SpecialCharacter)
                {
                    string Spl = "Password Should Contain Atleast " + Convert.ToString(SpecialCharacter) + " Special Character. ";
                    ErrorMessage = ErrorMessage + Spl + "\r\n";
                    Error = 1;
                }
            }
        }


        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + ErrorMessage + "');", true);
        }
        return ErrorMessage;
    }
    private int HasUpperCase(string str)
    {
        int UppChar = 0;
        for (int i = 0; i < str.Length; i++)
        {
            char Wstr = Convert.ToChar(str.Substring(i, 1));
            if (Wstr > 64 && Wstr < 91) // not nummeric 65 STARTING OF CHARCTER A nd 90 is Z
            {
                if (str.Substring(i, 1) == str.Substring(i, 1).ToUpper())
                {
                    UppChar = UppChar + 1;
                }
                //else
                //{
                //    UppChar = 0;
                //}
            }
        }
        return UppChar;
    }
    private int HasLowerCase(string str)
    {
        int Low = 0;
        for (int i = 0; i < str.Length; i++)
        {
            char Wstr = Convert.ToChar(str.Substring(i, 1));
            if (Wstr > 96 && Wstr < 123) //not nummeric 97 STARTING OF CHARCTER a nd 122 is z
            {
                if (str.Substring(i, 1) == str.Substring(i, 1).ToLower())
                {
                    Low = Low + 1;
                }
            }
        }
        return Low;
    }
    private int HasNumeric(string str)
    {
        int Num = 0;
        // string Num = "String Should Contain Atleast one Numeric Character. ";
        for (int i = 0; i < str.Length; i++)
        {
            char Wstr = Convert.ToChar(str.Substring(i, 1));
            if (Wstr > 47 && Wstr < 58) //  nummeric 0 = 48 and 9 = 57
            {
                Num = Num + 1;
            }
        }
        return Num;
    }
    private void LoadPasswordPolicy()
    {
        if (PasswordPolicy == "Y")  //if password policy set to Y check
        {
            PassPolicyList = Bus.AdmPasswordPolicySelect();
            foreach (PasswordPolicyMsg PPM in PassPolicyList)
            {
                if (PPM.PolicyCode.Trim().ToUpper() == "CAC") /// these CAC,SAC etc are harcoded in database hence should not change
                {
                    CapitalCharacter = Convert.ToInt32(PPM.CharacterLength);
                }
                if (PPM.PolicyCode.Trim().ToUpper() == "SAC")
                {
                    SmallCharacter = Convert.ToInt32(PPM.CharacterLength);
                }
                if (PPM.PolicyCode.Trim().ToUpper() == "SPC")
                {
                    SpecialCharacter = Convert.ToInt32(PPM.CharacterLength);
                }
                if (PPM.PolicyCode.Trim().ToUpper() == "NUC")
                {
                    NumberCharacter = Convert.ToInt32(PPM.CharacterLength);
                }
                if (PPM.PolicyCode.Trim() == "MaxPass")
                {
                    MaxCharacter = Convert.ToInt32(PPM.CharacterLength);
                }
                if (PPM.PolicyCode.Trim() == "MinPass")
                {
                    MinCharacter = Convert.ToInt32(PPM.CharacterLength);
                }
            }
            //bind policy grid
            GrdPassPolicy.DataSource = "";
            GrdPassPolicy.DataSource = PassPolicyList;
            GrdPassPolicy.DataBind();
            pnlPassPolicy.Visible = true;
        }
        else
        {
            pnlPassPolicy.Visible = false;
        }
    }




}