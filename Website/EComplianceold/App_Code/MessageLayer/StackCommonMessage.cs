using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EComplianceCommonMessage
/// </summary>
public class EComplianceCommonMessage
{
	public EComplianceCommonMessage()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string CompanyCode { get; set; }
    public string CompanyName { get; set; }
    public string EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
    public string EmailId { get; set; }
    public string Password { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public bool IsActive { get; set; }
}