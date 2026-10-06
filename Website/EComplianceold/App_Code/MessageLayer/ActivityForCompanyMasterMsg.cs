using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ActivityForCompanyMasterMsg
/// </summary>
public class ActivityForCompanyMasterMsg
{
	public ActivityForCompanyMasterMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int CompanyActivityId { get; set; }
    public int ActivityId { get; set; }
    public string ActivityName { get; set; }
    public string CompanyCode { get; set; }
    public string CompanyName { get; set; }
    public int FrequencyId { get; set; }
    public string FrequencyName { get; set; }
    public int DueMonth { get; set; }
    public int DueDate { get; set; }
    public string DueDay { get; set; }
    public int TriggerMonth { get; set; }
    public int TriggerDate { get; set; }
    public string TriggerDay { get; set; }
    public string ExecutionEmployeeCode { get; set; }
    public string Executioner { get; set; }
    public string ReviewEmployeeCode { get; set; }
    public string Reviewer { get; set; }
    public string HeadEmployeeCode { get; set; }
    public string HeadEmployeeName { get; set; }
    public string UltimateEmployeeCode { get; set; }
    public string UltimateEmployeeName { get; set; }
    public char ActivityOutsideStackFlag { get; set; }
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string Flag { get; set; }
    public string ActivityCompanyResult { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeCode { get; set; }
    public string Result { get; set; }
    public char NonComplianceTaskAvailableFlag { get; set; }
    public string FrqRemarks { get; set; }
    public int LocationDepartmentId { get; set; }
    public string ResponsibleGroupName { get; set; }
}