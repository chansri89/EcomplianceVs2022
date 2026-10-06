using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmployeeMasterMsg
/// </summary>
public class EmployeeMasterMsg
{
	public EmployeeMasterMsg()
	{
        //
        // TODO: Add constructor logic here
        //
        
	}
    public int ActId { get; set; }
    public string EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
    public string EmailId { get; set; }
    public string Password { get; set; }
    public string ManagerCode { get; set; }
    public string CompanyCode { get; set; }
    public string CompanyName { get; set; }
    public string ManagerName { get; set; }
    public string ActivityDesignation{get;set;}
    public bool IsActive { get; set; }
    public bool IsCompanyAdmin { get; set; }
    public bool IsGroupLevel { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string Flag { get; set; }
    public string EmployeeResult { get; set; }
    public string LoginEmployeeCode { get; set; }// added by abinayaa 020913
}