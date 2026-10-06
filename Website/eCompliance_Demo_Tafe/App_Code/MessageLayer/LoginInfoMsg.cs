using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LoginInfoMsg
/// </summary>
public class LoginInfoMsg
{
	public LoginInfoMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string UserName { get; set; }
    public string Password { get; set; }
    public string CompanyCode { get; set; }
    public string CompanyName { get; set; }
    public string EmployeeName { get; set; }
    public string Result { get; set; }
    public int UserSessionId { get; set; }
    public string MachineIP { get; set; }
    public string AdminFlag { get; set; }
    public bool IsGroupLevel { get; set; }
    public bool IsCompanyAdmin { get; set; }
}