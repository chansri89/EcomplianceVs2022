using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ActMasterMsg
/// </summary>
public class LocationDepartmentMasterMsg
{
    public LocationDepartmentMasterMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    public int LocationDeptId { get; set; }
    public string CompanyCode { get; set; }
    public string CompanyName { get; set; }
    public string EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public string ExecutionEmployeeCode { get; set; }
    public string HeadEmployeeCode { get; set; }
    public string UltimateEmployeeCode { get; set; }
    public string reviewEmployeeCode { get; set; }
    public string ExecutionEmployeeName { get; set; }
    public string HeadEmployeeName { get; set; }
    public string reviewEmployeeName { get; set; }
    public string UltimateEmployeeName { get; set; }  
    public string Flag { get; set; }
    public string LocationDeptResult { get; set; }
    public string ResponsibleGrpName { get; set; }
    public bool IsActive { get; set; }
    public int ActivityId { get; set; }
    public int CompanyActivityId { get; set; }
}