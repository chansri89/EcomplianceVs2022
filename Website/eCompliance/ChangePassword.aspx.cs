using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using Ganini.Security;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Ganini.Lib;


public partial class ChangePassword : System.Web.UI.Page
{
    ProcessBus Bus = new ProcessBus();
    BaseClass AccessClass = new BaseClass();
    List<ChangePasswordMsg> ChangePwdList = new List<ChangePasswordMsg>();
    public static List<PasswordPolicyMsg> PassPolicyList = new List<PasswordPolicyMsg>();
    KeyGen KeyGen = new KeyGen();
    public static string ProgramName = string.Empty;
    public static int CapitalCharacter = 0;
    public static int SmallCharacter = 0;
    public static int SpecialCharacter = 0;
    public static int NumberCharacter = 0;
    public static int MaxCharacter = 0;
    public static int MinCharacter = 0;
    public static string PasswordPolicy = ConfigurationManager.AppSettings["PasswordPolicy"].ToString();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!IsPostBack)
        {
            txtOldPassword.Focus();
            LoadPasswordPolicy();
        }
        txtOldPassword.Focus();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string Result = IsValidValues();
        if (Result == "")
        {
            ChangePwdSave();
           // lblMessage.Text = "New Password Saved. ";
            if (PasswordPolicy == "Y" && AccessClass.PassPolicy != "0") //scs160216 if policy change then logout
            {
                AccessClass.PassPolicy = "0"; 
                LogOut();
            }
            
           
        }
        else
        {
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + Result + "');", true);
            lblMessage.Text = Result;
            txtOldPassword.Focus();
        }
    }

    public string IsValidValues()
    {
        
        int Error = 0;
        int PassLength = 0;
        string ErrorMessage = string.Empty;
        
       
        if (txtOldPassword.Text.Trim() == string.Empty)
        {
            ErrorMessage = ErrorMessage + "Old Password should not be Empty."+"\r\n";
            Error = 1;
        }
        if (txtNewPassword.Text.Trim() == string.Empty)
        {
            ErrorMessage = ErrorMessage + "New Password should not be Empty." + "\r\n";
            Error = 1;
        }
        else //scs 070116 password policy
        {
            if (txtNewPassword.Text.Trim() == txtOldPassword.Text.Trim())
            {
                ErrorMessage = ErrorMessage + "Old and New Password Cannot be Same." + "\r\n";
                Error = 1;
            }
            if (PasswordPolicy == "Y")  //if password policy set to Y check
            {
                if (txtNewPassword.Text.Length < MinCharacter)
                {
                    ErrorMessage = ErrorMessage + "Password length to be greater than " + Convert.ToString(MinCharacter - 1) + " Characters. " + "\r\n";
                    Error = 1;
                }
                if (txtNewPassword.Text.Length > MaxCharacter)
                {
                    ErrorMessage = ErrorMessage + "Password length to be Less than " + Convert.ToString(MaxCharacter + 1) + " Characters. " + "\r\n";
                    Error = 1;
                }
                int Upper = HasUpperCase(txtNewPassword.Text);
                PassLength = PassLength + Upper; // to find special character find out Capital, small and numeric and minus from total length scs 050216
                if (Upper < CapitalCharacter)
                {
                    string Upp = "Password Should Contain Atleast " + Convert.ToString(CapitalCharacter) + " UpperCase Character. ";
                    ErrorMessage = ErrorMessage + Upp + "\r\n";
                    Error = 1;

                }
                int Lower = HasLowerCase(txtNewPassword.Text);
                PassLength = PassLength + Lower; // to find special character find out Capital, small and numeric and minus from total length scs 050216
                if (Lower < SmallCharacter)
                {
                    string Low = "Password Should Contain Atleast " + Convert.ToString(SmallCharacter) + " LowerCase Character. ";
                    ErrorMessage = ErrorMessage + Low + "\r\n";
                    Error = 1;

                }
                int Numer = HasNumeric(txtNewPassword.Text);
                PassLength = PassLength + Numer; // to find special character find out Capital, small and numeric and minus from total length scs 050216
                if (Numer < NumberCharacter)
                {
                    string Num = "Password Should Contain Atleast " + Convert.ToString(NumberCharacter) + " Numeric Case Character. ";
                    ErrorMessage = ErrorMessage + Num + "\r\n";
                    Error = 1;

                }
                int SpChar = txtNewPassword.Text.ToString().Length - PassLength;
                if (SpChar < SpecialCharacter)
                {
                    string Spl = "Password Should Contain Atleast " + Convert.ToString(SpecialCharacter) + " Special Character. ";
                    ErrorMessage = ErrorMessage + Spl + "\r\n";
                    Error = 1;
                }
            }
        }

        if (txtConfirmPassword.Text.Trim() == string.Empty)
        {
            ErrorMessage = ErrorMessage + "Confirm Password should not be Empty. " + "\r\n";
            Error = 1;
        }
        else if(txtConfirmPassword.Text.Trim()!=txtNewPassword.Text.Trim())
        {
            ErrorMessage = ErrorMessage + "New Password and Confirm Password must be same. ";
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + ErrorMessage + "');", true);
        }
        return ErrorMessage;
    }
    private void LogOut()
    {
        LoginInfoMsg Login = new LoginInfoMsg();
        Login.UserName = AccessClass.EmployeeCode;
        Login.UserSessionId = AccessClass.UserSessionId;
        Bus.UpdateUserLogoffInfo(Login);
        Session.Clear();
        Session.RemoveAll();
        string PortalLogin = ConfigurationManager.AppSettings["PortalLogin"].ToString();
        if (PortalLogin != "Y")
        {
            Response.Redirect("~/Login.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Close_Window", "self.close();", true);


        }
    }
    public void ChangePwdSave()
    {
        ChangePasswordMsg ChangePwd = new ChangePasswordMsg();
        //ChangePwd.NewPassword = txtNewPassword.Text;
        //ChangePwd.OldPassword = txtOldPassword.Text;
        ChangePwd.OldPassword = KeyGen.EncryptPwd(txtOldPassword.Text); //03062014 Vinoth Encrypt the old password and pass to DB
        ChangePwd.NewPassword = KeyGen.EncryptPwd(txtNewPassword.Text);
        //ChangePwd.OldPassword =KeyGen.EncryptPwd(txtOldPassword.Text);
        // ChangePwd.ConfirmPassword = txtConfirmPassword.Text;
        ChangePwd.EmployeeCode = AccessClass.EmployeeCode;
        ChangePwd.PasswordPolicySet = PasswordPolicy;
        ChangePwdList = Bus.AdmChangePaswordUpdateSp(ChangePwd);
        foreach (ChangePasswordMsg changepwd in ChangePwdList)
        {
            string PasswordMsg = "";
            if (changepwd.ChangePwdResult == "0")
            {

                if (PasswordPolicy == "Y" && AccessClass.PassPolicy != "0") //reset the policy for having changed the password.
                {
                    
                    PasswordMsg = " -- Please Log Out and Re login again ";
                   
                }

                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.SuccessFullySaved +PasswordMsg+ "');", true);
                ClearALL();
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + changepwd.ChangePwdResult + "');", true);
                break;
            }
        }
    }
    #region Clear
    public void ClearALL()
    {
        txtNewPassword.Text = "";
        txtOldPassword.Text = "";
        txtConfirmPassword.Text = "";
    }
    #endregion
    protected void txtConfirmPassword_TextChanged(object sender, EventArgs e)
    {

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
                    UppChar = UppChar+1;
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
                   Low = Low+1;
               }
           }
       }
       return Low;
   }
   private int  HasNumeric(string str)
   {
       int Num = 0;
      // string Num = "String Should Contain Atleast one Numeric Character. ";
       for (int i = 0; i < str.Length; i++)
       {
           char Wstr = Convert.ToChar(str.Substring(i, 1));
           if (Wstr > 47 && Wstr < 58) //  nummeric 0 = 48 and 9 = 57
           {
               Num = Num+1;
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