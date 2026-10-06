using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for NonComplianceTaskMasterMsg
/// </summary>
public class NonComplianceTaskMasterMsg
{
	public NonComplianceTaskMasterMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int NonComplianceTaskId { get; set; }
    public int ActivityId { get; set; }
    public int ActivityForCompanyId { get; set; }
    public string ActivityName { get; set; }
    public string ComplianceTaskName { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string NonCompResult { get; set; }
    public bool IsActive { get; set; }
}