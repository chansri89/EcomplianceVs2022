using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ActivityAction
/// </summary>
public class ActivityActionMsg
{
	public ActivityActionMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int ActId { get; set; }
    public int ActivityId { get; set; }
    public int ActivityForCompanyId { get; set; }
    public long ActivityActionId { get; set; }
    public int FrequencyId { get; set; }
    public string ActName { get; set; }
    public string ActivityName { get; set; }
    public bool IsStateSpecific { get; set; }
    public bool IsRegularActivity { get; set; }
    public int ReminderYear { get; set; }
    public int ReminderMonth { get; set; }
    public int ReminderDate { get; set; }
    public string ReminderDay { get; set; }
    public int TriggerMonth { get; set; }
    public int TriggerDate { get; set; }
    public string TriggerDay { get; set; }
    public string ExecutionEmployeeCode { get; set; }
    public string HeadEmployeeCode { get; set; }
    public string UltimateEmployeeCode { get; set; }
    public string reviewEmployeeCode { get; set; }
    public string ExecutionEmployeeName { get; set; }
    public string HeadEmployeeName { get; set; }
    public string reviewEmployeeName { get; set; }
    public string UltimateEmployeeName { get; set; }
    public string FrequencyName { get; set; }
    public string FrequencyDays { get; set; }
    public DateTime CompletedDate { get; set; }
    public string ActionResult { get; set; }
    public string CreatedBy { get; set; }
    public string Remarks { get; set; }
    public string Flag { get; set; }
    public string CompanyCode { get; set; }
    public string NonComplianceTaskAvailableFlag { get; set; }
    public bool AsandwhenFlag { get; set; }
    public string SeverityShortName { get; set; }
    public string SeverityName { get; set; }
    public int docAvbl { get; set; }// added by Abinayaa 040213--Check doc is available or not ----
    public string IsdocReq { get; set; }// added by Abinayaa 040213--Check doc is available or not ----

}

public class ActivityYearMsg
{ //SCS040116 solve select click giving error when due year > previous year
    public ActivityYearMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
   
    public int DueYear { get; set; }
    public string YearName { get; set; }
   
   
}