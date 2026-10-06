using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TranNonComplianceMsg
/// </summary>
public class TranNonComplianceMsg
{
	public TranNonComplianceMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int ActivityActionRootCauseId { get; set; }
    public long ActivityActionId { get; set; }
    public int ActivityForCompanyId { get; set; }
    public string RootCause { get; set; }
    public string CorrectiveAction { get; set; }
    public string PreventiveAction { get; set; }
    public string ApprovedBy { get; set; }
    public string Flag { get; set; }
    public string CreatedBy { get; set; }
    public string NonComplianceResult { get; set; }

    public int ActivityNonComplianceTaskMasterId { get; set; }
    public int ActivityNonComplianceTaskId { get; set; }
    
   
    public string ComplianceTaskName { get; set; }
    public string ExpectedDateOfCompletion { get; set; }
    public string CompletedDate { get; set; }
    public bool IsActive { get; set; }
}