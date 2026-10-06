using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ChangePasswordMsg
/// </summary>
public class ChangePasswordMsg
{
	public ChangePasswordMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string NewPassword { get; set; }
    public string EmployeeCode { get; set; }
    public string OldPassword { get; set; }
    public string ConfirmPassword { get; set; }
    public string ChangePwdResult { get; set; }
    public string PasswordPolicySet { get; set; }


}
public class PasswordPolicyMsg
{
    public PasswordPolicyMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string PolicyCode { get; set; }
    public string PolicyDescription { get; set; }
    public string CharacterLength { get; set; }
}