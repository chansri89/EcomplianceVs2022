using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DepartmentMsg
/// </summary>
public class DepartmentMsg
{
	public DepartmentMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public string DepartmentShortName { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string DeptResult { get; set; }
    public string Flag { get; set; }
    public bool IsActive { get; set; }
}